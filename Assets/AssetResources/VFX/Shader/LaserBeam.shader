// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32716,y:32678,varname:node_4795,prsc:2|emission-2393-OUT,alpha-798-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32235,y:32601,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b57b725c3101a0d45beb163a833f7a7d,ntxv:0,isnm:False|UVIN-1403-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-9248-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33081,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:798,x:32495,y:32923,varname:node_798,prsc:2|A-6074-A,B-2053-A,C-797-A;n:type:ShaderForge.SFN_ValueProperty,id:1608,x:31492,y:32604,ptovrint:False,ptlb:MainSpeedX,ptin:_MainSpeedX,varname:node_1608,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Time,id:9861,x:31492,y:32722,varname:node_9861,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7640,x:31643,y:32673,varname:node_7640,prsc:2|A-1608-OUT,B-9861-T;n:type:ShaderForge.SFN_TexCoord,id:4168,x:31593,y:32414,varname:node_4168,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:1117,x:31785,y:32525,varname:node_1117,prsc:2|A-4168-U,B-7640-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9035,x:31503,y:33080,ptovrint:False,ptlb:MainSpeedY,ptin:_MainSpeedY,varname:_MainSpeedX_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Time,id:5804,x:31503,y:33199,varname:node_5804,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5429,x:31654,y:33149,varname:node_5429,prsc:2|A-9035-OUT,B-5804-T;n:type:ShaderForge.SFN_TexCoord,id:1150,x:31604,y:32890,varname:node_1150,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:6982,x:31796,y:33001,varname:node_6982,prsc:2|A-1150-V,B-5429-OUT;n:type:ShaderForge.SFN_Append,id:1403,x:32016,y:32761,varname:node_1403,prsc:2|A-1117-OUT,B-6982-OUT;proporder:6074-797-1608-9035;pass:END;sub:END;*/

Shader "Shader Forge/LaserBeam" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _MainSpeedX ("MainSpeedX", Float ) = 1
        _MainSpeedY ("MainSpeedY", Float ) = 1
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
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _TintColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _MainSpeedX)
                UNITY_DEFINE_INSTANCED_PROP( float, _MainSpeedY)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
////// Lighting:
////// Emissive:
                float _MainSpeedX_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MainSpeedX );
                float4 node_9861 = _Time;
                float _MainSpeedY_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MainSpeedY );
                float4 node_5804 = _Time;
                float2 node_1403 = float2((i.uv0.r+(_MainSpeedX_var*node_9861.g)),(i.uv0.g+(_MainSpeedY_var*node_5804.g)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_1403, _MainTex));
                float4 _TintColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _TintColor );
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*_TintColor_var.rgb*2.0);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_MainTex_var.a*i.vertexColor.a*_TintColor_var.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
