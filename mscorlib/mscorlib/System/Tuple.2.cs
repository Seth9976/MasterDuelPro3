using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents a 2-tuple, or pair. </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000155 RID: 341
	[Serializable]
	public class Tuple<T1, T2> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the current <see cref="T:System.Tuple`2" /> object's first component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`2" /> object's first component.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00032A1D File Offset: 0x00030C1D
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`2" /> object's second component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`2" /> object's second component.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00032A25 File Offset: 0x00030C25
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`2" /> class.</summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		// Token: 0x06000B7D RID: 2941 RVA: 0x00032A2D File Offset: 0x00030C2D
		public Tuple(T1 item1, T2 item2)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`2" /> object is equal to a specified object.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x06000B7E RID: 2942 RVA: 0x00032A43 File Offset: 0x00030C43
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`2" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		// Token: 0x06000B7F RID: 2943 RVA: 0x00032A54 File Offset: 0x00030C54
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2> tuple = other as Tuple<T1, T2>;
			return tuple != null && comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`2" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="obj" />.ZeroThis instance and <paramref name="obj" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="obj" />.</returns>
		/// <param name="obj">An object to compare with the current instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`2" /> object.</exception>
		// Token: 0x06000B80 RID: 2944 RVA: 0x00032AAE File Offset: 0x00030CAE
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`2" /> object to a specified object by using a specified comparer, and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="other" />.ZeroThis instance and <paramref name="other" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="other" />.</returns>
		/// <param name="other">An object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`2" /> object.</exception>
		// Token: 0x06000B81 RID: 2945 RVA: 0x00032ABC File Offset: 0x00030CBC
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2> tuple = other as Tuple<T1, T2>;
			if (tuple == null)
			{
				throw new ArgumentException(SR.Format("Argument must be of type {0}.", base.GetType().ToString()), "other");
			}
			int num = comparer.Compare(this.m_Item1, tuple.m_Item1);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item2, tuple.m_Item2);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Tuple`2" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000B82 RID: 2946 RVA: 0x00032B38 File Offset: 0x00030D38
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`2" /> object by using a specified computation method.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" />  method calculates the hash code of the current <see cref="T:System.Tuple`2" /> object.</param>
		// Token: 0x06000B83 RID: 2947 RVA: 0x00032B45 File Offset: 0x00030D45
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2));
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`2" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`2" /> object.</returns>
		// Token: 0x06000B84 RID: 2948 RVA: 0x00032B70 File Offset: 0x00030D70
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00032B94 File Offset: 0x00030D94
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0000F253 File Offset: 0x0000D453
		int ITuple.Length
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x04000499 RID: 1177
		private readonly T1 m_Item1;

		// Token: 0x0400049A RID: 1178
		private readonly T2 m_Item2;
	}
}
