using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Threading;

namespace Mono.Btls
{
	// Token: 0x020000A5 RID: 165
	internal abstract class MonoBtlsObject : IDisposable
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000A8DE File Offset: 0x00008ADE
		internal MonoBtlsObject(MonoBtlsObject.MonoBtlsHandle handle)
		{
			this.handle = handle;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000A8ED File Offset: 0x00008AED
		internal MonoBtlsObject.MonoBtlsHandle Handle
		{
			get
			{
				this.CheckThrow();
				return this.handle;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000A8FB File Offset: 0x00008AFB
		public bool IsValid
		{
			get
			{
				return this.handle != null && !this.handle.IsInvalid;
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000A915 File Offset: 0x00008B15
		protected void CheckThrow()
		{
			if (this.lastError != null)
			{
				throw this.lastError;
			}
			if (this.handle == null || this.handle.IsInvalid)
			{
				throw new ObjectDisposedException("MonoBtlsSsl");
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000A946 File Offset: 0x00008B46
		protected Exception SetException(Exception ex)
		{
			if (this.lastError == null)
			{
				this.lastError = ex;
			}
			return ex;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000A958 File Offset: 0x00008B58
		protected void CheckError(bool ok, [CallerMemberName] string callerName = null)
		{
			if (ok)
			{
				return;
			}
			if (callerName != null)
			{
				throw new CryptographicException(string.Concat(new string[]
				{
					"`",
					base.GetType().Name,
					".",
					callerName,
					"` failed."
				}));
			}
			throw new CryptographicException();
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000A9AC File Offset: 0x00008BAC
		protected void CheckError(int ret, [CallerMemberName] string callerName = null)
		{
			this.CheckError(ret == 1, callerName);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000A9BC File Offset: 0x00008BBC
		protected internal void CheckLastError([CallerMemberName] string callerName = null)
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this.lastError, null);
			if (ex == null)
			{
				return;
			}
			if (ex is AuthenticationException || ex is NotSupportedException)
			{
				throw ex;
			}
			string text;
			if (callerName != null)
			{
				text = string.Concat(new string[]
				{
					"Caught unhandled exception in `",
					base.GetType().Name,
					".",
					callerName,
					"`."
				});
			}
			else
			{
				text = "Caught unhandled exception.";
			}
			throw new CryptographicException(text, ex);
		}

		// Token: 0x060002BA RID: 698
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_free(IntPtr data);

		// Token: 0x060002BB RID: 699 RVA: 0x0000AA34 File Offset: 0x00008C34
		protected void FreeDataPtr(IntPtr data)
		{
			MonoBtlsObject.mono_btls_free(data);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected virtual void Close()
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000AA3C File Offset: 0x00008C3C
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					if (this.handle != null)
					{
						this.Close();
						this.handle.Dispose();
						this.handle = null;
					}
				}
				finally
				{
					ObjectDisposedException ex = new ObjectDisposedException(base.GetType().Name);
					Interlocked.CompareExchange<Exception>(ref this.lastError, ex, null);
				}
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000AAA0 File Offset: 0x00008CA0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000AAB0 File Offset: 0x00008CB0
		~MonoBtlsObject()
		{
			this.Dispose(false);
		}

		// Token: 0x04000283 RID: 643
		private MonoBtlsObject.MonoBtlsHandle handle;

		// Token: 0x04000284 RID: 644
		private Exception lastError;

		// Token: 0x020000A6 RID: 166
		protected internal abstract class MonoBtlsHandle : SafeHandle
		{
			// Token: 0x060002C0 RID: 704 RVA: 0x0000AAE0 File Offset: 0x00008CE0
			internal MonoBtlsHandle(IntPtr handle, bool ownsHandle)
				: base(handle, ownsHandle)
			{
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000AAEA File Offset: 0x00008CEA
			public override bool IsInvalid
			{
				get
				{
					return this.handle == IntPtr.Zero;
				}
			}
		}
	}
}
