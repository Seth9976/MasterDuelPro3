using System;

namespace System.Collections.Generic
{
	// Token: 0x0200075F RID: 1887
	public static class CollectionExtensions
	{
		// Token: 0x06003C17 RID: 15383 RVA: 0x000E80C0 File Offset: 0x000E62C0
		public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
		{
			return dictionary.GetValueOrDefault(key, default(TValue));
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x000E80E0 File Offset: 0x000E62E0
		public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				return defaultValue;
			}
			return tvalue;
		}
	}
}
