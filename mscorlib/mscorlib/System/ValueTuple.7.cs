using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000169 RID: 361
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTuple<T1, T2, T3, T4, T5, T6> : IEquatable<ValueTuple<T1, T2, T3, T4, T5, T6>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4, T5, T6>>, IValueTupleInternal, ITuple
	{
		// Token: 0x06000D13 RID: 3347 RVA: 0x000361D5 File Offset: 0x000343D5
		public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
			this.Item6 = item6;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00036204 File Offset: 0x00034404
		public override bool Equals(object obj)
		{
			return obj is ValueTuple<T1, T2, T3, T4, T5, T6> && this.Equals((ValueTuple<T1, T2, T3, T4, T5, T6>)obj);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0003621C File Offset: 0x0003441C
		public bool Equals(ValueTuple<T1, T2, T3, T4, T5, T6> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(this.Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(this.Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(this.Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(this.Item6, other.Item6);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x000362BC File Offset: 0x000344BC
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is ValueTuple<T1, T2, T3, T4, T5, T6>))
			{
				return false;
			}
			ValueTuple<T1, T2, T3, T4, T5, T6> valueTuple = (ValueTuple<T1, T2, T3, T4, T5, T6>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2) && comparer.Equals(this.Item3, valueTuple.Item3) && comparer.Equals(this.Item4, valueTuple.Item4) && comparer.Equals(this.Item5, valueTuple.Item5) && comparer.Equals(this.Item6, valueTuple.Item6);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00036394 File Offset: 0x00034594
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6>))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			return this.CompareTo((ValueTuple<T1, T2, T3, T4, T5, T6>)other);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x000363E4 File Offset: 0x000345E4
		public int CompareTo(ValueTuple<T1, T2, T3, T4, T5, T6> other)
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
			num = Comparer<T4>.Default.Compare(this.Item4, other.Item4);
			if (num != 0)
			{
				return num;
			}
			num = Comparer<T5>.Default.Compare(this.Item5, other.Item5);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T6>.Default.Compare(this.Item6, other.Item6);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00036494 File Offset: 0x00034694
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6>))
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			ValueTuple<T1, T2, T3, T4, T5, T6> valueTuple = (ValueTuple<T1, T2, T3, T4, T5, T6>)other;
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
			num = comparer.Compare(this.Item4, valueTuple.Item4);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item5, valueTuple.Item5);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item6, valueTuple.Item6);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x000365A8 File Offset: 0x000347A8
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
			ref T5 ptr5 = ref this.Item5;
			T5 t5 = default(T5);
			int num5;
			if (t5 == null)
			{
				t5 = this.Item5;
				ptr5 = ref t5;
				if (t5 == null)
				{
					num5 = 0;
					goto IL_010C;
				}
			}
			num5 = ptr5.GetHashCode();
			IL_010C:
			ref T6 ptr6 = ref this.Item6;
			T6 t6 = default(T6);
			int num6;
			if (t6 == null)
			{
				t6 = this.Item6;
				ptr6 = ref t6;
				if (t6 == null)
				{
					num6 = 0;
					goto IL_0144;
				}
			}
			num6 = ptr6.GetHashCode();
			IL_0144:
			return ValueTuple.CombineHashCodes(num, num2, num3, num4, num5, num6);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x000366FE File Offset: 0x000348FE
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00036708 File Offset: 0x00034908
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return ValueTuple.CombineHashCodes(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6));
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x000366FE File Offset: 0x000348FE
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00036780 File Offset: 0x00034980
		public override string ToString()
		{
			string[] array = new string[13];
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
			array[8] = ", ";
			int num5 = 9;
			ref T5 ptr5 = ref this.Item5;
			T5 t5 = default(T5);
			string text5;
			if (t5 == null)
			{
				t5 = this.Item5;
				ptr5 = ref t5;
				if (t5 == null)
				{
					text5 = null;
					goto IL_014A;
				}
			}
			text5 = ptr5.ToString();
			IL_014A:
			array[num5] = text5;
			array[10] = ", ";
			int num6 = 11;
			ref T6 ptr6 = ref this.Item6;
			T6 t6 = default(T6);
			string text6;
			if (t6 == null)
			{
				t6 = this.Item6;
				ptr6 = ref t6;
				if (t6 == null)
				{
					text6 = null;
					goto IL_018F;
				}
			}
			text6 = ptr6.ToString();
			IL_018F:
			array[num6] = text6;
			array[12] = ")";
			return string.Concat(array);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0003692C File Offset: 0x00034B2C
		string IValueTupleInternal.ToStringEnd()
		{
			string[] array = new string[12];
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
					goto IL_003E;
				}
			}
			text = ptr.ToString();
			IL_003E:
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
					goto IL_007E;
				}
			}
			text2 = ptr2.ToString();
			IL_007E:
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
					goto IL_00BE;
				}
			}
			text3 = ptr3.ToString();
			IL_00BE:
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
					goto IL_00FE;
				}
			}
			text4 = ptr4.ToString();
			IL_00FE:
			array[num4] = text4;
			array[7] = ", ";
			int num5 = 8;
			ref T5 ptr5 = ref this.Item5;
			T5 t5 = default(T5);
			string text5;
			if (t5 == null)
			{
				t5 = this.Item5;
				ptr5 = ref t5;
				if (t5 == null)
				{
					text5 = null;
					goto IL_0141;
				}
			}
			text5 = ptr5.ToString();
			IL_0141:
			array[num5] = text5;
			array[9] = ", ";
			int num6 = 10;
			ref T6 ptr6 = ref this.Item6;
			T6 t6 = default(T6);
			string text6;
			if (t6 == null)
			{
				t6 = this.Item6;
				ptr6 = ref t6;
				if (t6 == null)
				{
					text6 = null;
					goto IL_0186;
				}
			}
			text6 = ptr6.ToString();
			IL_0186:
			array[num6] = text6;
			array[11] = ")";
			return string.Concat(array);
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00019723 File Offset: 0x00017923
		int ITuple.Length
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x040004D2 RID: 1234
		public T1 Item1;

		// Token: 0x040004D3 RID: 1235
		public T2 Item2;

		// Token: 0x040004D4 RID: 1236
		public T3 Item3;

		// Token: 0x040004D5 RID: 1237
		public T4 Item4;

		// Token: 0x040004D6 RID: 1238
		public T5 Item5;

		// Token: 0x040004D7 RID: 1239
		public T6 Item6;
	}
}
