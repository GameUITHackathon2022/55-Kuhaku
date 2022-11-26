Shader "Custom/AddPan" {
	Properties {
		_MainTexture ("MainTexture", 2D) = "white" {}
		_Mask ("Mask", 2D) = "white" {}
		_Color ("Color", Vector) = (1,1,1,1)
		_Intensity ("Intensity", Float) = 1
		_U_Speed ("U_Speed", Float) = 0.1
		_V_Speed ("V_Speed", Float) = 0.15
		_MaskPower ("MaskPower", Float) = 4
		_MainPower ("MainPower", Float) = 2
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