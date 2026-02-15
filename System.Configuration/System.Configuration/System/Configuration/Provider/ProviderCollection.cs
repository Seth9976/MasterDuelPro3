using System;
using System.Collections;

namespace System.Configuration.Provider
{
	/// <summary>Represents a collection of provider objects that inherit from <see cref="T:System.Configuration.Provider.ProviderBase" />.</summary>
	// Token: 0x02000047 RID: 71
	public class ProviderCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Provider.ProviderCollection" /> class. </summary>
		// Token: 0x060001D5 RID: 469 RVA: 0x00007C14 File Offset: 0x00005E14
		public ProviderCollection()
		{
			this.lookup = new Hashtable(10, StringComparer.InvariantCultureIgnoreCase);
			this.values = new ArrayList();
		}

		/// <summary>Adds a provider to the collection.</summary>
		/// <param name="provider">The provider to be added.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="provider" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> of <paramref name="provider" /> is null.- or -The length of the <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> of <paramref name="provider" /> is less than 1.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060001D6 RID: 470 RVA: 0x00007C3C File Offset: 0x00005E3C
		public virtual void Add(ProviderBase provider)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			if (provider == null || provider.Name == null)
			{
				throw new ArgumentNullException();
			}
			int num = this.values.Add(provider);
			try
			{
				this.lookup.Add(provider.Name, num);
			}
			catch
			{
				this.values.RemoveAt(num);
				throw;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Configuration.Provider.ProviderCollection" /> to an array, starting at a particular array index.</summary>
		/// <param name="array">The array to copy the elements of the collection to.</param>
		/// <param name="index">The index of the array at which to start copying provider instances from the collection.</param>
		// Token: 0x060001D7 RID: 471 RVA: 0x00007CB0 File Offset: 0x00005EB0
		void ICollection.CopyTo(Array array, int index)
		{
			this.values.CopyTo(array, index);
		}

		/// <summary>Returns an object that implements the <see cref="T:System.Collections.IEnumerator" /> interface to iterate through the collection.</summary>
		/// <returns>An object that implements <see cref="T:System.Collections.IEnumerator" /> to iterate through the collection.</returns>
		// Token: 0x060001D8 RID: 472 RVA: 0x00007CBF File Offset: 0x00005EBF
		public IEnumerator GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		/// <summary>Gets the number of providers in the collection.</summary>
		/// <returns>The number of providers in the collection.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00007CCC File Offset: 0x00005ECC
		public int Count
		{
			get
			{
				return this.values.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000329C File Offset: 0x0000149C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the current object.</summary>
		/// <returns>The current object.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00003C40 File Offset: 0x00001E40
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the provider with the specified name.</summary>
		/// <returns>The provider with the specified name.</returns>
		/// <param name="name">The key by which the provider is identified.</param>
		// Token: 0x17000095 RID: 149
		public ProviderBase this[string name]
		{
			get
			{
				object obj = this.lookup[name];
				if (obj == null)
				{
					return null;
				}
				return this.values[(int)obj] as ProviderBase;
			}
		}

		// Token: 0x040000EE RID: 238
		private Hashtable lookup;

		// Token: 0x040000EF RID: 239
		private bool readOnly;

		// Token: 0x040000F0 RID: 240
		private ArrayList values;
	}
}
