using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.PropertyOverrider;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C6C RID: 3180
	public class ProductWidget : ElementWidgetBase
	{
		// Token: 0x06005AFF RID: 23295 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ProductWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06005B00 RID: 23296 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAll(IProductContext productCtx)
		{
		}

		// Token: 0x06005B01 RID: 23297 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetLimitAlertStyle(bool isAlertOn)
		{
		}

		// Token: 0x06005B02 RID: 23298 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnClick()
		{
		}

		// Token: 0x06005B03 RID: 23299 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnSelected()
		{
		}

		// Token: 0x04009617 RID: 38423
		private const string k_TLabelStyleNormal = "Style_Normal";

		// Token: 0x04009618 RID: 38424
		private const string k_TLabelStyleHighlight = "Style_Highlight";

		// Token: 0x04009619 RID: 38425
		private const string k_TLabelLimitAlert_OFF = "LimitAlert_OFF";

		// Token: 0x0400961A RID: 38426
		private const string k_TLabelLimitAlert_ON = "LimitAlert_ON";

		// Token: 0x0400961B RID: 38427
		private const string k_TLabelProductRandom = "templateDuelLive";

		// Token: 0x0400961C RID: 38428
		private const string k_TLabelProductVS = "templateVS";

		// Token: 0x0400961D RID: 38429
		private const string k_TLabelProductEvent = "templateEvent";

		// Token: 0x0400961E RID: 38430
		private const string k_TLabelProductOfficialAccount = "templateSpecialAccount";

		// Token: 0x0400961F RID: 38431
		private const string k_TLabelProductCommingSoon = "templateComingSoon";

		// Token: 0x04009620 RID: 38432
		private const string k_TLabelCard_0_0 = "Card_0_0";

		// Token: 0x04009621 RID: 38433
		private const string k_TLabelCard_0_1 = "Card_0_1";

		// Token: 0x04009622 RID: 38434
		private const string k_TLabelCard_0_2 = "Card_0_2";

		// Token: 0x04009623 RID: 38435
		private const string k_TLabelCard_1_0 = "Card_1_0";

		// Token: 0x04009624 RID: 38436
		private const string k_TLabelCard_1_1 = "Card_1_1";

		// Token: 0x04009625 RID: 38437
		private const string k_TLabelCard_1_2 = "Card_1_2";

		// Token: 0x04009626 RID: 38438
		public int index;

		// Token: 0x04009627 RID: 38439
		public int menuId;

		// Token: 0x04009628 RID: 38440
		public int replayIdx;

		// Token: 0x04009629 RID: 38441
		public long duelLiveId;

		// Token: 0x0400962A RID: 38442
		public int widgetType;

		// Token: 0x0400962B RID: 38443
		public SelectionButton button;

		// Token: 0x0400962C RID: 38444
		private List<ElementObjectManager> widgets;

		// Token: 0x0400962D RID: 38445
		public List<SelectionButton> widgetButtons;

		// Token: 0x0400962E RID: 38446
		public readonly GameObject badge;

		// Token: 0x0400962F RID: 38447
		public readonly GameObject newGroup;

		// Token: 0x04009630 RID: 38448
		public readonly GameObject baseLower;

		// Token: 0x04009631 RID: 38449
		public readonly TMP_Text nameText;

		// Token: 0x04009632 RID: 38450
		public readonly GameObject priceGroup;

		// Token: 0x04009633 RID: 38451
		public readonly GameObject priceBGDefault;

		// Token: 0x04009634 RID: 38452
		public readonly GameObject priceBGHighlight;

		// Token: 0x04009635 RID: 38453
		public readonly Image priceButtonIcon;

		// Token: 0x04009636 RID: 38454
		public readonly TMP_Text priceText;

		// Token: 0x04009637 RID: 38455
		public readonly TMP_Text priceLabelText;

		// Token: 0x04009638 RID: 38456
		public readonly TMP_Text priceFreeText;

		// Token: 0x04009639 RID: 38457
		public readonly TMP_Text priceFreeLabelText;

		// Token: 0x0400963A RID: 38458
		public readonly GameObject priceIcon;

		// Token: 0x0400963B RID: 38459
		public readonly GameObject priceTimeLimitIcon;

		// Token: 0x0400963C RID: 38460
		public readonly GameObject ownedGroup;

		// Token: 0x0400963D RID: 38461
		public readonly RectTransform thumbHolder;

		// Token: 0x0400963E RID: 38462
		public readonly Image deckCaseImage;

		// Token: 0x0400963F RID: 38463
		public readonly GameObject soldOutCover;

		// Token: 0x04009640 RID: 38464
		public readonly GameObject limitGroup;

		// Token: 0x04009641 RID: 38465
		public readonly TMP_Text limitRemainText;

		// Token: 0x04009642 RID: 38466
		public readonly TMP_Text limitDateText;

		// Token: 0x04009643 RID: 38467
		public readonly GameObject numGroup;

		// Token: 0x04009644 RID: 38468
		public readonly TMP_Text numText;

		// Token: 0x04009645 RID: 38469
		public readonly TMP_Text descText;

		// Token: 0x04009646 RID: 38470
		public readonly GameObject packPickupMessageGroup;

		// Token: 0x04009647 RID: 38471
		public readonly TMP_Text packPickupMessage;

		// Token: 0x04009648 RID: 38472
		public readonly GameObject thumbRoot;

		// Token: 0x04009649 RID: 38473
		public readonly PlatformOverriderGroup platFormOverriderGroup;

		// Token: 0x0400964A RID: 38474
		public Action<ProductWidget> onClickCallback;

		// Token: 0x0400964B RID: 38475
		public Action<ProductWidget> onSelectedCallback;
	}
}
