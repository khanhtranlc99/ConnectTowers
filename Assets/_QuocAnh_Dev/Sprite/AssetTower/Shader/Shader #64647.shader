//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "" {
Properties {
_MainTex ("Texture", any) = "" { }
_NitsForPaperWhite ("NitsForPaperWhite", Float) = 160
_ColorGamut ("ColorGamut", Float) = 0
}
SubShader {
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 57251
}
}
}