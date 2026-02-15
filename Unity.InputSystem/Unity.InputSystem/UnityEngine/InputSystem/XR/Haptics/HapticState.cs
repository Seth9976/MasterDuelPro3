using System;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x020000F6 RID: 246
	public struct HapticState
	{
		// Token: 0x06000C74 RID: 3188 RVA: 0x0003F7BE File Offset: 0x0003D9BE
		public HapticState(uint samplesQueued, uint samplesAvailable)
		{
			this.samplesQueued = samplesQueued;
			this.samplesAvailable = samplesAvailable;
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0003F7CE File Offset: 0x0003D9CE
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x0003F7D6 File Offset: 0x0003D9D6
		public uint samplesQueued { readonly get; private set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0003F7DF File Offset: 0x0003D9DF
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x0003F7E7 File Offset: 0x0003D9E7
		public uint samplesAvailable { readonly get; private set; }
	}
}
