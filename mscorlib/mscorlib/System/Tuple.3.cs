using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	/// <summary>Represents a 3-tuple, or triple. </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000156 RID: 342
	[Serializable]
	public class Tuple<T1, T2, T3> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		/// <summary>Gets the value of the current <see cref="T:System.Tuple`3" /> object's first component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`3" /> object's first component.</returns>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x00032BE0 File Offset: 0x00030DE0
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`3" /> object's second component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`3" /> object's second component.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x00032BE8 File Offset: 0x00030DE8
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Tuple`3" /> object's third component.</summary>
		/// <returns>The value of the current <see cref="T:System.Tuple`3" /> object's third component.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x00032BF0 File Offset: 0x00030DF0
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Tuple`3" /> class.</summary>
		/// <param name="item1">The value of the tuple's first component.</param>
		/// <param name="item2">The value of the tuple's second component.</param>
		/// <param name="item3">The value of the tuple's third component.</param>
		// Token: 0x06000B8A RID: 2954 RVA: 0x00032BF8 File Offset: 0x00030DF8
		public Tuple(T1 item1, T2 item2, T3 item3)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`3" /> object is equal to a specified object.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x06000B8B RID: 2955 RVA: 0x00032A43 File Offset: 0x00030C43
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		/// <summary>Returns a value that indicates whether the current <see cref="T:System.Tuple`3" /> object is equal to a specified object based on a specified comparison method.</summary>
		/// <returns>true if the current instance is equal to the specified object; otherwise, false.</returns>
		/// <param name="other">The object to compare with this instance.</param>
		/// <param name="comparer">An object that defines the method to use to evaluate whether the two objects are equal.</param>
		// Token: 0x06000B8C RID: 2956 RVA: 0x00032C18 File Offset: 0x00030E18
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2)) && comparer.Equals(this.m_Item3, tuple.m_Item3);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`3" /> object to a specified object and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="obj" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="obj" />.ZeroThis instance and <paramref name="obj" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="obj" />.</returns>
		/// <param name="obj">An object to compare with the current instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Tuple`3" /> object.</exception>
		// Token: 0x06000B8D RID: 2957 RVA: 0x00032AAE File Offset: 0x00030CAE
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		/// <summary>Compares the current <see cref="T:System.Tuple`3" /> object to a specified object by using a specified comparer, and returns an integer that indicates whether the current object is before, after, or in the same position as the specified object in the sort order.</summary>
		/// <returns>A signed integer that indicates the relative position of this instance and <paramref name="other" /> in the sort order, as shown in the following table.ValueDescriptionA negative integerThis instance precedes <paramref name="other" />.ZeroThis instance and <paramref name="other" /> have the same position in the sort order.A positive integerThis instance follows <paramref name="other" />.</returns>
		/// <param name="other">An object to compare with the current instance.</param>
		/// <param name="comparer">An object that provides custom rules for comparison.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="other" /> is not a <see cref="T:System.Tuple`3" /> object.</exception>
		// Token: 0x06000B8E RID: 2958 RVA: 0x00032C90 File Offset: 0x00030E90
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
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
			return comparer.Compare(this.m_Item3, tuple.m_Item3);
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Tuple`3" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000B8F RID: 2959 RVA: 0x00032B38 File Offset: 0x00030D38
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		/// <summary>Calculates the hash code for the current <see cref="T:System.Tuple`3" /> object by using a specified computation method.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <param name="comparer">An object whose <see cref="M:System.Collections.IEqualityComparer.GetHashCode(System.Object)" />  method calculates the hash code of the current <see cref="T:System.Tuple`3" /> object.</param>
		// Token: 0x06000B90 RID: 2960 RVA: 0x00032D2E File Offset: 0x00030F2E
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3));
		}

		/// <summary>Returns a string that represents the value of this <see cref="T:System.Tuple`3" /> instance.</summary>
		/// <returns>The string representation of this <see cref="T:System.Tuple`3" /> object.</returns>
		// Token: 0x06000B91 RID: 2961 RVA: 0x00032D68 File Offset: 0x00030F68
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00032D8C File Offset: 0x00030F8C
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x000194AE File Offset: 0x000176AE
		int ITuple.Length
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x0400049B RID: 1179
		private readonly T1 m_Item1;

		// Token: 0x0400049C RID: 1180
		private readonly T2 m_Item2;

		// Token: 0x0400049D RID: 1181
		private readonly T3 m_Item3;
	}
}
