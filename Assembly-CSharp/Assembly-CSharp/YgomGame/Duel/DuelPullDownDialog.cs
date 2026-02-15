using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000D91 RID: 3473
	public class DuelPullDownDialog : DuelDialogBase
	{
		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060065FE RID: 26110 RVA: 0x000029CC File Offset: 0x00000BCC
		private int currentResultIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060065FF RID: 26111 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool useFieldView
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006600 RID: 26112 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(RunEffectWorker effectWorker, Transform parent, Action<DuelPullDownDialog> finishCallback)
		{
		}

		// Token: 0x06006601 RID: 26113 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CreateUI()
		{
		}

		// Token: 0x06006602 RID: 26114 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeScroll()
		{
		}

		// Token: 0x06006603 RID: 26115 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreateEntity(GameObject obj)
		{
		}

		// Token: 0x06006604 RID: 26116 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntity(GameObject item, int index)
		{
		}

		// Token: 0x06006605 RID: 26117 RVA: 0x0000216D File Offset: 0x0000036D
		private void Select(int index)
		{
		}

		// Token: 0x06006606 RID: 26118 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectItem(int index)
		{
		}

		// Token: 0x06006607 RID: 26119 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectDecideButton()
		{
		}

		// Token: 0x06006608 RID: 26120 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetResult(int result, int index)
		{
		}

		// Token: 0x06006609 RID: 26121 RVA: 0x0000216D File Offset: 0x0000036D
		private void CancelLastResult()
		{
		}

		// Token: 0x0600660A RID: 26122 RVA: 0x0000216D File Offset: 0x0000036D
		private void CancelResult(int result)
		{
		}

		// Token: 0x0600660B RID: 26123 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCounter()
		{
		}

		// Token: 0x0600660C RID: 26124 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateDecideButton()
		{
		}

		// Token: 0x0600660D RID: 26125 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnClosed()
		{
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ShowUI()
		{
		}

		// Token: 0x0600660F RID: 26127 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void HideUI()
		{
		}

		// Token: 0x06006610 RID: 26128 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Abort()
		{
		}

		// Token: 0x06006611 RID: 26129 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnCancel()
		{
		}

		// Token: 0x06006612 RID: 26130 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void FinishFieldView(bool isAbort)
		{
		}

		// Token: 0x06006613 RID: 26131 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open(string message, List<string> selectionList, int selectNum, Action<List<int>, bool> resultCallback, Action openCallback)
		{
		}

		// Token: 0x0400A039 RID: 41017
		private Action<List<int>, bool> resultCallback;

		// Token: 0x0400A03A RID: 41018
		private int selectNum;

		// Token: 0x0400A03B RID: 41019
		private List<int> result;

		// Token: 0x0400A03C RID: 41020
		private Selector scrollSelector;

		// Token: 0x0400A03D RID: 41021
		private List<string> selectionList;

		// Token: 0x0400A03E RID: 41022
		private InfinityScrollView scroll;

		// Token: 0x0400A03F RID: 41023
		private ExtendedTextMeshProUGUI textMessage;

		// Token: 0x0400A040 RID: 41024
		private SelectionButton decideButton;

		// Token: 0x0400A041 RID: 41025
		private GameObject counterArea;

		// Token: 0x0400A042 RID: 41026
		private ExtendedTextMeshProUGUI textCounter;

		// Token: 0x0400A043 RID: 41027
		private const string prefabPath = "Prefabs/Duel/DuelPullDownDialog";
	}
}
