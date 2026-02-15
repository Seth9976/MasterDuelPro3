using System;

namespace System.Drawing
{
	// Token: 0x0200006E RID: 110
	internal struct IconInfo
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000D15A File Offset: 0x0000B35A
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0000D165 File Offset: 0x0000B365
		public bool IsIcon
		{
			get
			{
				return this.fIcon == 1;
			}
			set
			{
				this.fIcon = (value ? 1 : 0);
			}
		}

		// Token: 0x04000205 RID: 517
		private int fIcon;

		// Token: 0x04000206 RID: 518
		public int xHotspot;

		// Token: 0x04000207 RID: 519
		public int yHotspot;

		// Token: 0x04000208 RID: 520
		public IntPtr hbmMask;

		// Token: 0x04000209 RID: 521
		public IntPtr hbmColor;
	}
}
