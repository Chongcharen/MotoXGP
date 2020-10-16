// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:2,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33673,y:32793,varname:node_9361,prsc:2|diff-9533-OUT,spec-2670-OUT,gloss-2281-OUT,normal-1391-OUT,amdfl-9488-OUT,amspl-9765-OUT;n:type:ShaderForge.SFN_VertexColor,id:9901,x:31087,y:32809,varname:node_9901,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:7927,x:32231,y:31998,ptovrint:False,ptlb:main_texture,ptin:_main_texture,varname:_main_texture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a768279e746ac694f91b8101cac12efe,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:2670,x:32193,y:36446,ptovrint:False,ptlb:specular,ptin:_specular,varname:_specular,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:0,max:5;n:type:ShaderForge.SFN_Slider,id:2281,x:32223,y:36179,ptovrint:False,ptlb:gloss,ptin:_gloss,varname:_gloss,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-5,cur:0,max:5;n:type:ShaderForge.SFN_Lerp,id:8633,x:32609,y:32205,varname:node_8633,prsc:2|A-7927-RGB,B-4968-RGB,T-9901-R;n:type:ShaderForge.SFN_Tex2d,id:4968,x:32238,y:32248,ptovrint:False,ptlb:r_blend_texture,ptin:_r_blend_texture,varname:_r_blend_texture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a768279e746ac694f91b8101cac12efe,ntxv:0,isnm:False|UVIN-282-OUT;n:type:ShaderForge.SFN_Tex2d,id:7193,x:32608,y:32478,ptovrint:False,ptlb:g_blend_texture,ptin:_g_blend_texture,varname:_g_blend_texture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b66bceaf0cc0ace4e9bdc92f14bba709,ntxv:0,isnm:False|UVIN-9703-OUT;n:type:ShaderForge.SFN_Lerp,id:4509,x:32863,y:32536,varname:node_4509,prsc:2|A-8633-OUT,B-7193-RGB,T-9901-G;n:type:ShaderForge.SFN_Tex2d,id:4869,x:32864,y:32730,ptovrint:False,ptlb:b_blend_texture,ptin:_b_blend_texture,varname:_b_blend_texture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:329856dbe23d6d84b93e3ebbe7841b84,ntxv:0,isnm:False|UVIN-5819-OUT;n:type:ShaderForge.SFN_Lerp,id:9533,x:33100,y:32722,varname:node_9533,prsc:2|A-4509-OUT,B-4869-RGB,T-9901-B;n:type:ShaderForge.SFN_Tex2d,id:9863,x:33096,y:32976,ptovrint:False,ptlb:a_blend_texture,ptin:_a_blend_texture,varname:_a_blend_texture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:97922ae1d3537b94d85165b3a909c3cc,ntxv:0,isnm:False|UVIN-8204-OUT;n:type:ShaderForge.SFN_Lerp,id:167,x:33286,y:32803,varname:node_167,prsc:2|A-9533-OUT,B-9863-RGB,T-344-OUT;n:type:ShaderForge.SFN_Multiply,id:344,x:32939,y:33241,varname:node_344,prsc:2|A-9901-A,B-5535-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5535,x:32772,y:33265,ptovrint:False,ptlb:alpha_value,ptin:_alpha_value,varname:_alpha_value,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:6770,x:32046,y:33179,ptovrint:False,ptlb:Normal_map,ptin:_BumpMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5ba95d83ff4883649b4eb6572ed8f65e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_NormalVector,id:7783,x:32630,y:36401,prsc:2,pt:False;n:type:ShaderForge.SFN_Cubemap,id:2010,x:32895,y:36410,ptovrint:False,ptlb:Diffuse_Ambient_Skybox,ptin:_Diffuse_Ambient_Skybox,varname:_Diffuse_Ambient_Skybox,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,cube:6bf085433a5c14c4a8c64333fba3eb8b,pvfc:1|DIR-7783-OUT;n:type:ShaderForge.SFN_Multiply,id:5645,x:33006,y:36164,varname:node_5645,prsc:2|A-5694-OUT,B-2010-A;n:type:ShaderForge.SFN_Multiply,id:9488,x:33290,y:36215,varname:node_9488,prsc:2|A-5645-OUT,B-2010-RGB;n:type:ShaderForge.SFN_Slider,id:5694,x:32662,y:36178,ptovrint:False,ptlb:Diffuse_Ambient_Light,ptin:_Diffuse_Ambient_Light,varname:_Diffuse_Ambient_Light,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:1.647748,max:10;n:type:ShaderForge.SFN_ViewReflectionVector,id:5568,x:33583,y:36320,varname:node_5568,prsc:2;n:type:ShaderForge.SFN_Cubemap,id:7317,x:33813,y:36320,ptovrint:False,ptlb:Specular_ambient_skybox,ptin:_Specular_ambient_skybox,varname:_Specular_ambient_skybox,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,cube:6bf085433a5c14c4a8c64333fba3eb8b,pvfc:1|DIR-5568-OUT;n:type:ShaderForge.SFN_Slider,id:2525,x:33802,y:36520,ptovrint:False,ptlb:Specular_Ambient_Light,ptin:_Specular_Ambient_Light,varname:_Specular_Ambient_Light,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:0,max:10;n:type:ShaderForge.SFN_Multiply,id:2830,x:34032,y:36353,varname:node_2830,prsc:2|A-7317-A,B-2525-OUT;n:type:ShaderForge.SFN_Multiply,id:9765,x:34212,y:36264,varname:node_9765,prsc:2|A-7317-RGB,B-2830-OUT;n:type:ShaderForge.SFN_Tex2d,id:6864,x:32046,y:33450,ptovrint:False,ptlb:r__Normal_map,ptin:_r__Normal_map,varname:_r__Normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5ba95d83ff4883649b4eb6572ed8f65e,ntxv:0,isnm:False|UVIN-478-OUT;n:type:ShaderForge.SFN_Lerp,id:2774,x:32288,y:33312,varname:node_2774,prsc:2|A-6770-RGB,B-6864-RGB,T-9901-R;n:type:ShaderForge.SFN_TexCoord,id:8681,x:31613,y:32089,varname:node_8681,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:2910,x:31841,y:32406,ptovrint:False,ptlb:red_value,ptin:_red_value,varname:_red_value,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-5;n:type:ShaderForge.SFN_Multiply,id:282,x:32057,y:32263,varname:node_282,prsc:2|A-8681-UVOUT,B-2910-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9621,x:32174,y:32657,ptovrint:False,ptlb:green_value,ptin:_green_value,varname:_green_value,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:9703,x:32411,y:32471,varname:node_9703,prsc:2|A-8681-UVOUT,B-9621-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3474,x:32392,y:32876,ptovrint:True,ptlb:blue_value,ptin:_blue_value,varname:_blue_value,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:5819,x:32608,y:32733,varname:node_5819,prsc:2|A-8681-UVOUT,B-3474-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3642,x:32669,y:33121,ptovrint:True,ptlb:alpha_uv_value,ptin:_alpha_uv_value,varname:_alpha_uv_value,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:8204,x:32885,y:32978,varname:node_8204,prsc:2|A-8681-UVOUT,B-3642-OUT;n:type:ShaderForge.SFN_Tex2d,id:7287,x:32402,y:33870,ptovrint:False,ptlb:g_Normal_map,ptin:_g_Normal_map,varname:_g_Normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5ba95d83ff4883649b4eb6572ed8f65e,ntxv:0,isnm:False|UVIN-2103-OUT;n:type:ShaderForge.SFN_Lerp,id:7968,x:32643,y:33732,varname:node_7968,prsc:2|A-2774-OUT,B-7287-RGB,T-9901-G;n:type:ShaderForge.SFN_Tex2d,id:1048,x:32697,y:34092,ptovrint:False,ptlb:b_Normal_map,ptin:_b_Normal_map,varname:_b_Normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5ba95d83ff4883649b4eb6572ed8f65e,ntxv:0,isnm:False|UVIN-8946-OUT;n:type:ShaderForge.SFN_Lerp,id:7796,x:32912,y:33947,varname:node_7796,prsc:2|A-7968-OUT,B-1048-RGB,T-9901-B;n:type:ShaderForge.SFN_Tex2d,id:386,x:33042,y:34409,ptovrint:False,ptlb:a_Normal_map,ptin:_a_Normal_map,varname:_a_Normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5ba95d83ff4883649b4eb6572ed8f65e,ntxv:0,isnm:False|UVIN-6681-OUT;n:type:ShaderForge.SFN_Lerp,id:1391,x:33283,y:34271,varname:node_1391,prsc:2|A-7796-OUT,B-386-RGB,T-9901-A;n:type:ShaderForge.SFN_ValueProperty,id:5010,x:31609,y:33400,ptovrint:False,ptlb:value_red_normal_map,ptin:_value_red_normal_map,varname:_value_red_normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:478,x:31804,y:33340,varname:node_478,prsc:2|A-8681-UVOUT,B-5010-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6665,x:31800,y:33999,ptovrint:False,ptlb:value_green_normal_map,ptin:_value_green_normal_map,varname:_value_green_normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:2103,x:31995,y:33939,varname:node_2103,prsc:2|A-8681-UVOUT,B-6665-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6430,x:31976,y:34318,ptovrint:False,ptlb:value_blue_normal_map,ptin:_value_blue_normal_map,varname:_value_blue_normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:8946,x:32171,y:34258,varname:node_8946,prsc:2|A-8681-UVOUT,B-6430-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1573,x:32484,y:34569,ptovrint:False,ptlb:value_alpha_normal_map,ptin:_value_alpha_normal_map,varname:_value_alpha_normal_map,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Multiply,id:6681,x:32679,y:34509,varname:node_6681,prsc:2|A-8681-UVOUT,B-1573-OUT;proporder:7927-4968-2910-7193-4869-9863-5535-6770-6864-7287-1048-386-2670-2281-2010-5694-7317-2525-5010-6665-6430-1573-9621-3474-3642;pass:END;sub:END;*/

Shader "Shader Forge/vertex_color" {
    Properties {
        _main_texture ("main_texture", 2D) = "white" {}
        _r_blend_texture ("r_blend_texture", 2D) = "white" {}
        _red_value ("red_value", Float ) = -5
        _g_blend_texture ("g_blend_texture", 2D) = "white" {}
        _b_blend_texture ("b_blend_texture", 2D) = "white" {}
        _a_blend_texture ("a_blend_texture", 2D) = "white" {}
        _alpha_value ("alpha_value", Float ) = 1
        _BumpMap ("Normal_map", 2D) = "white" {}
        _r__Normal_map ("r__Normal_map", 2D) = "white" {}
        _g_Normal_map ("g_Normal_map", 2D) = "white" {}
        _b_Normal_map ("b_Normal_map", 2D) = "white" {}
        _a_Normal_map ("a_Normal_map", 2D) = "white" {}
        _specular ("specular", Range(-5, 5)) = 0
        _gloss ("gloss", Range(-5, 5)) = 0
        _Diffuse_Ambient_Skybox ("Diffuse_Ambient_Skybox", Cube) = "_Skybox" {}
        _Diffuse_Ambient_Light ("Diffuse_Ambient_Light", Range(-10, 10)) = 1.647748
        _Specular_ambient_skybox ("Specular_ambient_skybox", Cube) = "_Skybox" {}
        _Specular_Ambient_Light ("Specular_Ambient_Light", Range(-10, 10)) = 0
        _value_red_normal_map ("value_red_normal_map", Float ) = 10
        _value_green_normal_map ("value_green_normal_map", Float ) = 10
        _value_blue_normal_map ("value_blue_normal_map", Float ) = 10
        _value_alpha_normal_map ("value_alpha_normal_map", Float ) = 10
        _green_value ("green_value", Float ) = 10
        _blue_value ("blue_value", Float ) = 10
        _alpha_uv_value ("alpha_uv_value", Float ) = 10
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _main_texture; uniform float4 _main_texture_ST;
            uniform float _specular;
            uniform float _gloss;
            uniform sampler2D _r_blend_texture; uniform float4 _r_blend_texture_ST;
            uniform sampler2D _g_blend_texture; uniform float4 _g_blend_texture_ST;
            uniform sampler2D _b_blend_texture; uniform float4 _b_blend_texture_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform samplerCUBE _Diffuse_Ambient_Skybox;
            uniform float _Diffuse_Ambient_Light;
            uniform samplerCUBE _Specular_ambient_skybox;
            uniform float _Specular_Ambient_Light;
            uniform sampler2D _r__Normal_map; uniform float4 _r__Normal_map_ST;
            uniform float _red_value;
            uniform float _green_value;
            uniform float _blue_value;
            uniform sampler2D _g_Normal_map; uniform float4 _g_Normal_map_ST;
            uniform sampler2D _b_Normal_map; uniform float4 _b_Normal_map_ST;
            uniform sampler2D _a_Normal_map; uniform float4 _a_Normal_map_ST;
            uniform float _value_red_normal_map;
            uniform float _value_green_normal_map;
            uniform float _value_blue_normal_map;
            uniform float _value_alpha_normal_map;
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
                float3 normalLocal = lerp(lerp(lerp(lerp(_BumpMap_var.rgb,_r__Normal_map_var.rgb,i.vertexColor.r),_g_Normal_map_var.rgb,i.vertexColor.g),_b_Normal_map_var.rgb,i.vertexColor.b),_a_Normal_map_var.rgb,i.vertexColor.a);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _gloss;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float4 _Specular_ambient_skybox_var = texCUBE(_Specular_ambient_skybox,viewReflectDirection);
                float3 specularColor = float3(_specular,_specular,_specular);
                float3 directSpecular = attenColor * pow(max(0,dot(reflect(-lightDirection, normalDirection),viewDirection)),specPow)*specularColor;
                float3 indirectSpecular = (0 + (_Specular_ambient_skybox_var.rgb*(_Specular_ambient_skybox_var.a*_Specular_Ambient_Light)))*specularColor;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _Diffuse_Ambient_Skybox_var = texCUBE(_Diffuse_Ambient_Skybox,i.normalDir);
                indirectDiffuse += ((_Diffuse_Ambient_Light*_Diffuse_Ambient_Skybox_var.a)*_Diffuse_Ambient_Skybox_var.rgb); // Diffuse Ambient Light
                float4 _main_texture_var = tex2D(_main_texture,TRANSFORM_TEX(i.uv0, _main_texture));
                float2 node_282 = (i.uv0*_red_value);
                float4 _r_blend_texture_var = tex2D(_r_blend_texture,TRANSFORM_TEX(node_282, _r_blend_texture));
                float2 node_9703 = (i.uv0*_green_value);
                float4 _g_blend_texture_var = tex2D(_g_blend_texture,TRANSFORM_TEX(node_9703, _g_blend_texture));
                float2 node_5819 = (i.uv0*_blue_value);
                float4 _b_blend_texture_var = tex2D(_b_blend_texture,TRANSFORM_TEX(node_5819, _b_blend_texture));
                float3 node_9533 = lerp(lerp(lerp(_main_texture_var.rgb,_r_blend_texture_var.rgb,i.vertexColor.r),_g_blend_texture_var.rgb,i.vertexColor.g),_b_blend_texture_var.rgb,i.vertexColor.b);
                float3 diffuseColor = node_9533;
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
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _main_texture; uniform float4 _main_texture_ST;
            uniform float _specular;
            uniform float _gloss;
            uniform sampler2D _r_blend_texture; uniform float4 _r_blend_texture_ST;
            uniform sampler2D _g_blend_texture; uniform float4 _g_blend_texture_ST;
            uniform sampler2D _b_blend_texture; uniform float4 _b_blend_texture_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform sampler2D _r__Normal_map; uniform float4 _r__Normal_map_ST;
            uniform float _red_value;
            uniform float _green_value;
            uniform float _blue_value;
            uniform sampler2D _g_Normal_map; uniform float4 _g_Normal_map_ST;
            uniform sampler2D _b_Normal_map; uniform float4 _b_Normal_map_ST;
            uniform sampler2D _a_Normal_map; uniform float4 _a_Normal_map_ST;
            uniform float _value_red_normal_map;
            uniform float _value_green_normal_map;
            uniform float _value_blue_normal_map;
            uniform float _value_alpha_normal_map;
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
                float3 normalLocal = lerp(lerp(lerp(lerp(_BumpMap_var.rgb,_r__Normal_map_var.rgb,i.vertexColor.r),_g_Normal_map_var.rgb,i.vertexColor.g),_b_Normal_map_var.rgb,i.vertexColor.b),_a_Normal_map_var.rgb,i.vertexColor.a);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _gloss;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_specular,_specular,_specular);
                float3 directSpecular = attenColor * pow(max(0,dot(reflect(-lightDirection, normalDirection),viewDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _main_texture_var = tex2D(_main_texture,TRANSFORM_TEX(i.uv0, _main_texture));
                float2 node_282 = (i.uv0*_red_value);
                float4 _r_blend_texture_var = tex2D(_r_blend_texture,TRANSFORM_TEX(node_282, _r_blend_texture));
                float2 node_9703 = (i.uv0*_green_value);
                float4 _g_blend_texture_var = tex2D(_g_blend_texture,TRANSFORM_TEX(node_9703, _g_blend_texture));
                float2 node_5819 = (i.uv0*_blue_value);
                float4 _b_blend_texture_var = tex2D(_b_blend_texture,TRANSFORM_TEX(node_5819, _b_blend_texture));
                float3 node_9533 = lerp(lerp(lerp(_main_texture_var.rgb,_r_blend_texture_var.rgb,i.vertexColor.r),_g_blend_texture_var.rgb,i.vertexColor.g),_b_blend_texture_var.rgb,i.vertexColor.b);
                float3 diffuseColor = node_9533;
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
