using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000248 RID: 584
	internal static class MiscHelpers
	{
		// Token: 0x06001559 RID: 5465 RVA: 0x00061A78 File Offset: 0x0005FC78
		public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
		{
			TValue value;
			if (!dictionary.TryGetValue(key, out value))
			{
				return default(TValue);
			}
			return value;
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00061A9B File Offset: 0x0005FC9B
		public static IEnumerable<TValue> EveryNth<TValue>(this IEnumerable<TValue> enumerable, int n, int start = 0)
		{
			int index = 0;
			foreach (TValue element in enumerable)
			{
				if (index < start)
				{
					int num = index + 1;
					index = num;
				}
				else
				{
					if ((index - start) % n == 0)
					{
						yield return element;
					}
					int num = index + 1;
					index = num;
				}
			}
			IEnumerator<TValue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00061ABC File Offset: 0x0005FCBC
		public static int IndexOf<TValue>(this IEnumerable<TValue> enumerable, TValue value)
		{
			int index = 0;
			foreach (TValue element in enumerable)
			{
				if (EqualityComparer<TValue>.Default.Equals(element, value))
				{
					return index;
				}
				index++;
			}
			return -1;
		}
	}
}
