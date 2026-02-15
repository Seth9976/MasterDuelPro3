using System;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an X.509 store, which is a physical store where certificates are persisted and managed. This class cannot be inherited.</summary>
	// Token: 0x020001D3 RID: 467
	public sealed class X509Store : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> and <see cref="T:System.Security.Cryptography.X509Certificates.StoreLocation" /> values.</summary>
		/// <param name="storeName">One of the enumeration values that specifies the name of the X.509 certificate store. </param>
		/// <param name="storeLocation">One of the enumeration values that specifies the location of the X.509 certificate store. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="storeLocation" /> is not a valid location or <paramref name="storeName" /> is not a valid name. </exception>
		// Token: 0x06000B67 RID: 2919 RVA: 0x00039784 File Offset: 0x00037984
		public X509Store(StoreName storeName, StoreLocation storeLocation)
		{
			if (storeName < StoreName.AddressBook || storeName > StoreName.TrustedPublisher)
			{
				throw new ArgumentException("storeName");
			}
			if (storeLocation < StoreLocation.CurrentUser || storeLocation > StoreLocation.LocalMachine)
			{
				throw new ArgumentException("storeLocation");
			}
			if (storeName == StoreName.CertificateAuthority)
			{
				this._name = "CA";
			}
			else
			{
				this._name = storeName.ToString();
			}
			this._location = storeLocation;
		}

		/// <summary>Returns a collection of certificates located in an X.509 certificate store.</summary>
		/// <returns>A collection of certificates.</returns>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x000397E8 File Offset: 0x000379E8
		public X509Certificate2Collection Certificates
		{
			get
			{
				if (this.list == null)
				{
					this.list = new X509Certificate2Collection();
				}
				else if (this.store == null)
				{
					this.list.Clear();
				}
				return this.list;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00039818 File Offset: 0x00037A18
		private X509Stores Factory
		{
			get
			{
				if (this._location == StoreLocation.CurrentUser)
				{
					return X509StoreManager.CurrentUser;
				}
				return X509StoreManager.LocalMachine;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0003982E File Offset: 0x00037A2E
		internal X509Store Store
		{
			get
			{
				return this.store;
			}
		}

		/// <summary>Closes an X.509 certificate store.</summary>
		// Token: 0x06000B6B RID: 2923 RVA: 0x00039836 File Offset: 0x00037A36
		public void Close()
		{
			this.store = null;
			if (this.list != null)
			{
				this.list.Clear();
			}
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00039852 File Offset: 0x00037A52
		public void Dispose()
		{
			this.Close();
		}

		/// <summary>Opens an X.509 certificate store or creates a new store, depending on <see cref="T:System.Security.Cryptography.X509Certificates.OpenFlags" /> flag settings.</summary>
		/// <param name="flags">A bitwise combination of enumeration values that specifies the way to open the X.509 certificate store. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The store is unreadable. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">The store contains invalid values.</exception>
		// Token: 0x06000B6D RID: 2925 RVA: 0x0003985C File Offset: 0x00037A5C
		public void Open(OpenFlags flags)
		{
			if (string.IsNullOrEmpty(this._name))
			{
				throw new CryptographicException(global::Locale.GetText("Invalid store name (null or empty)."));
			}
			string text;
			if (this._name == "Root")
			{
				text = "Trust";
			}
			else
			{
				text = this._name;
			}
			bool flag = (flags & OpenFlags.OpenExistingOnly) != OpenFlags.OpenExistingOnly;
			this.store = this.Factory.Open(text, flag);
			if (this.store == null)
			{
				throw new CryptographicException(global::Locale.GetText("Store {0} doesn't exists.", new object[] { this._name }));
			}
			this._flags = flags;
			foreach (X509Certificate x509Certificate in this.store.Certificates)
			{
				X509Certificate2 x509Certificate2 = new X509Certificate2(x509Certificate.RawData);
				x509Certificate2.Impl.PrivateKey = x509Certificate.RSA;
				this.Certificates.Add(x509Certificate2);
			}
		}

		// Token: 0x0400085D RID: 2141
		private string _name;

		// Token: 0x0400085E RID: 2142
		private StoreLocation _location;

		// Token: 0x0400085F RID: 2143
		private X509Certificate2Collection list;

		// Token: 0x04000860 RID: 2144
		private OpenFlags _flags;

		// Token: 0x04000861 RID: 2145
		private X509Store store;
	}
}
