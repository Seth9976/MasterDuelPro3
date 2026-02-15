using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000D5A RID: 3418
	public class DuelConfirmDialog : DuelDialogBase
	{
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x0600636C RID: 25452 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool useFieldView
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600636D RID: 25453 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(RunEffectWorker effectWorker, Transform parent, Action<DuelConfirmDialog> finishCallback)
		{
		}

		// Token: 0x0600636E RID: 25454 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CreateUI()
		{
		}

		// Token: 0x0600636F RID: 25455 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDecide(DuelConfirmDialog.Result result)
		{
		}

		// Token: 0x06006370 RID: 25456 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnClosed()
		{
		}

		// Token: 0x06006371 RID: 25457 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ShowUI()
		{
		}

		// Token: 0x06006372 RID: 25458 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void HideUI()
		{
		}

		// Token: 0x06006373 RID: 25459 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Abort()
		{
		}

		// Token: 0x06006374 RID: 25460 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnCancel()
		{
		}

		// Token: 0x06006375 RID: 25461 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open(string message, string rightButtonText, string leftButtonText, Action<DuelConfirmDialog.Result, bool> resultCallback, Action openCallback, bool useFieldView = true)
		{
		}

		// Token: 0x06006376 RID: 25462 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open(string message, Action<DuelConfirmDialog.Result, bool> resultCallback, Action openCallback, bool useFieldView = true)
		{
		}

		// Token: 0x04009D86 RID: 40326
		private DuelConfirmDialog.Result result;

		// Token: 0x04009D87 RID: 40327
		private Action<DuelConfirmDialog.Result, bool> resultCallback;

		// Token: 0x04009D88 RID: 40328
		private ExtendedTextMeshProUGUI textMessage;

		// Token: 0x04009D89 RID: 40329
		private ExtendedTextMeshProUGUI textLeftButton;

		// Token: 0x04009D8A RID: 40330
		private ExtendedTextMeshProUGUI textRightButton;

		// Token: 0x04009D8B RID: 40331
		private ContentSizeFitter dialogFitter;

		// Token: 0x04009D8C RID: 40332
		private RectTransform rightButtonRectTransform;

		// Token: 0x04009D8D RID: 40333
		private SelectionButton rightButton;

		// Token: 0x04009D8E RID: 40334
		private bool useFieldViewFlag;

		// Token: 0x04009D8F RID: 40335
		private const string prefabPath = "Prefabs/Duel/DuelConfirmDialog";

		// Token: 0x02000D5B RID: 3419
		public enum Result
		{
			// Token: 0x04009D91 RID: 40337
			Left,
			// Token: 0x04009D92 RID: 40338
			Right
		}
	}
}
