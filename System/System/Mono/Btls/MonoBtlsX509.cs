using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Mono.Btls
{
	// Token: 0x020000B9 RID: 185
	internal class MonoBtlsX509 : MonoBtlsObject
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000C1CE File Offset: 0x0000A3CE
		internal new MonoBtlsX509.BoringX509Handle Handle
		{
			get
			{
				return (MonoBtlsX509.BoringX509Handle)base.Handle;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00009A2B File Offset: 0x00007C2B
		internal MonoBtlsX509(MonoBtlsX509.BoringX509Handle handle)
			: base(handle)
		{
		}

		// Token: 0x06000355 RID: 853
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_up_ref(IntPtr handle);

		// Token: 0x06000356 RID: 854
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_from_data(IntPtr data, int len, MonoBtlsX509Format format);

		// Token: 0x06000357 RID: 855
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_get_subject_name(IntPtr handle);

		// Token: 0x06000358 RID: 856
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_get_raw_data(IntPtr handle, IntPtr bio, MonoBtlsX509Format format);

		// Token: 0x06000359 RID: 857
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_cmp(IntPtr a, IntPtr b);

		// Token: 0x0600035A RID: 858
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_free(IntPtr handle);

		// Token: 0x0600035B RID: 859
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_add_explicit_trust(IntPtr handle, MonoBtlsX509TrustKind kind);

		// Token: 0x0600035C RID: 860 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		internal MonoBtlsX509 Copy()
		{
			IntPtr intPtr = MonoBtlsX509.mono_btls_x509_up_ref(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "Copy");
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000C21C File Offset: 0x0000A41C
		public static MonoBtlsX509 LoadFromData(byte[] buffer, MonoBtlsX509Format format)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(buffer.Length);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			MonoBtlsX509 monoBtlsX;
			try
			{
				Marshal.Copy(buffer, 0, intPtr, buffer.Length);
				IntPtr intPtr2 = MonoBtlsX509.mono_btls_x509_from_data(intPtr, buffer.Length, format);
				if (intPtr2 == IntPtr.Zero)
				{
					throw new MonoBtlsException("Failed to read certificate from data.");
				}
				monoBtlsX = new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr2));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return monoBtlsX;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000C298 File Offset: 0x0000A498
		public MonoBtlsX509Name GetSubjectName()
		{
			IntPtr intPtr = MonoBtlsX509.mono_btls_x509_get_subject_name(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "GetSubjectName");
			return new MonoBtlsX509Name(new MonoBtlsX509Name.BoringX509NameHandle(intPtr, false));
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		public long GetSubjectNameHash()
		{
			base.CheckThrow();
			long hash;
			using (MonoBtlsX509Name subjectName = this.GetSubjectName())
			{
				hash = subjectName.GetHash();
			}
			return hash;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000C318 File Offset: 0x0000A518
		public byte[] GetRawData(MonoBtlsX509Format format)
		{
			byte[] data;
			using (MonoBtlsBioMemory monoBtlsBioMemory = new MonoBtlsBioMemory())
			{
				int num = MonoBtlsX509.mono_btls_x509_get_raw_data(this.Handle.DangerousGetHandle(), monoBtlsBioMemory.Handle.DangerousGetHandle(), format);
				base.CheckError(num, "GetRawData");
				data = monoBtlsBioMemory.GetData();
			}
			return data;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000C378 File Offset: 0x0000A578
		public static int Compare(MonoBtlsX509 a, MonoBtlsX509 b)
		{
			return MonoBtlsX509.mono_btls_x509_cmp(a.Handle.DangerousGetHandle(), b.Handle.DangerousGetHandle());
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000C398 File Offset: 0x0000A598
		public void AddExplicitTrust(MonoBtlsX509TrustKind kind)
		{
			base.CheckThrow();
			int num = MonoBtlsX509.mono_btls_x509_add_explicit_trust(this.Handle.DangerousGetHandle(), kind);
			base.CheckError(num, "AddExplicitTrust");
		}

		// Token: 0x020000BA RID: 186
		internal class BoringX509Handle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000363 RID: 867 RVA: 0x00009A41 File Offset: 0x00007C41
			public BoringX509Handle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x06000364 RID: 868 RVA: 0x0000C3C9 File Offset: 0x0000A5C9
			protected override bool ReleaseHandle()
			{
				if (this.handle != IntPtr.Zero)
				{
					MonoBtlsX509.mono_btls_x509_free(this.handle);
				}
				return true;
			}

			// Token: 0x06000365 RID: 869 RVA: 0x0000C3E9 File Offset: 0x0000A5E9
			public IntPtr StealHandle()
			{
				return Interlocked.Exchange(ref this.handle, IntPtr.Zero);
			}
		}
	}
}
