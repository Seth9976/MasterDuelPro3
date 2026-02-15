using System;
using System.Text;

namespace System.Windows.Forms.RTF
{
	// Token: 0x0200038A RID: 906
	internal class RTFException : ApplicationException
	{
		// Token: 0x06001D72 RID: 7538 RVA: 0x00090B68 File Offset: 0x0008ED68
		public RTFException(RTF rtf, string error_message)
		{
			this.pos = rtf.LinePos;
			this.line = rtf.LineNumber;
			this.token_class = rtf.TokenClass;
			this.major = rtf.Major;
			this.minor = rtf.Minor;
			this.param = rtf.Param;
			this.text = rtf.Text;
			this.error_message = error_message;
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001D73 RID: 7539 RVA: 0x00090BD8 File Offset: 0x0008EDD8
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.error_message);
				stringBuilder.Append("\n");
				stringBuilder.Append(string.Concat(new object[] { "RTF Stream Info: Pos:", this.pos, " Line:", this.line }));
				stringBuilder.Append("\n");
				stringBuilder.Append("TokenClass:" + this.token_class + ", ");
				stringBuilder.Append("Major:" + string.Format("{0}", (int)this.major) + ", ");
				stringBuilder.Append("Minor:" + string.Format("{0}", (int)this.minor) + ", ");
				stringBuilder.Append("Param:" + string.Format("{0}", this.param) + ", ");
				stringBuilder.Append("Text:" + this.text);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x04001B52 RID: 6994
		private int pos;

		// Token: 0x04001B53 RID: 6995
		private int line;

		// Token: 0x04001B54 RID: 6996
		private TokenClass token_class;

		// Token: 0x04001B55 RID: 6997
		private Major major;

		// Token: 0x04001B56 RID: 6998
		private Minor minor;

		// Token: 0x04001B57 RID: 6999
		private int param;

		// Token: 0x04001B58 RID: 7000
		private string text;

		// Token: 0x04001B59 RID: 7001
		private string error_message;
	}
}
