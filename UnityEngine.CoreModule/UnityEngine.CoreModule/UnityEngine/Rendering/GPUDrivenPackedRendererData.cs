using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000396 RID: 918
	[UsedByNativeCode]
	internal struct GPUDrivenPackedRendererData
	{
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x00035E33 File Offset: 0x00034033
		public bool staticShadowCaster
		{
			get
			{
				return (this.data & 2U) > 0U;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x00035E40 File Offset: 0x00034040
		public byte lodMask
		{
			get
			{
				return (byte)((this.data >> 2) & 255U);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x00035E51 File Offset: 0x00034051
		public ShadowCastingMode shadowCastingMode
		{
			get
			{
				return (ShadowCastingMode)((this.data >> 10) & 3U);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x00035E5E File Offset: 0x0003405E
		public LightProbeUsage lightProbeUsage
		{
			get
			{
				return (LightProbeUsage)((this.data >> 12) & 7U);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x00035E6B File Offset: 0x0003406B
		public MotionVectorGenerationMode motionVecGenMode
		{
			get
			{
				return (MotionVectorGenerationMode)((this.data >> 15) & 3U);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x00035E78 File Offset: 0x00034078
		public bool isPartOfStaticBatch
		{
			get
			{
				return (this.data & 131072U) > 0U;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x00035E89 File Offset: 0x00034089
		public bool hasTree
		{
			get
			{
				return (this.data & 524288U) > 0U;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x00035E9A File Offset: 0x0003409A
		public bool smallMeshCulling
		{
			get
			{
				return (this.data & 1048576U) > 0U;
			}
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00035EAB File Offset: 0x000340AB
		public GPUDrivenPackedRendererData()
		{
			this.data = 0U;
		}

		// Token: 0x04000B3E RID: 2878
		private uint data;
	}
}
