using System;
using System.Collections;

namespace SevenZip.CommandLineParser
{
	// Token: 0x02000028 RID: 40
	public class SwitchResult
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00008402 File Offset: 0x00006602
		public SwitchResult()
		{
			this.ThereIs = false;
		}

		// Token: 0x040000FA RID: 250
		public bool ThereIs;

		// Token: 0x040000FB RID: 251
		public bool WithMinus;

		// Token: 0x040000FC RID: 252
		public ArrayList PostStrings = new ArrayList();

		// Token: 0x040000FD RID: 253
		public int PostCharIndex;
	}
}
