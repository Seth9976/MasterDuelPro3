using System;

namespace YgomSystem
{
	// Token: 0x02000476 RID: 1142
	public static class BranchInfo
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x0000216A File Offset: 0x0000036A
		public static string branchName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06002601 RID: 9729 RVA: 0x0000216A File Offset: 0x0000036A
		public static string commitHash
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isReleaseBranch
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize(BranchData data)
		{
		}

		// Token: 0x0400273E RID: 10046
		private static string s_branchName;

		// Token: 0x0400273F RID: 10047
		private static string s_commitHash;

		// Token: 0x04002740 RID: 10048
		private static bool s_isReleaseBranch;
	}
}
