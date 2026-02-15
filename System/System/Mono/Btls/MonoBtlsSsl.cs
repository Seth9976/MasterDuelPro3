using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Util;

namespace Mono.Btls
{
	// Token: 0x020000AD RID: 173
	internal class MonoBtlsSsl : MonoBtlsObject
	{
		// Token: 0x060002F2 RID: 754
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_destroy(IntPtr handle);

		// Token: 0x060002F3 RID: 755
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_ssl_new(IntPtr handle);

		// Token: 0x060002F4 RID: 756
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_use_certificate(IntPtr handle, IntPtr x509);

		// Token: 0x060002F5 RID: 757
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_use_private_key(IntPtr handle, IntPtr key);

		// Token: 0x060002F6 RID: 758
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_add_chain_certificate(IntPtr handle, IntPtr x509);

		// Token: 0x060002F7 RID: 759
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_accept(IntPtr handle);

		// Token: 0x060002F8 RID: 760
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_connect(IntPtr handle);

		// Token: 0x060002F9 RID: 761
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_handshake(IntPtr handle);

		// Token: 0x060002FA RID: 762
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_close(IntPtr handle);

		// Token: 0x060002FB RID: 763
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_shutdown(IntPtr handle);

		// Token: 0x060002FC RID: 764
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_set_quiet_shutdown(IntPtr handle, int mode);

		// Token: 0x060002FD RID: 765
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_set_bio(IntPtr handle, IntPtr bio);

		// Token: 0x060002FE RID: 766
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_read(IntPtr handle, IntPtr data, int len);

		// Token: 0x060002FF RID: 767
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_write(IntPtr handle, IntPtr data, int len);

		// Token: 0x06000300 RID: 768
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_get_error(IntPtr handle, int ret_code);

		// Token: 0x06000301 RID: 769
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_get_version(IntPtr handle);

		// Token: 0x06000302 RID: 770
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_get_cipher(IntPtr handle);

		// Token: 0x06000303 RID: 771
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_ssl_get_peer_certificate(IntPtr handle);

		// Token: 0x06000304 RID: 772
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_print_errors_cb(IntPtr func, IntPtr ctx);

		// Token: 0x06000305 RID: 773
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_set_server_name(IntPtr handle, IntPtr name);

		// Token: 0x06000306 RID: 774
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_ssl_get_server_name(IntPtr handle);

		// Token: 0x06000307 RID: 775
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_set_renegotiate_mode(IntPtr handle, int mode);

		// Token: 0x06000308 RID: 776
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_renegotiate_pending(IntPtr handle);

		// Token: 0x06000309 RID: 777 RVA: 0x0000B3DC File Offset: 0x000095DC
		private static MonoBtlsSsl.BoringSslHandle Create_internal(MonoBtlsSslCtx ctx)
		{
			IntPtr intPtr = MonoBtlsSsl.mono_btls_ssl_new(ctx.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException();
			}
			return new MonoBtlsSsl.BoringSslHandle(intPtr);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000B406 File Offset: 0x00009606
		public MonoBtlsSsl(MonoBtlsSslCtx ctx)
			: base(MonoBtlsSsl.Create_internal(ctx))
		{
			this.printErrorsFunc = new MonoBtlsSsl.PrintErrorsCallbackFunc(MonoBtlsSsl.PrintErrorsCallback);
			this.printErrorsFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsSsl.PrintErrorsCallbackFunc>(this.printErrorsFunc);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000B437 File Offset: 0x00009637
		internal new MonoBtlsSsl.BoringSslHandle Handle
		{
			get
			{
				return (MonoBtlsSsl.BoringSslHandle)base.Handle;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000B444 File Offset: 0x00009644
		public void SetBio(MonoBtlsBio bio)
		{
			base.CheckThrow();
			this.bio = bio;
			MonoBtlsSsl.mono_btls_ssl_set_bio(this.Handle.DangerousGetHandle(), bio.Handle.DangerousGetHandle());
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B470 File Offset: 0x00009670
		private Exception ThrowError([CallerMemberName] string callerName = null)
		{
			string text;
			try
			{
				if (callerName == null)
				{
					callerName = base.GetType().Name;
				}
				text = this.GetErrors();
			}
			catch
			{
				text = null;
			}
			if (text != null)
			{
				throw new MonoBtlsException("{0} failed: {1}.", new object[] { callerName, text });
			}
			throw new MonoBtlsException("{0} failed.", new object[] { callerName });
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B4DC File Offset: 0x000096DC
		private MonoBtlsSslError GetError(int ret_code)
		{
			base.CheckThrow();
			this.bio.CheckLastError("GetError");
			return (MonoBtlsSslError)MonoBtlsSsl.mono_btls_ssl_get_error(this.Handle.DangerousGetHandle(), ret_code);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B505 File Offset: 0x00009705
		public void SetCertificate(MonoBtlsX509 x509)
		{
			base.CheckThrow();
			if (MonoBtlsSsl.mono_btls_ssl_use_certificate(this.Handle.DangerousGetHandle(), x509.Handle.DangerousGetHandle()) <= 0)
			{
				throw this.ThrowError("SetCertificate");
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B537 File Offset: 0x00009737
		public void SetPrivateKey(MonoBtlsKey key)
		{
			base.CheckThrow();
			if (MonoBtlsSsl.mono_btls_ssl_use_private_key(this.Handle.DangerousGetHandle(), key.Handle.DangerousGetHandle()) <= 0)
			{
				throw this.ThrowError("SetPrivateKey");
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B569 File Offset: 0x00009769
		public void AddIntermediateCertificate(MonoBtlsX509 x509)
		{
			base.CheckThrow();
			if (MonoBtlsSsl.mono_btls_ssl_add_chain_certificate(this.Handle.DangerousGetHandle(), x509.Handle.DangerousGetHandle()) <= 0)
			{
				throw this.ThrowError("AddIntermediateCertificate");
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B59C File Offset: 0x0000979C
		public MonoBtlsSslError Accept()
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_accept(this.Handle.DangerousGetHandle());
			return this.GetError(num);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000B5C8 File Offset: 0x000097C8
		public MonoBtlsSslError Connect()
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_connect(this.Handle.DangerousGetHandle());
			return this.GetError(num);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000B5F4 File Offset: 0x000097F4
		public MonoBtlsSslError Handshake()
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_handshake(this.Handle.DangerousGetHandle());
			return this.GetError(num);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000B620 File Offset: 0x00009820
		[MonoPInvokeCallback(typeof(MonoBtlsSsl.PrintErrorsCallbackFunc))]
		private static int PrintErrorsCallback(IntPtr str, IntPtr len, IntPtr ctx)
		{
			StringBuilder stringBuilder = (StringBuilder)GCHandle.FromIntPtr(ctx).Target;
			int num;
			try
			{
				string text = Marshal.PtrToStringAnsi(str, (int)len);
				stringBuilder.Append(text);
				num = 1;
			}
			catch
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000B670 File Offset: 0x00009870
		public string GetErrors()
		{
			StringBuilder stringBuilder = new StringBuilder();
			GCHandle gchandle = GCHandle.Alloc(stringBuilder);
			string text;
			try
			{
				MonoBtlsSsl.mono_btls_ssl_print_errors_cb(this.printErrorsFuncPtr, GCHandle.ToIntPtr(gchandle));
				text = stringBuilder.ToString();
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return text;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000B6C8 File Offset: 0x000098C8
		public void PrintErrors()
		{
			string errors = this.GetErrors();
			if (string.IsNullOrEmpty(errors))
			{
				return;
			}
			Console.Error.WriteLine(errors);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000B6F0 File Offset: 0x000098F0
		public MonoBtlsSslError Read(IntPtr data, ref int dataSize)
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_read(this.Handle.DangerousGetHandle(), data, dataSize);
			if (num > 0)
			{
				dataSize = num;
				return MonoBtlsSslError.None;
			}
			MonoBtlsSslError error = this.GetError(num);
			if (num == 0 && error == MonoBtlsSslError.Syscall)
			{
				dataSize = 0;
				return MonoBtlsSslError.None;
			}
			dataSize = 0;
			return error;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000B738 File Offset: 0x00009938
		public MonoBtlsSslError Write(IntPtr data, ref int dataSize)
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_write(this.Handle.DangerousGetHandle(), data, dataSize);
			if (num >= 0)
			{
				dataSize = num;
				return MonoBtlsSslError.None;
			}
			MonoBtlsSslError monoBtlsSslError = (MonoBtlsSslError)MonoBtlsSsl.mono_btls_ssl_get_error(this.Handle.DangerousGetHandle(), num);
			dataSize = 0;
			return monoBtlsSslError;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000B77C File Offset: 0x0000997C
		public int GetVersion()
		{
			base.CheckThrow();
			return MonoBtlsSsl.mono_btls_ssl_get_version(this.Handle.DangerousGetHandle());
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000B794 File Offset: 0x00009994
		public int GetCipher()
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_get_cipher(this.Handle.DangerousGetHandle());
			base.CheckError(num > 0, "GetCipher");
			return num;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000B7C8 File Offset: 0x000099C8
		public MonoBtlsX509 GetPeerCertificate()
		{
			base.CheckThrow();
			IntPtr intPtr = MonoBtlsSsl.mono_btls_ssl_get_peer_certificate(this.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000B808 File Offset: 0x00009A08
		public void SetServerName(string name)
		{
			base.CheckThrow();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				int num = MonoBtlsSsl.mono_btls_ssl_set_server_name(this.Handle.DangerousGetHandle(), intPtr);
				base.CheckError(num, "SetServerName");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000B86C File Offset: 0x00009A6C
		public string GetServerName()
		{
			base.CheckThrow();
			IntPtr intPtr = MonoBtlsSsl.mono_btls_ssl_get_server_name(this.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.PtrToStringAnsi(intPtr);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B8A5 File Offset: 0x00009AA5
		public void Shutdown()
		{
			base.CheckThrow();
			if (MonoBtlsSsl.mono_btls_ssl_shutdown(this.Handle.DangerousGetHandle()) < 0)
			{
				throw this.ThrowError("Shutdown");
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B8CC File Offset: 0x00009ACC
		public void SetQuietShutdown()
		{
			base.CheckThrow();
			MonoBtlsSsl.mono_btls_ssl_set_quiet_shutdown(this.Handle.DangerousGetHandle(), 1);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B8E5 File Offset: 0x00009AE5
		protected override void Close()
		{
			if (!this.Handle.IsInvalid)
			{
				MonoBtlsSsl.mono_btls_ssl_close(this.Handle.DangerousGetHandle());
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000B904 File Offset: 0x00009B04
		public void SetRenegotiateMode(MonoBtlsSslRenegotiateMode mode)
		{
			base.CheckThrow();
			MonoBtlsSsl.mono_btls_ssl_set_renegotiate_mode(this.Handle.DangerousGetHandle(), (int)mode);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000B91D File Offset: 0x00009B1D
		public bool RenegotiatePending()
		{
			return MonoBtlsSsl.mono_btls_ssl_renegotiate_pending(this.Handle.DangerousGetHandle()) != 0;
		}

		// Token: 0x04000286 RID: 646
		private MonoBtlsBio bio;

		// Token: 0x04000287 RID: 647
		private MonoBtlsSsl.PrintErrorsCallbackFunc printErrorsFunc;

		// Token: 0x04000288 RID: 648
		private IntPtr printErrorsFuncPtr;

		// Token: 0x020000AE RID: 174
		internal class BoringSslHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000324 RID: 804 RVA: 0x00009A41 File Offset: 0x00007C41
			public BoringSslHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x06000325 RID: 805 RVA: 0x0000B932 File Offset: 0x00009B32
			protected override bool ReleaseHandle()
			{
				MonoBtlsSsl.mono_btls_ssl_destroy(this.handle);
				this.handle = IntPtr.Zero;
				return true;
			}
		}

		// Token: 0x020000AF RID: 175
		// (Invoke) Token: 0x06000327 RID: 807
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int PrintErrorsCallbackFunc(IntPtr str, IntPtr len, IntPtr ctx);
	}
}
