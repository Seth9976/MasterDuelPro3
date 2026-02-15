using System;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x02000398 RID: 920
	internal struct GPUDrivenRendererGroupData
	{
		// Token: 0x04000B40 RID: 2880
		public NativeArray<int> rendererGroupID;

		// Token: 0x04000B41 RID: 2881
		public NativeArray<Bounds> localBounds;

		// Token: 0x04000B42 RID: 2882
		public NativeArray<Vector4> lightmapScaleOffset;

		// Token: 0x04000B43 RID: 2883
		public NativeArray<int> gameObjectLayer;

		// Token: 0x04000B44 RID: 2884
		public NativeArray<uint> renderingLayerMask;

		// Token: 0x04000B45 RID: 2885
		public NativeArray<int> lodGroupID;

		// Token: 0x04000B46 RID: 2886
		public NativeArray<int> lightmapIndex;

		// Token: 0x04000B47 RID: 2887
		public NativeArray<GPUDrivenPackedRendererData> packedRendererData;

		// Token: 0x04000B48 RID: 2888
		public NativeArray<int> rendererPriority;

		// Token: 0x04000B49 RID: 2889
		public NativeArray<int> meshIndex;

		// Token: 0x04000B4A RID: 2890
		public NativeArray<short> subMeshStartIndex;

		// Token: 0x04000B4B RID: 2891
		public NativeArray<int> materialsOffset;

		// Token: 0x04000B4C RID: 2892
		public NativeArray<short> materialsCount;

		// Token: 0x04000B4D RID: 2893
		public NativeArray<int> instancesOffset;

		// Token: 0x04000B4E RID: 2894
		public NativeArray<int> instancesCount;

		// Token: 0x04000B4F RID: 2895
		public NativeArray<GPUDrivenRendererEditorData> editorData;

		// Token: 0x04000B50 RID: 2896
		public NativeArray<int> invalidRendererGroupID;

		// Token: 0x04000B51 RID: 2897
		public NativeArray<Matrix4x4> localToWorldMatrix;

		// Token: 0x04000B52 RID: 2898
		public NativeArray<Matrix4x4> prevLocalToWorldMatrix;

		// Token: 0x04000B53 RID: 2899
		public NativeArray<int> rendererGroupIndex;

		// Token: 0x04000B54 RID: 2900
		public NativeArray<int> meshID;

		// Token: 0x04000B55 RID: 2901
		public NativeArray<short> subMeshCount;

		// Token: 0x04000B56 RID: 2902
		public NativeArray<int> subMeshDescOffset;

		// Token: 0x04000B57 RID: 2903
		public NativeArray<SubMeshDescriptor> subMeshDesc;

		// Token: 0x04000B58 RID: 2904
		public NativeArray<int> materialIndex;

		// Token: 0x04000B59 RID: 2905
		public NativeArray<int> materialID;

		// Token: 0x04000B5A RID: 2906
		public NativeArray<GPUDrivenPackedMaterialData> packedMaterialData;

		// Token: 0x04000B5B RID: 2907
		public NativeArray<int> materialFilterFlags;
	}
}
