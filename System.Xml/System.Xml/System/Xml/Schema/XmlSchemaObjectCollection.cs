using System;
using System.Collections;

namespace System.Xml.Schema
{
	/// <summary>A collection of <see cref="T:System.Xml.Schema.XmlSchemaObject" />s.</summary>
	// Token: 0x020002EE RID: 750
	public class XmlSchemaObjectCollection : CollectionBase
	{
		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaObject" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		// Token: 0x1700083A RID: 2106
		public virtual XmlSchemaObject this[int index]
		{
			get
			{
				return (XmlSchemaObject)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Returns an enumerator for iterating through the XmlSchemaObjects contained in the XmlSchemaObjectCollection.</summary>
		/// <returns>The iterator returns <see cref="T:System.Xml.Schema.XmlSchemaObjectEnumerator" />.</returns>
		// Token: 0x060021AF RID: 8623 RVA: 0x000C0756 File Offset: 0x000BE956
		public new XmlSchemaObjectEnumerator GetEnumerator()
		{
			return new XmlSchemaObjectEnumerator(base.InnerList.GetEnumerator());
		}

		/// <summary>Adds an <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to the XmlSchemaObjectCollection.</summary>
		/// <returns>The index at which the item has been added.</returns>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than Count. </exception>
		/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> parameter specified is not of type <see cref="T:System.Xml.Schema.XmlSchemaExternal" /> or its derived types <see cref="T:System.Xml.Schema.XmlSchemaImport" />, <see cref="T:System.Xml.Schema.XmlSchemaInclude" />, and <see cref="T:System.Xml.Schema.XmlSchemaRedefine" />.</exception>
		// Token: 0x060021B0 RID: 8624 RVA: 0x00060C5A File Offset: 0x0005EE5A
		public int Add(XmlSchemaObject item)
		{
			return base.List.Add(item);
		}

		/// <summary>Inserts an <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to the XmlSchemaObjectCollection.</summary>
		/// <param name="index">The zero-based index at which an item should be inserted. </param>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than Count. </exception>
		// Token: 0x060021B1 RID: 8625 RVA: 0x00060C68 File Offset: 0x0005EE68
		public void Insert(int index, XmlSchemaObject item)
		{
			base.List.Insert(index, item);
		}

		/// <summary>Indicates if the specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> is in the XmlSchemaObjectCollection.</summary>
		/// <returns>true if the specified qualified name is in the collection; otherwise, returns false. If null is supplied, false is returned because there is no qualified name with a null name.</returns>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		// Token: 0x060021B2 RID: 8626 RVA: 0x00060C85 File Offset: 0x0005EE85
		public bool Contains(XmlSchemaObject item)
		{
			return base.List.Contains(item);
		}

		/// <summary>Removes an <see cref="T:System.Xml.Schema.XmlSchemaObject" /> from the XmlSchemaObjectCollection.</summary>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to remove. </param>
		// Token: 0x060021B3 RID: 8627 RVA: 0x00060C93 File Offset: 0x0005EE93
		public void Remove(XmlSchemaObject item)
		{
			base.List.Remove(item);
		}

		/// <summary>Copies all the <see cref="T:System.Xml.Schema.XmlSchemaObject" />s from the collection into the given array, starting at the given index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the XmlSchemaObjectCollection. The array must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in the array at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multi-dimensional.- or - <paramref name="index" /> is equal to or greater than the length of <paramref name="array" />.- or - The number of elements in the source <see cref="T:System.Xml.Schema.XmlSchemaObject" /> is greater than the available space from index to the end of the destination array. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Xml.Schema.XmlSchemaObject" /> cannot be cast automatically to the type of the destination array. </exception>
		// Token: 0x060021B4 RID: 8628 RVA: 0x00060CA1 File Offset: 0x0005EEA1
		public void CopyTo(XmlSchemaObject[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>OnInsert is invoked before the standard Insert behavior. For more information, see OnInsert method <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="index">The index of <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <param name="item">The item. </param>
		// Token: 0x060021B5 RID: 8629 RVA: 0x000C0768 File Offset: 0x000BE968
		protected override void OnInsert(int index, object item)
		{
			if (this.parent != null)
			{
				this.parent.OnAdd(this, item);
			}
		}

		/// <summary>OnSet is invoked before the standard Set behavior. For more information, see the OnSet method for <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="index">The index of <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <param name="oldValue">The old value. </param>
		/// <param name="newValue">The new value. </param>
		// Token: 0x060021B6 RID: 8630 RVA: 0x000C077F File Offset: 0x000BE97F
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			if (this.parent != null)
			{
				this.parent.OnRemove(this, oldValue);
				this.parent.OnAdd(this, newValue);
			}
		}

		/// <summary>OnClear is invoked before the standard Clear behavior. For more information, see OnClear method for <see cref="T:System.Collections.CollectionBase" />.</summary>
		// Token: 0x060021B7 RID: 8631 RVA: 0x000C07A3 File Offset: 0x000BE9A3
		protected override void OnClear()
		{
			if (this.parent != null)
			{
				this.parent.OnClear(this);
			}
		}

		/// <summary>OnRemove is invoked before the standard Remove behavior. For more information, see the OnRemove method for <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="index">The index of <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <param name="item">The item. </param>
		// Token: 0x060021B8 RID: 8632 RVA: 0x000C07B9 File Offset: 0x000BE9B9
		protected override void OnRemove(int index, object item)
		{
			if (this.parent != null)
			{
				this.parent.OnRemove(this, item);
			}
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x000C07D0 File Offset: 0x000BE9D0
		internal XmlSchemaObjectCollection Clone()
		{
			return new XmlSchemaObjectCollection { this };
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x000C07DE File Offset: 0x000BE9DE
		private void Add(XmlSchemaObjectCollection collToAdd)
		{
			base.InnerList.InsertRange(0, collToAdd);
		}

		// Token: 0x04000FB0 RID: 4016
		private XmlSchemaObject parent;
	}
}
