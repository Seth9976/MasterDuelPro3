using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Mono.Btls
{
	// Token: 0x020000A7 RID: 167
	internal class MonoBtlsPkcs12 : MonoBtlsObject
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000AAFC File Offset: 0x00008CFC
		internal new MonoBtlsPkcs12.BoringPkcs12Handle Handle
		{
			get
			{
				return (MonoBtlsPkcs12.BoringPkcs12Handle)base.Handle;
			}
		}

		// Token: 0x060002C3 RID: 707
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_pkcs12_free(IntPtr handle);

		// Token: 0x060002C4 RID: 708
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_pkcs12_new();

		// Token: 0x060002C5 RID: 709
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_pkcs12_get_count(IntPtr handle);

		// Token: 0x060002C6 RID: 710
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_pkcs12_get_cert(IntPtr Handle, int index);

		// Token: 0x060002C7 RID: 711
		[DllImport("libmono-btls-shared")]
		private unsafe static extern int mono_btls_pkcs12_import(IntPtr chain, void* data, int len, SafePasswordHandle password);

		// Token: 0x060002C8 RID: 712
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_pkcs12_has_private_key(IntPtr pkcs12);

		// Token: 0x060002C9 RID: 713
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_pkcs12_get_private_key(IntPtr pkcs12);

		// Token: 0x060002CA RID: 714 RVA: 0x0000AB09 File Offset: 0x00008D09
		internal MonoBtlsPkcs12()
			: base(new MonoBtlsPkcs12.BoringPkcs12Handle(MonoBtlsPkcs12.mono_btls_pkcs12_new()))
		{
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000AB1B File Offset: 0x00008D1B
		public int Count
		{
			get
			{
				return MonoBtlsPkcs12.mono_btls_pkcs12_get_count(this.Handle.DangerousGetHandle());
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000AB30 File Offset: 0x00008D30
		public MonoBtlsX509 GetCertificate(int index)
		{
			if (index >= this.Count)
			{
				throw new IndexOutOfRangeException();
			}
			IntPtr intPtr = MonoBtlsPkcs12.mono_btls_pkcs12_get_cert(this.Handle.DangerousGetHandle(), index);
			base.CheckError(intPtr != IntPtr.Zero, "GetCertificate");
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000AB80 File Offset: 0x00008D80
		public unsafe void Import(byte[] buffer, SafePasswordHandle password)
		{
			fixed (byte[] array = buffer)
			{
				void* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = (void*)(&array[0]);
				}
				int num = MonoBtlsPkcs12.mono_btls_pkcs12_import(this.Handle.DangerousGetHandle(), ptr, buffer.Length, password);
				base.CheckError(num, "Import");
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000ABC9 File Offset: 0x00008DC9
		public bool HasPrivateKey
		{
			get
			{
				return MonoBtlsPkcs12.mono_btls_pkcs12_has_private_key(this.Handle.DangerousGetHandle()) != 0;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		public MonoBtlsKey GetPrivateKey()
		{
			if (!this.HasPrivateKey)
			{
				throw new InvalidOperationException();
			}
			if (this.privateKey == null)
			{
				IntPtr intPtr = MonoBtlsPkcs12.mono_btls_pkcs12_get_private_key(this.Handle.DangerousGetHandle());
				base.CheckError(intPtr != IntPtr.Zero, "GetPrivateKey");
				this.privateKey = new MonoBtlsKey(new MonoBtlsKey.BoringKeyHandle(intPtr));
			}
			return this.privateKey;
		}

		// Token: 0x04000285 RID: 645
		private MonoBtlsKey privateKey;

		// Token: 0x020000A8 RID: 168
		internal class BoringPkcs12Handle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x060002D0 RID: 720 RVA: 0x00009A41 File Offset: 0x00007C41
			public BoringPkcs12Handle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x060002D1 RID: 721 RVA: 0x0000AC41 File Offset: 0x00008E41
			protected override bool ReleaseHandle()
			{
				MonoBtlsPkcs12.mono_btls_pkcs12_free(this.handle);
				return true;
			}
		}
	}
}
