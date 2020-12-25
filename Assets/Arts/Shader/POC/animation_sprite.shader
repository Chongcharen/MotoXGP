// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33337,y:32722,varname:node_3138,prsc:2|emission-9848-OUT,alpha-2445-OUT,clip-7369-OUT;n:type:ShaderForge.SFN_UVTile,id:1645,x:31973,y:32463,varname:node_1645,prsc:2|UVIN-9366-UVOUT,WDT-5938-OUT,HGT-1020-OUT,TILE-968-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5938,x:31604,y:32307,ptovrint:False,ptlb:Width,ptin:_Width,varname:_Width,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_ValueProperty,id:1020,x:31608,y:32433,ptovrint:False,ptlb:Height,ptin:_Height,varname:_Height,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:6362,x:31150,y:32494,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:_Speed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2dAsset,id:9820,x:31979,y:32774,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:_Texture,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ed13b40c50b062248adef5451e02e30f,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8543,x:32201,y:32660,varname:_node_8543,prsc:2,tex:ed13b40c50b062248adef5451e02e30f,ntxv:0,isnm:False|UVIN-1645-UVOUT,TEX-9820-TEX;n:type:ShaderForge.SFN_TexCoord,id:9366,x:31604,y:32143,varname:node_9366,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:414,x:31150,y:32284,varname:node_414,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8039,x:31355,y:32416,varname:node_8039,prsc:2|A-414-T,B-6362-OUT;n:type:ShaderForge.SFN_Floor,id:968,x:31517,y:32533,varname:node_968,prsc:2|IN-8039-OUT;n:type:ShaderForge.SFN_Fmod,id:3650,x:32082,y:33112,varname:node_3650,prsc:2|A-968-OUT,B-7690-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7690,x:31815,y:33253,ptovrint:False,ptlb:Mod,ptin:_Mod,varname:_Mod,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:5676,x:32621,y:32289,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9848,x:33064,y:32482,varname:node_9848,prsc:2|A-8543-RGB,B-5674-RGB,C-6101-RGB;n:type:ShaderForge.SFN_VertexColor,id:5674,x:32478,y:32770,varname:node_5674,prsc:2;n:type:ShaderForge.SFN_Color,id:6101,x:32485,y:32966,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:7369,x:32962,y:32697,varname:node_7369,prsc:2|A-8543-A,B-5674-A,C-2445-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2445,x:32493,y:33241,ptovrint:False,ptlb:Alpha,ptin:_Alpha,varname:_Alpha,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:5938-1020-6362-9820-7690-5676-6101-2445;pass:END;sub:END;*/

Shader "Shader Forge/animation_sprite" {
    Properties {
        _Width ("Width", Float ) = 4
        _Height ("Height", Float ) = 1
        _Speed ("Speed", Float ) = 1
        _Texture ("Texture", 2D) = "black" {}
        _Mod ("Mod", Float ) = 1
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (1,1,1,1)
        _Alpha ("Alpha", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float _Width;
            uniform float _Height;
            uniform float _Speed;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float4 _TintColor;
            uniform float _Alpha;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_414 = _Time;
                float node_968 = floor((node_414.g*_Speed));
                float2 node_1645_tc_rcp = float2(1.0,1.0)/float2( _Width, _Height );
                float node_1645_ty = floor(node_968 * node_1645_tc_rcp.x);
                float node_1645_tx = node_968 - _Width * node_1645_ty;
                float2 node_1645 = (i.uv0 + float2(node_1645_tx, node_1645_ty)) * node_1645_tc_rcp;
                float4 _node_8543 = tex2D(_Texture,TRANSFORM_TEX(node_1645, _Texture));
                clip((_node_8543.a*i.vertexColor.a*_Alpha) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = (_node_8543.rgb*i.vertexColor.rgb*_TintColor.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,_Alpha);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float _Width;
            uniform float _Height;
            uniform float _Speed;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Alpha;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_414 = _Time;
                float node_968 = floor((node_414.g*_Speed));
                float2 node_1645_tc_rcp = float2(1.0,1.0)/float2( _Width, _Height );
                float node_1645_ty = floor(node_968 * node_1645_tc_rcp.x);
                float node_1645_tx = node_968 - _Width * node_1645_ty;
                float2 node_1645 = (i.uv0 + float2(node_1645_tx, node_1645_ty)) * node_1645_tc_rcp;
                float4 _node_8543 = tex2D(_Texture,TRANSFORM_TEX(node_1645, _Texture));
                clip((_node_8543.a*i.vertexColor.a*_Alpha) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
