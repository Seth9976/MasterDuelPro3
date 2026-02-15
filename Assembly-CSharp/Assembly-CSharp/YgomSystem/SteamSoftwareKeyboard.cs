using System;

namespace YgomSystem
{
	// Token: 0x020004D8 RID: 1240
	public class SteamSoftwareKeyboard
	{
		// Token: 0x060027B6 RID: 10166 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool Open(SteamSoftwareKeyboard.MODE mode = SteamSoftwareKeyboard.MODE.SingleLine, SteamSoftwareKeyboard.POSITION pos = SteamSoftwareKeyboard.POSITION.BOTTOM, Action callback = null, int x = -1, int y = -1, int w = -1, int h = -1)
		{
			return false;
		}

		// Token: 0x04002870 RID: 10352
		private static bool s_isOpen;

		// Token: 0x04002871 RID: 10353
		private static Action s_callback;

		// Token: 0x020004D9 RID: 1241
		public enum MODE
		{
			// Token: 0x04002873 RID: 10355
			SingleLine,
			// Token: 0x04002874 RID: 10356
			MultipleLines,
			// Token: 0x04002875 RID: 10357
			Email,
			// Token: 0x04002876 RID: 10358
			Numelic
		}

		// Token: 0x020004DA RID: 1242
		public enum POSITION
		{
			// Token: 0x04002878 RID: 10360
			BOTTOM,
			// Token: 0x04002879 RID: 10361
			TOP,
			// Token: 0x0400287A RID: 10362
			DIRECT
		}
	}
}
