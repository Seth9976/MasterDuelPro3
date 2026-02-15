using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Security.Cryptography
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.Oid" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020001A8 RID: 424
	public sealed class OidCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.OidCollection" /> class.</summary>
		// Token: 0x06000A24 RID: 2596 RVA: 0x00034530 File Offset: 0x00032730
		public OidCollection()
		{
			this._list = new List<Oid>();
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.Oid" /> object to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The index of the added <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <param name="oid">The <see cref="T:System.Security.Cryptography.Oid" /> object to add to the collection.</param>
		// Token: 0x06000A25 RID: 2597 RVA: 0x00034543 File Offset: 0x00032743
		public int Add(Oid oid)
		{
			int count = this._list.Count;
			this._list.Add(oid);
			return count;
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.Oid" /> object from the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		/// <param name="index">The location of the <see cref="T:System.Security.Cryptography.Oid" /> object in the collection.</param>
		// Token: 0x170001B8 RID: 440
		public Oid this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.Oid" /> objects in a collection. </summary>
		/// <returns>The number of <see cref="T:System.Security.Cryptography.Oid" /> objects in a collection.</returns>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0003456A File Offset: 0x0003276A
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidEnumerator" /> object.</returns>
		// Token: 0x06000A28 RID: 2600 RVA: 0x00034577 File Offset: 0x00032777
		public OidEnumerator GetEnumerator()
		{
			return new OidEnumerator(this);
		}

		/// <summary>Returns an <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidEnumerator" /> object that can be used to navigate the collection.</returns>
		// Token: 0x06000A29 RID: 2601 RVA: 0x0003457F File Offset: 0x0003277F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.OidCollection" /> object into an array.</summary>
		/// <param name="array">The array to copy the <see cref="T:System.Security.Cryptography.OidCollection" /> object to.</param>
		/// <param name="index">The location where the copy operation starts.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> cannot be a multidimensional array.-or-The length of <paramref name="array" /> is an invalid offset length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="index" /> is out range.</exception>
		// Token: 0x06000A2A RID: 2602 RVA: 0x00034588 File Offset: 0x00032788
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (index + this.Count > array.Length)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object is thread safe.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x000028AE File Offset: 0x00000AAE
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Security.Cryptography.OidCollection" /> object.</returns>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0001AE3D File Offset: 0x0001903D
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000785 RID: 1925
		private readonly List<Oid> _list;
	}
}
