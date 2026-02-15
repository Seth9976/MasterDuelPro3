using System;

namespace System.Collections.Generic
{
	// Token: 0x02000759 RID: 1881
	internal static class EnumerableHelpers
	{
		// Token: 0x06003BFC RID: 15356 RVA: 0x000E7AE8 File Offset: 0x000E5CE8
		internal static T[] ToArray<T>(IEnumerable<T> source)
		{
			ICollection<T> collection = source as ICollection<T>;
			if (collection == null)
			{
				LargeArrayBuilder<T> largeArrayBuilder = new LargeArrayBuilder<T>(true);
				largeArrayBuilder.AddRange(source);
				return largeArrayBuilder.ToArray();
			}
			int count = collection.Count;
			if (count == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[count];
			collection.CopyTo(array, 0);
			return array;
		}
	}
}
