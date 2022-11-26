Shader "Shader Forge/FXrongjie" {
	Properties {
		[HDR] _tietuyanse ("tietuyanse", Vector) = (1,1,1,1)
		_tietu ("tietu", 2D) = "white" {}
		_niuqutu ("niuqutu", 2D) = "white" {}
		_rongjie ("rongjie", Range(0, 1.5)) = 0
		_rongjiebianyuan ("rongjiebianyuan", Range(0, 1)) = 0
		[HDR] _bianyuanyanse ("bianyuanyanse", Vector) = (0.5,0.5,0.5,1)
		_fne ("fne", Range(0, 6)) = 1
		[HDR] _node_3394 ("node_3394", Vector) = (0.5,0.5,0.5,1)
		_node_9908 ("node_9908", Range(0, 6.4)) = 0
		_mask ("mask", 2D) = "white" {}
		_mask_02 ("mask_02", 2D) = "white" {}
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