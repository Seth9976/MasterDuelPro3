using System;
using System.Reflection;
using System.Resources;

namespace System.Windows.Forms
{
	// Token: 0x02000007 RID: 7
	internal static class Locale
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020AD File Offset: 0x000002AD
		public static ResourceManager ResourceManager
		{
			get
			{
				return Locale.rm;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020B4 File Offset: 0x000002B4
		public static string GetText(string msg)
		{
			string @string = Locale.ResourceManager.GetString(msg);
			if (@string != null)
			{
				return @string;
			}
			return msg;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020D3 File Offset: 0x000002D3
		public static string GetText(string msg, params object[] args)
		{
			return string.Format(Locale.GetText(msg), args);
		}

		// Token: 0x04000002 RID: 2
		private static ResourceManager rm = new ResourceManager("System.Windows.Forms", Assembly.GetExecutingAssembly());
	}
}
