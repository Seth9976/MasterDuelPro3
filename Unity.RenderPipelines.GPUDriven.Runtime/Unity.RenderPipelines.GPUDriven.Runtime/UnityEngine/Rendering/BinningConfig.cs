using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000038 RID: 56
	internal struct BinningConfig
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005C04 File Offset: 0x00003E04
		public int visibilityConfigCount
		{
			get
			{
				int bitCount = 1 + this.viewCount + (this.supportsCrossFade ? 1 : 0) + (this.supportsMotionCheck ? 1 : 0);
				return 1 << bitCount;
			}
		}

		// Token: 0x040000C1 RID: 193
		public int viewCount;

		// Token: 0x040000C2 RID: 194
		public bool supportsCrossFade;

		// Token: 0x040000C3 RID: 195
		public bool supportsMotionCheck;
	}
}
