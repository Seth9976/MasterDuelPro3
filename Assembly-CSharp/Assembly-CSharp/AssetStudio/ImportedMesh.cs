using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x02000153 RID: 339
	public class ImportedMesh
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000156A7 File Offset: 0x000138A7
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x000156AF File Offset: 0x000138AF
		public string Path { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000156B8 File Offset: 0x000138B8
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x000156C0 File Offset: 0x000138C0
		public List<ImportedVertex> VertexList { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x000156C9 File Offset: 0x000138C9
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x000156D1 File Offset: 0x000138D1
		public List<ImportedSubmesh> SubmeshList { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000156DA File Offset: 0x000138DA
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x000156E2 File Offset: 0x000138E2
		public List<ImportedBone> BoneList { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000156EB File Offset: 0x000138EB
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x000156F3 File Offset: 0x000138F3
		public bool hasNormal { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x000156FC File Offset: 0x000138FC
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00015704 File Offset: 0x00013904
		public bool[] hasUV { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0001570D File Offset: 0x0001390D
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00015715 File Offset: 0x00013915
		public bool hasTangent { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0001571E File Offset: 0x0001391E
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00015726 File Offset: 0x00013926
		public bool hasColor { get; set; }
	}
}
