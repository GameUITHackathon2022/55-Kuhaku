Shader "Shader Weaver/shuiqiu-11111111" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_Color_ROOT ("Color ROOT", Vector) = (1,1,1,1)
		_MainTex ("_MainTex", 2D) = "white" {}
		_222 ("_222", 2D) = "white" {}
		_Speed ("_Speed", Range(0, 50)) = 0
		_Amount ("_Amount", Range(0, 1)) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Standard"
}