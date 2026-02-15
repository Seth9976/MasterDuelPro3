using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000033 RID: 51
	internal struct RangeKey : IEquatable<RangeKey>
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00005A28 File Offset: 0x00003C28
		public bool Equals(RangeKey other)
		{
			return this.layer == other.layer && this.renderingLayerMask == other.renderingLayerMask && this.motionMode == other.motionMode && this.shadowCastingMode == other.shadowCastingMode && this.staticShadowCaster == other.staticShadowCaster && this.rendererPriority == other.rendererPriority && this.supportsIndirect == other.supportsIndirect;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005A9C File Offset: 0x00003C9C
		public override int GetHashCode()
		{
			return (int)((((((((MotionVectorGenerationMode)13 * (MotionVectorGenerationMode)23 + (int)this.layer) * (MotionVectorGenerationMode)23 + (int)this.renderingLayerMask) * (MotionVectorGenerationMode)23 + (int)this.motionMode) * (MotionVectorGenerationMode)23 + (int)this.shadowCastingMode) * (MotionVectorGenerationMode)23 + (this.staticShadowCaster ? 1 : 0)) * (MotionVectorGenerationMode)23 + this.rendererPriority) * (MotionVectorGenerationMode)23 + (this.supportsIndirect ? 1 : 0));
		}

		// Token: 0x040000A9 RID: 169
		public byte layer;

		// Token: 0x040000AA RID: 170
		public uint renderingLayerMask;

		// Token: 0x040000AB RID: 171
		public MotionVectorGenerationMode motionMode;

		// Token: 0x040000AC RID: 172
		public ShadowCastingMode shadowCastingMode;

		// Token: 0x040000AD RID: 173
		public bool staticShadowCaster;

		// Token: 0x040000AE RID: 174
		public int rendererPriority;

		// Token: 0x040000AF RID: 175
		public bool supportsIndirect;
	}
}
