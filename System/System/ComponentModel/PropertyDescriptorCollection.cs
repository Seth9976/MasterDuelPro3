using System;
using System.Collections;
using System.Collections.Generic;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</summary>
	// Token: 0x02000291 RID: 657
	public class PropertyDescriptorCollection : ICollection, IEnumerable, IList, IDictionary
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> class.</summary>
		/// <param name="properties">An array of type <see cref="T:System.ComponentModel.PropertyDescriptor" /> that provides the properties for this collection. </param>
		// Token: 0x06000FA3 RID: 4003 RVA: 0x000428E8 File Offset: 0x00040AE8
		public PropertyDescriptorCollection(PropertyDescriptor[] properties)
		{
			if (properties == null)
			{
				this._properties = Array.Empty<PropertyDescriptor>();
				this.Count = 0;
			}
			else
			{
				this._properties = properties;
				this.Count = properties.Length;
			}
			this._propsOwned = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> class, which is optionally read-only.</summary>
		/// <param name="properties">An array of type <see cref="T:System.ComponentModel.PropertyDescriptor" /> that provides the properties for this collection.</param>
		/// <param name="readOnly">If true, specifies that the collection cannot be modified.</param>
		// Token: 0x06000FA4 RID: 4004 RVA: 0x00042934 File Offset: 0x00040B34
		public PropertyDescriptorCollection(PropertyDescriptor[] properties, bool readOnly)
			: this(properties)
		{
			this._readOnly = readOnly;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00042944 File Offset: 0x00040B44
		private PropertyDescriptorCollection(PropertyDescriptor[] properties, int propCount, string[] namedSort, IComparer comparer)
		{
			this._propsOwned = false;
			if (namedSort != null)
			{
				this._namedSort = (string[])namedSort.Clone();
			}
			this._comparer = comparer;
			this._properties = properties;
			this.Count = propCount;
			this._needSort = true;
		}

		/// <summary>Gets the number of property descriptors in the collection.</summary>
		/// <returns>The number of property descriptors in the collection.</returns>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0004299A File Offset: 0x00040B9A
		// (set) Token: 0x06000FA7 RID: 4007 RVA: 0x000429A2 File Offset: 0x00040BA2
		public int Count { get; private set; }

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> at the specified index number.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified index number.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to get or set. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is not a valid index for <see cref="P:System.ComponentModel.PropertyDescriptorCollection.Item(System.Int32)" />. </exception>
		// Token: 0x17000352 RID: 850
		public virtual PropertyDescriptor this[int index]
		{
			get
			{
				if (index >= this.Count)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsurePropsOwned();
				return this._properties[index];
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, or null if the property does not exist.</returns>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to get from the collection. </param>
		// Token: 0x17000353 RID: 851
		public virtual PropertyDescriptor this[string name]
		{
			get
			{
				return this.Find(name, false);
			}
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the collection.</summary>
		/// <returns>The index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added to the collection.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000FAA RID: 4010 RVA: 0x000429D4 File Offset: 0x00040BD4
		public int Add(PropertyDescriptor value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.Count + 1);
			PropertyDescriptor[] properties = this._properties;
			int count = this.Count;
			this.Count = count + 1;
			properties[count] = value;
			return this.Count - 1;
		}

		/// <summary>Removes all <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000FAB RID: 4011 RVA: 0x00042A1E File Offset: 0x00040C1E
		public void Clear()
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			this.Count = 0;
			this._cachedFoundProperties = null;
		}

		/// <summary>Returns whether the collection contains the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>true if the collection contains the given <see cref="T:System.ComponentModel.PropertyDescriptor" />; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to find in the collection. </param>
		// Token: 0x06000FAC RID: 4012 RVA: 0x00042A3C File Offset: 0x00040C3C
		public bool Contains(PropertyDescriptor value)
		{
			return this.IndexOf(value) >= 0;
		}

		/// <summary>Copies the entire collection to an array, starting at the specified index number.</summary>
		/// <param name="array">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to copy elements of the collection to. </param>
		/// <param name="index">The index of the <paramref name="array" /> parameter at which copying begins. </param>
		// Token: 0x06000FAD RID: 4013 RVA: 0x00042A4B File Offset: 0x00040C4B
		public void CopyTo(Array array, int index)
		{
			this.EnsurePropsOwned();
			Array.Copy(this._properties, 0, array, index, this.Count);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00042A68 File Offset: 0x00040C68
		private void EnsurePropsOwned()
		{
			if (!this._propsOwned)
			{
				this._propsOwned = true;
				if (this._properties != null)
				{
					PropertyDescriptor[] array = new PropertyDescriptor[this.Count];
					Array.Copy(this._properties, 0, array, 0, this.Count);
					this._properties = array;
				}
			}
			if (this._needSort)
			{
				this._needSort = false;
				this.InternalSort(this._namedSort);
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00042AD0 File Offset: 0x00040CD0
		private void EnsureSize(int sizeNeeded)
		{
			if (sizeNeeded <= this._properties.Length)
			{
				return;
			}
			if (this._properties.Length == 0)
			{
				this.Count = 0;
				this._properties = new PropertyDescriptor[sizeNeeded];
				return;
			}
			this.EnsurePropsOwned();
			PropertyDescriptor[] array = new PropertyDescriptor[Math.Max(sizeNeeded, this._properties.Length * 2)];
			Array.Copy(this._properties, 0, array, 0, this.Count);
			this._properties = array;
		}

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, using a Boolean to indicate whether to ignore case.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, or null if the property does not exist.</returns>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to return from the collection. </param>
		/// <param name="ignoreCase">true if you want to ignore the case of the property name; otherwise, false. </param>
		// Token: 0x06000FB0 RID: 4016 RVA: 0x00042B40 File Offset: 0x00040D40
		public virtual PropertyDescriptor Find(string name, bool ignoreCase)
		{
			object internalSyncObject = this._internalSyncObject;
			PropertyDescriptor propertyDescriptor2;
			lock (internalSyncObject)
			{
				PropertyDescriptor propertyDescriptor = null;
				if (this._cachedFoundProperties == null || this._cachedIgnoreCase != ignoreCase)
				{
					this._cachedIgnoreCase = ignoreCase;
					if (ignoreCase)
					{
						this._cachedFoundProperties = new Hashtable(StringComparer.OrdinalIgnoreCase);
					}
					else
					{
						this._cachedFoundProperties = new Hashtable();
					}
				}
				object obj = this._cachedFoundProperties[name];
				if (obj != null)
				{
					propertyDescriptor2 = (PropertyDescriptor)obj;
				}
				else
				{
					for (int i = 0; i < this.Count; i++)
					{
						if (ignoreCase)
						{
							if (string.Equals(this._properties[i].Name, name, StringComparison.OrdinalIgnoreCase))
							{
								this._cachedFoundProperties[name] = this._properties[i];
								propertyDescriptor = this._properties[i];
								break;
							}
						}
						else if (this._properties[i].Name.Equals(name))
						{
							this._cachedFoundProperties[name] = this._properties[i];
							propertyDescriptor = this._properties[i];
							break;
						}
					}
					propertyDescriptor2 = propertyDescriptor;
				}
			}
			return propertyDescriptor2;
		}

		/// <summary>Returns the index of the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <returns>The index of the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to return the index of. </param>
		// Token: 0x06000FB1 RID: 4017 RVA: 0x00042C60 File Offset: 0x00040E60
		public int IndexOf(PropertyDescriptor value)
		{
			return Array.IndexOf<PropertyDescriptor>(this._properties, value, 0, this.Count);
		}

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the collection at the specified index number.</summary>
		/// <param name="index">The index at which to add the <paramref name="value" /> parameter to the collection. </param>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000FB2 RID: 4018 RVA: 0x00042C78 File Offset: 0x00040E78
		public void Insert(int index, PropertyDescriptor value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.Count + 1);
			if (index < this.Count)
			{
				Array.Copy(this._properties, index, this._properties, index + 1, this.Count - index);
			}
			this._properties[index] = value;
			int count = this.Count;
			this.Count = count + 1;
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000FB3 RID: 4019 RVA: 0x00042CE0 File Offset: 0x00040EE0
		public void Remove(PropertyDescriptor value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			int num = this.IndexOf(value);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the collection. </param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000FB4 RID: 4020 RVA: 0x00042D10 File Offset: 0x00040F10
		public void RemoveAt(int index)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException();
			}
			if (index < this.Count - 1)
			{
				Array.Copy(this._properties, index + 1, this._properties, index, this.Count - index - 1);
			}
			this._properties[this.Count - 1] = null;
			int count = this.Count;
			this.Count = count - 1;
		}

		/// <summary>Sorts the members of this collection. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		// Token: 0x06000FB5 RID: 4021 RVA: 0x00042D75 File Offset: 0x00040F75
		public virtual PropertyDescriptorCollection Sort(string[] names)
		{
			return new PropertyDescriptorCollection(this._properties, this.Count, names, this._comparer);
		}

		/// <summary>Sorts the members of this collection. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		// Token: 0x06000FB6 RID: 4022 RVA: 0x00042D90 File Offset: 0x00040F90
		protected void InternalSort(string[] names)
		{
			if (this._properties.Length == 0)
			{
				return;
			}
			this.InternalSort(this._comparer);
			if (names != null && names.Length != 0)
			{
				List<PropertyDescriptor> list = new List<PropertyDescriptor>(this._properties);
				int num = 0;
				int num2 = this._properties.Length;
				for (int i = 0; i < names.Length; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						PropertyDescriptor propertyDescriptor = list[j];
						if (propertyDescriptor != null && propertyDescriptor.Name.Equals(names[i]))
						{
							this._properties[num++] = propertyDescriptor;
							list[j] = null;
							break;
						}
					}
				}
				for (int k = 0; k < num2; k++)
				{
					if (list[k] != null)
					{
						this._properties[num++] = list[k];
					}
				}
			}
		}

		/// <summary>Sorts the members of this collection, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="sorter">A comparer to use to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection. </param>
		// Token: 0x06000FB7 RID: 4023 RVA: 0x00042E5B File Offset: 0x0004105B
		protected void InternalSort(IComparer sorter)
		{
			if (sorter == null)
			{
				TypeDescriptor.SortDescriptorArray(this);
				return;
			}
			Array.Sort(this._properties, sorter);
		}

		/// <summary>Returns an enumerator for this class.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06000FB8 RID: 4024 RVA: 0x00042E74 File Offset: 0x00041074
		public virtual IEnumerator GetEnumerator()
		{
			this.EnsurePropsOwned();
			if (this._properties.Length != this.Count)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[this.Count];
				Array.Copy(this._properties, 0, array, 0, this.Count);
				return array.GetEnumerator();
			}
			return this._properties.GetEnumerator();
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the collection is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x000028AE File Offset: 0x00000AAE
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x000027B6 File Offset: 0x000009B6
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00042EC9 File Offset: 0x000410C9
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000FBC RID: 4028 RVA: 0x00042ED1 File Offset: 0x000410D1
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.IDictionary" />. </summary>
		// Token: 0x06000FBD RID: 4029 RVA: 0x00042ED1 File Offset: 0x000410D1
		void IDictionary.Clear()
		{
			this.Clear();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x06000FBE RID: 4030 RVA: 0x00042ED9 File Offset: 0x000410D9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000FBF RID: 4031 RVA: 0x00042EE1 File Offset: 0x000410E1
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000FC0 RID: 4032 RVA: 0x00042EEC File Offset: 0x000410EC
		void IDictionary.Add(object key, object value)
		{
			PropertyDescriptor propertyDescriptor = value as PropertyDescriptor;
			if (propertyDescriptor == null)
			{
				throw new ArgumentException("value");
			}
			this.Add(propertyDescriptor);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		// Token: 0x06000FC1 RID: 4033 RVA: 0x00042F16 File Offset: 0x00041116
		bool IDictionary.Contains(object key)
		{
			return key is string && this[(string)key] != null;
		}

		/// <summary>Returns an enumerator for this class.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06000FC2 RID: 4034 RVA: 0x00042F31 File Offset: 0x00041131
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new PropertyDescriptorCollection.PropertyDescriptorEnumerator(this);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00042F39 File Offset: 0x00041139
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, false.</returns>
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00042F39 File Offset: 0x00041139
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets or sets the element with the specified key. </summary>
		/// <returns>The element with the specified key.</returns>
		/// <param name="key">The key of the element to get or set. </param>
		// Token: 0x17000359 RID: 857
		object IDictionary.this[object key]
		{
			get
			{
				if (key is string)
				{
					return this[(string)key];
				}
				return null;
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException();
				}
				if (value != null && !(value is PropertyDescriptor))
				{
					throw new ArgumentException("value");
				}
				int num = -1;
				if (key is int)
				{
					num = (int)key;
					if (num < 0 || num >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
				}
				else
				{
					if (!(key is string))
					{
						throw new ArgumentException("key");
					}
					for (int i = 0; i < this.Count; i++)
					{
						if (this._properties[i].Name.Equals((string)key))
						{
							num = i;
							break;
						}
					}
				}
				if (num == -1)
				{
					this.Add((PropertyDescriptor)value);
					return;
				}
				this.EnsurePropsOwned();
				this._properties[num] = (PropertyDescriptor)value;
				if (this._cachedFoundProperties != null && key is string)
				{
					this._cachedFoundProperties[key] = value;
				}
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00043038 File Offset: 0x00041238
		ICollection IDictionary.Keys
		{
			get
			{
				string[] array = new string[this.Count];
				for (int i = 0; i < this.Count; i++)
				{
					array[i] = this._properties[i].Name;
				}
				return array;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00043074 File Offset: 0x00041274
		ICollection IDictionary.Values
		{
			get
			{
				if (this._properties.Length != this.Count)
				{
					PropertyDescriptor[] array = new PropertyDescriptor[this.Count];
					Array.Copy(this._properties, 0, array, 0, this.Count);
					return array;
				}
				return (ICollection)this._properties.Clone();
			}
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />. </summary>
		/// <param name="key">The key of the element to remove.</param>
		// Token: 0x06000FC9 RID: 4041 RVA: 0x000430C4 File Offset: 0x000412C4
		void IDictionary.Remove(object key)
		{
			if (key is string)
			{
				PropertyDescriptor propertyDescriptor = this[(string)key];
				if (propertyDescriptor != null)
				{
					((IList)this).Remove(propertyDescriptor);
				}
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The item to add to the collection.</param>
		// Token: 0x06000FCA RID: 4042 RVA: 0x000430F0 File Offset: 0x000412F0
		int IList.Add(object value)
		{
			return this.Add((PropertyDescriptor)value);
		}

		/// <summary>Determines whether the collection contains a specific value.</summary>
		/// <returns>true if the item is found in the collection; otherwise, false.</returns>
		/// <param name="value">The item to locate in the collection.</param>
		// Token: 0x06000FCB RID: 4043 RVA: 0x000430FE File Offset: 0x000412FE
		bool IList.Contains(object value)
		{
			return this.Contains((PropertyDescriptor)value);
		}

		/// <summary>Determines the index of a specified item in the collection.</summary>
		/// <returns>The index of <paramref name="value" /> if found in the list, otherwise -1.</returns>
		/// <param name="value">The item to locate in the collection.</param>
		// Token: 0x06000FCC RID: 4044 RVA: 0x0004310C File Offset: 0x0004130C
		int IList.IndexOf(object value)
		{
			return this.IndexOf((PropertyDescriptor)value);
		}

		/// <summary>Inserts an item into the collection at a specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The item to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000FCD RID: 4045 RVA: 0x0004311A File Offset: 0x0004131A
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (PropertyDescriptor)value);
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x00042F39 File Offset: 0x00041139
		bool IList.IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00042F39 File Offset: 0x00041139
		bool IList.IsFixedSize
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Removes the first occurrence of a specified value from the collection.</summary>
		/// <param name="value">The item to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06000FD0 RID: 4048 RVA: 0x00043129 File Offset: 0x00041329
		void IList.Remove(object value)
		{
			this.Remove((PropertyDescriptor)value);
		}

		/// <summary>Gets or sets an item from the collection at a specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the item to get or set.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.ComponentModel.PropertyDescriptor" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0. -or-<paramref name="index" /> is equal to or greater than <see cref="P:System.ComponentModel.EventDescriptorCollection.Count" />.</exception>
		// Token: 0x1700035E RID: 862
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException();
				}
				if (index >= this.Count)
				{
					throw new IndexOutOfRangeException();
				}
				if (value != null && !(value is PropertyDescriptor))
				{
					throw new ArgumentException("value");
				}
				this.EnsurePropsOwned();
				this._properties[index] = (PropertyDescriptor)value;
			}
		}

		/// <summary>Specifies an empty collection that you can use instead of creating a new one with no items. This static field is read-only.</summary>
		// Token: 0x04000A27 RID: 2599
		public static readonly PropertyDescriptorCollection Empty = new PropertyDescriptorCollection(null, true);

		// Token: 0x04000A28 RID: 2600
		private IDictionary _cachedFoundProperties;

		// Token: 0x04000A29 RID: 2601
		private bool _cachedIgnoreCase;

		// Token: 0x04000A2A RID: 2602
		private PropertyDescriptor[] _properties;

		// Token: 0x04000A2B RID: 2603
		private readonly string[] _namedSort;

		// Token: 0x04000A2C RID: 2604
		private readonly IComparer _comparer;

		// Token: 0x04000A2D RID: 2605
		private bool _propsOwned;

		// Token: 0x04000A2E RID: 2606
		private bool _needSort;

		// Token: 0x04000A2F RID: 2607
		private bool _readOnly;

		// Token: 0x04000A30 RID: 2608
		private readonly object _internalSyncObject = new object();

		// Token: 0x02000292 RID: 658
		private class PropertyDescriptorEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06000FD4 RID: 4052 RVA: 0x000431A2 File Offset: 0x000413A2
			public PropertyDescriptorEnumerator(PropertyDescriptorCollection owner)
			{
				this._owner = owner;
			}

			// Token: 0x1700035F RID: 863
			// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x000431B8 File Offset: 0x000413B8
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x17000360 RID: 864
			// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x000431C8 File Offset: 0x000413C8
			public DictionaryEntry Entry
			{
				get
				{
					PropertyDescriptor propertyDescriptor = this._owner[this._index];
					return new DictionaryEntry(propertyDescriptor.Name, propertyDescriptor);
				}
			}

			// Token: 0x17000361 RID: 865
			// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x000431F3 File Offset: 0x000413F3
			public object Key
			{
				get
				{
					return this._owner[this._index].Name;
				}
			}

			// Token: 0x17000362 RID: 866
			// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x000431F3 File Offset: 0x000413F3
			public object Value
			{
				get
				{
					return this._owner[this._index].Name;
				}
			}

			// Token: 0x06000FD9 RID: 4057 RVA: 0x0004320B File Offset: 0x0004140B
			public bool MoveNext()
			{
				if (this._index < this._owner.Count - 1)
				{
					this._index++;
					return true;
				}
				return false;
			}

			// Token: 0x06000FDA RID: 4058 RVA: 0x00043233 File Offset: 0x00041433
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x04000A32 RID: 2610
			private PropertyDescriptorCollection _owner;

			// Token: 0x04000A33 RID: 2611
			private int _index = -1;
		}
	}
}
