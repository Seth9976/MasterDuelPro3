using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C8 RID: 968
	[UsedByNativeCode]
	public struct ShadowDrawingSettings : IEquatable<ShadowDrawingSettings>
	{
		// Token: 0x170003FB RID: 1019
		// (set) Token: 0x06001A7E RID: 6782 RVA: 0x00039C2C File Offset: 0x00037E2C
		public bool useRenderingLayerMaskTest
		{
			set
			{
				this.m_UseRenderingLayerMaskTest = (value ? 1 : 0);
			}
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00039C3C File Offset: 0x00037E3C
		public ShadowDrawingSettings(CullingResults cullingResults, int lightIndex)
		{
			this.m_CullingResults = cullingResults;
			this.m_LightIndex = lightIndex;
			this.m_SplitIndex = -1;
			this.m_UseRenderingLayerMaskTest = 0;
			this.m_BatchLayerMask = uint.MaxValue;
			this.m_SplitData = default(ShadowSplitData);
			this.m_SplitData.shadowCascadeBlendCullingFactor = 1f;
			this.m_ObjectsFilter = ShadowObjectsFilter.AllObjects;
			this.m_ProjectionType = BatchCullingProjectionType.Unknown;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00039C98 File Offset: 0x00037E98
		public bool Equals(ShadowDrawingSettings other)
		{
			return this.m_CullingResults.Equals(other.m_CullingResults) && this.m_LightIndex == other.m_LightIndex && this.m_SplitIndex == other.m_SplitIndex && this.m_SplitData.Equals(other.m_SplitData) && this.m_UseRenderingLayerMaskTest.Equals(other.m_UseRenderingLayerMaskTest) && this.m_BatchLayerMask == other.m_BatchLayerMask && this.m_ObjectsFilter.Equals(other.m_ObjectsFilter);
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00039D2C File Offset: 0x00037F2C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ShadowDrawingSettings && this.Equals((ShadowDrawingSettings)obj);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00039D64 File Offset: 0x00037F64
		public override int GetHashCode()
		{
			int hashCode = this.m_CullingResults.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_LightIndex;
			hashCode = (hashCode * 397) ^ this.m_SplitIndex;
			hashCode = (hashCode * 397) ^ this.m_UseRenderingLayerMaskTest;
			hashCode = (hashCode * 397) ^ (int)this.m_BatchLayerMask;
			hashCode = (hashCode * 397) ^ this.m_SplitData.GetHashCode();
			return (hashCode * 397) ^ (int)this.m_ObjectsFilter;
		}

		// Token: 0x04000C74 RID: 3188
		private CullingResults m_CullingResults;

		// Token: 0x04000C75 RID: 3189
		private int m_LightIndex;

		// Token: 0x04000C76 RID: 3190
		private int m_SplitIndex;

		// Token: 0x04000C77 RID: 3191
		private int m_UseRenderingLayerMaskTest;

		// Token: 0x04000C78 RID: 3192
		private uint m_BatchLayerMask;

		// Token: 0x04000C79 RID: 3193
		private ShadowSplitData m_SplitData;

		// Token: 0x04000C7A RID: 3194
		private ShadowObjectsFilter m_ObjectsFilter;

		// Token: 0x04000C7B RID: 3195
		private BatchCullingProjectionType m_ProjectionType;
	}
}
