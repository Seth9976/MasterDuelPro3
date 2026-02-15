using System;
using System.Runtime.InteropServices;
using Mono.Util;

namespace Mono.Btls
{
	// Token: 0x020000C2 RID: 194
	internal abstract class MonoBtlsX509LookupMono : MonoBtlsObject
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000C6AE File Offset: 0x0000A8AE
		internal new MonoBtlsX509LookupMono.BoringX509LookupMonoHandle Handle
		{
			get
			{
				return (MonoBtlsX509LookupMono.BoringX509LookupMonoHandle)base.Handle;
			}
		}

		// Token: 0x06000385 RID: 901
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_lookup_mono_new();

		// Token: 0x06000386 RID: 902
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_lookup_mono_init(IntPtr handle, IntPtr instance, IntPtr by_subject_func);

		// Token: 0x06000387 RID: 903
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_lookup_mono_free(IntPtr handle);

		// Token: 0x06000388 RID: 904 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		internal MonoBtlsX509LookupMono()
			: base(new MonoBtlsX509LookupMono.BoringX509LookupMonoHandle(MonoBtlsX509LookupMono.mono_btls_x509_lookup_mono_new()))
		{
			this.gch = GCHandle.Alloc(this);
			this.instance = GCHandle.ToIntPtr(this.gch);
			this.bySubjectFunc = new MonoBtlsX509LookupMono.BySubjectFunc(MonoBtlsX509LookupMono.OnGetBySubject);
			this.bySubjectFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsX509LookupMono.BySubjectFunc>(this.bySubjectFunc);
			MonoBtlsX509LookupMono.mono_btls_x509_lookup_mono_init(this.Handle.DangerousGetHandle(), this.instance, this.bySubjectFuncPtr);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000C735 File Offset: 0x0000A935
		internal void Install(MonoBtlsX509Lookup lookup)
		{
			if (this.lookup != null)
			{
				throw new InvalidOperationException();
			}
			this.lookup = lookup;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000C74C File Offset: 0x0000A94C
		protected void AddCertificate(MonoBtlsX509 certificate)
		{
			this.lookup.AddCertificate(certificate);
		}

		// Token: 0x0600038B RID: 907
		protected abstract MonoBtlsX509 OnGetBySubject(MonoBtlsX509Name name);

		// Token: 0x0600038C RID: 908 RVA: 0x0000C75C File Offset: 0x0000A95C
		[MonoPInvokeCallback(typeof(MonoBtlsX509LookupMono.BySubjectFunc))]
		private static int OnGetBySubject(IntPtr instance, IntPtr name_ptr, out IntPtr x509_ptr)
		{
			int num;
			try
			{
				MonoBtlsX509Name.BoringX509NameHandle boringX509NameHandle = null;
				try
				{
					MonoBtlsX509LookupMono monoBtlsX509LookupMono = (MonoBtlsX509LookupMono)GCHandle.FromIntPtr(instance).Target;
					boringX509NameHandle = new MonoBtlsX509Name.BoringX509NameHandle(name_ptr, false);
					MonoBtlsX509Name monoBtlsX509Name = new MonoBtlsX509Name(boringX509NameHandle);
					MonoBtlsX509 monoBtlsX = monoBtlsX509LookupMono.OnGetBySubject(monoBtlsX509Name);
					if (monoBtlsX != null)
					{
						x509_ptr = monoBtlsX.Handle.StealHandle();
						num = 1;
					}
					else
					{
						x509_ptr = IntPtr.Zero;
						num = 0;
					}
				}
				finally
				{
					if (boringX509NameHandle != null)
					{
						boringX509NameHandle.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("LOOKUP METHOD - GET BY SUBJECT EX: {0}", ex);
				x509_ptr = IntPtr.Zero;
				num = 0;
			}
			return num;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
		protected override void Close()
		{
			try
			{
				if (this.gch.IsAllocated)
				{
					this.gch.Free();
				}
			}
			finally
			{
				this.instance = IntPtr.Zero;
				this.bySubjectFunc = null;
				this.bySubjectFuncPtr = IntPtr.Zero;
				base.Close();
			}
		}

		// Token: 0x040002F3 RID: 755
		private GCHandle gch;

		// Token: 0x040002F4 RID: 756
		private IntPtr instance;

		// Token: 0x040002F5 RID: 757
		private MonoBtlsX509LookupMono.BySubjectFunc bySubjectFunc;

		// Token: 0x040002F6 RID: 758
		private IntPtr bySubjectFuncPtr;

		// Token: 0x040002F7 RID: 759
		private MonoBtlsX509Lookup lookup;

		// Token: 0x020000C3 RID: 195
		internal class BoringX509LookupMonoHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x0600038E RID: 910 RVA: 0x00009A41 File Offset: 0x00007C41
			public BoringX509LookupMonoHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x0600038F RID: 911 RVA: 0x0000C854 File Offset: 0x0000AA54
			protected override bool ReleaseHandle()
			{
				MonoBtlsX509LookupMono.mono_btls_x509_lookup_mono_free(this.handle);
				return true;
			}
		}

		// Token: 0x020000C4 RID: 196
		// (Invoke) Token: 0x06000391 RID: 913
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int BySubjectFunc(IntPtr instance, IntPtr name, out IntPtr x509_ptr);
	}
}
