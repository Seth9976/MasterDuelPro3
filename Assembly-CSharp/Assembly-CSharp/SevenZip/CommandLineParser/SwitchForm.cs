using System;

namespace SevenZip.CommandLineParser
{
	// Token: 0x020001A8 RID: 424
	public class SwitchForm
	{
		// Token: 0x06000651 RID: 1617 RVA: 0x0001F544 File Offset: 0x0001D744
		public SwitchForm(string idString, SwitchType type, bool multi, int minLen, int maxLen, string postCharSet)
		{
			this.IDString = idString;
			this.Type = type;
			this.Multi = multi;
			this.MinLen = minLen;
			this.MaxLen = maxLen;
			this.PostCharSet = postCharSet;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001F579 File Offset: 0x0001D779
		public SwitchForm(string idString, SwitchType type, bool multi, int minLen)
			: this(idString, type, multi, minLen, 0, "")
		{
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001F58C File Offset: 0x0001D78C
		public SwitchForm(string idString, SwitchType type, bool multi)
			: this(idString, type, multi, 0)
		{
		}

		// Token: 0x04000B16 RID: 2838
		public string IDString;

		// Token: 0x04000B17 RID: 2839
		public SwitchType Type;

		// Token: 0x04000B18 RID: 2840
		public bool Multi;

		// Token: 0x04000B19 RID: 2841
		public int MinLen;

		// Token: 0x04000B1A RID: 2842
		public int MaxLen;

		// Token: 0x04000B1B RID: 2843
		public string PostCharSet;
	}
}
