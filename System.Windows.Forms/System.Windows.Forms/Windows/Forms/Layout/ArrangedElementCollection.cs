using System;
using System.Collections;

namespace System.Windows.Forms.Layout
{
	/// <summary>Represents a collection of objects.</summary>
	// Token: 0x02000391 RID: 913
	public class ArrangedElementCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06001D84 RID: 7556 RVA: 0x000916A9 File Offset: 0x0008F8A9
		internal ArrangedElementCollection()
		{
			this.list = new ArrayList();
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements currently contained in the collection.</returns>
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x000916BC File Offset: 0x0008F8BC
		public virtual int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x000916C9 File Offset: 0x0008F8C9
		public virtual bool IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		/// <summary>Copies the entire contents of this collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or-The number of elements in the source collection is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source element cannot be cast automatically to the type of <paramref name="array" />.</exception>
		// Token: 0x06001D87 RID: 7559 RVA: 0x000916D6 File Offset: 0x0008F8D6
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Determines whether two <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> instances are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> is equal to the current <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> to compare with the current <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />.</param>
		// Token: 0x06001D88 RID: 7560 RVA: 0x000916E5 File Offset: 0x0008F8E5
		public override bool Equals(object obj)
		{
			return obj is ArrangedElementCollection && this == obj;
		}

		/// <summary>Returns an enumerator for the entire collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire collection.</returns>
		// Token: 0x06001D89 RID: 7561 RVA: 0x000916F6 File Offset: 0x0008F8F6
		public virtual IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />.</returns>
		// Token: 0x06001D8A RID: 7562 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Add(System.Object)" /> method.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06001D8B RID: 7563 RVA: 0x00091703 File Offset: 0x0008F903
		int IList.Add(object value)
		{
			return this.Add(value);
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0009170C File Offset: 0x0008F90C
		internal int Add(object value)
		{
			return this.list.Add(value);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Clear" /> method.</summary>
		// Token: 0x06001D8D RID: 7565 RVA: 0x0009171A File Offset: 0x0008F91A
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x00091722 File Offset: 0x0008F922
		internal void Clear()
		{
			this.list.Clear();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Contains(System.Object)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06001D8F RID: 7567 RVA: 0x00065693 File Offset: 0x00063893
		bool IList.Contains(object value)
		{
			return this.Contains(value);
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x000103E5 File Offset: 0x0000E5E5
		internal bool Contains(object value)
		{
			return this.list.Contains(value);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.IndexOf(System.Object)" /> method.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06001D91 RID: 7569 RVA: 0x000656A6 File Offset: 0x000638A6
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value);
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x000104D4 File Offset: 0x0000E6D4
		internal int IndexOf(object value)
		{
			return this.list.IndexOf(value);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06001D93 RID: 7571 RVA: 0x00022B0C File Offset: 0x00020D0C
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0009172F File Offset: 0x0008F92F
		internal void Insert(int index, object value)
		{
			this.list.Insert(index, value);
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.IList.IsFixedSize" /> property.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0006588D File Offset: 0x00063A8D
		bool IList.IsFixedSize
		{
			get
			{
				return this.IsFixedSize;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x0009173E File Offset: 0x0008F93E
		internal bool IsFixedSize
		{
			get
			{
				return this.list.IsFixedSize;
			}
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06001D97 RID: 7575 RVA: 0x0009174B File Offset: 0x0008F94B
		void IList.Remove(object value)
		{
			this.Remove(value);
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00091754 File Offset: 0x0008F954
		internal void Remove(object value)
		{
			this.list.Remove(value);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" /> method.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		// Token: 0x06001D99 RID: 7577 RVA: 0x00091762 File Offset: 0x0008F962
		void IList.RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.IList.Item(System.Int32)" /> property.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		// Token: 0x1700076B RID: 1899
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = value;
			}
		}

		// Token: 0x1700076C RID: 1900
		internal object this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.ICollection.IsSynchronized" /> property.</summary>
		/// <returns>true if access to the <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x000917A0 File Offset: 0x0008F9A0
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>For a description of this member, see the <see cref="P:System.Collections.ICollection.SyncRoot" /> property.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.Layout.ArrangedElementCollection" />.</returns>
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x000917AD File Offset: 0x0008F9AD
		object ICollection.SyncRoot
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		// Token: 0x04001CD7 RID: 7383
		internal ArrayList list;
	}
}
