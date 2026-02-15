using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200003C RID: 60
	internal struct EntryPatchData
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000089BC File Offset: 0x00006BBC
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x000089C4 File Offset: 0x00006BC4
		public long SizePatchOffset { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000089CD File Offset: 0x00006BCD
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x000089D5 File Offset: 0x00006BD5
		public long CrcPatchOffset { get; set; }
	}
}
