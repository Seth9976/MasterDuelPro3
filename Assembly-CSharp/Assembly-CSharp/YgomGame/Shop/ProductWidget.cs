using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.CardPack;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x02000943 RID: 2371
	public class ProductWidget : ElementWidgetBase
	{
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06004594 RID: 17812 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text newLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004595 RID: 17813 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ProductWidget(string widgetLabel, ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004596 RID: 17814 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLimitAlertStyle(bool isAlertOn)
		{
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayStyleTween(string label)
		{
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnClick()
		{
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnSelected()
		{
		}

		// Token: 0x04008387 RID: 33671
		private readonly string k_ELabelHeadIconText;

		// Token: 0x04008388 RID: 33672
		internal const string k_TLabelStyleNormal = "Style_Normal";

		// Token: 0x04008389 RID: 33673
		internal const string k_TLabelStyleHighlight = "Style_Highlight";

		// Token: 0x0400838A RID: 33674
		private const string k_TLabelLimitAlert_OFF = "LimitAlert_OFF";

		// Token: 0x0400838B RID: 33675
		private const string k_TLabelLimitAlert_ON = "LimitAlert_ON";

		// Token: 0x0400838C RID: 33676
		public int index;

		// Token: 0x0400838D RID: 33677
		public int shopId;

		// Token: 0x0400838E RID: 33678
		public readonly string widgetLabel;

		// Token: 0x0400838F RID: 33679
		public readonly SelectionButton button;

		// Token: 0x04008390 RID: 33680
		public readonly GameObject badge;

		// Token: 0x04008391 RID: 33681
		public readonly GameObject newGroup;

		// Token: 0x04008392 RID: 33682
		public readonly GameObject baseLower;

		// Token: 0x04008393 RID: 33683
		public readonly TMP_Text pickupNameText;

		// Token: 0x04008394 RID: 33684
		public readonly TMP_Text nameText;

		// Token: 0x04008395 RID: 33685
		public readonly GameObject priceGroup;

		// Token: 0x04008396 RID: 33686
		public readonly GameObject priceBGDefault;

		// Token: 0x04008397 RID: 33687
		public readonly GameObject priceBGHighlight;

		// Token: 0x04008398 RID: 33688
		public readonly Image priceButtonIcon;

		// Token: 0x04008399 RID: 33689
		public readonly TMP_Text priceText;

		// Token: 0x0400839A RID: 33690
		public readonly TMP_Text priceLabelText;

		// Token: 0x0400839B RID: 33691
		public readonly TMP_Text priceFreeText;

		// Token: 0x0400839C RID: 33692
		public readonly TMP_Text priceFreeLabelText;

		// Token: 0x0400839D RID: 33693
		public readonly GameObject priceIcon;

		// Token: 0x0400839E RID: 33694
		public readonly GameObject priceTimeLimitIcon;

		// Token: 0x0400839F RID: 33695
		public readonly GameObject ownedGroup;

		// Token: 0x040083A0 RID: 33696
		public readonly RectTransform thumbHolder;

		// Token: 0x040083A1 RID: 33697
		public readonly ParticleAlphaGroup thumbHolderParticleAlphaGroup;

		// Token: 0x040083A2 RID: 33698
		public readonly GameObject soldOutCover;

		// Token: 0x040083A3 RID: 33699
		public readonly GameObject limitGroup;

		// Token: 0x040083A4 RID: 33700
		public readonly TMP_Text limitRemainText;

		// Token: 0x040083A5 RID: 33701
		public readonly TMP_Text limitDateText;

		// Token: 0x040083A6 RID: 33702
		public readonly GameObject numGroup;

		// Token: 0x040083A7 RID: 33703
		public readonly TMP_Text numText;

		// Token: 0x040083A8 RID: 33704
		public readonly CardPackChartWidget pickupChartWidget;

		// Token: 0x040083A9 RID: 33705
		public readonly TMP_Text descText;

		// Token: 0x040083AA RID: 33706
		public readonly GameObject packPickupMessageGroup;

		// Token: 0x040083AB RID: 33707
		public readonly TMP_Text packPickupMessage;

		// Token: 0x040083AC RID: 33708
		private List<Tween> tweens;

		// Token: 0x040083AD RID: 33709
		public Action<ProductWidget> onClickCallback;

		// Token: 0x040083AE RID: 33710
		public Action<ProductWidget> onSelectedCallback;
	}
}
