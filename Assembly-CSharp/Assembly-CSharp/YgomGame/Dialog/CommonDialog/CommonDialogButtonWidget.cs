using System;
using TMPro;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F62 RID: 3938
	public class CommonDialogButtonWidget : ContentWidgetBase<CommonDialogButtonWidget, EntryButtonData>
	{
		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x060073FB RID: 29691 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton button
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (set) Token: 0x060073FC RID: 29692 RVA: 0x0000216D File Offset: 0x0000036D
		private Action YgomGame_002EDialog_002ECommonDialog_002EIConentWidgetSendableClose_002EonSendCloseCallback
		{
			set
			{
			}
		}

		// Token: 0x060073FD RID: 29693 RVA: 0x0000216A File Offset: 0x0000036A
		public static CommonDialogButtonWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x060073FE RID: 29694 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x060073FF RID: 29695 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InnerBinding(EntryButtonData entryData)
		{
		}

		// Token: 0x06007400 RID: 29696 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAsDefault(bool isDefault = true)
		{
		}

		// Token: 0x06007401 RID: 29697 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClick()
		{
		}

		// Token: 0x0400ACF6 RID: 44278
		private readonly string k_ELabelLabel;

		// Token: 0x0400ACF7 RID: 44279
		private SelectionButton m_Button;

		// Token: 0x0400ACF8 RID: 44280
		private TMP_Text m_Text;

		// Token: 0x0400ACF9 RID: 44281
		private Action m_OnClickCallback;

		// Token: 0x0400ACFA RID: 44282
		private Action<CommonDialogButtonWidget> m_OnClickWidgetCallback;

		// Token: 0x0400ACFB RID: 44283
		private string m_OnClickUrlScheme;

		// Token: 0x0400ACFC RID: 44284
		private bool m_CloseOnClick;

		// Token: 0x0400ACFD RID: 44285
		public Action onSendCloseCallback;
	}
}
