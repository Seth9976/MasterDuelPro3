using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000D1 RID: 209
	internal class MonoBtlsX509VerifyParam : MonoBtlsObject
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000D105 File Offset: 0x0000B305
		internal new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle Handle
		{
			get
			{
				return (MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle)base.Handle;
			}
		}

		// Token: 0x060003D6 RID: 982
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_verify_param_copy(IntPtr handle);

		// Token: 0x060003D7 RID: 983
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_verify_param_lookup(IntPtr name);

		// Token: 0x060003D8 RID: 984
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_can_modify(IntPtr param);

		// Token: 0x060003D9 RID: 985
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_host(IntPtr handle, IntPtr name, int namelen);

		// Token: 0x060003DA RID: 986
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_time(IntPtr handle, long time);

		// Token: 0x060003DB RID: 987
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_verify_param_free(IntPtr handle);

		// Token: 0x060003DC RID: 988 RVA: 0x00009A2B File Offset: 0x00007C2B
		internal MonoBtlsX509VerifyParam(MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle handle)
			: base(handle)
		{
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000D114 File Offset: 0x0000B314
		public MonoBtlsX509VerifyParam Copy()
		{
			IntPtr intPtr = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_copy(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "Copy");
			return new MonoBtlsX509VerifyParam(new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle(intPtr));
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000D153 File Offset: 0x0000B353
		public static MonoBtlsX509VerifyParam GetSslClient()
		{
			return MonoBtlsX509VerifyParam.Lookup("ssl_client", true);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000D160 File Offset: 0x0000B360
		public static MonoBtlsX509VerifyParam GetSslServer()
		{
			return MonoBtlsX509VerifyParam.Lookup("ssl_server", true);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000D170 File Offset: 0x0000B370
		public static MonoBtlsX509VerifyParam Lookup(string name, bool fail = false)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			MonoBtlsX509VerifyParam monoBtlsX509VerifyParam;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				intPtr2 = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_lookup(intPtr);
				if (intPtr2 == IntPtr.Zero)
				{
					if (fail)
					{
						throw new MonoBtlsException("X509_VERIFY_PARAM_lookup() could not find '{0}'.", new object[] { name });
					}
					monoBtlsX509VerifyParam = null;
				}
				else
				{
					monoBtlsX509VerifyParam = new MonoBtlsX509VerifyParam(new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle(intPtr2));
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return monoBtlsX509VerifyParam;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000D1F4 File Offset: 0x0000B3F4
		public bool CanModify
		{
			get
			{
				return MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_can_modify(this.Handle.DangerousGetHandle()) != 0;
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000D209 File Offset: 0x0000B409
		private void WantToModify()
		{
			if (!this.CanModify)
			{
				throw new MonoBtlsException("Attempting to modify read-only MonoBtlsX509VerifyParam instance.");
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000D220 File Offset: 0x0000B420
		public void SetHost(string name)
		{
			this.WantToModify();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_host(this.Handle.DangerousGetHandle(), intPtr, name.Length);
				base.CheckError(num, "SetHost");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000D28C File Offset: 0x0000B48C
		public void SetTime(DateTime time)
		{
			this.WantToModify();
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			long num = (long)time.Subtract(dateTime).TotalSeconds;
			int num2 = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_time(this.Handle.DangerousGetHandle(), num);
			base.CheckError(num2, "SetTime");
		}

		// Token: 0x020000D2 RID: 210
		internal class BoringX509VerifyParamHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x060003E5 RID: 997 RVA: 0x00009A41 File Offset: 0x00007C41
			public BoringX509VerifyParamHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x060003E6 RID: 998 RVA: 0x0000D2E1 File Offset: 0x0000B4E1
			protected override bool ReleaseHandle()
			{
				MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_free(this.handle);
				return true;
			}
		}
	}
}
