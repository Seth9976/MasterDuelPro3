using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x02000154 RID: 340
	public class ImportedSubmesh
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0001572F File Offset: 0x0001392F
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x00015737 File Offset: 0x00013937
		public List<ImportedFace> FaceList { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00015740 File Offset: 0x00013940
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x00015748 File Offset: 0x00013948
		public string Material { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00015751 File Offset: 0x00013951
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00015759 File Offset: 0x00013959
		public int BaseVertex { get; set; }
	}
}
