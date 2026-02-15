using System;
using System.Collections;

namespace SevenZip.CommandLineParser
{
	// Token: 0x020001A9 RID: 425
	public class SwitchResult
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x0001F598 File Offset: 0x0001D798
		public SwitchResult()
		{
			this.ThereIs = false;
		}

		// Token: 0x04000B1C RID: 2844
		public bool ThereIs;

		// Token: 0x04000B1D RID: 2845
		public bool WithMinus;

		// Token: 0x04000B1E RID: 2846
		public ArrayList PostStrings = new ArrayList();

		// Token: 0x04000B1F RID: 2847
		public int PostCharIndex;
	}
}
