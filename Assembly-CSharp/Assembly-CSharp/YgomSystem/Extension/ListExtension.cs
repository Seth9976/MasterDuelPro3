using System;
using System.Collections.Generic;

namespace YgomSystem.Extension
{
	// Token: 0x02000770 RID: 1904
	public static class ListExtension
	{
		// Token: 0x06003B4F RID: 15183 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsNullOrEmpty<T>(this List<T> self)
		{
			return false;
		}

		// Token: 0x06003B50 RID: 15184 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsExists<T>(this List<T> self)
		{
			return false;
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int SafeGetCount<T>(this List<T> self)
		{
			return 0;
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x000F393C File Offset: 0x000F1B3C
		public static T GetValueAt<T>(this List<object> list, int index, Func<T, bool> predicator)
		{
			return default(T);
		}

		// Token: 0x06003B53 RID: 15187 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDicAt(this List<object> list, int index)
		{
			return null;
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDicOrEmptyAt(this List<object> list, int index)
		{
			return null;
		}
	}
}
