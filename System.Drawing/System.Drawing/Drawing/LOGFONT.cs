using System;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x0200006C RID: 108
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct LOGFONT
	{
		// Token: 0x040001F3 RID: 499
		internal int lfHeight;

		// Token: 0x040001F4 RID: 500
		internal uint lfWidth;

		// Token: 0x040001F5 RID: 501
		internal uint lfEscapement;

		// Token: 0x040001F6 RID: 502
		internal uint lfOrientation;

		// Token: 0x040001F7 RID: 503
		internal uint lfWeight;

		// Token: 0x040001F8 RID: 504
		internal byte lfItalic;

		// Token: 0x040001F9 RID: 505
		internal byte lfUnderline;

		// Token: 0x040001FA RID: 506
		internal byte lfStrikeOut;

		// Token: 0x040001FB RID: 507
		internal byte lfCharSet;

		// Token: 0x040001FC RID: 508
		internal byte lfOutPrecision;

		// Token: 0x040001FD RID: 509
		internal byte lfClipPrecision;

		// Token: 0x040001FE RID: 510
		internal byte lfQuality;

		// Token: 0x040001FF RID: 511
		internal byte lfPitchAndFamily;

		// Token: 0x04000200 RID: 512
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		internal string lfFaceName;
	}
}
