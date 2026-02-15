using System;
using System.Collections.Concurrent;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000509 RID: 1289
	internal sealed class NameCache
	{
		// Token: 0x060028C5 RID: 10437 RVA: 0x000A745C File Offset: 0x000A565C
		internal object GetCachedValue(string name)
		{
			this.name = name;
			object obj;
			if (!NameCache.ht.TryGetValue(name, out obj))
			{
				return null;
			}
			return obj;
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x000A7482 File Offset: 0x000A5682
		internal void SetCachedValue(object value)
		{
			NameCache.ht[this.name] = value;
		}

		// Token: 0x0400149B RID: 5275
		private static ConcurrentDictionary<string, object> ht = new ConcurrentDictionary<string, object>();

		// Token: 0x0400149C RID: 5276
		private string name;
	}
}
