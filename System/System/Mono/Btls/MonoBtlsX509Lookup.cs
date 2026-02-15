using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000C0 RID: 192
	internal class MonoBtlsX509Lookup : MonoBtlsObject
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000C4E9 File Offset: 0x0000A6E9
		internal new MonoBtlsX509Lookup.BoringX509LookupHandle Handle
		{
			get
			{
				return (MonoBtlsX509Lookup.BoringX509LookupHandle)base.Handle;
			}
		}

		// Token: 0x06000376 RID: 886
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_lookup_new(IntPtr store, MonoBtlsX509LookupType type);

		// Token: 0x06000377 RID: 887
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_lookup_add_dir(IntPtr handle, IntPtr dir, MonoBtlsX509FileType type);

		// Token: 0x06000378 RID: 888
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_lookup_add_mono(IntPtr handle, IntPtr monoLookup);

		// Token: 0x06000379 RID: 889
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_lookup_free(IntPtr handle);

		// Token: 0x0600037A RID: 890
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_lookup_peek_lookup(IntPtr handle);

		// Token: 0x0600037B RID: 891 RVA: 0x0000C4F6 File Offset: 0x0000A6F6
		private static MonoBtlsX509Lookup.BoringX509LookupHandle Create_internal(MonoBtlsX509Store store, MonoBtlsX509LookupType type)
		{
			IntPtr intPtr = MonoBtlsX509Lookup.mono_btls_x509_lookup_new(store.Handle.DangerousGetHandle(), type);
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException();
			}
			return new MonoBtlsX509Lookup.BoringX509LookupHandle(intPtr);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000C521 File Offset: 0x0000A721
		internal MonoBtlsX509Lookup(MonoBtlsX509Store store, MonoBtlsX509LookupType type)
			: base(MonoBtlsX509Lookup.Create_internal(store, type))
		{
			this.store = store;
			this.type = type;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000C53E File Offset: 0x0000A73E
		internal IntPtr GetNativeLookup()
		{
			return MonoBtlsX509Lookup.mono_btls_x509_lookup_peek_lookup(this.Handle.DangerousGetHandle());
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000C550 File Offset: 0x0000A750
		public void AddDirectory(string dir, MonoBtlsX509FileType type)
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				if (dir != null)
				{
					intPtr = Marshal.StringToHGlobalAnsi(dir);
				}
				int num = MonoBtlsX509Lookup.mono_btls_x509_lookup_add_dir(this.Handle.DangerousGetHandle(), intPtr, type);
				base.CheckError(num, "AddDirectory");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		internal void AddMono(MonoBtlsX509LookupMono monoLookup)
		{
			if (this.type != MonoBtlsX509LookupType.MONO)
			{
				throw new NotSupportedException();
			}
			int num = MonoBtlsX509Lookup.mono_btls_x509_lookup_add_mono(this.Handle.DangerousGetHandle(), monoLookup.Handle.DangerousGetHandle());
			base.CheckError(num, "AddMono");
			monoLookup.Install(this);
			if (this.monoLookups == null)
			{
				this.monoLookups = new List<MonoBtlsX509LookupMono>();
			}
			this.monoLookups.Add(monoLookup);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000C61E File Offset: 0x0000A81E
		internal void AddCertificate(MonoBtlsX509 certificate)
		{
			this.store.AddCertificate(certificate);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000C62C File Offset: 0x0000A82C
		protected override void Close()
		{
			try
			{
				if (this.monoLookups != null)
				{
					foreach (MonoBtlsX509LookupMono monoBtlsX509LookupMono in this.monoLookups)
					{
						monoBtlsX509LookupMono.Dispose();
					}
					this.monoLookups = null;
				}
			}
			finally
			{
				base.Close();
			}
		}

		// Token: 0x040002F0 RID: 752
		private MonoBtlsX509Store store;

		// Token: 0x040002F1 RID: 753
		private MonoBtlsX509LookupType type;

		// Token: 0x040002F2 RID: 754
		private List<MonoBtlsX509LookupMono> monoLookups;

		// Token: 0x020000C1 RID: 193
		internal class BoringX509LookupHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000382 RID: 898 RVA: 0x00009A41 File Offset: 0x00007C41
			public BoringX509LookupHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x06000383 RID: 899 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
			protected override bool ReleaseHandle()
			{
				MonoBtlsX509Lookup.mono_btls_x509_lookup_free(this.handle);
				return true;
			}
		}
	}
}
