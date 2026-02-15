using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000D60 RID: 3424
	public class DuelDiceDialog : DuelDialogBase
	{
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x060063A4 RID: 25508 RVA: 0x000029CC File Offset: 0x00000BCC
		private int currentResultIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x060063A5 RID: 25509 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool useFieldView
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060063A6 RID: 25510 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(RunEffectWorker effectWorker, Transform parent, Action<DuelDiceDialog> finishCallback)
		{
		}

		// Token: 0x060063A7 RID: 25511 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CreateUI()
		{
		}

		// Token: 0x060063A8 RID: 25512 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectItem(int index)
		{
		}

		// Token: 0x060063A9 RID: 25513 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectDecideButton()
		{
		}

		// Token: 0x060063AA RID: 25514 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetResult(int result, int index)
		{
		}

		// Token: 0x060063AB RID: 25515 RVA: 0x0000216D File Offset: 0x0000036D
		private void CancelLastResult()
		{
		}

		// Token: 0x060063AC RID: 25516 RVA: 0x0000216D File Offset: 0x0000036D
		private void CancelResult(int result)
		{
		}

		// Token: 0x060063AD RID: 25517 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateDiceList()
		{
		}

		// Token: 0x060063AE RID: 25518 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCounter()
		{
		}

		// Token: 0x060063AF RID: 25519 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateDecideButton()
		{
		}

		// Token: 0x060063B0 RID: 25520 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnClosed()
		{
		}

		// Token: 0x060063B1 RID: 25521 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ShowUI()
		{
		}

		// Token: 0x060063B2 RID: 25522 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void HideUI()
		{
		}

		// Token: 0x060063B3 RID: 25523 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Abort()
		{
		}

		// Token: 0x060063B4 RID: 25524 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnCancel()
		{
		}

		// Token: 0x060063B5 RID: 25525 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open(string message, List<int> selectionList, int selectNum, Action<List<int>, bool> resultCallback, Action openCallback)
		{
		}

		// Token: 0x060063B6 RID: 25526 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupDiceList(List<int> selectionList)
		{
		}

		// Token: 0x04009DC7 RID: 40391
		private Action<List<int>, bool> resultCallback;

		// Token: 0x04009DC8 RID: 40392
		private List<int> result;

		// Token: 0x04009DC9 RID: 40393
		private int selectNum;

		// Token: 0x04009DCA RID: 40394
		private ExtendedTextMeshProUGUI textMessage;

		// Token: 0x04009DCB RID: 40395
		private List<DuelDiceDialog.DiceInfo> diceList;

		// Token: 0x04009DCC RID: 40396
		private GameObject counterArea;

		// Token: 0x04009DCD RID: 40397
		private ExtendedTextMeshProUGUI textCounter;

		// Token: 0x04009DCE RID: 40398
		private SelectionButton decideButton;

		// Token: 0x04009DCF RID: 40399
		private const string prefabPath = "Prefabs/Duel/DuelDiceDialog";

		// Token: 0x02000D61 RID: 3425
		private class DiceInfo
		{
			// Token: 0x04009DD0 RID: 40400
			public ElementObjectManager dice;

			// Token: 0x04009DD1 RID: 40401
			public int index;

			// Token: 0x04009DD2 RID: 40402
			public int pip;
		}
	}
}
