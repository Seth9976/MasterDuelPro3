using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x02000097 RID: 151
	internal class MonoBtlsBio : MonoBtlsObject
	{
		// Token: 0x0600025F RID: 607 RVA: 0x00009A2B File Offset: 0x00007C2B
		internal MonoBtlsBio(MonoBtlsBio.BoringBioHandle handle)
			: base(handle)
		{
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00009A34 File Offset: 0x00007C34
		protected internal new MonoBtlsBio.BoringBioHandle Handle
		{
			get
			{
				return (MonoBtlsBio.BoringBioHandle)base.Handle;
			}
		}

		// Token: 0x06000261 RID: 609
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_bio_free(IntPtr handle);

		// Token: 0x02000098 RID: 152
		protected internal class BoringBioHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000262 RID: 610 RVA: 0x00009A41 File Offset: 0x00007C41
			public BoringBioHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x06000263 RID: 611 RVA: 0x00009A4B File Offset: 0x00007C4B
			protected override bool ReleaseHandle()
			{
				if (this.handle != IntPtr.Zero)
				{
					MonoBtlsBio.mono_btls_bio_free(this.handle);
					this.handle = IntPtr.Zero;
				}
				return true;
			}
		}
	}
}
