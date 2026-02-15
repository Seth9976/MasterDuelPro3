using System;

namespace SevenZip.CommandLineParser
{
	// Token: 0x0200002A RID: 42
	public class CommandForm
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x0000897F File Offset: 0x00006B7F
		public CommandForm(string idString, bool postStringMode)
		{
			this.IDString = idString;
			this.PostStringMode = postStringMode;
		}

		// Token: 0x04000104 RID: 260
		public string IDString = "";

		// Token: 0x04000105 RID: 261
		public bool PostStringMode = false;
	}
}
