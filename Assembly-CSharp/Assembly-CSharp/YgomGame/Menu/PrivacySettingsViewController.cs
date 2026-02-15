using System;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000ABE RID: 2750
	public class PrivacySettingsViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x06005009 RID: 20489 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeOptOut()
		{
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickOKButton()
		{
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickButtonCancel()
		{
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetActiveOKButton(bool flag)
		{
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x0000216D File Offset: 0x0000036D
		private void setButton()
		{
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x0000216D File Offset: 0x0000036D
		private void setUnChangeAble()
		{
		}

		// Token: 0x04008DF8 RID: 36344
		private readonly string LABEL_BUTTONOK;

		// Token: 0x04008DF9 RID: 36345
		private readonly string LABEL_BUTTONCANCEL;

		// Token: 0x04008DFA RID: 36346
		private readonly string LABEL_BUTTON_1;

		// Token: 0x04008DFB RID: 36347
		private readonly string LABEL_BUTTON_2;

		// Token: 0x04008DFC RID: 36348
		[SerializeField]
		public int dataCount;

		// Token: 0x04008DFD RID: 36349
		private bool optOutBool;

		// Token: 0x04008DFE RID: 36350
		private SelectionButton m_CancelButton;

		// Token: 0x04008DFF RID: 36351
		private SelectionButton m_OKButton;

		// Token: 0x04008E00 RID: 36352
		private ElementObject subTitleText1;

		// Token: 0x04008E01 RID: 36353
		private ElementObject subTitleText2;

		// Token: 0x04008E02 RID: 36354
		private ElementObjectManager button1;

		// Token: 0x04008E03 RID: 36355
		private ElementObjectManager button2;

		// Token: 0x04008E04 RID: 36356
		private SelectionButton leftButton;

		// Token: 0x04008E05 RID: 36357
		private SelectionButton rightButton;

		// Token: 0x04008E06 RID: 36358
		private bool leftselect;
	}
}
