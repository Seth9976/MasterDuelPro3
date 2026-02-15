using System;
using System.Reflection;

namespace YgomSystem.Utility
{
	// Token: 0x02000513 RID: 1299
	public class Clipboard
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060029B5 RID: 10677 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool support
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x0000216A File Offset: 0x0000036A
		private static PropertyInfo GetSystemCopyBufferProperty()
		{
			return null;
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetText(string text)
		{
		}

		// Token: 0x04002954 RID: 10580
		private static PropertyInfo systemCopyBufferProperty;
	}
}
