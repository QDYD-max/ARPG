Shader "TYX/AddTwoSide"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		[HDR]_TintColor("Tint Color",color)=(1,1,1,1)
    }
    SubShader
    {
		Tags{"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		ZWrite Off
		Cull Off

        Pass
        {
		Blend One One

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

             struct appdata {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float4 texcoords : TEXCOORD0;     
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float4 color:COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _TintColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoords, _MainTex);
				o.color = v.color *_TintColor;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample the texture
				float4 tex = tex2D(_MainTex, i.uv);
                float3 col = tex.rgb * i.color *_TintColor.a * tex.a ;
                return float4( col , 1);
            }
            ENDCG
        }
    }
}
