using System;
using System.Collections;
using System.Diagnostics;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a configuration element containing a collection of child elements.</summary>
	// Token: 0x02000010 RID: 16
	[DebuggerDisplay("Count = {Count}")]
	public abstract class ConfigurationElementCollection : ConfigurationElement, ICollection, IEnumerable
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003B90 File Offset: 0x00001D90
		internal override void InitFromProperty(PropertyInformation propertyInfo)
		{
			ConfigurationCollectionAttribute configurationCollectionAttribute = propertyInfo.Property.CollectionAttribute;
			if (configurationCollectionAttribute == null)
			{
				configurationCollectionAttribute = Attribute.GetCustomAttribute(propertyInfo.Type, typeof(ConfigurationCollectionAttribute)) as ConfigurationCollectionAttribute;
			}
			if (configurationCollectionAttribute != null)
			{
				this.addElementName = configurationCollectionAttribute.AddItemName;
				this.clearElementName = configurationCollectionAttribute.ClearItemsName;
				this.removeElementName = configurationCollectionAttribute.RemoveItemName;
			}
			base.InitFromProperty(propertyInfo);
		}

		/// <summary>Gets the type of the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> of this collection.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003BF5 File Offset: 0x00001DF5
		public virtual ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003BF8 File Offset: 0x00001DF8
		private bool IsBasic
		{
			get
			{
				return this.CollectionType == ConfigurationElementCollectionType.BasicMap || this.CollectionType == ConfigurationElementCollectionType.BasicMapAlternate;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003C0D File Offset: 0x00001E0D
		private bool IsAlternate
		{
			get
			{
				return this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate || this.CollectionType == ConfigurationElementCollectionType.BasicMapAlternate;
			}
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003C23 File Offset: 0x00001E23
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets the name used to identify this collection of elements in the configuration file when overridden in a derived class.</summary>
		/// <returns>The name of the collection; otherwise, an empty string. The default is an empty string.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003C30 File Offset: 0x00001E30
		protected virtual string ElementName
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the collection has been cleared.</summary>
		/// <returns>true if the collection has been cleared; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration is read-only.</exception>
		// Token: 0x17000026 RID: 38
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003C37 File Offset: 0x00001E37
		public bool EmitClear
		{
			set
			{
				this.emitClear = value;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized.</summary>
		/// <returns>true if access to the <see cref="T:System.Configuration.ConfigurationElementCollection" /> is synchronized; otherwise, false.</returns>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000329C File Offset: 0x0000149C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object used to synchronize access to the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>An object used to synchronize access to the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</returns>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003C40 File Offset: 0x00001E40
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether an attempt to add a duplicate <see cref="T:System.Configuration.ConfigurationElement" /> to the <see cref="T:System.Configuration.ConfigurationElementCollection" /> will cause an exception to be thrown.</summary>
		/// <returns>true if an attempt to add a duplicate <see cref="T:System.Configuration.ConfigurationElement" /> to this <see cref="T:System.Configuration.ConfigurationElementCollection" /> will cause an exception to be thrown; otherwise, false. </returns>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003C43 File Offset: 0x00001E43
		protected virtual bool ThrowOnDuplicate
		{
			get
			{
				return this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMap || this.CollectionType == ConfigurationElementCollectionType.AddRemoveClearMapAlternate;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the add operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a derived class. </summary>
		/// <returns>The name of the element.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value starts with the reserved prefix "config" or "lock".</exception>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003C5A File Offset: 0x00001E5A
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003C62 File Offset: 0x00001E62
		protected internal string AddElementName
		{
			get
			{
				return this.addElementName;
			}
			set
			{
				this.addElementName = value;
			}
		}

		/// <summary>Gets or sets the name for the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the clear operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a derived class. </summary>
		/// <returns>The name of the element.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value starts with the reserved prefix "config" or "lock".</exception>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003C6B File Offset: 0x00001E6B
		protected internal string ClearElementName
		{
			get
			{
				return this.clearElementName;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.ConfigurationElement" /> to associate with the remove operation in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> when overridden in a derived class. </summary>
		/// <returns>The name of the element.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value starts with the reserved prefix "config" or "lock".</exception>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003C73 File Offset: 0x00001E73
		protected internal string RemoveElementName
		{
			get
			{
				return this.removeElementName;
			}
		}

		/// <summary>Adds a configuration element to the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to add.</param>
		// Token: 0x0600007E RID: 126 RVA: 0x00003C7B File Offset: 0x00001E7B
		protected virtual void BaseAdd(ConfigurationElement element)
		{
			this.BaseAdd(element, this.ThrowOnDuplicate);
		}

		/// <summary>Adds a configuration element to the configuration element collection.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to add.</param>
		/// <param name="throwIfExists">true to throw an exception if the <see cref="T:System.Configuration.ConfigurationElement" /> specified is already contained in the <see cref="T:System.Configuration.ConfigurationElementCollection" />; otherwise, false. </param>
		/// <exception cref="T:System.Exception">The <see cref="T:System.Configuration.ConfigurationElement" /> to add already exists in the <see cref="T:System.Configuration.ConfigurationElementCollection" /> and the <paramref name="throwIfExists" /> parameter is true. </exception>
		// Token: 0x0600007F RID: 127 RVA: 0x00003C8C File Offset: 0x00001E8C
		protected void BaseAdd(ConfigurationElement element, bool throwIfExists)
		{
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			if (this.IsAlternate)
			{
				this.list.Insert(this.inheritedLimitIndex, element);
				this.inheritedLimitIndex++;
			}
			else
			{
				int num = this.IndexOfKey(this.GetElementKey(element));
				if (num >= 0)
				{
					if (element.Equals(this.list[num]))
					{
						return;
					}
					if (throwIfExists)
					{
						throw new ConfigurationErrorsException("Duplicate element in collection");
					}
					this.list.RemoveAt(num);
				}
				this.list.Add(element);
			}
			this.modified = true;
		}

		/// <summary>Adds a configuration element to the configuration element collection.</summary>
		/// <param name="index">The index location at which to add the specified <see cref="T:System.Configuration.ConfigurationElement" />. </param>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to add. </param>
		// Token: 0x06000080 RID: 128 RVA: 0x00003D2C File Offset: 0x00001F2C
		protected virtual void BaseAdd(int index, ConfigurationElement element)
		{
			if (this.ThrowOnDuplicate && this.BaseIndexOf(element) != -1)
			{
				throw new ConfigurationErrorsException("Duplicate element in collection");
			}
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			if (this.IsAlternate && index > this.inheritedLimitIndex)
			{
				throw new ConfigurationErrorsException("Can't insert new elements below the inherited elements.");
			}
			if (!this.IsAlternate && index <= this.inheritedLimitIndex)
			{
				throw new ConfigurationErrorsException("Can't insert new elements above the inherited elements.");
			}
			this.list.Insert(index, element);
			this.modified = true;
		}

		/// <summary>Removes all configuration element objects from the collection.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration is read-only.- or -A collection item has been locked in a higher-level configuration.</exception>
		// Token: 0x06000081 RID: 129 RVA: 0x00003DB5 File Offset: 0x00001FB5
		protected internal void BaseClear()
		{
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			this.list.Clear();
			this.modified = true;
		}

		/// <summary>Gets the configuration element at the specified index location.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElement" /> at the specified index.</returns>
		/// <param name="index">The index location of the <see cref="T:System.Configuration.ConfigurationElement" /> to return. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="index" /> is less than 0.- or -There is no <see cref="T:System.Configuration.ConfigurationElement" /> at the specified <paramref name="index" />.</exception>
		// Token: 0x06000082 RID: 130 RVA: 0x00003DDC File Offset: 0x00001FDC
		protected internal ConfigurationElement BaseGet(int index)
		{
			return (ConfigurationElement)this.list[index];
		}

		/// <summary>Returns the configuration element with the specified key.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElement" /> with the specified key; otherwise, null.</returns>
		/// <param name="key">The key of the element to return. </param>
		// Token: 0x06000083 RID: 131 RVA: 0x00003DF0 File Offset: 0x00001FF0
		protected internal ConfigurationElement BaseGet(object key)
		{
			int num = this.IndexOfKey(key);
			if (num != -1)
			{
				return (ConfigurationElement)this.list[num];
			}
			return null;
		}

		/// <summary>Indicates the index of the specified <see cref="T:System.Configuration.ConfigurationElement" />.</summary>
		/// <returns>The index of the specified <see cref="T:System.Configuration.ConfigurationElement" />; otherwise, -1.</returns>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> for the specified index location.</param>
		// Token: 0x06000084 RID: 132 RVA: 0x00003E1C File Offset: 0x0000201C
		protected int BaseIndexOf(ConfigurationElement element)
		{
			return this.list.IndexOf(element);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003E2C File Offset: 0x0000202C
		private int IndexOfKey(object key)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (this.CompareKeys(this.GetElementKey((ConfigurationElement)this.list[i]), key))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Removes a <see cref="T:System.Configuration.ConfigurationElement" /> from the collection.</summary>
		/// <param name="key">The key of the <see cref="T:System.Configuration.ConfigurationElement" /> to remove.</param>
		/// <exception cref="T:System.Exception">No <see cref="T:System.Configuration.ConfigurationElement" /> with the specified key exists in the collection, the element has already been removed, or the element cannot be removed because the value of its <see cref="P:System.Configuration.ConfigurationProperty.Type" /> is not <see cref="F:System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMap" />. </exception>
		// Token: 0x06000086 RID: 134 RVA: 0x00003E74 File Offset: 0x00002074
		protected internal void BaseRemove(object key)
		{
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			int num = this.IndexOfKey(key);
			if (num != -1)
			{
				this.BaseRemoveAt(num);
				this.modified = true;
			}
		}

		/// <summary>Removes the <see cref="T:System.Configuration.ConfigurationElement" /> at the specified index location.</summary>
		/// <param name="index">The index location of the <see cref="T:System.Configuration.ConfigurationElement" /> to remove.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration is read-only.- or -<paramref name="index" /> is less than 0 or greater than the number of <see cref="T:System.Configuration.ConfigurationElement" /> objects in the collection.- or -The <see cref="T:System.Configuration.ConfigurationElement" /> object has already been removed.- or -The value of the <see cref="T:System.Configuration.ConfigurationElement" /> object has been locked at a higher level.- or -The <see cref="T:System.Configuration.ConfigurationElement" /> object was inherited.- or -The value of the <see cref="T:System.Configuration.ConfigurationElement" /> object's <see cref="P:System.Configuration.ConfigurationProperty.Type" /> is not <see cref="F:System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMap" /> or <see cref="F:System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMapAlternate" />.</exception>
		// Token: 0x06000087 RID: 135 RVA: 0x00003EB0 File Offset: 0x000020B0
		protected internal void BaseRemoveAt(int index)
		{
			if (this.IsReadOnly())
			{
				throw new ConfigurationErrorsException("Collection is read only.");
			}
			ConfigurationElement configurationElement = (ConfigurationElement)this.list[index];
			if (!this.IsElementRemovable(configurationElement))
			{
				throw new ConfigurationErrorsException("Element can't be removed from element collection.");
			}
			if (this.inherited != null && this.inherited.Contains(configurationElement))
			{
				throw new ConfigurationErrorsException("Inherited items can't be removed.");
			}
			this.list.RemoveAt(index);
			if (this.IsAlternate && this.inheritedLimitIndex > 0)
			{
				this.inheritedLimitIndex--;
			}
			this.modified = true;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003F49 File Offset: 0x00002149
		private bool CompareKeys(object key1, object key2)
		{
			if (this.comparer != null)
			{
				return this.comparer.Compare(key1, key2) == 0;
			}
			return object.Equals(key1, key2);
		}

		/// <summary>When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.</summary>
		/// <returns>A newly created <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		// Token: 0x06000089 RID: 137
		protected abstract ConfigurationElement CreateNewElement();

		/// <summary>Creates a new <see cref="T:System.Configuration.ConfigurationElement" /> when overridden in a derived class.</summary>
		/// <returns>A new <see cref="T:System.Configuration.ConfigurationElement" /> with a specified name.</returns>
		/// <param name="elementName">The name of the <see cref="T:System.Configuration.ConfigurationElement" /> to create. </param>
		// Token: 0x0600008A RID: 138 RVA: 0x00003F6B File Offset: 0x0000216B
		protected virtual ConfigurationElement CreateNewElement(string elementName)
		{
			return this.CreateNewElement();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003F74 File Offset: 0x00002174
		private ConfigurationElement CreateNewElementInternal(string elementName)
		{
			ConfigurationElement configurationElement;
			if (elementName == null)
			{
				configurationElement = this.CreateNewElement();
			}
			else
			{
				configurationElement = this.CreateNewElement(elementName);
			}
			configurationElement.Init();
			return configurationElement;
		}

		/// <summary>Compares the <see cref="T:System.Configuration.ConfigurationElementCollection" /> to the specified object.</summary>
		/// <returns>true if the object to compare with is equal to the current <see cref="T:System.Configuration.ConfigurationElementCollection" /> instance; otherwise, false. The default is false.</returns>
		/// <param name="compareTo">The object to compare. </param>
		// Token: 0x0600008C RID: 140 RVA: 0x00003F9C File Offset: 0x0000219C
		public override bool Equals(object compareTo)
		{
			ConfigurationElementCollection configurationElementCollection = compareTo as ConfigurationElementCollection;
			if (configurationElementCollection == null)
			{
				return false;
			}
			if (base.GetType() != configurationElementCollection.GetType())
			{
				return false;
			}
			if (this.Count != configurationElementCollection.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (!this.BaseGet(i).Equals(configurationElementCollection.BaseGet(i)))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets the element key for a specified configuration element when overridden in a derived class.</summary>
		/// <returns>An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
		/// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for. </param>
		// Token: 0x0600008D RID: 141
		protected abstract object GetElementKey(ConfigurationElement element);

		/// <summary>Gets a unique value representing the <see cref="T:System.Configuration.ConfigurationElementCollection" /> instance.</summary>
		/// <returns>A unique value representing the <see cref="T:System.Configuration.ConfigurationElementCollection" /> current instance.</returns>
		// Token: 0x0600008E RID: 142 RVA: 0x00004004 File Offset: 0x00002204
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.Count; i++)
			{
				num += this.BaseGet(i).GetHashCode();
			}
			return num;
		}

		/// <summary>Copies the <see cref="T:System.Configuration.ConfigurationElementCollection" /> to an array.</summary>
		/// <param name="arr">Array to which to copy this <see cref="T:System.Configuration.ConfigurationElementCollection" />.</param>
		/// <param name="index">Index location at which to begin copying.</param>
		// Token: 0x0600008F RID: 143 RVA: 0x00004034 File Offset: 0x00002234
		void ICollection.CopyTo(Array arr, int index)
		{
			this.list.CopyTo(arr, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> which is used to iterate through the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> which is used to iterate through the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</returns>
		// Token: 0x06000090 RID: 144 RVA: 0x00004043 File Offset: 0x00002243
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Configuration.ConfigurationElement" /> exists in the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>true if the element exists in the collection; otherwise, false. The default is false.</returns>
		/// <param name="elementName">The name of the element to verify. </param>
		// Token: 0x06000091 RID: 145 RVA: 0x0000329C File Offset: 0x0000149C
		protected virtual bool IsElementName(string elementName)
		{
			return false;
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Configuration.ConfigurationElement" /> can be removed from the <see cref="T:System.Configuration.ConfigurationElementCollection" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Configuration.ConfigurationElement" /> can be removed from this <see cref="T:System.Configuration.ConfigurationElementCollection" />; otherwise, false. The default is true.</returns>
		/// <param name="element">The element to check.</param>
		// Token: 0x06000092 RID: 146 RVA: 0x00004050 File Offset: 0x00002250
		protected virtual bool IsElementRemovable(ConfigurationElement element)
		{
			return !this.IsReadOnly();
		}

		/// <summary>Indicates whether this <see cref="T:System.Configuration.ConfigurationElementCollection" /> has been modified since it was last saved or loaded when overridden in a derived class.</summary>
		/// <returns>true if any contained element has been modified; otherwise, false</returns>
		// Token: 0x06000093 RID: 147 RVA: 0x0000405C File Offset: 0x0000225C
		protected internal override bool IsModified()
		{
			if (this.modified)
			{
				return true;
			}
			for (int i = 0; i < this.list.Count; i++)
			{
				if (((ConfigurationElement)this.list[i]).IsModified())
				{
					this.modified = true;
					break;
				}
			}
			return this.modified;
		}

		/// <summary>Indicates whether the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object is read only.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object is read only; otherwise, false.</returns>
		// Token: 0x06000094 RID: 148 RVA: 0x000040B0 File Offset: 0x000022B0
		[MonoTODO]
		public override bool IsReadOnly()
		{
			return base.IsReadOnly();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000040B8 File Offset: 0x000022B8
		internal override void PrepareSave(ConfigurationElement parentElement, ConfigurationSaveMode mode)
		{
			ConfigurationElementCollection configurationElementCollection = (ConfigurationElementCollection)parentElement;
			base.PrepareSave(parentElement, mode);
			for (int i = 0; i < this.list.Count; i++)
			{
				ConfigurationElement configurationElement = (ConfigurationElement)this.list[i];
				object elementKey = this.GetElementKey(configurationElement);
				ConfigurationElement configurationElement2 = ((configurationElementCollection != null) ? configurationElementCollection.BaseGet(elementKey) : null);
				configurationElement.PrepareSave(configurationElement2, mode);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000411C File Offset: 0x0000231C
		internal override bool HasValues(ConfigurationElement parentElement, ConfigurationSaveMode mode)
		{
			ConfigurationElementCollection configurationElementCollection = (ConfigurationElementCollection)parentElement;
			if (mode == ConfigurationSaveMode.Full)
			{
				return this.list.Count > 0;
			}
			for (int i = 0; i < this.list.Count; i++)
			{
				ConfigurationElement configurationElement = (ConfigurationElement)this.list[i];
				object elementKey = this.GetElementKey(configurationElement);
				ConfigurationElement configurationElement2 = ((configurationElementCollection != null) ? configurationElementCollection.BaseGet(elementKey) : null);
				if (configurationElement.HasValues(configurationElement2, mode))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Resets the <see cref="T:System.Configuration.ConfigurationElementCollection" /> to its unmodified state when overridden in a derived class.</summary>
		/// <param name="parentElement">The <see cref="T:System.Configuration.ConfigurationElement" /> representing the collection parent element, if any; otherwise, null. </param>
		// Token: 0x06000097 RID: 151 RVA: 0x00004190 File Offset: 0x00002390
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			bool isBasic = this.IsBasic;
			ConfigurationElementCollection configurationElementCollection = (ConfigurationElementCollection)parentElement;
			for (int i = 0; i < configurationElementCollection.Count; i++)
			{
				ConfigurationElement configurationElement = configurationElementCollection.BaseGet(i);
				ConfigurationElement configurationElement2 = this.CreateNewElementInternal(null);
				configurationElement2.Reset(configurationElement);
				this.BaseAdd(configurationElement2);
				if (isBasic)
				{
					if (this.inherited == null)
					{
						this.inherited = new ArrayList();
					}
					this.inherited.Add(configurationElement2);
				}
			}
			if (this.IsAlternate)
			{
				this.inheritedLimitIndex = 0;
			}
			else
			{
				this.inheritedLimitIndex = this.Count - 1;
			}
			this.modified = false;
		}

		/// <summary>Resets the value of the <see cref="M:System.Configuration.ConfigurationElementCollection.IsModified" /> property to false when overridden in a derived class.</summary>
		// Token: 0x06000098 RID: 152 RVA: 0x00004228 File Offset: 0x00002428
		protected internal override void ResetModified()
		{
			this.modified = false;
			for (int i = 0; i < this.list.Count; i++)
			{
				((ConfigurationElement)this.list[i]).ResetModified();
			}
		}

		/// <summary>Sets the <see cref="M:System.Configuration.ConfigurationElementCollection.IsReadOnly" /> property for the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object and for all sub-elements.</summary>
		// Token: 0x06000099 RID: 153 RVA: 0x00004268 File Offset: 0x00002468
		[MonoTODO]
		protected internal override void SetReadOnly()
		{
			base.SetReadOnly();
		}

		/// <summary>Writes the configuration data to an XML element in the configuration file when overridden in a derived class.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationElementCollection" /> was written to the configuration file successfully.</returns>
		/// <param name="writer">Output stream that writes XML to the configuration file.</param>
		/// <param name="serializeCollectionKey">true to serialize the collection key; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentException">One of the elements in the collection was added or replaced and starts with the reserved prefix "config" or "lock".</exception>
		// Token: 0x0600009A RID: 154 RVA: 0x00004270 File Offset: 0x00002470
		protected internal override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			if (serializeCollectionKey)
			{
				return base.SerializeElement(writer, serializeCollectionKey);
			}
			bool flag = false;
			if (this.IsBasic)
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					ConfigurationElement configurationElement = (ConfigurationElement)this.list[i];
					if (this.ElementName != string.Empty)
					{
						flag = configurationElement.SerializeToXmlElement(writer, this.ElementName) || flag;
					}
					else
					{
						flag = configurationElement.SerializeElement(writer, false) || flag;
					}
				}
			}
			else
			{
				if (this.emitClear)
				{
					writer.WriteElementString(this.clearElementName, "");
					flag = true;
				}
				if (this.removed != null)
				{
					for (int j = 0; j < this.removed.Count; j++)
					{
						writer.WriteStartElement(this.removeElementName);
						((ConfigurationElement)this.removed[j]).SerializeElement(writer, true);
						writer.WriteEndElement();
					}
					flag = flag || this.removed.Count > 0;
				}
				for (int k = 0; k < this.list.Count; k++)
				{
					((ConfigurationElement)this.list[k]).SerializeToXmlElement(writer, this.addElementName);
				}
				flag = flag || this.list.Count > 0;
			}
			return flag;
		}

		/// <summary>Causes the configuration system to throw an exception.</summary>
		/// <returns>true if the unrecognized element was deserialized successfully; otherwise, false. The default is false.</returns>
		/// <param name="elementName">The name of the unrecognized element.</param>
		/// <param name="reader">An input stream that reads XML from the configuration file. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The element specified in <paramref name="elementName" /> is the &lt;clear&gt; element.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="elementName" /> starts with the reserved prefix "config" or "lock".</exception>
		// Token: 0x0600009B RID: 155 RVA: 0x000043B8 File Offset: 0x000025B8
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			if (this.IsBasic)
			{
				ConfigurationElement configurationElement = null;
				if (elementName == this.ElementName)
				{
					configurationElement = this.CreateNewElementInternal(null);
				}
				if (this.IsElementName(elementName))
				{
					configurationElement = this.CreateNewElementInternal(elementName);
				}
				if (configurationElement != null)
				{
					configurationElement.DeserializeElement(reader, false);
					this.BaseAdd(configurationElement);
					this.modified = false;
					return true;
				}
			}
			else if (elementName == this.clearElementName)
			{
				reader.MoveToContent();
				if (reader.MoveToNextAttribute())
				{
					throw new ConfigurationErrorsException("Unrecognized attribute '" + reader.LocalName + "'.");
				}
				reader.MoveToElement();
				reader.Skip();
				this.BaseClear();
				this.emitClear = true;
				this.modified = false;
				return true;
			}
			else
			{
				if (elementName == this.removeElementName)
				{
					ConfigurationElementCollection.ConfigurationRemoveElement configurationRemoveElement = new ConfigurationElementCollection.ConfigurationRemoveElement(this.CreateNewElementInternal(null), this);
					configurationRemoveElement.DeserializeElement(reader, true);
					this.BaseRemove(configurationRemoveElement.KeyValue);
					this.modified = false;
					return true;
				}
				if (elementName == this.addElementName)
				{
					ConfigurationElement configurationElement2 = this.CreateNewElementInternal(null);
					configurationElement2.DeserializeElement(reader, false);
					this.BaseAdd(configurationElement2);
					this.modified = false;
					return true;
				}
			}
			return false;
		}

		/// <summary>Reverses the effect of merging configuration information from different levels of the configuration hierarchy </summary>
		/// <param name="sourceElement">A <see cref="T:System.Configuration.ConfigurationElement" /> object at the current level containing a merged view of the properties.</param>
		/// <param name="parentElement">The parent <see cref="T:System.Configuration.ConfigurationElement" /> object of the current element, or null if this is the top level.</param>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> enumerated value that determines which property values to include.</param>
		// Token: 0x0600009C RID: 156 RVA: 0x000044D8 File Offset: 0x000026D8
		protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			ConfigurationElementCollection configurationElementCollection = (ConfigurationElementCollection)sourceElement;
			ConfigurationElementCollection configurationElementCollection2 = (ConfigurationElementCollection)parentElement;
			for (int i = 0; i < configurationElementCollection.Count; i++)
			{
				ConfigurationElement configurationElement = configurationElementCollection.BaseGet(i);
				object elementKey = configurationElementCollection.GetElementKey(configurationElement);
				ConfigurationElement configurationElement2 = ((configurationElementCollection2 != null) ? configurationElementCollection2.BaseGet(elementKey) : null);
				ConfigurationElement configurationElement3 = this.CreateNewElementInternal(null);
				if (configurationElement2 != null && saveMode != ConfigurationSaveMode.Full)
				{
					configurationElement3.Unmerge(configurationElement, configurationElement2, saveMode);
					if (configurationElement3.HasValues(configurationElement2, saveMode))
					{
						this.BaseAdd(configurationElement3);
					}
				}
				else
				{
					configurationElement3.Unmerge(configurationElement, null, ConfigurationSaveMode.Full);
					this.BaseAdd(configurationElement3);
				}
			}
			if (saveMode == ConfigurationSaveMode.Full)
			{
				this.EmitClear = true;
				return;
			}
			if (configurationElementCollection2 != null)
			{
				for (int j = 0; j < configurationElementCollection2.Count; j++)
				{
					ConfigurationElement configurationElement4 = configurationElementCollection2.BaseGet(j);
					object elementKey2 = configurationElementCollection2.GetElementKey(configurationElement4);
					if (configurationElementCollection.IndexOfKey(elementKey2) == -1)
					{
						if (this.removed == null)
						{
							this.removed = new ArrayList();
						}
						this.removed.Add(configurationElement4);
					}
				}
			}
		}

		// Token: 0x0400003C RID: 60
		private ArrayList list = new ArrayList();

		// Token: 0x0400003D RID: 61
		private ArrayList removed;

		// Token: 0x0400003E RID: 62
		private ArrayList inherited;

		// Token: 0x0400003F RID: 63
		private bool emitClear;

		// Token: 0x04000040 RID: 64
		private bool modified;

		// Token: 0x04000041 RID: 65
		private IComparer comparer;

		// Token: 0x04000042 RID: 66
		private int inheritedLimitIndex;

		// Token: 0x04000043 RID: 67
		private string addElementName = "add";

		// Token: 0x04000044 RID: 68
		private string clearElementName = "clear";

		// Token: 0x04000045 RID: 69
		private string removeElementName = "remove";

		// Token: 0x02000011 RID: 17
		private sealed class ConfigurationRemoveElement : ConfigurationElement
		{
			// Token: 0x0600009D RID: 157 RVA: 0x000045D0 File Offset: 0x000027D0
			internal ConfigurationRemoveElement(ConfigurationElement origElement, ConfigurationElementCollection origCollection)
			{
				this._origElement = origElement;
				this._origCollection = origCollection;
				foreach (object obj in origElement.Properties)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
					if (configurationProperty.IsKey)
					{
						this.properties.Add(configurationProperty);
					}
				}
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x0600009E RID: 158 RVA: 0x00004658 File Offset: 0x00002858
			internal object KeyValue
			{
				get
				{
					foreach (object obj in this.Properties)
					{
						ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
						this._origElement[configurationProperty] = base[configurationProperty];
					}
					return this._origCollection.GetElementKey(this._origElement);
				}
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x0600009F RID: 159 RVA: 0x000046D0 File Offset: 0x000028D0
			protected internal override ConfigurationPropertyCollection Properties
			{
				get
				{
					return this.properties;
				}
			}

			// Token: 0x04000046 RID: 70
			private readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

			// Token: 0x04000047 RID: 71
			private readonly ConfigurationElement _origElement;

			// Token: 0x04000048 RID: 72
			private readonly ConfigurationElementCollection _origCollection;
		}
	}
}
