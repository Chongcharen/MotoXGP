// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32899,y:32667,varname:node_4795,prsc:2|emission-2393-OUT,alpha-798-OUT,voffset-5837-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32178,y:32368,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2393,x:32592,y:32517,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-2365-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32178,y:32539,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32178,y:32697,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:798,x:32607,y:32693,varname:node_798,prsc:2|A-6074-A,B-2053-A,C-797-A;n:type:ShaderForge.SFN_ValueProperty,id:2365,x:32182,y:32856,ptovrint:False,ptlb:Alpha,ptin:_Alpha,varname:_Alpha,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:5837,x:32569,y:33093,cmnt:Wind animation,varname:node_5837,prsc:2|A-8031-RGB,B-6169-OUT;n:type:ShaderForge.SFN_VertexColor,id:8031,x:32263,y:33017,varname:node_8031,prsc:2;n:type:ShaderForge.SFN_Append,id:6169,x:32263,y:33177,varname:node_6169,prsc:2|A-3901-OUT,B-4397-OUT;n:type:ShaderForge.SFN_Vector1,id:3901,x:32060,y:33177,varname:node_3901,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:4397,x:32060,y:33234,varname:node_4397,prsc:2|A-3034-OUT,B-7153-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:7366,x:31811,y:32996,varname:node_7366,prsc:2;n:type:ShaderForge.SFN_Sin,id:3034,x:31811,y:33143,varname:node_3034,prsc:2|IN-7366-X;n:type:ShaderForge.SFN_Multiply,id:7153,x:31811,y:33293,varname:node_7153,prsc:2|A-6557-OUT,B-7837-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6557,x:31557,y:33177,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:_Intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:7837,x:31557,y:33312,varname:node_7837,prsc:2|A-55-U,B-4251-OUT;n:type:ShaderForge.SFN_TexCoord,id:55,x:31320,y:33157,varname:node_55,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Cos,id:4251,x:31320,y:33312,varname:node_4251,prsc:2|IN-3327-T;n:type:ShaderForge.SFN_Time,id:3327,x:31123,y:33293,varname:node_3327,prsc:2;proporder:6074-797-2365-6557;pass:END;sub:END;*/

Shader "Shader Forge/AlphaBlendLocation" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (1,1,1,1)
        _Alpha ("Alpha", Float ) = 1
        _Intensity ("Intensity", Float ) = 0.5
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
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 2.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _Alpha;
            uniform float _Intensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                float4 node_3327 = _Time;
                v.vertex.xyz += (o.vertexColor.rgb*float3(float2(0.5,(sin(mul(unity_ObjectToWorld, v.vertex).r)*(_Intensity*(o.uv0.r*cos(node_3327.g))))),0.0));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*_TintColor.rgb*_Alpha);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_MainTex_var.a*i.vertexColor.a*_TintColor.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
