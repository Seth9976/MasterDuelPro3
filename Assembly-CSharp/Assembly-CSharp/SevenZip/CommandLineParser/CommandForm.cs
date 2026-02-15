using System;

namespace SevenZip.CommandLineParser
{
	// Token: 0x020001AB RID: 427
	public class CommandForm
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x0001F9BA File Offset: 0x0001DBBA
		public CommandForm(string idString, bool postStringMode)
		{
			this.IDString = idString;
			this.PostStringMode = postStringMode;
		}

		// Token: 0x04000B26 RID: 2854
		public string IDString = "";

		// Token: 0x04000B27 RID: 2855
		public bool PostStringMode;
	}
}
