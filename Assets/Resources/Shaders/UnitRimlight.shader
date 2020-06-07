// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33195,y:32734,varname:node_3138,prsc:2|emission-5849-RGB,custl-1782-OUT;n:type:ShaderForge.SFN_Tex2d,id:5849,x:32672,y:32756,varname:node_5849,prsc:2,ntxv:0,isnm:False|TEX-7286-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:7286,x:32468,y:32756,ptovrint:False,ptlb:Main Texture,ptin:_MainTexture,varname:node_7286,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Fresnel,id:80,x:32453,y:32937,varname:node_80,prsc:2|EXP-2910-OUT;n:type:ShaderForge.SFN_Multiply,id:4597,x:32671,y:33017,varname:node_4597,prsc:2|A-80-OUT,B-9748-RGB;n:type:ShaderForge.SFN_Color,id:9748,x:32453,y:33092,ptovrint:False,ptlb:RimLight Color,ptin:_RimLightColor,varname:node_9748,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:2910,x:32263,y:32986,ptovrint:False,ptlb:Rimlight Value,ptin:_RimlightValue,varname:node_2910,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Lerp,id:1782,x:32912,y:32975,varname:node_1782,prsc:2|A-4597-OUT,B-8008-OUT,T-9771-OUT;n:type:ShaderForge.SFN_Slider,id:9771,x:32550,y:33292,ptovrint:False,ptlb:Rimlight Show,ptin:_RimlightShow,varname:node_9771,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1054515,max:1;n:type:ShaderForge.SFN_Vector1,id:8008,x:32671,y:33180,varname:node_8008,prsc:2,v1:0;proporder:7286-9748-2910-9771;pass:END;sub:END;*/

Shader "Shader Forge/UnitRimlight" {
    Properties {
        _MainTexture ("Main Texture", 2D) = "white" {}
        _RimLightColor ("RimLight Color", Color) = (1,0,0,1)
        _RimlightValue ("Rimlight Value", Float ) = 1
        _RimlightShow ("Rimlight Show", Range(0, 1)) = 0.1054515
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
            //#define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            //#pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float4 _RimLightColor;
            uniform float _RimlightValue;
            uniform float _RimlightShow;
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
                float4 node_5849 = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                float3 emissive = node_5849.rgb;
                float node_8008 = 0.0;
                float3 finalColor = emissive + lerp((pow(1.0-max(0,dot(normalDirection, viewDirection)),_RimlightValue)*_RimLightColor.rgb),float3(node_8008,node_8008,node_8008),_RimlightShow);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
