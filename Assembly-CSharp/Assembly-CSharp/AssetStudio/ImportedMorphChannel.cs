using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x02000161 RID: 353
	public class ImportedMorphChannel
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00015A6A File Offset: 0x00013C6A
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x00015A72 File Offset: 0x00013C72
		public string Name { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00015A7B File Offset: 0x00013C7B
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x00015A83 File Offset: 0x00013C83
		public List<ImportedMorphKeyframe> KeyframeList { get; set; }
	}
}
