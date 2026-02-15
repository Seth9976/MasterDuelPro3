using System;

namespace System
{
	// Token: 0x020001A8 RID: 424
	internal enum TypeNameFormatFlags
	{
		// Token: 0x04000613 RID: 1555
		FormatBasic,
		// Token: 0x04000614 RID: 1556
		FormatNamespace,
		// Token: 0x04000615 RID: 1557
		FormatFullInst,
		// Token: 0x04000616 RID: 1558
		FormatAssembly = 4,
		// Token: 0x04000617 RID: 1559
		FormatSignature = 8,
		// Token: 0x04000618 RID: 1560
		FormatNoVersion = 16,
		// Token: 0x04000619 RID: 1561
		FormatAngleBrackets = 64,
		// Token: 0x0400061A RID: 1562
		FormatStubInfo = 128,
		// Token: 0x0400061B RID: 1563
		FormatGenericParam = 256,
		// Token: 0x0400061C RID: 1564
		FormatSerialization = 259
	}
}
