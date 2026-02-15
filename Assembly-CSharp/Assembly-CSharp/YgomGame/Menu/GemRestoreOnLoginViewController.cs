using System;
using System.Collections;
using System.Collections.Generic;
using YgomGame.GemShop;
using YgomSystem.Network;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000A7D RID: 2685
	public class GemRestoreOnLoginViewController : BaseMenuViewController
	{
		// Token: 0x06004E8E RID: 20110 RVA: 0x0000216D File Offset: 0x0000036D
		public static void GoFromTestScene()
		{
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Go(ViewControllerManager manager, Action onEnd)
		{
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004E92 RID: 20114 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06004E93 RID: 20115 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator ProcessGemRestoring()
		{
			return null;
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator CallNetworkAPI(Handle handle, Action<int> callback)
		{
			return null;
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, string> AnalyzeAndGetProductInfos()
		{
			return null;
		}

		// Token: 0x04008C57 RID: 35927
		private const string VC_PATH = "GemRestoreOnLogin/GemRestoreOnLogin";

		// Token: 0x04008C58 RID: 35928
		private const string ARG_KEY_NOREBOOT = "NoRebootMode";

		// Token: 0x04008C59 RID: 35929
		private const string ARG_KEY_ONEND = "OnEnd";

		// Token: 0x04008C5A RID: 35930
		private static IEnumerator s_Routine;

		// Token: 0x04008C5B RID: 35931
		private YgomSystem.Network.EventHandler _onNetworkErrorHandler;

		// Token: 0x04008C5C RID: 35932
		private bool _isNoRebootMode;

		// Token: 0x04008C5D RID: 35933
		private Action _onEnd;

		// Token: 0x04008C5E RID: 35934
		private JsonGemShopAnalyzer _pdAnalyzer;
	}
}
