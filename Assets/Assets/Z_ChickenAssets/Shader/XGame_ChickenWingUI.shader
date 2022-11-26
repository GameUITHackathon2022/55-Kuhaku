Shader "XGame/ChickenWingUI" {
	Properties {
		_Brightness ("Brightness", Float) = 1
		_Intensity ("Intensity", Float) = 1
		_Pan_Speed ("Pan_Speed", Float) = 1
		[MaterialToggle] _Gradient_Or_Solid_Color ("Gradient_Or_Solid_Color", Float) = 1
		_Gradient_Color ("Gradient_Color", 2D) = "white" {}
		_Solid_Color ("Solid_Color", Vector) = (0.1764706,0.5229208,1,1)
		_Texture ("Texture", 2D) = "white" {}
		_Gradient_Texture_Decay ("Gradient_Texture_Decay", 2D) = "white" {}
		_Decay ("Decay", Range(0.05, 0.95)) = 0.3
		[MaterialToggle] _Fresnel ("Fresnel", Float) = 1
		[MaterialToggle] _Make_Same_As_Fresnel ("Make_Same_As_Fresnel", Float) = 0
		_Fresnel_Exponent ("Fresnel_Exponent", Float) = 1
		[MaterialToggle] _Edge_Detection_Fake ("Edge_Detection_Fake", Float) = 0.2901961
		_Gradient_Edge_Fake ("Gradient_Edge_Fake", 2D) = "white" {}
		[MaterialToggle] _Soft_Texture ("Soft_Texture", Float) = 1.698039
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255
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