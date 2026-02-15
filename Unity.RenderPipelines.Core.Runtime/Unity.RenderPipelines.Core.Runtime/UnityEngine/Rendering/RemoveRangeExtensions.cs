using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace UnityEngine.Rendering
{
	// Token: 0x02000063 RID: 99
	public static class RemoveRangeExtensions
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x0000929C File Offset: 0x0000749C
		[CollectionAccess(CollectionAccessType.ModifyExistingContent)]
		[MustUseReturnValue]
		public static bool TryRemoveElementsInRange<TValue>([DisallowNull] this IList<TValue> list, int index, int count, [NotNullWhen(false)] out Exception error)
		{
			try
			{
				List<TValue> genericList = list as List<TValue>;
				if (genericList != null)
				{
					genericList.RemoveRange(index, count);
				}
				else
				{
					for (int i = count; i > 0; i--)
					{
						list.RemoveAt(index);
					}
				}
			}
			catch (Exception e)
			{
				error = e;
				return false;
			}
			error = null;
			return true;
		}
	}
}
