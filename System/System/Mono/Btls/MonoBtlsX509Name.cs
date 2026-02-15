using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Btls
{
	// Token: 0x020000C7 RID: 199
	internal class MonoBtlsX509Name : MonoBtlsObject
	{
		// Token: 0x06000396 RID: 918
		[DllImport("libmono-btls-shared")]
		private static extern long mono_btls_x509_name_hash(IntPtr handle);

		// Token: 0x06000397 RID: 919
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_get_entry_count(IntPtr handle);

		// Token: 0x06000398 RID: 920
		[DllImport("libmono-btls-shared")]
		private static extern MonoBtlsX509NameEntryType mono_btls_x509_name_get_entry_type(IntPtr name, int index);

		// Token: 0x06000399 RID: 921
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_get_entry_oid(IntPtr name, int index, IntPtr buffer, int size);

		// Token: 0x0600039A RID: 922
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_get_entry_oid_data(IntPtr name, int index, out IntPtr data);

		// Token: 0x0600039B RID: 923
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_get_entry_value(IntPtr name, int index, out int tag, out IntPtr str);

		// Token: 0x0600039C RID: 924
		[DllImport("libmono-btls-shared")]
		private unsafe static extern IntPtr mono_btls_x509_name_from_data(void* data, int len, int use_canon_enc);

		// Token: 0x0600039D RID: 925
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_name_free(IntPtr handle);

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000C9DC File Offset: 0x0000ABDC
		internal new MonoBtlsX509Name.BoringX509NameHandle Handle
		{
			get
			{
				return (MonoBtlsX509Name.BoringX509NameHandle)base.Handle;
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00009A2B File Offset: 0x00007C2B
		internal MonoBtlsX509Name(MonoBtlsX509Name.BoringX509NameHandle handle)
			: base(handle)
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000C9E9 File Offset: 0x0000ABE9
		public long GetHash()
		{
			return MonoBtlsX509Name.mono_btls_x509_name_hash(this.Handle.DangerousGetHandle());
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000C9FB File Offset: 0x0000ABFB
		public int GetEntryCount()
		{
			return MonoBtlsX509Name.mono_btls_x509_name_get_entry_count(this.Handle.DangerousGetHandle());
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000CA0D File Offset: 0x0000AC0D
		public MonoBtlsX509NameEntryType GetEntryType(int index)
		{
			if (index >= this.GetEntryCount())
			{
				throw new ArgumentOutOfRangeException();
			}
			return MonoBtlsX509Name.mono_btls_x509_name_get_entry_type(this.Handle.DangerousGetHandle(), index);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000CA30 File Offset: 0x0000AC30
		public string GetEntryOid(int index)
		{
			if (index >= this.GetEntryCount())
			{
				throw new ArgumentOutOfRangeException();
			}
			IntPtr intPtr = Marshal.AllocHGlobal(4096);
			string text;
			try
			{
				int num = MonoBtlsX509Name.mono_btls_x509_name_get_entry_oid(this.Handle.DangerousGetHandle(), index, intPtr, 4096);
				base.CheckError(num > 0, "GetEntryOid");
				text = Marshal.PtrToStringAnsi(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return text;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000CAA0 File Offset: 0x0000ACA0
		public byte[] GetEntryOidData(int index)
		{
			IntPtr intPtr;
			int num = MonoBtlsX509Name.mono_btls_x509_name_get_entry_oid_data(this.Handle.DangerousGetHandle(), index, out intPtr);
			base.CheckError(num > 0, "GetEntryOidData");
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			return array;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		public unsafe string GetEntryValue(int index, out int tag)
		{
			if (index >= this.GetEntryCount())
			{
				throw new ArgumentOutOfRangeException();
			}
			IntPtr intPtr;
			int num = MonoBtlsX509Name.mono_btls_x509_name_get_entry_value(this.Handle.DangerousGetHandle(), index, out tag, out intPtr);
			if (num <= 0)
			{
				return null;
			}
			string @string;
			try
			{
				@string = new UTF8Encoding().GetString((byte*)(void*)intPtr, num);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					base.FreeDataPtr(intPtr);
				}
			}
			return @string;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000CB58 File Offset: 0x0000AD58
		public unsafe static MonoBtlsX509Name CreateFromData(byte[] data, bool use_canon_enc)
		{
			void* ptr;
			if (data == null || data.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = (void*)(&data[0]);
			}
			IntPtr intPtr = MonoBtlsX509Name.mono_btls_x509_name_from_data(ptr, data.Length, use_canon_enc ? 1 : 0);
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException("mono_btls_x509_name_from_data() failed.");
			}
			return new MonoBtlsX509Name(new MonoBtlsX509Name.BoringX509NameHandle(intPtr, false));
		}

		// Token: 0x020000C8 RID: 200
		internal class BoringX509NameHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x060003A7 RID: 935 RVA: 0x0000CBB0 File Offset: 0x0000ADB0
			internal BoringX509NameHandle(IntPtr handle, bool ownsHandle)
				: base(handle, ownsHandle)
			{
				this.dontFree = !ownsHandle;
			}

			// Token: 0x060003A8 RID: 936 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
			protected override bool ReleaseHandle()
			{
				if (!this.dontFree)
				{
					MonoBtlsX509Name.mono_btls_x509_name_free(this.handle);
				}
				return true;
			}

			// Token: 0x04000301 RID: 769
			private bool dontFree;
		}
	}
}
