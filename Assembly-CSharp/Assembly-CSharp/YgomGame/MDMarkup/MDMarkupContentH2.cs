using System;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B97 RID: 2967
	[Serializable]
	public class MDMarkupContentH2 : MDMarkupContentH1
	{
		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x0600552A RID: 21802 RVA: 0x000029CC File Offset: 0x00000BCC
		public override MDMarkupDef.MarkupType markupType
		{
			get
			{
				return MDMarkupDef.MarkupType.None;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x0600552B RID: 21803 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int contentIndent
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600552C RID: 21804 RVA: 0x000F4C9C File Offset: 0x000F2E9C
		public MDMarkupContentH2()
		{
		}

		// Token: 0x0600552D RID: 21805 RVA: 0x000F4C9C File Offset: 0x000F2E9C
		public MDMarkupContentH2(string rawText)
		{
		}
	}
}
