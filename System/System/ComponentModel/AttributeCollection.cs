using System;
using System.Collections;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of attributes.</summary>
	// Token: 0x02000255 RID: 597
	public class AttributeCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeCollection" /> class.</summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that provides the attributes for this collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributes" /> is null.</exception>
		// Token: 0x06000E3C RID: 3644 RVA: 0x0003ED0C File Offset: 0x0003CF0C
		public AttributeCollection(params Attribute[] attributes)
		{
			this._attributes = attributes ?? Array.Empty<Attribute>();
			for (int i = 0; i < this._attributes.Length; i++)
			{
				if (this._attributes[i] == null)
				{
					throw new ArgumentNullException("attributes");
				}
			}
		}

		/// <summary>Gets the attribute collection.</summary>
		/// <returns>The attribute collection.</returns>
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0003ED57 File Offset: 0x0003CF57
		protected virtual Attribute[] Attributes
		{
			get
			{
				return this._attributes;
			}
		}

		/// <summary>Gets the number of attributes.</summary>
		/// <returns>The number of attributes.</returns>
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0003ED5F File Offset: 0x0003CF5F
		public int Count
		{
			get
			{
				return this.Attributes.Length;
			}
		}

		/// <summary>Gets the attribute with the specified type.</summary>
		/// <returns>The <see cref="T:System.Attribute" /> with the specified type or, if the attribute does not exist, the default value for the attribute type.</returns>
		/// <param name="attributeType">The <see cref="T:System.Type" /> of the <see cref="T:System.Attribute" /> to get from the collection. </param>
		// Token: 0x17000303 RID: 771
		public virtual Attribute this[Type attributeType]
		{
			get
			{
				object obj = AttributeCollection.s_internalSyncObject;
				Attribute defaultAttribute;
				lock (obj)
				{
					if (this._foundAttributeTypes == null)
					{
						this._foundAttributeTypes = new AttributeCollection.AttributeEntry[5];
					}
					int i = 0;
					while (i < 5)
					{
						if (this._foundAttributeTypes[i].type == attributeType)
						{
							int index = this._foundAttributeTypes[i].index;
							if (index != -1)
							{
								return this.Attributes[index];
							}
							return this.GetDefaultAttribute(attributeType);
						}
						else
						{
							if (this._foundAttributeTypes[i].type == null)
							{
								break;
							}
							i++;
						}
					}
					int index2 = this._index;
					this._index = index2 + 1;
					i = index2;
					if (this._index >= 5)
					{
						this._index = 0;
					}
					this._foundAttributeTypes[i].type = attributeType;
					int num = this.Attributes.Length;
					for (int j = 0; j < num; j++)
					{
						Attribute attribute = this.Attributes[j];
						if (attribute.GetType() == attributeType)
						{
							this._foundAttributeTypes[i].index = j;
							return attribute;
						}
					}
					for (int k = 0; k < num; k++)
					{
						Attribute attribute2 = this.Attributes[k];
						if (attributeType.IsInstanceOfType(attribute2))
						{
							this._foundAttributeTypes[i].index = k;
							return attribute2;
						}
					}
					this._foundAttributeTypes[i].index = -1;
					defaultAttribute = this.GetDefaultAttribute(attributeType);
				}
				return defaultAttribute;
			}
		}

		/// <summary>Determines whether this collection of attributes has the specified attribute.</summary>
		/// <returns>true if the collection contains the attribute or is the default attribute for the type of attribute; otherwise, false.</returns>
		/// <param name="attribute">An <see cref="T:System.Attribute" /> to find in the collection. </param>
		// Token: 0x06000E40 RID: 3648 RVA: 0x0003EF18 File Offset: 0x0003D118
		public bool Contains(Attribute attribute)
		{
			Attribute attribute2 = this[attribute.GetType()];
			return attribute2 != null && attribute2.Equals(attribute);
		}

		/// <summary>Returns the default <see cref="T:System.Attribute" /> of a given <see cref="T:System.Type" />.</summary>
		/// <returns>The default <see cref="T:System.Attribute" /> of a given <paramref name="attributeType" />.</returns>
		/// <param name="attributeType">The <see cref="T:System.Type" /> of the attribute to retrieve. </param>
		// Token: 0x06000E41 RID: 3649 RVA: 0x0003EF40 File Offset: 0x0003D140
		protected Attribute GetDefaultAttribute(Type attributeType)
		{
			object obj = AttributeCollection.s_internalSyncObject;
			Attribute attribute;
			lock (obj)
			{
				if (AttributeCollection.s_defaultAttributes == null)
				{
					AttributeCollection.s_defaultAttributes = new Hashtable();
				}
				if (AttributeCollection.s_defaultAttributes.ContainsKey(attributeType))
				{
					attribute = (Attribute)AttributeCollection.s_defaultAttributes[attributeType];
				}
				else
				{
					Attribute attribute2 = null;
					Type reflectionType = TypeDescriptor.GetReflectionType(attributeType);
					FieldInfo field = reflectionType.GetField("Default", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
					if (field != null && field.IsStatic)
					{
						attribute2 = (Attribute)field.GetValue(null);
					}
					else
					{
						ConstructorInfo constructor = reflectionType.UnderlyingSystemType.GetConstructor(Array.Empty<Type>());
						if (constructor != null)
						{
							attribute2 = (Attribute)constructor.Invoke(Array.Empty<object>());
							if (!attribute2.IsDefaultAttribute())
							{
								attribute2 = null;
							}
						}
					}
					AttributeCollection.s_defaultAttributes[attributeType] = attribute2;
					attribute = attribute2;
				}
			}
			return attribute;
		}

		/// <summary>Gets an enumerator for this collection.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06000E42 RID: 3650 RVA: 0x0003F038 File Offset: 0x0003D238
		public IEnumerator GetEnumerator()
		{
			return this.Attributes.GetEnumerator();
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>true if access to the collection is synchronized (thread-safe); otherwise, false.</returns>
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x000028AE File Offset: 0x00000AAE
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x000027B6 File Offset: 0x000009B6
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0003F045 File Offset: 0x0003D245
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x06000E46 RID: 3654 RVA: 0x0003F04D File Offset: 0x0003D24D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Copies the collection to an array, starting at the specified index.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> to copy the collection to. </param>
		/// <param name="index">The index to start from. </param>
		// Token: 0x06000E47 RID: 3655 RVA: 0x0003F055 File Offset: 0x0003D255
		public void CopyTo(Array array, int index)
		{
			Array.Copy(this.Attributes, 0, array, index, this.Attributes.Length);
		}

		/// <summary>Specifies an empty collection that you can use, rather than creating a new one. This field is read-only.</summary>
		// Token: 0x040009B6 RID: 2486
		public static readonly AttributeCollection Empty = new AttributeCollection(null);

		// Token: 0x040009B7 RID: 2487
		private static Hashtable s_defaultAttributes;

		// Token: 0x040009B8 RID: 2488
		private readonly Attribute[] _attributes;

		// Token: 0x040009B9 RID: 2489
		private static readonly object s_internalSyncObject = new object();

		// Token: 0x040009BA RID: 2490
		private AttributeCollection.AttributeEntry[] _foundAttributeTypes;

		// Token: 0x040009BB RID: 2491
		private int _index;

		// Token: 0x02000256 RID: 598
		private struct AttributeEntry
		{
			// Token: 0x040009BC RID: 2492
			public Type type;

			// Token: 0x040009BD RID: 2493
			public int index;
		}
	}
}
