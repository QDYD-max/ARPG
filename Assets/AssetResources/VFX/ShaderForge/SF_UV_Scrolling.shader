// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|normal-9043-RGB,emission-9043-RGB,voffset-9322-OUT;n:type:ShaderForge.SFN_TexCoord,id:2209,x:31765,y:32855,varname:node_2209,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:9043,x:32366,y:32848,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_9043,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1543940b23281fa468ce0405860760a2,ntxv:0,isnm:False|UVIN-5947-OUT;n:type:ShaderForge.SFN_Multiply,id:9322,x:32541,y:33088,varname:node_9322,prsc:2|A-9043-RGB,B-7107-XYZ;n:type:ShaderForge.SFN_Add,id:6840,x:31972,y:32798,varname:node_6840,prsc:2|A-6618-OUT,B-2209-U;n:type:ShaderForge.SFN_Multiply,id:6618,x:31821,y:32669,varname:node_6618,prsc:2|A-9461-OUT,B-6697-TSL;n:type:ShaderForge.SFN_Time,id:6697,x:31498,y:32858,varname:node_6697,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:9461,x:31498,y:32694,ptovrint:False,ptlb:Xspeed,ptin:_Xspeed,varname:node_9461,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:8555,x:31498,y:33103,ptovrint:False,ptlb:YSpeed,ptin:_YSpeed,varname:_XSpeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Time,id:9608,x:31514,y:33169,varname:node_9608,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7140,x:31791,y:33083,varname:node_7140,prsc:2|A-8555-OUT,B-9608-T;n:type:ShaderForge.SFN_Add,id:1282,x:31972,y:33016,varname:node_1282,prsc:2|A-2209-V,B-7140-OUT;n:type:ShaderForge.SFN_Append,id:5947,x:32155,y:32776,varname:node_5947,prsc:2|A-6840-OUT,B-1282-OUT;n:type:ShaderForge.SFN_Vector4Property,id:7107,x:32312,y:33181,ptovrint:False,ptlb:vectorPower,ptin:_vectorPower,varname:node_7107,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;proporder:9043-9461-8555-7107;pass:END;sub:END;*/

Shader "Shader Forge/SF_UV_Scrolling" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Xspeed ("Xspeed", Float ) = 1
        _YSpeed ("YSpeed", Float ) = 1
        _vectorPower ("vectorPower", Vector) = (0,0,0,0)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Xspeed)
                UNITY_DEFINE_INSTANCED_PROP( float, _YSpeed)
                UNITY_DEFINE_INSTANCED_PROP( float4, _vectorPower)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float3 tangentDir : TEXCOORD2;
                float3 bitangentDir : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float _Xspeed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Xspeed );
                float4 node_6697 = _Time;
                float _YSpeed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _YSpeed );
                float4 node_9608 = _Time;
                float2 node_5947 = float2(((_Xspeed_var*node_6697.r)+o.uv0.r),(o.uv0.g+(_YSpeed_var*node_9608.g)));
                float4 _Diffuse_var = tex2Dlod(_Diffuse,float4(TRANSFORM_TEX(node_5947, _Diffuse),0.0,0));
                float4 _vectorPower_var = UNITY_ACCESS_INSTANCED_PROP( Props, _vectorPower );
                v.vertex.xyz += (_Diffuse_var.rgb*_vectorPower_var.rgb);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float _Xspeed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Xspeed );
                float4 node_6697 = _Time;
                float _YSpeed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _YSpeed );
                float4 node_9608 = _Time;
                float2 node_5947 = float2(((_Xspeed_var*node_6697.r)+i.uv0.r),(i.uv0.g+(_YSpeed_var*node_9608.g)));
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(node_5947, _Diffuse));
                float3 normalLocal = _Diffuse_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
////// Lighting:
////// Emissive:
                float3 emissive = _Diffuse_var.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Xspeed)
                UNITY_DEFINE_INSTANCED_PROP( float, _YSpeed)
                UNITY_DEFINE_INSTANCED_PROP( float4, _vectorPower)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                float _Xspeed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Xspeed );
                float4 node_6697 = _Time;
                float _YSpeed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _YSpeed );
                float4 node_9608 = _Time;
                float2 node_5947 = float2(((_Xspeed_var*node_6697.r)+o.uv0.r),(o.uv0.g+(_YSpeed_var*node_9608.g)));
                float4 _Diffuse_var = tex2Dlod(_Diffuse,float4(TRANSFORM_TEX(node_5947, _Diffuse),0.0,0));
                float4 _vectorPower_var = UNITY_ACCESS_INSTANCED_PROP( Props, _vectorPower );
                v.vertex.xyz += (_Diffuse_var.rgb*_vectorPower_var.rgb);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
