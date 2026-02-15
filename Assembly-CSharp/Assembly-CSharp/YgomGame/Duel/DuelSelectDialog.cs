using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000D96 RID: 3478
	public class DuelSelectDialog : DuelDialogBase
	{
		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06006645 RID: 26181 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool useFieldView
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006646 RID: 26182 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(RunEffectWorker effectWorker, Transform parent, Action<DuelSelectDialog> finishCallback)
		{
		}

		// Token: 0x06006647 RID: 26183 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CreateUI()
		{
		}

		// Token: 0x06006648 RID: 26184 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupTab(int tabNum)
		{
		}

		// Token: 0x06006649 RID: 26185 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool DecideEffect(int index)
		{
			return false;
		}

		// Token: 0x0600664A RID: 26186 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetResult(int index)
		{
		}

		// Token: 0x0600664B RID: 26187 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTab()
		{
		}

		// Token: 0x0600664C RID: 26188 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateMessage(int effectIndex)
		{
		}

		// Token: 0x0600664D RID: 26189 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnClosed()
		{
		}

		// Token: 0x0600664E RID: 26190 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ShowUI()
		{
		}

		// Token: 0x0600664F RID: 26191 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void HideUI()
		{
		}

		// Token: 0x06006650 RID: 26192 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Abort()
		{
		}

		// Token: 0x06006651 RID: 26193 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnCancel()
		{
		}

		// Token: 0x06006652 RID: 26194 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open(string message, List<DuelSelectDialog.Info> infoList, Action<int, bool> resultCallback, Action openCallback)
		{
		}

		// Token: 0x0400A055 RID: 41045
		private int result;

		// Token: 0x0400A056 RID: 41046
		private Action<int, bool> resultCallback;

		// Token: 0x0400A057 RID: 41047
		private TMP_Text textMessage;

		// Token: 0x0400A058 RID: 41048
		private TMP_Text textEffectMessage;

		// Token: 0x0400A059 RID: 41049
		private ExtendedScrollRect textScroll;

		// Token: 0x0400A05A RID: 41050
		private ContentSizeFitter contentFitter;

		// Token: 0x0400A05B RID: 41051
		private ContentSizeFitter textFitter;

		// Token: 0x0400A05C RID: 41052
		private ElementObjectManager tabTemplate;

		// Token: 0x0400A05D RID: 41053
		private RectTransform tabParent;

		// Token: 0x0400A05E RID: 41054
		private SelectionButton decideButton;

		// Token: 0x0400A05F RID: 41055
		private Image disableScreen;

		// Token: 0x0400A060 RID: 41056
		private List<ElementObjectManager> tabList;

		// Token: 0x0400A061 RID: 41057
		private List<DuelSelectDialog.Info> infoList;

		// Token: 0x0400A062 RID: 41058
		private const string prefabPath = "Prefabs/Duel/DuelSelectDialog";

		// Token: 0x02000D97 RID: 3479
		public struct Info
		{
			// Token: 0x0400A063 RID: 41059
			public string message;

			// Token: 0x0400A064 RID: 41060
			public bool isEnable;
		}
	}
}
