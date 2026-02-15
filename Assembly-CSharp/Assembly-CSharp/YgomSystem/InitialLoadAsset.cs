using System;

namespace YgomSystem
{
	// Token: 0x020004A7 RID: 1191
	public class InitialLoadAsset
	{
		// Token: 0x06002671 RID: 9841 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Load()
		{
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsLoaded()
		{
			return false;
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Unload()
		{
		}

		// Token: 0x04002790 RID: 10128
		private static bool s_isLoaded;

		// Token: 0x04002791 RID: 10129
		private static readonly string[] kLoadList;
	}
}
