using System;
using System.Collections.Generic;

namespace System.Dynamic.Utils
{
	// Token: 0x0200014A RID: 330
	internal static class Helpers
	{
		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002ADDC File Offset: 0x00028FDC
		internal static T CommonNode<T>(T first, T second, Func<T, T> parent) where T : class
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (@default.Equals(first, second))
			{
				return first;
			}
			HashSet<T> hashSet = new HashSet<T>(@default);
			for (T t = first; t != null; t = parent(t))
			{
				hashSet.Add(t);
			}
			for (T t2 = second; t2 != null; t2 = parent(t2))
			{
				if (hashSet.Contains(t2))
				{
					return t2;
				}
			}
			return default(T);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002AE48 File Offset: 0x00029048
		internal static void IncrementCount<T>(T key, Dictionary<T, int> dict)
		{
			int num;
			dict.TryGetValue(key, out num);
			dict[key] = num + 1;
		}
	}
}
