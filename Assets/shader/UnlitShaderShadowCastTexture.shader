Shader "Unlit/UnlitShaderShadowCastTexture"
{
	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}

	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100
		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
#pragma multi_compile_fog

#include "UnityCG.cginc"

		struct appdata_t {
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f {
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
		UNITY_FOG_COORDS(0)
			UNITY_VERTEX_OUTPUT_STEREO
	};

	fixed4 _Color;
	sampler2D _MainTex;
	float4 _MainTex_ST;


	v2f vert(appdata_t v)
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(v);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}

	fixed4 frag(v2f i) : COLOR
	{
		fixed4 col = tex2D(_MainTex, i.uv);
	UNITY_APPLY_FOG(i.fogCoord, col);
	//UNITY_OPAQUE_ALPHA(col.a);
	return col;
	}
		ENDCG
	}

		// Pass to render object as a shadow caster
		Pass
	{
		Name "CastShadow"
		Tags{ "LightMode" = "ShadowCaster" }

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_shadowcaster
#include "UnityCG.cginc"

		struct v2f
	{
		V2F_SHADOW_CASTER;
	};

	v2f vert(appdata_base v)
	{
		v2f o;
		TRANSFER_SHADOW_CASTER(o)
			return o;
	}

	float4 frag(v2f i) : COLOR
	{
		SHADOW_CASTER_FRAGMENT(i)
	}
		ENDCG
	}

	}
}
