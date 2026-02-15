using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>The base class for a collection of parameters relevant to a <see cref="T:System.Data.Common.DbCommand" />. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FE RID: 254
	public abstract class DbParameterCollection : MarshalByRefObject, IList, ICollection, IEnumerable
	{
		/// <summary>Specifies the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000D64 RID: 3428
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public abstract int Count { get; }

		/// <summary>Specifies whether the collection is a fixed size.</summary>
		/// <returns>true if the collection is a fixed size; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00011ED5 File Offset: 0x000100D5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Specifies whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x00011ED5 File Offset: 0x000100D5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Specifies whether the collection is synchronized.</summary>
		/// <returns>true if the collection is synchronized; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00011ED5 File Offset: 0x000100D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Specifies the <see cref="T:System.Object" /> to be used to synchronize access to the collection.</summary>
		/// <returns>A <see cref="T:System.Object" /> to be used to synchronize access to the <see cref="T:System.Data.Common.DbParameterCollection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000D68 RID: 3432
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public abstract object SyncRoot { get; }

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		// Token: 0x17000215 RID: 533
		object IList.this[int index]
		{
			get
			{
				return this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, (DbParameter)value);
			}
		}

		/// <summary>Gets and sets the <see cref="T:System.Data.Common.DbParameter" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbParameter" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the parameter.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified index does not exist. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000216 RID: 534
		public DbParameter this[int index]
		{
			get
			{
				return this.GetParameter(index);
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.Common.DbParameter" /> object to the <see cref="T:System.Data.Common.DbParameterCollection" />.</summary>
		/// <returns>The index of the <see cref="T:System.Data.Common.DbParameter" /> object in the collection.</returns>
		/// <param name="value">The <see cref="P:System.Data.Common.DbParameter.Value" /> of the <see cref="T:System.Data.Common.DbParameter" /> to add to the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D6C RID: 3436
		public abstract int Add(object value);

		/// <summary>Indicates whether a <see cref="T:System.Data.Common.DbParameter" /> with the specified <see cref="P:System.Data.Common.DbParameter.Value" /> is contained in the collection.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbParameter" /> is in the collection; otherwise false.</returns>
		/// <param name="value">The <see cref="P:System.Data.Common.DbParameter.Value" /> of the <see cref="T:System.Data.Common.DbParameter" /> to look for in the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D6D RID: 3437
		public abstract bool Contains(object value);

		/// <summary>Copies an array of items to the collection starting at the specified index.</summary>
		/// <param name="array">The array of items to copy to the collection.</param>
		/// <param name="index">The index in the collection to copy the items.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D6E RID: 3438
		public abstract void CopyTo(Array array, int index);

		/// <summary>Removes all <see cref="T:System.Data.Common.DbParameter" /> values from the <see cref="T:System.Data.Common.DbParameterCollection" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D6F RID: 3439
		public abstract void Clear();

		/// <summary>Exposes the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method, which supports a simple iteration over a collection by a .NET Framework data provider.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D70 RID: 3440
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract IEnumerator GetEnumerator();

		/// <summary>Returns the <see cref="T:System.Data.Common.DbParameter" /> object at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbParameter" /> object at the specified index in the collection.</returns>
		/// <param name="index">The index of the <see cref="T:System.Data.Common.DbParameter" /> in the collection.</param>
		// Token: 0x06000D71 RID: 3441
		protected abstract DbParameter GetParameter(int index);

		/// <summary>Returns the index of the specified <see cref="T:System.Data.Common.DbParameter" /> object.</summary>
		/// <returns>The index of the specified <see cref="T:System.Data.Common.DbParameter" /> object.</returns>
		/// <param name="value">The <see cref="T:System.Data.Common.DbParameter" /> object in the collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D72 RID: 3442
		public abstract int IndexOf(object value);

		/// <summary>Returns the index of the <see cref="T:System.Data.Common.DbParameter" /> object with the specified name.</summary>
		/// <returns>The index of the <see cref="T:System.Data.Common.DbParameter" /> object with the specified name.</returns>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.Common.DbParameter" /> object in the collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D73 RID: 3443
		public abstract int IndexOf(string parameterName);

		/// <summary>Inserts the specified index of the <see cref="T:System.Data.Common.DbParameter" /> object with the specified name into the collection at the specified index.</summary>
		/// <param name="index">The index at which to insert the <see cref="T:System.Data.Common.DbParameter" /> object.</param>
		/// <param name="value">The <see cref="T:System.Data.Common.DbParameter" /> object to insert into the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D74 RID: 3444
		public abstract void Insert(int index, object value);

		/// <summary>Removes the specified <see cref="T:System.Data.Common.DbParameter" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.Common.DbParameter" /> object to remove.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D75 RID: 3445
		public abstract void Remove(object value);

		/// <summary>Removes the <see cref="T:System.Data.Common.DbParameter" /> object at the specified from the collection.</summary>
		/// <param name="index">The index where the <see cref="T:System.Data.Common.DbParameter" /> object is located.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D76 RID: 3446
		public abstract void RemoveAt(int index);

		/// <summary>Sets the <see cref="T:System.Data.Common.DbParameter" /> object at the specified index to a new value. </summary>
		/// <param name="index">The index where the <see cref="T:System.Data.Common.DbParameter" /> object is located.</param>
		/// <param name="value">The new <see cref="T:System.Data.Common.DbParameter" /> value.</param>
		// Token: 0x06000D77 RID: 3447
		protected abstract void SetParameter(int index, DbParameter value);
	}
}
