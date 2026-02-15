using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents a 4-tuple, or quadruple. </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000157 RID: 343
	[Serializable]
	public class Tuple<T1, T2, T3, T4> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the current <see cref="T:System.Tuple`4" /> object's first component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`4" /> object's first component.</returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00032DF6 File Offset: 0x00030FF6
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`4" /> object's second component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`4" /> object's second component.</returns>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x00032DFE File Offset: 0x00030FFE
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`4" /> object's third component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`4" /> object's third component.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x00032E06 File Offset: 0x00031006
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`4" /> object's fourth component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`4" /> object's fourth component.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x00032E0E File Offset: 0x0003100E
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`4" /> class.</summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		/// <param name="item3">The value of the tuple's third component.</param>
		/// <param name="item4">The value of the tuple's fourth component</param>
		// Token: 0x06000B98 RID: 2968 RVA: 0x00032E16 File Offset: 0x00031016
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`4" /> object is equal to a specified object.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x06000B99 RID: 2969 RVA: 0x00032A43 File Offset: 0x00030C43
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`4" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false. </returns>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		// Token: 0x06000B9A RID: 2970 RVA: 0x00032E3C File Offset: 0x0003103C
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4> tuple = other as Tuple<T1, T2, T3, T4>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3)) && comparer.Equals(this.m_Item4, tuple.m_Item4);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`4" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="obj" />.ZeroThis instance and <paramref name="obj" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="obj" />.</returns>
		/// <param name="obj">An object to compare with the current instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`4" /> object.</exception>
		// Token: 0x06000B9B RID: 2971 RVA: 0x00032AAE File Offset: 0x00030CAE
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`4" /> object to a specified object by using a specified comparer and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="other" />.ZeroThis instance and <paramref name="other" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="other" />.</returns>
		/// <param name="other">An object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`4" /> object.</exception>
		// Token: 0x06000B9C RID: 2972 RVA: 0x00032ED4 File Offset: 0x000310D4
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4> tuple = other as Tuple<T1, T2, T3, T4>;
			if (tuple == null)
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			int num = comparer.Compare(this.m_Item1, tuple.m_Item1);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item2, tuple.m_Item2);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item3, tuple.m_Item3);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item4, tuple.m_Item4);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Tuple`4" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000B9D RID: 2973 RVA: 0x00032B38 File Offset: 0x00030D38
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`4" /> object by using a specified computation method.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" />  method calculates the hash code of the current <see cref="T:System.Tuple`4" /> object.</param>
		// Token: 0x06000B9E RID: 2974 RVA: 0x00032F94 File Offset: 0x00031194
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4));
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`4" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`4" /> object.</returns>
		// Token: 0x06000B9F RID: 2975 RVA: 0x00032FEC File Offset: 0x000311EC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00033010 File Offset: 0x00031210
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(", ");
			sb.Append(this.m_Item4);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00019A7F File Offset: 0x00017C7F
		int ITuple.Length
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x0400049E RID: 1182
		private readonly T1 m_Item1;

		// Token: 0x0400049F RID: 1183
		private readonly T2 m_Item2;

		// Token: 0x040004A0 RID: 1184
		private readonly T3 m_Item3;

		// Token: 0x040004A1 RID: 1185
		private readonly T4 m_Item4;
	}
}
