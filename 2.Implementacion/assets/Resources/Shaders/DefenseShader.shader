Shader "Custom/DefenseShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _JacketColor("Jacket Color", Color) = (0.125, 0.125, 0.125)
        _TargetJacketColor("Target Jacket Color", Color) = (0.0667, 0.4549, 0.7333)
        _PantsColor("Pants Color", Color) = (0.0, 0.0863, 0.2196)
        _TargetPantsColor("Target Pants Color", Color) = (0.302, 0.031, 0.0)
        _PantsHighlightColor("Pants Highlight Color", Color) = (0.0353, 0.1294, 0.2706)
        _TargetPantsHighlightColor("Target Pants Highlight Color", Color) = (0.37, 0.0353, 0.0)
        _Progress("Progress", Range(0, 1)) = 0
        _Tolerance("Tolerance", Range(0, 0.01)) = 0.001  
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _JacketColor;
            float4 _TargetJacketColor;
            float4 _PantsColor;
            float4 _TargetPantsColor;
            float4 _PantsHighlightColor;
            float4 _TargetPantsHighlightColor;
            float _Progress; 

            float _Tolerance;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);

                if (col.a == 0)
                {
                    return half4(0, 0, 0, 0);
                }

                float progressThreshold = 0.77 + (_Progress * (0.94 - 0.77)); 

                if (i.uv.y <= progressThreshold)
                {
                    if (length(col - _JacketColor) < _Tolerance)
                    {
                        col = _TargetJacketColor;
                    }

                    if (length(col - _PantsColor) < _Tolerance)
                    {
                        col = _TargetPantsColor;
                    }

                    if (length(col - _PantsHighlightColor) < _Tolerance)
                    {
                        col = _TargetPantsHighlightColor;
                    }
                }

                col *= i.color;

                return col;
            }

            ENDCG
        }
    }
}
