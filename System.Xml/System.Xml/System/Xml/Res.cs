using System;

namespace System.Xml
{
	// Token: 0x02000135 RID: 309
	internal static class Res
	{
		// Token: 0x06000F58 RID: 3928 RVA: 0x00035F33 File Offset: 0x00034133
		public static string GetString(string name)
		{
			return name;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0004CCC6 File Offset: 0x0004AEC6
		public static string GetString(string name, params object[] args)
		{
			if (args == null)
			{
				return name;
			}
			return SR.GetString(name, args);
		}
	}
}
