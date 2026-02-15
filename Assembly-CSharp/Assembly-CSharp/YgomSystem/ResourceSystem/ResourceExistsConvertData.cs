using System;
using System.Collections.Generic;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006EF RID: 1775
	public class ResourceExistsConvertData
	{
		// Token: 0x06003750 RID: 14160 RVA: 0x0000216D File Offset: 0x0000036D
		public void Load()
		{
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Exists(string path)
		{
			return false;
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x0000216D File Offset: 0x0000036D
		public void Clear()
		{
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadFromFile()
		{
		}

		// Token: 0x04003161 RID: 12641
		private const string kExistsFileName = "ExistsFileList.json";

		// Token: 0x04003162 RID: 12642
		private HashSet<string> existsFileList;
	}
}
