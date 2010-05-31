xof 0302txt 0064
template Header {
 <3D82AB43-62DA-11cf-AB39-0020AF71E433>
 WORD major;
 WORD minor;
 DWORD flags;
}

template Vector {
 <3D82AB5E-62DA-11cf-AB39-0020AF71E433>
 FLOAT x;
 FLOAT y;
 FLOAT z;
}

template Coords2d {
 <F6F23F44-7686-11cf-8F52-0040333594A3>
 FLOAT u;
 FLOAT v;
}

template Matrix4x4 {
 <F6F23F45-7686-11cf-8F52-0040333594A3>
 array FLOAT matrix[16];
}

template ColorRGBA {
 <35FF44E0-6C7C-11cf-8F52-0040333594A3>
 FLOAT red;
 FLOAT green;
 FLOAT blue;
 FLOAT alpha;
}

template ColorRGB {
 <D3E16E81-7835-11cf-8F52-0040333594A3>
 FLOAT red;
 FLOAT green;
 FLOAT blue;
}

template IndexedColor {
 <1630B820-7842-11cf-8F52-0040333594A3>
 DWORD index;
 ColorRGBA indexColor;
}

template Boolean {
 <4885AE61-78E8-11cf-8F52-0040333594A3>
 WORD truefalse;
}

template Boolean2d {
 <4885AE63-78E8-11cf-8F52-0040333594A3>
 Boolean u;
 Boolean v;
}

template MaterialWrap {
 <4885AE60-78E8-11cf-8F52-0040333594A3>
 Boolean u;
 Boolean v;
}

template TextureFilename {
 <A42790E1-7810-11cf-8F52-0040333594A3>
 STRING filename;
}

template Material {
 <3D82AB4D-62DA-11cf-AB39-0020AF71E433>
 ColorRGBA faceColor;
 FLOAT power;
 ColorRGB specularColor;
 ColorRGB emissiveColor;
 [...]
}

template MeshFace {
 <3D82AB5F-62DA-11cf-AB39-0020AF71E433>
 DWORD nFaceVertexIndices;
 array DWORD faceVertexIndices[nFaceVertexIndices];
}

template MeshFaceWraps {
 <4885AE62-78E8-11cf-8F52-0040333594A3>
 DWORD nFaceWrapValues;
 Boolean2d faceWrapValues;
}

template MeshTextureCoords {
 <F6F23F40-7686-11cf-8F52-0040333594A3>
 DWORD nTextureCoords;
 array Coords2d textureCoords[nTextureCoords];
}

template MeshMaterialList {
 <F6F23F42-7686-11cf-8F52-0040333594A3>
 DWORD nMaterials;
 DWORD nFaceIndexes;
 array DWORD faceIndexes[nFaceIndexes];
 [Material]
}

template MeshNormals {
 <F6F23F43-7686-11cf-8F52-0040333594A3>
 DWORD nNormals;
 array Vector normals[nNormals];
 DWORD nFaceNormals;
 array MeshFace faceNormals[nFaceNormals];
}

template MeshVertexColors {
 <1630B821-7842-11cf-8F52-0040333594A3>
 DWORD nVertexColors;
 array IndexedColor vertexColors[nVertexColors];
}

template Mesh {
 <3D82AB44-62DA-11cf-AB39-0020AF71E433>
 DWORD nVertices;
 array Vector vertices[nVertices];
 DWORD nFaces;
 array MeshFace faces[nFaces];
 [...]
}

Header{
1;
0;
1;
}

Mesh {
 121;
 -2000.00000;0.00000;2000.00000;,
 -1500.00000;0.00000;2000.00000;,
 -1500.00000;0.00000;1500.00000;,
 -2000.00000;0.00000;1500.00000;,
 -1000.00000;0.00000;2000.00000;,
 -1000.00000;0.00000;1500.00000;,
 -500.00000;0.00000;2000.00000;,
 -500.00000;0.00000;1500.00000;,
 0.00000;0.00000;2000.00000;,
 0.00000;0.00000;1500.00000;,
 500.00000;0.00000;2000.00000;,
 500.00000;0.00000;1500.00000;,
 1000.00000;0.00000;2000.00000;,
 1000.00000;0.00000;1500.00000;,
 1500.00000;0.00000;2000.00000;,
 1500.00000;0.00000;1500.00000;,
 2000.00000;0.00000;2000.00000;,
 2000.00000;0.00000;1500.00000;,
 -1500.00000;0.00000;1000.00000;,
 -2000.00000;0.00000;1000.00000;,
 -1000.00000;0.00000;1000.00000;,
 -500.00000;0.00000;1000.00000;,
 0.00000;0.00000;1000.00000;,
 500.00000;0.00000;1000.00000;,
 1000.00000;0.00000;1000.00000;,
 1500.00000;0.00000;1000.00000;,
 2000.00000;0.00000;1000.00000;,
 -1500.00000;0.00000;500.00000;,
 -2000.00000;0.00000;500.00000;,
 -1000.00000;0.00000;500.00000;,
 -500.00000;0.00000;500.00000;,
 0.00000;0.00000;500.00000;,
 500.00000;0.00000;500.00000;,
 1000.00000;0.00000;500.00000;,
 1500.00000;0.00000;500.00000;,
 2000.00000;0.00000;500.00000;,
 -1500.00000;0.00000;0.00000;,
 -2000.00000;0.00000;0.00000;,
 -1000.00000;0.00000;0.00000;,
 -500.00000;0.00000;0.00000;,
 0.00000;0.00000;0.00000;,
 500.00000;0.00000;0.00000;,
 1000.00000;0.00000;0.00000;,
 1500.00000;0.00000;0.00000;,
 2000.00000;0.00000;0.00000;,
 -1500.00000;0.00000;-500.00000;,
 -2000.00000;0.00000;-500.00000;,
 -1000.00000;0.00000;-500.00000;,
 -500.00000;0.00000;-500.00000;,
 0.00000;0.00000;-500.00000;,
 500.00000;0.00000;-500.00000;,
 1000.00000;0.00000;-500.00000;,
 1500.00000;0.00000;-500.00000;,
 2000.00000;0.00000;-500.00000;,
 -1500.00000;0.00000;-1000.00000;,
 -2000.00000;0.00000;-1000.00000;,
 -1000.00000;0.00000;-1000.00000;,
 -500.00000;0.00000;-1000.00000;,
 0.00000;0.00000;-1000.00000;,
 500.00000;0.00000;-1000.00000;,
 1000.00000;0.00000;-1000.00000;,
 1500.00000;0.00000;-1000.00000;,
 2000.00000;0.00000;-1000.00000;,
 -1500.00000;0.00000;-1500.00000;,
 -2000.00000;0.00000;-1500.00000;,
 -1000.00000;0.00000;-1500.00000;,
 -500.00000;0.00000;-1500.00000;,
 0.00000;0.00000;-1500.00000;,
 500.00000;0.00000;-1500.00000;,
 1000.00000;0.00000;-1500.00000;,
 1500.00000;0.00000;-1500.00000;,
 2000.00000;0.00000;-1500.00000;,
 -1500.00000;0.00000;-2000.00000;,
 -2000.00000;0.00000;-2000.00000;,
 -1000.00000;0.00000;-2000.00000;,
 -500.00000;0.00000;-2000.00000;,
 0.00000;0.00000;-2000.00000;,
 500.00000;0.00000;-2000.00000;,
 1000.00000;0.00000;-2000.00000;,
 1500.00000;0.00000;-2000.00000;,
 2000.00000;0.00000;-2000.00000;,
 -0.50000;505.00000;-0.50000;,
 0.50000;505.00000;-0.50000;,
 0.50000;495.00000;-0.50000;,
 -0.50000;495.00000;-0.50000;,
 0.50000;505.00000;-0.50000;,
 0.50000;505.00000;0.50000;,
 0.50000;495.00000;0.50000;,
 0.50000;495.00000;-0.50000;,
 0.50000;505.00000;0.50000;,
 -0.50000;505.00000;0.50000;,
 -0.50000;495.00000;0.50000;,
 0.50000;495.00000;0.50000;,
 -0.50000;505.00000;0.50000;,
 -0.50000;505.00000;-0.50000;,
 -0.50000;495.00000;-0.50000;,
 -0.50000;495.00000;0.50000;,
 0.50000;505.00000;-0.50000;,
 -0.50000;505.00000;-0.50000;,
 -0.50000;495.00000;-0.50000;,
 0.50000;495.00000;-0.50000;,
 -0.10000;10.00000;-0.10000;,
 0.10000;10.00000;-0.10000;,
 0.10000;0.00000;-0.10000;,
 -0.10000;0.00000;-0.10000;,
 0.10000;10.00000;-0.10000;,
 0.10000;10.00000;0.10000;,
 0.10000;0.00000;0.10000;,
 0.10000;0.00000;-0.10000;,
 0.10000;10.00000;0.10000;,
 -0.10000;10.00000;0.10000;,
 -0.10000;0.00000;0.10000;,
 0.10000;0.00000;0.10000;,
 -0.10000;10.00000;0.10000;,
 -0.10000;10.00000;-0.10000;,
 -0.10000;0.00000;-0.10000;,
 -0.10000;0.00000;0.10000;,
 0.10000;10.00000;-0.10000;,
 -0.10000;10.00000;-0.10000;,
 -0.10000;0.00000;-0.10000;,
 0.10000;0.00000;-0.10000;;
 
 76;
 4;0,1,2,3;,
 4;1,4,5,2;,
 4;4,6,7,5;,
 4;6,8,9,7;,
 4;8,10,11,9;,
 4;10,12,13,11;,
 4;12,14,15,13;,
 4;14,16,17,15;,
 4;3,2,18,19;,
 4;2,5,20,18;,
 4;5,7,21,20;,
 4;7,9,22,21;,
 4;9,11,23,22;,
 4;11,13,24,23;,
 4;13,15,25,24;,
 4;15,17,26,25;,
 4;19,18,27,28;,
 4;18,20,29,27;,
 4;20,21,30,29;,
 4;21,22,31,30;,
 4;22,23,32,31;,
 4;23,24,33,32;,
 4;24,25,34,33;,
 4;25,26,35,34;,
 4;28,27,36,37;,
 4;27,29,38,36;,
 4;29,30,39,38;,
 4;30,31,40,39;,
 4;31,32,41,40;,
 4;32,33,42,41;,
 4;33,34,43,42;,
 4;34,35,44,43;,
 4;37,36,45,46;,
 4;36,38,47,45;,
 4;38,39,48,47;,
 4;39,40,49,48;,
 4;40,41,50,49;,
 4;41,42,51,50;,
 4;42,43,52,51;,
 4;43,44,53,52;,
 4;46,45,54,55;,
 4;45,47,56,54;,
 4;47,48,57,56;,
 4;48,49,58,57;,
 4;49,50,59,58;,
 4;50,51,60,59;,
 4;51,52,61,60;,
 4;52,53,62,61;,
 4;55,54,63,64;,
 4;54,56,65,63;,
 4;56,57,66,65;,
 4;57,58,67,66;,
 4;58,59,68,67;,
 4;59,60,69,68;,
 4;60,61,70,69;,
 4;61,62,71,70;,
 4;64,63,72,73;,
 4;63,65,74,72;,
 4;65,66,75,74;,
 4;66,67,76,75;,
 4;67,68,77,76;,
 4;68,69,78,77;,
 4;69,70,79,78;,
 4;70,71,80,79;,
 4;81,82,83,84;,
 4;85,86,87,88;,
 4;89,90,91,92;,
 4;93,94,95,96;,
 4;93,86,97,98;,
 4;99,100,87,96;,
 4;101,102,103,104;,
 4;105,106,107,108;,
 4;109,110,111,112;,
 4;113,114,115,116;,
 4;113,106,117,118;,
 4;119,120,107,116;;
 
 MeshMaterialList {
  3;
  76;
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  2,
  2,
  2,
  2,
  2,
  2,
  1,
  1,
  1,
  1,
  1,
  1;;
  Material {
   0.800000;0.800000;0.800000;1.000000;;
   38.000000;
   0.000000;0.000000;0.000000;;
   0.310000;0.310000;0.310000;;
   TextureFilename {
    "floor64x64.bmp";
   }
  }
  Material {
   0.180000;1.000000;0.000000;1.000000;;
   12.000000;
   0.000000;0.000000;0.000000;;
   0.180000;1.000000;0.000000;;
  }
  Material {
   1.000000;1.000000;1.000000;1.000000;;
   0.000000;
   0.000000;0.000000;0.000000;;
   0.000000;0.000000;0.000000;;
  }
 }
 MeshNormals {
  12;
  0.000000;1.000000;0.000000;,
  0.000000;0.000000;-1.000000;,
  1.000000;0.000000;0.000000;,
  0.000000;0.000000;1.000000;,
  -1.000000;0.000000;0.000000;,
  0.000000;-1.000000;-0.000000;,
  0.000000;0.000000;-1.000000;,
  1.000000;0.000000;0.000000;,
  0.000000;0.000000;1.000000;,
  -1.000000;0.000000;0.000000;,
  0.000000;1.000000;0.000000;,
  0.000000;-1.000000;-0.000000;;
  76;
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;0,0,0,0;,
  4;1,1,1,1;,
  4;2,2,2,2;,
  4;3,3,3,3;,
  4;4,4,4,4;,
  4;0,0,0,0;,
  4;5,5,5,5;,
  4;6,6,6,6;,
  4;7,7,7,7;,
  4;8,8,8,8;,
  4;9,9,9,9;,
  4;10,10,10,10;,
  4;11,11,11,11;;
 }
 MeshTextureCoords {
  121;
  -200.000000;201.000000;,
  -150.000000;201.000000;,
  -150.000000;151.000000;,
  -200.000000;151.000000;,
  -100.000000;201.000000;,
  -100.000000;151.000000;,
  -50.000000;201.000000;,
  -50.000000;151.000000;,
  0.000000;201.000000;,
  0.000000;151.000000;,
  50.000000;201.000000;,
  50.000000;151.000000;,
  100.000000;201.000000;,
  100.000000;151.000000;,
  150.000000;201.000000;,
  150.000000;151.000000;,
  200.000000;201.000000;,
  200.000000;151.000000;,
  -150.000000;101.000000;,
  -200.000000;101.000000;,
  -100.000000;101.000000;,
  -50.000000;101.000000;,
  0.000000;101.000000;,
  50.000000;101.000000;,
  100.000000;101.000000;,
  150.000000;101.000000;,
  200.000000;101.000000;,
  -150.000000;51.000000;,
  -200.000000;51.000000;,
  -100.000000;51.000000;,
  -50.000000;51.000000;,
  0.000000;51.000000;,
  50.000000;51.000000;,
  100.000000;51.000000;,
  150.000000;51.000000;,
  200.000000;51.000000;,
  -150.000000;1.000000;,
  -200.000000;1.000000;,
  -100.000000;1.000000;,
  -50.000000;1.000000;,
  0.000000;1.000000;,
  50.000000;1.000000;,
  100.000000;1.000000;,
  150.000000;1.000000;,
  200.000000;1.000000;,
  -150.000000;-49.000000;,
  -200.000000;-49.000000;,
  -100.000000;-49.000000;,
  -50.000000;-49.000000;,
  0.000000;-49.000000;,
  50.000000;-49.000000;,
  100.000000;-49.000000;,
  150.000000;-49.000000;,
  200.000000;-49.000000;,
  -150.000000;-99.000000;,
  -200.000000;-99.000000;,
  -100.000000;-99.000000;,
  -50.000000;-99.000000;,
  0.000000;-99.000000;,
  50.000000;-99.000000;,
  100.000000;-99.000000;,
  150.000000;-99.000000;,
  200.000000;-99.000000;,
  -150.000000;-149.000000;,
  -200.000000;-149.000000;,
  -100.000000;-149.000000;,
  -50.000000;-149.000000;,
  0.000000;-149.000000;,
  50.000000;-149.000000;,
  100.000000;-149.000000;,
  150.000000;-149.000000;,
  200.000000;-149.000000;,
  -150.000000;-199.000000;,
  -200.000000;-199.000000;,
  -100.000000;-199.000000;,
  -50.000000;-199.000000;,
  0.000000;-199.000000;,
  50.000000;-199.000000;,
  100.000000;-199.000000;,
  150.000000;-199.000000;,
  200.000000;-199.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  1.000000;1.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  1.000000;1.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  1.000000;1.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  1.000000;1.000000;,
  0.000000;1.000000;,
  1.000000;1.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  1.000000;1.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  1.000000;1.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  1.000000;1.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;,
  1.000000;1.000000;,
  0.000000;1.000000;,
  1.000000;1.000000;,
  0.000000;1.000000;,
  0.000000;0.000000;,
  1.000000;0.000000;;
 }
 MeshVertexColors {
  121;
  0;1.000000;1.000000;1.000000;1.000000;,
  1;1.000000;1.000000;1.000000;1.000000;,
  2;1.000000;1.000000;1.000000;1.000000;,
  3;1.000000;1.000000;1.000000;1.000000;,
  4;1.000000;1.000000;1.000000;1.000000;,
  5;1.000000;1.000000;1.000000;1.000000;,
  6;1.000000;1.000000;1.000000;1.000000;,
  7;1.000000;1.000000;1.000000;1.000000;,
  8;1.000000;1.000000;1.000000;1.000000;,
  9;1.000000;1.000000;1.000000;1.000000;,
  10;1.000000;1.000000;1.000000;1.000000;,
  11;1.000000;1.000000;1.000000;1.000000;,
  12;1.000000;1.000000;1.000000;1.000000;,
  13;1.000000;1.000000;1.000000;1.000000;,
  14;1.000000;1.000000;1.000000;1.000000;,
  15;1.000000;1.000000;1.000000;1.000000;,
  16;1.000000;1.000000;1.000000;1.000000;,
  17;1.000000;1.000000;1.000000;1.000000;,
  18;1.000000;1.000000;1.000000;1.000000;,
  19;1.000000;1.000000;1.000000;1.000000;,
  20;1.000000;1.000000;1.000000;1.000000;,
  21;1.000000;1.000000;1.000000;1.000000;,
  22;1.000000;1.000000;1.000000;1.000000;,
  23;1.000000;1.000000;1.000000;1.000000;,
  24;1.000000;1.000000;1.000000;1.000000;,
  25;1.000000;1.000000;1.000000;1.000000;,
  26;1.000000;1.000000;1.000000;1.000000;,
  27;1.000000;1.000000;1.000000;1.000000;,
  28;1.000000;1.000000;1.000000;1.000000;,
  29;1.000000;1.000000;1.000000;1.000000;,
  30;1.000000;1.000000;1.000000;1.000000;,
  31;1.000000;1.000000;1.000000;1.000000;,
  32;1.000000;1.000000;1.000000;1.000000;,
  33;1.000000;1.000000;1.000000;1.000000;,
  34;1.000000;1.000000;1.000000;1.000000;,
  35;1.000000;1.000000;1.000000;1.000000;,
  36;1.000000;1.000000;1.000000;1.000000;,
  37;1.000000;1.000000;1.000000;1.000000;,
  38;1.000000;1.000000;1.000000;1.000000;,
  39;1.000000;1.000000;1.000000;1.000000;,
  40;1.000000;1.000000;1.000000;1.000000;,
  41;1.000000;1.000000;1.000000;1.000000;,
  42;1.000000;1.000000;1.000000;1.000000;,
  43;1.000000;1.000000;1.000000;1.000000;,
  44;1.000000;1.000000;1.000000;1.000000;,
  45;1.000000;1.000000;1.000000;1.000000;,
  46;1.000000;1.000000;1.000000;1.000000;,
  47;1.000000;1.000000;1.000000;1.000000;,
  48;1.000000;1.000000;1.000000;1.000000;,
  49;1.000000;1.000000;1.000000;1.000000;,
  50;1.000000;1.000000;1.000000;1.000000;,
  51;1.000000;1.000000;1.000000;1.000000;,
  52;1.000000;1.000000;1.000000;1.000000;,
  53;1.000000;1.000000;1.000000;1.000000;,
  54;1.000000;1.000000;1.000000;1.000000;,
  55;1.000000;1.000000;1.000000;1.000000;,
  56;1.000000;1.000000;1.000000;1.000000;,
  57;1.000000;1.000000;1.000000;1.000000;,
  58;1.000000;1.000000;1.000000;1.000000;,
  59;1.000000;1.000000;1.000000;1.000000;,
  60;1.000000;1.000000;1.000000;1.000000;,
  61;1.000000;1.000000;1.000000;1.000000;,
  62;1.000000;1.000000;1.000000;1.000000;,
  63;1.000000;1.000000;1.000000;1.000000;,
  64;1.000000;1.000000;1.000000;1.000000;,
  65;1.000000;1.000000;1.000000;1.000000;,
  66;1.000000;1.000000;1.000000;1.000000;,
  67;1.000000;1.000000;1.000000;1.000000;,
  68;1.000000;1.000000;1.000000;1.000000;,
  69;1.000000;1.000000;1.000000;1.000000;,
  70;1.000000;1.000000;1.000000;1.000000;,
  71;1.000000;1.000000;1.000000;1.000000;,
  72;1.000000;1.000000;1.000000;1.000000;,
  73;1.000000;1.000000;1.000000;1.000000;,
  74;1.000000;1.000000;1.000000;1.000000;,
  75;1.000000;1.000000;1.000000;1.000000;,
  76;1.000000;1.000000;1.000000;1.000000;,
  77;1.000000;1.000000;1.000000;1.000000;,
  78;1.000000;1.000000;1.000000;1.000000;,
  79;1.000000;1.000000;1.000000;1.000000;,
  80;1.000000;1.000000;1.000000;1.000000;,
  81;1.000000;1.000000;1.000000;1.000000;,
  82;1.000000;1.000000;1.000000;1.000000;,
  83;1.000000;1.000000;1.000000;1.000000;,
  84;1.000000;1.000000;1.000000;1.000000;,
  85;1.000000;1.000000;1.000000;1.000000;,
  86;1.000000;1.000000;1.000000;1.000000;,
  87;1.000000;1.000000;1.000000;1.000000;,
  88;1.000000;1.000000;1.000000;1.000000;,
  89;1.000000;1.000000;1.000000;1.000000;,
  90;1.000000;1.000000;1.000000;1.000000;,
  91;1.000000;1.000000;1.000000;1.000000;,
  92;1.000000;1.000000;1.000000;1.000000;,
  93;1.000000;1.000000;1.000000;1.000000;,
  94;1.000000;1.000000;1.000000;1.000000;,
  95;1.000000;1.000000;1.000000;1.000000;,
  96;1.000000;1.000000;1.000000;1.000000;,
  97;1.000000;1.000000;1.000000;1.000000;,
  98;1.000000;1.000000;1.000000;1.000000;,
  99;1.000000;1.000000;1.000000;1.000000;,
  100;1.000000;1.000000;1.000000;1.000000;,
  101;1.000000;1.000000;1.000000;1.000000;,
  102;1.000000;1.000000;1.000000;1.000000;,
  103;1.000000;1.000000;1.000000;1.000000;,
  104;1.000000;1.000000;1.000000;1.000000;,
  105;1.000000;1.000000;1.000000;1.000000;,
  106;1.000000;1.000000;1.000000;1.000000;,
  107;1.000000;1.000000;1.000000;1.000000;,
  108;1.000000;1.000000;1.000000;1.000000;,
  109;1.000000;1.000000;1.000000;1.000000;,
  110;1.000000;1.000000;1.000000;1.000000;,
  111;1.000000;1.000000;1.000000;1.000000;,
  112;1.000000;1.000000;1.000000;1.000000;,
  113;1.000000;1.000000;1.000000;1.000000;,
  114;1.000000;1.000000;1.000000;1.000000;,
  115;1.000000;1.000000;1.000000;1.000000;,
  116;1.000000;1.000000;1.000000;1.000000;,
  117;1.000000;1.000000;1.000000;1.000000;,
  118;1.000000;1.000000;1.000000;1.000000;,
  119;1.000000;1.000000;1.000000;1.000000;,
  120;1.000000;1.000000;1.000000;1.000000;;
 }
}