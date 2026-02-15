using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x02000935 RID: 2357
	public class InformButtonGroupWidget : ElementWidgetBase
	{
		// Token: 0x060044A6 RID: 17574 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetTemplateIdxByStyle(ShopInformButtonData.ButtonStyle buttonStyle)
		{
			return 0;
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetButtonLabel(ShopInformButtonData buttonData)
		{
			return null;
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x0000216A File Offset: 0x0000036A
		private Action GetBehaviourCallback(ShopInformButtonData buttonData)
		{
			return null;
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public InformButtonGroupWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060044AA RID: 17578 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedEntity(GameObject entity, int templateIdx)
		{
		}

		// Token: 0x060044AB RID: 17579 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnActivateEntity(GameObject entity)
		{
		}

		// Token: 0x060044AC RID: 17580 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeactivateEntity(GameObject entity)
		{
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntity(GameObject entity, int dataIdx)
		{
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickEntity(GameObject entity)
		{
		}

		// Token: 0x060044AF RID: 17583 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetContext(ShopSettings shopSettings, ProductContext productContext)
		{
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectHeadButton()
		{
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectBottomButton()
		{
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenItemPreview(ProductContext productContext)
		{
		}

		// Token: 0x04008319 RID: 33561
		private readonly int k_TemplateIdx_Normal;

		// Token: 0x0400831A RID: 33562
		private readonly int k_TemplateIdx_Small;

		// Token: 0x0400831B RID: 33563
		private readonly int k_TemplateIdx_SmallMB;

		// Token: 0x0400831C RID: 33564
		private readonly int k_TemplateIdx_Highlight;

		// Token: 0x0400831D RID: 33565
		private readonly string k_ELabelText;

		// Token: 0x0400831E RID: 33566
		private readonly ElementEntityFactory m_Factory;

		// Token: 0x0400831F RID: 33567
		private readonly List<ValueTuple<string, Action>> m_DataList;

		// Token: 0x04008320 RID: 33568
		private readonly List<int> m_TemplateIdList;

		// Token: 0x04008321 RID: 33569
		private ShopSettings m_ShopSettings;

		// Token: 0x04008322 RID: 33570
		private ProductContext m_ProductContext;

		// Token: 0x04008323 RID: 33571
		public bool blockPurchase;

		// Token: 0x04008324 RID: 33572
		public bool setDefaultButton;
	}
}
