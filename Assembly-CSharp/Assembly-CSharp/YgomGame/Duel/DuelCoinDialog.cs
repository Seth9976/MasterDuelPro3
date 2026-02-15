using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000D57 RID: 3415
	public class DuelCoinDialog : DuelDialogBase
	{
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x0600635B RID: 25435 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool useFieldView
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600635C RID: 25436 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(RunEffectWorker effectWorker, Transform parent, Action<DuelCoinDialog> finishCallback)
		{
		}

		// Token: 0x0600635D RID: 25437 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CreateUI()
		{
		}

		// Token: 0x0600635E RID: 25438 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectItem(DuelCoinDialog.Result result)
		{
		}

		// Token: 0x0600635F RID: 25439 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectDecideButton()
		{
		}

		// Token: 0x06006360 RID: 25440 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetResult(DuelCoinDialog.Result result)
		{
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x0000216D File Offset: 0x0000036D
		private void CancelResult()
		{
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCoinList()
		{
		}

		// Token: 0x06006363 RID: 25443 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateDecideButton()
		{
		}

		// Token: 0x06006364 RID: 25444 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnClosed()
		{
		}

		// Token: 0x06006365 RID: 25445 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ShowUI()
		{
		}

		// Token: 0x06006366 RID: 25446 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void HideUI()
		{
		}

		// Token: 0x06006367 RID: 25447 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Abort()
		{
		}

		// Token: 0x06006368 RID: 25448 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnCancel()
		{
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open(string message, Action<DuelCoinDialog.Result, bool> resultCallback, Action openCallback)
		{
		}

		// Token: 0x04009D79 RID: 40313
		private Action<DuelCoinDialog.Result, bool> resultCallback;

		// Token: 0x04009D7A RID: 40314
		private DuelCoinDialog.Result result;

		// Token: 0x04009D7B RID: 40315
		private ExtendedTextMeshProUGUI textMessage;

		// Token: 0x04009D7C RID: 40316
		private List<DuelCoinDialog.CoinInfo> coinList;

		// Token: 0x04009D7D RID: 40317
		private SelectionButton decideButton;

		// Token: 0x04009D7E RID: 40318
		private const string prefabPath = "Prefabs/Duel/DuelCoinDialog";

		// Token: 0x02000D58 RID: 3416
		public enum Result
		{
			// Token: 0x04009D80 RID: 40320
			Back,
			// Token: 0x04009D81 RID: 40321
			Front,
			// Token: 0x04009D82 RID: 40322
			None
		}

		// Token: 0x02000D59 RID: 3417
		private class CoinInfo
		{
			// Token: 0x04009D83 RID: 40323
			public ElementObjectManager coin;

			// Token: 0x04009D84 RID: 40324
			public int index;

			// Token: 0x04009D85 RID: 40325
			public DuelCoinDialog.Result result;
		}
	}
}
