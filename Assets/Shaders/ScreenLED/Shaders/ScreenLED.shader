Shader "K13A_Labs/ScreenLED"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_EmissionRange("Emission", Range(0, 10)) = 1
		_RedMask("Red Mask (RGB)", 2D) = "white" {}
		_GreenMask("Green Mask (RGB)", 2D) = "white" {}
		_BlueMask("Blue Mask (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _RedMask;
		sampler2D _GreenMask;
		sampler2D _BlueMask;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_RedMask;
			float2 uv_GreenMask;
			float2 uv_BlueMask;
        };

        fixed4 _Color;
		fixed _EmissionRange;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 r = tex2D(_RedMask, IN.uv_RedMask);
			fixed4 g = tex2D(_GreenMask, IN.uv_GreenMask);
			fixed4 b = tex2D(_BlueMask, IN.uv_BlueMask);
			c.r *= r;
			c.g *= g;
			c.b *= b;
			o.Albedo = c.rgb;
			o.Emission = c * _EmissionRange;
            // Metallic and smoothness come from slider variables
            o.Alpha = r.a + g.a + b.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
