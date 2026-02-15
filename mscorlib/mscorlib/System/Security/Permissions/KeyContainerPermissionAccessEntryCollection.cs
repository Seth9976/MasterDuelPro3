using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects. This class cannot be inherited. </summary>
	// Token: 0x02000363 RID: 867
	[DefaultMember("Item")]
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryCollection : ICollection, IEnumerable
	{
		// Token: 0x06001E46 RID: 7750 RVA: 0x0007784B File Offset: 0x00075A4B
		internal KeyContainerPermissionAccessEntryCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects in the collection.</returns>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x0007785E File Offset: 0x00075A5E
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x00033991 File Offset: 0x00031B91
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection. </returns>
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001E49 RID: 7753 RVA: 0x00002645 File Offset: 0x00000845
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="accessEntry">The <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="accessEntry" /> is null.</exception>
		// Token: 0x06001E4A RID: 7754 RVA: 0x0007786B File Offset: 0x00075A6B
		public int Add(KeyContainerPermissionAccessEntry accessEntry)
		{
			return this._list.Add(accessEntry);
		}

		/// <summary>Copies the elements of the collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> array that is the destination of the elements copied from the current collection. </param>
		/// <param name="index">The index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		// Token: 0x06001E4B RID: 7755 RVA: 0x00077879 File Offset: 0x00075A79
		public void CopyTo(KeyContainerPermissionAccessEntry[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the current collection.</param>
		/// <param name="index">The index in <paramref name="array" /> at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
		// Token: 0x06001E4C RID: 7756 RVA: 0x00077879 File Offset: 0x00075A79
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> object that can be used to iterate through the objects in the collection.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06001E4D RID: 7757 RVA: 0x00077888 File Offset: 0x00075A88
		public KeyContainerPermissionAccessEntryEnumerator GetEnumerator()
		{
			return new KeyContainerPermissionAccessEntryEnumerator(this._list);
		}

		/// <summary>Returns a <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> object that can be used to iterate through the objects in the collection.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator" /> object that can be used to iterate through the collection. </returns>
		// Token: 0x06001E4E RID: 7758 RVA: 0x00077888 File Offset: 0x00075A88
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new KeyContainerPermissionAccessEntryEnumerator(this._list);
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object, if it exists in the collection.</summary>
		/// <returns>The index of the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object in the collection, or –1 if no match is found.</returns>
		/// <param name="accessEntry">The <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to locate.</param>
		// Token: 0x06001E4F RID: 7759 RVA: 0x00077898 File Offset: 0x00075A98
		public int IndexOf(KeyContainerPermissionAccessEntry accessEntry)
		{
			if (accessEntry == null)
			{
				throw new ArgumentNullException("accessEntry");
			}
			for (int i = 0; i < this._list.Count; i++)
			{
				if (accessEntry.Equals(this._list[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04000E0A RID: 3594
		private ArrayList _list;
	}
}
