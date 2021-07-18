// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32702,y:32257,varname:node_4795,prsc:2|emission-2393-OUT,alpha-6906-OUT,voffset-4378-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32162,y:31870,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2393,x:32422,y:32062,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-6074-A;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32170,y:32039,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32170,y:32207,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:6538,x:31089,y:32325,ptovrint:False,ptlb:ASpeed_U,ptin:_ASpeed_U,varname:node_6538,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-1;n:type:ShaderForge.SFN_ValueProperty,id:6015,x:31099,y:32647,ptovrint:False,ptlb:ASpeed_V,ptin:_ASpeed_V,varname:_XSpeed_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Time,id:1198,x:31089,y:32433,varname:node_1198,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8501,x:31376,y:32267,varname:node_8501,prsc:2|A-6538-OUT,B-1198-T;n:type:ShaderForge.SFN_Multiply,id:2906,x:31362,y:32642,varname:node_2906,prsc:2|A-1198-T,B-6015-OUT;n:type:ShaderForge.SFN_TexCoord,id:9003,x:31376,y:32441,varname:node_9003,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:9117,x:31580,y:32301,varname:node_9117,prsc:2|A-8501-OUT,B-9003-U;n:type:ShaderForge.SFN_Add,id:4180,x:31577,y:32622,varname:node_4180,prsc:2|A-9003-V,B-2906-OUT;n:type:ShaderForge.SFN_Append,id:2949,x:31735,y:32473,varname:node_2949,prsc:2|A-9117-OUT,B-4180-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3814,x:31113,y:32908,ptovrint:False,ptlb:BSpeed_U,ptin:_BSpeed_U,varname:_ASpeed_U_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.4;n:type:ShaderForge.SFN_ValueProperty,id:6732,x:31123,y:33230,ptovrint:False,ptlb:BSpeed_V,ptin:_BSpeed_V,varname:_ASpeed_V_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Time,id:2550,x:31113,y:33016,varname:node_2550,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3123,x:31400,y:32850,varname:node_3123,prsc:2|A-3814-OUT,B-2550-T;n:type:ShaderForge.SFN_Multiply,id:7244,x:31386,y:33225,varname:node_7244,prsc:2|A-2550-T,B-6732-OUT;n:type:ShaderForge.SFN_TexCoord,id:285,x:31400,y:33024,varname:node_285,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:3991,x:31604,y:32884,varname:node_3991,prsc:2|A-3123-OUT,B-285-U;n:type:ShaderForge.SFN_Add,id:2563,x:31601,y:33205,varname:node_2563,prsc:2|A-285-V,B-7244-OUT;n:type:ShaderForge.SFN_Append,id:268,x:31759,y:33056,varname:node_268,prsc:2|A-3991-OUT,B-2563-OUT;n:type:ShaderForge.SFN_Tex2d,id:709,x:31938,y:32517,ptovrint:False,ptlb:ATexture,ptin:_ATexture,varname:node_709,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2f7a418a6d445ae41814e2cc41b71edb,ntxv:0,isnm:False|UVIN-2949-OUT;n:type:ShaderForge.SFN_Tex2d,id:2058,x:31950,y:32969,ptovrint:False,ptlb:BTexture,ptin:_BTexture,varname:_ATexture_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2f7a418a6d445ae41814e2cc41b71edb,ntxv:0,isnm:False|UVIN-268-OUT;n:type:ShaderForge.SFN_Multiply,id:5924,x:32113,y:32665,varname:node_5924,prsc:2|A-709-RGB,B-2058-R;n:type:ShaderForge.SFN_NormalVector,id:3940,x:32186,y:32474,prsc:2,pt:True;n:type:ShaderForge.SFN_Multiply,id:4378,x:32463,y:32631,varname:node_4378,prsc:2|A-3940-OUT,B-5924-OUT,C-3270-U;n:type:ShaderForge.SFN_TexCoord,id:3270,x:32232,y:32790,varname:node_3270,prsc:2,uv:1,uaff:True;n:type:ShaderForge.SFN_Multiply,id:6906,x:32403,y:32228,varname:node_6906,prsc:2|A-6074-A,B-797-A;proporder:6074-797-6538-6015-3814-6732-709-2058;pass:END;sub:END;*/

Shader "TYX/RandomRing" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _ASpeed_U ("ASpeed_U", Float ) = -1
        _ASpeed_V ("ASpeed_V", Float ) = 1
        _BSpeed_U ("BSpeed_U", Float ) = 0.4
        _BSpeed_V ("BSpeed_V", Float ) = 2
        _ATexture ("ATexture", 2D) = "white" {}
        _BTexture ("BTexture", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _ATexture; uniform float4 _ATexture_ST;
            uniform sampler2D _BTexture; uniform float4 _BTexture_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _TintColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _ASpeed_U)
                UNITY_DEFINE_INSTANCED_PROP( float, _ASpeed_V)
                UNITY_DEFINE_INSTANCED_PROP( float, _BSpeed_U)
                UNITY_DEFINE_INSTANCED_PROP( float, _BSpeed_V)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 texcoord1 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float _ASpeed_U_var = UNITY_ACCESS_INSTANCED_PROP( Props, _ASpeed_U );
                float4 node_1198 = _Time;
                float _ASpeed_V_var = UNITY_ACCESS_INSTANCED_PROP( Props, _ASpeed_V );
                float2 node_2949 = float2(((_ASpeed_U_var*node_1198.g)+o.uv0.r),(o.uv0.g+(node_1198.g*_ASpeed_V_var)));
                float4 _ATexture_var = tex2Dlod(_ATexture,float4(TRANSFORM_TEX(node_2949, _ATexture),0.0,0));
                float _BSpeed_U_var = UNITY_ACCESS_INSTANCED_PROP( Props, _BSpeed_U );
                float4 node_2550 = _Time;
                float _BSpeed_V_var = UNITY_ACCESS_INSTANCED_PROP( Props, _BSpeed_V );
                float2 node_268 = float2(((_BSpeed_U_var*node_2550.g)+o.uv0.r),(o.uv0.g+(node_2550.g*_BSpeed_V_var)));
                float4 _BTexture_var = tex2Dlod(_BTexture,float4(TRANSFORM_TEX(node_268, _BTexture),0.0,0));
                v.vertex.xyz += (v.normal*(_ATexture_var.rgb*_BTexture_var.r)*o.uv1.r);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _TintColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _TintColor );
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*_TintColor_var.rgb*_MainTex_var.a);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_MainTex_var.a*_TintColor_var.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
