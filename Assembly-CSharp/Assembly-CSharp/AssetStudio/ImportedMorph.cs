using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x02000160 RID: 352
	public class ImportedMorph
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00015A48 File Offset: 0x00013C48
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00015A50 File Offset: 0x00013C50
		public string Path { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00015A59 File Offset: 0x00013C59
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00015A61 File Offset: 0x00013C61
		public List<ImportedMorphChannel> Channels { get; set; }
	}
}
