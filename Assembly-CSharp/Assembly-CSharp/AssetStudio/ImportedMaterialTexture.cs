using System;

namespace AssetStudio
{
	// Token: 0x02000159 RID: 345
	public class ImportedMaterialTexture
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x000158A5 File Offset: 0x00013AA5
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x000158AD File Offset: 0x00013AAD
		public string Name { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x000158B6 File Offset: 0x00013AB6
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x000158BE File Offset: 0x00013ABE
		public int Dest { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x000158C7 File Offset: 0x00013AC7
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x000158CF File Offset: 0x00013ACF
		public Vector2 Offset { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x000158D8 File Offset: 0x00013AD8
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x000158E0 File Offset: 0x00013AE0
		public Vector2 Scale { get; set; }
	}
}
