using System;
using TMPro;
using YgomGame.Shop;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.CardPack.OpenResult
{
	// Token: 0x020010BB RID: 4283
	public class SecretPackWidget : ElementWidgetBehaviourBase<SecretPackWidget>
	{
		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06007F46 RID: 32582 RVA: 0x000029CC File Offset: 0x00000BCC
		public int shopId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06007F47 RID: 32583 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton selectionButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06007F48 RID: 32584 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text nameText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06007F49 RID: 32585 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingShopProductThumb bindingShopProductThumb
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007F4A RID: 32586 RVA: 0x0000216A File Offset: 0x0000036A
		public static SecretPackWidget Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06007F4B RID: 32587 RVA: 0x0000216D File Offset: 0x0000036D
		public void Binding(int shopId, string name, BindingShopProductThumb.Context thumbContext)
		{
		}

		// Token: 0x06007F4C RID: 32588 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClick()
		{
		}

		// Token: 0x0400B7DA RID: 47066
		private readonly string k_ELabelButton;

		// Token: 0x0400B7DB RID: 47067
		private readonly string k_ELabelNameText;

		// Token: 0x0400B7DC RID: 47068
		private readonly string k_ELabelThumbHolder;

		// Token: 0x0400B7DD RID: 47069
		private int m_ShopId;

		// Token: 0x0400B7DE RID: 47070
		private SelectionButton m_SelectionButton;

		// Token: 0x0400B7DF RID: 47071
		private TMP_Text m_NameText;

		// Token: 0x0400B7E0 RID: 47072
		private BindingShopProductThumb m_BindingShopProductThumb;

		// Token: 0x0400B7E1 RID: 47073
		public Action<SecretPackWidget> onClickCallback;
	}
}
