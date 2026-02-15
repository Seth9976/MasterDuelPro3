using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	/// <summary>Provides the base functionality for creating collections.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000026 RID: 38
	public class InternalDataCollectionBase : ICollection, IEnumerable
	{
		/// <summary>Gets the total number of elements in a collection.</summary>
		/// <returns>The total number of elements in a collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00011EAC File Offset: 0x000100AC
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.InternalDataCollectionBase" /> to a one-dimensional <see cref="T:System.Array" />, starting at the specified <see cref="T:System.Data.InternalDataCollectionBase" /> index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> to copy the current <see cref="T:System.Data.InternalDataCollectionBase" /> object's elements into. </param>
		/// <param name="index">The destination <see cref="T:System.Array" /> index to start copying into. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000324 RID: 804 RVA: 0x00011EB9 File Offset: 0x000100B9
		public virtual void CopyTo(Array ar, int index)
		{
			this.List.CopyTo(ar, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000325 RID: 805 RVA: 0x00011EC8 File Offset: 0x000100C8
		public virtual IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.InternalDataCollectionBase" /> is synchonized.</summary>
		/// <returns>true if the collection is synchronized; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00011ED5 File Offset: 0x000100D5
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00011ED8 File Offset: 0x000100D8
		internal int NamesEqual(string s1, string s2, bool fCaseSensitive, CultureInfo locale)
		{
			if (fCaseSensitive)
			{
				if (string.Compare(s1, s2, false, locale) != 0)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (locale.CompareInfo.Compare(s1, s2, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) != 0)
				{
					return 0;
				}
				if (string.Compare(s1, s2, false, locale) != 0)
				{
					return -1;
				}
				return 1;
			}
		}

		/// <summary>Gets an object that can be used to synchronize the collection.</summary>
		/// <returns>The <see cref="T:System.object" /> used to synchronize the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000207F File Offset: 0x0000027F
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the items of the collection as a list.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains the collection.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00011F10 File Offset: 0x00010110
		protected virtual ArrayList List
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000EA RID: 234
		internal static readonly CollectionChangeEventArgs s_refreshEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null);
	}
}
