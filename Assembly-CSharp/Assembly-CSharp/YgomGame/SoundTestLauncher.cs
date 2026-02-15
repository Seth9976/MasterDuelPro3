using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame
{
	// Token: 0x020007DB RID: 2011
	public class SoundTestLauncher : ViewController
	{
		// Token: 0x06003E9D RID: 16029 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupScrollToSelectingItem(SelectionItem item, RectTransform itemRootRect)
		{
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupAnalogInputShortcut(SelectionItem item, Transform player, InputField inputX, InputField inputY, InputField inputZ)
		{
		}

		// Token: 0x06003EA0 RID: 16032 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupPlayShortcut(SelectionItem item, SoundTestLauncher.TestPlayerInfo info)
		{
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x04003790 RID: 14224
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x04003791 RID: 14225
		[SerializeField]
		private GameObject prefabPlayer;

		// Token: 0x04003792 RID: 14226
		private ElementObjectManager ui;

		// Token: 0x04003793 RID: 14227
		private ElementObjectManager infoTemplate;

		// Token: 0x04003794 RID: 14228
		private Transform infoListParent;

		// Token: 0x04003795 RID: 14229
		private RectTransform label3dParent;

		// Token: 0x04003796 RID: 14230
		private List<SoundTestLauncher.TestPlayerInfo> playerList;

		// Token: 0x04003797 RID: 14231
		private int createCounter;

		// Token: 0x020007DC RID: 2012
		private class TestPlayerInfo
		{
			// Token: 0x04003798 RID: 14232
			public GameObject player;

			// Token: 0x04003799 RID: 14233
			public string soundLabel;

			// Token: 0x0400379A RID: 14234
			public bool oneShot;

			// Token: 0x0400379B RID: 14235
			public RectTransform label3d;
		}
	}
}
