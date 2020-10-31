// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:True,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32827,y:32977,varname:node_2865,prsc:2|diff-6192-OUT,spec-358-OUT,gloss-1813-OUT,normal-5964-RGB,emission-1488-OUT,amdfl-5125-OUT,amspl-2786-OUT;n:type:ShaderForge.SFN_Multiply,id:6343,x:31663,y:32512,varname:node_6343,prsc:2|A-7736-RGB,B-6665-RGB;n:type:ShaderForge.SFN_Color,id:6665,x:31297,y:32433,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7736,x:31304,y:32209,ptovrint:True,ptlb:MainTexture,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:624fa8f566ef74ec6ab04a3ee3c4282f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5964,x:32183,y:33503,ptovrint:True,ptlb:Normal Map,ptin:_BumpMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5ba95d83ff4883649b4eb6572ed8f65e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:358,x:31778,y:33199,ptovrint:False,ptlb:Specular,ptin:_Specular,varname:_Specular,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:-4.199001,max:5;n:type:ShaderForge.SFN_Slider,id:1813,x:31785,y:33393,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:_Gloss,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:1.318471,max:5;n:type:ShaderForge.SFN_Fresnel,id:6161,x:30931,y:33054,varname:node_6161,prsc:2|EXP-7231-OUT;n:type:ShaderForge.SFN_Color,id:2724,x:30763,y:33299,ptovrint:False,ptlb:Glow_Color,ptin:_Glow_Color,varname:_Glow_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:5993,x:31073,y:33187,varname:node_5993,prsc:2|A-6161-OUT,B-2724-RGB;n:type:ShaderForge.SFN_Slider,id:7231,x:30532,y:33106,ptovrint:False,ptlb:Glow_Power,ptin:_Glow_Power,varname:_Glow_Power,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:47.7707,max:100;n:type:ShaderForge.SFN_SwitchProperty,id:1488,x:31146,y:33066,ptovrint:False,ptlb:use_Glow,ptin:_use_Glow,varname:_use_Glow,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-7842-OUT,B-5993-OUT;n:type:ShaderForge.SFN_Vector1,id:7842,x:30997,y:32911,varname:node_7842,prsc:2,v1:0;n:type:ShaderForge.SFN_NormalVector,id:9841,x:31381,y:34046,prsc:2,pt:False;n:type:ShaderForge.SFN_Cubemap,id:9785,x:31822,y:33986,ptovrint:False,ptlb:Diffuse_Ambient_Skybox,ptin:_Diffuse_Ambient_Skybox,varname:_Diffuse_Ambient_Skybox,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,cube:6bf085433a5c14c4a8c64333fba3eb8b,pvfc:1|DIR-9841-OUT;n:type:ShaderForge.SFN_Multiply,id:4246,x:32075,y:33721,varname:node_4246,prsc:2|A-5813-OUT,B-9785-A;n:type:ShaderForge.SFN_Multiply,id:5125,x:32543,y:33416,varname:node_5125,prsc:2|A-4246-OUT,B-9785-RGB;n:type:ShaderForge.SFN_Slider,id:5813,x:31614,y:33766,ptovrint:False,ptlb:Diffuse_Ambient_Light,ptin:_Diffuse_Ambient_Light,varname:_Diffuse_Ambient_Light,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:1.647748,max:10;n:type:ShaderForge.SFN_ViewReflectionVector,id:1895,x:32382,y:33937,varname:node_1895,prsc:2;n:type:ShaderForge.SFN_Cubemap,id:4968,x:32612,y:33937,ptovrint:False,ptlb:Specular_ambient_skybox,ptin:_Specular_ambient_skybox,varname:_Specular_ambient_skybox,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,cube:6bf085433a5c14c4a8c64333fba3eb8b,pvfc:1|DIR-1895-OUT;n:type:ShaderForge.SFN_Slider,id:4081,x:32273,y:34252,ptovrint:False,ptlb:Specular_Ambient_Light,ptin:_Specular_Ambient_Light,varname:_Specular_Ambient_Light,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0,max:10;n:type:ShaderForge.SFN_Multiply,id:7513,x:32831,y:33970,varname:node_7513,prsc:2|A-4968-A,B-4081-OUT;n:type:ShaderForge.SFN_Multiply,id:2786,x:32547,y:33753,varname:node_2786,prsc:2|A-4968-RGB,B-7513-OUT;n:type:ShaderForge.SFN_Blend,id:1848,x:31929,y:32786,varname:node_1848,prsc:2,blmd:10,clmp:True|SRC-6343-OUT,DST-543-RGB;n:type:ShaderForge.SFN_Tex2d,id:543,x:31679,y:32792,ptovrint:False,ptlb:specular_texture,ptin:_specular_texture,varname:_specular_texture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:905ae8c2929177d469050952f5bba91d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_SwitchProperty,id:6192,x:32180,y:32628,ptovrint:False,ptlb:use_specular_texture,ptin:_use_specular_texture,varname:_use_specular_texture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-6343-OUT,B-1848-OUT;n:type:ShaderForge.SFN_Multiply,id:1837,x:33385,y:32095,varname:node_1837,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:9648,x:33164,y:32282,ptovrint:True,ptlb:MainText_copy_copy_copy,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:624fa8f566ef74ec6ab04a3ee3c4282f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Fresnel,id:4241,x:30983,y:33772,varname:node_4241,prsc:2|EXP-4243-OUT;n:type:ShaderForge.SFN_Color,id:7490,x:30815,y:34017,ptovrint:False,ptlb:Glow_Color_copy,ptin:_Glow_Color_copy,varname:_Glow_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:210,x:31125,y:33905,varname:node_210,prsc:2|A-4241-OUT,B-7490-RGB;n:type:ShaderForge.SFN_Slider,id:4243,x:30584,y:33824,ptovrint:False,ptlb:Glow_Power_copy,ptin:_Glow_Power_copy,varname:_Glow_Power_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:47.7707,max:100;n:type:ShaderForge.SFN_Vector1,id:3893,x:31049,y:33629,varname:node_3893,prsc:2,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:9525,x:32839,y:32993,ptovrint:False,ptlb:use_specular_texture_copy,ptin:_use_specular_texture_copy,varname:_use_specular_texture_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-1837-OUT;proporder:5964-6665-7736-6192-543-358-1813-1488-7231-2724-9785-5813-4968-4081;pass:END;sub:END;*/

Shader "Shader Forge/Lit_PBR" {
    Properties {
        _BumpMap ("Normal Map", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("MainTexture", 2D) = "white" {}
        [MaterialToggle] _use_specular_texture ("use_specular_texture", Float ) = 0
        _specular_texture ("specular_texture", 2D) = "white" {}
        _Specular ("Specular", Range(-5, 5)) = -4.199001
        _Gloss ("Gloss", Range(-5, 5)) = 1.318471
        [MaterialToggle] _use_Glow ("use_Glow", Float ) = 0
        _Glow_Power ("Glow_Power", Range(0, 100)) = 47.7707
        _Glow_Color ("Glow_Color", Color) = (1,1,1,1)
        _Diffuse_Ambient_Skybox ("Diffuse_Ambient_Skybox", Cube) = "_Skybox" {}
        _Diffuse_Ambient_Light ("Diffuse_Ambient_Light", Range(-10, 10)) = 1.647748
        _Specular_ambient_skybox ("Specular_ambient_skybox", Cube) = "_Skybox" {}
        _Specular_Ambient_Light ("Specular_Ambient_Light", Range(-10, 10)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
            "CanUseSpriteAtlas"="True"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _Specular;
            uniform float _Gloss;
            uniform float4 _Glow_Color;
            uniform float _Glow_Power;
            uniform fixed _use_Glow;
            uniform samplerCUBE _Diffuse_Ambient_Skybox;
            uniform float _Diffuse_Ambient_Light;
            uniform samplerCUBE _Specular_ambient_skybox;
            uniform float _Specular_Ambient_Light;
            uniform sampler2D _specular_texture; uniform float4 _specular_texture_ST;
            uniform fixed _use_specular_texture;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _BumpMap_var = tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float perceptualRoughness = 1.0 - _Gloss;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float4 _Specular_ambient_skybox_var = texCUBE(_Specular_ambient_skybox,viewReflectDirection);
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = float3(_Specular,_Specular,_Specular);
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_6343 = (_MainTex_var.rgb*_Color.rgb);
                float4 _specular_texture_var = tex2D(_specular_texture,TRANSFORM_TEX(i.uv0, _specular_texture));
                float3 diffuseColor = lerp( node_6343, saturate(( _specular_texture_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_specular_texture_var.rgb-0.5))*(1.0-node_6343)) : (2.0*_specular_texture_var.rgb*node_6343) )), _use_specular_texture ); // Need this for specular when using metallic
                diffuseColor = EnergyConservationBetweenDiffuseAndSpecular(diffuseColor, specularColor, specularMonochrome);
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular + (_Specular_ambient_skybox_var.rgb*(_Specular_ambient_skybox_var.a*_Specular_Ambient_Light)));
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                float4 _Diffuse_Ambient_Skybox_var = texCUBE(_Diffuse_Ambient_Skybox,i.normalDir);
                indirectDiffuse += ((_Diffuse_Ambient_Light*_Diffuse_Ambient_Skybox_var.a)*_Diffuse_Ambient_Skybox_var.rgb); // Diffuse Ambient Light
                indirectDiffuse += gi.indirect.diffuse;
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = lerp( 0.0, (pow(1.0-max(0,dot(normalDirection, viewDirection)),_Glow_Power)*_Glow_Color.rgb), _use_Glow );
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
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _Specular;
            uniform float _Gloss;
            uniform float4 _Glow_Color;
            uniform float _Glow_Power;
            uniform fixed _use_Glow;
            uniform sampler2D _specular_texture; uniform float4 _specular_texture_ST;
            uniform fixed _use_specular_texture;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 _BumpMap_var = tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float perceptualRoughness = 1.0 - _Gloss;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = float3(_Specular,_Specular,_Specular);
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_6343 = (_MainTex_var.rgb*_Color.rgb);
                float4 _specular_texture_var = tex2D(_specular_texture,TRANSFORM_TEX(i.uv0, _specular_texture));
                float3 diffuseColor = lerp( node_6343, saturate(( _specular_texture_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_specular_texture_var.rgb-0.5))*(1.0-node_6343)) : (2.0*_specular_texture_var.rgb*node_6343) )), _use_specular_texture ); // Need this for specular when using metallic
                diffuseColor = EnergyConservationBetweenDiffuseAndSpecular(diffuseColor, specularColor, specularMonochrome);
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Specular;
            uniform float _Gloss;
            uniform float4 _Glow_Color;
            uniform float _Glow_Power;
            uniform fixed _use_Glow;
            uniform sampler2D _specular_texture; uniform float4 _specular_texture_ST;
            uniform fixed _use_specular_texture;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = lerp( 0.0, (pow(1.0-max(0,dot(normalDirection, viewDirection)),_Glow_Power)*_Glow_Color.rgb), _use_Glow );
                
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_6343 = (_MainTex_var.rgb*_Color.rgb);
                float4 _specular_texture_var = tex2D(_specular_texture,TRANSFORM_TEX(i.uv0, _specular_texture));
                float3 diffColor = lerp( node_6343, saturate(( _specular_texture_var.rgb > 0.5 ? (1.0-(1.0-2.0*(_specular_texture_var.rgb-0.5))*(1.0-node_6343)) : (2.0*_specular_texture_var.rgb*node_6343) )), _use_specular_texture );
                float3 specColor = float3(_Specular,_Specular,_Specular);
                float specularMonochrome = max(max(specColor.r, specColor.g),specColor.b);
                diffColor *= (1.0-specularMonochrome);
                float roughness = 1.0 - _Gloss;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
