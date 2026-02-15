using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000BB RID: 187
	internal class MonoBtlsX509Chain : MonoBtlsObject
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000C3FB File Offset: 0x0000A5FB
		internal new MonoBtlsX509Chain.BoringX509ChainHandle Handle
		{
			get
			{
				return (MonoBtlsX509Chain.BoringX509ChainHandle)base.Handle;
			}
		}

		// Token: 0x06000367 RID: 871
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_chain_new();

		// Token: 0x06000368 RID: 872
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_chain_get_count(IntPtr handle);

		// Token: 0x06000369 RID: 873
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_chain_get_cert(IntPtr Handle, int index);

		// Token: 0x0600036A RID: 874
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_chain_add_cert(IntPtr chain, IntPtr x509);

		// Token: 0x0600036B RID: 875
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_chain_up_ref(IntPtr handle);

		// Token: 0x0600036C RID: 876
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_chain_free(IntPtr handle);

		// Token: 0x0600036D RID: 877 RVA: 0x0000C408 File Offset: 0x0000A608
		public MonoBtlsX509Chain()
			: base(new MonoBtlsX509Chain.BoringX509ChainHandle(MonoBtlsX509Chain.mono_btls_x509_chain_new()))
		{
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00009A2B File Offset: 0x00007C2B
		internal MonoBtlsX509Chain(MonoBtlsX509Chain.BoringX509ChainHandle handle)
			: base(handle)
		{
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000C41A File Offset: 0x0000A61A
		public int Count
		{
			get
			{
				return MonoBtlsX509Chain.mono_btls_x509_chain_get_count(this.Handle.DangerousGetHandle());
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000C42C File Offset: 0x0000A62C
		public MonoBtlsX509 GetCertificate(int index)
		{
			if (index >= this.Count)
			{
				throw new IndexOutOfRangeException();
			}
			IntPtr intPtr = MonoBtlsX509Chain.mono_btls_x509_chain_get_cert(this.Handle.DangerousGetHandle(), index);
			base.CheckError(intPtr != IntPtr.Zero, "GetCertificate");
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000C47B File Offset: 0x0000A67B
		public void AddCertificate(MonoBtlsX509 x509)
		{
			MonoBtlsX509Chain.mono_btls_x509_chain_add_cert(this.Handle.DangerousGetHandle(), x509.Handle.DangerousGetHandle());
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000C49C File Offset: 0x0000A69C
		internal MonoBtlsX509Chain Copy()
		{
			IntPtr intPtr = MonoBtlsX509Chain.mono_btls_x509_chain_up_ref(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "Copy");
			return new MonoBtlsX509Chain(new MonoBtlsX509Chain.BoringX509ChainHandle(intPtr));
		}

		// Token: 0x020000BC RID: 188
		internal class BoringX509ChainHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000373 RID: 883 RVA: 0x00009A41 File Offset: 0x00007C41
			public BoringX509ChainHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x06000374 RID: 884 RVA: 0x0000C4DB File Offset: 0x0000A6DB
			protected override bool ReleaseHandle()
			{
				MonoBtlsX509Chain.mono_btls_x509_chain_free(this.handle);
				return true;
			}
		}
	}
}
