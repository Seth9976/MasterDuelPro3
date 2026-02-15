using System;
using UnityEngine;

namespace YgomGame.Menu
{
	// Token: 0x02000AEB RID: 2795
	public class SaveDialogViewController : DialogViewControllerBase, IBokeSupported
	{
		// Token: 0x0600515C RID: 20828 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool LoadPrefab()
		{
			return false;
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string title, string message, string button1Label, Action button1Action, string button2Label, Action button2Action, string button3Label, Action button3Action)
		{
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupUI()
		{
		}

		// Token: 0x06005160 RID: 20832 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x04008FA9 RID: 36777
		private static GameObject prefab;
	}
}
