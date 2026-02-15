using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Manages the collection of <see cref="T:System.Windows.Forms.BindingManagerBase" /> objects for any object that inherits from the <see cref="T:System.Windows.Forms.Control" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200001E RID: 30
	[DefaultEvent("CollectionChanged")]
	public class BindingContext : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingContext" /> class.</summary>
		// Token: 0x0600008A RID: 138 RVA: 0x00003AE7 File Offset: 0x00001CE7
		public BindingContext()
		{
			this.managers = new Hashtable();
			this.onCollectionChangedHandler = null;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.BindingManagerBase" /> that is associated with the specified data source.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingManagerBase" /> for the specified data source.</returns>
		/// <param name="dataSource">The data source associated with a particular <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000029 RID: 41
		public BindingManagerBase this[object dataSource]
		{
			get
			{
				return this[dataSource, string.Empty];
			}
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.BindingManagerBase" /> that is associated with the specified data source and data member.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.BindingManagerBase" /> for the specified data source and data member.</returns>
		/// <param name="dataSource">The data source associated with a particular <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
		/// <param name="dataMember">A navigation path containing the information that resolves to a specific <see cref="T:System.Windows.Forms.BindingManagerBase" />. </param>
		/// <exception cref="T:System.Exception">The specified <paramref name="dataMember" /> does not exist within the data source. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700002A RID: 42
		public BindingManagerBase this[object dataSource, string dataMember]
		{
			get
			{
				if (dataSource == null)
				{
					throw new ArgumentNullException("dataSource");
				}
				if (dataMember == null)
				{
					dataMember = string.Empty;
				}
				ICurrencyManagerProvider currencyManagerProvider = dataSource as ICurrencyManagerProvider;
				if (currencyManagerProvider != null)
				{
					if (dataMember.Length == 0)
					{
						return currencyManagerProvider.CurrencyManager;
					}
					return currencyManagerProvider.GetRelatedCurrencyManager(dataMember);
				}
				else
				{
					BindingContext.HashKey hashKey = new BindingContext.HashKey(dataSource, dataMember);
					BindingManagerBase bindingManagerBase = this.managers[hashKey] as BindingManagerBase;
					if (bindingManagerBase != null)
					{
						return bindingManagerBase;
					}
					bindingManagerBase = this.CreateBindingManager(dataSource, dataMember);
					if (bindingManagerBase == null)
					{
						return null;
					}
					this.managers[hashKey] = bindingManagerBase;
					return bindingManagerBase;
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003B94 File Offset: 0x00001D94
		private BindingManagerBase CreateBindingManager(object data_source, string data_member)
		{
			if (data_member == "")
			{
				if (this.IsListType(data_source.GetType()))
				{
					return new CurrencyManager(data_source);
				}
				return new PropertyManager(data_source);
			}
			else
			{
				BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(data_member);
				BindingManagerBase bindingManagerBase = this[data_source, bindingMemberInfo.BindingPath];
				PropertyDescriptor propertyDescriptor = ((bindingManagerBase == null) ? null : bindingManagerBase.GetItemProperties().Find(bindingMemberInfo.BindingField, true));
				if (propertyDescriptor == null)
				{
					throw new ArgumentException(string.Format("Cannot create a child list for field {0}.", bindingMemberInfo.BindingField));
				}
				if (this.IsListType(propertyDescriptor.PropertyType))
				{
					return new RelatedCurrencyManager(bindingManagerBase, propertyDescriptor);
				}
				return new RelatedPropertyManager(bindingManagerBase, bindingMemberInfo.BindingField);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003C38 File Offset: 0x00001E38
		private bool IsListType(Type t)
		{
			return typeof(IList).IsAssignableFrom(t) || typeof(IListSource).IsAssignableFrom(t);
		}

		/// <summary>Copies the elements of the collection into a specified array, starting at the collection index.</summary>
		/// <param name="ar">An <see cref="T:System.Array" /> to copy into. </param>
		/// <param name="index">The collection index to begin copying from. </param>
		// Token: 0x0600008F RID: 143 RVA: 0x00003C5E File Offset: 0x00001E5E
		void ICollection.CopyTo(Array ar, int index)
		{
			this.managers.CopyTo(ar, index);
		}

		/// <summary>Gets the total number of <see cref="T:System.Windows.Forms.CurrencyManager" /> objects managed by the <see cref="T:System.Windows.Forms.BindingContext" />.</summary>
		/// <returns>The number of data sources managed by the <see cref="T:System.Windows.Forms.BindingContext" />.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003C6D File Offset: 0x00001E6D
		int ICollection.Count
		{
			get
			{
				return this.managers.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is synchronized.</summary>
		/// <returns>true if the collection is thread safe; otherwise, false.</returns>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002D70 File Offset: 0x00000F70
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object to use for synchronization (thread safety).</summary>
		/// <returns>This property is derived from <see cref="T:System.Collections.ICollection" />, and is overridden to always return null.</returns>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003C7A File Offset: 0x00001E7A
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets an enumerator for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		// Token: 0x06000093 RID: 147 RVA: 0x00003C7D File Offset: 0x00001E7D
		[MonoInternalNote("our enumerator is slightly different.  in MS's implementation the Values are WeakReferences to the managers.")]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.managers.GetEnumerator();
		}

		// Token: 0x040000BB RID: 187
		private Hashtable managers;

		// Token: 0x040000BC RID: 188
		private EventHandler onCollectionChangedHandler;

		// Token: 0x0200001F RID: 31
		private class HashKey
		{
			// Token: 0x06000094 RID: 148 RVA: 0x00003C8A File Offset: 0x00001E8A
			public HashKey(object source, string member)
			{
				this.source = source;
				this.member = member;
			}

			// Token: 0x06000095 RID: 149 RVA: 0x00003CA0 File Offset: 0x00001EA0
			public override int GetHashCode()
			{
				return this.source.GetHashCode() ^ this.member.GetHashCode();
			}

			// Token: 0x06000096 RID: 150 RVA: 0x00003CBC File Offset: 0x00001EBC
			public override bool Equals(object o)
			{
				BindingContext.HashKey hashKey = o as BindingContext.HashKey;
				return hashKey != null && hashKey.source == this.source && hashKey.member == this.member;
			}

			// Token: 0x040000BD RID: 189
			public object source;

			// Token: 0x040000BE RID: 190
			public string member;
		}
	}
}
