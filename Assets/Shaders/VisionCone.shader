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

			float3 _Color;
			float _Radius;

			struct appdata {
				float4 vertex : POSITION;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float4 color : COLOR;
			};

			v2f vert(appdata v) : POSITION
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				float dist = length(v.vertex);
				o.color = float4(_Color, (_Radius - dist)/_Radius); //not the best practive, but it works, and GPU doesn't care
				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				return i.color;
			}
			ENDCG
		}
	} 
	FallBack Off
}
