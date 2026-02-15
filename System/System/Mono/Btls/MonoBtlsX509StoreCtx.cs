using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000CC RID: 204
	internal class MonoBtlsX509StoreCtx : MonoBtlsObject
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000CD9E File Offset: 0x0000AF9E
		internal new MonoBtlsX509StoreCtx.BoringX509StoreCtxHandle Handle
		{
			get
			{
				return (MonoBtlsX509StoreCtx.BoringX509StoreCtxHandle)base.Handle;
			}
		}

		// Token: 0x060003BA RID: 954
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_ctx_new();

		// Token: 0x060003BB RID: 955
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_ctx_from_ptr(IntPtr ctx);

		// Token: 0x060003BC RID: 956
		[DllImport("libmono-btls-shared")]
		private static extern MonoBtlsX509Error mono_btls_x509_store_ctx_get_error(IntPtr handle, out IntPtr error_string);

		// Token: 0x060003BD RID: 957
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_ctx_get_chain(IntPtr handle);

		// Token: 0x060003BE RID: 958
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_store_ctx_init(IntPtr handle, IntPtr store, IntPtr chain);

		// Token: 0x060003BF RID: 959
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_store_ctx_set_param(IntPtr handle, IntPtr param);

		// Token: 0x060003C0 RID: 960
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_store_ctx_verify_cert(IntPtr handle);

		// Token: 0x060003C1 RID: 961
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_ctx_get_untrusted(IntPtr handle);

		// Token: 0x060003C2 RID: 962
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_store_ctx_up_ref(IntPtr handle);

		// Token: 0x060003C3 RID: 963
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_store_ctx_free(IntPtr handle);

		// Token: 0x060003C4 RID: 964 RVA: 0x0000CDAB File Offset: 0x0000AFAB
		internal MonoBtlsX509StoreCtx()
			: base(new MonoBtlsX509StoreCtx.BoringX509StoreCtxHandle(MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_new(), true))
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000CDBE File Offset: 0x0000AFBE
		private static MonoBtlsX509StoreCtx.BoringX509StoreCtxHandle Create_internal(IntPtr store_ctx)
		{
			IntPtr intPtr = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_from_ptr(store_ctx);
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException();
			}
			return new MonoBtlsX509StoreCtx.BoringX509StoreCtxHandle(intPtr, true);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000CDDF File Offset: 0x0000AFDF
		internal MonoBtlsX509StoreCtx(int preverify_ok, IntPtr store_ctx)
			: base(MonoBtlsX509StoreCtx.Create_internal(store_ctx))
		{
			this.verifyResult = new int?(preverify_ok);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000CDF9 File Offset: 0x0000AFF9
		internal MonoBtlsX509StoreCtx(MonoBtlsX509StoreCtx.BoringX509StoreCtxHandle ptr, int? verifyResult)
			: base(ptr)
		{
			this.verifyResult = verifyResult;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000CE0C File Offset: 0x0000B00C
		public MonoBtlsX509Error GetError()
		{
			IntPtr intPtr;
			return MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_get_error(this.Handle.DangerousGetHandle(), out intPtr);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000CE2C File Offset: 0x0000B02C
		public MonoBtlsX509Chain GetChain()
		{
			IntPtr intPtr = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_get_chain(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "GetChain");
			return new MonoBtlsX509Chain(new MonoBtlsX509Chain.BoringX509ChainHandle(intPtr));
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000CE6C File Offset: 0x0000B06C
		public MonoBtlsX509Chain GetUntrusted()
		{
			IntPtr intPtr = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_get_untrusted(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "GetUntrusted");
			return new MonoBtlsX509Chain(new MonoBtlsX509Chain.BoringX509ChainHandle(intPtr));
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000CEAC File Offset: 0x0000B0AC
		public void Initialize(MonoBtlsX509Store store, MonoBtlsX509Chain chain)
		{
			int num = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_init(this.Handle.DangerousGetHandle(), store.Handle.DangerousGetHandle(), chain.Handle.DangerousGetHandle());
			base.CheckError(num, "Initialize");
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		public void SetVerifyParam(MonoBtlsX509VerifyParam param)
		{
			int num = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_set_param(this.Handle.DangerousGetHandle(), param.Handle.DangerousGetHandle());
			base.CheckError(num, "SetVerifyParam");
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000CF21 File Offset: 0x0000B121
		public int VerifyResult
		{
			get
			{
				if (this.verifyResult == null)
				{
					throw new InvalidOperationException();
				}
				return this.verifyResult.Value;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000CF41 File Offset: 0x0000B141
		public int Verify()
		{
			this.verifyResult = new int?(MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_verify_cert(this.Handle.DangerousGetHandle()));
			return this.verifyResult.Value;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000CF6C File Offset: 0x0000B16C
		public MonoBtlsX509StoreCtx Copy()
		{
			IntPtr intPtr = MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_up_ref(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "Copy");
			return new MonoBtlsX509StoreCtx(new MonoBtlsX509StoreCtx.BoringX509StoreCtxHandle(intPtr, true), this.verifyResult);
		}

		// Token: 0x04000315 RID: 789
		private int? verifyResult;

		// Token: 0x020000CD RID: 205
		internal class BoringX509StoreCtxHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x060003D0 RID: 976 RVA: 0x0000CFB2 File Offset: 0x0000B1B2
			internal BoringX509StoreCtxHandle(IntPtr handle, bool ownsHandle = true)
				: base(handle, ownsHandle)
			{
				this.dontFree = !ownsHandle;
			}

			// Token: 0x060003D1 RID: 977 RVA: 0x0000CFC6 File Offset: 0x0000B1C6
			protected override bool ReleaseHandle()
			{
				if (!this.dontFree)
				{
					MonoBtlsX509StoreCtx.mono_btls_x509_store_ctx_free(this.handle);
				}
				return true;
			}

			// Token: 0x04000316 RID: 790
			private bool dontFree;
		}
	}
}
