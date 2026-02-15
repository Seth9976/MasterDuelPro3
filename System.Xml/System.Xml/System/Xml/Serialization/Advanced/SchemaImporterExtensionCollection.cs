using System;
using System.Collections;

namespace System.Xml.Serialization.Advanced
{
	/// <summary>Represents a collection of <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> objects.</summary>
	// Token: 0x020001FE RID: 510
	public class SchemaImporterExtensionCollection : CollectionBase
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x00097801 File Offset: 0x00095A01
		internal Hashtable Names
		{
			get
			{
				if (this.exNames == null)
				{
					this.exNames = new Hashtable();
				}
				return this.exNames;
			}
		}

		/// <summary>Adds the specified importer extension to the collection.</summary>
		/// <returns>The index of the added extension.</returns>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> to add.</param>
		// Token: 0x060019C2 RID: 6594 RVA: 0x0009781C File Offset: 0x00095A1C
		public int Add(SchemaImporterExtension extension)
		{
			return this.Add(extension.GetType().FullName, extension);
		}

		/// <summary>Adds the specified importer extension to the collection. The name parameter allows you to supply a custom name for the extension.</summary>
		/// <returns>The index of the newly added item.</returns>
		/// <param name="name">A custom name for the extension.</param>
		/// <param name="type">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> to add.</param>
		/// <exception cref="T:System.ArgumentException">The value of type does not inherit from <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" />.</exception>
		// Token: 0x060019C3 RID: 6595 RVA: 0x00097830 File Offset: 0x00095A30
		public int Add(string name, Type type)
		{
			if (type.IsSubclassOf(typeof(SchemaImporterExtension)))
			{
				return this.Add(name, (SchemaImporterExtension)Activator.CreateInstance(type));
			}
			throw new ArgumentException(Res.GetString("'{0}' is not a valid SchemaExtensionType.", new object[] { type }));
		}

		/// <summary>Removes the <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" />, specified by name, from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to remove. The name is set using the <see cref="M:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection.Add(System.String,System.Type)" /> method.</param>
		// Token: 0x060019C4 RID: 6596 RVA: 0x00097870 File Offset: 0x00095A70
		public void Remove(string name)
		{
			if (this.Names[name] != null)
			{
				base.List.Remove(this.Names[name]);
				this.Names[name] = null;
			}
		}

		/// <summary>Clears the collection of importer extensions.</summary>
		// Token: 0x060019C5 RID: 6597 RVA: 0x000978A4 File Offset: 0x00095AA4
		public new void Clear()
		{
			this.Names.Clear();
			base.List.Clear();
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x000978BC File Offset: 0x00095ABC
		internal SchemaImporterExtensionCollection Clone()
		{
			SchemaImporterExtensionCollection schemaImporterExtensionCollection = new SchemaImporterExtensionCollection();
			schemaImporterExtensionCollection.exNames = (Hashtable)this.Names.Clone();
			foreach (object obj in base.List)
			{
				schemaImporterExtensionCollection.List.Add(obj);
			}
			return schemaImporterExtensionCollection;
		}

		/// <summary>Gets the <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> at the specified index.</returns>
		/// <param name="index">The index of the item to find.</param>
		// Token: 0x170005BB RID: 1467
		public SchemaImporterExtension this[int index]
		{
			get
			{
				return (SchemaImporterExtension)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00097948 File Offset: 0x00095B48
		internal int Add(string name, SchemaImporterExtension extension)
		{
			if (this.Names[name] == null)
			{
				this.Names[name] = extension;
				return base.List.Add(extension);
			}
			if (this.Names[name].GetType() != extension.GetType())
			{
				throw new InvalidOperationException(Res.GetString("Duplicate extension name.  schemaImporterExtension with name '{0}' already been added.", new object[] { name }));
			}
			return -1;
		}

		/// <summary>Inserts the specified <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-base index at which the <paramref name="extension" /> should be inserted.</param>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to insert.</param>
		// Token: 0x060019CA RID: 6602 RVA: 0x00060C68 File Offset: 0x0005EE68
		public void Insert(int index, SchemaImporterExtension extension)
		{
			base.List.Insert(index, extension);
		}

		/// <summary>Searches for the specified item and returns the zero-based index of the first occurrence within the collection.</summary>
		/// <returns>The index of the found item.</returns>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to search for.</param>
		// Token: 0x060019CB RID: 6603 RVA: 0x00060C77 File Offset: 0x0005EE77
		public int IndexOf(SchemaImporterExtension extension)
		{
			return base.List.IndexOf(extension);
		}

		/// <summary>Gets a value that indicates whether the specified importer extension exists in the collection.</summary>
		/// <returns>true if the extension is found; otherwise, false.</returns>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> to search for.</param>
		// Token: 0x060019CC RID: 6604 RVA: 0x00060C85 File Offset: 0x0005EE85
		public bool Contains(SchemaImporterExtension extension)
		{
			return base.List.Contains(extension);
		}

		/// <summary>Removes the specified <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> from the collection.</summary>
		/// <param name="extension">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to remove. </param>
		// Token: 0x060019CD RID: 6605 RVA: 0x00060C93 File Offset: 0x0005EE93
		public void Remove(SchemaImporterExtension extension)
		{
			base.List.Remove(extension);
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtensionCollection" /> to the specified array of <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> objects at the specified index. </summary>
		/// <param name="array">The <see cref="T:System.Xml.Serialization.Advanced.SchemaImporterExtension" /> to copy the current collection to.</param>
		/// <param name="index">The zero-based index at which the collection is added.</param>
		// Token: 0x060019CE RID: 6606 RVA: 0x00060CA1 File Offset: 0x0005EEA1
		public void CopyTo(SchemaImporterExtension[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x04000AF3 RID: 2803
		private Hashtable exNames;
	}
}
