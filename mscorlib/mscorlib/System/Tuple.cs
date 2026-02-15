using System;

namespace System
{
	/// <summary>Provides static methods for creating tuple objects. </summary>
	// Token: 0x02000154 RID: 340
	public static class Tuple
	{
		/// <summary>Creates a new 2-tuple, or pair.</summary>
		/// <returns>A 2-tuple whose value is (<paramref name="item1" />, <paramref name="item2" />).</returns>
		/// <param name="item1">The value of the first component of the tuple.</param>
		/// <param name="item2">The value of the second component of the tuple.</param>
		/// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
		// Token: 0x06000B75 RID: 2933 RVA: 0x000329DB File Offset: 0x00030BDB
		public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new Tuple<T1, T2>(item1, item2);
		}

		/// <summary>Creates a new 3-tuple, or triple.</summary>
		/// <returns>A 3-tuple whose value is (<paramref name="item1" />, <paramref name="item2" />, <paramref name="item3" />).</returns>
		/// <param name="item1">The value of the first component of the tuple.</param>
		/// <param name="item2">The value of the second component of the tuple.</param>
		/// <param name="item3">The value of the third component of the tuple.</param>
		/// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third component of the tuple.</typeparam>
		// Token: 0x06000B76 RID: 2934 RVA: 0x000329E4 File Offset: 0x00030BE4
		public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return new Tuple<T1, T2, T3>(item1, item2, item3);
		}

		/// <summary>Creates a new 4-tuple, or quadruple.</summary>
		/// <returns>A 4-tuple whose value is (<paramref name="item1" />, <paramref name="item2" />, <paramref name="item3" />, <paramref name="item4" />).</returns>
		/// <param name="item1">The value of the first component of the tuple.</param>
		/// <param name="item2">The value of the second component of the tuple.</param>
		/// <param name="item3">The value of the third component of the tuple.</param>
		/// <param name="item4">The value of the fourth component of the tuple.</param>
		/// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third component of the tuple.</typeparam>
		/// <typeparam name="T4">The type of the fourth component of the tuple.  </typeparam>
		// Token: 0x06000B77 RID: 2935 RVA: 0x000329EE File Offset: 0x00030BEE
		public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00028E9F File Offset: 0x0002709F
		internal static int CombineHashCodes(int h1, int h2)
		{
			return ((h1 << 5) + h1) ^ h2;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000329F9 File Offset: 0x00030BF9
		internal static int CombineHashCodes(int h1, int h2, int h3)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00032A08 File Offset: 0x00030C08
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2), Tuple.CombineHashCodes(h3, h4));
		}
	}
}
