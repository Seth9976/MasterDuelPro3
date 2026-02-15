using System;
using System.Text;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	public class WWWForm
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000241C File Offset: 0x0000061C
		internal static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.ASCII;
			}
		}

		// Token: 0x04000002 RID: 2
		private static byte[] dDash = WWWForm.DefaultEncoding.GetBytes("--");

		// Token: 0x04000003 RID: 3
		private static byte[] crlf = WWWForm.DefaultEncoding.GetBytes("\r\n");

		// Token: 0x04000004 RID: 4
		private static byte[] contentTypeHeader = WWWForm.DefaultEncoding.GetBytes("Content-Type: ");

		// Token: 0x04000005 RID: 5
		private static byte[] dispositionHeader = WWWForm.DefaultEncoding.GetBytes("Content-disposition: form-data; name=\"");

		// Token: 0x04000006 RID: 6
		private static byte[] endQuote = WWWForm.DefaultEncoding.GetBytes("\"");

		// Token: 0x04000007 RID: 7
		private static byte[] fileNameField = WWWForm.DefaultEncoding.GetBytes("; filename=\"");

		// Token: 0x04000008 RID: 8
		private static byte[] ampersand = WWWForm.DefaultEncoding.GetBytes("&");

		// Token: 0x04000009 RID: 9
		private static byte[] equal = WWWForm.DefaultEncoding.GetBytes("=");
	}
}
