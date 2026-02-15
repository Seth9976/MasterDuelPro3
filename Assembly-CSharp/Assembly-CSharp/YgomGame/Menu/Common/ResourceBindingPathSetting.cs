using System;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B5A RID: 2906
	public class ResourceBindingPathSetting : ScriptableObject
	{
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06005418 RID: 21528 RVA: 0x0000216A File Offset: 0x0000036A
		public ConsumeItemBinder.ConsumeItemPathData consumeItemPathData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06005419 RID: 21529 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckResourceBinder.DeckPathData deckPathData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x0600541A RID: 21530 RVA: 0x0000216A File Offset: 0x0000036A
		public PlayerIconResourceBinder.PlayerIconPathData playerIconPathData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x0600541B RID: 21531 RVA: 0x0000216A File Offset: 0x0000036A
		public FieldResourceBinder.FieldPathData fieldPathData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x0600541C RID: 21532 RVA: 0x0000216A File Offset: 0x0000036A
		public EventLogoResourceBinder.EventLogoPathData eventLogoPathData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x0600541D RID: 21533 RVA: 0x0000216A File Offset: 0x0000036A
		public RegulationLogoResourceBinder.RegulationLogoPathData regulationLogoPathData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x0600541E RID: 21534 RVA: 0x0000216A File Offset: 0x0000036A
		public AvatarResourceBinder.AvatarResourcePathData avatarResourcePathData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x0600541F RID: 21535 RVA: 0x0000216A File Offset: 0x0000036A
		public WallPaperResourceBinder.WallPaperResourcePathData wallPaperResourcePathData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06005420 RID: 21536 RVA: 0x0000216A File Offset: 0x0000036A
		public ProfileResourceBinder.ProfileResource profileResourcePathData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400918A RID: 37258
		internal const string k_SettingPath = "Definition/UISystem/ResourceBindingPathSetting";

		// Token: 0x0400918B RID: 37259
		internal const string k_RarityIcon_RaritySpriteContainerPath = "Images/Card/RaritySpriteContainer";

		// Token: 0x0400918C RID: 37260
		internal const string k_CraftIcon_CP0Path = "Images/Menu/GUI_alpha_Icon_CP0";

		// Token: 0x0400918D RID: 37261
		internal const string k_CraftIcon_CP1Path = "Images/Menu/GUI_alpha_Icon_CP1";

		// Token: 0x0400918E RID: 37262
		internal const string k_CraftIcon_CP2Path = "Images/Menu/GUI_alpha_Icon_CP2";

		// Token: 0x0400918F RID: 37263
		internal const string k_CraftIcon_CP3Path = "Images/Menu/GUI_alpha_Icon_CP3";

		// Token: 0x04009190 RID: 37264
		internal const string k_RegulationSpriteContainerPath = "Images/Card/RegulationSpriteContainer";

		// Token: 0x04009191 RID: 37265
		internal const string k_Shop_CardThumbSettingPath = "Definition/Shop/CardThumbSettings";

		// Token: 0x04009192 RID: 37266
		internal const string k_Shop_HighlightThumbImagePath = "Images/Shop/HighlightThumbs/{0}";

		// Token: 0x04009193 RID: 37267
		internal const string k_Shop_HighlightThumbPrefPath = "Prefabs/Shop/HighlightThumbs/{0}";

		// Token: 0x04009194 RID: 37268
		internal const string k_OutGameBG_FrontBGPath = "Prefabs/OutGameBg/Front/Front{0:D4}";

		// Token: 0x04009195 RID: 37269
		internal const string k_OutGameBG_BackBGPath = "Prefabs/OutGameBg/Back/Back{0:D4}";

		// Token: 0x04009196 RID: 37270
		internal const string k_Solo_CardThumbSettingPath = "Definition/Solo/SoloCardThumbSettings";

		// Token: 0x04009197 RID: 37271
		internal const string k_CardPack_PackTicketPath = "Images/PackTicket/<_RESOURCE_TYPE_>/PackTicket";

		// Token: 0x04009198 RID: 37272
		internal const string k_CardPack_PackTexPath = "Images/CardPack/<_RESOURCE_TYPE_>/<_CARD_ILLUST_>/{0}";

		// Token: 0x04009199 RID: 37273
		[SerializeField]
		private ConsumeItemBinder.ConsumeItemPathData m_ConsumeItemPathData;

		// Token: 0x0400919A RID: 37274
		[SerializeField]
		private DeckResourceBinder.DeckPathData m_DeckPathData;

		// Token: 0x0400919B RID: 37275
		[SerializeField]
		private PlayerIconResourceBinder.PlayerIconPathData m_PlayerIconPathData;

		// Token: 0x0400919C RID: 37276
		[SerializeField]
		private FieldResourceBinder.FieldPathData m_FieldPathData;

		// Token: 0x0400919D RID: 37277
		[SerializeField]
		private EventLogoResourceBinder.EventLogoPathData m_EventLogoPathData;

		// Token: 0x0400919E RID: 37278
		[SerializeField]
		private RegulationLogoResourceBinder.RegulationLogoPathData m_RegulationLogoPathData;

		// Token: 0x0400919F RID: 37279
		[SerializeField]
		private AvatarResourceBinder.AvatarResourcePathData m_AvatarResourcePathData;

		// Token: 0x040091A0 RID: 37280
		[SerializeField]
		private WallPaperResourceBinder.WallPaperResourcePathData m_WallPaperResourcePathData;

		// Token: 0x040091A1 RID: 37281
		[SerializeField]
		private ProfileResourceBinder.ProfileResource m_ProfileResource;

		// Token: 0x02000B5B RID: 2907
		[Serializable]
		public class ItemPathData
		{
			// Token: 0x06005422 RID: 21538 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetPath(bool isLarge)
			{
				return null;
			}

			// Token: 0x040091A2 RID: 37282
			public bool divideResourceType;

			// Token: 0x040091A3 RID: 37283
			public string SD;

			// Token: 0x040091A4 RID: 37284
			public string SD_L;

			// Token: 0x040091A5 RID: 37285
			[SerializeField]
			public string HighEndHD;

			// Token: 0x040091A6 RID: 37286
			[SerializeField]
			public string HighEndHD_L;
		}

		// Token: 0x02000B5C RID: 2908
		public class EnabledIfAttribute : PropertyAttribute
		{
			// Token: 0x06005424 RID: 21540 RVA: 0x000F1A32 File Offset: 0x000EFC32
			public EnabledIfAttribute(string switcherFieldName, bool enable)
			{
			}

			// Token: 0x040091A7 RID: 37287
			public string switcherFieldName;

			// Token: 0x040091A8 RID: 37288
			public bool enable;
		}
	}
}
