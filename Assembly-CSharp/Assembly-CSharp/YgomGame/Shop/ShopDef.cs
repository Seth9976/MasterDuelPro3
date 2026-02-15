using System;

namespace YgomGame.Shop
{
	// Token: 0x02000952 RID: 2386
	public static class ShopDef
	{
		// Token: 0x0400843D RID: 33853
		internal const string k_SettingKey_LimitAlertSec = "limitAlertSec";

		// Token: 0x0400843E RID: 33854
		internal const string k_SettingKey_HighlightStyleType = "highlightStyleType";

		// Token: 0x0400843F RID: 33855
		internal const string k_SettingKey_ProductWidgetLabel = "productWidget";

		// Token: 0x04008440 RID: 33856
		internal const string k_SettingKey_HeadLabelText = "headLabelText";

		// Token: 0x04008441 RID: 33857
		internal const string k_SettingKey_SkipSoldoutSort = "skipSoldoutSort";

		// Token: 0x04008442 RID: 33858
		internal const string k_SettingKey_IsShortPayAmountSort = "isShortPayAmountSort";

		// Token: 0x04008443 RID: 33859
		internal const string k_SettingKey_IgnoreTurnoffBadge = "ignoreTurnoffBadge";

		// Token: 0x04008444 RID: 33860
		internal const string k_SettingKey_BgId = "bgId";

		// Token: 0x04008445 RID: 33861
		internal const string k_SettingKey_ListDesc = "listDesc";

		// Token: 0x04008446 RID: 33862
		internal const string k_SettingKey_InformButtonLabel = "informButton";

		// Token: 0x04008447 RID: 33863
		internal const string k_SettingKey_HighlightType = "HighlightType";

		// Token: 0x04008448 RID: 33864
		internal const string k_SettingKey_ProductSubLabel = "productSubLabel";

		// Token: 0x04008449 RID: 33865
		internal const string k_SettingKey_ViewerLoopType = "viewerLoopType";

		// Token: 0x0400844A RID: 33866
		internal const string k_SettingKey_HideSummonPlay = "hideSummonPlay";

		// Token: 0x02000953 RID: 2387
		public enum ProductType
		{
			// Token: 0x0400844C RID: 33868
			Pack = 1,
			// Token: 0x0400844D RID: 33869
			Structure,
			// Token: 0x0400844E RID: 33870
			Accessories,
			// Token: 0x0400844F RID: 33871
			Duelpass,
			// Token: 0x04008450 RID: 33872
			Card,
			// Token: 0x04008451 RID: 33873
			Prize
		}

		// Token: 0x02000954 RID: 2388
		public enum ShowcaseCategory
		{
			// Token: 0x04008453 RID: 33875
			Pickup = 5,
			// Token: 0x04008454 RID: 33876
			Pack = 1,
			// Token: 0x04008455 RID: 33877
			Structure,
			// Token: 0x04008456 RID: 33878
			Accessory,
			// Token: 0x04008457 RID: 33879
			Special
		}

		// Token: 0x02000955 RID: 2389
		public enum PackSubCategory
		{
			// Token: 0x04008459 RID: 33881
			Normal = 1,
			// Token: 0x0400845A RID: 33882
			Secret,
			// Token: 0x0400845B RID: 33883
			Bonus
		}

		// Token: 0x02000956 RID: 2390
		public enum SpecialSubCategory
		{
			// Token: 0x0400845D RID: 33885
			DuelPass = 1,
			// Token: 0x0400845E RID: 33886
			Set,
			// Token: 0x0400845F RID: 33887
			DeckLimit
		}

		// Token: 0x02000957 RID: 2391
		public enum HighlightType
		{
			// Token: 0x04008461 RID: 33889
			CardThumb = 1,
			// Token: 0x04008462 RID: 33890
			WideThumb = 3
		}

		// Token: 0x02000958 RID: 2392
		public enum ListButtonType
		{
			// Token: 0x04008464 RID: 33892
			Default,
			// Token: 0x04008465 RID: 33893
			Highlight
		}

		// Token: 0x02000959 RID: 2393
		public enum ViewerLoopType
		{
			// Token: 0x04008467 RID: 33895
			Default,
			// Token: 0x04008468 RID: 33896
			None
		}
	}
}
