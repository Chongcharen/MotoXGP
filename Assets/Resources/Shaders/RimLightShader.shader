// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32982,y:32768,varname:node_4013,prsc:2|diff-5935-RGB,spec-2312-OUT,normal-3416-RGB,emission-5233-OUT;n:type:ShaderForge.SFN_Tex2d,id:5935,x:32537,y:32564,ptovrint:False,ptlb:Albedo,ptin:_Albedo,varname:node_5935,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3416,x:32537,y:32948,ptovrint:False,ptlb:Normal Map,ptin:_NormalMap,varname:node_3416,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:4428,x:32212,y:32694,ptovrint:False,ptlb:Specular,ptin:_Specular,varname:node_4428,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2312,x:32537,y:32764,varname:node_2312,prsc:2|A-4428-RGB,B-6445-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6445,x:32212,y:32919,ptovrint:False,ptlb:Specular Effect,ptin:_SpecularEffect,varname:node_6445,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Fresnel,id:7152,x:32413,y:33151,varname:node_7152,prsc:2|EXP-1824-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1824,x:32192,y:33151,ptovrint:False,ptlb:Fresnel Light Effect,ptin:_FresnelLightEffect,varname:node_1824,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:5233,x:32625,y:33191,varname:node_5233,prsc:2|A-7152-OUT,B-9887-RGB;n:type:ShaderForge.SFN_Color,id:9887,x:32413,y:33323,ptovrint:False,ptlb:Rim Light Color,ptin:_RimLightColor,varname:node_9887,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;proporder:5935-3416-4428-6445-9887-1824;pass:END;sub:END;*/

Shader "Shader Forge/RimLightShader" {
	Properties{
		_Albedo("Albedo", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "bump" {}
		_Specular("Specular", 2D) = "white" {}
		_SpecularEffect("Specular Effect", Float) = 0.5
		_RimLightColor("Rim Light Color", Color) = (1,1,1,1)
		_FresnelLightEffect("Fresnel Light Effect", Float) = 1
		_TintColor("Color", Color) = (1,1,1,1)
	}
		SubShader{
			Tags {
				"RenderType" = "Opaque"
			}
			Pass {
				Name "FORWARD"
				Tags {
					"LightMode" = "ForwardBase"
				}


				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
			//#define UNITY_PASS_FORWARDBASE
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#pragma multi_compile_fwdbase_fullshadows
			#pragma multi_compile_fog
			#pragma target 3.0
			uniform float4 _LightColor0;
			uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
			uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
			uniform sampler2D _Specular; uniform float4 _Specular_ST;
			uniform float _SpecularEffect;
			uniform float _FresnelLightEffect;
			uniform float4 _TintColor;
			uniform float4 _RimLightColor;
			struct VertexInput {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float2 texcoord0 : TEXCOORD0;
			};
			struct VertexOutput {
				float4 pos : SV_POSITION;
				float2 uv0 : TEXCOORD0;
				float4 posWorld : TEXCOORD1;
				float3 normalDir : TEXCOORD2;
				float3 tangentDir : TEXCOORD3;
				float3 bitangentDir : TEXCOORD4;
				LIGHTING_COORDS(5,6)
				UNITY_FOG_COORDS(7)
			};
			VertexOutput vert(VertexInput v) {
				VertexOutput o = (VertexOutput)0;
				o.uv0 = v.texcoord0;
				o.normalDir = UnityObjectToWorldNormal(v.normal);
				o.tangentDir = normalize(mul(unity_ObjectToWorld, float4(v.tangent.xyz, 0.0)).xyz);
				o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				float3 lightColor = _LightColor0.rgb;
				o.pos = UnityObjectToClipPos(v.vertex);
				UNITY_TRANSFER_FOG(o,o.pos);
				TRANSFER_VERTEX_TO_FRAGMENT(o)
				return o;
			}
			float4 frag(VertexOutput i) : COLOR {
				i.normalDir = normalize(i.normalDir);
				float3x3 tangentTransform = float3x3(i.tangentDir, i.bitangentDir, i.normalDir);
				float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
				float3 normalLocal = _NormalMap_var.rgb;
				float3 normalDirection = normalize(mul(normalLocal, tangentTransform)); // Perturbed normals
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				float3 lightColor = _LightColor0.rgb;
				float3 halfDirection = normalize(viewDirection + lightDirection);
				////// Lighting:
								float attenuation = LIGHT_ATTENUATION(i);
								float3 attenColor = attenuation * _LightColor0.xyz;
								///////// Gloss:
												float gloss = 0.5;
												float specPow = exp2(gloss * 10.0 + 1.0);
												////// Specular:
																float NdotL = saturate(dot(normalDirection, lightDirection));
																float4 _Specular_var = tex2D(_Specular,TRANSFORM_TEX(i.uv0, _Specular));
																float3 specularColor = (_Specular_var.rgb*_SpecularEffect);
																float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
																float3 specular = directSpecular;
																/////// Diffuse:
																				NdotL = max(0.0,dot(normalDirection, lightDirection));
																				float3 directDiffuse = max(0.0, NdotL) * attenColor;
																				float3 indirectDiffuse = float3(0,0,0);
																				indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
																				float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
																				float3 diffuseColor = _Albedo_var.rgb;
																				float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
																				////// Emissive:
																								float3 emissive = (pow(1.0 - max(0,dot(normalDirection, viewDirection)),_FresnelLightEffect)*_RimLightColor.rgb);
																								/// Final Color:
																												float3 finalColor = diffuse + specular + emissive;
																												fixed4 finalRGBA = fixed4(finalColor,1);
																												UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
																												return finalRGBA;
																											}
																											ENDCG
																										}
																										Pass {
																											Name "FORWARD_DELTA"
																											Tags {
																												"LightMode" = "ForwardAdd"
																											}
																											Blend One One


																											CGPROGRAM
																											#pragma vertex vert
																											#pragma fragment frag
																												//#define UNITY_PASS_FORWARDADD
																												#include "UnityCG.cginc"
																												#include "AutoLight.cginc"
																												#pragma multi_compile_fwdadd_fullshadows
																												#pragma multi_compile_fog
																												#pragma target 3.0
																												uniform float4 _LightColor0;
																												uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
																												uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
																												uniform sampler2D _Specular; uniform float4 _Specular_ST;
																												uniform float _SpecularEffect;
																												uniform float _FresnelLightEffect;
																												uniform float4 _RimLightColor;
																												struct VertexInput {
																													float4 vertex : POSITION;
																													float3 normal : NORMAL;
																													float4 tangent : TANGENT;
																													float2 texcoord0 : TEXCOORD0;
																												};
																												struct VertexOutput {
																													float4 pos : SV_POSITION;
																													float2 uv0 : TEXCOORD0;
																													float4 posWorld : TEXCOORD1;
																													float3 normalDir : TEXCOORD2;
																													float3 tangentDir : TEXCOORD3;
																													float3 bitangentDir : TEXCOORD4;
																													LIGHTING_COORDS(5,6)
																													UNITY_FOG_COORDS(7)
																												};
																												VertexOutput vert(VertexInput v) {
																													VertexOutput o = (VertexOutput)0;
																													o.uv0 = v.texcoord0;
																													o.normalDir = UnityObjectToWorldNormal(v.normal);
																													o.tangentDir = normalize(mul(unity_ObjectToWorld, float4(v.tangent.xyz, 0.0)).xyz);
																													o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
																													o.posWorld = mul(unity_ObjectToWorld, v.vertex);
																													float3 lightColor = _LightColor0.rgb;
																													o.pos = UnityObjectToClipPos(v.vertex);
																													UNITY_TRANSFER_FOG(o,o.pos);
																													TRANSFER_VERTEX_TO_FRAGMENT(o)
																													return o;
																												}
																												float4 frag(VertexOutput i) : COLOR {
																													i.normalDir = normalize(i.normalDir);
																													float3x3 tangentTransform = float3x3(i.tangentDir, i.bitangentDir, i.normalDir);
																													float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
																													float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
																													float3 normalLocal = _NormalMap_var.rgb;
																													float3 normalDirection = normalize(mul(normalLocal, tangentTransform)); // Perturbed normals
																													float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
																													float3 lightColor = _LightColor0.rgb;
																													float3 halfDirection = normalize(viewDirection + lightDirection);
																													////// Lighting:
																																	float attenuation = LIGHT_ATTENUATION(i);
																																	float3 attenColor = attenuation * _LightColor0.xyz;
																																	///////// Gloss:
																																					float gloss = 0.5;
																																					float specPow = exp2(gloss * 10.0 + 1.0);
																																					////// Specular:
																																									float NdotL = saturate(dot(normalDirection, lightDirection));
																																									float4 _Specular_var = tex2D(_Specular,TRANSFORM_TEX(i.uv0, _Specular));
																																									float3 specularColor = (_Specular_var.rgb*_SpecularEffect);
																																									float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
																																									float3 specular = directSpecular;
																																									/////// Diffuse:
																																													NdotL = max(0.0,dot(normalDirection, lightDirection));
																																													float3 directDiffuse = max(0.0, NdotL) * attenColor;
																																													float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
																																													float3 diffuseColor = _Albedo_var.rgb;
																																													float3 diffuse = directDiffuse * diffuseColor;
																																													/// Final Color:
																																																	float3 finalColor = diffuse + specular;
																																																	fixed4 finalRGBA = fixed4(finalColor * 1,0);
																																																	UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
																																																	return finalRGBA;
																																																}

																																																ENDCG
																																															}
		}
			FallBack "Diffuse"
																																																	CustomEditor "ShaderForgeMaterialInspector"
}