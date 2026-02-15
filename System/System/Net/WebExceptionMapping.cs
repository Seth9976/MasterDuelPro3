using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020003BD RID: 957
	internal static class WebExceptionMapping
	{
		// Token: 0x060017C2 RID: 6082 RVA: 0x00065078 File Offset: 0x00063278
		internal static string GetWebStatusString(WebExceptionStatus status)
		{
			int num = (int)status;
			if (num >= WebExceptionMapping.s_Mapping.Length || num < 0)
			{
				throw new InternalException();
			}
			string text = Volatile.Read<string>(ref WebExceptionMapping.s_Mapping[num]);
			if (text == null)
			{
				text = "net_webstatus_" + status.ToString();
				Volatile.Write<string>(ref WebExceptionMapping.s_Mapping[num], text);
			}
			return text;
		}

		// Token: 0x04000EFE RID: 3838
		private static readonly string[] s_Mapping = new string[21];
	}
}
