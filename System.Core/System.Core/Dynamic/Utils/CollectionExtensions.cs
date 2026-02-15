using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Dynamic.Utils
{
	// Token: 0x02000145 RID: 325
	internal static class CollectionExtensions
	{
		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002A6BC File Offset: 0x000288BC
		public static TrueReadOnlyCollection<T> AddFirst<T>(this ReadOnlyCollection<T> list, T item)
		{
			T[] array = new T[list.Count + 1];
			array[0] = item;
			list.CopyTo(array, 1);
			return new TrueReadOnlyCollection<T>(array);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002A6F0 File Offset: 0x000288F0
		public static T[] AddFirst<T>(this T[] array, T item)
		{
			T[] array2 = new T[array.Length + 1];
			array2[0] = item;
			array.CopyTo(array2, 1);
			return array2;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002A71C File Offset: 0x0002891C
		public static T[] AddLast<T>(this T[] array, T item)
		{
			T[] array2 = new T[array.Length + 1];
			array.CopyTo(array2, 0);
			array2[array.Length] = item;
			return array2;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002A748 File Offset: 0x00028948
		public static T[] RemoveFirst<T>(this T[] array)
		{
			T[] array2 = new T[array.Length - 1];
			Array.Copy(array, 1, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002A770 File Offset: 0x00028970
		public static T[] RemoveLast<T>(this T[] array)
		{
			T[] array2 = new T[array.Length - 1];
			Array.Copy(array, 0, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002A798 File Offset: 0x00028998
		public static ReadOnlyCollection<T> ToReadOnly<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				return EmptyReadOnlyCollection<T>.Instance;
			}
			TrueReadOnlyCollection<T> trueReadOnlyCollection = enumerable as TrueReadOnlyCollection<T>;
			if (trueReadOnlyCollection != null)
			{
				return trueReadOnlyCollection;
			}
			ReadOnlyCollectionBuilder<T> readOnlyCollectionBuilder = enumerable as ReadOnlyCollectionBuilder<T>;
			if (readOnlyCollectionBuilder != null)
			{
				return readOnlyCollectionBuilder.ToReadOnlyCollection();
			}
			T[] array = enumerable.ToArray<T>();
			if (array.Length != 0)
			{
				return new TrueReadOnlyCollection<T>(array);
			}
			return EmptyReadOnlyCollection<T>.Instance;
		}
	}
}
