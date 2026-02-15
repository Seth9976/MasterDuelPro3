using System;

namespace AssetStudio
{
	// Token: 0x02000163 RID: 355
	public class ImportedMorphVertex
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00015AD0 File Offset: 0x00013CD0
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00015AD8 File Offset: 0x00013CD8
		public uint Index { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00015AE1 File Offset: 0x00013CE1
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00015AE9 File Offset: 0x00013CE9
		public ImportedVertex Vertex { get; set; }
	}
}
