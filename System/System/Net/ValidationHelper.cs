using System;

namespace System.Net
{
	// Token: 0x020003B2 RID: 946
	internal static class ValidationHelper
	{
		// Token: 0x06001789 RID: 6025 RVA: 0x0006489F File Offset: 0x00062A9F
		public static bool IsBlankString(string stringValue)
		{
			return stringValue == null || stringValue.Length == 0;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0005ECC0 File Offset: 0x0005CEC0
		public static bool ValidateTcpPort(int port)
		{
			return port >= 0 && port <= 65535;
		}

		// Token: 0x04000ECF RID: 3791
		public static string[] EmptyArray = new string[0];

		// Token: 0x04000ED0 RID: 3792
		internal static readonly char[] InvalidMethodChars = new char[] { ' ', '\r', '\n', '\t' };

		// Token: 0x04000ED1 RID: 3793
		internal static readonly char[] InvalidParamChars = new char[]
		{
			'(', ')', '<', '>', '@', ',', ';', ':', '\\', '"',
			'\'', '/', '[', ']', '?', '=', '{', '}', ' ', '\t',
			'\r', '\n'
		};
	}
}
