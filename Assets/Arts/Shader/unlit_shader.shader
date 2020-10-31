// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|diff-5954-RGB,spec-5800-OUT,gloss-7203-OUT,emission-2309-RGB,lwrap-3717-OUT,custl-3717-OUT;n:type:ShaderForge.SFN_Tex2d,id:2309,x:32203,y:32452,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:624fa8f566ef74ec6ab04a3ee3c4282f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Fresnel,id:8688,x:31973,y:32921,varname:node_8688,prsc:2|EXP-3975-OUT;n:type:ShaderForge.SFN_Multiply,id:1806,x:32191,y:33001,varname:node_1806,prsc:2|A-8688-OUT,B-889-RGB;n:type:ShaderForge.SFN_Color,id:889,x:31656,y:33307,ptovrint:False,ptlb:RimLight Color,ptin:_RimLightColor,varname:_RimLightColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:3975,x:31783,y:32970,ptovrint:False,ptlb:Rimlight Value,ptin:_RimlightValue,varname:_RimlightValue,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Lerp,id:3717,x:32432,y:32959,varname:node_3717,prsc:2|A-1806-OUT,B-1369-OUT,T-5018-OUT;n:type:ShaderForge.SFN_Slider,id:5018,x:31996,y:33373,ptovrint:False,ptlb:Rimlight Show,ptin:_RimlightShow,varname:_RimlightShow,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Vector1,id:1369,x:32191,y:33164,varname:node_1369,prsc:2,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:5800,x:32200,y:32682,ptovrint:False,ptlb:specular,ptin:_specular,varname:_specular,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:7203,x:32198,y:32810,ptovrint:False,ptlb:Golss,ptin:_Golss,varname:_Golss,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:5954,x:32199,y:32225,ptovrint:False,ptlb:Albedo,ptin:_Albedo,varname:_Albedo,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:624fa8f566ef74ec6ab04a3ee3c4282f,ntxv:0,isnm:False;proporder:2309-5954-889-3975-5018-5800-7203;pass:END;sub:END;*/

Shader "Shader Forge/unlit_shader" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Albedo ("Albedo", 2D) = "white" {}
        _RimLightColor ("RimLight Color", Color) = (1,0,0,1)
        _RimlightValue ("Rimlight Value", Float ) = 1
        _RimlightShow ("Rimlight Show", Range(0, 1)) = 0
        _specular ("specular", Float ) = 1
        _Golss ("Golss", Float ) = 1
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _RimLightColor;
            uniform float _RimlightValue;
            uniform float _RimlightShow;
            uniform float _specular;
            uniform float _Golss;
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 emissive = _MainTex_var.rgb;
                float node_1369 = 0.0;
                float3 node_3717 = lerp((pow(1.0-max(0,dot(normalDirection, viewDirection)),_RimlightValue)*_RimLightColor.rgb),float3(node_1369,node_1369,node_1369),_RimlightShow);
                float3 finalColor = emissive + node_3717;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
