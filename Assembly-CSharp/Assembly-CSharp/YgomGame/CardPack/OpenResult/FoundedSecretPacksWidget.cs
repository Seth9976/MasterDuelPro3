using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.Shop;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.CardPack.OpenResult
{
	// Token: 0x020010B8 RID: 4280
	public class FoundedSecretPacksWidget : ElementWidgetBase
	{
		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06007F27 RID: 32551 RVA: 0x0000216A File Offset: 0x0000036A
		public InfinityScrollView scrollView
		{
			get
			{
				return null;
			}
		}

		// Token: 0x140000BD RID: 189
		// (add) Token: 0x06007F28 RID: 32552 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06007F29 RID: 32553 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<ProductContext> onClickPackEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06007F2A RID: 32554 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public FoundedSecretPacksWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06007F2B RID: 32555 RVA: 0x0000216D File Offset: 0x0000036D
		public void ActivateScroll(Action onConplete)
		{
		}

		// Token: 0x06007F2C RID: 32556 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCreatedEntity(GameObject gob)
		{
		}

		// Token: 0x06007F2D RID: 32557 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnUpdateEntity(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06007F2E RID: 32558 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenClickPackWidget(ProductWidget packWidget)
		{
		}

		// Token: 0x06007F2F RID: 32559 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectBottomEntity()
		{
		}

		// Token: 0x0400B7BD RID: 47037
		private readonly string k_ELabelScrollView;

		// Token: 0x0400B7BE RID: 47038
		private readonly InfinityScrollView m_ScrollView;

		// Token: 0x0400B7BF RID: 47039
		private readonly Dictionary<GameObject, ProductWidget> m_EntityWidgetMap;

		// Token: 0x0400B7C0 RID: 47040
		public readonly List<ProductContext> productContexts;

		// Token: 0x0400B7C1 RID: 47041
		public List<object> extraPackGroupsWork;
	}
}
