using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	/// <summary>Provides a set of static (Shared in Visual Basic) methods for querying objects that implement <see cref="T:System.Collections.Generic.IEnumerable`1" />.</summary>
	// Token: 0x02000019 RID: 25
	public static class Enumerable
	{
		/// <summary>Applies an accumulator function over a sequence.</summary>
		/// <returns>The final accumulator value.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to aggregate over.</param>
		/// <param name="func">An accumulator function to be invoked on each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="func" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000060 RID: 96 RVA: 0x00004398 File Offset: 0x00002598
		public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (func == null)
			{
				throw Error.ArgumentNull("func");
			}
			TSource tsource3;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				TSource tsource = enumerator.Current;
				while (enumerator.MoveNext())
				{
					TSource tsource2 = enumerator.Current;
					tsource = func(tsource, tsource2);
				}
				tsource3 = tsource;
			}
			return tsource3;
		}

		/// <summary>Determines whether a sequence contains any elements.</summary>
		/// <returns>true if the source sequence contains any elements; otherwise, false.</returns>
		/// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to check for emptiness.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000061 RID: 97 RVA: 0x00004414 File Offset: 0x00002614
		public static bool Any<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			bool flag;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				flag = enumerator.MoveNext();
			}
			return flag;
		}

		/// <summary>Determines whether any element of a sequence satisfies a condition.</summary>
		/// <returns>true if any elements in the source sequence pass the test in the specified predicate; otherwise, false.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to apply the predicate to.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x06000062 RID: 98 RVA: 0x0000445C File Offset: 0x0000265C
		public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			foreach (TSource tsource in source)
			{
				if (predicate(tsource))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether all elements of a sequence satisfy a condition.</summary>
		/// <returns>true if every element of the source sequence passes the test in the specified predicate, or if the sequence is empty; otherwise, false.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements to apply the predicate to.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x06000063 RID: 99 RVA: 0x000044CC File Offset: 0x000026CC
		public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			foreach (TSource tsource in source)
			{
				if (!predicate(tsource))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Filters the elements of an <see cref="T:System.Collections.IEnumerable" /> based on a specified type.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements from the input sequence of type <paramref name="TResult" />.</returns>
		/// <param name="source">The <see cref="T:System.Collections.IEnumerable" /> whose elements to filter.</param>
		/// <typeparam name="TResult">The type to filter the elements of the sequence on.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000064 RID: 100 RVA: 0x0000453C File Offset: 0x0000273C
		public static IEnumerable<TResult> OfType<TResult>(this IEnumerable source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return Enumerable.OfTypeIterator<TResult>(source);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004552 File Offset: 0x00002752
		private static IEnumerable<TResult> OfTypeIterator<TResult>(IEnumerable source)
		{
			foreach (object obj in source)
			{
				if (obj is TResult)
				{
					yield return (TResult)((object)obj);
				}
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Casts the elements of an <see cref="T:System.Collections.IEnumerable" /> to the specified type.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains each element of the source sequence cast to the specified type.</returns>
		/// <param name="source">The <see cref="T:System.Collections.IEnumerable" /> that contains the elements to be cast to type <paramref name="TResult" />.</param>
		/// <typeparam name="TResult">The type to cast the elements of <paramref name="source" /> to.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">An element in the sequence cannot be cast to type <paramref name="TResult" />.</exception>
		// Token: 0x06000066 RID: 102 RVA: 0x00004564 File Offset: 0x00002764
		public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
		{
			IEnumerable<TResult> enumerable = source as IEnumerable<TResult>;
			if (enumerable != null)
			{
				return enumerable;
			}
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return Enumerable.CastIterator<TResult>(source);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004591 File Offset: 0x00002791
		private static IEnumerable<TResult> CastIterator<TResult>(IEnumerable source)
		{
			foreach (object obj in source)
			{
				yield return (TResult)((object)obj);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Concatenates two sequences.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the concatenated elements of the two input sequences.</returns>
		/// <param name="first">The first sequence to concatenate.</param>
		/// <param name="second">The sequence to concatenate to the first sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x06000068 RID: 104 RVA: 0x000045A4 File Offset: 0x000027A4
		public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			Enumerable.ConcatIterator<TSource> concatIterator = first as Enumerable.ConcatIterator<TSource>;
			if (concatIterator == null)
			{
				return new Enumerable.Concat2Iterator<TSource>(first, second);
			}
			return concatIterator.Concat(second);
		}

		/// <summary>Determines whether a sequence contains a specified element by using the default equality comparer.</summary>
		/// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
		/// <param name="source">A sequence in which to locate a value.</param>
		/// <param name="value">The value to locate in the sequence.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000069 RID: 105 RVA: 0x000045E8 File Offset: 0x000027E8
		public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection == null)
			{
				return source.Contains(value, null);
			}
			return collection.Contains(value);
		}

		/// <summary>Determines whether a sequence contains a specified element by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
		/// <param name="source">A sequence in which to locate a value.</param>
		/// <param name="value">The value to locate in the sequence.</param>
		/// <param name="comparer">An equality comparer to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x0600006A RID: 106 RVA: 0x00004610 File Offset: 0x00002810
		public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (comparer == null)
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TSource tsource = enumerator.Current;
						if (EqualityComparer<TSource>.Default.Equals(tsource, value))
						{
							return true;
						}
					}
					return false;
				}
			}
			foreach (TSource tsource2 in source)
			{
				if (comparer.Equals(tsource2, value))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Returns the number of elements in a sequence.</summary>
		/// <returns>The number of elements in the input sequence.</returns>
		/// <param name="source">A sequence that contains elements to be counted.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600006B RID: 107 RVA: 0x000046B8 File Offset: 0x000028B8
		public static int Count<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection != null)
			{
				return collection.Count;
			}
			IIListProvider<TSource> iilistProvider = source as IIListProvider<TSource>;
			if (iilistProvider != null)
			{
				return iilistProvider.GetCount(false);
			}
			ICollection collection2 = source as ICollection;
			if (collection2 != null)
			{
				return collection2.Count;
			}
			int num = 0;
			checked
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						num++;
					}
				}
				return num;
			}
		}

		/// <summary>Returns a number that represents how many elements in the specified sequence satisfy a condition.</summary>
		/// <returns>A number that represents how many elements in the sequence satisfy the condition in the predicate function.</returns>
		/// <param name="source">A sequence that contains elements to be tested and counted.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600006C RID: 108 RVA: 0x00004740 File Offset: 0x00002940
		public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			int num = 0;
			checked
			{
				foreach (TSource tsource in source)
				{
					if (predicate(tsource))
					{
						num++;
					}
				}
				return num;
			}
		}

		/// <summary>Returns distinct elements from a sequence by using the default equality comparer to compare values.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains distinct elements from the source sequence.</returns>
		/// <param name="source">The sequence to remove duplicate elements from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x0600006D RID: 109 RVA: 0x000047B0 File Offset: 0x000029B0
		public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
		{
			return source.Distinct(null);
		}

		/// <summary>Returns distinct elements from a sequence by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains distinct elements from the source sequence.</returns>
		/// <param name="source">The sequence to remove duplicate elements from.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x0600006E RID: 110 RVA: 0x000047B9 File Offset: 0x000029B9
		public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return new Enumerable.DistinctIterator<TSource>(source, comparer);
		}

		/// <summary>Returns the element at a specified index in a sequence.</summary>
		/// <returns>The element at the specified position in the source sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than or equal to the number of elements in <paramref name="source" />.</exception>
		// Token: 0x0600006F RID: 111 RVA: 0x000047D0 File Offset: 0x000029D0
		public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IPartition<TSource> partition = source as IPartition<TSource>;
			if (partition != null)
			{
				bool flag;
				TSource tsource = partition.TryGetElementAt(index, out flag);
				if (flag)
				{
					return tsource;
				}
			}
			else
			{
				IList<TSource> list = source as IList<TSource>;
				if (list != null)
				{
					return list[index];
				}
				if (index >= 0)
				{
					using (IEnumerator<TSource> enumerator = source.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (index == 0)
							{
								return enumerator.Current;
							}
							index--;
						}
					}
				}
			}
			throw Error.ArgumentOutOfRange("index");
		}

		/// <summary>Returns the element at a specified index in a sequence or a default value if the index is out of range.</summary>
		/// <returns>default(<paramref name="TSource" />) if the index is outside the bounds of the source sequence; otherwise, the element at the specified position in the source sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000070 RID: 112 RVA: 0x0000486C File Offset: 0x00002A6C
		public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IPartition<TSource> partition = source as IPartition<TSource>;
			if (partition != null)
			{
				bool flag;
				return partition.TryGetElementAt(index, out flag);
			}
			if (index >= 0)
			{
				IList<TSource> list = source as IList<TSource>;
				if (list != null)
				{
					if (index < list.Count)
					{
						return list[index];
					}
				}
				else
				{
					using (IEnumerator<TSource> enumerator = source.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (index == 0)
							{
								return enumerator.Current;
							}
							index--;
						}
					}
				}
			}
			return default(TSource);
		}

		/// <summary>Returns an empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> that has the specified type argument.</summary>
		/// <returns>An empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose type argument is <paramref name="TResult" />.</returns>
		/// <typeparam name="TResult">The type to assign to the type parameter of the returned generic <see cref="T:System.Collections.Generic.IEnumerable`1" />.</typeparam>
		// Token: 0x06000071 RID: 113 RVA: 0x00004908 File Offset: 0x00002B08
		public static IEnumerable<TResult> Empty<TResult>()
		{
			return Array.Empty<TResult>();
		}

		/// <summary>Produces the set difference of two sequences by using the default equality comparer to compare values.</summary>
		/// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
		/// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements that are not also in <paramref name="second" /> will be returned.</param>
		/// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x06000072 RID: 114 RVA: 0x0000490F File Offset: 0x00002B0F
		public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			return Enumerable.ExceptIterator<TSource>(first, second, null);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004935 File Offset: 0x00002B35
		private static IEnumerable<TSource> ExceptIterator<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			Set<TSource> set = new Set<TSource>(comparer);
			foreach (TSource tsource in second)
			{
				set.Add(tsource);
			}
			foreach (TSource tsource2 in first)
			{
				if (set.Add(tsource2))
				{
					yield return tsource2;
				}
			}
			IEnumerator<TSource> enumerator2 = null;
			yield break;
			yield break;
		}

		/// <summary>Returns the first element of a sequence.</summary>
		/// <returns>The first element in the specified sequence.</returns>
		/// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the first element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
		// Token: 0x06000074 RID: 116 RVA: 0x00004954 File Offset: 0x00002B54
		public static TSource First<TSource>(this IEnumerable<TSource> source)
		{
			bool flag;
			TSource tsource = source.TryGetFirst(out flag);
			if (!flag)
			{
				throw Error.NoElements();
			}
			return tsource;
		}

		/// <summary>Returns the first element in a sequence that satisfies a specified condition.</summary>
		/// <returns>The first element in the sequence that passes the test in the specified predicate function.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
		// Token: 0x06000075 RID: 117 RVA: 0x00004974 File Offset: 0x00002B74
		public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			bool flag;
			TSource tsource = source.TryGetFirst(predicate, out flag);
			if (!flag)
			{
				throw Error.NoMatch();
			}
			return tsource;
		}

		/// <summary>Returns the first element of a sequence, or a default value if the sequence contains no elements.</summary>
		/// <returns>default(<paramref name="TSource" />) if <paramref name="source" /> is empty; otherwise, the first element in <paramref name="source" />.</returns>
		/// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the first element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000076 RID: 118 RVA: 0x00004994 File Offset: 0x00002B94
		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			bool flag;
			return source.TryGetFirst(out flag);
		}

		/// <summary>Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.</summary>
		/// <returns>default(<paramref name="TSource" />) if <paramref name="source" /> is empty or if no element passes the test specified by <paramref name="predicate" />; otherwise, the first element in <paramref name="source" /> that passes the test specified by <paramref name="predicate" />.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x06000077 RID: 119 RVA: 0x000049AC File Offset: 0x00002BAC
		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			bool flag;
			return source.TryGetFirst(predicate, out flag);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000049C4 File Offset: 0x00002BC4
		private static TSource TryGetFirst<TSource>(this IEnumerable<TSource> source, out bool found)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IPartition<TSource> partition = source as IPartition<TSource>;
			if (partition != null)
			{
				return partition.TryGetFirst(out found);
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				if (list.Count > 0)
				{
					found = true;
					return list[0];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						found = true;
						return enumerator.Current;
					}
				}
			}
			found = false;
			return default(TSource);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004A58 File Offset: 0x00002C58
		private static TSource TryGetFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, out bool found)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			OrderedEnumerable<TSource> orderedEnumerable = source as OrderedEnumerable<TSource>;
			if (orderedEnumerable != null)
			{
				return orderedEnumerable.TryGetFirst(predicate, out found);
			}
			foreach (TSource tsource in source)
			{
				if (predicate(tsource))
				{
					found = true;
					return tsource;
				}
			}
			found = false;
			return default(TSource);
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function.</summary>
		/// <returns>An IEnumerable&lt;IGrouping&lt;TKey, TSource&gt;&gt; in C# or IEnumerable(Of IGrouping(Of TKey, TSource)) in Visual Basic where each <see cref="T:System.Linq.IGrouping`2" /> object contains a sequence of objects and a key.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x0600007A RID: 122 RVA: 0x00004AE8 File Offset: 0x00002CE8
		public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return new GroupedEnumerable<TSource, TKey>(source, keySelector, null);
		}

		/// <summary>Produces the set intersection of two sequences by using the default equality comparer to compare values.</summary>
		/// <returns>A sequence that contains the elements that form the set intersection of two sequences.</returns>
		/// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements that also appear in <paramref name="second" /> will be returned.</param>
		/// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements that also appear in the first sequence will be returned.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x0600007B RID: 123 RVA: 0x00004AF2 File Offset: 0x00002CF2
		public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			return Enumerable.IntersectIterator<TSource>(first, second, null);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004B18 File Offset: 0x00002D18
		private static IEnumerable<TSource> IntersectIterator<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			Set<TSource> set = new Set<TSource>(comparer);
			foreach (TSource tsource in second)
			{
				set.Add(tsource);
			}
			foreach (TSource tsource2 in first)
			{
				if (set.Remove(tsource2))
				{
					yield return tsource2;
				}
			}
			IEnumerator<TSource> enumerator2 = null;
			yield break;
			yield break;
		}

		/// <summary>Returns the last element of a sequence.</summary>
		/// <returns>The value at the last position in the source sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the last element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
		// Token: 0x0600007D RID: 125 RVA: 0x00004B38 File Offset: 0x00002D38
		public static TSource Last<TSource>(this IEnumerable<TSource> source)
		{
			bool flag;
			TSource tsource = source.TryGetLast(out flag);
			if (!flag)
			{
				throw Error.NoElements();
			}
			return tsource;
		}

		/// <summary>Returns the last element of a sequence, or a default value if the sequence contains no elements.</summary>
		/// <returns>default(<paramref name="TSource" />) if the source sequence is empty; otherwise, the last element in the <see cref="T:System.Collections.Generic.IEnumerable`1" />.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the last element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x0600007E RID: 126 RVA: 0x00004B58 File Offset: 0x00002D58
		public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			bool flag;
			return source.TryGetLast(out flag);
		}

		/// <summary>Returns the last element of a sequence that satisfies a condition or a default value if no such element is found.</summary>
		/// <returns>default(<paramref name="TSource" />) if the sequence is empty or if no elements pass the test in the predicate function; otherwise, the last element that passes the test in the predicate function.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x0600007F RID: 127 RVA: 0x00004B70 File Offset: 0x00002D70
		public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			bool flag;
			return source.TryGetLast(predicate, out flag);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004B88 File Offset: 0x00002D88
		private static TSource TryGetLast<TSource>(this IEnumerable<TSource> source, out bool found)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IPartition<TSource> partition = source as IPartition<TSource>;
			if (partition != null)
			{
				return partition.TryGetLast(out found);
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				int count = list.Count;
				if (count > 0)
				{
					found = true;
					return list[count - 1];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						TSource tsource;
						do
						{
							tsource = enumerator.Current;
						}
						while (enumerator.MoveNext());
						found = true;
						return tsource;
					}
				}
			}
			found = false;
			return default(TSource);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004C2C File Offset: 0x00002E2C
		private static TSource TryGetLast<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, out bool found)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			OrderedEnumerable<TSource> orderedEnumerable = source as OrderedEnumerable<TSource>;
			if (orderedEnumerable != null)
			{
				return orderedEnumerable.TryGetLast(predicate, out found);
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					TSource tsource = list[i];
					if (predicate(tsource))
					{
						found = true;
						return tsource;
					}
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TSource tsource2 = enumerator.Current;
						if (predicate(tsource2))
						{
							while (enumerator.MoveNext())
							{
								TSource tsource3 = enumerator.Current;
								if (predicate(tsource3))
								{
									tsource2 = tsource3;
								}
							}
							found = true;
							return tsource2;
						}
					}
				}
			}
			found = false;
			return default(TSource);
		}

		/// <summary>Returns the maximum value in a sequence of <see cref="T:System.Int32" /> values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Int32" /> values to determine the maximum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000082 RID: 130 RVA: 0x00004D1C File Offset: 0x00002F1C
		public static int Max(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int num;
			using (IEnumerator<int> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = enumerator.Current;
				while (enumerator.MoveNext())
				{
					int num2 = enumerator.Current;
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the maximum <see cref="T:System.Int32" /> value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000083 RID: 131 RVA: 0x00004D88 File Offset: 0x00002F88
		public static int Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			int num;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = selector(enumerator.Current);
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					int num2 = selector(tsource);
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the maximum nullable <see cref="T:System.Int32" /> value.</summary>
		/// <returns>The value of type Nullable&lt;Int32&gt; in C# or Nullable(Of Int32) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000084 RID: 132 RVA: 0x00004E10 File Offset: 0x00003010
		public static int? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			int? num = null;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					num = selector(tsource);
					if (num != null)
					{
						int num2 = num.GetValueOrDefault();
						if (num2 >= 0)
						{
							while (enumerator.MoveNext())
							{
								TSource tsource2 = enumerator.Current;
								int? num3 = selector(tsource2);
								int valueOrDefault = num3.GetValueOrDefault();
								if (valueOrDefault > num2)
								{
									num2 = valueOrDefault;
									num = num3;
								}
							}
							return num;
						}
						while (enumerator.MoveNext())
						{
							TSource tsource3 = enumerator.Current;
							int? num4 = selector(tsource3);
							int valueOrDefault2 = num4.GetValueOrDefault();
							if ((num4 != null) & (valueOrDefault2 > num2))
							{
								num2 = valueOrDefault2;
								num = num4;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a generic sequence and returns the maximum resulting value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000085 RID: 133 RVA: 0x00004EFC File Offset: 0x000030FC
		public static TResult Max<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			Comparer<TResult> @default = Comparer<TResult>.Default;
			TResult tresult = default(TResult);
			if (tresult == null)
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TSource tsource = enumerator.Current;
						tresult = selector(tsource);
						if (tresult != null)
						{
							while (enumerator.MoveNext())
							{
								TSource tsource2 = enumerator.Current;
								TResult tresult2 = selector(tsource2);
								if (tresult2 != null && @default.Compare(tresult2, tresult) > 0)
								{
									tresult = tresult2;
								}
							}
							return tresult;
						}
					}
					return tresult;
				}
			}
			using (IEnumerator<TSource> enumerator2 = source.GetEnumerator())
			{
				if (!enumerator2.MoveNext())
				{
					throw Error.NoElements();
				}
				tresult = selector(enumerator2.Current);
				while (enumerator2.MoveNext())
				{
					TSource tsource3 = enumerator2.Current;
					TResult tresult3 = selector(tsource3);
					if (@default.Compare(tresult3, tresult) > 0)
					{
						tresult = tresult3;
					}
				}
			}
			return tresult;
		}

		/// <summary>Returns the minimum value in a sequence of <see cref="T:System.Int32" /> values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Int32" /> values to determine the minimum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000086 RID: 134 RVA: 0x00005020 File Offset: 0x00003220
		public static int Min(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int num;
			using (IEnumerator<int> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = enumerator.Current;
				while (enumerator.MoveNext())
				{
					int num2 = enumerator.Current;
					if (num2 < num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Sorts the elements of a sequence in ascending order according to a key.</summary>
		/// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted according to a key.</returns>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x06000087 RID: 135 RVA: 0x0000508C File Offset: 0x0000328C
		public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, null, false, null);
		}

		/// <summary>Sorts the elements of a sequence in ascending order by using a specified comparer.</summary>
		/// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted according to a key.</returns>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x06000088 RID: 136 RVA: 0x00005098 File Offset: 0x00003298
		public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, false, null);
		}

		/// <summary>Sorts the elements of a sequence in descending order according to a key.</summary>
		/// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted in descending order according to a key.</returns>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x06000089 RID: 137 RVA: 0x000050A4 File Offset: 0x000032A4
		public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, null, true, null);
		}

		/// <summary>Performs a subsequent ordering of the elements in a sequence in ascending order according to a key.</summary>
		/// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted according to a key.</returns>
		/// <param name="source">An <see cref="T:System.Linq.IOrderedEnumerable`1" /> that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x0600008A RID: 138 RVA: 0x000050B0 File Offset: 0x000032B0
		public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.CreateOrderedEnumerable<TKey>(keySelector, null, false);
		}

		/// <summary>Performs a subsequent ordering of the elements in a sequence in descending order, according to a key.</summary>
		/// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted in descending order according to a key.</returns>
		/// <param name="source">An <see cref="T:System.Linq.IOrderedEnumerable`1" /> that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x0600008B RID: 139 RVA: 0x000050C9 File Offset: 0x000032C9
		public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.CreateOrderedEnumerable<TKey>(keySelector, null, true);
		}

		/// <summary>Generates a sequence of integral numbers within a specified range.</summary>
		/// <returns>An IEnumerable&lt;Int32&gt; in C# or IEnumerable(Of Int32) in Visual Basic that contains a range of sequential integral numbers.</returns>
		/// <param name="start">The value of the first integer in the sequence.</param>
		/// <param name="count">The number of sequential integers to generate.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than 0.-or-<paramref name="start" /> + <paramref name="count" /> -1 is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600008C RID: 140 RVA: 0x000050E4 File Offset: 0x000032E4
		public static IEnumerable<int> Range(int start, int count)
		{
			long num = (long)start + (long)count - 1L;
			if (count < 0 || num > 2147483647L)
			{
				throw Error.ArgumentOutOfRange("count");
			}
			if (count == 0)
			{
				return EmptyPartition<int>.Instance;
			}
			return new Enumerable.RangeIterator(start, count);
		}

		/// <summary>Generates a sequence that contains one repeated value.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains a repeated value.</returns>
		/// <param name="element">The value to be repeated.</param>
		/// <param name="count">The number of times to repeat the value in the generated sequence.</param>
		/// <typeparam name="TResult">The type of the value to be repeated in the result sequence.</typeparam>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than 0.</exception>
		// Token: 0x0600008D RID: 141 RVA: 0x00005122 File Offset: 0x00003322
		public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
		{
			if (count < 0)
			{
				throw Error.ArgumentOutOfRange("count");
			}
			if (count == 0)
			{
				return EmptyPartition<TResult>.Instance;
			}
			return new Enumerable.RepeatIterator<TResult>(element, count);
		}

		/// <summary>Inverts the order of the elements in a sequence.</summary>
		/// <returns>A sequence whose elements correspond to those of the input sequence in reverse order.</returns>
		/// <param name="source">A sequence of values to reverse.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x0600008E RID: 142 RVA: 0x00005143 File Offset: 0x00003343
		public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return new Enumerable.ReverseIterator<TSource>(source);
		}

		/// <summary>Projects each element of a sequence into a new form.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the transform function on each element of <paramref name="source" />.</returns>
		/// <param name="source">A sequence of values to invoke a transform function on.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x0600008F RID: 143 RVA: 0x0000515C File Offset: 0x0000335C
		public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			Enumerable.Iterator<TSource> iterator = source as Enumerable.Iterator<TSource>;
			if (iterator != null)
			{
				return iterator.Select<TResult>(selector);
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				TSource[] array = source as TSource[];
				if (array != null)
				{
					if (array.Length != 0)
					{
						return new Enumerable.SelectArrayIterator<TSource, TResult>(array, selector);
					}
					return EmptyPartition<TResult>.Instance;
				}
				else
				{
					List<TSource> list2 = source as List<TSource>;
					if (list2 != null)
					{
						return new Enumerable.SelectListIterator<TSource, TResult>(list2, selector);
					}
					return new Enumerable.SelectIListIterator<TSource, TResult>(list, selector);
				}
			}
			else
			{
				IPartition<TSource> partition = source as IPartition<TSource>;
				if (partition == null)
				{
					return new Enumerable.SelectEnumerableIterator<TSource, TResult>(source, selector);
				}
				if (!(partition is EmptyPartition<TSource>))
				{
					return new Enumerable.SelectIPartitionIterator<TSource, TResult>(partition, selector);
				}
				return EmptyPartition<TResult>.Instance;
			}
		}

		/// <summary>Projects each element of a sequence into a new form by incorporating the element's index.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the transform function on each element of <paramref name="source" />.</returns>
		/// <param name="source">A sequence of values to invoke a transform function on.</param>
		/// <param name="selector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000090 RID: 144 RVA: 0x00005209 File Offset: 0x00003409
		public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return Enumerable.SelectIterator<TSource, TResult>(source, selector);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000522E File Offset: 0x0000342E
		private static IEnumerable<TResult> SelectIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
		{
			int index = -1;
			foreach (TSource tsource in source)
			{
				int num = index;
				index = checked(num + 1);
				yield return selector(tsource, index);
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" /> and flattens the resulting sequences into one sequence.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the sequence returned by <paramref name="selector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000092 RID: 146 RVA: 0x00005245 File Offset: 0x00003445
		public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return new Enumerable.SelectManySingleSelectorIterator<TSource, TResult>(source, selector);
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" />, and flattens the resulting sequences into one sequence. The index of each source element is used in the projected form of that element.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the one-to-many transform function on each element of an input sequence.</returns>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="selector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the sequence returned by <paramref name="selector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000093 RID: 147 RVA: 0x0000526A File Offset: 0x0000346A
		public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return Enumerable.SelectManyIterator<TSource, TResult>(source, selector);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000528F File Offset: 0x0000348F
		private static IEnumerable<TResult> SelectManyIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
		{
			int index = -1;
			foreach (TSource tsource in source)
			{
				int num = index;
				index = checked(num + 1);
				foreach (TResult tresult in selector(tsource, index))
				{
					yield return tresult;
				}
				IEnumerator<TResult> enumerator2 = null;
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Determines whether two sequences are equal by comparing the elements by using the default equality comparer for their type.</summary>
		/// <returns>true if the two source sequences are of equal length and their corresponding elements are equal according to the default equality comparer for their type; otherwise, false.</returns>
		/// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to compare to <paramref name="second" />.</param>
		/// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to compare to the first sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x06000095 RID: 149 RVA: 0x000052A6 File Offset: 0x000034A6
		public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			return first.SequenceEqual(second, null);
		}

		/// <summary>Determines whether two sequences are equal by comparing their elements by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <returns>true if the two source sequences are of equal length and their corresponding elements compare equal according to <paramref name="comparer" />; otherwise, false.</returns>
		/// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to compare to <paramref name="second" />.</param>
		/// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to compare to the first sequence.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to use to compare elements.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x06000096 RID: 150 RVA: 0x000052B0 File Offset: 0x000034B0
		public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			ICollection<TSource> collection = first as ICollection<TSource>;
			if (collection != null)
			{
				ICollection<TSource> collection2 = second as ICollection<TSource>;
				if (collection2 != null)
				{
					if (collection.Count != collection2.Count)
					{
						return false;
					}
					IList<TSource> list = collection as IList<TSource>;
					if (list != null)
					{
						IList<TSource> list2 = collection2 as IList<TSource>;
						if (list2 != null)
						{
							int count = collection.Count;
							for (int i = 0; i < count; i++)
							{
								if (!comparer.Equals(list[i], list2[i]))
								{
									return false;
								}
							}
							return true;
						}
					}
				}
			}
			bool flag;
			using (IEnumerator<TSource> enumerator = first.GetEnumerator())
			{
				using (IEnumerator<TSource> enumerator2 = second.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator2.MoveNext() || !comparer.Equals(enumerator.Current, enumerator2.Current))
						{
							return false;
						}
					}
					flag = !enumerator2.MoveNext();
				}
			}
			return flag;
		}

		/// <summary>Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.</summary>
		/// <returns>The single element of the input sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the single element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The input sequence contains more than one element.-or-The input sequence is empty.</exception>
		// Token: 0x06000097 RID: 151 RVA: 0x000053D4 File Offset: 0x000035D4
		public static TSource Single<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				int count = list.Count;
				if (count == 0)
				{
					throw Error.NoElements();
				}
				if (count == 1)
				{
					return list[0];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						throw Error.NoElements();
					}
					TSource tsource = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						return tsource;
					}
				}
			}
			throw Error.MoreThanOneElement();
		}

		/// <summary>Returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.</summary>
		/// <returns>The single element of the input sequence that satisfies a condition.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return a single element from.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-More than one element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
		// Token: 0x06000098 RID: 152 RVA: 0x00005464 File Offset: 0x00003664
		public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					if (predicate(tsource))
					{
						while (enumerator.MoveNext())
						{
							if (predicate(enumerator.Current))
							{
								throw Error.MoreThanOneMatch();
							}
						}
						return tsource;
					}
				}
			}
			throw Error.NoMatch();
		}

		/// <summary>Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.</summary>
		/// <returns>The single element of the input sequence, or default(<paramref name="TSource" />) if the sequence contains no elements.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the single element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The input sequence contains more than one element.</exception>
		// Token: 0x06000099 RID: 153 RVA: 0x000054F4 File Offset: 0x000036F4
		public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				int count = list.Count;
				if (count == 0)
				{
					TSource tsource = default(TSource);
					return tsource;
				}
				if (count == 1)
				{
					return list[0];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						TSource tsource = default(TSource);
						return tsource;
					}
					TSource tsource2 = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						return tsource2;
					}
				}
			}
			throw Error.MoreThanOneElement();
		}

		/// <summary>Returns the only element of a sequence that satisfies a specified condition or a default value if no such element exists; this method throws an exception if more than one element satisfies the condition.</summary>
		/// <returns>The single element of the input sequence that satisfies the condition, or default(<paramref name="TSource" />) if no such element is found.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return a single element from.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x0600009A RID: 154 RVA: 0x00005590 File Offset: 0x00003790
		public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					if (predicate(tsource))
					{
						while (enumerator.MoveNext())
						{
							if (predicate(enumerator.Current))
							{
								throw Error.MoreThanOneMatch();
							}
						}
						return tsource;
					}
				}
			}
			return default(TSource);
		}

		/// <summary>Bypasses a specified number of elements in a sequence and then returns the remaining elements.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements that occur after the specified index in the input sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return elements from.</param>
		/// <param name="count">The number of elements to skip before returning the remaining elements.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x0600009B RID: 155 RVA: 0x00005624 File Offset: 0x00003824
		public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (count <= 0)
			{
				if (source is Enumerable.Iterator<TSource> || source is IPartition<TSource>)
				{
					return source;
				}
				count = 0;
			}
			else
			{
				IPartition<TSource> partition = source as IPartition<TSource>;
				if (partition != null)
				{
					return partition.Skip(count);
				}
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				return new Enumerable.ListPartition<TSource>(list, count, int.MaxValue);
			}
			return new Enumerable.EnumerablePartition<TSource>(source, count, -1);
		}

		/// <summary>Computes the sum of a sequence of <see cref="T:System.Int32" /> values.</summary>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Int32" /> values to calculate the sum of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600009C RID: 156 RVA: 0x0000568C File Offset: 0x0000388C
		public static int Sum(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int num = 0;
			checked
			{
				foreach (int num2 in source)
				{
					num += num2;
				}
				return num;
			}
		}

		/// <summary>Computes the sum of the sequence of <see cref="T:System.Int32" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values.</returns>
		/// <param name="source">A sequence of values that are used to calculate a sum.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600009D RID: 157 RVA: 0x000056E4 File Offset: 0x000038E4
		public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			int num = 0;
			checked
			{
				foreach (TSource tsource in source)
				{
					num += selector(tsource);
				}
				return num;
			}
		}

		/// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Int32" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values.</returns>
		/// <param name="source">A sequence of values that are used to calculate a sum.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600009E RID: 158 RVA: 0x00005750 File Offset: 0x00003950
		public static int? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			int num = 0;
			checked
			{
				foreach (TSource tsource in source)
				{
					int? num2 = selector(tsource);
					if (num2 != null)
					{
						num += num2.GetValueOrDefault();
					}
				}
				return new int?(num);
			}
		}

		/// <summary>Computes the sum of the sequence of <see cref="T:System.Int64" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values.</returns>
		/// <param name="source">A sequence of values that are used to calculate a sum.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x0600009F RID: 159 RVA: 0x000057D0 File Offset: 0x000039D0
		public static long Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
		{
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num = 0L;
			checked
			{
				foreach (TSource tsource in source)
				{
					num += selector(tsource);
				}
				return num;
			}
		}

		/// <summary>Returns a specified number of contiguous elements from the start of a sequence.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the specified number of elements from the start of the input sequence.</returns>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="count">The number of elements to return.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060000A0 RID: 160 RVA: 0x0000583C File Offset: 0x00003A3C
		public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (count <= 0)
			{
				return EmptyPartition<TSource>.Instance;
			}
			IPartition<TSource> partition = source as IPartition<TSource>;
			if (partition != null)
			{
				return partition.Take(count);
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				return new Enumerable.ListPartition<TSource>(list, 0, count - 1);
			}
			return new Enumerable.EnumerablePartition<TSource>(source, 0, count - 1);
		}

		/// <summary>Creates an array from a <see cref="T:System.Collections.Generic.IEnumerable`1" />.</summary>
		/// <returns>An array that contains the elements from the input sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create an array from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060000A1 RID: 161 RVA: 0x00005894 File Offset: 0x00003A94
		public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IIListProvider<TSource> iilistProvider = source as IIListProvider<TSource>;
			if (iilistProvider == null)
			{
				return global::System.Collections.Generic.EnumerableHelpers.ToArray<TSource>(source);
			}
			return iilistProvider.ToArray();
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.List`1" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.List`1" /> that contains elements from the input sequence.</returns>
		/// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.List`1" /> from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060000A2 RID: 162 RVA: 0x000058C8 File Offset: 0x00003AC8
		public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IIListProvider<TSource> iilistProvider = source as IIListProvider<TSource>;
			if (iilistProvider == null)
			{
				return new List<TSource>(source);
			}
			return iilistProvider.ToList();
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains keys and values.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.-or-<paramref name="keySelector" /> produces a key that is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keySelector" /> produces duplicate keys for two elements.</exception>
		// Token: 0x060000A3 RID: 163 RVA: 0x000058FA File Offset: 0x00003AFA
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToDictionary(keySelector, null);
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function and key comparer.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains keys and values.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.-or-<paramref name="keySelector" /> produces a key that is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keySelector" /> produces duplicate keys for two elements.</exception>
		// Token: 0x060000A4 RID: 164 RVA: 0x00005904 File Offset: 0x00003B04
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			int num = 0;
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection != null)
			{
				num = collection.Count;
				if (num == 0)
				{
					return new Dictionary<TKey, TSource>(comparer);
				}
				TSource[] array = collection as TSource[];
				if (array != null)
				{
					return Enumerable.ToDictionary<TSource, TKey>(array, keySelector, comparer);
				}
				List<TSource> list = collection as List<TSource>;
				if (list != null)
				{
					return Enumerable.ToDictionary<TSource, TKey>(list, keySelector, comparer);
				}
			}
			Dictionary<TKey, TSource> dictionary = new Dictionary<TKey, TSource>(num, comparer);
			foreach (TSource tsource in source)
			{
				dictionary.Add(keySelector(tsource), tsource);
			}
			return dictionary;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000059C8 File Offset: 0x00003BC8
		private static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(TSource[] source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, TSource> dictionary = new Dictionary<TKey, TSource>(source.Length, comparer);
			for (int i = 0; i < source.Length; i++)
			{
				dictionary.Add(keySelector(source[i]), source[i]);
			}
			return dictionary;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005A08 File Offset: 0x00003C08
		private static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(List<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, TSource> dictionary = new Dictionary<TKey, TSource>(source.Count, comparer);
			foreach (TSource tsource in source)
			{
				dictionary.Add(keySelector(tsource), tsource);
			}
			return dictionary;
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to specified key selector and element selector functions.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is null.-or-<paramref name="keySelector" /> produces a key that is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keySelector" /> produces duplicate keys for two elements.</exception>
		// Token: 0x060000A7 RID: 167 RVA: 0x00005A6C File Offset: 0x00003C6C
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.ToDictionary(keySelector, elementSelector, null);
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function, a comparer, and an element selector function.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is null.-or-<paramref name="keySelector" /> produces a key that is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keySelector" /> produces duplicate keys for two elements.</exception>
		// Token: 0x060000A8 RID: 168 RVA: 0x00005A78 File Offset: 0x00003C78
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			int num = 0;
			ICollection<TSource> collection = source as ICollection<TSource>;
			if (collection != null)
			{
				num = collection.Count;
				if (num == 0)
				{
					return new Dictionary<TKey, TElement>(comparer);
				}
				TSource[] array = collection as TSource[];
				if (array != null)
				{
					return Enumerable.ToDictionary<TSource, TKey, TElement>(array, keySelector, elementSelector, comparer);
				}
				List<TSource> list = collection as List<TSource>;
				if (list != null)
				{
					return Enumerable.ToDictionary<TSource, TKey, TElement>(list, keySelector, elementSelector, comparer);
				}
			}
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(num, comparer);
			foreach (TSource tsource in source)
			{
				dictionary.Add(keySelector(tsource), elementSelector(tsource));
			}
			return dictionary;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005B50 File Offset: 0x00003D50
		private static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(TSource[] source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(source.Length, comparer);
			for (int i = 0; i < source.Length; i++)
			{
				dictionary.Add(keySelector(source[i]), elementSelector(source[i]));
			}
			return dictionary;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005B98 File Offset: 0x00003D98
		private static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(List<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(source.Count, comparer);
			foreach (TSource tsource in source)
			{
				dictionary.Add(keySelector(tsource), elementSelector(tsource));
			}
			return dictionary;
		}

		/// <summary>Produces the set union of two sequences by using the default equality comparer.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements from both input sequences, excluding duplicates.</returns>
		/// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements form the first set for the union.</param>
		/// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements form the second set for the union.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x060000AB RID: 171 RVA: 0x00005C04 File Offset: 0x00003E04
		public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			return first.Union(second, null);
		}

		/// <summary>Produces the set union of two sequences by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements from both input sequences, excluding duplicates.</returns>
		/// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements form the first set for the union.</param>
		/// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements form the second set for the union.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x060000AC RID: 172 RVA: 0x00005C10 File Offset: 0x00003E10
		public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			Enumerable.UnionIterator<TSource> unionIterator = first as Enumerable.UnionIterator<TSource>;
			if (unionIterator == null || !Utilities.AreEqualityComparersEqual<TSource>(comparer, unionIterator._comparer))
			{
				return new Enumerable.UnionIterator2<TSource>(first, second, comparer);
			}
			return unionIterator.Union(second);
		}

		/// <summary>Filters a sequence of values based on a predicate.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements from the input sequence that satisfy the condition.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to filter.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x060000AD RID: 173 RVA: 0x00005C64 File Offset: 0x00003E64
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			Enumerable.Iterator<TSource> iterator = source as Enumerable.Iterator<TSource>;
			if (iterator != null)
			{
				return iterator.Where(predicate);
			}
			TSource[] array = source as TSource[];
			if (array != null)
			{
				if (array.Length != 0)
				{
					return new Enumerable.WhereArrayIterator<TSource>(array, predicate);
				}
				return EmptyPartition<TSource>.Instance;
			}
			else
			{
				List<TSource> list = source as List<TSource>;
				if (list != null)
				{
					return new Enumerable.WhereListIterator<TSource>(list, predicate);
				}
				return new Enumerable.WhereEnumerableIterator<TSource>(source, predicate);
			}
		}

		/// <summary>Filters a sequence of values based on a predicate. Each element's index is used in the logic of the predicate function.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements from the input sequence that satisfy the condition.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to filter.</param>
		/// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x060000AE RID: 174 RVA: 0x00005CD8 File Offset: 0x00003ED8
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return Enumerable.WhereIterator<TSource>(source, predicate);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005CFD File Offset: 0x00003EFD
		private static IEnumerable<TSource> WhereIterator<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			int index = -1;
			foreach (TSource tsource in source)
			{
				int num = index;
				index = checked(num + 1);
				if (predicate(tsource, index))
				{
					yield return tsource;
				}
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0200001A RID: 26
		private sealed class Concat2Iterator<TSource> : Enumerable.ConcatIterator<TSource>
		{
			// Token: 0x060000B0 RID: 176 RVA: 0x00005D14 File Offset: 0x00003F14
			internal Concat2Iterator(IEnumerable<TSource> first, IEnumerable<TSource> second)
			{
				this._first = first;
				this._second = second;
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x00005D2A File Offset: 0x00003F2A
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.Concat2Iterator<TSource>(this._first, this._second);
			}

			// Token: 0x060000B2 RID: 178 RVA: 0x00005D40 File Offset: 0x00003F40
			internal override Enumerable.ConcatIterator<TSource> Concat(IEnumerable<TSource> next)
			{
				bool flag = next is ICollection<TSource> && this._first is ICollection<TSource> && this._second is ICollection<TSource>;
				return new Enumerable.ConcatNIterator<TSource>(this, next, 2, flag);
			}

			// Token: 0x060000B3 RID: 179 RVA: 0x00005D80 File Offset: 0x00003F80
			public override int GetCount(bool onlyIfCheap)
			{
				int num;
				if (!global::System.Collections.Generic.EnumerableHelpers.TryGetCount<TSource>(this._first, out num))
				{
					if (onlyIfCheap)
					{
						return -1;
					}
					num = this._first.Count<TSource>();
				}
				int num2;
				if (!global::System.Collections.Generic.EnumerableHelpers.TryGetCount<TSource>(this._second, out num2))
				{
					if (onlyIfCheap)
					{
						return -1;
					}
					num2 = this._second.Count<TSource>();
				}
				return checked(num + num2);
			}

			// Token: 0x060000B4 RID: 180 RVA: 0x00005DD0 File Offset: 0x00003FD0
			internal override IEnumerable<TSource> GetEnumerable(int index)
			{
				if (index == 0)
				{
					return this._first;
				}
				if (index != 1)
				{
					return null;
				}
				return this._second;
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x00005DEC File Offset: 0x00003FEC
			public override TSource[] ToArray()
			{
				SparseArrayBuilder<TSource> sparseArrayBuilder = new SparseArrayBuilder<TSource>(true);
				bool flag = sparseArrayBuilder.ReserveOrAdd(this._first);
				bool flag2 = sparseArrayBuilder.ReserveOrAdd(this._second);
				TSource[] array = sparseArrayBuilder.ToArray();
				if (flag)
				{
					Marker marker = sparseArrayBuilder.Markers.First();
					global::System.Collections.Generic.EnumerableHelpers.Copy<TSource>(this._first, array, 0, marker.Count);
				}
				if (flag2)
				{
					Marker marker2 = sparseArrayBuilder.Markers.Last();
					global::System.Collections.Generic.EnumerableHelpers.Copy<TSource>(this._second, array, marker2.Index, marker2.Count);
				}
				return array;
			}

			// Token: 0x04000014 RID: 20
			internal readonly IEnumerable<TSource> _first;

			// Token: 0x04000015 RID: 21
			internal readonly IEnumerable<TSource> _second;
		}

		// Token: 0x0200001B RID: 27
		private sealed class ConcatNIterator<TSource> : Enumerable.ConcatIterator<TSource>
		{
			// Token: 0x060000B6 RID: 182 RVA: 0x00005E7B File Offset: 0x0000407B
			internal ConcatNIterator(Enumerable.ConcatIterator<TSource> tail, IEnumerable<TSource> head, int headIndex, bool hasOnlyCollections)
			{
				this._tail = tail;
				this._head = head;
				this._headIndex = headIndex;
				this._hasOnlyCollections = hasOnlyCollections;
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005EA0 File Offset: 0x000040A0
			private Enumerable.ConcatNIterator<TSource> PreviousN
			{
				get
				{
					return this._tail as Enumerable.ConcatNIterator<TSource>;
				}
			}

			// Token: 0x060000B8 RID: 184 RVA: 0x00005EAD File Offset: 0x000040AD
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.ConcatNIterator<TSource>(this._tail, this._head, this._headIndex, this._hasOnlyCollections);
			}

			// Token: 0x060000B9 RID: 185 RVA: 0x00005ECC File Offset: 0x000040CC
			internal override Enumerable.ConcatIterator<TSource> Concat(IEnumerable<TSource> next)
			{
				if (this._headIndex == 2147483645)
				{
					return new Enumerable.Concat2Iterator<TSource>(this, next);
				}
				bool flag = this._hasOnlyCollections && next is ICollection<TSource>;
				return new Enumerable.ConcatNIterator<TSource>(this, next, this._headIndex + 1, flag);
			}

			// Token: 0x060000BA RID: 186 RVA: 0x00005F14 File Offset: 0x00004114
			public override int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap && !this._hasOnlyCollections)
				{
					return -1;
				}
				int num = 0;
				Enumerable.ConcatNIterator<TSource> concatNIterator = this;
				checked
				{
					Enumerable.ConcatNIterator<TSource> concatNIterator2;
					do
					{
						concatNIterator2 = concatNIterator;
						IEnumerable<TSource> head = concatNIterator2._head;
						ICollection<TSource> collection = head as ICollection<TSource>;
						int num2 = ((collection != null) ? collection.Count : head.Count<TSource>());
						num += num2;
					}
					while ((concatNIterator = concatNIterator2.PreviousN) != null);
					return num + concatNIterator2._tail.GetCount(onlyIfCheap);
				}
			}

			// Token: 0x060000BB RID: 187 RVA: 0x00005F74 File Offset: 0x00004174
			internal override IEnumerable<TSource> GetEnumerable(int index)
			{
				if (index > this._headIndex)
				{
					return null;
				}
				Enumerable.ConcatNIterator<TSource> concatNIterator = this;
				Enumerable.ConcatNIterator<TSource> concatNIterator2;
				for (;;)
				{
					concatNIterator2 = concatNIterator;
					if (index == concatNIterator2._headIndex)
					{
						break;
					}
					if ((concatNIterator = concatNIterator2.PreviousN) == null)
					{
						goto Block_3;
					}
				}
				return concatNIterator2._head;
				Block_3:
				return concatNIterator2._tail.GetEnumerable(index);
			}

			// Token: 0x060000BC RID: 188 RVA: 0x00005FB6 File Offset: 0x000041B6
			public override TSource[] ToArray()
			{
				if (!this._hasOnlyCollections)
				{
					return this.LazyToArray();
				}
				return this.PreallocatingToArray();
			}

			// Token: 0x060000BD RID: 189 RVA: 0x00005FD0 File Offset: 0x000041D0
			private TSource[] LazyToArray()
			{
				SparseArrayBuilder<TSource> sparseArrayBuilder = new SparseArrayBuilder<TSource>(true);
				global::System.Collections.Generic.ArrayBuilder<int> arrayBuilder = default(global::System.Collections.Generic.ArrayBuilder<int>);
				int num = 0;
				for (;;)
				{
					IEnumerable<TSource> enumerable = this.GetEnumerable(num);
					if (enumerable == null)
					{
						break;
					}
					if (sparseArrayBuilder.ReserveOrAdd(enumerable))
					{
						arrayBuilder.Add(num);
					}
					num++;
				}
				TSource[] array = sparseArrayBuilder.ToArray();
				global::System.Collections.Generic.ArrayBuilder<Marker> markers = sparseArrayBuilder.Markers;
				for (int i = 0; i < markers.Count; i++)
				{
					Marker marker = markers[i];
					global::System.Collections.Generic.EnumerableHelpers.Copy<TSource>(this.GetEnumerable(arrayBuilder[i]), array, marker.Index, marker.Count);
				}
				return array;
			}

			// Token: 0x060000BE RID: 190 RVA: 0x00006070 File Offset: 0x00004270
			private TSource[] PreallocatingToArray()
			{
				int count = this.GetCount(true);
				if (count == 0)
				{
					return Array.Empty<TSource>();
				}
				TSource[] array = new TSource[count];
				int num = array.Length;
				Enumerable.ConcatNIterator<TSource> concatNIterator = this;
				checked
				{
					Enumerable.ConcatNIterator<TSource> concatNIterator2;
					do
					{
						concatNIterator2 = concatNIterator;
						ICollection<TSource> collection = (ICollection<TSource>)concatNIterator2._head;
						int count2 = collection.Count;
						if (count2 > 0)
						{
							num -= count2;
							collection.CopyTo(array, num);
						}
					}
					while ((concatNIterator = concatNIterator2.PreviousN) != null);
					Enumerable.Concat2Iterator<TSource> concat2Iterator = (Enumerable.Concat2Iterator<TSource>)concatNIterator2._tail;
					ICollection<TSource> collection2 = (ICollection<TSource>)concat2Iterator._second;
					int count3 = collection2.Count;
					if (count3 > 0)
					{
						collection2.CopyTo(array, num - count3);
					}
					if (num > count3)
					{
						((ICollection<TSource>)concat2Iterator._first).CopyTo(array, 0);
					}
					return array;
				}
			}

			// Token: 0x04000016 RID: 22
			private readonly Enumerable.ConcatIterator<TSource> _tail;

			// Token: 0x04000017 RID: 23
			private readonly IEnumerable<TSource> _head;

			// Token: 0x04000018 RID: 24
			private readonly int _headIndex;

			// Token: 0x04000019 RID: 25
			private readonly bool _hasOnlyCollections;
		}

		// Token: 0x0200001C RID: 28
		private abstract class ConcatIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x060000BF RID: 191 RVA: 0x00006121 File Offset: 0x00004321
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x060000C0 RID: 192
			internal abstract IEnumerable<TSource> GetEnumerable(int index);

			// Token: 0x060000C1 RID: 193
			internal abstract Enumerable.ConcatIterator<TSource> Concat(IEnumerable<TSource> next);

			// Token: 0x060000C2 RID: 194 RVA: 0x00006144 File Offset: 0x00004344
			public override bool MoveNext()
			{
				if (this._state == 1)
				{
					this._enumerator = this.GetEnumerable(0).GetEnumerator();
					this._state = 2;
				}
				if (this._state > 1)
				{
					while (!this._enumerator.MoveNext())
					{
						int state = this._state;
						this._state = state + 1;
						IEnumerable<TSource> enumerable = this.GetEnumerable(state - 1);
						if (enumerable == null)
						{
							this.Dispose();
							return false;
						}
						this._enumerator.Dispose();
						this._enumerator = enumerable.GetEnumerator();
					}
					this._current = this._enumerator.Current;
					return true;
				}
				return false;
			}

			// Token: 0x060000C3 RID: 195
			public abstract int GetCount(bool onlyIfCheap);

			// Token: 0x060000C4 RID: 196
			public abstract TSource[] ToArray();

			// Token: 0x060000C5 RID: 197 RVA: 0x000061DC File Offset: 0x000043DC
			public List<TSource> ToList()
			{
				int count = this.GetCount(true);
				List<TSource> list = ((count != -1) ? new List<TSource>(count) : new List<TSource>());
				int num = 0;
				for (;;)
				{
					IEnumerable<TSource> enumerable = this.GetEnumerable(num);
					if (enumerable == null)
					{
						break;
					}
					list.AddRange(enumerable);
					num++;
				}
				return list;
			}

			// Token: 0x0400001A RID: 26
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x0200001D RID: 29
		private sealed class DistinctIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x060000C7 RID: 199 RVA: 0x00006226 File Offset: 0x00004426
			public DistinctIterator(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
			{
				this._source = source;
				this._comparer = comparer;
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x0000623C File Offset: 0x0000443C
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.DistinctIterator<TSource>(this._source, this._comparer);
			}

			// Token: 0x060000C9 RID: 201 RVA: 0x00006250 File Offset: 0x00004450
			public override bool MoveNext()
			{
				int state = this._state;
				TSource tsource;
				if (state != 1)
				{
					if (state == 2)
					{
						while (this._enumerator.MoveNext())
						{
							tsource = this._enumerator.Current;
							if (this._set.Add(tsource))
							{
								this._current = tsource;
								return true;
							}
						}
					}
					this.Dispose();
					return false;
				}
				this._enumerator = this._source.GetEnumerator();
				if (!this._enumerator.MoveNext())
				{
					this.Dispose();
					return false;
				}
				tsource = this._enumerator.Current;
				this._set = new Set<TSource>(this._comparer);
				this._set.Add(tsource);
				this._current = tsource;
				this._state = 2;
				return true;
			}

			// Token: 0x060000CA RID: 202 RVA: 0x0000630B File Offset: 0x0000450B
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
					this._set = null;
				}
				base.Dispose();
			}

			// Token: 0x060000CB RID: 203 RVA: 0x00006334 File Offset: 0x00004534
			private Set<TSource> FillSet()
			{
				Set<TSource> set = new Set<TSource>(this._comparer);
				set.UnionWith(this._source);
				return set;
			}

			// Token: 0x060000CC RID: 204 RVA: 0x0000634D File Offset: 0x0000454D
			public TSource[] ToArray()
			{
				return this.FillSet().ToArray();
			}

			// Token: 0x060000CD RID: 205 RVA: 0x0000635A File Offset: 0x0000455A
			public List<TSource> ToList()
			{
				return this.FillSet().ToList();
			}

			// Token: 0x060000CE RID: 206 RVA: 0x00006367 File Offset: 0x00004567
			public int GetCount(bool onlyIfCheap)
			{
				if (!onlyIfCheap)
				{
					return this.FillSet().Count;
				}
				return -1;
			}

			// Token: 0x0400001B RID: 27
			private readonly IEnumerable<TSource> _source;

			// Token: 0x0400001C RID: 28
			private readonly IEqualityComparer<TSource> _comparer;

			// Token: 0x0400001D RID: 29
			private Set<TSource> _set;

			// Token: 0x0400001E RID: 30
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x0200001E RID: 30
		internal abstract class Iterator<TSource> : IEnumerable<TSource>, IEnumerable, IEnumerator<TSource>, IDisposable, IEnumerator
		{
			// Token: 0x060000CF RID: 207 RVA: 0x00006379 File Offset: 0x00004579
			protected Iterator()
			{
				this._threadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000638C File Offset: 0x0000458C
			public TSource Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x060000D1 RID: 209
			public abstract Enumerable.Iterator<TSource> Clone();

			// Token: 0x060000D2 RID: 210 RVA: 0x00006394 File Offset: 0x00004594
			public virtual void Dispose()
			{
				this._current = default(TSource);
				this._state = -1;
			}

			// Token: 0x060000D3 RID: 211 RVA: 0x000063A9 File Offset: 0x000045A9
			public IEnumerator<TSource> GetEnumerator()
			{
				Enumerable.Iterator iterator = ((this._state == 0 && this._threadId == Environment.CurrentManagedThreadId) ? this : this.Clone());
				iterator._state = 1;
				return iterator;
			}

			// Token: 0x060000D4 RID: 212
			public abstract bool MoveNext();

			// Token: 0x060000D5 RID: 213 RVA: 0x000063D0 File Offset: 0x000045D0
			public virtual IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.SelectEnumerableIterator<TSource, TResult>(this, selector);
			}

			// Token: 0x060000D6 RID: 214 RVA: 0x000063D9 File Offset: 0x000045D9
			public virtual IEnumerable<TSource> Where(Func<TSource, bool> predicate)
			{
				return new Enumerable.WhereEnumerableIterator<TSource>(this, predicate);
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x060000D7 RID: 215 RVA: 0x000063E2 File Offset: 0x000045E2
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060000D8 RID: 216 RVA: 0x000063EF File Offset: 0x000045EF
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060000D9 RID: 217 RVA: 0x000063F7 File Offset: 0x000045F7
			void IEnumerator.Reset()
			{
				throw Error.NotSupported();
			}

			// Token: 0x0400001F RID: 31
			private readonly int _threadId;

			// Token: 0x04000020 RID: 32
			internal int _state;

			// Token: 0x04000021 RID: 33
			internal TSource _current;
		}

		// Token: 0x0200001F RID: 31
		private sealed class ListPartition<TSource> : Enumerable.Iterator<TSource>, IPartition<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x060000DA RID: 218 RVA: 0x000063FE File Offset: 0x000045FE
			public ListPartition(IList<TSource> source, int minIndexInclusive, int maxIndexInclusive)
			{
				this._source = source;
				this._minIndexInclusive = minIndexInclusive;
				this._maxIndexInclusive = maxIndexInclusive;
			}

			// Token: 0x060000DB RID: 219 RVA: 0x0000641B File Offset: 0x0000461B
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.ListPartition<TSource>(this._source, this._minIndexInclusive, this._maxIndexInclusive);
			}

			// Token: 0x060000DC RID: 220 RVA: 0x00006434 File Offset: 0x00004634
			public override bool MoveNext()
			{
				int num = this._state - 1;
				if (num <= this._maxIndexInclusive - this._minIndexInclusive && num < this._source.Count - this._minIndexInclusive)
				{
					this._current = this._source[this._minIndexInclusive + num];
					this._state++;
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x060000DD RID: 221 RVA: 0x0000649F File Offset: 0x0000469F
			public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, selector, this._minIndexInclusive, this._maxIndexInclusive);
			}

			// Token: 0x060000DE RID: 222 RVA: 0x000064BC File Offset: 0x000046BC
			public IPartition<TSource> Skip(int count)
			{
				int num = this._minIndexInclusive + count;
				if (num <= this._maxIndexInclusive)
				{
					return new Enumerable.ListPartition<TSource>(this._source, num, this._maxIndexInclusive);
				}
				return EmptyPartition<TSource>.Instance;
			}

			// Token: 0x060000DF RID: 223 RVA: 0x000064F8 File Offset: 0x000046F8
			public IPartition<TSource> Take(int count)
			{
				int num = this._minIndexInclusive + count - 1;
				if (num < this._maxIndexInclusive)
				{
					return new Enumerable.ListPartition<TSource>(this._source, this._minIndexInclusive, num);
				}
				return this;
			}

			// Token: 0x060000E0 RID: 224 RVA: 0x00006530 File Offset: 0x00004730
			public TSource TryGetElementAt(int index, out bool found)
			{
				if (index <= this._maxIndexInclusive - this._minIndexInclusive && index < this._source.Count - this._minIndexInclusive)
				{
					found = true;
					return this._source[this._minIndexInclusive + index];
				}
				found = false;
				return default(TSource);
			}

			// Token: 0x060000E1 RID: 225 RVA: 0x00006588 File Offset: 0x00004788
			public TSource TryGetFirst(out bool found)
			{
				if (this._source.Count > this._minIndexInclusive)
				{
					found = true;
					return this._source[this._minIndexInclusive];
				}
				found = false;
				return default(TSource);
			}

			// Token: 0x060000E2 RID: 226 RVA: 0x000065CC File Offset: 0x000047CC
			public TSource TryGetLast(out bool found)
			{
				int num = this._source.Count - 1;
				if (num >= this._minIndexInclusive)
				{
					found = true;
					return this._source[Math.Min(num, this._maxIndexInclusive)];
				}
				found = false;
				return default(TSource);
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x060000E3 RID: 227 RVA: 0x00006618 File Offset: 0x00004818
			private int Count
			{
				get
				{
					int count = this._source.Count;
					if (count <= this._minIndexInclusive)
					{
						return 0;
					}
					return Math.Min(count - 1, this._maxIndexInclusive) - this._minIndexInclusive + 1;
				}
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x00006654 File Offset: 0x00004854
			public TSource[] ToArray()
			{
				int count = this.Count;
				if (count == 0)
				{
					return Array.Empty<TSource>();
				}
				TSource[] array = new TSource[count];
				int num = 0;
				int num2 = this._minIndexInclusive;
				while (num != array.Length)
				{
					array[num] = this._source[num2];
					num++;
					num2++;
				}
				return array;
			}

			// Token: 0x060000E5 RID: 229 RVA: 0x000066A8 File Offset: 0x000048A8
			public List<TSource> ToList()
			{
				int count = this.Count;
				if (count == 0)
				{
					return new List<TSource>();
				}
				List<TSource> list = new List<TSource>(count);
				int num = this._minIndexInclusive + count;
				for (int num2 = this._minIndexInclusive; num2 != num; num2++)
				{
					list.Add(this._source[num2]);
				}
				return list;
			}

			// Token: 0x060000E6 RID: 230 RVA: 0x000066F9 File Offset: 0x000048F9
			public int GetCount(bool onlyIfCheap)
			{
				return this.Count;
			}

			// Token: 0x04000022 RID: 34
			private readonly IList<TSource> _source;

			// Token: 0x04000023 RID: 35
			private readonly int _minIndexInclusive;

			// Token: 0x04000024 RID: 36
			private readonly int _maxIndexInclusive;
		}

		// Token: 0x02000020 RID: 32
		private sealed class EnumerablePartition<TSource> : Enumerable.Iterator<TSource>, IPartition<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x060000E7 RID: 231 RVA: 0x00006701 File Offset: 0x00004901
			internal EnumerablePartition(IEnumerable<TSource> source, int minIndexInclusive, int maxIndexInclusive)
			{
				this._source = source;
				this._minIndexInclusive = minIndexInclusive;
				this._maxIndexInclusive = maxIndexInclusive;
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000671E File Offset: 0x0000491E
			private bool HasLimit
			{
				get
				{
					return this._maxIndexInclusive != -1;
				}
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000672C File Offset: 0x0000492C
			private int Limit
			{
				get
				{
					return this._maxIndexInclusive + 1 - this._minIndexInclusive;
				}
			}

			// Token: 0x060000EA RID: 234 RVA: 0x0000673D File Offset: 0x0000493D
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.EnumerablePartition<TSource>(this._source, this._minIndexInclusive, this._maxIndexInclusive);
			}

			// Token: 0x060000EB RID: 235 RVA: 0x00006756 File Offset: 0x00004956
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x060000EC RID: 236 RVA: 0x00006778 File Offset: 0x00004978
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				if (!this.HasLimit)
				{
					return Math.Max(this._source.Count<TSource>() - this._minIndexInclusive, 0);
				}
				int num;
				using (IEnumerator<TSource> enumerator = this._source.GetEnumerator())
				{
					num = Math.Max((int)(Enumerable.EnumerablePartition<TSource>.SkipAndCount((uint)(this._maxIndexInclusive + 1), enumerator) - (uint)this._minIndexInclusive), 0);
				}
				return num;
			}

			// Token: 0x060000ED RID: 237 RVA: 0x000067F0 File Offset: 0x000049F0
			public override bool MoveNext()
			{
				int num = this._state - 3;
				if (num < -2)
				{
					this.Dispose();
					return false;
				}
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						goto IL_0054;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				if (!this.SkipBeforeFirst(this._enumerator))
				{
					goto IL_009B;
				}
				this._state = 3;
				IL_0054:
				if ((!this.HasLimit || num < this.Limit) && this._enumerator.MoveNext())
				{
					if (this.HasLimit)
					{
						this._state++;
					}
					this._current = this._enumerator.Current;
					return true;
				}
				IL_009B:
				this.Dispose();
				return false;
			}

			// Token: 0x060000EE RID: 238 RVA: 0x0000689F File Offset: 0x00004A9F
			public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.SelectIPartitionIterator<TSource, TResult>(this, selector);
			}

			// Token: 0x060000EF RID: 239 RVA: 0x000068A8 File Offset: 0x00004AA8
			public IPartition<TSource> Skip(int count)
			{
				int num = this._minIndexInclusive + count;
				if (!this.HasLimit)
				{
					if (num < 0)
					{
						return new Enumerable.EnumerablePartition<TSource>(this, count, -1);
					}
				}
				else if (num > this._maxIndexInclusive)
				{
					return EmptyPartition<TSource>.Instance;
				}
				return new Enumerable.EnumerablePartition<TSource>(this._source, num, this._maxIndexInclusive);
			}

			// Token: 0x060000F0 RID: 240 RVA: 0x000068F4 File Offset: 0x00004AF4
			public IPartition<TSource> Take(int count)
			{
				int num = this._minIndexInclusive + count - 1;
				if (!this.HasLimit)
				{
					if (num < 0)
					{
						return new Enumerable.EnumerablePartition<TSource>(this, 0, count - 1);
					}
				}
				else if (num >= this._maxIndexInclusive)
				{
					return this;
				}
				return new Enumerable.EnumerablePartition<TSource>(this._source, this._minIndexInclusive, num);
			}

			// Token: 0x060000F1 RID: 241 RVA: 0x00006940 File Offset: 0x00004B40
			public TSource TryGetElementAt(int index, out bool found)
			{
				if (index >= 0 && (!this.HasLimit || index < this.Limit))
				{
					using (IEnumerator<TSource> enumerator = this._source.GetEnumerator())
					{
						if (Enumerable.EnumerablePartition<TSource>.SkipBefore(this._minIndexInclusive + index, enumerator) && enumerator.MoveNext())
						{
							found = true;
							return enumerator.Current;
						}
					}
				}
				found = false;
				return default(TSource);
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x000069BC File Offset: 0x00004BBC
			public TSource TryGetFirst(out bool found)
			{
				using (IEnumerator<TSource> enumerator = this._source.GetEnumerator())
				{
					if (this.SkipBeforeFirst(enumerator) && enumerator.MoveNext())
					{
						found = true;
						return enumerator.Current;
					}
				}
				found = false;
				return default(TSource);
			}

			// Token: 0x060000F3 RID: 243 RVA: 0x00006A1C File Offset: 0x00004C1C
			public TSource TryGetLast(out bool found)
			{
				using (IEnumerator<TSource> enumerator = this._source.GetEnumerator())
				{
					if (this.SkipBeforeFirst(enumerator) && enumerator.MoveNext())
					{
						int num = this.Limit - 1;
						int num2 = (this.HasLimit ? 0 : int.MinValue);
						TSource tsource;
						do
						{
							num--;
							tsource = enumerator.Current;
						}
						while (num >= num2 && enumerator.MoveNext());
						found = true;
						return tsource;
					}
				}
				found = false;
				return default(TSource);
			}

			// Token: 0x060000F4 RID: 244 RVA: 0x00006AAC File Offset: 0x00004CAC
			public TSource[] ToArray()
			{
				using (IEnumerator<TSource> enumerator = this._source.GetEnumerator())
				{
					if (this.SkipBeforeFirst(enumerator) && enumerator.MoveNext())
					{
						int num = this.Limit - 1;
						int num2 = (this.HasLimit ? 0 : int.MinValue);
						int num3 = (this.HasLimit ? this.Limit : int.MaxValue);
						global::System.Collections.Generic.LargeArrayBuilder<TSource> largeArrayBuilder = new global::System.Collections.Generic.LargeArrayBuilder<TSource>(num3);
						do
						{
							num--;
							largeArrayBuilder.Add(enumerator.Current);
						}
						while (num >= num2 && enumerator.MoveNext());
						return largeArrayBuilder.ToArray();
					}
				}
				return Array.Empty<TSource>();
			}

			// Token: 0x060000F5 RID: 245 RVA: 0x00006B5C File Offset: 0x00004D5C
			public List<TSource> ToList()
			{
				List<TSource> list = new List<TSource>();
				using (IEnumerator<TSource> enumerator = this._source.GetEnumerator())
				{
					if (this.SkipBeforeFirst(enumerator) && enumerator.MoveNext())
					{
						int num = this.Limit - 1;
						int num2 = (this.HasLimit ? 0 : int.MinValue);
						do
						{
							num--;
							list.Add(enumerator.Current);
						}
						while (num >= num2 && enumerator.MoveNext());
					}
				}
				return list;
			}

			// Token: 0x060000F6 RID: 246 RVA: 0x00006BE0 File Offset: 0x00004DE0
			private bool SkipBeforeFirst(IEnumerator<TSource> en)
			{
				return Enumerable.EnumerablePartition<TSource>.SkipBefore(this._minIndexInclusive, en);
			}

			// Token: 0x060000F7 RID: 247 RVA: 0x00006BEE File Offset: 0x00004DEE
			private static bool SkipBefore(int index, IEnumerator<TSource> en)
			{
				return Enumerable.EnumerablePartition<TSource>.SkipAndCount(index, en) == index;
			}

			// Token: 0x060000F8 RID: 248 RVA: 0x00006BFA File Offset: 0x00004DFA
			private static int SkipAndCount(int index, IEnumerator<TSource> en)
			{
				return (int)Enumerable.EnumerablePartition<TSource>.SkipAndCount((uint)index, en);
			}

			// Token: 0x060000F9 RID: 249 RVA: 0x00006C04 File Offset: 0x00004E04
			private static uint SkipAndCount(uint index, IEnumerator<TSource> en)
			{
				for (uint num = 0U; num < index; num += 1U)
				{
					if (!en.MoveNext())
					{
						return num;
					}
				}
				return index;
			}

			// Token: 0x04000025 RID: 37
			private readonly IEnumerable<TSource> _source;

			// Token: 0x04000026 RID: 38
			private readonly int _minIndexInclusive;

			// Token: 0x04000027 RID: 39
			private readonly int _maxIndexInclusive;

			// Token: 0x04000028 RID: 40
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x02000021 RID: 33
		private sealed class RangeIterator : Enumerable.Iterator<int>, IPartition<int>, IIListProvider<int>, IEnumerable<int>, IEnumerable
		{
			// Token: 0x060000FA RID: 250 RVA: 0x00006C28 File Offset: 0x00004E28
			public RangeIterator(int start, int count)
			{
				this._start = start;
				this._end = start + count;
			}

			// Token: 0x060000FB RID: 251 RVA: 0x00006C40 File Offset: 0x00004E40
			public override Enumerable.Iterator<int> Clone()
			{
				return new Enumerable.RangeIterator(this._start, this._end - this._start);
			}

			// Token: 0x060000FC RID: 252 RVA: 0x00006C5C File Offset: 0x00004E5C
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state == 2)
					{
						int num = this._current + 1;
						this._current = num;
						if (num != this._end)
						{
							return true;
						}
					}
					this._state = -1;
					return false;
				}
				this._current = this._start;
				this._state = 2;
				return true;
			}

			// Token: 0x060000FD RID: 253 RVA: 0x00006CB2 File Offset: 0x00004EB2
			public override void Dispose()
			{
				this._state = -1;
			}

			// Token: 0x060000FE RID: 254 RVA: 0x00006CBB File Offset: 0x00004EBB
			public override IEnumerable<TResult> Select<TResult>(Func<int, TResult> selector)
			{
				return new Enumerable.SelectIPartitionIterator<int, TResult>(this, selector);
			}

			// Token: 0x060000FF RID: 255 RVA: 0x00006CC4 File Offset: 0x00004EC4
			public int[] ToArray()
			{
				int[] array = new int[this._end - this._start];
				int num = this._start;
				for (int num2 = 0; num2 != array.Length; num2++)
				{
					array[num2] = num;
					num++;
				}
				return array;
			}

			// Token: 0x06000100 RID: 256 RVA: 0x00006D04 File Offset: 0x00004F04
			public List<int> ToList()
			{
				List<int> list = new List<int>(this._end - this._start);
				for (int num = this._start; num != this._end; num++)
				{
					list.Add(num);
				}
				return list;
			}

			// Token: 0x06000101 RID: 257 RVA: 0x00006D42 File Offset: 0x00004F42
			public int GetCount(bool onlyIfCheap)
			{
				return this._end - this._start;
			}

			// Token: 0x06000102 RID: 258 RVA: 0x00006D51 File Offset: 0x00004F51
			public IPartition<int> Skip(int count)
			{
				if (count >= this._end - this._start)
				{
					return EmptyPartition<int>.Instance;
				}
				return new Enumerable.RangeIterator(this._start + count, this._end - this._start - count);
			}

			// Token: 0x06000103 RID: 259 RVA: 0x00006D88 File Offset: 0x00004F88
			public IPartition<int> Take(int count)
			{
				int num = this._end - this._start;
				if (count >= num)
				{
					return this;
				}
				return new Enumerable.RangeIterator(this._start, count);
			}

			// Token: 0x06000104 RID: 260 RVA: 0x00006DB5 File Offset: 0x00004FB5
			public int TryGetElementAt(int index, out bool found)
			{
				if (index < this._end - this._start)
				{
					found = true;
					return this._start + index;
				}
				found = false;
				return 0;
			}

			// Token: 0x06000105 RID: 261 RVA: 0x00006DD7 File Offset: 0x00004FD7
			public int TryGetFirst(out bool found)
			{
				found = true;
				return this._start;
			}

			// Token: 0x06000106 RID: 262 RVA: 0x00006DE2 File Offset: 0x00004FE2
			public int TryGetLast(out bool found)
			{
				found = true;
				return this._end - 1;
			}

			// Token: 0x04000029 RID: 41
			private readonly int _start;

			// Token: 0x0400002A RID: 42
			private readonly int _end;
		}

		// Token: 0x02000022 RID: 34
		private sealed class RepeatIterator<TResult> : Enumerable.Iterator<TResult>, IPartition<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x06000107 RID: 263 RVA: 0x00006DEF File Offset: 0x00004FEF
			public RepeatIterator(TResult element, int count)
			{
				this._current = element;
				this._count = count;
			}

			// Token: 0x06000108 RID: 264 RVA: 0x00006E05 File Offset: 0x00005005
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.RepeatIterator<TResult>(this._current, this._count);
			}

			// Token: 0x06000109 RID: 265 RVA: 0x00006E18 File Offset: 0x00005018
			public override void Dispose()
			{
				this._state = -1;
			}

			// Token: 0x0600010A RID: 266 RVA: 0x00006E24 File Offset: 0x00005024
			public override bool MoveNext()
			{
				int num = this._state - 1;
				if (num >= 0 && num != this._count)
				{
					this._state++;
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x0600010B RID: 267 RVA: 0x0000689F File Offset: 0x00004A9F
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectIPartitionIterator<TResult, TResult2>(this, selector);
			}

			// Token: 0x0600010C RID: 268 RVA: 0x00006E60 File Offset: 0x00005060
			public TResult[] ToArray()
			{
				TResult[] array = new TResult[this._count];
				if (this._current != null)
				{
					Array.Fill<TResult>(array, this._current);
				}
				return array;
			}

			// Token: 0x0600010D RID: 269 RVA: 0x00006E94 File Offset: 0x00005094
			public List<TResult> ToList()
			{
				List<TResult> list = new List<TResult>(this._count);
				for (int num = 0; num != this._count; num++)
				{
					list.Add(this._current);
				}
				return list;
			}

			// Token: 0x0600010E RID: 270 RVA: 0x00006ECB File Offset: 0x000050CB
			public int GetCount(bool onlyIfCheap)
			{
				return this._count;
			}

			// Token: 0x0600010F RID: 271 RVA: 0x00006ED3 File Offset: 0x000050D3
			public IPartition<TResult> Skip(int count)
			{
				if (count >= this._count)
				{
					return EmptyPartition<TResult>.Instance;
				}
				return new Enumerable.RepeatIterator<TResult>(this._current, this._count - count);
			}

			// Token: 0x06000110 RID: 272 RVA: 0x00006EF7 File Offset: 0x000050F7
			public IPartition<TResult> Take(int count)
			{
				if (count >= this._count)
				{
					return this;
				}
				return new Enumerable.RepeatIterator<TResult>(this._current, count);
			}

			// Token: 0x06000111 RID: 273 RVA: 0x00006F10 File Offset: 0x00005110
			public TResult TryGetElementAt(int index, out bool found)
			{
				if (index < this._count)
				{
					found = true;
					return this._current;
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x06000112 RID: 274 RVA: 0x00006F3C File Offset: 0x0000513C
			public TResult TryGetFirst(out bool found)
			{
				found = true;
				return this._current;
			}

			// Token: 0x06000113 RID: 275 RVA: 0x00006F3C File Offset: 0x0000513C
			public TResult TryGetLast(out bool found)
			{
				found = true;
				return this._current;
			}

			// Token: 0x0400002B RID: 43
			private readonly int _count;
		}

		// Token: 0x02000023 RID: 35
		private sealed class ReverseIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x06000114 RID: 276 RVA: 0x00006F47 File Offset: 0x00005147
			public ReverseIterator(IEnumerable<TSource> source)
			{
				this._source = source;
			}

			// Token: 0x06000115 RID: 277 RVA: 0x00006F56 File Offset: 0x00005156
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.ReverseIterator<TSource>(this._source);
			}

			// Token: 0x06000116 RID: 278 RVA: 0x00006F64 File Offset: 0x00005164
			public override bool MoveNext()
			{
				if (this._state - 2 <= -2)
				{
					this.Dispose();
					return false;
				}
				if (this._state == 1)
				{
					Buffer<TSource> buffer = new Buffer<TSource>(this._source);
					this._buffer = buffer._items;
					this._state = buffer._count + 2;
				}
				int num = this._state - 3;
				if (num != -1)
				{
					this._current = this._buffer[num];
					this._state--;
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06000117 RID: 279 RVA: 0x00006FEB File Offset: 0x000051EB
			public override void Dispose()
			{
				this._buffer = null;
				base.Dispose();
			}

			// Token: 0x06000118 RID: 280 RVA: 0x00006FFA File Offset: 0x000051FA
			public TSource[] ToArray()
			{
				TSource[] array = this._source.ToArray<TSource>();
				Array.Reverse<TSource>(array);
				return array;
			}

			// Token: 0x06000119 RID: 281 RVA: 0x0000700D File Offset: 0x0000520D
			public List<TSource> ToList()
			{
				List<TSource> list = this._source.ToList<TSource>();
				list.Reverse();
				return list;
			}

			// Token: 0x0600011A RID: 282 RVA: 0x00007020 File Offset: 0x00005220
			public int GetCount(bool onlyIfCheap)
			{
				if (!onlyIfCheap)
				{
					return this._source.Count<TSource>();
				}
				IEnumerable<TSource> source = this._source;
				IIListProvider<TSource> iilistProvider = source as IIListProvider<TSource>;
				if (iilistProvider != null)
				{
					return iilistProvider.GetCount(true);
				}
				ICollection<TSource> collection = source as ICollection<TSource>;
				if (collection != null)
				{
					return collection.Count;
				}
				ICollection collection2 = source as ICollection;
				if (collection2 == null)
				{
					return -1;
				}
				return collection2.Count;
			}

			// Token: 0x0400002C RID: 44
			private readonly IEnumerable<TSource> _source;

			// Token: 0x0400002D RID: 45
			private TSource[] _buffer;
		}

		// Token: 0x02000024 RID: 36
		private sealed class SelectEnumerableIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x0600011B RID: 283 RVA: 0x0000707A File Offset: 0x0000527A
			public SelectEnumerableIterator(IEnumerable<TSource> source, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x0600011C RID: 284 RVA: 0x00007090 File Offset: 0x00005290
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectEnumerableIterator<TSource, TResult>(this._source, this._selector);
			}

			// Token: 0x0600011D RID: 285 RVA: 0x000070A3 File Offset: 0x000052A3
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x0600011E RID: 286 RVA: 0x000070C8 File Offset: 0x000052C8
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				if (this._enumerator.MoveNext())
				{
					this._current = this._selector(this._enumerator.Current);
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x0600011F RID: 287 RVA: 0x00007130 File Offset: 0x00005330
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectEnumerableIterator<TSource, TResult2>(this._source, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x06000120 RID: 288 RVA: 0x0000714C File Offset: 0x0000534C
			public TResult[] ToArray()
			{
				global::System.Collections.Generic.LargeArrayBuilder<TResult> largeArrayBuilder = new global::System.Collections.Generic.LargeArrayBuilder<TResult>(true);
				foreach (TSource tsource in this._source)
				{
					largeArrayBuilder.Add(this._selector(tsource));
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x06000121 RID: 289 RVA: 0x000071B4 File Offset: 0x000053B4
			public List<TResult> ToList()
			{
				List<TResult> list = new List<TResult>();
				foreach (TSource tsource in this._source)
				{
					list.Add(this._selector(tsource));
				}
				return list;
			}

			// Token: 0x06000122 RID: 290 RVA: 0x00007214 File Offset: 0x00005414
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				checked
				{
					foreach (TSource tsource in this._source)
					{
						this._selector(tsource);
						num++;
					}
					return num;
				}
			}

			// Token: 0x0400002E RID: 46
			private readonly IEnumerable<TSource> _source;

			// Token: 0x0400002F RID: 47
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x04000030 RID: 48
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x02000025 RID: 37
		private sealed class SelectArrayIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IPartition<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x06000123 RID: 291 RVA: 0x00007274 File Offset: 0x00005474
			public SelectArrayIterator(TSource[] source, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x06000124 RID: 292 RVA: 0x0000728A File Offset: 0x0000548A
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectArrayIterator<TSource, TResult>(this._source, this._selector);
			}

			// Token: 0x06000125 RID: 293 RVA: 0x000072A0 File Offset: 0x000054A0
			public override bool MoveNext()
			{
				if ((this._state < 1) | (this._state == this._source.Length + 1))
				{
					this.Dispose();
					return false;
				}
				int state = this._state;
				this._state = state + 1;
				int num = state - 1;
				this._current = this._selector(this._source[num]);
				return true;
			}

			// Token: 0x06000126 RID: 294 RVA: 0x00007305 File Offset: 0x00005505
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectArrayIterator<TSource, TResult2>(this._source, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x06000127 RID: 295 RVA: 0x00007320 File Offset: 0x00005520
			public TResult[] ToArray()
			{
				TResult[] array = new TResult[this._source.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._selector(this._source[i]);
				}
				return array;
			}

			// Token: 0x06000128 RID: 296 RVA: 0x00007368 File Offset: 0x00005568
			public List<TResult> ToList()
			{
				TSource[] source = this._source;
				List<TResult> list = new List<TResult>(source.Length);
				for (int i = 0; i < source.Length; i++)
				{
					list.Add(this._selector(source[i]));
				}
				return list;
			}

			// Token: 0x06000129 RID: 297 RVA: 0x000073AC File Offset: 0x000055AC
			public int GetCount(bool onlyIfCheap)
			{
				if (!onlyIfCheap)
				{
					foreach (TSource tsource in this._source)
					{
						this._selector(tsource);
					}
				}
				return this._source.Length;
			}

			// Token: 0x0600012A RID: 298 RVA: 0x000073EE File Offset: 0x000055EE
			public IPartition<TResult> Skip(int count)
			{
				if (count >= this._source.Length)
				{
					return EmptyPartition<TResult>.Instance;
				}
				return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, count, int.MaxValue);
			}

			// Token: 0x0600012B RID: 299 RVA: 0x00007418 File Offset: 0x00005618
			public IPartition<TResult> Take(int count)
			{
				if (count < this._source.Length)
				{
					return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, 0, count - 1);
				}
				return this;
			}

			// Token: 0x0600012C RID: 300 RVA: 0x0000744C File Offset: 0x0000564C
			public TResult TryGetElementAt(int index, out bool found)
			{
				if (index < this._source.Length)
				{
					found = true;
					return this._selector(this._source[index]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x0600012D RID: 301 RVA: 0x0000748B File Offset: 0x0000568B
			public TResult TryGetFirst(out bool found)
			{
				found = true;
				return this._selector(this._source[0]);
			}

			// Token: 0x0600012E RID: 302 RVA: 0x000074A7 File Offset: 0x000056A7
			public TResult TryGetLast(out bool found)
			{
				found = true;
				return this._selector(this._source[this._source.Length - 1]);
			}

			// Token: 0x04000031 RID: 49
			private readonly TSource[] _source;

			// Token: 0x04000032 RID: 50
			private readonly Func<TSource, TResult> _selector;
		}

		// Token: 0x02000026 RID: 38
		private sealed class SelectListIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IPartition<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x0600012F RID: 303 RVA: 0x000074CC File Offset: 0x000056CC
			public SelectListIterator(List<TSource> source, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x06000130 RID: 304 RVA: 0x000074E2 File Offset: 0x000056E2
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectListIterator<TSource, TResult>(this._source, this._selector);
			}

			// Token: 0x06000131 RID: 305 RVA: 0x000074F8 File Offset: 0x000056F8
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				if (this._enumerator.MoveNext())
				{
					this._current = this._selector(this._enumerator.Current);
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06000132 RID: 306 RVA: 0x00007560 File Offset: 0x00005760
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectListIterator<TSource, TResult2>(this._source, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x06000133 RID: 307 RVA: 0x0000757C File Offset: 0x0000577C
			public TResult[] ToArray()
			{
				int count = this._source.Count;
				if (count == 0)
				{
					return Array.Empty<TResult>();
				}
				TResult[] array = new TResult[count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._selector(this._source[i]);
				}
				return array;
			}

			// Token: 0x06000134 RID: 308 RVA: 0x000075D4 File Offset: 0x000057D4
			public List<TResult> ToList()
			{
				int count = this._source.Count;
				List<TResult> list = new List<TResult>(count);
				for (int i = 0; i < count; i++)
				{
					list.Add(this._selector(this._source[i]));
				}
				return list;
			}

			// Token: 0x06000135 RID: 309 RVA: 0x00007620 File Offset: 0x00005820
			public int GetCount(bool onlyIfCheap)
			{
				int count = this._source.Count;
				if (!onlyIfCheap)
				{
					for (int i = 0; i < count; i++)
					{
						this._selector(this._source[i]);
					}
				}
				return count;
			}

			// Token: 0x06000136 RID: 310 RVA: 0x00007661 File Offset: 0x00005861
			public IPartition<TResult> Skip(int count)
			{
				return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, count, int.MaxValue);
			}

			// Token: 0x06000137 RID: 311 RVA: 0x0000767A File Offset: 0x0000587A
			public IPartition<TResult> Take(int count)
			{
				return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, 0, count - 1);
			}

			// Token: 0x06000138 RID: 312 RVA: 0x00007694 File Offset: 0x00005894
			public TResult TryGetElementAt(int index, out bool found)
			{
				if (index < this._source.Count)
				{
					found = true;
					return this._selector(this._source[index]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x06000139 RID: 313 RVA: 0x000076D8 File Offset: 0x000058D8
			public TResult TryGetFirst(out bool found)
			{
				if (this._source.Count != 0)
				{
					found = true;
					return this._selector(this._source[0]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x0600013A RID: 314 RVA: 0x0000771C File Offset: 0x0000591C
			public TResult TryGetLast(out bool found)
			{
				int count = this._source.Count;
				if (count != 0)
				{
					found = true;
					return this._selector(this._source[count - 1]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x04000033 RID: 51
			private readonly List<TSource> _source;

			// Token: 0x04000034 RID: 52
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x04000035 RID: 53
			private List<TSource>.Enumerator _enumerator;
		}

		// Token: 0x02000027 RID: 39
		private sealed class SelectIListIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IPartition<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x0600013B RID: 315 RVA: 0x00007761 File Offset: 0x00005961
			public SelectIListIterator(IList<TSource> source, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x0600013C RID: 316 RVA: 0x00007777 File Offset: 0x00005977
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectIListIterator<TSource, TResult>(this._source, this._selector);
			}

			// Token: 0x0600013D RID: 317 RVA: 0x0000778C File Offset: 0x0000598C
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				if (this._enumerator.MoveNext())
				{
					this._current = this._selector(this._enumerator.Current);
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x0600013E RID: 318 RVA: 0x000077F4 File Offset: 0x000059F4
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x0600013F RID: 319 RVA: 0x00007816 File Offset: 0x00005A16
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectIListIterator<TSource, TResult2>(this._source, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x06000140 RID: 320 RVA: 0x00007830 File Offset: 0x00005A30
			public TResult[] ToArray()
			{
				int count = this._source.Count;
				if (count == 0)
				{
					return Array.Empty<TResult>();
				}
				TResult[] array = new TResult[count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._selector(this._source[i]);
				}
				return array;
			}

			// Token: 0x06000141 RID: 321 RVA: 0x00007888 File Offset: 0x00005A88
			public List<TResult> ToList()
			{
				int count = this._source.Count;
				List<TResult> list = new List<TResult>(count);
				for (int i = 0; i < count; i++)
				{
					list.Add(this._selector(this._source[i]));
				}
				return list;
			}

			// Token: 0x06000142 RID: 322 RVA: 0x000078D4 File Offset: 0x00005AD4
			public int GetCount(bool onlyIfCheap)
			{
				int count = this._source.Count;
				if (!onlyIfCheap)
				{
					for (int i = 0; i < count; i++)
					{
						this._selector(this._source[i]);
					}
				}
				return count;
			}

			// Token: 0x06000143 RID: 323 RVA: 0x00007915 File Offset: 0x00005B15
			public IPartition<TResult> Skip(int count)
			{
				return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, count, int.MaxValue);
			}

			// Token: 0x06000144 RID: 324 RVA: 0x0000792E File Offset: 0x00005B2E
			public IPartition<TResult> Take(int count)
			{
				return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, 0, count - 1);
			}

			// Token: 0x06000145 RID: 325 RVA: 0x00007948 File Offset: 0x00005B48
			public TResult TryGetElementAt(int index, out bool found)
			{
				if (index < this._source.Count)
				{
					found = true;
					return this._selector(this._source[index]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x06000146 RID: 326 RVA: 0x0000798C File Offset: 0x00005B8C
			public TResult TryGetFirst(out bool found)
			{
				if (this._source.Count != 0)
				{
					found = true;
					return this._selector(this._source[0]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x06000147 RID: 327 RVA: 0x000079D0 File Offset: 0x00005BD0
			public TResult TryGetLast(out bool found)
			{
				int count = this._source.Count;
				if (count != 0)
				{
					found = true;
					return this._selector(this._source[count - 1]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x04000036 RID: 54
			private readonly IList<TSource> _source;

			// Token: 0x04000037 RID: 55
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x04000038 RID: 56
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x02000028 RID: 40
		private sealed class SelectIPartitionIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IPartition<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x06000148 RID: 328 RVA: 0x00007A15 File Offset: 0x00005C15
			public SelectIPartitionIterator(IPartition<TSource> source, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x06000149 RID: 329 RVA: 0x00007A2B File Offset: 0x00005C2B
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectIPartitionIterator<TSource, TResult>(this._source, this._selector);
			}

			// Token: 0x0600014A RID: 330 RVA: 0x00007A40 File Offset: 0x00005C40
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				if (this._enumerator.MoveNext())
				{
					this._current = this._selector(this._enumerator.Current);
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x0600014B RID: 331 RVA: 0x00007AA8 File Offset: 0x00005CA8
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x0600014C RID: 332 RVA: 0x00007ACA File Offset: 0x00005CCA
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectIPartitionIterator<TSource, TResult2>(this._source, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x0600014D RID: 333 RVA: 0x00007AE3 File Offset: 0x00005CE3
			public IPartition<TResult> Skip(int count)
			{
				return new Enumerable.SelectIPartitionIterator<TSource, TResult>(this._source.Skip(count), this._selector);
			}

			// Token: 0x0600014E RID: 334 RVA: 0x00007AFC File Offset: 0x00005CFC
			public IPartition<TResult> Take(int count)
			{
				return new Enumerable.SelectIPartitionIterator<TSource, TResult>(this._source.Take(count), this._selector);
			}

			// Token: 0x0600014F RID: 335 RVA: 0x00007B18 File Offset: 0x00005D18
			public TResult TryGetElementAt(int index, out bool found)
			{
				bool flag;
				TSource tsource = this._source.TryGetElementAt(index, out flag);
				found = flag;
				if (!flag)
				{
					return default(TResult);
				}
				return this._selector(tsource);
			}

			// Token: 0x06000150 RID: 336 RVA: 0x00007B50 File Offset: 0x00005D50
			public TResult TryGetFirst(out bool found)
			{
				bool flag;
				TSource tsource = this._source.TryGetFirst(out flag);
				found = flag;
				if (!flag)
				{
					return default(TResult);
				}
				return this._selector(tsource);
			}

			// Token: 0x06000151 RID: 337 RVA: 0x00007B88 File Offset: 0x00005D88
			public TResult TryGetLast(out bool found)
			{
				bool flag;
				TSource tsource = this._source.TryGetLast(out flag);
				found = flag;
				if (!flag)
				{
					return default(TResult);
				}
				return this._selector(tsource);
			}

			// Token: 0x06000152 RID: 338 RVA: 0x00007BC0 File Offset: 0x00005DC0
			private TResult[] LazyToArray()
			{
				global::System.Collections.Generic.LargeArrayBuilder<TResult> largeArrayBuilder = new global::System.Collections.Generic.LargeArrayBuilder<TResult>(true);
				foreach (TSource tsource in this._source)
				{
					largeArrayBuilder.Add(this._selector(tsource));
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x06000153 RID: 339 RVA: 0x00007C28 File Offset: 0x00005E28
			private TResult[] PreallocatingToArray(int count)
			{
				TResult[] array = new TResult[count];
				int num = 0;
				foreach (TSource tsource in this._source)
				{
					array[num] = this._selector(tsource);
					num++;
				}
				return array;
			}

			// Token: 0x06000154 RID: 340 RVA: 0x00007C90 File Offset: 0x00005E90
			public TResult[] ToArray()
			{
				int count = this._source.GetCount(true);
				if (count == -1)
				{
					return this.LazyToArray();
				}
				if (count != 0)
				{
					return this.PreallocatingToArray(count);
				}
				return Array.Empty<TResult>();
			}

			// Token: 0x06000155 RID: 341 RVA: 0x00007CC8 File Offset: 0x00005EC8
			public List<TResult> ToList()
			{
				int count = this._source.GetCount(true);
				List<TResult> list;
				if (count != -1)
				{
					if (count == 0)
					{
						return new List<TResult>();
					}
					list = new List<TResult>(count);
				}
				else
				{
					list = new List<TResult>();
				}
				foreach (TSource tsource in this._source)
				{
					list.Add(this._selector(tsource));
				}
				return list;
			}

			// Token: 0x06000156 RID: 342 RVA: 0x00007D4C File Offset: 0x00005F4C
			public int GetCount(bool onlyIfCheap)
			{
				if (!onlyIfCheap)
				{
					foreach (TSource tsource in this._source)
					{
						this._selector(tsource);
					}
				}
				return this._source.GetCount(onlyIfCheap);
			}

			// Token: 0x04000039 RID: 57
			private readonly IPartition<TSource> _source;

			// Token: 0x0400003A RID: 58
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x0400003B RID: 59
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x02000029 RID: 41
		private sealed class SelectListPartitionIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IPartition<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x06000157 RID: 343 RVA: 0x00007DB0 File Offset: 0x00005FB0
			public SelectListPartitionIterator(IList<TSource> source, Func<TSource, TResult> selector, int minIndexInclusive, int maxIndexInclusive)
			{
				this._source = source;
				this._selector = selector;
				this._minIndexInclusive = minIndexInclusive;
				this._maxIndexInclusive = maxIndexInclusive;
			}

			// Token: 0x06000158 RID: 344 RVA: 0x00007DD5 File Offset: 0x00005FD5
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, this._minIndexInclusive, this._maxIndexInclusive);
			}

			// Token: 0x06000159 RID: 345 RVA: 0x00007DF4 File Offset: 0x00005FF4
			public override bool MoveNext()
			{
				int num = this._state - 1;
				if (num <= this._maxIndexInclusive - this._minIndexInclusive && num < this._source.Count - this._minIndexInclusive)
				{
					this._current = this._selector(this._source[this._minIndexInclusive + num]);
					this._state++;
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x0600015A RID: 346 RVA: 0x00007E6A File Offset: 0x0000606A
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectListPartitionIterator<TSource, TResult2>(this._source, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector), this._minIndexInclusive, this._maxIndexInclusive);
			}

			// Token: 0x0600015B RID: 347 RVA: 0x00007E90 File Offset: 0x00006090
			public IPartition<TResult> Skip(int count)
			{
				int num = this._minIndexInclusive + count;
				if (num <= this._maxIndexInclusive)
				{
					return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, num, this._maxIndexInclusive);
				}
				return EmptyPartition<TResult>.Instance;
			}

			// Token: 0x0600015C RID: 348 RVA: 0x00007ED0 File Offset: 0x000060D0
			public IPartition<TResult> Take(int count)
			{
				int num = this._minIndexInclusive + count - 1;
				if (num < this._maxIndexInclusive)
				{
					return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, this._minIndexInclusive, num);
				}
				return this;
			}

			// Token: 0x0600015D RID: 349 RVA: 0x00007F0C File Offset: 0x0000610C
			public TResult TryGetElementAt(int index, out bool found)
			{
				if (index <= this._maxIndexInclusive - this._minIndexInclusive && index < this._source.Count - this._minIndexInclusive)
				{
					found = true;
					return this._selector(this._source[this._minIndexInclusive + index]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x0600015E RID: 350 RVA: 0x00007F6C File Offset: 0x0000616C
			public TResult TryGetFirst(out bool found)
			{
				if (this._source.Count > this._minIndexInclusive)
				{
					found = true;
					return this._selector(this._source[this._minIndexInclusive]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x0600015F RID: 351 RVA: 0x00007FB8 File Offset: 0x000061B8
			public TResult TryGetLast(out bool found)
			{
				int num = this._source.Count - 1;
				if (num >= this._minIndexInclusive)
				{
					found = true;
					return this._selector(this._source[Math.Min(num, this._maxIndexInclusive)]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000160 RID: 352 RVA: 0x00008010 File Offset: 0x00006210
			private int Count
			{
				get
				{
					int count = this._source.Count;
					if (count <= this._minIndexInclusive)
					{
						return 0;
					}
					return Math.Min(count - 1, this._maxIndexInclusive) - this._minIndexInclusive + 1;
				}
			}

			// Token: 0x06000161 RID: 353 RVA: 0x0000804C File Offset: 0x0000624C
			public TResult[] ToArray()
			{
				int count = this.Count;
				if (count == 0)
				{
					return Array.Empty<TResult>();
				}
				TResult[] array = new TResult[count];
				int num = 0;
				int num2 = this._minIndexInclusive;
				while (num != array.Length)
				{
					array[num] = this._selector(this._source[num2]);
					num++;
					num2++;
				}
				return array;
			}

			// Token: 0x06000162 RID: 354 RVA: 0x000080A8 File Offset: 0x000062A8
			public List<TResult> ToList()
			{
				int count = this.Count;
				if (count == 0)
				{
					return new List<TResult>();
				}
				List<TResult> list = new List<TResult>(count);
				int num = this._minIndexInclusive + count;
				for (int num2 = this._minIndexInclusive; num2 != num; num2++)
				{
					list.Add(this._selector(this._source[num2]));
				}
				return list;
			}

			// Token: 0x06000163 RID: 355 RVA: 0x00008104 File Offset: 0x00006304
			public int GetCount(bool onlyIfCheap)
			{
				int count = this.Count;
				if (!onlyIfCheap)
				{
					int num = this._minIndexInclusive + count;
					for (int num2 = this._minIndexInclusive; num2 != num; num2++)
					{
						this._selector(this._source[num2]);
					}
				}
				return count;
			}

			// Token: 0x0400003C RID: 60
			private readonly IList<TSource> _source;

			// Token: 0x0400003D RID: 61
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x0400003E RID: 62
			private readonly int _minIndexInclusive;

			// Token: 0x0400003F RID: 63
			private readonly int _maxIndexInclusive;
		}

		// Token: 0x0200002A RID: 42
		private sealed class SelectManySingleSelectorIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x06000164 RID: 356 RVA: 0x0000814E File Offset: 0x0000634E
			internal SelectManySingleSelectorIterator(IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x06000165 RID: 357 RVA: 0x00008164 File Offset: 0x00006364
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectManySingleSelectorIterator<TSource, TResult>(this._source, this._selector);
			}

			// Token: 0x06000166 RID: 358 RVA: 0x00008177 File Offset: 0x00006377
			public override void Dispose()
			{
				if (this._subEnumerator != null)
				{
					this._subEnumerator.Dispose();
					this._subEnumerator = null;
				}
				if (this._sourceEnumerator != null)
				{
					this._sourceEnumerator.Dispose();
					this._sourceEnumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x06000167 RID: 359 RVA: 0x000081B4 File Offset: 0x000063B4
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				checked
				{
					foreach (TSource tsource in this._source)
					{
						num += this._selector(tsource).Count<TResult>();
					}
					return num;
				}
			}

			// Token: 0x06000168 RID: 360 RVA: 0x00008218 File Offset: 0x00006418
			public override bool MoveNext()
			{
				switch (this._state)
				{
				case 1:
					this._sourceEnumerator = this._source.GetEnumerator();
					this._state = 2;
					break;
				case 2:
					break;
				case 3:
					goto IL_006F;
				default:
					goto IL_00AA;
				}
				IL_0038:
				if (!this._sourceEnumerator.MoveNext())
				{
					goto IL_00AA;
				}
				TSource tsource = this._sourceEnumerator.Current;
				this._subEnumerator = this._selector(tsource).GetEnumerator();
				this._state = 3;
				IL_006F:
				if (!this._subEnumerator.MoveNext())
				{
					this._subEnumerator.Dispose();
					this._subEnumerator = null;
					this._state = 2;
					goto IL_0038;
				}
				this._current = this._subEnumerator.Current;
				return true;
				IL_00AA:
				this.Dispose();
				return false;
			}

			// Token: 0x06000169 RID: 361 RVA: 0x000082D8 File Offset: 0x000064D8
			public TResult[] ToArray()
			{
				SparseArrayBuilder<TResult> sparseArrayBuilder = new SparseArrayBuilder<TResult>(true);
				global::System.Collections.Generic.ArrayBuilder<IEnumerable<TResult>> arrayBuilder = default(global::System.Collections.Generic.ArrayBuilder<IEnumerable<TResult>>);
				foreach (TSource tsource in this._source)
				{
					IEnumerable<TResult> enumerable = this._selector(tsource);
					if (sparseArrayBuilder.ReserveOrAdd(enumerable))
					{
						arrayBuilder.Add(enumerable);
					}
				}
				TResult[] array = sparseArrayBuilder.ToArray();
				global::System.Collections.Generic.ArrayBuilder<Marker> markers = sparseArrayBuilder.Markers;
				for (int i = 0; i < markers.Count; i++)
				{
					Marker marker = markers[i];
					global::System.Collections.Generic.EnumerableHelpers.Copy<TResult>(arrayBuilder[i], array, marker.Index, marker.Count);
				}
				return array;
			}

			// Token: 0x0600016A RID: 362 RVA: 0x000083A8 File Offset: 0x000065A8
			public List<TResult> ToList()
			{
				List<TResult> list = new List<TResult>();
				foreach (TSource tsource in this._source)
				{
					list.AddRange(this._selector(tsource));
				}
				return list;
			}

			// Token: 0x04000040 RID: 64
			private readonly IEnumerable<TSource> _source;

			// Token: 0x04000041 RID: 65
			private readonly Func<TSource, IEnumerable<TResult>> _selector;

			// Token: 0x04000042 RID: 66
			private IEnumerator<TSource> _sourceEnumerator;

			// Token: 0x04000043 RID: 67
			private IEnumerator<TResult> _subEnumerator;
		}

		// Token: 0x0200002B RID: 43
		private abstract class UnionIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x0600016B RID: 363 RVA: 0x00008408 File Offset: 0x00006608
			protected UnionIterator(IEqualityComparer<TSource> comparer)
			{
				this._comparer = comparer;
			}

			// Token: 0x0600016C RID: 364 RVA: 0x00008417 File Offset: 0x00006617
			public sealed override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
					this._set = null;
				}
				base.Dispose();
			}

			// Token: 0x0600016D RID: 365
			internal abstract IEnumerable<TSource> GetEnumerable(int index);

			// Token: 0x0600016E RID: 366
			internal abstract Enumerable.UnionIterator<TSource> Union(IEnumerable<TSource> next);

			// Token: 0x0600016F RID: 367 RVA: 0x00008440 File Offset: 0x00006640
			private void SetEnumerator(IEnumerator<TSource> enumerator)
			{
				IEnumerator<TSource> enumerator2 = this._enumerator;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
				this._enumerator = enumerator;
			}

			// Token: 0x06000170 RID: 368 RVA: 0x0000845C File Offset: 0x0000665C
			private void StoreFirst()
			{
				Set<TSource> set = new Set<TSource>(this._comparer);
				TSource tsource = this._enumerator.Current;
				set.Add(tsource);
				this._current = tsource;
				this._set = set;
			}

			// Token: 0x06000171 RID: 369 RVA: 0x00008498 File Offset: 0x00006698
			private bool GetNext()
			{
				Set<TSource> set = this._set;
				while (this._enumerator.MoveNext())
				{
					TSource tsource = this._enumerator.Current;
					if (set.Add(tsource))
					{
						this._current = tsource;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000172 RID: 370 RVA: 0x000084DC File Offset: 0x000066DC
			public sealed override bool MoveNext()
			{
				if (this._state == 1)
				{
					for (IEnumerable<TSource> enumerable = this.GetEnumerable(0); enumerable != null; enumerable = this.GetEnumerable(this._state - 1))
					{
						IEnumerator<TSource> enumerator = enumerable.GetEnumerator();
						this._state++;
						if (enumerator.MoveNext())
						{
							this.SetEnumerator(enumerator);
							this.StoreFirst();
							return true;
						}
					}
				}
				else if (this._state > 0)
				{
					while (!this.GetNext())
					{
						IEnumerable<TSource> enumerable2 = this.GetEnumerable(this._state - 1);
						if (enumerable2 == null)
						{
							goto IL_0094;
						}
						this.SetEnumerator(enumerable2.GetEnumerator());
						this._state++;
					}
					return true;
				}
				IL_0094:
				this.Dispose();
				return false;
			}

			// Token: 0x06000173 RID: 371 RVA: 0x00008584 File Offset: 0x00006784
			private Set<TSource> FillSet()
			{
				Set<TSource> set = new Set<TSource>(this._comparer);
				int num = 0;
				for (;;)
				{
					IEnumerable<TSource> enumerable = this.GetEnumerable(num);
					if (enumerable == null)
					{
						break;
					}
					set.UnionWith(enumerable);
					num++;
				}
				return set;
			}

			// Token: 0x06000174 RID: 372 RVA: 0x000085B8 File Offset: 0x000067B8
			public TSource[] ToArray()
			{
				return this.FillSet().ToArray();
			}

			// Token: 0x06000175 RID: 373 RVA: 0x000085C5 File Offset: 0x000067C5
			public List<TSource> ToList()
			{
				return this.FillSet().ToList();
			}

			// Token: 0x06000176 RID: 374 RVA: 0x000085D2 File Offset: 0x000067D2
			public int GetCount(bool onlyIfCheap)
			{
				if (!onlyIfCheap)
				{
					return this.FillSet().Count;
				}
				return -1;
			}

			// Token: 0x04000044 RID: 68
			internal readonly IEqualityComparer<TSource> _comparer;

			// Token: 0x04000045 RID: 69
			private IEnumerator<TSource> _enumerator;

			// Token: 0x04000046 RID: 70
			private Set<TSource> _set;
		}

		// Token: 0x0200002C RID: 44
		private sealed class UnionIterator2<TSource> : Enumerable.UnionIterator<TSource>
		{
			// Token: 0x06000177 RID: 375 RVA: 0x000085E4 File Offset: 0x000067E4
			public UnionIterator2(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
				: base(comparer)
			{
				this._first = first;
				this._second = second;
			}

			// Token: 0x06000178 RID: 376 RVA: 0x000085FB File Offset: 0x000067FB
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.UnionIterator2<TSource>(this._first, this._second, this._comparer);
			}

			// Token: 0x06000179 RID: 377 RVA: 0x00008614 File Offset: 0x00006814
			internal override IEnumerable<TSource> GetEnumerable(int index)
			{
				if (index == 0)
				{
					return this._first;
				}
				if (index != 1)
				{
					return null;
				}
				return this._second;
			}

			// Token: 0x0600017A RID: 378 RVA: 0x0000862E File Offset: 0x0000682E
			internal override Enumerable.UnionIterator<TSource> Union(IEnumerable<TSource> next)
			{
				return new Enumerable.UnionIteratorN<TSource>(new SingleLinkedNode<IEnumerable<TSource>>(this._first).Add(this._second).Add(next), 2, this._comparer);
			}

			// Token: 0x04000047 RID: 71
			private readonly IEnumerable<TSource> _first;

			// Token: 0x04000048 RID: 72
			private readonly IEnumerable<TSource> _second;
		}

		// Token: 0x0200002D RID: 45
		private sealed class UnionIteratorN<TSource> : Enumerable.UnionIterator<TSource>
		{
			// Token: 0x0600017B RID: 379 RVA: 0x00008658 File Offset: 0x00006858
			public UnionIteratorN(SingleLinkedNode<IEnumerable<TSource>> sources, int headIndex, IEqualityComparer<TSource> comparer)
				: base(comparer)
			{
				this._sources = sources;
				this._headIndex = headIndex;
			}

			// Token: 0x0600017C RID: 380 RVA: 0x0000866F File Offset: 0x0000686F
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.UnionIteratorN<TSource>(this._sources, this._headIndex, this._comparer);
			}

			// Token: 0x0600017D RID: 381 RVA: 0x00008688 File Offset: 0x00006888
			internal override IEnumerable<TSource> GetEnumerable(int index)
			{
				if (index <= this._headIndex)
				{
					return this._sources.GetNode(this._headIndex - index).Item;
				}
				return null;
			}

			// Token: 0x0600017E RID: 382 RVA: 0x000086AD File Offset: 0x000068AD
			internal override Enumerable.UnionIterator<TSource> Union(IEnumerable<TSource> next)
			{
				if (this._headIndex == 2147483645)
				{
					return new Enumerable.UnionIterator2<TSource>(this, next, this._comparer);
				}
				return new Enumerable.UnionIteratorN<TSource>(this._sources.Add(next), this._headIndex + 1, this._comparer);
			}

			// Token: 0x04000049 RID: 73
			private readonly SingleLinkedNode<IEnumerable<TSource>> _sources;

			// Token: 0x0400004A RID: 74
			private readonly int _headIndex;
		}

		// Token: 0x0200002E RID: 46
		private sealed class WhereEnumerableIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x0600017F RID: 383 RVA: 0x000086E9 File Offset: 0x000068E9
			public WhereEnumerableIterator(IEnumerable<TSource> source, Func<TSource, bool> predicate)
			{
				this._source = source;
				this._predicate = predicate;
			}

			// Token: 0x06000180 RID: 384 RVA: 0x000086FF File Offset: 0x000068FF
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.WhereEnumerableIterator<TSource>(this._source, this._predicate);
			}

			// Token: 0x06000181 RID: 385 RVA: 0x00008712 File Offset: 0x00006912
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x06000182 RID: 386 RVA: 0x00008734 File Offset: 0x00006934
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				checked
				{
					foreach (TSource tsource in this._source)
					{
						if (this._predicate(tsource))
						{
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x06000183 RID: 387 RVA: 0x00008794 File Offset: 0x00006994
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				while (this._enumerator.MoveNext())
				{
					TSource tsource = this._enumerator.Current;
					if (this._predicate(tsource))
					{
						this._current = tsource;
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06000184 RID: 388 RVA: 0x00008803 File Offset: 0x00006A03
			public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.WhereSelectEnumerableIterator<TSource, TResult>(this._source, this._predicate, selector);
			}

			// Token: 0x06000185 RID: 389 RVA: 0x00008818 File Offset: 0x00006A18
			public TSource[] ToArray()
			{
				global::System.Collections.Generic.LargeArrayBuilder<TSource> largeArrayBuilder = new global::System.Collections.Generic.LargeArrayBuilder<TSource>(true);
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						largeArrayBuilder.Add(tsource);
					}
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x06000186 RID: 390 RVA: 0x00008884 File Offset: 0x00006A84
			public List<TSource> ToList()
			{
				List<TSource> list = new List<TSource>();
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						list.Add(tsource);
					}
				}
				return list;
			}

			// Token: 0x06000187 RID: 391 RVA: 0x000088E8 File Offset: 0x00006AE8
			public override IEnumerable<TSource> Where(Func<TSource, bool> predicate)
			{
				return new Enumerable.WhereEnumerableIterator<TSource>(this._source, Utilities.CombinePredicates<TSource>(this._predicate, predicate));
			}

			// Token: 0x0400004B RID: 75
			private readonly IEnumerable<TSource> _source;

			// Token: 0x0400004C RID: 76
			private readonly Func<TSource, bool> _predicate;

			// Token: 0x0400004D RID: 77
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x0200002F RID: 47
		internal sealed class WhereArrayIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x06000188 RID: 392 RVA: 0x00008901 File Offset: 0x00006B01
			public WhereArrayIterator(TSource[] source, Func<TSource, bool> predicate)
			{
				this._source = source;
				this._predicate = predicate;
			}

			// Token: 0x06000189 RID: 393 RVA: 0x00008917 File Offset: 0x00006B17
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.WhereArrayIterator<TSource>(this._source, this._predicate);
			}

			// Token: 0x0600018A RID: 394 RVA: 0x0000892C File Offset: 0x00006B2C
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				checked
				{
					foreach (TSource tsource in this._source)
					{
						if (this._predicate(tsource))
						{
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x0600018B RID: 395 RVA: 0x00008970 File Offset: 0x00006B70
			public override bool MoveNext()
			{
				int i = this._state - 1;
				TSource[] source = this._source;
				while (i < source.Length)
				{
					TSource tsource = source[i];
					int state = this._state;
					this._state = state + 1;
					i = state;
					if (this._predicate(tsource))
					{
						this._current = tsource;
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x0600018C RID: 396 RVA: 0x000089CD File Offset: 0x00006BCD
			public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.WhereSelectArrayIterator<TSource, TResult>(this._source, this._predicate, selector);
			}

			// Token: 0x0600018D RID: 397 RVA: 0x000089E4 File Offset: 0x00006BE4
			public TSource[] ToArray()
			{
				global::System.Collections.Generic.LargeArrayBuilder<TSource> largeArrayBuilder = new global::System.Collections.Generic.LargeArrayBuilder<TSource>(this._source.Length);
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						largeArrayBuilder.Add(tsource);
					}
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x0600018E RID: 398 RVA: 0x00008A3C File Offset: 0x00006C3C
			public List<TSource> ToList()
			{
				List<TSource> list = new List<TSource>();
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						list.Add(tsource);
					}
				}
				return list;
			}

			// Token: 0x0600018F RID: 399 RVA: 0x00008A82 File Offset: 0x00006C82
			public override IEnumerable<TSource> Where(Func<TSource, bool> predicate)
			{
				return new Enumerable.WhereArrayIterator<TSource>(this._source, Utilities.CombinePredicates<TSource>(this._predicate, predicate));
			}

			// Token: 0x0400004E RID: 78
			private readonly TSource[] _source;

			// Token: 0x0400004F RID: 79
			private readonly Func<TSource, bool> _predicate;
		}

		// Token: 0x02000030 RID: 48
		private sealed class WhereListIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x06000190 RID: 400 RVA: 0x00008A9B File Offset: 0x00006C9B
			public WhereListIterator(List<TSource> source, Func<TSource, bool> predicate)
			{
				this._source = source;
				this._predicate = predicate;
			}

			// Token: 0x06000191 RID: 401 RVA: 0x00008AB1 File Offset: 0x00006CB1
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.WhereListIterator<TSource>(this._source, this._predicate);
			}

			// Token: 0x06000192 RID: 402 RVA: 0x00008AC4 File Offset: 0x00006CC4
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				for (int i = 0; i < this._source.Count; i++)
				{
					TSource tsource = this._source[i];
					checked
					{
						if (this._predicate(tsource))
						{
							num++;
						}
					}
				}
				return num;
			}

			// Token: 0x06000193 RID: 403 RVA: 0x00008B10 File Offset: 0x00006D10
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				while (this._enumerator.MoveNext())
				{
					TSource tsource = this._enumerator.Current;
					if (this._predicate(tsource))
					{
						this._current = tsource;
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06000194 RID: 404 RVA: 0x00008B7F File Offset: 0x00006D7F
			public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.WhereSelectListIterator<TSource, TResult>(this._source, this._predicate, selector);
			}

			// Token: 0x06000195 RID: 405 RVA: 0x00008B94 File Offset: 0x00006D94
			public TSource[] ToArray()
			{
				global::System.Collections.Generic.LargeArrayBuilder<TSource> largeArrayBuilder = new global::System.Collections.Generic.LargeArrayBuilder<TSource>(this._source.Count);
				for (int i = 0; i < this._source.Count; i++)
				{
					TSource tsource = this._source[i];
					if (this._predicate(tsource))
					{
						largeArrayBuilder.Add(tsource);
					}
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x06000196 RID: 406 RVA: 0x00008BF4 File Offset: 0x00006DF4
			public List<TSource> ToList()
			{
				List<TSource> list = new List<TSource>();
				for (int i = 0; i < this._source.Count; i++)
				{
					TSource tsource = this._source[i];
					if (this._predicate(tsource))
					{
						list.Add(tsource);
					}
				}
				return list;
			}

			// Token: 0x06000197 RID: 407 RVA: 0x00008C40 File Offset: 0x00006E40
			public override IEnumerable<TSource> Where(Func<TSource, bool> predicate)
			{
				return new Enumerable.WhereListIterator<TSource>(this._source, Utilities.CombinePredicates<TSource>(this._predicate, predicate));
			}

			// Token: 0x04000050 RID: 80
			private readonly List<TSource> _source;

			// Token: 0x04000051 RID: 81
			private readonly Func<TSource, bool> _predicate;

			// Token: 0x04000052 RID: 82
			private List<TSource>.Enumerator _enumerator;
		}

		// Token: 0x02000031 RID: 49
		private sealed class WhereSelectArrayIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x06000198 RID: 408 RVA: 0x00008C59 File Offset: 0x00006E59
			public WhereSelectArrayIterator(TSource[] source, Func<TSource, bool> predicate, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._predicate = predicate;
				this._selector = selector;
			}

			// Token: 0x06000199 RID: 409 RVA: 0x00008C76 File Offset: 0x00006E76
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.WhereSelectArrayIterator<TSource, TResult>(this._source, this._predicate, this._selector);
			}

			// Token: 0x0600019A RID: 410 RVA: 0x00008C90 File Offset: 0x00006E90
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				checked
				{
					foreach (TSource tsource in this._source)
					{
						if (this._predicate(tsource))
						{
							this._selector(tsource);
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x0600019B RID: 411 RVA: 0x00008CE4 File Offset: 0x00006EE4
			public override bool MoveNext()
			{
				int i = this._state - 1;
				TSource[] source = this._source;
				while (i < source.Length)
				{
					TSource tsource = source[i];
					int state = this._state;
					this._state = state + 1;
					i = state;
					if (this._predicate(tsource))
					{
						this._current = this._selector(tsource);
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x0600019C RID: 412 RVA: 0x00008D4C File Offset: 0x00006F4C
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.WhereSelectArrayIterator<TSource, TResult2>(this._source, this._predicate, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x0600019D RID: 413 RVA: 0x00008D6C File Offset: 0x00006F6C
			public TResult[] ToArray()
			{
				global::System.Collections.Generic.LargeArrayBuilder<TResult> largeArrayBuilder = new global::System.Collections.Generic.LargeArrayBuilder<TResult>(this._source.Length);
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						largeArrayBuilder.Add(this._selector(tsource));
					}
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x0600019E RID: 414 RVA: 0x00008DD0 File Offset: 0x00006FD0
			public List<TResult> ToList()
			{
				List<TResult> list = new List<TResult>();
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						list.Add(this._selector(tsource));
					}
				}
				return list;
			}

			// Token: 0x04000053 RID: 83
			private readonly TSource[] _source;

			// Token: 0x04000054 RID: 84
			private readonly Func<TSource, bool> _predicate;

			// Token: 0x04000055 RID: 85
			private readonly Func<TSource, TResult> _selector;
		}

		// Token: 0x02000032 RID: 50
		private sealed class WhereSelectListIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x0600019F RID: 415 RVA: 0x00008E21 File Offset: 0x00007021
			public WhereSelectListIterator(List<TSource> source, Func<TSource, bool> predicate, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._predicate = predicate;
				this._selector = selector;
			}

			// Token: 0x060001A0 RID: 416 RVA: 0x00008E3E File Offset: 0x0000703E
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.WhereSelectListIterator<TSource, TResult>(this._source, this._predicate, this._selector);
			}

			// Token: 0x060001A1 RID: 417 RVA: 0x00008E58 File Offset: 0x00007058
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				for (int i = 0; i < this._source.Count; i++)
				{
					TSource tsource = this._source[i];
					checked
					{
						if (this._predicate(tsource))
						{
							this._selector(tsource);
							num++;
						}
					}
				}
				return num;
			}

			// Token: 0x060001A2 RID: 418 RVA: 0x00008EB0 File Offset: 0x000070B0
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				while (this._enumerator.MoveNext())
				{
					TSource tsource = this._enumerator.Current;
					if (this._predicate(tsource))
					{
						this._current = this._selector(tsource);
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x060001A3 RID: 419 RVA: 0x00008F2A File Offset: 0x0000712A
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.WhereSelectListIterator<TSource, TResult2>(this._source, this._predicate, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x060001A4 RID: 420 RVA: 0x00008F4C File Offset: 0x0000714C
			public TResult[] ToArray()
			{
				global::System.Collections.Generic.LargeArrayBuilder<TResult> largeArrayBuilder = new global::System.Collections.Generic.LargeArrayBuilder<TResult>(this._source.Count);
				for (int i = 0; i < this._source.Count; i++)
				{
					TSource tsource = this._source[i];
					if (this._predicate(tsource))
					{
						largeArrayBuilder.Add(this._selector(tsource));
					}
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x060001A5 RID: 421 RVA: 0x00008FB8 File Offset: 0x000071B8
			public List<TResult> ToList()
			{
				List<TResult> list = new List<TResult>();
				for (int i = 0; i < this._source.Count; i++)
				{
					TSource tsource = this._source[i];
					if (this._predicate(tsource))
					{
						list.Add(this._selector(tsource));
					}
				}
				return list;
			}

			// Token: 0x04000056 RID: 86
			private readonly List<TSource> _source;

			// Token: 0x04000057 RID: 87
			private readonly Func<TSource, bool> _predicate;

			// Token: 0x04000058 RID: 88
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x04000059 RID: 89
			private List<TSource>.Enumerator _enumerator;
		}

		// Token: 0x02000033 RID: 51
		private sealed class WhereSelectEnumerableIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x060001A6 RID: 422 RVA: 0x0000900F File Offset: 0x0000720F
			public WhereSelectEnumerableIterator(IEnumerable<TSource> source, Func<TSource, bool> predicate, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._predicate = predicate;
				this._selector = selector;
			}

			// Token: 0x060001A7 RID: 423 RVA: 0x0000902C File Offset: 0x0000722C
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.WhereSelectEnumerableIterator<TSource, TResult>(this._source, this._predicate, this._selector);
			}

			// Token: 0x060001A8 RID: 424 RVA: 0x00009045 File Offset: 0x00007245
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x060001A9 RID: 425 RVA: 0x00009068 File Offset: 0x00007268
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				checked
				{
					foreach (TSource tsource in this._source)
					{
						if (this._predicate(tsource))
						{
							this._selector(tsource);
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x060001AA RID: 426 RVA: 0x000090D8 File Offset: 0x000072D8
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				while (this._enumerator.MoveNext())
				{
					TSource tsource = this._enumerator.Current;
					if (this._predicate(tsource))
					{
						this._current = this._selector(tsource);
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x060001AB RID: 427 RVA: 0x00009152 File Offset: 0x00007352
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.WhereSelectEnumerableIterator<TSource, TResult2>(this._source, this._predicate, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x060001AC RID: 428 RVA: 0x00009174 File Offset: 0x00007374
			public TResult[] ToArray()
			{
				global::System.Collections.Generic.LargeArrayBuilder<TResult> largeArrayBuilder = new global::System.Collections.Generic.LargeArrayBuilder<TResult>(true);
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						largeArrayBuilder.Add(this._selector(tsource));
					}
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x060001AD RID: 429 RVA: 0x000091EC File Offset: 0x000073EC
			public List<TResult> ToList()
			{
				List<TResult> list = new List<TResult>();
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						list.Add(this._selector(tsource));
					}
				}
				return list;
			}

			// Token: 0x0400005A RID: 90
			private readonly IEnumerable<TSource> _source;

			// Token: 0x0400005B RID: 91
			private readonly Func<TSource, bool> _predicate;

			// Token: 0x0400005C RID: 92
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x0400005D RID: 93
			private IEnumerator<TSource> _enumerator;
		}
	}
}
