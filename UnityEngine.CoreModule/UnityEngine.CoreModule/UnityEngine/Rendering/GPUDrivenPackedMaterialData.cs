using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000397 RID: 919
	[UsedByNativeCode]
	internal struct GPUDrivenPackedMaterialData
	{
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x00035EB5 File Offset: 0x000340B5
		public bool isTransparent
		{
			get
			{
				return (this.data & 1U) > 0U;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x00035EC2 File Offset: 0x000340C2
		public bool isMotionVectorsPassEnabled
		{
			get
			{
				return (this.data & 2U) > 0U;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x00035ECF File Offset: 0x000340CF
		public bool isIndirectSupported
		{
			get
			{
				return (this.data & 4U) > 0U;
			}
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00035EDC File Offset: 0x000340DC
		public GPUDrivenPackedMaterialData()
		{
			this.data = 0U;
		}

		// Token: 0x04000B3F RID: 2879
		private uint data;
	}
}
