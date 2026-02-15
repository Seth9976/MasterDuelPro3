using System;

namespace SevenZip.CommandLineParser
{
	// Token: 0x02000027 RID: 39
	public class SwitchForm
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x000083A8 File Offset: 0x000065A8
		public SwitchForm(string idString, SwitchType type, bool multi, int minLen, int maxLen, string postCharSet)
		{
			this.IDString = idString;
			this.Type = type;
			this.Multi = multi;
			this.MinLen = minLen;
			this.MaxLen = maxLen;
			this.PostCharSet = postCharSet;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000083DF File Offset: 0x000065DF
		public SwitchForm(string idString, SwitchType type, bool multi, int minLen)
			: this(idString, type, multi, minLen, 0, "")
		{
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000083F4 File Offset: 0x000065F4
		public SwitchForm(string idString, SwitchType type, bool multi)
			: this(idString, type, multi, 0)
		{
		}

		// Token: 0x040000F4 RID: 244
		public string IDString;

		// Token: 0x040000F5 RID: 245
		public SwitchType Type;

		// Token: 0x040000F6 RID: 246
		public bool Multi;

		// Token: 0x040000F7 RID: 247
		public int MinLen;

		// Token: 0x040000F8 RID: 248
		public int MaxLen;

		// Token: 0x040000F9 RID: 249
		public string PostCharSet;
	}
}
