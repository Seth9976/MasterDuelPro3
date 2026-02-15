using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using YgomSystem.YGomTMPro;

namespace YgomSystem
{
	// Token: 0x020004C0 RID: 1216
	public class RubyTextEx : ExtendedTextMeshProUGUI
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600272A RID: 10026 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600272B RID: 10027 RVA: 0x0000216D File Offset: 0x0000036D
		public bool RubyInfoCalculated
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetText(string str, RubyTextEx.Mode mode, RubyRoot.Lang lang, List<RubyRoot.RubyInfo> infoList, Action populated = null)
		{
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnPopulateRubyText(TMP_TextInfo tmpinfo)
		{
		}

		// Token: 0x04002808 RID: 10248
		private string orgStr;

		// Token: 0x04002809 RID: 10249
		private RubyTextEx.Mode textMode;

		// Token: 0x0400280A RID: 10250
		private RubyRoot.Lang textLang;

		// Token: 0x0400280B RID: 10251
		private List<RubyRoot.RubyInfo> rubyInfoList;

		// Token: 0x0400280C RID: 10252
		private Action populatedAction;

		// Token: 0x020004C1 RID: 1217
		public enum Mode
		{
			// Token: 0x0400280E RID: 10254
			BaseText,
			// Token: 0x0400280F RID: 10255
			RubyText
		}
	}
}
