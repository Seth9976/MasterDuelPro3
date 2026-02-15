using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B59 RID: 2905
	public static class ResourceBindingManager
	{
		// Token: 0x06005413 RID: 21523 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize()
		{
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingItemThumb BindingItemThumb(GameObject target, bool isPeriod, int itemCategory, int itemId, bool isLarge = false, BindingItemThumb.DxBadgeMode dxBadgeMode = YgomGame.Menu.Common.BindingItemThumb.DxBadgeMode.None)
		{
			return null;
		}

		// Token: 0x06005415 RID: 21525 RVA: 0x0000216A File Offset: 0x0000036A
		public static Component BindingItemThumbContent(GameObject target, bool isPeriod, int itemCategory, int itemId)
		{
			return null;
		}

		// Token: 0x06005416 RID: 21526 RVA: 0x0000216A File Offset: 0x0000036A
		public static Component BindingItemThumbLargeContent(GameObject target, bool isPeriod, int itemCategory, int itemId)
		{
			return null;
		}

		// Token: 0x06005417 RID: 21527 RVA: 0x0000216A File Offset: 0x0000036A
		private static Component DummyBindingThumb(GameObject target, int itemId)
		{
			return null;
		}

		// Token: 0x04009176 RID: 37238
		public static readonly CardResourceBinder cardBinder;

		// Token: 0x04009177 RID: 37239
		public static readonly RarityIconBinder rarityIconBinder;

		// Token: 0x04009178 RID: 37240
		public static readonly CraftIconBinder craftIconBinder;

		// Token: 0x04009179 RID: 37241
		public static readonly OutGameBGResourceBinder outGameBGBinder;

		// Token: 0x0400917A RID: 37242
		public static readonly ShopResourceBinder shopResourceBinder;

		// Token: 0x0400917B RID: 37243
		public static readonly CardPackResourceBinder cardPackBinder;

		// Token: 0x0400917C RID: 37244
		public static readonly SoloResourceBinder soloResourceBinder;

		// Token: 0x0400917D RID: 37245
		public static readonly ConsumeItemBinder consumeBinder;

		// Token: 0x0400917E RID: 37246
		public static readonly DeckResourceBinder deckBinder;

		// Token: 0x0400917F RID: 37247
		public static readonly RegulationIconBinder regulationIconBinder;

		// Token: 0x04009180 RID: 37248
		public static readonly PlayerIconResourceBinder playerIconBinder;

		// Token: 0x04009181 RID: 37249
		public static readonly AvatarResourceBinder avatarBinder;

		// Token: 0x04009182 RID: 37250
		public static readonly ProfileResourceBinder profileBinder;

		// Token: 0x04009183 RID: 37251
		public static readonly WallPaperResourceBinder wallPaperBinder;

		// Token: 0x04009184 RID: 37252
		public static readonly FieldResourceBinder fieldBinder;

		// Token: 0x04009185 RID: 37253
		public static readonly EventLogoResourceBinder eventLogoBinder;

		// Token: 0x04009186 RID: 37254
		public static readonly RegulationLogoResourceBinder regulationLogoBinder;

		// Token: 0x04009187 RID: 37255
		internal const string deluxebadgePath = "Prefabs/Profile/DeluxeBadge/DeluxeBadge";

		// Token: 0x04009188 RID: 37256
		internal const string deluxebadgePath2 = "Prefabs/Profile/DeluxeBadge/DeluxeBadge2";

		// Token: 0x04009189 RID: 37257
		internal const string deluxebadgePath2_L = "Prefabs/Profile/DeluxeBadge/DeluxeBadge2_L";
	}
}
