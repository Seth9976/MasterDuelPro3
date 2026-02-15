using System;

namespace TMPro
{
	// Token: 0x020000A0 RID: 160
	public struct CaretInfo
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x0002C485 File Offset: 0x0002A685
		public CaretInfo(int index, CaretPosition position)
		{
			this.index = index;
			this.position = position;
		}

		// Token: 0x04000582 RID: 1410
		public int index;

		// Token: 0x04000583 RID: 1411
		public CaretPosition position;
	}
}
