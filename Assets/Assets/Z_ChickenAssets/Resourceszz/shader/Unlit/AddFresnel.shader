Shader "Unlit/AddFresnel" {
	Properties {
		_MainTexture ("MainTexture", 2D) = "white" {}
		_Glow ("Glow", Float) = 1.5
		_Depth ("Depth", Float) = 0.25
		_FresnelExp ("FresnelExp", Float) = 1
		_FresnelPow ("FresnelPow", Float) = 2
		_Color ("Color", Vector) = (1,1,1,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
	//CustomEditor "ShaderForgeMaterialInspector"
}