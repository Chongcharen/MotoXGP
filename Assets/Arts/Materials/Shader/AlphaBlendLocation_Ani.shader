// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32802,y:33190,varname:node_4795,prsc:2|emission-4020-OUT,alpha-3916-OUT,voffset-6609-OUT;n:type:ShaderForge.SFN_Multiply,id:6609,x:32502,y:33549,cmnt:Wind animation,varname:node_6609,prsc:2|A-5-RGB,B-7991-OUT;n:type:ShaderForge.SFN_VertexColor,id:5,x:32196,y:33473,varname:node_5,prsc:2;n:type:ShaderForge.SFN_Append,id:7991,x:32196,y:33633,varname:node_7991,prsc:2|A-3419-OUT,B-5788-OUT;n:type:ShaderForge.SFN_Vector1,id:3419,x:31993,y:33633,varname:node_3419,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:5788,x:31993,y:33690,varname:node_5788,prsc:2|A-9175-OUT,B-8436-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:9793,x:31744,y:33452,varname:node_9793,prsc:2;n:type:ShaderForge.SFN_Sin,id:9175,x:31744,y:33599,varname:node_9175,prsc:2|IN-9793-X;n:type:ShaderForge.SFN_Multiply,id:8436,x:31744,y:33749,varname:node_8436,prsc:2|A-7680-OUT,B-3930-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7680,x:31490,y:33633,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:_Intensity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:3930,x:31490,y:33768,varname:node_3930,prsc:2|A-7814-U,B-3335-OUT;n:type:ShaderForge.SFN_TexCoord,id:7814,x:31253,y:33613,varname:node_7814,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Cos,id:3335,x:31253,y:33768,varname:node_3335,prsc:2|IN-2487-T;n:type:ShaderForge.SFN_Time,id:2487,x:31056,y:33749,varname:node_2487,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:1599,x:31850,y:32593,ptovrint:False,ptlb:MainTex_copy,ptin:_MainTex_copy,varname:_MainTex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:4020,x:32269,y:32812,varname:node_4020,prsc:2|A-1599-RGB,B-8468-RGB,C-9054-RGB,D-7949-OUT;n:type:ShaderForge.SFN_VertexColor,id:8468,x:31840,y:32821,varname:node_8468,prsc:2;n:type:ShaderForge.SFN_Color,id:9054,x:31840,y:32952,ptovrint:True,ptlb:Color_copy_copy,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:3916,x:32269,y:32948,varname:node_3916,prsc:2|A-1599-A,B-8468-A,C-9054-A;n:type:ShaderForge.SFN_ValueProperty,id:7949,x:31840,y:33152,ptovrint:False,ptlb:Alpha_copy,ptin:_Alpha_copy,varname:_Alpha_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:7680-1599-9054-7949;pass:END;sub:END;*/

Shader "Shader Forge/AlphaBlendLocation" {
    Properties {
        _Intensity ("Intensity", Float ) = 0.5
        _MainTex_copy ("MainTex_copy", 2D) = "white" {}
        _TintColor ("Color_copy_copy", Color) = (1,1,1,1)
        _Alpha_copy ("Alpha_copy", Float ) = 1
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 2.0
            uniform float _Intensity;
            uniform sampler2D _MainTex_copy; uniform float4 _MainTex_copy_ST;
            uniform float4 _TintColor;
            uniform float _Alpha_copy;
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
                float4 node_2487 = _Time;
                v.vertex.xyz += (o.vertexColor.rgb*float3(float2(0.5,(sin(mul(unity_ObjectToWorld, v.vertex).r)*(_Intensity*(o.uv0.r*cos(node_2487.g))))),0.0));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _MainTex_copy_var = tex2D(_MainTex_copy,TRANSFORM_TEX(i.uv0, _MainTex_copy));
                float3 emissive = (_MainTex_copy_var.rgb*i.vertexColor.rgb*_TintColor.rgb*_Alpha_copy);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_MainTex_copy_var.a*i.vertexColor.a*_TintColor.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
