using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200037E RID: 894
	public struct BatchFilterSettings
	{
		// Token: 0x17000381 RID: 897
		// (set) Token: 0x060018CF RID: 6351 RVA: 0x00035210 File Offset: 0x00033410
		public byte batchLayer
		{
			set
			{
				this.m_batchLayer = value;
			}
		}

		// Token: 0x17000382 RID: 898
		// (set) Token: 0x060018D0 RID: 6352 RVA: 0x00035219 File Offset: 0x00033419
		public MotionVectorGenerationMode motionMode
		{
			set
			{
				this.m_motionMode = (byte)value;
			}
		}

		// Token: 0x17000383 RID: 899
		// (set) Token: 0x060018D1 RID: 6353 RVA: 0x00035223 File Offset: 0x00033423
		public ShadowCastingMode shadowCastingMode
		{
			set
			{
				this.m_shadowMode = (byte)value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (set) Token: 0x060018D2 RID: 6354 RVA: 0x0003522D File Offset: 0x0003342D
		public bool receiveShadows
		{
			set
			{
				this.m_receiveShadows = (value ? 1 : 0);
			}
		}

		// Token: 0x17000385 RID: 901
		// (set) Token: 0x060018D3 RID: 6355 RVA: 0x0003523D File Offset: 0x0003343D
		public bool staticShadowCaster
		{
			set
			{
				this.m_staticShadowCaster = (value ? 1 : 0);
			}
		}

		// Token: 0x17000386 RID: 902
		// (set) Token: 0x060018D4 RID: 6356 RVA: 0x0003524D File Offset: 0x0003344D
		public bool allDepthSorted
		{
			set
			{
				this.m_allDepthSorted = (value ? 1 : 0);
			}
		}

		// Token: 0x04000AB4 RID: 2740
		public uint renderingLayerMask;

		// Token: 0x04000AB5 RID: 2741
		public int rendererPriority;

		// Token: 0x04000AB6 RID: 2742
		private ulong m_sceneCullingMask;

		// Token: 0x04000AB7 RID: 2743
		public byte layer;

		// Token: 0x04000AB8 RID: 2744
		private byte m_batchLayer;

		// Token: 0x04000AB9 RID: 2745
		private byte m_motionMode;

		// Token: 0x04000ABA RID: 2746
		private byte m_shadowMode;

		// Token: 0x04000ABB RID: 2747
		private byte m_receiveShadows;

		// Token: 0x04000ABC RID: 2748
		private byte m_staticShadowCaster;

		// Token: 0x04000ABD RID: 2749
		private byte m_allDepthSorted;

		// Token: 0x04000ABE RID: 2750
		private byte m_isSceneCullingMaskSet;
	}
}
