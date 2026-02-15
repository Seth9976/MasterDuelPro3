using System;

namespace AssetStudio
{
	// Token: 0x0200015E RID: 350
	public class ImportedKeyframe<T>
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x000159FD File Offset: 0x00013BFD
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x00015A05 File Offset: 0x00013C05
		public float time { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00015A0E File Offset: 0x00013C0E
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x00015A16 File Offset: 0x00013C16
		public T value { get; set; }

		// Token: 0x06000451 RID: 1105 RVA: 0x00015A1F File Offset: 0x00013C1F
		public ImportedKeyframe(float time, T value)
		{
			this.time = time;
			this.value = value;
		}
	}
}
