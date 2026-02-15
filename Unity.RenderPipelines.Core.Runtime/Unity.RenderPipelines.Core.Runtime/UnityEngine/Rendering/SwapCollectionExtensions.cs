using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace UnityEngine.Rendering
{
	// Token: 0x02000068 RID: 104
	public static class SwapCollectionExtensions
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x000094B8 File Offset: 0x000076B8
		[CollectionAccess(CollectionAccessType.ModifyExistingContent)]
		[MustUseReturnValue]
		public static bool TrySwap<TValue>([DisallowNull] this IList<TValue> list, int from, int to, [NotNullWhen(false)] out Exception error)
		{
			error = null;
			if (list == null)
			{
				error = new ArgumentNullException("list");
			}
			else
			{
				if (from < 0 || from >= list.Count)
				{
					error = new ArgumentOutOfRangeException("from");
				}
				if (to < 0 || to >= list.Count)
				{
					error = new ArgumentOutOfRangeException("to");
				}
			}
			if (error != null)
			{
				return false;
			}
			TValue tvalue = list[from];
			TValue tvalue2 = list[to];
			list[to] = tvalue;
			list[from] = tvalue2;
			return true;
		}
	}
}
