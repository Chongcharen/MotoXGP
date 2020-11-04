// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:True,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33673,y:32793,varname:node_9361,prsc:2|diff-8379-OUT,spec-6671-OUT,gloss-921-OUT,normal-2119-OUT,amdfl-9488-OUT,amspl-9765-OUT;n:type:ShaderForge.SFN_VertexColor,id:9901,x:31028,y:33296,varname:node_9901,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:7927,x:32187,y:31839,ptovrint:False,ptlb:main_texture,ptin:_main_texture,varname:_main_texture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-935-OUT;n:type:ShaderForge.SFN_Lerp,id:8633,x:32609,y:32205,varname:node_8633,prsc:2|A-7927-RGB,B-4968-RGB,T-9901-R;n:type:ShaderForge.SFN_Tex2d,id:4968,x:32238,y:32248,ptovrint:False,ptlb:r_blend_texture,ptin:_r_blend_texture,varname:_r_blend_texture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-282-OUT;n:type:ShaderForge.SFN_Tex2d,id:7193,x:32608,y:32478,ptovrint:False,ptlb:g_blend_texture,ptin:_g_blend_texture,varname:_g_blend_texture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9703-OUT;n:type:ShaderForge.SFN_Lerp,id:4509,x:32863,y:32536,varname:node_4509,prsc:2|A-8633-OUT,B-7193-RGB,T-9901-G;n:type:ShaderForge.SFN_Tex2d,id:4869,x:32864,y:32730,ptovrint:False,ptlb:b_blend_texture,ptin:_b_blend_texture,varname:_b_blend_texture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-5819-OUT;n:type:ShaderForge.SFN_Lerp,id:9533,x:33100,y:32722,varname:node_9533,prsc:2|A-4509-OUT,B-4869-RGB,T-9901-B;n:type:ShaderForge.SFN_Tex2d,id:9863,x:33096,y:32976,ptovrint:False,ptlb:a_blend_texture,ptin:_a_blend_texture,varname:_a_blend_texture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8204-OUT;n:type:ShaderForge.SFN_Lerp,id:167,x:33286,y:32803,varname:node_167,prsc:2|A-9533-OUT,B-9863-RGB,T-344-OUT;n:type:ShaderForge.SFN_Multiply,id:344,x:32967,y:33227,varname:node_344,prsc:2|A-9901-A,B-5535-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5535,x:32665,y:33356,ptovrint:False,ptlb:alpha_value,ptin:_alpha_value,varname:_alpha_value,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:6770,x:31959,y:33680,ptovrint:False,ptlb:Normal_map,ptin:_BumpMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_NormalVector,id:7783,x:32630,y:36401,prsc:2,pt:False;n:type:ShaderForge.SFN_Cubemap,id:2010,x:32895,y:36410,ptovrint:False,ptlb:Diffuse_Ambient_Skybox,ptin:_Diffuse_Ambient_Skybox,varname:_Diffuse_Ambient_Skybox,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,pvfc:1|DIR-7783-OUT;n:type:ShaderForge.SFN_Multiply,id:5645,x:33006,y:36164,varname:node_5645,prsc:2|A-2203-OUT,B-2010-A;n:type:ShaderForge.SFN_Multiply,id:9488,x:33290,y:36215,varname:node_9488,prsc:2|A-5645-OUT,B-2010-RGB;n:type:ShaderForge.SFN_ViewReflectionVector,id:5568,x:33583,y:36320,varname:node_5568,prsc:2;n:type:ShaderForge.SFN_Cubemap,id:7317,x:33813,y:36320,ptovrint:False,ptlb:Specular_ambient_skybox,ptin:_Specular_ambient_skybox,varname:_Specular_ambient_skybox,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,pvfc:1|DIR-5568-OUT;n:type:ShaderForge.SFN_Multiply,id:2830,x:34032,y:36353,varname:node_2830,prsc:2|A-7317-A,B-5105-OUT;n:type:ShaderForge.SFN_Multiply,id:9765,x:34212,y:36264,varname:node_9765,prsc:2|A-7317-RGB,B-2830-OUT;n:type:ShaderForge.SFN_Tex2d,id:6864,x:31959,y:33951,ptovrint:False,ptlb:r__Normal_map,ptin:_r__Normal_map,varname:_r__Normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-478-OUT;n:type:ShaderForge.SFN_Lerp,id:2774,x:32201,y:33813,varname:node_2774,prsc:2|A-6770-RGB,B-6864-RGB,T-9901-R;n:type:ShaderForge.SFN_TexCoord,id:8681,x:31456,y:32609,varname:node_8681,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:2910,x:31841,y:32406,ptovrint:False,ptlb:red_value,ptin:_red_value,varname:_red_value,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:282,x:32057,y:32263,varname:node_282,prsc:2|A-8681-UVOUT,B-2910-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9621,x:32174,y:32657,ptovrint:False,ptlb:green_value,ptin:_green_value,varname:_green_value,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:9703,x:32411,y:32471,varname:node_9703,prsc:2|A-8681-UVOUT,B-9621-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3474,x:32392,y:32876,ptovrint:True,ptlb:blue_value,ptin:_blue_value,varname:_blue_value,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:5819,x:32608,y:32733,varname:node_5819,prsc:2|A-8681-UVOUT,B-3474-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3642,x:32632,y:33075,ptovrint:True,ptlb:alpha_uv_value,ptin:_alpha_uv_value,varname:_alpha_uv_value,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:8204,x:32885,y:32978,varname:node_8204,prsc:2|A-8681-UVOUT,B-3642-OUT;n:type:ShaderForge.SFN_Tex2d,id:7287,x:32315,y:34371,ptovrint:False,ptlb:g_Normal_map,ptin:_g_Normal_map,varname:_g_Normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2103-OUT;n:type:ShaderForge.SFN_Lerp,id:7968,x:32556,y:34233,varname:node_7968,prsc:2|A-2774-OUT,B-7287-RGB,T-9901-G;n:type:ShaderForge.SFN_Tex2d,id:1048,x:32610,y:34593,ptovrint:False,ptlb:b_Normal_map,ptin:_b_Normal_map,varname:_b_Normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8946-OUT;n:type:ShaderForge.SFN_Lerp,id:7796,x:32825,y:34448,varname:node_7796,prsc:2|A-7968-OUT,B-1048-RGB,T-9901-B;n:type:ShaderForge.SFN_Tex2d,id:386,x:32955,y:34910,ptovrint:False,ptlb:a_Normal_map,ptin:_a_Normal_map,varname:_a_Normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-6681-OUT;n:type:ShaderForge.SFN_Lerp,id:1391,x:33196,y:34772,varname:node_1391,prsc:2|A-7796-OUT,B-386-RGB,T-9901-A;n:type:ShaderForge.SFN_ValueProperty,id:5010,x:31496,y:34085,ptovrint:False,ptlb:value_red_normal_map,ptin:_value_red_normal_map,varname:_value_red_normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:478,x:31691,y:34025,varname:node_478,prsc:2|A-8681-UVOUT,B-5010-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6665,x:31713,y:34500,ptovrint:False,ptlb:value_green_normal_map,ptin:_value_green_normal_map,varname:_value_green_normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:2103,x:31908,y:34440,varname:node_2103,prsc:2|A-8681-UVOUT,B-6665-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6430,x:31889,y:34819,ptovrint:False,ptlb:value_blue_normal_map,ptin:_value_blue_normal_map,varname:_value_blue_normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:8946,x:32084,y:34759,varname:node_8946,prsc:2|A-8681-UVOUT,B-6430-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1573,x:32397,y:35070,ptovrint:False,ptlb:value_alpha_normal_map,ptin:_value_alpha_normal_map,varname:_value_alpha_normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:6681,x:32592,y:35010,varname:node_6681,prsc:2|A-8681-UVOUT,B-1573-OUT;n:type:ShaderForge.SFN_Normalize,id:2119,x:33189,y:34182,varname:node_2119,prsc:2|IN-1391-OUT;n:type:ShaderForge.SFN_Multiply,id:176,x:33286,y:32581,varname:node_176,prsc:2|A-167-OUT,B-9901-RGB;n:type:ShaderForge.SFN_ValueProperty,id:9754,x:31516,y:31770,ptovrint:False,ptlb:uv_main_value,ptin:_uv_main_value,varname:_uv_main_value,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:935,x:31763,y:31683,varname:node_935,prsc:2|A-8681-UVOUT,B-9754-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2203,x:32808,y:36116,ptovrint:False,ptlb:ambient_light_value,ptin:_ambient_light_value,varname:_ambient_light_value,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:5105,x:33915,y:36620,ptovrint:False,ptlb:Specular_Ambient_Light,ptin:_Specular_Ambient_Light,varname:_Specular_Ambient_Light,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:921,x:33285,y:33512,ptovrint:False,ptlb:gloss,ptin:_gloss,varname:_gloss,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:6671,x:33283,y:33384,ptovrint:False,ptlb:specular,ptin:_specular,varname:_specular,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Blend,id:8379,x:33666,y:32442,varname:node_8379,prsc:2,blmd:1,clmp:True|SRC-9901-RGB,DST-8633-OUT;proporder:7927-4968-7193-4869-9863-6770-6864-7287-1048-386-2010-7317-9754-2910-9621-3474-5535-3642-6671-921-5010-6665-6430-1573-2203-5105;pass:END;sub:END;*/

Shader "Shader Forge/vertex_color" {
    Properties {
        _main_texture ("main_texture", 2D) = "white" {}
        _r_blend_texture ("r_blend_texture", 2D) = "white" {}
        _g_blend_texture ("g_blend_texture", 2D) = "white" {}
        _b_blend_texture ("b_blend_texture", 2D) = "white" {}
        _a_blend_texture ("a_blend_texture", 2D) = "white" {}
        _BumpMap ("Normal_map", 2D) = "white" {}
        _r__Normal_map ("r__Normal_map", 2D) = "white" {}
        _g_Normal_map ("g_Normal_map", 2D) = "white" {}
        _b_Normal_map ("b_Normal_map", 2D) = "white" {}
        _a_Normal_map ("a_Normal_map", 2D) = "white" {}
        _Diffuse_Ambient_Skybox ("Diffuse_Ambient_Skybox", Cube) = "_Skybox" {}
        _Specular_ambient_skybox ("Specular_ambient_skybox", Cube) = "_Skybox" {}
        _uv_main_value ("uv_main_value", Float ) = 1
        _red_value ("red_value", Float ) = 1
        _green_value ("green_value", Float ) = 10
        _blue_value ("blue_value", Float ) = 10
        _alpha_value ("alpha_value", Float ) = 1
        _alpha_uv_value ("alpha_uv_value", Float ) = 10
        _specular ("specular", Float ) = 0
        _gloss ("gloss", Float ) = 0
        _value_red_normal_map ("value_red_normal_map", Float ) = 10
        _value_green_normal_map ("value_green_normal_map", Float ) = 10
        _value_blue_normal_map ("value_blue_normal_map", Float ) = 10
        _value_alpha_normal_map ("value_alpha_normal_map", Float ) = 10
        _ambient_light_value ("ambient_light_value", Float ) = 1
        _Specular_Ambient_Light ("Specular_Ambient_Light", Float ) = 0
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
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _main_texture; uniform float4 _main_texture_ST;
            uniform sampler2D _r_blend_texture; uniform float4 _r_blend_texture_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform samplerCUBE _Diffuse_Ambient_Skybox;
            uniform samplerCUBE _Specular_ambient_skybox;
            uniform sampler2D _r__Normal_map; uniform float4 _r__Normal_map_ST;
            uniform float _red_value;
            uniform sampler2D _g_Normal_map; uniform float4 _g_Normal_map_ST;
            uniform sampler2D _b_Normal_map; uniform float4 _b_Normal_map_ST;
            uniform sampler2D _a_Normal_map; uniform float4 _a_Normal_map_ST;
            uniform float _value_red_normal_map;
            uniform float _value_green_normal_map;
            uniform float _value_blue_normal_map;
            uniform float _value_alpha_normal_map;
            uniform float _uv_main_value;
            uniform float _ambient_light_value;
            uniform float _Specular_Ambient_Light;
            uniform float _gloss;
            uniform float _specular;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
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
                float2 node_478 = (i.uv0*_value_red_normal_map);
                float4 _r__Normal_map_var = tex2D(_r__Normal_map,TRANSFORM_TEX(node_478, _r__Normal_map));
                float2 node_2103 = (i.uv0*_value_green_normal_map);
                float4 _g_Normal_map_var = tex2D(_g_Normal_map,TRANSFORM_TEX(node_2103, _g_Normal_map));
                float2 node_8946 = (i.uv0*_value_blue_normal_map);
                float4 _b_Normal_map_var = tex2D(_b_Normal_map,TRANSFORM_TEX(node_8946, _b_Normal_map));
                float2 node_6681 = (i.uv0*_value_alpha_normal_map);
                float4 _a_Normal_map_var = tex2D(_a_Normal_map,TRANSFORM_TEX(node_6681, _a_Normal_map));
                float3 normalLocal = normalize(lerp(lerp(lerp(lerp(_BumpMap_var.rgb,_r__Normal_map_var.rgb,i.vertexColor.r),_g_Normal_map_var.rgb,i.vertexColor.g),_b_Normal_map_var.rgb,i.vertexColor.b),_a_Normal_map_var.rgb,i.vertexColor.a));
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
                float gloss = _gloss;
                float perceptualRoughness = 1.0 - _gloss;
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
                float3 specularColor = float3(_specular,_specular,_specular);
                float specularMonochrome;
                float2 node_935 = (i.uv0*_uv_main_value);
                float4 _main_texture_var = tex2D(_main_texture,TRANSFORM_TEX(node_935, _main_texture));
                float2 node_282 = (i.uv0*_red_value);
                float4 _r_blend_texture_var = tex2D(_r_blend_texture,TRANSFORM_TEX(node_282, _r_blend_texture));
                float3 node_8633 = lerp(_main_texture_var.rgb,_r_blend_texture_var.rgb,i.vertexColor.r);
                float3 diffuseColor = saturate((i.vertexColor.rgb*node_8633)); // Need this for specular when using metallic
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
                float3 indirectSpecular = (0 + (_Specular_ambient_skybox_var.rgb*(_Specular_ambient_skybox_var.a*_Specular_Ambient_Light)));
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
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _Diffuse_Ambient_Skybox_var = texCUBE(_Diffuse_Ambient_Skybox,i.normalDir);
                indirectDiffuse += ((_ambient_light_value*_Diffuse_Ambient_Skybox_var.a)*_Diffuse_Ambient_Skybox_var.rgb); // Diffuse Ambient Light
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
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
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _main_texture; uniform float4 _main_texture_ST;
            uniform sampler2D _r_blend_texture; uniform float4 _r_blend_texture_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform sampler2D _r__Normal_map; uniform float4 _r__Normal_map_ST;
            uniform float _red_value;
            uniform sampler2D _g_Normal_map; uniform float4 _g_Normal_map_ST;
            uniform sampler2D _b_Normal_map; uniform float4 _b_Normal_map_ST;
            uniform sampler2D _a_Normal_map; uniform float4 _a_Normal_map_ST;
            uniform float _value_red_normal_map;
            uniform float _value_green_normal_map;
            uniform float _value_blue_normal_map;
            uniform float _value_alpha_normal_map;
            uniform float _uv_main_value;
            uniform float _gloss;
            uniform float _specular;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
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
                float2 node_478 = (i.uv0*_value_red_normal_map);
                float4 _r__Normal_map_var = tex2D(_r__Normal_map,TRANSFORM_TEX(node_478, _r__Normal_map));
                float2 node_2103 = (i.uv0*_value_green_normal_map);
                float4 _g_Normal_map_var = tex2D(_g_Normal_map,TRANSFORM_TEX(node_2103, _g_Normal_map));
                float2 node_8946 = (i.uv0*_value_blue_normal_map);
                float4 _b_Normal_map_var = tex2D(_b_Normal_map,TRANSFORM_TEX(node_8946, _b_Normal_map));
                float2 node_6681 = (i.uv0*_value_alpha_normal_map);
                float4 _a_Normal_map_var = tex2D(_a_Normal_map,TRANSFORM_TEX(node_6681, _a_Normal_map));
                float3 normalLocal = normalize(lerp(lerp(lerp(lerp(_BumpMap_var.rgb,_r__Normal_map_var.rgb,i.vertexColor.r),_g_Normal_map_var.rgb,i.vertexColor.g),_b_Normal_map_var.rgb,i.vertexColor.b),_a_Normal_map_var.rgb,i.vertexColor.a));
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
                float gloss = _gloss;
                float perceptualRoughness = 1.0 - _gloss;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = float3(_specular,_specular,_specular);
                float specularMonochrome;
                float2 node_935 = (i.uv0*_uv_main_value);
                float4 _main_texture_var = tex2D(_main_texture,TRANSFORM_TEX(node_935, _main_texture));
                float2 node_282 = (i.uv0*_red_value);
                float4 _r_blend_texture_var = tex2D(_r_blend_texture,TRANSFORM_TEX(node_282, _r_blend_texture));
                float3 node_8633 = lerp(_main_texture_var.rgb,_r_blend_texture_var.rgb,i.vertexColor.r);
                float3 diffuseColor = saturate((i.vertexColor.rgb*node_8633)); // Need this for specular when using metallic
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
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
