using System;

namespace System.Collections
{
	/// <summary>Provides the abstract base class for a strongly typed non-generic read-only collection.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200070D RID: 1805
	[Serializable]
	public abstract class ReadOnlyCollectionBase : ICollection, IEnumerable
	{
		/// <summary>Gets the list of elements contained in the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> representing the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance itself.</returns>
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x000DCDC1 File Offset: 0x000DAFC1
		protected ArrayList InnerList
		{
			get
			{
				if (this._list == null)
				{
					this._list = new ArrayList();
				}
				return this._list;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.Retrieving the value of this property is an O(1) operation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06003891 RID: 14481 RVA: 0x000DCDDC File Offset: 0x000DAFDC
		public virtual int Count
		{
			get
			{
				return this.InnerList.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to a <see cref="T:System.Collections.ReadOnlyCollectionBase" /> object is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> object is synchronized (thread safe); otherwise, false. The default is false.</returns>
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06003892 RID: 14482 RVA: 0x000DCDE9 File Offset: 0x000DAFE9
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerList.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to a <see cref="T:System.Collections.ReadOnlyCollectionBase" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> object.</returns>
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06003893 RID: 14483 RVA: 0x000DCDF6 File Offset: 0x000DAFF6
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerList.SyncRoot;
			}
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.ReadOnlyCollectionBase" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ReadOnlyCollectionBase" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ReadOnlyCollectionBase" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ReadOnlyCollectionBase" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
		// Token: 0x06003894 RID: 14484 RVA: 0x000DCE03 File Offset: 0x000DB003
		void ICollection.CopyTo(Array array, int index)
		{
			this.InnerList.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.ReadOnlyCollectionBase" /> instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003895 RID: 14485 RVA: 0x000DCE12 File Offset: 0x000DB012
		public virtual IEnumerator GetEnumerator()
		{
			return this.InnerList.GetEnumerator();
		}

		// Token: 0x04001E6B RID: 7787
		private ArrayList _list;
	}
}
