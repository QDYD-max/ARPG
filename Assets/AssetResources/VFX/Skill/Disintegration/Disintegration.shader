Shader "TYX/Disintegration"
{
//https://www.youtube.com/watch?v=ndhFZ7WlWB8
//整体思路就是整个Mesh被分为两部分——模型原先的固定部分和消散的粒子部分，固定部分加溶解效果，消散粒子通过GeometryShader从三角形转成quad，并且可以上贴图
    	Properties
		{
		 _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
        [HDR]_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
        _BumpMap("Normal Map", 2D) = "bump" {}
        _BumpStr("Normal Map Strenght", float) = 1
 
        _FlowMap("Flow (RG)", 2D) = "black" {}
        _DissolveTexture("Dissolve Texutre", 2D) = "white" {}
        _DissolveColor("Dissolve Color Border", Color) = (1, 1, 1, 1) 
        _DissolveBorder("Dissolve Border", float) =  0.05


        _Exapnd("Expand", float) = 1
        _Weight("Weight", Range(0,1)) = 0
        _Direction("Direction", Vector) = (0, 0, 0, 0)
        [HDR]_DisintegrationColor("Disintegration Color", Color) = (1, 1, 1, 1)
        _Glow("Glow", float) = 1

        _Shape("Shape Texutre", 2D) = "white" {} 
        _R("Radius", float) = .1
		}
		CGINCLUDE

			#include "Lighting.cginc"
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			float4 _AmbientColor;
			sampler2D _BumpMap;
			float _BumpStr;
			float _Metallic;
			
			sampler2D _FlowMap;
			float4 _FlowMap_ST;
			sampler2D _DissolveTexture;
			float4 _DissolveColor;
			float _DissolveBorder;
			
			
			float _Exapnd;
			float _Weight;
			float4 _Direction;
			float4 _DisintegrationColor;
			float _Glow;
			sampler2D _Shape;
			float _R;

			struct appdata{
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float2 uv : TEXCOORD0;
			};

			struct v2g{
			    float4 objPos : SV_POSITION;
			    float2 uv : TEXCOORD0;
			    float3 normal : NORMAL;
			    float3 worldPos : TEXCOORD1;
			};

			struct g2f{
			    float4 worldPos : SV_POSITION;
			    float2 uv : TEXCOORD0;
			    fixed4 color : COLOR;
			    float3 normal : NORMAL;
			};

			 v2g vert (appdata v){
			    v2g o;
			    o.objPos = v.vertex;
			    o.uv = v.uv;
			    o.normal = UnityObjectToWorldNormal(v.normal);
			    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
			    return o;
			}	

			float random (float2 uv){
			    return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453123);
			}

			float remap (float value, float from1, float to1, float from2, float to2) {
			    return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
			}

			float randomMapped(float2 uv, float from, float to){
			    return remap(random(uv), 0, 1, from, to);
			}

			float4 remapFlowTexture(float4 tex){
			    return float4(
			        remap(tex.x, 0, 1, -1, 1),
			        remap(tex.y, 0, 1, -1, 1),
			        0,
			        remap(tex.w, 0, 1, -1, 1)
			    );
			}

			//每次调用所允许输出的最大顶点数目
			[maxvertexcount(7)]  
			void geom(triangle v2g IN[3] : SV_POSITION,inout TriangleStream<g2f>triStream)
			{	
				float3 avgPos = (IN[0].objPos+IN[1].objPos +IN[2].objPos)/3;
				float2 avgUV = (IN[0].uv + IN[1].uv + IN[2].uv)/3;
				float3 avgNormal = (IN[0].normal + IN[1].normal + IN[2].normal) / 3;
				//增加dissolve贴图，让mesh根据贴图r通道在不同的时间分离
				float dissolve = tex2Dlod(_DissolveTexture,float4(avgUV,0,0)).r;
				float t = saturate(_Weight*2-dissolve);

				float2 flowUV = TRANSFORM_TEX(mul(unity_ObjectToWorld,avgPos).xz,_FlowMap);
				float4 flowVector = remapFlowTexture(tex2Dlod(_FlowMap,float4(flowUV,0,0)));//要让三角面破碎的方向按照flowmap取随机

				float3 pseudoRandomPos = (avgPos) + _Direction;  //伪随机位，用_Direction控制方向，_Exapnd控制长度,为了让其消亡需要将其汇聚到一点，即pseudoRandomPos
				pseudoRandomPos += (flowVector.xyz * _Exapnd);

				//为每一个分离出来的三角形创建quad
				float3 p =  lerp(avgPos, pseudoRandomPos, t);
				float radius = lerp(_R, 0, t);

				if(t > 0){
				    float3 look = _WorldSpaceCameraPos - p;
				    look = normalize(look);

					//下面是广告牌技术
					//MV transforms points from object to eye space,IT_MV rotates normals from object to eye space
				    float3 right = UNITY_MATRIX_IT_MV[0].xyz;
				    float3 up = UNITY_MATRIX_IT_MV[1].xyz;

				    float halfS = 0.5f * radius;

				    float4 v[4];
				    v[0] = float4(p + halfS * right - halfS * up, 1.0f);  //这四行就是将他转换到正方形的四个角上
				    v[1] = float4(p + halfS * right + halfS * up, 1.0f);
				    v[2] = float4(p - halfS * right - halfS * up, 1.0f);
				    v[3] = float4(p - halfS * right + halfS * up, 1.0f);
				    

				    g2f vert;
				    vert.worldPos = UnityObjectToClipPos(v[0]);
				    vert.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, float2(1.0f, 0.0f));  //等价于o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				    vert.color = float4(1, 1, 1, 1);
				    vert.normal = avgNormal;
				    triStream.Append(vert);

					vert.worldPos = UnityObjectToClipPos(v[1]);
					vert.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, float2(1.0f, 1.0f));
					vert.color = float4(1, 1, 1, 1);
					vert.normal = avgNormal;
					triStream.Append(vert);

					vert.worldPos =UnityObjectToClipPos(v[2]);
					vert.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, float2(0.0f, 0.0f));
					vert.color = float4(1, 1, 1, 1);
					vert.normal = avgNormal;
					triStream.Append(vert);

					vert.worldPos = UnityObjectToClipPos(v[3]);
					vert.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, float2(0.0f, 1.0f));
					vert.color = float4(1, 1, 1, 1);
					vert.normal = avgNormal;
					triStream.Append(vert);

					triStream.RestartStrip();
				}

				for(int j=0;j<3;j++)
				{
					g2f o;

					o.worldPos = UnityObjectToClipPos(IN[j].objPos);  //保留原始点，这些点会在后面根据dissolve贴图部分溶解
					o.uv = TRANSFORM_TEX(IN[j].uv,_MainTex);
					o.color = fixed4(0,0,0,0);
					o.normal = IN[j].normal;
					triStream.Append(o);

				}
				 triStream.RestartStrip();
				//float3 p = IN[i].objPos;
				//p = lerp(IN[i].objPos,targetPos,t);


				//流输出对象都具有下面两种方法：
				//1.Append 向指定的流输出对象添加一个输出的数据
				//2.RestartStrip 在以线段或者三角形作为图元的时候，默认是以strip的形式输出的，

				//如果我们不希望下一个输出的顶点与之前的顶点构成新图元，则需要调用此方法来重新开始新的strip。
				//若希望输出的图元类型也保持和原来一样的TriangleList，则需要每调用3次Append方法后就调用一次RestartStrip
				}
		ENDCG

		SubShader
		{

        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Off

        Pass{

            Tags { 
                "LightMode" = "ForwardBase"
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom
            #pragma multi_compile_fwdbase
           
		    fixed4 frag (g2f i) : SV_Target
			{
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                
                float3 normal = normalize(i.normal);
                half3 tnormal = UnpackNormal(tex2D(_BumpMap, i.uv));
                tnormal.xy *= _BumpStr;
                tnormal = normalize(tnormal);

                float NdotL = dot(_WorldSpaceLightPos0, normal * tnormal);
                float4 light = NdotL * _LightColor0;
                col *= (_AmbientColor + light);
               
                float brightness = i.color.w  * _Glow;
                col = lerp(col, _DisintegrationColor,  i.color.x);

                if(brightness > 0){
                    col *= brightness + _Weight;
                }


                float dissolve = tex2D(_DissolveTexture, i.uv).r;
                
                if(i.color.w == 0){
                    clip(dissolve - 2*_Weight);
                    if(_Weight > 0){
                        col +=  _DissolveColor * _Glow * step( dissolve - 2*_Weight, _DissolveBorder);
                    }
                }else{
                    float s = tex2D(_Shape, i.uv).r;
                    if(s < .5) {
                        discard;
                    }

                }

                return col;
            }
            ENDCG
        }
		}
}
