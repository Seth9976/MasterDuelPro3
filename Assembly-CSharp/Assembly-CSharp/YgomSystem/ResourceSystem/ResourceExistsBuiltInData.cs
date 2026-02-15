using System;
using System.Collections.Generic;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006EE RID: 1774
	public class ResourceExistsBuiltInData
	{
		// Token: 0x0600374A RID: 14154 RVA: 0x0000216A File Offset: 0x0000036A
		public static ResourceExistsBuiltInData GetInstance()
		{
			return null;
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x0000216D File Offset: 0x0000036D
		private void Load()
		{
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadFromFile()
		{
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool Exists(string path)
		{
			return false;
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool exists(string path)
		{
			return false;
		}

		// Token: 0x0400315E RID: 12638
		private const string kExistsFileName = "ExistsBuiltInFileList.json";

		// Token: 0x0400315F RID: 12639
		private HashSet<string> existsFileList;

		// Token: 0x04003160 RID: 12640
		private static ResourceExistsBuiltInData s_instance;
	}
}
