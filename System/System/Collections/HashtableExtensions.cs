using System;

namespace System.Collections
{
	// Token: 0x020002F4 RID: 756
	internal static class HashtableExtensions
	{
		// Token: 0x0600122E RID: 4654 RVA: 0x0005249F File Offset: 0x0005069F
		public static bool TryGetValue<T>(this Hashtable table, object key, out T value)
		{
			if (table.ContainsKey(key))
			{
				value = (T)((object)table[key]);
				return true;
			}
			value = default(T);
			return false;
		}
	}
}
