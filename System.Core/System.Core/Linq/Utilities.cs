using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000055 RID: 85
	internal static class Utilities
	{
		// Token: 0x06000295 RID: 661 RVA: 0x0000B86C File Offset: 0x00009A6C
		public static bool AreEqualityComparersEqual<TSource>(IEqualityComparer<TSource> left, IEqualityComparer<TSource> right)
		{
			if (left == right)
			{
				return true;
			}
			EqualityComparer<TSource> @default = EqualityComparer<TSource>.Default;
			if (left == null)
			{
				return right == @default || right.Equals(@default);
			}
			if (right == null)
			{
				return left == @default || left.Equals(@default);
			}
			return left.Equals(right);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000B8AE File Offset: 0x00009AAE
		public static Func<TSource, bool> CombinePredicates<TSource>(Func<TSource, bool> predicate1, Func<TSource, bool> predicate2)
		{
			return (TSource x) => predicate1(x) && predicate2(x);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000B8CE File Offset: 0x00009ACE
		public static Func<TSource, TResult> CombineSelectors<TSource, TMiddle, TResult>(Func<TSource, TMiddle> selector1, Func<TMiddle, TResult> selector2)
		{
			return (TSource x) => selector2(selector1(x));
		}
	}
}
