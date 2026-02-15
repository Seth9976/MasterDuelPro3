using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000D90 RID: 3472
	public class DuelOkDialog : DuelDialogBase
	{
		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060065F2 RID: 26098 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool useFieldView
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060065F3 RID: 26099 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(RunEffectWorker effectWorker, Transform parent, Action<DuelOkDialog> finishCallback)
		{
		}

		// Token: 0x060065F4 RID: 26100 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CreateUI()
		{
		}

		// Token: 0x060065F5 RID: 26101 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickOk()
		{
		}

		// Token: 0x060065F6 RID: 26102 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnClosed()
		{
		}

		// Token: 0x060065F7 RID: 26103 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ShowUI()
		{
		}

		// Token: 0x060065F8 RID: 26104 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void HideUI()
		{
		}

		// Token: 0x060065F9 RID: 26105 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Abort()
		{
		}

		// Token: 0x060065FA RID: 26106 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnCancel()
		{
		}

		// Token: 0x060065FB RID: 26107 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open(string message, string buttonText, Action<bool> resultCallback, Action openCallback)
		{
		}

		// Token: 0x060065FC RID: 26108 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open(string message, Action<bool> resultCallback, Action openCallback)
		{
		}

		// Token: 0x0400A034 RID: 41012
		private Action<bool> resultCallback;

		// Token: 0x0400A035 RID: 41013
		private ExtendedTextMeshProUGUI textMessage;

		// Token: 0x0400A036 RID: 41014
		private ExtendedTextMeshProUGUI textButton;

		// Token: 0x0400A037 RID: 41015
		private ContentSizeFitter dialogFitter;

		// Token: 0x0400A038 RID: 41016
		private const string prefabPath = "Prefabs/Duel/DuelOkDialog";
	}
}
