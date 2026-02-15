using System;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BA3 RID: 2979
	public static class MDMarkupDef
	{
		// Token: 0x02000BA4 RID: 2980
		public enum ContainerType
		{
			// Token: 0x04009261 RID: 37473
			Board = 1,
			// Token: 0x04009262 RID: 37474
			Pager,
			// Token: 0x04009263 RID: 37475
			Tabs,
			// Token: 0x04009264 RID: 37476
			Embed,
			// Token: 0x04009265 RID: 37477
			BoardPager
		}

		// Token: 0x02000BA5 RID: 2981
		public enum CloseButtonType
		{
			// Token: 0x04009267 RID: 37479
			None,
			// Token: 0x04009268 RID: 37480
			Always,
			// Token: 0x04009269 RID: 37481
			ReachLast
		}

		// Token: 0x02000BA6 RID: 2982
		public enum MarkupType
		{
			// Token: 0x0400926B RID: 37483
			None,
			// Token: 0x0400926C RID: 37484
			H1,
			// Token: 0x0400926D RID: 37485
			H2,
			// Token: 0x0400926E RID: 37486
			Text,
			// Token: 0x0400926F RID: 37487
			Image,
			// Token: 0x04009270 RID: 37488
			Table,
			// Token: 0x04009271 RID: 37489
			Separator,
			// Token: 0x04009272 RID: 37490
			Spacer,
			// Token: 0x04009273 RID: 37491
			HalfImageTextPage,
			// Token: 0x04009274 RID: 37492
			HalfImageMarkupPage,
			// Token: 0x04009275 RID: 37493
			HalfBannerMarkupPage,
			// Token: 0x04009276 RID: 37494
			FullImagePage,
			// Token: 0x04009277 RID: 37495
			FullTextPage,
			// Token: 0x04009278 RID: 37496
			EmbedContainerTab,
			// Token: 0x04009279 RID: 37497
			RawContainerTab,
			// Token: 0x0400927A RID: 37498
			CustomBoardPageHandler = 100
		}

		// Token: 0x02000BA7 RID: 2983
		public enum TableRowStyle
		{
			// Token: 0x0400927C RID: 37500
			Normal,
			// Token: 0x0400927D RID: 37501
			Header
		}

		// Token: 0x02000BA8 RID: 2984
		public enum TableCellValueType
		{
			// Token: 0x0400927F RID: 37503
			Text,
			// Token: 0x04009280 RID: 37504
			Image,
			// Token: 0x04009281 RID: 37505
			Card,
			// Token: 0x04009282 RID: 37506
			Item,
			// Token: 0x04009283 RID: 37507
			Banner,
			// Token: 0x04009284 RID: 37508
			Button
		}

		// Token: 0x02000BA9 RID: 2985
		public enum CardSize
		{
			// Token: 0x04009286 RID: 37510
			S,
			// Token: 0x04009287 RID: 37511
			M,
			// Token: 0x04009288 RID: 37512
			L
		}

		// Token: 0x02000BAA RID: 2986
		public enum ItemSize
		{
			// Token: 0x0400928A RID: 37514
			S,
			// Token: 0x0400928B RID: 37515
			M,
			// Token: 0x0400928C RID: 37516
			L
		}

		// Token: 0x02000BAB RID: 2987
		public enum SpacerSize
		{
			// Token: 0x0400928E RID: 37518
			S,
			// Token: 0x0400928F RID: 37519
			M,
			// Token: 0x04009290 RID: 37520
			L
		}

		// Token: 0x02000BAC RID: 2988
		public enum ButtonStyle
		{
			// Token: 0x04009292 RID: 37522
			S,
			// Token: 0x04009293 RID: 37523
			M,
			// Token: 0x04009294 RID: 37524
			L
		}
	}
}
