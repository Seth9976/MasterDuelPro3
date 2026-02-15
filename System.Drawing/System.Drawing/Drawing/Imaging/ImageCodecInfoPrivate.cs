using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x0200008A RID: 138
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal class ImageCodecInfoPrivate
	{
		// Token: 0x0400027D RID: 637
		[MarshalAs(UnmanagedType.Struct)]
		public Guid Clsid;

		// Token: 0x0400027E RID: 638
		[MarshalAs(UnmanagedType.Struct)]
		public Guid FormatID;

		// Token: 0x0400027F RID: 639
		public IntPtr CodecName = IntPtr.Zero;

		// Token: 0x04000280 RID: 640
		public IntPtr DllName = IntPtr.Zero;

		// Token: 0x04000281 RID: 641
		public IntPtr FormatDescription = IntPtr.Zero;

		// Token: 0x04000282 RID: 642
		public IntPtr FilenameExtension = IntPtr.Zero;

		// Token: 0x04000283 RID: 643
		public IntPtr MimeType = IntPtr.Zero;

		// Token: 0x04000284 RID: 644
		public int Flags;

		// Token: 0x04000285 RID: 645
		public int Version;

		// Token: 0x04000286 RID: 646
		public int SigCount;

		// Token: 0x04000287 RID: 647
		public int SigSize;

		// Token: 0x04000288 RID: 648
		public IntPtr SigPattern = IntPtr.Zero;

		// Token: 0x04000289 RID: 649
		public IntPtr SigMask = IntPtr.Zero;
	}
}
