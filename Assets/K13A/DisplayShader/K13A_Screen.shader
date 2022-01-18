Shader "K13A_Labs/Screen_RGI" {
	Properties{
	  _Overlay("Overlay (RGB)", 2D) = "black" {}
	  _MainTex("Video RenderTexture (RGB)", 2D) = "black" {}
	  [HideInInspector]_EmissionMap("Emissive (RGB)", 2D) = "black" {}
	  _Emission("Emission Scale", Float) = 1.3
	  _Gamma("Gamma Scale", Float) = 2.2
	  _Mask("Mask (RGB)", 2D) = "white" {}
	  _PixelMask("Pixel Map (RGB)", 2D) = "white" {}

	  _AxisRange("Axis Range", Range(0, 10)) = 1
	}
		SubShader{
		  Tags { "RenderType" = "Opaque" }
		  LOD 200

			CGPROGRAM
	#pragma surface surf Standard fullforwardshadows
	#pragma target 3.5
	#pragma shader_feature _EMISSION
	#pragma multi_compile APPLY_GAMMA_OFF APPLY_GAMMA

		fixed _Emission;
		fixed _Gamma;
		sampler2D _MainTex;
		sampler2D _Overlay;
		sampler2D _EmissionMap;
		sampler2D _Mask;

		sampler2D _PixelMask;

		struct Input {
		  float2 uv_MainTex;
		  float2 uv_Overlay;
		  float2 uv_EmissionMap;
		  float2 uv_PixelMask;

		  float3 viewDir;
		};

		fixed _AxisRange;

		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			void surf(Input IN, inout SurfaceOutputStandard o) {
			fixed4 bg = tex2D(_Overlay, IN.uv_Overlay);

			fixed4 em = tex2D(_EmissionMap, IN.uv_EmissionMap);
			fixed4 e = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = bg;
			o.Alpha = e.a;

			fixed4 m = tex2D(_Mask, IN.uv_MainTex);

	#if APPLY_GAMMA
			e.rgb = pow(e.rgb, _Gamma);
	#endif

			fixed4 p = tex2D(_PixelMask, IN.uv_PixelMask);
			e.r *= p.r;
			e.g *= p.g;
			e.b *= p.b;

			o.Emission = (e * _Emission + em * _Emission) * m * pow(saturate(dot(normalize(IN.viewDir), o.Normal)), _AxisRange);
			o.Metallic = 0;
			o.Smoothness = 0;
		  }
		ENDCG
	  }
		  FallBack "Diffuse"
			CustomEditor "K13A_ScreenEditor"
}