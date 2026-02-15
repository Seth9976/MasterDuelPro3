using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure the operation of the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" />. This class cannot be inherited.</summary>
	// Token: 0x020001F8 RID: 504
	[ConfigurationCollection(typeof(SchemaImporterExtensionElement))]
	public sealed class SchemaImporterExtensionElementCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the object that represents the XML element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the XML element to get or set.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than Count.</exception>
		// Token: 0x170005AE RID: 1454
		public SchemaImporterExtensionElement this[int index]
		{
			get
			{
				return (SchemaImporterExtensionElement)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		/// <summary>Gets or sets the item with the specified name.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> with the specified name.</returns>
		/// <param name="name">The name of the item to get or set.</param>
		// Token: 0x170005AF RID: 1455
		public SchemaImporterExtensionElement this[string name]
		{
			get
			{
				return (SchemaImporterExtensionElement)base.BaseGet(name);
			}
			set
			{
				if (base.BaseGet(name) != null)
				{
					base.BaseRemove(name);
				}
				this.BaseAdd(value);
			}
		}

		/// <summary>Adds an item to the end of the collection.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> to add to the collection.</param>
		// Token: 0x0600199F RID: 6559 RVA: 0x00097214 File Offset: 0x00095414
		public void Add(SchemaImporterExtensionElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all items from the collection.</summary>
		// Token: 0x060019A0 RID: 6560 RVA: 0x0009721D File Offset: 0x0009541D
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00097225 File Offset: 0x00095425
		protected override ConfigurationElement CreateNewElement()
		{
			return new SchemaImporterExtensionElement();
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0009722C File Offset: 0x0009542C
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SchemaImporterExtensionElement)element).Key;
		}

		/// <summary>Returns the zero-based index of the first element in the collection with the specified value.</summary>
		/// <returns>The index of the found element.</returns>
		/// <param name="element">The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> to find.</param>
		// Token: 0x060019A3 RID: 6563 RVA: 0x00097239 File Offset: 0x00095439
		public int IndexOf(SchemaImporterExtensionElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the first occurrence of a specific item from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> to remove.</param>
		// Token: 0x060019A4 RID: 6564 RVA: 0x00097242 File Offset: 0x00095442
		public void Remove(SchemaImporterExtensionElement element)
		{
			base.BaseRemove(element.Key);
		}

		/// <summary>Removes the item with the specified name from the collection.</summary>
		/// <param name="name">The name of the item to remove.</param>
		// Token: 0x060019A5 RID: 6565 RVA: 0x00097250 File Offset: 0x00095450
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the item at the specified index from the collection.</summary>
		/// <param name="index">The index of the object to remove.</param>
		// Token: 0x060019A6 RID: 6566 RVA: 0x00097259 File Offset: 0x00095459
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
