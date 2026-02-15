using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides the base functionality for creating data-related collections in the <see cref="N:System.Windows.Forms" /> namespace.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000018 RID: 24
	public class BaseCollection : MarshalByRefObject, ICollection, IEnumerable
	{
		/// <summary>Gets the total number of elements in the collection.</summary>
		/// <returns>The total number of elements in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000030E1 File Offset: 0x000012E1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>This property is always false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002D70 File Offset: 0x00000F70
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized.</summary>
		/// <returns>This property always returns false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002D70 File Offset: 0x00000F70
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.BaseCollection" />.</summary>
		/// <returns>An object that can be used to synchronize the <see cref="T:System.Windows.Forms.BaseCollection" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002F7A File Offset: 0x0000117A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the list of elements contained in the <see cref="T:System.Windows.Forms.BaseCollection" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the elements of the collection. This property returns null unless overridden in a derived class.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000030EE File Offset: 0x000012EE
		protected virtual ArrayList List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		/// <summary>Copies all the elements of the current one-dimensional <see cref="T:System.Array" /> to the specified one-dimensional <see cref="T:System.Array" /> starting at the specified destination <see cref="T:System.Array" /> index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current Array. </param>
		/// <param name="index">The zero-based relative index in <paramref name="ar" /> at which copying begins. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000066 RID: 102 RVA: 0x00003109 File Offset: 0x00001309
		public void CopyTo(Array ar, int index)
		{
			this.List.CopyTo(ar, index);
		}

		/// <summary>Gets the object that enables iterating through the members of the collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000067 RID: 103 RVA: 0x00003118 File Offset: 0x00001318
		public IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		// Token: 0x04000098 RID: 152
		internal ArrayList list;
	}
}
