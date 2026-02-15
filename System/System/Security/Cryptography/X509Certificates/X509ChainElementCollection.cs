using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020001C7 RID: 455
	public sealed class X509ChainElementCollection : ICollection, IEnumerable
	{
		// Token: 0x06000AEE RID: 2798 RVA: 0x0003764E File Offset: 0x0003584E
		internal X509ChainElementCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>An integer representing the number of elements in the collection.</returns>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00037661 File Offset: 0x00035861
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection of chain elements is synchronized.</summary>
		/// <returns>Always returns false.</returns>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0003766E File Offset: 0x0003586E
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> object at the specified index.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> object.</returns>
		/// <param name="index">An integer value. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is greater than or equal to the length of the collection. </exception>
		// Token: 0x17000202 RID: 514
		public X509ChainElement this[int index]
		{
			get
			{
				return (X509ChainElement)this._list[index];
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object.</summary>
		/// <returns>A pointer reference to the current object.</returns>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0003768E File Offset: 0x0003588E
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		/// <summary>Copies an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object into an array, starting at the specified index.</summary>
		/// <param name="array">An array to copy the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object to.</param>
		/// <param name="index">The index of <paramref name="array" /> at which to start copying.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is less than zero, or greater than or equal to the length of the array. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> plus the current count is greater than the length of the array. </exception>
		// Token: 0x06000AF3 RID: 2803 RVA: 0x0003769B File Offset: 0x0003589B
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementEnumerator" /> object that can be used to navigate through a collection of chain elements.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementEnumerator" /> object.</returns>
		// Token: 0x06000AF4 RID: 2804 RVA: 0x000376AA File Offset: 0x000358AA
		public X509ChainElementEnumerator GetEnumerator()
		{
			return new X509ChainElementEnumerator(this._list);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> object that can be used to navigate a collection of chain elements.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object.</returns>
		// Token: 0x06000AF5 RID: 2805 RVA: 0x000376AA File Offset: 0x000358AA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new X509ChainElementEnumerator(this._list);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000376B7 File Offset: 0x000358B7
		internal void Add(X509Certificate2 certificate)
		{
			this._list.Add(new X509ChainElement(certificate));
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000376CB File Offset: 0x000358CB
		internal void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000376D8 File Offset: 0x000358D8
		internal bool Contains(X509Certificate2 certificate)
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				if (certificate.Equals((this._list[i] as X509ChainElement).Certificate))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000835 RID: 2101
		private ArrayList _list;
	}
}
