using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.Utility;

namespace YgomGame.Duel
{
	// Token: 0x02000D5F RID: 3423
	public abstract class DuelDialogBase : DuelUIBase
	{
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06006391 RID: 25489
		protected abstract bool useFieldView { get; }

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06006392 RID: 25490 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override UITransitionUtil.BlockType openCloseBlockType
		{
			get
			{
				return UITransitionUtil.BlockType.None;
			}
		}

		// Token: 0x06006393 RID: 25491 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Initialize(RunEffectWorker effectWorker)
		{
		}

		// Token: 0x06006394 RID: 25492 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void CreateUI()
		{
		}

		// Token: 0x06006395 RID: 25493 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateBaseUI()
		{
		}

		// Token: 0x06006396 RID: 25494 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetupFieldViewButton(ElementObjectManager eom, string buttonLabel, string shortcutLabel, string iconOnLabel, string iconOffLabel)
		{
		}

		// Token: 0x06006397 RID: 25495 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetDialogSize(Vector2 size)
		{
		}

		// Token: 0x06006398 RID: 25496 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangeFieldView(bool fieldViewing, bool isAbort)
		{
		}

		// Token: 0x06006399 RID: 25497 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void StartFieldView()
		{
		}

		// Token: 0x0600639A RID: 25498 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void FinishFieldView(bool isAbort)
		{
		}

		// Token: 0x0600639B RID: 25499 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Abort()
		{
		}

		// Token: 0x0600639C RID: 25500
		public abstract void OnCancel();

		// Token: 0x0600639D RID: 25501 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Open(Action openedCallback = null)
		{
		}

		// Token: 0x0600639E RID: 25502 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Close(Action closedCallback = null)
		{
		}

		// Token: 0x0600639F RID: 25503 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ShowUI()
		{
		}

		// Token: 0x060063A0 RID: 25504 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void HideUI()
		{
		}

		// Token: 0x060063A1 RID: 25505 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenCardInfo(int mixid, int efx, bool effectidflag)
		{
		}

		// Token: 0x060063A2 RID: 25506 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangeDuelLogOpenClose(bool isOpen)
		{
		}

		// Token: 0x04009DB5 RID: 40373
		[SerializeField]
		protected GameObject prefabDialogBase;

		// Token: 0x04009DB6 RID: 40374
		private ElementObjectManager dialogBaseUI;

		// Token: 0x04009DB7 RID: 40375
		private Selector baseSelector;

		// Token: 0x04009DB8 RID: 40376
		private SelectionItem screenItem;

		// Token: 0x04009DB9 RID: 40377
		protected bool fieldViewing;

		// Token: 0x04009DBA RID: 40378
		private Transform fieldViewRoot;

		// Token: 0x04009DBB RID: 40379
		private Image fieldViewIconOn;

		// Token: 0x04009DBC RID: 40380
		private Image fieldViewIconOff;

		// Token: 0x04009DBD RID: 40381
		private TweenPositionTo tweenStartFieldView;

		// Token: 0x04009DBE RID: 40382
		private const float fieldViewToPosOffset = 18f;

		// Token: 0x04009DBF RID: 40383
		private const string tweenLabelStartFieldView = "StartFieldView";

		// Token: 0x04009DC0 RID: 40384
		private const string tweenLabelFinishFieldView = "FinishFieldView";

		// Token: 0x04009DC1 RID: 40385
		private bool useCardInfo;

		// Token: 0x04009DC2 RID: 40386
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x04009DC3 RID: 40387
		protected ElementObjectManager ui;

		// Token: 0x04009DC4 RID: 40388
		protected Selector selector;

		// Token: 0x04009DC5 RID: 40389
		protected RectTransform dialog;

		// Token: 0x04009DC6 RID: 40390
		protected List<Selector> selectors;
	}
}
