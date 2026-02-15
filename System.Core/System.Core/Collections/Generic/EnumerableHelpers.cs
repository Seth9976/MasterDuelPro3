using System;
using System.Linq;

namespace System.Collections.Generic
{
	// Token: 0x02000152 RID: 338
	internal static class EnumerableHelpers
	{
		// Token: 0x06000B34 RID: 2868 RVA: 0x0002BFB4 File Offset: 0x0002A1B4
		internal static bool TryGetCount<T>(IEnumerable<T> source, out int count)
		{
			ICollection<T> collection = source as ICollection<T>;
			if (collection != null)
			{
				count = collection.Count;
				return true;
			}
			IIListProvider<T> iilistProvider = source as IIListProvider<T>;
			if (iilistProvider != null)
			{
				return (count = iilistProvider.GetCount(true)) >= 0;
			}
			count = -1;
			return false;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002BFF8 File Offset: 0x0002A1F8
		internal static void Copy<T>(IEnumerable<T> source, T[] array, int arrayIndex, int count)
		{
			ICollection<T> collection = source as ICollection<T>;
			if (collection != null)
			{
				collection.CopyTo(array, arrayIndex);
				return;
			}
			EnumerableHelpers.IterativeCopy<T>(source, array, arrayIndex, count);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002C024 File Offset: 0x0002A224
		internal static void IterativeCopy<T>(IEnumerable<T> source, T[] array, int arrayIndex, int count)
		{
			foreach (T t in source)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002C074 File Offset: 0x0002A274
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

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002C0C4 File Offset: 0x0002A2C4
		internal static T[] ToArray<T>(IEnumerable<T> source, out int length)
		{
			ICollection<T> collection = source as ICollection<T>;
			if (collection != null)
			{
				int count = collection.Count;
				if (count != 0)
				{
					T[] array = new T[count];
					collection.CopyTo(array, 0);
					length = count;
					return array;
				}
			}
			else
			{
				using (IEnumerator<T> enumerator = source.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						T[] array2 = new T[4];
						array2[0] = enumerator.Current;
						int num = 1;
						while (enumerator.MoveNext())
						{
							if (num == array2.Length)
							{
								int num2 = num << 1;
								if (num2 > 2146435071)
								{
									num2 = ((2146435071 <= num) ? (num + 1) : 2146435071);
								}
								Array.Resize<T>(ref array2, num2);
							}
							array2[num++] = enumerator.Current;
						}
						length = num;
						return array2;
					}
				}
			}
			length = 0;
			return Array.Empty<T>();
		}
	}
}
