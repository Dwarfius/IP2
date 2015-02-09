Shader "Custom/VisionCone" {
	Properties {
		_Color("Cone Color", Color) = (1,1,1,1)
		_Radius("Cone Radius", float) = 0
	}
	SubShader {
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 200
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			float3 _Color;
			float _Radius;

			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 modelPos : TEXCOORD0;
			};

			vertexOutput vert(appdata_full v) : POSITION
			{
				vertexOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.modelPos = v.vertex;
				return o;
			}

			half4 frag(vertexOutput i) : COLOR
			{
				float len = length(i.modelPos);
				return float4(_Color, 1 - len/_Radius);
			}
			ENDCG
		}
	} 
	FallBack Off
}
