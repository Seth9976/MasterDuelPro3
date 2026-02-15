using System;

namespace System.Drawing
{
	// Token: 0x0200006A RID: 106
	internal struct GdiplusStartupInput
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0000D098 File Offset: 0x0000B298
		internal static GdiplusStartupInput MakeGdiplusStartupInput()
		{
			return new GdiplusStartupInput
			{
				GdiplusVersion = 1U,
				DebugEventCallback = IntPtr.Zero,
				SuppressBackgroundThread = 0,
				SuppressExternalCodecs = 0
			};
		}

		// Token: 0x040001ED RID: 493
		internal uint GdiplusVersion;

		// Token: 0x040001EE RID: 494
		internal IntPtr DebugEventCallback;

		// Token: 0x040001EF RID: 495
		internal int SuppressBackgroundThread;

		// Token: 0x040001F0 RID: 496
		internal int SuppressExternalCodecs;
	}
}
