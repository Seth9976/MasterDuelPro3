using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Card;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.Utility;
using YgomSystem.YGomTMPro;

namespace YgomGame.Deck
{
	// Token: 0x02000FE8 RID: 4072
	public class FilterDialog : SelectDialogViewControllerBase<SearchFilter.Setting, SearchFilter.Setting, List<FilterDialog.FilterGroupType>, SearchFilter.Setting>, IBokeSupported
	{
		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06007ABE RID: 31422 RVA: 0x0000216A File Offset: 0x0000036A
		private static Content m_cci
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007ABF RID: 31423 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(SearchFilter.Setting setting, Action<SearchFilter.Setting> callback = null)
		{
		}

		// Token: 0x06007AC0 RID: 31424 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(SearchFilter.Setting setting, SearchFilter.Setting defaultSetting, List<FilterDialog.FilterGroupType> filterGroupTypes, Action<SearchFilter.Setting> callback = null)
		{
		}

		// Token: 0x06007AC1 RID: 31425 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007AC2 RID: 31426 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007AC3 RID: 31427 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeDefault()
		{
		}

		// Token: 0x06007AC4 RID: 31428 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeFooter()
		{
		}

		// Token: 0x06007AC5 RID: 31429 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InitializeFileterMenu()
		{
			return null;
		}

		// Token: 0x06007AC6 RID: 31430 RVA: 0x0000216D File Offset: 0x0000036D
		private void setFilterFrame(FilterToggle ft)
		{
		}

		// Token: 0x06007AC7 RID: 31431 RVA: 0x0000216D File Offset: 0x0000036D
		private void setFilterAttr(FilterToggle ft)
		{
		}

		// Token: 0x06007AC8 RID: 31432 RVA: 0x0000216D File Offset: 0x0000036D
		private void setFilterTribe(FilterToggle ft)
		{
		}

		// Token: 0x06007AC9 RID: 31433 RVA: 0x0000216D File Offset: 0x0000036D
		private void setFilterLevel(FilterToggle ft)
		{
		}

		// Token: 0x06007ACA RID: 31434 RVA: 0x0000216D File Offset: 0x0000036D
		private void setFilterSpell(FilterToggle ft)
		{
		}

		// Token: 0x06007ACB RID: 31435 RVA: 0x0000216D File Offset: 0x0000036D
		private void setFilterTrap(FilterToggle ft)
		{
		}

		// Token: 0x06007ACC RID: 31436 RVA: 0x0000216D File Offset: 0x0000036D
		private void setFilterRarity(FilterToggle ft)
		{
		}

		// Token: 0x06007ACD RID: 31437 RVA: 0x0000216D File Offset: 0x0000036D
		private void setFilterAbility(FilterToggle ft)
		{
		}

		// Token: 0x06007ACE RID: 31438 RVA: 0x0000216D File Offset: 0x0000036D
		private void setFilterStyle(FilterToggle ft)
		{
		}

		// Token: 0x06007ACF RID: 31439 RVA: 0x0000216D File Offset: 0x0000036D
		private void setFilterLimit(FilterToggle ft)
		{
		}

		// Token: 0x06007AD0 RID: 31440 RVA: 0x0000216D File Offset: 0x0000036D
		private void setFilterCutin(FilterToggle ft)
		{
		}

		// Token: 0x06007AD1 RID: 31441 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnReset()
		{
		}

		// Token: 0x06007AD2 RID: 31442 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectNextGroupTop(FilterDialog.FilterGroupType filterGroup)
		{
		}

		// Token: 0x06007AD3 RID: 31443 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectPrevGroupTop(FilterDialog.FilterGroupType filterGroup)
		{
		}

		// Token: 0x06007AD4 RID: 31444 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupScrollToSelectingItem(SelectionItem item, RectTransform itemRootRect)
		{
		}

		// Token: 0x06007AD5 RID: 31445 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitPadTransition(SelectionButton button, RectTransform buttonArea, FilterDialog.FilterGroupType type)
		{
		}

		// Token: 0x06007AD6 RID: 31446 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06007AD7 RID: 31447 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x0400B172 RID: 45426
		[SerializeField]
		private KeyConfigContainer keyConfig;

		// Token: 0x0400B173 RID: 45427
		private const string PREFAB_PATH_FILTERDIALOG = "DeckEdit/FilterDialog";

		// Token: 0x0400B174 RID: 45428
		private const string k_ELabelFilterMenuArea = "FilterMenuArea";

		// Token: 0x0400B175 RID: 45429
		private const string k_ELabelFilterScroll = "FilterScroll";

		// Token: 0x0400B176 RID: 45430
		private const string k_ELabelFooterArea = "FooterArea";

		// Token: 0x0400B177 RID: 45431
		private const string k_ELabelTitleText = "TitleText";

		// Token: 0x0400B178 RID: 45432
		private ElementObjectManager m_FilterMenuEom;

		// Token: 0x0400B179 RID: 45433
		private ElementObjectManager m_FooterEom;

		// Token: 0x0400B17A RID: 45434
		private ExtendedTextMeshProUGUI m_TitleText;

		// Token: 0x0400B17B RID: 45435
		private const string k_ALabelFilterToggleTemplate = "FilterToggleTemplate";

		// Token: 0x0400B17C RID: 45436
		private GameObject m_FilterToggleTemplate;

		// Token: 0x0400B17D RID: 45437
		private ValueTuple<SelectorManager.KeyType, SelectorManager.KeyType> keyInfoUpGroup;

		// Token: 0x0400B17E RID: 45438
		private ValueTuple<SelectorManager.KeyType, SelectorManager.KeyType> keyInfoDownGroup;

		// Token: 0x0400B17F RID: 45439
		private Coroutine m_InitializeCoroutine;

		// Token: 0x0400B180 RID: 45440
		private const string Label_Frame_Normal = "\ufffd";

		// Token: 0x0400B181 RID: 45441
		private const string Label_Frame_Effect = "\ufffd";

		// Token: 0x0400B182 RID: 45442
		private const string Label_Frame_Fusion = "\ufffd";

		// Token: 0x0400B183 RID: 45443
		private const string Label_Frame_Ritual = "\ufffd";

		// Token: 0x0400B184 RID: 45444
		private const string Label_Frame_Synchro = "シ\ufffd";

		// Token: 0x0400B185 RID: 45445
		private const string Label_Frame_Xyz = "エ\ufffd";

		// Token: 0x0400B186 RID: 45446
		private const string Label_Frame_Pendulum = "ペン";

		// Token: 0x0400B187 RID: 45447
		private const string Label_Frame_Link = "リ";

		// Token: 0x0400B188 RID: 45448
		private const string Label_Frame_Magic = "\ufffd";

		// Token: 0x0400B189 RID: 45449
		private const string Label_Frame_Trap = "\ufffd";

		// Token: 0x0400B18A RID: 45450
		private const string Label_Attr_Light = "光";

		// Token: 0x0400B18B RID: 45451
		private const string Label_Attr_Dark = "闇";

		// Token: 0x0400B18C RID: 45452
		private const string Label_Attr_Water = "水";

		// Token: 0x0400B18D RID: 45453
		private const string Label_Attr_Fire = "炎";

		// Token: 0x0400B18E RID: 45454
		private const string Label_Attr_Earth = "地";

		// Token: 0x0400B18F RID: 45455
		private const string Label_Attr_Wind = "風";

		// Token: 0x0400B190 RID: 45456
		private const string Label_Attr_Divine = "神";

		// Token: 0x0400B191 RID: 45457
		private const string Label_Tribe_SpellCaster = "魔\ufffd";

		// Token: 0x0400B192 RID: 45458
		private const string Label_Tribe_Dragon = "ド\ufffd";

		// Token: 0x0400B193 RID: 45459
		private const string Label_Tribe_Zombie = "アン";

		// Token: 0x0400B194 RID: 45460
		private const string Label_Tribe_Warrior = "戦";

		// Token: 0x0400B195 RID: 45461
		private const string Label_Tribe_BeastWarrior = "獣\ufffd";

		// Token: 0x0400B196 RID: 45462
		private const string Label_Tribe_Beast = "\ufffd";

		// Token: 0x0400B197 RID: 45463
		private const string Label_Tribe_WingedBeast = "鳥";

		// Token: 0x0400B198 RID: 45464
		private const string Label_Tribe_Machine = "機";

		// Token: 0x0400B199 RID: 45465
		private const string Label_Tribe_Fiend = "悪";

		// Token: 0x0400B19A RID: 45466
		private const string Label_Tribe_Fairy = "天";

		// Token: 0x0400B19B RID: 45467
		private const string Label_Tribe_Insect = "昆";

		// Token: 0x0400B19C RID: 45468
		private const string Label_Tribe_Dinosaur = "恐";

		// Token: 0x0400B19D RID: 45469
		private const string Label_Tribe_Reptile = "爬\ufffd";

		// Token: 0x0400B19E RID: 45470
		private const string Label_Tribe_Fish = "\ufffd";

		// Token: 0x0400B19F RID: 45471
		private const string Label_Tribe_SeaSerpent = "海";

		// Token: 0x0400B1A0 RID: 45472
		private const string Label_Tribe_Aqua = "\ufffd";

		// Token: 0x0400B1A1 RID: 45473
		private const string Label_Tribe_Pyro = "\ufffd";

		// Token: 0x0400B1A2 RID: 45474
		private const string Label_Tribe_Thunder = "\ufffd";

		// Token: 0x0400B1A3 RID: 45475
		private const string Label_Tribe_Rock = "岩";

		// Token: 0x0400B1A4 RID: 45476
		private const string Label_Tribe_Plant = "植";

		// Token: 0x0400B1A5 RID: 45477
		private const string Label_Tribe_Psychic = "サイ";

		// Token: 0x0400B1A6 RID: 45478
		private const string Label_Tribe_Wyrm = "幻";

		// Token: 0x0400B1A7 RID: 45479
		private const string Label_Tribe_Cyberse = "サイ";

		// Token: 0x0400B1A8 RID: 45480
		private const string Label_Tribe_DivineBeast = "幻\ufffd";

		// Token: 0x0400B1A9 RID: 45481
		private const string Label_Lvl0 = "0";

		// Token: 0x0400B1AA RID: 45482
		private const string Label_Lvl1 = "1";

		// Token: 0x0400B1AB RID: 45483
		private const string Label_Lvl2 = "2";

		// Token: 0x0400B1AC RID: 45484
		private const string Label_Lvl3 = "3";

		// Token: 0x0400B1AD RID: 45485
		private const string Label_Lvl4 = "4";

		// Token: 0x0400B1AE RID: 45486
		private const string Label_Lvl5 = "5";

		// Token: 0x0400B1AF RID: 45487
		private const string Label_Lvl6 = "6";

		// Token: 0x0400B1B0 RID: 45488
		private const string Label_Lvl7 = "7";

		// Token: 0x0400B1B1 RID: 45489
		private const string Label_Lvl8 = "8";

		// Token: 0x0400B1B2 RID: 45490
		private const string Label_Lvl9 = "9";

		// Token: 0x0400B1B3 RID: 45491
		private const string Label_Lvl10 = "10";

		// Token: 0x0400B1B4 RID: 45492
		private const string Label_Lvl11 = "11";

		// Token: 0x0400B1B5 RID: 45493
		private const string Label_Lvl12 = "12";

		// Token: 0x0400B1B6 RID: 45494
		private const string Label_Lvl13 = "13";

		// Token: 0x0400B1B7 RID: 45495
		private const string Label_Spell_Normal = "通\ufffd";

		// Token: 0x0400B1B8 RID: 45496
		private const string Label_Spell_Field = "フィ\ufffd";

		// Token: 0x0400B1B9 RID: 45497
		private const string Label_Spell_Equip = "装\ufffd";

		// Token: 0x0400B1BA RID: 45498
		private const string Label_Spell_Continuous = "永\ufffd";

		// Token: 0x0400B1BB RID: 45499
		private const string Label_Spell_QuickPlay = "速\ufffd";

		// Token: 0x0400B1BC RID: 45500
		private const string Label_Spell_Ritual = "儀\ufffd";

		// Token: 0x0400B1BD RID: 45501
		private const string Label_Trap_Normal = "通";

		// Token: 0x0400B1BE RID: 45502
		private const string Label_Trap_Continuous = "永";

		// Token: 0x0400B1BF RID: 45503
		private const string Label_Trap_Counter = "カウ";

		// Token: 0x0400B1C0 RID: 45504
		private const string Label_Rarity_Normal = "N";

		// Token: 0x0400B1C1 RID: 45505
		private const string Label_Rarity_Rare = "R";

		// Token: 0x0400B1C2 RID: 45506
		private const string Label_Rarity_SuperRare = "SR";

		// Token: 0x0400B1C3 RID: 45507
		private const string Label_Rarity_UltraRare = "UR";

		// Token: 0x0400B1C4 RID: 45508
		private const string Label_Style_Normal = "Normal";

		// Token: 0x0400B1C5 RID: 45509
		private const string Label_Style_Shine = "Shine";

		// Token: 0x0400B1C6 RID: 45510
		private const string Label_Style_Royal = "Royal";

		// Token: 0x0400B1C7 RID: 45511
		private const string Label_Limit_0 = "禁\ufffd";

		// Token: 0x0400B1C8 RID: 45512
		private const string Label_Limit_1 = "制\ufffd";

		// Token: 0x0400B1C9 RID: 45513
		private const string Label_Limit_2 = "準制";

		// Token: 0x0400B1CA RID: 45514
		private const string Label_Limit_3 = "無制";

		// Token: 0x0400B1CB RID: 45515
		private const string Label_Cutin_Exist = "有\ufffd";

		// Token: 0x0400B1CC RID: 45516
		private const string Label_Cutin_NotExist = "無\ufffd";

		// Token: 0x0400B1CD RID: 45517
		private const string Label_Ability_Toon = "トゥ\ufffd";

		// Token: 0x0400B1CE RID: 45518
		private const string Label_Ability_Dual = "デュ\ufffd";

		// Token: 0x0400B1CF RID: 45519
		private const string Label_Ability_Union = "ユニ\ufffd";

		// Token: 0x0400B1D0 RID: 45520
		private const string Label_Ability_Spirit = "スピ\ufffd";

		// Token: 0x0400B1D1 RID: 45521
		private const string Label_Ability_Tuner = "チュ\ufffd";

		// Token: 0x0400B1D2 RID: 45522
		private const string Label_Ability_Reverse = "リバ\ufffd";

		// Token: 0x0400B1D3 RID: 45523
		private const string Label_Ability_SpSummon = "特殊\ufffd";

		// Token: 0x0400B1D4 RID: 45524
		private SearchFilter.Setting m_setting;

		// Token: 0x0400B1D5 RID: 45525
		private static Dictionary<string, SearchFilter.Setting.FRAME> frameSettingTbl;

		// Token: 0x0400B1D6 RID: 45526
		private static Dictionary<string, SearchFilter.Setting.ATTR> attrSettingTbl;

		// Token: 0x0400B1D7 RID: 45527
		private static Dictionary<string, SearchFilter.Setting.TRIBE> tribeSettingTbl;

		// Token: 0x0400B1D8 RID: 45528
		private static Dictionary<string, SearchFilter.Setting.LEVEL> lvlSettingTbl;

		// Token: 0x0400B1D9 RID: 45529
		private static Dictionary<string, SearchFilter.Setting.NOTMONSTER> spellSettingTbl;

		// Token: 0x0400B1DA RID: 45530
		private static Dictionary<string, SearchFilter.Setting.NOTMONSTER> trapSettingTbl;

		// Token: 0x0400B1DB RID: 45531
		private static Dictionary<string, SearchFilter.Setting.RARITY> raritySettingTbl;

		// Token: 0x0400B1DC RID: 45532
		private static Dictionary<string, SearchFilter.Setting.STYLE> styleSettingTbl;

		// Token: 0x0400B1DD RID: 45533
		private static Dictionary<string, SearchFilter.Setting.LIMIT> limitSettingTbl;

		// Token: 0x0400B1DE RID: 45534
		private static Dictionary<string, SearchFilter.Setting.CUTIN> cutinSettingTbl;

		// Token: 0x0400B1DF RID: 45535
		private static Dictionary<string, SearchFilter.Setting.ABILITY> abilitySettingTbl;

		// Token: 0x0400B1E0 RID: 45536
		private static Dictionary<string, string> frameButtonLabelTbl;

		// Token: 0x0400B1E1 RID: 45537
		private static Dictionary<string, Content.Attribute> attrButtonLabelTbl;

		// Token: 0x0400B1E2 RID: 45538
		private static Dictionary<string, Content.Type> tribeButtonLabelTbl;

		// Token: 0x0400B1E3 RID: 45539
		private static Dictionary<string, Content.Icon> spellButtonLabelTbl;

		// Token: 0x0400B1E4 RID: 45540
		private static Dictionary<string, Content.Icon> trapButtonLabelTbl;

		// Token: 0x0400B1E5 RID: 45541
		private static Dictionary<string, string> rarityButtonLabelTbl;

		// Token: 0x0400B1E6 RID: 45542
		private static Dictionary<string, string> styleButtonLabelTbl;

		// Token: 0x0400B1E7 RID: 45543
		private static Dictionary<string, string> limitButtonLabelTbl;

		// Token: 0x0400B1E8 RID: 45544
		private static Dictionary<string, string> cutinButtonLabelTbl;

		// Token: 0x0400B1E9 RID: 45545
		private static Dictionary<string, string> abilityButtonLabelTbl;

		// Token: 0x0400B1EA RID: 45546
		private static Dictionary<string, string> toggleLabelTbl;

		// Token: 0x0400B1EB RID: 45547
		private const int numToggles = 84;

		// Token: 0x0400B1EC RID: 45548
		private List<FilterToggle> toggles;

		// Token: 0x0400B1ED RID: 45549
		private Dictionary<FilterDialog.FilterGroupType, SelectionItem> groupTopItem;

		// Token: 0x0400B1EE RID: 45550
		private ExtendedScrollRect m_FilterScroll;

		// Token: 0x0400B1EF RID: 45551
		private bool contentScrollAnimation;

		// Token: 0x0400B1F0 RID: 45552
		private Vector2 targetContentPosition;

		// Token: 0x0400B1F1 RID: 45553
		private static SearchFilter.Setting deckEditSetting;

		// Token: 0x0400B1F2 RID: 45554
		private static List<FilterDialog.FilterGroupType> deckEditFilterGroupTypes;

		// Token: 0x0400B1F3 RID: 45555
		private readonly Dictionary<FilterDialog.FilterGroupType, int> groupSize;

		// Token: 0x0400B1F4 RID: 45556
		private readonly int spacing;

		// Token: 0x0400B1F5 RID: 45557
		private readonly int spacingMobile;

		// Token: 0x0400B1F6 RID: 45558
		private readonly int paddingTop;

		// Token: 0x0400B1F7 RID: 45559
		private readonly int paddingBottom;

		// Token: 0x0400B1F8 RID: 45560
		private readonly Dictionary<FilterDialog.FilterGroupType, int> groupSizeMobile;

		// Token: 0x0400B1F9 RID: 45561
		private const string k_ELabelCancelButton = "CancelButton";

		// Token: 0x0400B1FA RID: 45562
		private const string k_ELabelFilterButton = "FilterButton";

		// Token: 0x0400B1FB RID: 45563
		private const string k_ELabelResetButton = "ResetButton";

		// Token: 0x0400B1FC RID: 45564
		private const string k_ELabelCancelShortcut = "CancelShortcut";

		// Token: 0x0400B1FD RID: 45565
		private const string k_ELabelFilterShortcut = "FilterShortcut";

		// Token: 0x0400B1FE RID: 45566
		private const string k_ELabelResetShortcut = "ResetShortcut";

		// Token: 0x0400B1FF RID: 45567
		private const string k_ELabelResetShortcutMain = "ResetShortcut/ShortcutIcon0";

		// Token: 0x0400B200 RID: 45568
		private const string k_ELabelResetShortcutSub = "ResetShortcut/ShortcutIcon1";

		// Token: 0x0400B201 RID: 45569
		private SelectionButton m_CancelButton;

		// Token: 0x0400B202 RID: 45570
		private SelectionButton m_FilterButton;

		// Token: 0x0400B203 RID: 45571
		private SelectionButton m_ResetButton;

		// Token: 0x0400B204 RID: 45572
		private const string k_ELabelFilterGroupFrame = "Frame";

		// Token: 0x0400B205 RID: 45573
		private const string k_ELabelFilterGroupAttribute = "Attribute";

		// Token: 0x0400B206 RID: 45574
		private const string k_ELabelFilterGroupSpellTrap = "SpellTrap";

		// Token: 0x0400B207 RID: 45575
		private const string k_ELabelFilterGroupTribe = "Tribe";

		// Token: 0x0400B208 RID: 45576
		private const string k_ELabelFilterGroupLevel = "Level";

		// Token: 0x0400B209 RID: 45577
		private const string k_ELabelFilterGroupRarity = "Rarity";

		// Token: 0x0400B20A RID: 45578
		private const string k_ELabelFilterGroupAbility = "Ability";

		// Token: 0x0400B20B RID: 45579
		private const string k_ELabelFilterGroupStyle = "Style";

		// Token: 0x0400B20C RID: 45580
		private const string k_ELabelFilterGroupLimit = "Limit";

		// Token: 0x0400B20D RID: 45581
		private const string k_ELabelFilterGroupCutin = "Cutin";

		// Token: 0x0400B20E RID: 45582
		private List<FilterDialog.FilterGroupType> m_FilterGroupTypes;

		// Token: 0x0400B20F RID: 45583
		public SearchFilter.Setting m_DefaultSetting;

		// Token: 0x02000FE9 RID: 4073
		public enum FilterGroupType
		{
			// Token: 0x0400B211 RID: 45585
			Frame,
			// Token: 0x0400B212 RID: 45586
			Attribute,
			// Token: 0x0400B213 RID: 45587
			Tribe,
			// Token: 0x0400B214 RID: 45588
			Level,
			// Token: 0x0400B215 RID: 45589
			Spell,
			// Token: 0x0400B216 RID: 45590
			Trap,
			// Token: 0x0400B217 RID: 45591
			Rarity,
			// Token: 0x0400B218 RID: 45592
			Ability,
			// Token: 0x0400B219 RID: 45593
			Style,
			// Token: 0x0400B21A RID: 45594
			Limit,
			// Token: 0x0400B21B RID: 45595
			Cutin
		}

		// Token: 0x02000FEA RID: 4074
		private class FilterGroup : MonoBehaviour
		{
			// Token: 0x17000F90 RID: 3984
			// (get) Token: 0x06007AD9 RID: 31449 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007ADA RID: 31450 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_ButtonArea
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F91 RID: 3985
			// (get) Token: 0x06007ADB RID: 31451 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007ADC RID: 31452 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_RectTransform
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x06007ADD RID: 31453 RVA: 0x0000216D File Offset: 0x0000036D
			public void InitializeElements()
			{
			}

			// Token: 0x06007ADE RID: 31454 RVA: 0x0000216D File Offset: 0x0000036D
			private void Awake()
			{
			}

			// Token: 0x06007ADF RID: 31455 RVA: 0x0000216D File Offset: 0x0000036D
			public void Initialize(FilterDialog.FilterGroupType type)
			{
			}

			// Token: 0x06007AE0 RID: 31456 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddFilterToggle(string label, FilterToggle filterToggle)
			{
			}

			// Token: 0x0400B21C RID: 45596
			private const string k_ELabelIcon = "Icon";

			// Token: 0x0400B21D RID: 45597
			private const string k_ELabelText = "TextTMP";

			// Token: 0x0400B21E RID: 45598
			private const string k_ELabelButtonArea = "ButtonArea";

			// Token: 0x0400B21F RID: 45599
			private ElementObjectManager m_Eom;

			// Token: 0x0400B220 RID: 45600
			private Image m_GroupIconImage;

			// Token: 0x0400B221 RID: 45601
			private ExtendedTextMeshProUGUI m_GroupNameText;

			// Token: 0x0400B222 RID: 45602
			private GridLayoutGroup m_GridLayoutGroup;

			// Token: 0x0400B223 RID: 45603
			private bool isInitialized;

			// Token: 0x0400B224 RID: 45604
			private FilterDialog.FilterGroupType m_Type;

			// Token: 0x0400B225 RID: 45605
			private Dictionary<string, FilterToggle> m_FilterToggles;
		}
	}
}
