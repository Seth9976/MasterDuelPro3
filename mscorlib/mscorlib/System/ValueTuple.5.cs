using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000167 RID: 359
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTuple<T1, T2, T3, T4> : IEquatable<ValueTuple<T1, T2, T3, T4>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4>>, IValueTupleInternal, ITuple
	{
		// Token: 0x06000CF7 RID: 3319 RVA: 0x000353CC File Offset: 0x000335CC
		public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000353EB File Offset: 0x000335EB
		public override bool Equals(object obj)
		{
			return obj is ValueTuple<T1, T2, T3, T4> && this.Equals((ValueTuple<T1, T2, T3, T4>)obj);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00035404 File Offset: 0x00033604
		public bool Equals(ValueTuple<T1, T2, T3, T4> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(this.Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(this.Item4, other.Item4);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00035474 File Offset: 0x00033674
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is ValueTuple<T1, T2, T3, T4>))
			{
				return false;
			}
			ValueTuple<T1, T2, T3, T4> valueTuple = (ValueTuple<T1, T2, T3, T4>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2) && comparer.Equals(this.Item3, valueTuple.Item3) && comparer.Equals(this.Item4, valueTuple.Item4);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00035510 File Offset: 0x00033710
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3, T4>))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return this.CompareTo((ValueTuple<T1, T2, T3, T4>)other);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00035560 File Offset: 0x00033760
		public int CompareTo(ValueTuple<T1, T2, T3, T4> other)
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
			num = Comparer<T3>.Default.Compare(this.Item3, other.Item3);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T4>.Default.Compare(this.Item4, other.Item4);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000355D8 File Offset: 0x000337D8
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3, T4>))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			ValueTuple<T1, T2, T3, T4> valueTuple = (ValueTuple<T1, T2, T3, T4>)other;
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
			num = comparer.Compare(this.Item3, valueTuple.Item3);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item4, valueTuple.Item4);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x000356A8 File Offset: 0x000338A8
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
			ref T4 ptr4 = ref this.Item4;
			T4 t4 = default(T4);
			int num4;
			if (t4 == null)
			{
				t4 = this.Item4;
				ptr4 = ref t4;
				if (t4 == null)
				{
					num4 = 0;
					goto IL_00D4;
				}
			}
			num4 = ptr4.GetHashCode();
			IL_00D4:
			return ValueTuple.CombineHashCodes(num, num2, num3, num4);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0003578E File Offset: 0x0003398E
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00035798 File Offset: 0x00033998
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4));
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0003578E File Offset: 0x0003398E
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x000357F0 File Offset: 0x000339F0
		public override string ToString()
		{
			string[] array = new string[9];
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
					goto IL_0046;
				}
			}
			text = ptr.ToString();
			IL_0046:
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
					goto IL_0086;
				}
			}
			text2 = ptr2.ToString();
			IL_0086:
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
					goto IL_00C6;
				}
			}
			text3 = ptr3.ToString();
			IL_00C6:
			array[num3] = text3;
			array[6] = ", ";
			int num4 = 7;
			ref T4 ptr4 = ref this.Item4;
			T4 t4 = default(T4);
			string text4;
			if (t4 == null)
			{
				t4 = this.Item4;
				ptr4 = ref t4;
				if (t4 == null)
				{
					text4 = null;
					goto IL_0106;
				}
			}
			text4 = ptr4.ToString();
			IL_0106:
			array[num4] = text4;
			array[8] = ")";
			return string.Concat(array);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00035914 File Offset: 0x00033B14
		string IValueTupleInternal.ToStringEnd()
		{
			string[] array = new string[8];
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
			array[5] = ", ";
			int num4 = 6;
			ref T4 ptr4 = ref this.Item4;
			T4 t4 = default(T4);
			string text4;
			if (t4 == null)
			{
				t4 = this.Item4;
				ptr4 = ref t4;
				if (t4 == null)
				{
					text4 = null;
					goto IL_00FD;
				}
			}
			text4 = ptr4.ToString();
			IL_00FD:
			array[num4] = text4;
			array[7] = ")";
			return string.Concat(array);
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00019A7F File Offset: 0x00017C7F
		int ITuple.Length
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x040004C9 RID: 1225
		public T1 Item1;

		// Token: 0x040004CA RID: 1226
		public T2 Item2;

		// Token: 0x040004CB RID: 1227
		public T3 Item3;

		// Token: 0x040004CC RID: 1228
		public T4 Item4;
	}
}
