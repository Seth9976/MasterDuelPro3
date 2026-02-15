using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x02000099 RID: 153
	internal class MonoBtlsBioMemory : MonoBtlsBio
	{
		// Token: 0x06000264 RID: 612
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_bio_mem_new();

		// Token: 0x06000265 RID: 613
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_bio_mem_get_data(IntPtr handle, out IntPtr data);

		// Token: 0x06000266 RID: 614 RVA: 0x00009A76 File Offset: 0x00007C76
		public MonoBtlsBioMemory()
			: base(new MonoBtlsBio.BoringBioHandle(MonoBtlsBioMemory.mono_btls_bio_mem_new()))
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009A88 File Offset: 0x00007C88
		public byte[] GetData()
		{
			bool flag = false;
			byte[] array2;
			try
			{
				base.Handle.DangerousAddRef(ref flag);
				IntPtr intPtr;
				int num = MonoBtlsBioMemory.mono_btls_bio_mem_get_data(base.Handle.DangerousGetHandle(), out intPtr);
				base.CheckError(num > 0, "GetData");
				byte[] array = new byte[num];
				Marshal.Copy(intPtr, array, 0, num);
				array2 = array;
			}
			finally
			{
				if (flag)
				{
					base.Handle.DangerousRelease();
				}
			}
			return array2;
		}
	}
}
