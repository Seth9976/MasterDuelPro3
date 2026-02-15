using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomGame.Shop;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.CardPack.OpenResult
{
	// Token: 0x020010BC RID: 4284
	public class SecretPulldownWidget : ElementWidgetBase
	{
		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06007F4E RID: 32590 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isExpanded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007F4F RID: 32591 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public SecretPulldownWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06007F50 RID: 32592 RVA: 0x0000216D File Offset: 0x0000036D
		public void Binding(IReadOnlyList<int> shopIds, bool isExpand = false)
		{
		}

		// Token: 0x06007F51 RID: 32593 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenExpand(bool isTween = true, bool enableRetry = false)
		{
		}

		// Token: 0x06007F52 RID: 32594 RVA: 0x0000216D File Offset: 0x0000036D
		public void CloseExpand(bool isTween = true, bool enableRetry = false)
		{
		}

		// Token: 0x06007F53 RID: 32595 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnTweenOpenStart()
		{
		}

		// Token: 0x06007F54 RID: 32596 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnTweenCloseEnd()
		{
		}

		// Token: 0x0400B7E2 RID: 47074
		private readonly string k_ELabelLabelRoot;

		// Token: 0x0400B7E3 RID: 47075
		private readonly string k_ELabelLabelText;

		// Token: 0x0400B7E4 RID: 47076
		private readonly string k_ELabelExpands;

		// Token: 0x0400B7E5 RID: 47077
		private readonly string k_TweenOpen;

		// Token: 0x0400B7E6 RID: 47078
		private readonly string k_TweenClose;

		// Token: 0x0400B7E7 RID: 47079
		public readonly SelectionItem selectionItem;

		// Token: 0x0400B7E8 RID: 47080
		private readonly GameObject m_LabelRoot;

		// Token: 0x0400B7E9 RID: 47081
		private readonly TMP_Text m_LabelText;

		// Token: 0x0400B7EA RID: 47082
		private readonly SecretPulldownWidget.ExpandsWidget m_ExpandsWidget;

		// Token: 0x0400B7EB RID: 47083
		private readonly ProductContext m_PackProductContent;

		// Token: 0x0400B7EC RID: 47084
		private List<int> m_ShopIds;

		// Token: 0x0400B7ED RID: 47085
		private bool m_IsExpanded;

		// Token: 0x0400B7EE RID: 47086
		private bool m_ExpandDirty;

		// Token: 0x020010BD RID: 4285
		public class ExpandsWidget : ElementWidgetBase
		{
			// Token: 0x06007F55 RID: 32597 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public ExpandsWidget(ElementObjectManager eom)
				: base(null)
			{
			}

			// Token: 0x06007F56 RID: 32598 RVA: 0x0000216D File Offset: 0x0000036D
			public void Binding(IReadOnlyList<int> shopIds)
			{
			}

			// Token: 0x0400B7EF RID: 47087
			private readonly string k_ELabelLineHead;

			// Token: 0x0400B7F0 RID: 47088
			private readonly string k_ELabelLineTemplate;

			// Token: 0x0400B7F1 RID: 47089
			private readonly string k_ELineLabelText;

			// Token: 0x0400B7F2 RID: 47090
			private readonly ElementObjectManager m_LineTemplate;

			// Token: 0x0400B7F3 RID: 47091
			private List<ElementObjectManager> m_LineEoms;
		}
	}
}
