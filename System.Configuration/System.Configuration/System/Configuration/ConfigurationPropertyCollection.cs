using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Configuration
{
	/// <summary>Represents a collection of configuration-element properties.</summary>
	// Token: 0x0200001C RID: 28
	public class ConfigurationPropertyCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> class. </summary>
		// Token: 0x060000EB RID: 235 RVA: 0x0000532D File Offset: 0x0000352D
		public ConfigurationPropertyCollection()
		{
			this.collection = new List<ConfigurationProperty>();
		}

		/// <summary>Gets the number of properties in the collection.</summary>
		/// <returns>The number of properties in the collection.</returns>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005340 File Offset: 0x00003540
		public int Count
		{
			get
			{
				return this.collection.Count;
			}
		}

		/// <summary>Gets the collection item with the specified name.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationProperty" /> with the specified <paramref name="name" />.</returns>
		/// <param name="name">The <see cref="T:System.Configuration.ConfigurationProperty" /> to return. </param>
		// Token: 0x1700004A RID: 74
		public ConfigurationProperty this[string name]
		{
			get
			{
				foreach (ConfigurationProperty configurationProperty in this.collection)
				{
					if (configurationProperty.Name == name)
					{
						return configurationProperty;
					}
				}
				return null;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> is synchronized; otherwise, false.</returns>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000329C File Offset: 0x0000149C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the object to synchronize access to the collection.</summary>
		/// <returns>The object to synchronize access to the collection.</returns>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000053B4 File Offset: 0x000035B4
		public object SyncRoot
		{
			get
			{
				return this.collection;
			}
		}

		/// <summary>Adds a configuration property to the collection.</summary>
		/// <param name="property">The <see cref="T:System.Configuration.ConfigurationProperty" />  to add. </param>
		// Token: 0x060000F0 RID: 240 RVA: 0x000053BC File Offset: 0x000035BC
		public void Add(ConfigurationProperty property)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			this.collection.Add(property);
		}

		/// <summary>Copies this collection to an array.</summary>
		/// <param name="array">The array to which to copy.</param>
		/// <param name="index">The index location at which to begin copying.</param>
		// Token: 0x060000F1 RID: 241 RVA: 0x000053D8 File Offset: 0x000035D8
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.collection).CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection</returns>
		// Token: 0x060000F2 RID: 242 RVA: 0x000053E7 File Offset: 0x000035E7
		public IEnumerator GetEnumerator()
		{
			return this.collection.GetEnumerator();
		}

		/// <summary>Removes a configuration property from the collection.</summary>
		/// <returns>true if the specified <see cref="T:System.Configuration.ConfigurationProperty" /> was removed; otherwise, false.</returns>
		/// <param name="name">The <see cref="T:System.Configuration.ConfigurationProperty" /> to remove. </param>
		// Token: 0x060000F3 RID: 243 RVA: 0x000053F9 File Offset: 0x000035F9
		public bool Remove(string name)
		{
			return this.collection.Remove(this[name]);
		}

		// Token: 0x04000071 RID: 113
		private List<ConfigurationProperty> collection;
	}
}
