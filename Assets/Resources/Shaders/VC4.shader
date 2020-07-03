// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/VC4"
{
	Properties{
		_MainTex("Base (RGB) Transparency (A)", 2D) = "white" {}
		//_range("Range", Float) = 100
		_Cutoff("Alpha cutoff", Range(0,1)) = 0.5

	}

	SubShader{
			Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
			LOD 300
			

		Pass{
			Cull Off
			ZTest LEqual
			AlphaTest Greater[_Cutoff]
			Blend SrcAlpha OneMinusSrcAlpha

			AlphaToMask On
			

		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 2.0
		//#pragma multi_compile_fog

		#include "UnityCG.cginc"

		struct appdata {
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
		fixed4 color : COLOR;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f {
		float4 vertex : SV_POSITION;
		fixed4 color : COLOR;
		float2 uv : TEXCOORD0;
		UNITY_FOG_COORDS(1)
		UNITY_VERTEX_OUTPUT_STEREO
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;
	fixed _Cutoff;
	//float _range;

	v2f vert(appdata v) {

		v2f o;
		UNITY_SETUP_INSTANCE_ID(v);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.color = v.color;
		//o.color.a = distance(mul(unity_ObjectToWorld, v.vertex), _WorldSpaceCameraPos) / _range;
		o.uv = TRANSFORM_TEX(v.uv,_MainTex)
		UNITY_TRANSFER_FOG(o, o.vertex);

		return o;
	}

	fixed4 frag(v2f i) : SV_Target{ 

	fixed4 col = tex2D(_MainTex, i.uv);
	clip(col.a - _Cutoff);
	UNITY_APPLY_FOG(i.fogCoord, col);

	//float edgeHeight = 0.015;

	fixed4 col2 = i.color;
	//fixed4 col3 = col*col2;

	return col*col2;
	}

	ENDCG 
		//AlphaTest Greater 0.5
		SetTexture[_MainTex]{ combine texture }
	}

	}
}
