using System;
using YgomSystem.UI;

namespace YgomSystem.Utility
{
	// Token: 0x02000527 RID: 1319
	public static class KeyCommandUtil
	{
		// Token: 0x06002A68 RID: 10856 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize(Action<KeyCommandSetting> onFinished)
		{
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x0000216A File Offset: 0x0000036A
		public static KeyCommandSetting.KeyCommandInfo GetInfo(string label)
		{
			return null;
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x0000216A File Offset: 0x0000036A
		public static KeyCommand Begin(string settingLabel, Action<KeyCommand.OnKeyResult> onSetKeyCallback)
		{
			return null;
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetupKeyCommandReceiver(KeyCommand keyCommand, SelectionItem target)
		{
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x0000216D File Offset: 0x0000036D
		private static void SetupKeyCommandShortcut(KeyCommand keyCommand, SelectionItem target, SelectorManager.KeyType keyType)
		{
		}

		// Token: 0x04002996 RID: 10646
		private static KeyCommandSetting setting;

		// Token: 0x04002997 RID: 10647
		private const string settingPath = "ScriptableObjects/KeyCommand/KeyCommandSetting";
	}
}
