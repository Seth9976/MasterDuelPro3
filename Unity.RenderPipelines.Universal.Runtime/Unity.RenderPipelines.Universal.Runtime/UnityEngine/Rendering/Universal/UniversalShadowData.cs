using System;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000CA RID: 202
	public class UniversalShadowData : ContextItem
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x00013494 File Offset: 0x00011694
		public override void Reset()
		{
			this.supportsMainLightShadows = false;
			this.mainLightShadowmapWidth = 0;
			this.mainLightShadowmapHeight = 0;
			this.mainLightShadowCascadesCount = 0;
			this.mainLightShadowCascadesSplit = Vector3.zero;
			this.mainLightShadowCascadeBorder = 0f;
			this.supportsAdditionalLightShadows = false;
			this.additionalLightsShadowmapWidth = 0;
			this.additionalLightsShadowmapHeight = 0;
			this.supportsSoftShadows = false;
			this.shadowmapDepthBufferBits = 0;
			List<Vector4> list = this.bias;
			if (list != null)
			{
				list.Clear();
			}
			List<int> list2 = this.resolution;
			if (list2 != null)
			{
				list2.Clear();
			}
			this.isKeywordAdditionalLightShadowsEnabled = false;
			this.isKeywordSoftShadowsEnabled = false;
			this.mainLightShadowResolution = 0;
			this.mainLightRenderTargetWidth = 0;
			this.mainLightRenderTargetHeight = 0;
			this.visibleLightsShadowCullingInfos = default(NativeArray<URPLightShadowCullingInfos>);
			this.shadowAtlasLayout = default(AdditionalLightsShadowAtlasLayout);
		}

		// Token: 0x04000475 RID: 1141
		public bool supportsMainLightShadows;

		// Token: 0x04000476 RID: 1142
		internal bool mainLightShadowsEnabled;

		// Token: 0x04000477 RID: 1143
		public int mainLightShadowmapWidth;

		// Token: 0x04000478 RID: 1144
		public int mainLightShadowmapHeight;

		// Token: 0x04000479 RID: 1145
		public int mainLightShadowCascadesCount;

		// Token: 0x0400047A RID: 1146
		public Vector3 mainLightShadowCascadesSplit;

		// Token: 0x0400047B RID: 1147
		public float mainLightShadowCascadeBorder;

		// Token: 0x0400047C RID: 1148
		public bool supportsAdditionalLightShadows;

		// Token: 0x0400047D RID: 1149
		internal bool additionalLightShadowsEnabled;

		// Token: 0x0400047E RID: 1150
		public int additionalLightsShadowmapWidth;

		// Token: 0x0400047F RID: 1151
		public int additionalLightsShadowmapHeight;

		// Token: 0x04000480 RID: 1152
		public bool supportsSoftShadows;

		// Token: 0x04000481 RID: 1153
		public int shadowmapDepthBufferBits;

		// Token: 0x04000482 RID: 1154
		public List<Vector4> bias;

		// Token: 0x04000483 RID: 1155
		public List<int> resolution;

		// Token: 0x04000484 RID: 1156
		internal bool isKeywordAdditionalLightShadowsEnabled;

		// Token: 0x04000485 RID: 1157
		internal bool isKeywordSoftShadowsEnabled;

		// Token: 0x04000486 RID: 1158
		internal int mainLightShadowResolution;

		// Token: 0x04000487 RID: 1159
		internal int mainLightRenderTargetWidth;

		// Token: 0x04000488 RID: 1160
		internal int mainLightRenderTargetHeight;

		// Token: 0x04000489 RID: 1161
		internal NativeArray<URPLightShadowCullingInfos> visibleLightsShadowCullingInfos;

		// Token: 0x0400048A RID: 1162
		internal AdditionalLightsShadowAtlasLayout shadowAtlasLayout;
	}
}
