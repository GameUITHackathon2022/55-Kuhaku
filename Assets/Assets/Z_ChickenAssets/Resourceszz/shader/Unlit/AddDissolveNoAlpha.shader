Shader "Unlit/AddDissolveNoAlpha" {
	Properties {
		_MainTexture ("MainTexture", 2D) = "white" {}
		_Glow ("Glow", Float) = 1.5
		_Depth ("Depth", Float) = 0.25
		_Noise ("Noise", 2D) = "white" {}
		_DissolvePower ("DissolvePower", Float) = 2
		_FinalOpacity ("FinalOpacity", Float) = 1
		[HideInInspector] _Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Diffuse"
	//CustomEditor "ShaderForgeMaterialInspector"
}