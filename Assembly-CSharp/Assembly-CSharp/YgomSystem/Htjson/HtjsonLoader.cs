using System;
using System.Collections.Generic;

namespace YgomSystem.Htjson
{
	// Token: 0x0200075A RID: 1882
	public class HtjsonLoader
	{
		// Token: 0x06003ACA RID: 15050 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OverrideByteLoader(Func<string, byte[]> loader)
		{
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetCommonStyles()
		{
			return null;
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x0000216A File Offset: 0x0000036A
		public static object byteToJsonObject(byte[] data)
		{
			return null;
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> byteToDictionary(byte[] data)
		{
			return null;
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> byteToList(byte[] data)
		{
			return null;
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> loadedImmediateResourceDictionary(string path)
		{
			return null;
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<object> loadedImmediateResourceList(string path)
		{
			return null;
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x0000216A File Offset: 0x0000036A
		public static object loadedImmediateJson(string url)
		{
			return null;
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x0000216D File Offset: 0x0000036D
		public static void loadSchemeUrl(string url, Action<object> loaded, Action failed)
		{
		}

		// Token: 0x04003460 RID: 13408
		private static Dictionary<string, object> commonStyles;

		// Token: 0x04003461 RID: 13409
		private static Func<string, byte[]> byteloader;
	}
}
