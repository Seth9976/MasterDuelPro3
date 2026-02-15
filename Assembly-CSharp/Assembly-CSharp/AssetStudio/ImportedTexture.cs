using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x0200015A RID: 346
	public class ImportedTexture
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x000158E9 File Offset: 0x00013AE9
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x000158F1 File Offset: 0x00013AF1
		public string Name { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x000158FA File Offset: 0x00013AFA
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x00015902 File Offset: 0x00013B02
		public byte[] Data { get; set; }

		// Token: 0x0600043F RID: 1087 RVA: 0x0001590B File Offset: 0x00013B0B
		public ImportedTexture(MemoryStream stream, string name)
		{
			this.Name = name;
			this.Data = stream.ToArray();
		}
	}
}
