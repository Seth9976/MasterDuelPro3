using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.Utility;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000D87 RID: 3463
	public abstract class DuelInfoDialogBase : DuelTransitionUIBase
	{
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06006559 RID: 25945 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override UITransitionUtil.BlockType openCloseBlockType
		{
			get
			{
				return UITransitionUtil.BlockType.None;
			}
		}

		// Token: 0x0600655A RID: 25946 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqClose()
		{
		}

		// Token: 0x0600655B RID: 25947 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqSetPlace(DuelInfoDialogBase.Place place, bool immediate)
		{
		}

		// Token: 0x0600655C RID: 25948 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Initialize(RunEffectWorker effectWorker)
		{
		}

		// Token: 0x0600655D RID: 25949
		protected abstract void CreateUI();

		// Token: 0x0600655E RID: 25950 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickCancel()
		{
		}

		// Token: 0x0600655F RID: 25951 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickDecision()
		{
		}

		// Token: 0x06006560 RID: 25952 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ShowUI()
		{
		}

		// Token: 0x06006561 RID: 25953 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void HideUI()
		{
		}

		// Token: 0x06006562 RID: 25954 RVA: 0x0000216D File Offset: 0x0000036D
		protected void Open(string message, DuelInfoDialogBase.Place place, bool cancelable, Action cancelCallback, Action closeCallback = null, bool decidable = false, Action decisionCallback = null)
		{
		}

		// Token: 0x06006563 RID: 25955 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetPlace(DuelInfoDialogBase.Place place, bool immediate)
		{
		}

		// Token: 0x06006564 RID: 25956 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void UpdateUI()
		{
		}

		// Token: 0x06006565 RID: 25957 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePlace()
		{
		}

		// Token: 0x06006566 RID: 25958 RVA: 0x0000216D File Offset: 0x0000036D
		private void Close()
		{
		}

		// Token: 0x04009F9C RID: 40860
		[SerializeField]
		protected GameObject prefabUI;

		// Token: 0x04009F9D RID: 40861
		protected Action cancelCallback;

		// Token: 0x04009F9E RID: 40862
		protected Action decisionCallback;

		// Token: 0x04009F9F RID: 40863
		protected Action closeCallback;

		// Token: 0x04009FA0 RID: 40864
		protected ElementObjectManager ui;

		// Token: 0x04009FA1 RID: 40865
		protected Selector selector;

		// Token: 0x04009FA2 RID: 40866
		protected ExtendedTextMeshProUGUI textMessage;

		// Token: 0x04009FA3 RID: 40867
		protected ContentSizeFitter dialogFitter;

		// Token: 0x04009FA4 RID: 40868
		protected GameObject dialogObject;

		// Token: 0x04009FA5 RID: 40869
		private DuelInfoDialogBase.Place currentPlace;

		// Token: 0x04009FA6 RID: 40870
		private DuelInfoDialogBase.Place targetPlace;

		// Token: 0x04009FA7 RID: 40871
		private float timer;

		// Token: 0x04009FA8 RID: 40872
		private const float placeChangeTime = 1f;

		// Token: 0x02000D88 RID: 3464
		public enum Place
		{
			// Token: 0x04009FAA RID: 40874
			Near,
			// Token: 0x04009FAB RID: 40875
			Nearer,
			// Token: 0x04009FAC RID: 40876
			Center,
			// Token: 0x04009FAD RID: 40877
			Farer,
			// Token: 0x04009FAE RID: 40878
			Far,
			// Token: 0x04009FAF RID: 40879
			Farest
		}

		// Token: 0x02000D89 RID: 3465
		protected class OperationInfo : DuelTransitionUIBase.OperationInfoBase
		{
			// Token: 0x06006568 RID: 25960 RVA: 0x0000216A File Offset: 0x0000036A
			public static DuelInfoDialogBase.OperationInfo CloseOperation(DuelInfoDialogBase dialog)
			{
				return null;
			}

			// Token: 0x06006569 RID: 25961 RVA: 0x0000216A File Offset: 0x0000036A
			public static DuelInfoDialogBase.OperationInfo SetPlaceOperation(DuelInfoDialogBase dialog, DuelInfoDialogBase.Place place, bool immediate)
			{
				return null;
			}
		}
	}
}
