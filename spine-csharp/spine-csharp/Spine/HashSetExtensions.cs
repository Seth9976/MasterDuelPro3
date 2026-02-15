using System;
using System.Collections.Generic;

namespace Spine
{
	// Token: 0x0200003D RID: 61
	public static class HashSetExtensions
	{
		// Token: 0x06000185 RID: 389 RVA: 0x000093CC File Offset: 0x000075CC
		public static bool AddAll<T>(this HashSet<T> set, T[] addSet)
		{
			bool anyItemAdded = false;
			int i = 0;
			int j = addSet.Length;
			while (i < j)
			{
				T item = addSet[i];
				anyItemAdded |= set.Add(item);
				i++;
			}
			return anyItemAdded;
		}
	}
}
