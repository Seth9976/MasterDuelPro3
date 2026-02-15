using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Represents a collection of <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> objects.</summary>
	// Token: 0x020001A5 RID: 421
	public class XmlArrayItemAttributes : CollectionBase
	{
		/// <summary>Gets or sets the item at the specified index.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the collection member to get or set. </param>
		// Token: 0x170004AB RID: 1195
		public XmlArrayItemAttribute this[int index]
		{
			get
			{
				return (XmlArrayItemAttribute)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to the collection.</summary>
		/// <returns>The index of the added item.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to add to the collection. </param>
		// Token: 0x060013E0 RID: 5088 RVA: 0x00060C5A File Offset: 0x0005EE5A
		public int Add(XmlArrayItemAttribute attribute)
		{
			return base.List.Add(attribute);
		}

		/// <summary>Inserts an <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> into the collection at the specified index. </summary>
		/// <param name="index">The index at which the attribute is inserted.</param>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" />  to insert.</param>
		// Token: 0x060013E1 RID: 5089 RVA: 0x00060C68 File Offset: 0x0005EE68
		public void Insert(int index, XmlArrayItemAttribute attribute)
		{
			base.List.Insert(index, attribute);
		}

		/// <summary>Returns the zero-based index of the first occurrence of the specified <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> in the collection or -1 if the attribute is not found in the collection. </summary>
		/// <returns>The first index of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> in the collection or -1 if the attribute is not found in the collection.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to locate in the collection.</param>
		// Token: 0x060013E2 RID: 5090 RVA: 0x00060C77 File Offset: 0x0005EE77
		public int IndexOf(XmlArrayItemAttribute attribute)
		{
			return base.List.IndexOf(attribute);
		}

		/// <summary>Determines whether the collection contains the specified <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" />. </summary>
		/// <returns>true if the collection contains the specified <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" />; otherwise, false.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to check for.</param>
		// Token: 0x060013E3 RID: 5091 RVA: 0x00060C85 File Offset: 0x0005EE85
		public bool Contains(XmlArrayItemAttribute attribute)
		{
			return base.List.Contains(attribute);
		}

		/// <summary>Removes an <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> from the collection, if it is present. </summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to remove.</param>
		// Token: 0x060013E4 RID: 5092 RVA: 0x00060C93 File Offset: 0x0005EE93
		public void Remove(XmlArrayItemAttribute attribute)
		{
			base.List.Remove(attribute);
		}

		/// <summary>Copies an <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> array to the collection, starting at a specified target index. </summary>
		/// <param name="array">The array of <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> objects to copy to the collection.</param>
		/// <param name="index">The index at which the copied attributes begin.</param>
		// Token: 0x060013E5 RID: 5093 RVA: 0x00060CA1 File Offset: 0x0005EEA1
		public void CopyTo(XmlArrayItemAttribute[] array, int index)
		{
			base.List.CopyTo(array, index);
		}
	}
}
