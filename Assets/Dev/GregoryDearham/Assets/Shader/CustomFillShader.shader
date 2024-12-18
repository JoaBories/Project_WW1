Shader "Custom/BottomUpFillShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _FillAmount ("Fill Amount", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _FillAmount;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR; // Vertex color to receive SpriteRenderer.color
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR; // Pass color to fragment shader
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color; // Pass the color from the SpriteRenderer
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Discard pixels above the fill threshold (bottom-up)
                if (i.uv.y > _FillAmount) discard;

                // Sample the texture and apply the color tint
                fixed4 texColor = tex2D(_MainTex, i.uv);
                return texColor * i.color; // Multiply texture by SpriteRenderer.color
            }
            ENDCG
        }
    }
}
