using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DF RID: 223
	public struct ShaderDebugPrintInput
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00011048 File Offset: 0x0000F248
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x00011050 File Offset: 0x0000F250
		public Vector2 pos { readonly get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00011059 File Offset: 0x0000F259
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x00011061 File Offset: 0x0000F261
		public bool leftDown { readonly get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001106A File Offset: 0x0000F26A
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x00011072 File Offset: 0x0000F272
		public bool rightDown { readonly get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001107B File Offset: 0x0000F27B
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x00011083 File Offset: 0x0000F283
		public bool middleDown { readonly get; set; }

		// Token: 0x0600073A RID: 1850 RVA: 0x0001108C File Offset: 0x0000F28C
		public string String()
		{
			return string.Format("Mouse: {0}x{1}  Btns: Left:{2} Right:{3} Middle:{4} ", new object[]
			{
				this.pos.x,
				this.pos.y,
				this.leftDown,
				this.rightDown,
				this.middleDown
			});
		}
	}
}
