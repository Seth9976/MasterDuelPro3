using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Util;

namespace Mono.Btls
{
	// Token: 0x020000B0 RID: 176
	internal class MonoBtlsSslCtx : MonoBtlsObject
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B94B File Offset: 0x00009B4B
		internal new MonoBtlsSslCtx.BoringSslCtxHandle Handle
		{
			get
			{
				return (MonoBtlsSslCtx.BoringSslCtxHandle)base.Handle;
			}
		}

		// Token: 0x06000329 RID: 809
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_ssl_ctx_new();

		// Token: 0x0600032A RID: 810
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_ctx_free(IntPtr handle);

		// Token: 0x0600032B RID: 811
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_ctx_initialize(IntPtr handle, IntPtr instance);

		// Token: 0x0600032C RID: 812
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_ctx_set_cert_verify_callback(IntPtr handle, IntPtr func, int cert_required);

		// Token: 0x0600032D RID: 813
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_ctx_set_cert_select_callback(IntPtr handle, IntPtr func);

		// Token: 0x0600032E RID: 814
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_ctx_set_min_version(IntPtr handle, int version);

		// Token: 0x0600032F RID: 815
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_ctx_set_max_version(IntPtr handle, int version);

		// Token: 0x06000330 RID: 816
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_ctx_set_ciphers(IntPtr handle, int count, IntPtr data, int allow_unsupported);

		// Token: 0x06000331 RID: 817
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_ctx_set_verify_param(IntPtr handle, IntPtr param);

		// Token: 0x06000332 RID: 818
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_ctx_set_client_ca_list(IntPtr handle, int count, IntPtr sizes, IntPtr data);

		// Token: 0x06000333 RID: 819
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_ctx_set_server_name_callback(IntPtr handle, IntPtr func);

		// Token: 0x06000334 RID: 820 RVA: 0x0000B958 File Offset: 0x00009B58
		public MonoBtlsSslCtx()
			: this(new MonoBtlsSslCtx.BoringSslCtxHandle(MonoBtlsSslCtx.mono_btls_ssl_ctx_new()))
		{
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000B96C File Offset: 0x00009B6C
		internal MonoBtlsSslCtx(MonoBtlsSslCtx.BoringSslCtxHandle handle)
			: base(handle)
		{
			this.instance = GCHandle.Alloc(this);
			this.instancePtr = GCHandle.ToIntPtr(this.instance);
			MonoBtlsSslCtx.mono_btls_ssl_ctx_initialize(handle.DangerousGetHandle(), this.instancePtr);
			this.verifyFunc = new MonoBtlsSslCtx.NativeVerifyFunc(MonoBtlsSslCtx.NativeVerifyCallback);
			this.selectFunc = new MonoBtlsSslCtx.NativeSelectFunc(MonoBtlsSslCtx.NativeSelectCallback);
			this.serverNameFunc = new MonoBtlsSslCtx.NativeServerNameFunc(MonoBtlsSslCtx.NativeServerNameCallback);
			this.verifyFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsSslCtx.NativeVerifyFunc>(this.verifyFunc);
			this.selectFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsSslCtx.NativeSelectFunc>(this.selectFunc);
			this.serverNameFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsSslCtx.NativeServerNameFunc>(this.serverNameFunc);
			this.store = new MonoBtlsX509Store(this.Handle);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000BA28 File Offset: 0x00009C28
		public MonoBtlsX509Store CertificateStore
		{
			get
			{
				return this.store;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000BA30 File Offset: 0x00009C30
		private int VerifyCallback(bool preverify_ok, MonoBtlsX509StoreCtx ctx)
		{
			if (this.verifyCallback != null)
			{
				return this.verifyCallback(ctx);
			}
			return 0;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000BA48 File Offset: 0x00009C48
		[MonoPInvokeCallback(typeof(MonoBtlsSslCtx.NativeVerifyFunc))]
		private static int NativeVerifyCallback(IntPtr instance, int preverify_ok, IntPtr store_ctx)
		{
			MonoBtlsSslCtx monoBtlsSslCtx = (MonoBtlsSslCtx)GCHandle.FromIntPtr(instance).Target;
			using (MonoBtlsX509StoreCtx monoBtlsX509StoreCtx = new MonoBtlsX509StoreCtx(preverify_ok, store_ctx))
			{
				try
				{
					return monoBtlsSslCtx.VerifyCallback(preverify_ok != 0, monoBtlsX509StoreCtx);
				}
				catch (Exception ex)
				{
					monoBtlsSslCtx.SetException(ex);
				}
			}
			return 0;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		[MonoPInvokeCallback(typeof(MonoBtlsSslCtx.NativeSelectFunc))]
		private static int NativeSelectCallback(IntPtr instance, int count, IntPtr sizes, IntPtr data)
		{
			MonoBtlsSslCtx monoBtlsSslCtx = (MonoBtlsSslCtx)GCHandle.FromIntPtr(instance).Target;
			int num;
			try
			{
				string[] array = MonoBtlsSslCtx.CopyIssuers(count, sizes, data);
				if (monoBtlsSslCtx.selectCallback != null)
				{
					num = monoBtlsSslCtx.selectCallback(array);
				}
				else
				{
					num = 1;
				}
			}
			catch (Exception ex)
			{
				monoBtlsSslCtx.SetException(ex);
				num = 0;
			}
			return num;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000BB20 File Offset: 0x00009D20
		private static string[] CopyIssuers(int count, IntPtr sizesPtr, IntPtr dataPtr)
		{
			if (count == 0 || sizesPtr == IntPtr.Zero || dataPtr == IntPtr.Zero)
			{
				return null;
			}
			int[] array = new int[count];
			Marshal.Copy(sizesPtr, array, 0, count);
			IntPtr[] array2 = new IntPtr[count];
			Marshal.Copy(dataPtr, array2, 0, count);
			string[] array3 = new string[count];
			for (int i = 0; i < count; i++)
			{
				byte[] array4 = new byte[array[i]];
				Marshal.Copy(array2[i], array4, 0, array4.Length);
				using (MonoBtlsX509Name monoBtlsX509Name = MonoBtlsX509Name.CreateFromData(array4, false))
				{
					array3[i] = MonoBtlsUtils.FormatName(monoBtlsX509Name, true, ", ", true);
				}
			}
			return array3;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000BBD4 File Offset: 0x00009DD4
		public void SetVerifyCallback(MonoBtlsVerifyCallback callback, bool client_cert_required)
		{
			base.CheckThrow();
			this.verifyCallback = callback;
			MonoBtlsSslCtx.mono_btls_ssl_ctx_set_cert_verify_callback(this.Handle.DangerousGetHandle(), this.verifyFuncPtr, client_cert_required ? 1 : 0);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000BC00 File Offset: 0x00009E00
		public void SetSelectCallback(MonoBtlsSelectCallback callback)
		{
			base.CheckThrow();
			this.selectCallback = callback;
			MonoBtlsSslCtx.mono_btls_ssl_ctx_set_cert_select_callback(this.Handle.DangerousGetHandle(), this.selectFuncPtr);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000BC25 File Offset: 0x00009E25
		public void SetMinVersion(int version)
		{
			base.CheckThrow();
			MonoBtlsSslCtx.mono_btls_ssl_ctx_set_min_version(this.Handle.DangerousGetHandle(), version);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000BC3E File Offset: 0x00009E3E
		public void SetMaxVersion(int version)
		{
			base.CheckThrow();
			MonoBtlsSslCtx.mono_btls_ssl_ctx_set_max_version(this.Handle.DangerousGetHandle(), version);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000BC58 File Offset: 0x00009E58
		public void SetCiphers(short[] ciphers, bool allow_unsupported)
		{
			base.CheckThrow();
			IntPtr intPtr = Marshal.AllocHGlobal(ciphers.Length * 2);
			try
			{
				Marshal.Copy(ciphers, 0, intPtr, ciphers.Length);
				int num = MonoBtlsSslCtx.mono_btls_ssl_ctx_set_ciphers(this.Handle.DangerousGetHandle(), ciphers.Length, intPtr, allow_unsupported ? 1 : 0);
				base.CheckError(num > 0, "SetCiphers");
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000BCC8 File Offset: 0x00009EC8
		public void SetVerifyParam(MonoBtlsX509VerifyParam param)
		{
			base.CheckThrow();
			int num = MonoBtlsSslCtx.mono_btls_ssl_ctx_set_verify_param(this.Handle.DangerousGetHandle(), param.Handle.DangerousGetHandle());
			base.CheckError(num, "SetVerifyParam");
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000BD04 File Offset: 0x00009F04
		public void SetClientCertificateIssuers(string[] acceptableIssuers)
		{
			base.CheckThrow();
			if (acceptableIssuers == null || acceptableIssuers.Length == 0)
			{
				return;
			}
			int num = acceptableIssuers.Length;
			new byte[num][];
			int[] array = new int[num];
			IntPtr[] array2 = new IntPtr[num];
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				for (int i = 0; i < num; i++)
				{
					byte[] rawData = new X500DistinguishedName(acceptableIssuers[i]).RawData;
					array[i] = rawData.Length;
					array2[i] = Marshal.AllocHGlobal(rawData.Length);
					Marshal.Copy(rawData, 0, array2[i], rawData.Length);
				}
				intPtr = Marshal.AllocHGlobal(num * 4);
				Marshal.Copy(array, 0, intPtr, num);
				intPtr2 = Marshal.AllocHGlobal(num * 8);
				Marshal.Copy(array2, 0, intPtr2, num);
				int num2 = MonoBtlsSslCtx.mono_btls_ssl_ctx_set_client_ca_list(this.Handle.DangerousGetHandle(), num, intPtr, intPtr2);
				base.CheckError(num2, "SetClientCertificateIssuers");
			}
			finally
			{
				for (int j = 0; j < num; j++)
				{
					if (array2[j] != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(array2[j]);
					}
				}
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000BE38 File Offset: 0x0000A038
		public void SetServerNameCallback(MonoBtlsServerNameCallback callback)
		{
			base.CheckThrow();
			this.serverNameCallback = callback;
			MonoBtlsSslCtx.mono_btls_ssl_ctx_set_server_name_callback(this.Handle.DangerousGetHandle(), this.serverNameFuncPtr);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000BE60 File Offset: 0x0000A060
		[MonoPInvokeCallback(typeof(MonoBtlsSslCtx.NativeServerNameFunc))]
		private static int NativeServerNameCallback(IntPtr instance)
		{
			MonoBtlsSslCtx monoBtlsSslCtx = (MonoBtlsSslCtx)GCHandle.FromIntPtr(instance).Target;
			int num;
			try
			{
				num = monoBtlsSslCtx.serverNameCallback();
			}
			catch (Exception ex)
			{
				monoBtlsSslCtx.SetException(ex);
				num = 0;
			}
			return num;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
		protected override void Close()
		{
			if (this.store != null)
			{
				this.store.Dispose();
				this.store = null;
			}
			if (this.instance.IsAllocated)
			{
				this.instance.Free();
			}
			base.Close();
		}

		// Token: 0x04000289 RID: 649
		private MonoBtlsSslCtx.NativeVerifyFunc verifyFunc;

		// Token: 0x0400028A RID: 650
		private MonoBtlsSslCtx.NativeSelectFunc selectFunc;

		// Token: 0x0400028B RID: 651
		private MonoBtlsSslCtx.NativeServerNameFunc serverNameFunc;

		// Token: 0x0400028C RID: 652
		private IntPtr verifyFuncPtr;

		// Token: 0x0400028D RID: 653
		private IntPtr selectFuncPtr;

		// Token: 0x0400028E RID: 654
		private IntPtr serverNameFuncPtr;

		// Token: 0x0400028F RID: 655
		private MonoBtlsVerifyCallback verifyCallback;

		// Token: 0x04000290 RID: 656
		private MonoBtlsSelectCallback selectCallback;

		// Token: 0x04000291 RID: 657
		private MonoBtlsServerNameCallback serverNameCallback;

		// Token: 0x04000292 RID: 658
		private MonoBtlsX509Store store;

		// Token: 0x04000293 RID: 659
		private GCHandle instance;

		// Token: 0x04000294 RID: 660
		private IntPtr instancePtr;

		// Token: 0x020000B1 RID: 177
		internal class BoringSslCtxHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000345 RID: 837 RVA: 0x00009A41 File Offset: 0x00007C41
			public BoringSslCtxHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x06000346 RID: 838 RVA: 0x0000BEEA File Offset: 0x0000A0EA
			protected override bool ReleaseHandle()
			{
				MonoBtlsSslCtx.mono_btls_ssl_ctx_free(this.handle);
				return true;
			}
		}

		// Token: 0x020000B2 RID: 178
		// (Invoke) Token: 0x06000348 RID: 840
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int NativeVerifyFunc(IntPtr instance, int preverify_ok, IntPtr ctx);

		// Token: 0x020000B3 RID: 179
		// (Invoke) Token: 0x0600034A RID: 842
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int NativeSelectFunc(IntPtr instance, int count, IntPtr sizes, IntPtr data);

		// Token: 0x020000B4 RID: 180
		// (Invoke) Token: 0x0600034C RID: 844
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int NativeServerNameFunc(IntPtr instance);
	}
}
