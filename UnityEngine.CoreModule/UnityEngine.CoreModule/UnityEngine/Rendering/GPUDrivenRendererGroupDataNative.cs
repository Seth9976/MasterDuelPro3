using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000394 RID: 916
	[UsedByNativeCode]
	internal struct GPUDrivenRendererGroupDataNative
	{
		// Token: 0x04000B0C RID: 2828
		public unsafe int* rendererGroupID;

		// Token: 0x04000B0D RID: 2829
		public unsafe Bounds* localBounds;

		// Token: 0x04000B0E RID: 2830
		public unsafe Vector4* lightmapScaleOffset;

		// Token: 0x04000B0F RID: 2831
		public unsafe int* gameObjectLayer;

		// Token: 0x04000B10 RID: 2832
		public unsafe uint* renderingLayerMask;

		// Token: 0x04000B11 RID: 2833
		public unsafe int* lodGroupID;

		// Token: 0x04000B12 RID: 2834
		public unsafe MotionVectorGenerationMode* motionVecGenMode;

		// Token: 0x04000B13 RID: 2835
		public unsafe GPUDrivenPackedRendererData* packedRendererData;

		// Token: 0x04000B14 RID: 2836
		public unsafe int* rendererPriority;

		// Token: 0x04000B15 RID: 2837
		public unsafe int* meshIndex;

		// Token: 0x04000B16 RID: 2838
		public unsafe short* subMeshStartIndex;

		// Token: 0x04000B17 RID: 2839
		public unsafe int* materialsOffset;

		// Token: 0x04000B18 RID: 2840
		public unsafe short* materialsCount;

		// Token: 0x04000B19 RID: 2841
		public unsafe int* instancesOffset;

		// Token: 0x04000B1A RID: 2842
		public unsafe int* instancesCount;

		// Token: 0x04000B1B RID: 2843
		public unsafe GPUDrivenRendererEditorData* editorData;

		// Token: 0x04000B1C RID: 2844
		public int rendererGroupCount;

		// Token: 0x04000B1D RID: 2845
		public unsafe int* invalidRendererGroupID;

		// Token: 0x04000B1E RID: 2846
		public int invalidRendererGroupIDCount;

		// Token: 0x04000B1F RID: 2847
		public unsafe Matrix4x4* localToWorldMatrix;

		// Token: 0x04000B20 RID: 2848
		public unsafe Matrix4x4* prevLocalToWorldMatrix;

		// Token: 0x04000B21 RID: 2849
		public unsafe int* rendererGroupIndex;

		// Token: 0x04000B22 RID: 2850
		public int instanceCount;

		// Token: 0x04000B23 RID: 2851
		public unsafe int* meshID;

		// Token: 0x04000B24 RID: 2852
		public unsafe short* subMeshCount;

		// Token: 0x04000B25 RID: 2853
		public unsafe int* subMeshDescOffset;

		// Token: 0x04000B26 RID: 2854
		public int meshCount;

		// Token: 0x04000B27 RID: 2855
		public unsafe SubMeshDescriptor* subMeshDesc;

		// Token: 0x04000B28 RID: 2856
		public int subMeshDescCount;

		// Token: 0x04000B29 RID: 2857
		public unsafe int* materialIndex;

		// Token: 0x04000B2A RID: 2858
		public int materialIndexCount;

		// Token: 0x04000B2B RID: 2859
		public unsafe int* materialID;

		// Token: 0x04000B2C RID: 2860
		public unsafe GPUDrivenPackedMaterialData* packedMaterialData;

		// Token: 0x04000B2D RID: 2861
		public unsafe int* materialFilterFlags;

		// Token: 0x04000B2E RID: 2862
		public int materialCount;
	}
}
