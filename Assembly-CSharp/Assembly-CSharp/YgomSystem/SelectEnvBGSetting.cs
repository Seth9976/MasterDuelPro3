using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem
{
	// Token: 0x020004CA RID: 1226
	[Serializable]
	public class SelectEnvBGSetting
	{
		// Token: 0x0600274F RID: 10063 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsMatchBranch(string branchName)
		{
			return false;
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsMatchAnyBranch()
		{
			return false;
		}

		// Token: 0x04002836 RID: 10294
		public List<string> targetBranchPatterns;

		// Token: 0x04002837 RID: 10295
		public Color bgColor;
	}
}
