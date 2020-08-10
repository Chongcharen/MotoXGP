// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32033,y:33304,varname:node_2865,prsc:2|emission-5015-RGB,alpha-7228-OUT;n:type:ShaderForge.SFN_VertexColor,id:7198,x:31459,y:33612,varname:node_7198,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:4966,x:30791,y:32954,varname:node_4966,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Divide,id:637,x:30869,y:33108,varname:node_637,prsc:2|A-4966-UVOUT,B-340-OUT;n:type:ShaderForge.SFN_Add,id:7016,x:30869,y:33287,varname:node_7016,prsc:2|A-637-OUT,B-3437-OUT;n:type:ShaderForge.SFN_Append,id:340,x:30624,y:33119,varname:node_340,prsc:2|A-76-OUT,B-2615-OUT;n:type:ShaderForge.SFN_Append,id:3437,x:30852,y:33440,varname:node_3437,prsc:2|A-6394-OUT,B-3457-OUT;n:type:ShaderForge.SFN_OneMinus,id:3457,x:30698,y:33573,cmnt:toptobutton,varname:node_3457,prsc:2|IN-5702-OUT;n:type:ShaderForge.SFN_Relay,id:76,x:29712,y:33030,cmnt:numRow,varname:node_76,prsc:2|IN-2096-X;n:type:ShaderForge.SFN_Vector4Property,id:2096,x:29368,y:33099,ptovrint:False,ptlb:Frame,ptin:_Frame,varname:_Frame,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4,v2:1,v3:0.1,v4:0;n:type:ShaderForge.SFN_Relay,id:2615,x:29712,y:33133,varname:node_2615,prsc:2|IN-2096-Y;n:type:ShaderForge.SFN_Divide,id:6394,x:30353,y:33194,cmnt:UOffset,varname:node_6394,prsc:2|A-1175-OUT,B-76-OUT;n:type:ShaderForge.SFN_Fmod,id:1175,x:30304,y:33354,cmnt:currentCol,varname:node_1175,prsc:2|A-1591-OUT,B-76-OUT;n:type:ShaderForge.SFN_Relay,id:1512,x:29523,y:33225,cmnt:speed,varname:node_1512,prsc:2|IN-2096-Z;n:type:ShaderForge.SFN_Time,id:7476,x:29252,y:33339,varname:node_7476,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6915,x:29523,y:33393,varname:node_6915,prsc:2|A-1512-OUT,B-7476-T;n:type:ShaderForge.SFN_Frac,id:9478,x:29645,y:33641,varname:node_9478,prsc:2|IN-6915-OUT;n:type:ShaderForge.SFN_Multiply,id:8180,x:29883,y:33518,cmnt:currentFrame,varname:node_8180,prsc:2|A-1493-OUT,B-9478-OUT;n:type:ShaderForge.SFN_Multiply,id:5573,x:29725,y:33251,cmnt:totalnumframe,varname:node_5573,prsc:2|A-76-OUT,B-2615-OUT;n:type:ShaderForge.SFN_Round,id:1591,x:29893,y:33752,cmnt:currentIndex,varname:node_1591,prsc:2|IN-8180-OUT;n:type:ShaderForge.SFN_Divide,id:8191,x:30085,y:33752,varname:node_8191,prsc:2|A-1591-OUT,B-76-OUT;n:type:ShaderForge.SFN_Floor,id:1750,x:30304,y:33735,cmnt:currentRow,varname:node_1750,prsc:2|IN-8191-OUT;n:type:ShaderForge.SFN_Divide,id:5702,x:30304,y:33495,cmnt:V Offset,varname:node_5702,prsc:2|A-1750-OUT,B-2615-OUT;n:type:ShaderForge.SFN_Tex2d,id:5015,x:31387,y:33220,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:_Texture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-7016-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6686,x:31459,y:33476,ptovrint:False,ptlb:Alpha,ptin:_Alpha,varname:_Alpha,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:7228,x:31682,y:33563,varname:node_7228,prsc:2|A-7198-A,B-6686-OUT,C-5015-A;n:type:ShaderForge.SFN_ValueProperty,id:5424,x:29883,y:33446,ptovrint:False,ptlb:SplitLastFrame,ptin:_SplitLastFrame,varname:_SplitLastFrame,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_Subtract,id:1493,x:29902,y:33254,varname:node_1493,prsc:2|A-5573-OUT,B-5424-OUT;proporder:2096-5015-6686-5424;pass:END;sub:END;*/

Shader "Shader Forge/spriteTexture" {
    Properties {
        _Frame ("Frame", Vector) = (4,1,0.1,0)
        _Texture ("Texture", 2D) = "white" {}
        _Alpha ("Alpha", Float ) = 1
        _SplitLastFrame ("SplitLastFrame", Float ) = 5
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
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform float4 _Frame;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Alpha;
            uniform float _SplitLastFrame;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float node_76 = _Frame.r; // numRow
                float node_2615 = _Frame.g;
                float4 node_7476 = _Time;
                float node_1591 = round((((node_76*node_2615)-_SplitLastFrame)*frac((_Frame.b*node_7476.g)))); // currentIndex
                float2 node_7016 = ((i.uv0/float2(node_76,node_2615))+float2((fmod(node_1591,node_76)/node_76),(1.0 - (floor((node_1591/node_76))/node_2615))));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_7016, _Texture));
                float3 emissive = _Texture_var.rgb;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(i.vertexColor.a*_Alpha*_Texture_var.a));
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
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform float4 _Frame;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _SplitLastFrame;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float node_76 = _Frame.r; // numRow
                float node_2615 = _Frame.g;
                float4 node_7476 = _Time;
                float node_1591 = round((((node_76*node_2615)-_SplitLastFrame)*frac((_Frame.b*node_7476.g)))); // currentIndex
                float2 node_7016 = ((i.uv0/float2(node_76,node_2615))+float2((fmod(node_1591,node_76)/node_76),(1.0 - (floor((node_1591/node_76))/node_2615))));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_7016, _Texture));
                o.Emission = _Texture_var.rgb;
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
