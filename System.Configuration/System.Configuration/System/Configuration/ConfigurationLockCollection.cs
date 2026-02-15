using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Contains a collection of locked configuration objects. This class cannot be inherited.</summary>
	// Token: 0x02000018 RID: 24
	public sealed class ConfigurationLockCollection : ICollection, IEnumerable
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004BFC File Offset: 0x00002DFC
		internal ConfigurationLockCollection(ConfigurationElement element, ConfigurationLockType lockType)
		{
			this.names = new ArrayList();
			this.element = element;
			this.lockType = lockType;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004C20 File Offset: 0x00002E20
		private void CheckName(string name)
		{
			bool flag = (this.lockType & ConfigurationLockType.Attribute) == ConfigurationLockType.Attribute;
			if (this.valid_name_hash == null)
			{
				this.valid_name_hash = new Hashtable();
				foreach (object obj in this.element.Properties)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
					if (flag != configurationProperty.IsElement)
					{
						this.valid_name_hash.Add(configurationProperty.Name, true);
					}
				}
				if (!flag)
				{
					ConfigurationElementCollection defaultCollection = this.element.GetDefaultCollection();
					this.valid_name_hash.Add(defaultCollection.AddElementName, true);
					this.valid_name_hash.Add(defaultCollection.ClearElementName, true);
					this.valid_name_hash.Add(defaultCollection.RemoveElementName, true);
				}
				string[] array = new string[this.valid_name_hash.Keys.Count];
				this.valid_name_hash.Keys.CopyTo(array, 0);
				this.valid_names = string.Join(",", array);
			}
			if (this.valid_name_hash[name] == null)
			{
				throw new ConfigurationErrorsException(string.Format("The {2} '{0}' is not valid in the locked list for this section.  The following {3} can be locked: '{1}'", new object[]
				{
					name,
					this.valid_names,
					flag ? "attribute" : "element",
					flag ? "attributes" : "elements"
				}));
			}
		}

		/// <summary>Locks a configuration object by adding it to the collection.</summary>
		/// <param name="name">The name of the configuration object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs when the <paramref name="name" /> does not match an existing configuration object within the collection.</exception>
		// Token: 0x060000C0 RID: 192 RVA: 0x00004DA4 File Offset: 0x00002FA4
		public void Add(string name)
		{
			this.CheckName(name);
			if (!this.names.Contains(name))
			{
				this.names.Add(name);
				this.is_modified = true;
			}
		}

		/// <summary>Clears all configuration objects from the collection.</summary>
		// Token: 0x060000C1 RID: 193 RVA: 0x00004DCF File Offset: 0x00002FCF
		public void Clear()
		{
			this.names.Clear();
			this.is_modified = true;
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> object, which is used to iterate through this <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object.</returns>
		// Token: 0x060000C2 RID: 194 RVA: 0x00004DE3 File Offset: 0x00002FE3
		public IEnumerator GetEnumerator()
		{
			return this.names.GetEnumerator();
		}

		/// <summary>Locks a set of configuration objects based on the supplied list.</summary>
		/// <param name="attributeList">A comma-delimited string.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs when an item in the <paramref name="attributeList" /> parameter is not a valid lockable configuration attribute.</exception>
		// Token: 0x060000C3 RID: 195 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public void SetFromList(string attributeList)
		{
			this.Clear();
			char[] array = new char[] { ',' };
			foreach (string text in attributeList.Split(array))
			{
				this.Add(text.Trim());
			}
		}

		/// <summary>Copies the entire <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060000C4 RID: 196 RVA: 0x00004E35 File Offset: 0x00003035
		void ICollection.CopyTo(Array array, int index)
		{
			this.names.CopyTo(array, index);
		}

		/// <summary>Gets the number of locked configuration objects contained in the collection.</summary>
		/// <returns>The number of locked configuration objects contained in the collection.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004E44 File Offset: 0x00003044
		public int Count
		{
			get
			{
				return this.names.Count;
			}
		}

		/// <summary>Gets a value specifying whether the collection is synchronized.</summary>
		/// <returns>true if the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection is synchronized; otherwise, false.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000329C File Offset: 0x0000149C
		[MonoTODO]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object used to synchronize access to this <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection.</summary>
		/// <returns>An object used to synchronize access to this <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003C40 File Offset: 0x00001E40
		[MonoTODO]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0400005C RID: 92
		private ArrayList names;

		// Token: 0x0400005D RID: 93
		private ConfigurationElement element;

		// Token: 0x0400005E RID: 94
		private ConfigurationLockType lockType;

		// Token: 0x0400005F RID: 95
		private bool is_modified;

		// Token: 0x04000060 RID: 96
		private Hashtable valid_name_hash;

		// Token: 0x04000061 RID: 97
		private string valid_names;
	}
}
