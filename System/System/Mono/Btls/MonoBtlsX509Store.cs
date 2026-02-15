using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Btls
{
	// Token: 0x020000CA RID: 202
	internal class MonoBtlsX509Store : MonoBtlsObject
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000CBDA File Offset: 0x0000ADDA
		internal new MonoBtlsX509Store.BoringX509StoreHandle Handle
		{
			get
			{
				return (MonoBtlsX509Store.BoringX509StoreHandle)base.Handle;
			}
		}

		// Token: 0x060003AA RID: 938
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_new();

		// Token: 0x060003AB RID: 939
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_from_ssl_ctx(IntPtr handle);

		// Token: 0x060003AC RID: 940
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_store_add_cert(IntPtr handle, IntPtr x509);

		// Token: 0x060003AD RID: 941
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_store_free(IntPtr handle);

		// Token: 0x060003AE RID: 942 RVA: 0x0000CBE7 File Offset: 0x0000ADE7
		private static MonoBtlsX509Store.BoringX509StoreHandle Create_internal()
		{
			IntPtr intPtr = MonoBtlsX509Store.mono_btls_x509_store_new();
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException();
			}
			return new MonoBtlsX509Store.BoringX509StoreHandle(intPtr);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000CC06 File Offset: 0x0000AE06
		private static MonoBtlsX509Store.BoringX509StoreHandle Create_internal(MonoBtlsSslCtx.BoringSslCtxHandle ctx)
		{
			IntPtr intPtr = MonoBtlsX509Store.mono_btls_x509_store_from_ssl_ctx(ctx.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException();
			}
			return new MonoBtlsX509Store.BoringX509StoreHandle(intPtr);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000CC2B File Offset: 0x0000AE2B
		internal MonoBtlsX509Store()
			: base(MonoBtlsX509Store.Create_internal())
		{
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000CC38 File Offset: 0x0000AE38
		internal MonoBtlsX509Store(MonoBtlsSslCtx.BoringSslCtxHandle ctx)
			: base(MonoBtlsX509Store.Create_internal(ctx))
		{
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000CC48 File Offset: 0x0000AE48
		public void AddCertificate(MonoBtlsX509 x509)
		{
			int num = MonoBtlsX509Store.mono_btls_x509_store_add_cert(this.Handle.DangerousGetHandle(), x509.Handle.DangerousGetHandle());
			base.CheckError(num, "AddCertificate");
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000CC80 File Offset: 0x0000AE80
		public MonoBtlsX509Lookup AddLookup(MonoBtlsX509LookupType type)
		{
			if (this.lookupHash == null)
			{
				this.lookupHash = new Dictionary<IntPtr, MonoBtlsX509Lookup>();
			}
			MonoBtlsX509Lookup monoBtlsX509Lookup = new MonoBtlsX509Lookup(this, type);
			IntPtr nativeLookup = monoBtlsX509Lookup.GetNativeLookup();
			if (this.lookupHash.ContainsKey(nativeLookup))
			{
				monoBtlsX509Lookup.Dispose();
				monoBtlsX509Lookup = this.lookupHash[nativeLookup];
			}
			else
			{
				this.lookupHash.Add(nativeLookup, monoBtlsX509Lookup);
			}
			return monoBtlsX509Lookup;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		public void AddDirectoryLookup(string dir, MonoBtlsX509FileType type)
		{
			this.AddLookup(MonoBtlsX509LookupType.HASH_DIR).AddDirectory(dir, type);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		public void AddCollection(X509CertificateCollection collection, MonoBtlsX509TrustKind trust)
		{
			MonoBtlsX509LookupMonoCollection monoBtlsX509LookupMonoCollection = new MonoBtlsX509LookupMonoCollection(collection, trust);
			new MonoBtlsX509Lookup(this, MonoBtlsX509LookupType.MONO).AddMono(monoBtlsX509LookupMonoCollection);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000CD14 File Offset: 0x0000AF14
		protected override void Close()
		{
			try
			{
				if (this.lookupHash != null)
				{
					foreach (MonoBtlsX509Lookup monoBtlsX509Lookup in this.lookupHash.Values)
					{
						monoBtlsX509Lookup.Dispose();
					}
					this.lookupHash = null;
				}
			}
			finally
			{
				base.Close();
			}
		}

		// Token: 0x04000314 RID: 788
		private Dictionary<IntPtr, MonoBtlsX509Lookup> lookupHash;

		// Token: 0x020000CB RID: 203
		internal class BoringX509StoreHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x060003B7 RID: 951 RVA: 0x00009A41 File Offset: 0x00007C41
			public BoringX509StoreHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x060003B8 RID: 952 RVA: 0x0000CD90 File Offset: 0x0000AF90
			protected override bool ReleaseHandle()
			{
				MonoBtlsX509Store.mono_btls_x509_store_free(this.handle);
				return true;
			}
		}
	}
}
