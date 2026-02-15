using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x02000162 RID: 354
	public class ImportedMorphKeyframe
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00015A8C File Offset: 0x00013C8C
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00015A94 File Offset: 0x00013C94
		public bool hasNormals { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00015A9D File Offset: 0x00013C9D
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00015AA5 File Offset: 0x00013CA5
		public bool hasTangents { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00015AAE File Offset: 0x00013CAE
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00015AB6 File Offset: 0x00013CB6
		public float Weight { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00015ABF File Offset: 0x00013CBF
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00015AC7 File Offset: 0x00013CC7
		public List<ImportedMorphVertex> VertexList { get; set; }
	}
}
