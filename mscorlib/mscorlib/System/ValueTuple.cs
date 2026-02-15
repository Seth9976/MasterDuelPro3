using System;
using System.Collections;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000163 RID: 355
	[Serializable]
	public struct ValueTuple : IEquatable<ValueTuple>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple>, IValueTupleInternal, ITuple
	{
		// Token: 0x06000CBA RID: 3258 RVA: 0x000347E0 File Offset: 0x000329E0
		public override bool Equals(object obj)
		{
			return obj is ValueTuple;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0000C091 File Offset: 0x0000A291
		public bool Equals(ValueTuple other)
		{
			return true;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000347E0 File Offset: 0x000329E0
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			return other is ValueTuple;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x000347EB File Offset: 0x000329EB
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return 0;
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00033991 File Offset: 0x00031B91
		public int CompareTo(ValueTuple other)
		{
			return 0;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x000347EB File Offset: 0x000329EB
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return 0;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00033991 File Offset: 0x00031B91
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00033991 File Offset: 0x00031B91
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00033991 File Offset: 0x00031B91
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00034825 File Offset: 0x00032A25
		public override string ToString()
		{
			return "()";
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0003482C File Offset: 0x00032A2C
		string IValueTupleInternal.ToStringEnd()
		{
			return ")";
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x00033991 File Offset: 0x00031B91
		int ITuple.Length
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00034833 File Offset: 0x00032A33
		public static ValueTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new ValueTuple<T1, T2>(item1, item2);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0003483C File Offset: 0x00032A3C
		internal static int CombineHashCodes(int h1, int h2)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(global::System.Numerics.Hashing.HashHelpers.Combine(global::System.Numerics.Hashing.HashHelpers.RandomSeed, h1), h2);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0003484F File Offset: 0x00032A4F
		internal static int CombineHashCodes(int h1, int h2, int h3)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0003485E File Offset: 0x00032A5E
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3), h4);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0003486E File Offset: 0x00032A6E
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4), h5);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00034880 File Offset: 0x00032A80
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5), h6);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00034894 File Offset: 0x00032A94
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6), h7);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x000348AA File Offset: 0x00032AAA
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
		{
			return global::System.Numerics.Hashing.HashHelpers.Combine(ValueTuple.CombineHashCodes(h1, h2, h3, h4, h5, h6, h7), h8);
		}
	}
}
