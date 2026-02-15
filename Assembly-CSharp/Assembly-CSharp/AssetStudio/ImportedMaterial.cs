using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x02000158 RID: 344
	public class ImportedMaterial
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0001580C File Offset: 0x00013A0C
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00015814 File Offset: 0x00013A14
		public string Name { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0001581D File Offset: 0x00013A1D
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00015825 File Offset: 0x00013A25
		public Color Diffuse { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0001582E File Offset: 0x00013A2E
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00015836 File Offset: 0x00013A36
		public Color Ambient { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001583F File Offset: 0x00013A3F
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00015847 File Offset: 0x00013A47
		public Color Specular { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00015850 File Offset: 0x00013A50
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00015858 File Offset: 0x00013A58
		public Color Emissive { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00015861 File Offset: 0x00013A61
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x00015869 File Offset: 0x00013A69
		public Color Reflection { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00015872 File Offset: 0x00013A72
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x0001587A File Offset: 0x00013A7A
		public float Shininess { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00015883 File Offset: 0x00013A83
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0001588B File Offset: 0x00013A8B
		public float Transparency { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00015894 File Offset: 0x00013A94
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0001589C File Offset: 0x00013A9C
		public List<ImportedMaterialTexture> Textures { get; set; }
	}
}
