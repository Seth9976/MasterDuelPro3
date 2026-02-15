using System;
using YgomSystem.UI;

namespace YgomSystem.Utility
{
	// Token: 0x02000523 RID: 1315
	public class KeyCommand
	{
		// Token: 0x06002A61 RID: 10849 RVA: 0x00002739 File Offset: 0x00000939
		public KeyCommand(KeyCommandSetting.KeyCommandInfo info, Action<KeyCommand.OnKeyResult> onSetKeyCallback)
		{
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetKey(SelectorManager.KeyType keyType)
		{
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x0000216D File Offset: 0x0000036D
		public void Reset()
		{
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x0000216D File Offset: 0x0000036D
		private void InvokeOnSetKeyResult(KeyCommand.OnKeyResult result)
		{
		}

		// Token: 0x0400298B RID: 10635
		private KeyCommandSetting.KeyCommandInfo info;

		// Token: 0x0400298C RID: 10636
		private int index;

		// Token: 0x0400298D RID: 10637
		private Action<KeyCommand.OnKeyResult> onSetKeyCallback;

		// Token: 0x02000524 RID: 1316
		public enum OnKeyResult
		{
			// Token: 0x0400298F RID: 10639
			Success,
			// Token: 0x04002990 RID: 10640
			Complete,
			// Token: 0x04002991 RID: 10641
			AlreadyCompleted,
			// Token: 0x04002992 RID: 10642
			Failed
		}
	}
}
