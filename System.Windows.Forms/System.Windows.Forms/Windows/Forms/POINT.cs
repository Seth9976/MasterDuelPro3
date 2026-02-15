using System;

namespace System.Windows.Forms
{
	// Token: 0x020002A0 RID: 672
	internal struct POINT
	{
		// Token: 0x06001912 RID: 6418 RVA: 0x00079014 File Offset: 0x00077214
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Point {",
				this.x.ToString(),
				", ",
				this.y.ToString(),
				"}"
			});
		}

		// Token: 0x0400135D RID: 4957
		public int x;

		// Token: 0x0400135E RID: 4958
		public int y;
	}
}
