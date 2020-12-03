// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32971,y:32779,varname:node_3138,prsc:2|emission-1997-RGB;n:type:ShaderForge.SFN_UVTile,id:1645,x:32073,y:32705,varname:node_1645,prsc:2|UVIN-9366-UVOUT,WDT-5938-OUT,HGT-1020-OUT,TILE-968-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5938,x:31704,y:32549,ptovrint:False,ptlb:Width,ptin:_Width,varname:_Width,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_ValueProperty,id:1020,x:31708,y:32675,ptovrint:False,ptlb:Height,ptin:_Height,varname:_Height,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_ValueProperty,id:6362,x:32122,y:32231,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:_Speed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Tex2dAsset,id:9820,x:32597,y:33192,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:_Texture,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:cd74389c94f3cc14aa95fc41469afc20,ntxv:3,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8543,x:32621,y:32902,varname:_node_8543,prsc:2,tex:cd74389c94f3cc14aa95fc41469afc20,ntxv:0,isnm:False|UVIN-1645-UVOUT,TEX-9820-TEX;n:type:ShaderForge.SFN_TexCoord,id:9366,x:31708,y:32377,varname:node_9366,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:414,x:32122,y:32032,varname:node_414,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8039,x:32327,y:32164,varname:node_8039,prsc:2|A-414-T,B-6362-OUT;n:type:ShaderForge.SFN_Floor,id:968,x:31744,y:32833,varname:node_968,prsc:2|IN-9081-OUT;n:type:ShaderForge.SFN_Fmod,id:3650,x:31987,y:33253,varname:node_3650,prsc:2|A-968-OUT,B-7690-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7690,x:31797,y:33274,ptovrint:False,ptlb:Mod,ptin:_Mod,varname:_Mod,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:16;n:type:ShaderForge.SFN_TexCoord,id:6428,x:32655,y:31704,varname:node_6428,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_UVTile,id:5497,x:32667,y:31892,varname:node_5497,prsc:2|UVIN-6428-UVOUT,WDT-9001-OUT,HGT-2109-OUT,TILE-9249-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9001,x:32411,y:31660,ptovrint:False,ptlb:node_9001,ptin:_node_9001,varname:_node_9001,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_ValueProperty,id:2109,x:32414,y:31782,ptovrint:False,ptlb:node_2109,ptin:_node_2109,varname:_node_2109,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_ValueProperty,id:9971,x:32407,y:31938,ptovrint:False,ptlb:node_9971,ptin:_node_9971,varname:_node_9971,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Tex2d,id:1997,x:32937,y:31865,varname:_node_1997,prsc:2,tex:cd74389c94f3cc14aa95fc41469afc20,ntxv:0,isnm:False|UVIN-5497-UVOUT,TEX-3991-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:3991,x:32696,y:32107,ptovrint:False,ptlb:node_3991,ptin:_node_3991,varname:_node_3991,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:cd74389c94f3cc14aa95fc41469afc20,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:1235,x:31236,y:32790,ptovrint:False,ptlb:Speed_copy,ptin:_Speed_copy,varname:_Speed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Time,id:1281,x:31236,y:32591,varname:node_1281,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9081,x:31441,y:32723,varname:node_9081,prsc:2|A-1281-T,B-1235-OUT;n:type:ShaderForge.SFN_Floor,id:9249,x:32503,y:32167,varname:node_9249,prsc:2|IN-8039-OUT;proporder:5938-1020-6362-9820-1235-9001-2109-3991;pass:END;sub:END;*/

Shader "Shader Forge/animation_sprite2" {
    Properties {
        _Width ("Width", Float ) = 4
        _Height ("Height", Float ) = 4
        _Speed ("Speed", Float ) = 2
        _Texture ("Texture", 2D) = "bump" {}
        _Speed_copy ("Speed_copy", Float ) = 2
        _node_9001 ("node_9001", Float ) = 4
        _node_2109 ("node_2109", Float ) = 4
        _node_3991 ("node_3991", 2D) = "white" {}
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
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _Speed;
            uniform float _node_9001;
            uniform float _node_2109;
            uniform sampler2D _node_3991; uniform float4 _node_3991_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_414 = _Time;
                float node_9249 = floor((node_414.g*_Speed));
                float2 node_5497_tc_rcp = float2(1.0,1.0)/float2( _node_9001, _node_2109 );
                float node_5497_ty = floor(node_9249 * node_5497_tc_rcp.x);
                float node_5497_tx = node_9249 - _node_9001 * node_5497_ty;
                float2 node_5497 = (i.uv0 + float2(node_5497_tx, node_5497_ty)) * node_5497_tc_rcp;
                float4 _node_1997 = tex2D(_node_3991,TRANSFORM_TEX(node_5497, _node_3991));
                float3 emissive = _node_1997.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
