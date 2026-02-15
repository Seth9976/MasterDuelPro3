using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000166 RID: 358
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTuple<T1, T2, T3> : IEquatable<ValueTuple<T1, T2, T3>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3>>, IValueTupleInternal, ITuple
	{
		// Token: 0x06000CE9 RID: 3305 RVA: 0x00034EC2 File Offset: 0x000330C2
		public ValueTuple(T1 item1, T2 item2, T3 item3)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00034ED9 File Offset: 0x000330D9
		public override bool Equals(object obj)
		{
			return obj is ValueTuple<T1, T2, T3> && this.Equals((ValueTuple<T1, T2, T3>)obj);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00034EF4 File Offset: 0x000330F4
		public bool Equals(ValueTuple<T1, T2, T3> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(this.Item3, other.Item3);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00034F4C File Offset: 0x0003314C
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is ValueTuple<T1, T2, T3>))
			{
				return false;
			}
			ValueTuple<T1, T2, T3> valueTuple = (ValueTuple<T1, T2, T3>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2) && comparer.Equals(this.Item3, valueTuple.Item3);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00034FC8 File Offset: 0x000331C8
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3>))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return this.CompareTo((ValueTuple<T1, T2, T3>)other);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00035018 File Offset: 0x00033218
		public int CompareTo(ValueTuple<T1, T2, T3> other)
		{
			int num = Comparer<T1>.Default.Compare(this.Item1, other.Item1);
			if (num != 0)
			{
				return num;
			}
			num = Comparer<T2>.Default.Compare(this.Item2, other.Item2);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T3>.Default.Compare(this.Item3, other.Item3);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00035074 File Offset: 0x00033274
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3>))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			ValueTuple<T1, T2, T3> valueTuple = (ValueTuple<T1, T2, T3>)other;
			int num = comparer.Compare(this.Item1, valueTuple.Item1);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item2, valueTuple.Item2);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item3, valueTuple.Item3);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00035120 File Offset: 0x00033320
		public override int GetHashCode()
		{
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			int num;
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					num = 0;
					goto IL_0035;
				}
			}
			num = ptr.GetHashCode();
			IL_0035:
			ref T2 ptr2 = ref this.Item2;
			T2 t2 = default(T2);
			int num2;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr2 = ref t2;
				if (t2 == null)
				{
					num2 = 0;
					goto IL_006A;
				}
			}
			num2 = ptr2.GetHashCode();
			IL_006A:
			ref T3 ptr3 = ref this.Item3;
			T3 t3 = default(T3);
			int num3;
			if (t3 == null)
			{
				t3 = this.Item3;
				ptr3 = ref t3;
				if (t3 == null)
				{
					num3 = 0;
					goto IL_009F;
				}
			}
			num3 = ptr3.GetHashCode();
			IL_009F:
			return ValueTuple.CombineHashCodes(num, num2, num3);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x000351D1 File Offset: 0x000333D1
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x000351DA File Offset: 0x000333DA
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3));
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x000351D1 File Offset: 0x000333D1
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00035214 File Offset: 0x00033414
		public override string ToString()
		{
			string[] array = new string[7];
			array[0] = "(";
			int num = 1;
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
					goto IL_0045;
				}
			}
			text = ptr.ToString();
			IL_0045:
			array[num] = text;
			array[2] = ", ";
			int num2 = 3;
			ref T2 ptr2 = ref this.Item2;
			T2 t2 = default(T2);
			string text2;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr2 = ref t2;
				if (t2 == null)
				{
					text2 = null;
					goto IL_0085;
				}
			}
			text2 = ptr2.ToString();
			IL_0085:
			array[num2] = text2;
			array[4] = ", ";
			int num3 = 5;
			ref T3 ptr3 = ref this.Item3;
			T3 t3 = default(T3);
			string text3;
			if (t3 == null)
			{
				t3 = this.Item3;
				ptr3 = ref t3;
				if (t3 == null)
				{
					text3 = null;
					goto IL_00C5;
				}
			}
			text3 = ptr3.ToString();
			IL_00C5:
			array[num3] = text3;
			array[6] = ")";
			return string.Concat(array);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x000352F4 File Offset: 0x000334F4
		string IValueTupleInternal.ToStringEnd()
		{
			string[] array = new string[6];
			int num = 0;
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
					goto IL_003D;
				}
			}
			text = ptr.ToString();
			IL_003D:
			array[num] = text;
			array[1] = ", ";
			int num2 = 2;
			ref T2 ptr2 = ref this.Item2;
			T2 t2 = default(T2);
			string text2;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr2 = ref t2;
				if (t2 == null)
				{
					text2 = null;
					goto IL_007D;
				}
			}
			text2 = ptr2.ToString();
			IL_007D:
			array[num2] = text2;
			array[3] = ", ";
			int num3 = 4;
			ref T3 ptr3 = ref this.Item3;
			T3 t3 = default(T3);
			string text3;
			if (t3 == null)
			{
				t3 = this.Item3;
				ptr3 = ref t3;
				if (t3 == null)
				{
					text3 = null;
					goto IL_00BD;
				}
			}
			text3 = ptr3.ToString();
			IL_00BD:
			array[num3] = text3;
			array[5] = ")";
			return string.Concat(array);
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x000194AE File Offset: 0x000176AE
		int ITuple.Length
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x040004C6 RID: 1222
		public T1 Item1;

		// Token: 0x040004C7 RID: 1223
		public T2 Item2;

		// Token: 0x040004C8 RID: 1224
		public T3 Item3;
	}
}
