using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020003D0 RID: 976
	internal class NetRes
	{
		// Token: 0x0600184B RID: 6219 RVA: 0x00067340 File Offset: 0x00065540
		public static string GetWebStatusString(string Res, WebExceptionStatus Status)
		{
			string @string = SR.GetString(WebExceptionMapping.GetWebStatusString(Status));
			string string2 = SR.GetString(Res);
			return string.Format(CultureInfo.CurrentCulture, string2, @string);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0006736C File Offset: 0x0006556C
		public static string GetWebStatusString(WebExceptionStatus Status)
		{
			return SR.GetString(WebExceptionMapping.GetWebStatusString(Status));
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0006737C File Offset: 0x0006557C
		public static string GetWebStatusCodeString(FtpStatusCode statusCode, string statusDescription)
		{
			string text = "(";
			int num = (int)statusCode;
			string text2 = text + num.ToString(NumberFormatInfo.InvariantInfo) + ")";
			string text3 = null;
			try
			{
				text3 = SR.GetString("net_ftpstatuscode_" + statusCode.ToString(), null);
			}
			catch
			{
			}
			if (text3 != null && text3.Length > 0)
			{
				text2 = text2 + " " + text3;
			}
			else if (statusDescription != null && statusDescription.Length > 0)
			{
				text2 = text2 + " " + statusDescription;
			}
			return text2;
		}
	}
}
