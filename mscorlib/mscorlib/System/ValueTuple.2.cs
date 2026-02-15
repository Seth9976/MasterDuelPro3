using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000164 RID: 356
	[Serializable]
	public struct ValueTuple<T1> : IEquatable<ValueTuple<T1>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1>>, IValueTupleInternal, ITuple
	{
		// Token: 0x06000CCE RID: 3278 RVA: 0x000348C2 File Offset: 0x00032AC2
		public ValueTuple(T1 item1)
		{
			this.Item1 = item1;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x000348CB File Offset: 0x00032ACB
		public override bool Equals(object obj)
		{
			return obj is ValueTuple<T1> && this.Equals((ValueTuple<T1>)obj);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000348E3 File Offset: 0x00032AE3
		public bool Equals(ValueTuple<T1> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x000348FC File Offset: 0x00032AFC
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is ValueTuple<T1>))
			{
				return false;
			}
			ValueTuple<T1> valueTuple = (ValueTuple<T1>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0003493C File Offset: 0x00032B3C
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1>))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			ValueTuple<T1> valueTuple = (ValueTuple<T1>)other;
			return Comparer<T1>.Default.Compare(this.Item1, valueTuple.Item1);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0003499D File Offset: 0x00032B9D
		public int CompareTo(ValueTuple<T1> other)
		{
			return Comparer<T1>.Default.Compare(this.Item1, other.Item1);
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x000349B8 File Offset: 0x00032BB8
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1>))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			ValueTuple<T1> valueTuple = (ValueTuple<T1>)other;
			return comparer.Compare(this.Item1, valueTuple.Item1);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00034A20 File Offset: 0x00032C20
		public override int GetHashCode()
		{
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					return 0;
				}
			}
			return ptr.GetHashCode();
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00034A61 File Offset: 0x00032C61
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.Item1);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00034A61 File Offset: 0x00032C61
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.Item1);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00034A74 File Offset: 0x00032C74
		public override string ToString()
		{
			string text = "(";
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			string text2;
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					text2 = null;
					goto IL_003A;
				}
			}
			text2 = ptr.ToString();
			IL_003A:
			return text + text2 + ")";
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00034AC8 File Offset: 0x00032CC8
		string IValueTupleInternal.ToStringEnd()
		{
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			string text;
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					text = null;
					goto IL_0035;
				}
			}
			text = ptr.ToString();
			IL_0035:
			return text + ")";
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0000C091 File Offset: 0x0000A291
		int ITuple.Length
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x040004C3 RID: 1219
		public T1 Item1;
	}
}
