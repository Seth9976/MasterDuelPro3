using System;

namespace AssetStudio
{
	// Token: 0x02000157 RID: 343
	public class ImportedBone
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x000157EA File Offset: 0x000139EA
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x000157F2 File Offset: 0x000139F2
		public string Path { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x000157FB File Offset: 0x000139FB
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x00015803 File Offset: 0x00013A03
		public Matrix4x4 Matrix { get; set; }
	}
}
