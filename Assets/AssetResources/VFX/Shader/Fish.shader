Shader "TYX/Other/Fish"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
		_MaskTex("Mask Texture", 2D) = "white" {}
		_VertexValue("_VertexValue",Vector)=(0,0,0,0)
		_SpeedX("SpeedX",Range(0,5))=1
		_SpeedY("SpeedY",Range(0,5))=1
		_SpecularColor ("Specular Color", Color) = (1, 1, 1, 1)
		_Shininess ("Shininess", Range(8.0, 256)) = 20
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
          Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM

			#pragma multi_compile_fwdbase

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
				float3 worldLightDir:TEXCOORD1;//世界坐标系下的指向光源的矢量
                float3 worldNormal:TEXCOORD2;//世界坐标系下法线
                float3 worldViewDir :TEXCOORD3; //世界坐标系下的指向观察者的矢量
				float3 worldPos:TEXCOORD4;
				SHADOW_COORDS(5)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			sampler2D _MaskTex;
            float4 _MaskTex_ST;
			float4 _VertexValue;
			float _SpeedX;
			float _SpeedY;
			fixed4 _SpecularColor;
            float _Shininess;

            v2f vert (appdata v)
            {
                v2f o;

                v.vertex.xyz+= tex2Dlod(_MaskTex, float4(v.uv+float2(_SpeedX,_SpeedY)*_Time.y, 0,0))*_VertexValue.xyz;

				o.worldNormal =  normalize(UnityObjectToWorldNormal(v.normal));
				o.worldLightDir =  normalize(WorldSpaceLightDir(v.vertex));
				o.worldViewDir =  normalize(WorldSpaceViewDir(v.vertex));
				o.worldPos = mul(unity_ObjectToWorld,v.vertex);

				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.pos = UnityObjectToClipPos(v.vertex);

				TRANSFER_SHADOW(o);
				
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				//贴图采样
                fixed3 albedo = tex2D(_MainTex, i.uv);

				//计算环境光
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz*albedo;
	
				 //计算漫反射
                fixed3 diffuse = (_LightColor0.rgb*albedo) * saturate(dot(i.worldNormal,i.worldLightDir));

				//计算高光
				fixed3 halfDir = normalize(i.worldViewDir+i.worldLightDir);
				fixed3 specular = (_SpecularColor.rgb*_LightColor0.rgb)*pow(saturate(dot(halfDir,i.worldNormal)),_Shininess);

				//fixed shadow =SHADOW_ATTENUATION(i);
				UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);

                return fixed4((ambient+(diffuse+specular)*atten),1);
            }
            ENDCG
        }
	Pass { 
			Tags { "LightMode"="ForwardAdd" }
			
			Blend One One //如果没有Blend命令，这个pass会直接覆盖掉之前的光照结果
		
			CGPROGRAM
			
			#pragma multi_compile_fwdadd
			// Use the line below to add shadows for point and spot lights
			//#pragma multi_compile_fwdadd_fullshadows
			
			#pragma vertex vert
			#pragma fragment frag
			
			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			
			 sampler2D _MainTex;
            float4 _MainTex_ST;
			sampler2D _MaskTex;
            float4 _MaskTex_ST;
			float4 _VertexValue;
			float _SpeedX;
			float _SpeedY;
			fixed4 _SpecularColor;
            float _Shininess;
			
			struct a2v {
				float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};
			
			struct v2f {
				float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
				float3 worldLightDir:TEXCOORD1;//世界坐标系下的指向光源的矢量
                float3 worldNormal:TEXCOORD2;//世界坐标系下法线
                float3 worldViewDir :TEXCOORD3; //世界坐标系下的指向观察者的矢量
				float3 worldPos :TEXCOORD4;
				SHADOW_COORDS(5)
			};
			
			v2f vert(a2v v) {
			 	v2f o;
	
                v.vertex.xyz+= tex2Dlod(_MaskTex, float4(v.uv+float2(_SpeedX,_SpeedY)*_Time.y, 0,0))*_VertexValue.xyz;
	
				o.worldNormal =  normalize(UnityObjectToWorldNormal(v.normal));
				o.worldLightDir =  normalize(WorldSpaceLightDir(v.vertex));
				o.worldViewDir =  normalize(WorldSpaceViewDir(v.vertex));
				o.worldPos = mul(unity_ObjectToWorld,v.vertex);
	
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.pos = UnityObjectToClipPos(v.vertex);
	
				TRANSFER_SHADOW(o);
			 	
			 	return o;
			}
			
			fixed4 frag(v2f i) : SV_Target {
				//贴图采样
                fixed3 albedo = tex2D(_MainTex, i.uv);

				//计算环境光
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz*albedo;
	
				 //计算漫反射
                fixed3 diffuse = (_LightColor0.rgb*albedo) * saturate(dot(i.worldNormal,i.worldLightDir));

				//计算高光
				fixed3 halfDir = normalize(i.worldViewDir+i.worldLightDir);
				fixed3 specular = (_SpecularColor.rgb*_LightColor0.rgb)*pow(saturate(dot(halfDir,i.worldNormal)),_Shininess);

				//fixed shadow =SHADOW_ATTENUATION(i);
				UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);

                return fixed4(((diffuse+specular)*atten),1);
			}
			
			ENDCG
		}
	}
	FallBack "Specular"
}
