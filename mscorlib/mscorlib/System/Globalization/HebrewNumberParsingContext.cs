using System;

namespace System.Globalization
{
	// Token: 0x0200069C RID: 1692
	internal struct HebrewNumberParsingContext
	{
		// Token: 0x06003532 RID: 13618 RVA: 0x000CC100 File Offset: 0x000CA300
		public HebrewNumberParsingContext(int result)
		{
			this.state = HebrewNumber.HS.Start;
			this.result = result;
		}

		// Token: 0x04001C37 RID: 7223
		internal HebrewNumber.HS state;

		// Token: 0x04001C38 RID: 7224
		internal int result;
	}
}
