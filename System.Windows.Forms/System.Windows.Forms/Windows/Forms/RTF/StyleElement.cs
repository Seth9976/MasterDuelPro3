using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x0200038D RID: 909
	internal class StyleElement
	{
		// Token: 0x06001D80 RID: 7552 RVA: 0x00090DFC File Offset: 0x0008EFFC
		public StyleElement(Style s, TokenClass token_class, Major major, Minor minor, int param, string text)
		{
			this.token_class = token_class;
			this.major = major;
			this.minor = minor;
			this.param = param;
			this.text = text;
			lock (s)
			{
				if (s.Elements == null)
				{
					s.Elements = this;
				}
				else
				{
					StyleElement elements = s.Elements;
					while (elements.next != null)
					{
						elements = elements.next;
					}
					elements.next = this;
				}
			}
		}

		// Token: 0x04001CC4 RID: 7364
		private TokenClass token_class;

		// Token: 0x04001CC5 RID: 7365
		private Major major;

		// Token: 0x04001CC6 RID: 7366
		private Minor minor;

		// Token: 0x04001CC7 RID: 7367
		private int param;

		// Token: 0x04001CC8 RID: 7368
		private string text;

		// Token: 0x04001CC9 RID: 7369
		private StyleElement next;
	}
}
