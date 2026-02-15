using System;
using System.Collections;
using System.Collections.Generic;
using YgomGame.Card;

namespace YgomGame.Deck
{
	// Token: 0x02000FF3 RID: 4083
	public static class SearchFilter
	{
		// Token: 0x06007B37 RID: 31543 RVA: 0x000F66E2 File Offset: 0x000F48E2
		private static void getSetting(SearchFilter.Setting setting, out BitArray frame, out BitArray attr, out BitArray tribe, out BitArray level, out BitArray notmonster, out BitArray rarity, out BitArray style, out BitArray limit, out BitArray cutin, out BitArray ability, out BitArray regulation, out BitArray option)
		{
			frame = null;
			attr = null;
			tribe = null;
			level = null;
			notmonster = null;
			rarity = null;
			style = null;
			limit = null;
			cutin = null;
			ability = null;
			regulation = null;
			option = null;
		}

		// Token: 0x06007B38 RID: 31544 RVA: 0x000F66E2 File Offset: 0x000F48E2
		private static void getRawSetting(SearchFilter.Setting setting, out BitArray frame, out BitArray attr, out BitArray tribe, out BitArray level, out BitArray notmonster, out BitArray rarity, out BitArray style, out BitArray limit, out BitArray cutin, out BitArray ability, out BitArray regulation, out BitArray option)
		{
			frame = null;
			attr = null;
			tribe = null;
			level = null;
			notmonster = null;
			rarity = null;
			style = null;
			limit = null;
			cutin = null;
			ability = null;
			regulation = null;
			option = null;
		}

		// Token: 0x06007B39 RID: 31545 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<CardBaseData> Filter(List<CardBaseData> list, SearchFilter.Setting setting, string keyword, bool includeDesc = true)
		{
			return null;
		}

		// Token: 0x06007B3A RID: 31546 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool predicate(int cardID, BitArray frame, BitArray attr, BitArray tribe, BitArray level, BitArray notmonster, BitArray rarity, BitArray style, BitArray limit, BitArray cutin, BitArray ability, BitArray regulation, BitArray option, int rareID = -1, int styleID = -1, bool includeDesc = true)
		{
			return false;
		}

		// Token: 0x06007B3B RID: 31547 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkEmpty(BitArray ba)
		{
			return false;
		}

		// Token: 0x06007B3C RID: 31548 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkFrame(Content.Frame val, BitArray ba)
		{
			return false;
		}

		// Token: 0x06007B3D RID: 31549 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkAttr(Content.Attribute val, BitArray ba)
		{
			return false;
		}

		// Token: 0x06007B3E RID: 31550 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkTribe(Content.Type val, BitArray ba)
		{
			return false;
		}

		// Token: 0x06007B3F RID: 31551 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkLevel(int val, BitArray ba)
		{
			return false;
		}

		// Token: 0x06007B40 RID: 31552 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkSpellIcon(Content.Icon val, BitArray ba)
		{
			return false;
		}

		// Token: 0x06007B41 RID: 31553 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkTrapIcon(Content.Icon val, BitArray ba)
		{
			return false;
		}

		// Token: 0x06007B42 RID: 31554 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkRarity(CardCollectionInfo.Rarity val, BitArray ba)
		{
			return false;
		}

		// Token: 0x06007B43 RID: 31555 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkStyle(CardCollectionInfo.Premium val, BitArray ba)
		{
			return false;
		}

		// Token: 0x06007B44 RID: 31556 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkLimit(int val, BitArray ba)
		{
			return false;
		}

		// Token: 0x06007B45 RID: 31557 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkCutin(bool val, BitArray ba)
		{
			return false;
		}

		// Token: 0x06007B46 RID: 31558 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkAbility(SearchFilter.AbilityMask val, BitArray ba)
		{
			return false;
		}

		// Token: 0x06007B47 RID: 31559 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkRegulation(CardCollectionInfo.Regulation val, BitArray ba)
		{
			return false;
		}

		// Token: 0x06007B48 RID: 31560 RVA: 0x0000216D File Offset: 0x0000036D
		private static void checkCollectionSetting(string keywordsString)
		{
		}

		// Token: 0x06007B49 RID: 31561 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkCollection(int mrk, bool includeDesc = true)
		{
			return false;
		}

		// Token: 0x06007B4A RID: 31562 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool checkCollectionIDList(List<int> mrkList, string keywordsString, List<int> hitList, bool includeDesc = true)
		{
			return false;
		}

		// Token: 0x06007B4B RID: 31563 RVA: 0x0000216A File Offset: 0x0000036A
		private static string convertSearchText(string src)
		{
			return null;
		}

		// Token: 0x06007B4C RID: 31564 RVA: 0x0000216A File Offset: 0x0000036A
		private static string toZenkaku(string src)
		{
			return null;
		}

		// Token: 0x06007B4D RID: 31565 RVA: 0x0000216A File Offset: 0x0000036A
		private static string toKatakana_Komoji(string src)
		{
			return null;
		}

		// Token: 0x0400B267 RID: 45671
		public static int nowRegulationID;

		// Token: 0x0400B268 RID: 45672
		private static string[] searchKeywords;

		// Token: 0x0400B269 RID: 45673
		private static Dictionary<Content.Frame, SearchFilter.Setting.FRAME> frameTbl;

		// Token: 0x0400B26A RID: 45674
		private static Dictionary<Content.Frame, SearchFilter.Setting.FRAME> pendulumFrameTbl;

		// Token: 0x0400B26B RID: 45675
		private static Dictionary<Content.Attribute, SearchFilter.Setting.ATTR> attrTbl;

		// Token: 0x0400B26C RID: 45676
		private static Dictionary<Content.Type, SearchFilter.Setting.TRIBE> tribeTbl;

		// Token: 0x0400B26D RID: 45677
		private static Dictionary<int, SearchFilter.Setting.LEVEL> levelTbl;

		// Token: 0x0400B26E RID: 45678
		private static Dictionary<Content.Icon, SearchFilter.Setting.NOTMONSTER> spellTbl;

		// Token: 0x0400B26F RID: 45679
		private static Dictionary<Content.Icon, SearchFilter.Setting.NOTMONSTER> trapTbl;

		// Token: 0x0400B270 RID: 45680
		private static Dictionary<CardCollectionInfo.Rarity, SearchFilter.Setting.RARITY> rarityTbl;

		// Token: 0x0400B271 RID: 45681
		private static Dictionary<CardCollectionInfo.Premium, SearchFilter.Setting.STYLE> styleTbl;

		// Token: 0x0400B272 RID: 45682
		private static Dictionary<int, SearchFilter.Setting.LIMIT> limitTbl;

		// Token: 0x0400B273 RID: 45683
		private static Dictionary<bool, SearchFilter.Setting.CUTIN> cutinTbl;

		// Token: 0x0400B274 RID: 45684
		private static Dictionary<SearchFilter.AbilityMask, SearchFilter.Setting.ABILITY> abilityTbl;

		// Token: 0x0400B275 RID: 45685
		private static Dictionary<CardCollectionInfo.Regulation, SearchFilter.Setting.REGULATION> regulationTbl;

		// Token: 0x0400B276 RID: 45686
		private const int HAN_SPACE = 32;

		// Token: 0x0400B277 RID: 45687
		private const int HAN_YEN = 92;

		// Token: 0x0400B278 RID: 45688
		private const int HAN_ALPHA_START = 33;

		// Token: 0x0400B279 RID: 45689
		private const int HAN_ALPHA_END = 126;

		// Token: 0x0400B27A RID: 45690
		private const int ZEN_SPACE = 12288;

		// Token: 0x0400B27B RID: 45691
		private const int ZEN_YEN = 65509;

		// Token: 0x0400B27C RID: 45692
		private const int ZEN_ALPHA_START = 65281;

		// Token: 0x0400B27D RID: 45693
		private const int ZEN_ALPHA_OFFSET = 65248;

		// Token: 0x0400B27E RID: 45694
		private const int HIRA_START = 12352;

		// Token: 0x0400B27F RID: 45695
		private const int HIRA_END = 12442;

		// Token: 0x0400B280 RID: 45696
		private const int KATA_START = 12448;

		// Token: 0x0400B281 RID: 45697
		private const int KATA_OFFSET = 96;

		// Token: 0x0400B282 RID: 45698
		private const int ZAL_START = 65313;

		// Token: 0x0400B283 RID: 45699
		private const int ZAL_END = 65338;

		// Token: 0x0400B284 RID: 45700
		private const int ZAS_START = 65345;

		// Token: 0x0400B285 RID: 45701
		private const int ZAS_OFFSET = 32;

		// Token: 0x02000FF4 RID: 4084
		public class Setting
		{
			// Token: 0x06007B4E RID: 31566 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsFiltered()
			{
				return false;
			}

			// Token: 0x06007B4F RID: 31567 RVA: 0x000029CC File Offset: 0x00000BCC
			private static bool checkAny(BitArray ba)
			{
				return false;
			}

			// Token: 0x06007B50 RID: 31568 RVA: 0x0000216A File Offset: 0x0000036A
			public BitArray GetFrameSetting()
			{
				return null;
			}

			// Token: 0x06007B51 RID: 31569 RVA: 0x0000216A File Offset: 0x0000036A
			public BitArray GetAttrSetting()
			{
				return null;
			}

			// Token: 0x06007B52 RID: 31570 RVA: 0x0000216A File Offset: 0x0000036A
			public BitArray GetTribeSetting()
			{
				return null;
			}

			// Token: 0x06007B53 RID: 31571 RVA: 0x0000216A File Offset: 0x0000036A
			public BitArray GetLevel()
			{
				return null;
			}

			// Token: 0x06007B54 RID: 31572 RVA: 0x0000216A File Offset: 0x0000036A
			public BitArray GetNotMonster()
			{
				return null;
			}

			// Token: 0x06007B55 RID: 31573 RVA: 0x0000216A File Offset: 0x0000036A
			public BitArray GetRarity()
			{
				return null;
			}

			// Token: 0x06007B56 RID: 31574 RVA: 0x0000216A File Offset: 0x0000036A
			public BitArray GetStyle()
			{
				return null;
			}

			// Token: 0x06007B57 RID: 31575 RVA: 0x0000216A File Offset: 0x0000036A
			public BitArray GetLimit()
			{
				return null;
			}

			// Token: 0x06007B58 RID: 31576 RVA: 0x0000216A File Offset: 0x0000036A
			public BitArray GetCutin()
			{
				return null;
			}

			// Token: 0x06007B59 RID: 31577 RVA: 0x0000216A File Offset: 0x0000036A
			public BitArray GetAbility()
			{
				return null;
			}

			// Token: 0x06007B5A RID: 31578 RVA: 0x0000216A File Offset: 0x0000036A
			public BitArray GetRegulation()
			{
				return null;
			}

			// Token: 0x06007B5B RID: 31579 RVA: 0x0000216A File Offset: 0x0000036A
			public BitArray GetOption()
			{
				return null;
			}

			// Token: 0x06007B5C RID: 31580 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetRarityFilter(SearchFilter.Setting.RARITY val, bool b)
			{
			}

			// Token: 0x06007B5D RID: 31581 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetStyleFilter(SearchFilter.Setting.STYLE val, bool b)
			{
			}

			// Token: 0x06007B5E RID: 31582 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetLimitFilter(SearchFilter.Setting.LIMIT val, bool b)
			{
			}

			// Token: 0x06007B5F RID: 31583 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetCutinFilter(SearchFilter.Setting.CUTIN val, bool b)
			{
			}

			// Token: 0x06007B60 RID: 31584 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetAbilityFilter(SearchFilter.Setting.ABILITY val, bool b)
			{
			}

			// Token: 0x06007B61 RID: 31585 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetFrameFilter(SearchFilter.Setting.FRAME val, bool b)
			{
			}

			// Token: 0x06007B62 RID: 31586 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetNotMonsterFilter(SearchFilter.Setting.NOTMONSTER val, bool b)
			{
			}

			// Token: 0x06007B63 RID: 31587 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetAttrFilter(SearchFilter.Setting.ATTR val, bool b)
			{
			}

			// Token: 0x06007B64 RID: 31588 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetTribeFilter(SearchFilter.Setting.TRIBE val, bool b)
			{
			}

			// Token: 0x06007B65 RID: 31589 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetLevelFilter(SearchFilter.Setting.LEVEL val, bool b)
			{
			}

			// Token: 0x06007B66 RID: 31590 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOptionSetting(SearchFilter.Setting.OPTIONS val, bool b)
			{
			}

			// Token: 0x06007B67 RID: 31591 RVA: 0x0000216A File Offset: 0x0000036A
			public SearchFilter.Setting Clone()
			{
				return null;
			}

			// Token: 0x0400B286 RID: 45702
			public BitArray Options;

			// Token: 0x0400B287 RID: 45703
			public BitArray Frame;

			// Token: 0x0400B288 RID: 45704
			public BitArray Attr;

			// Token: 0x0400B289 RID: 45705
			public BitArray Tribe;

			// Token: 0x0400B28A RID: 45706
			public BitArray Level;

			// Token: 0x0400B28B RID: 45707
			public BitArray NotMonster;

			// Token: 0x0400B28C RID: 45708
			public BitArray Rarity;

			// Token: 0x0400B28D RID: 45709
			public BitArray Style;

			// Token: 0x0400B28E RID: 45710
			public BitArray Limit;

			// Token: 0x0400B28F RID: 45711
			public BitArray Cutin;

			// Token: 0x0400B290 RID: 45712
			public BitArray Ability;

			// Token: 0x0400B291 RID: 45713
			public BitArray Regulation;

			// Token: 0x02000FF5 RID: 4085
			public enum OPTIONS
			{
				// Token: 0x0400B293 RID: 45715
				NotOwned,
				// Token: 0x0400B294 RID: 45716
				SIZE
			}

			// Token: 0x02000FF6 RID: 4086
			public enum FRAME
			{
				// Token: 0x0400B296 RID: 45718
				Normal,
				// Token: 0x0400B297 RID: 45719
				Effect,
				// Token: 0x0400B298 RID: 45720
				Fusion,
				// Token: 0x0400B299 RID: 45721
				Ritual,
				// Token: 0x0400B29A RID: 45722
				Synchro,
				// Token: 0x0400B29B RID: 45723
				Xyz,
				// Token: 0x0400B29C RID: 45724
				Pendulum,
				// Token: 0x0400B29D RID: 45725
				Link,
				// Token: 0x0400B29E RID: 45726
				Magic,
				// Token: 0x0400B29F RID: 45727
				Trap,
				// Token: 0x0400B2A0 RID: 45728
				SIZE
			}

			// Token: 0x02000FF7 RID: 4087
			public enum ATTR
			{
				// Token: 0x0400B2A2 RID: 45730
				Light,
				// Token: 0x0400B2A3 RID: 45731
				Dark,
				// Token: 0x0400B2A4 RID: 45732
				Water,
				// Token: 0x0400B2A5 RID: 45733
				Fire,
				// Token: 0x0400B2A6 RID: 45734
				Earth,
				// Token: 0x0400B2A7 RID: 45735
				Wind,
				// Token: 0x0400B2A8 RID: 45736
				Divine,
				// Token: 0x0400B2A9 RID: 45737
				SIZE
			}

			// Token: 0x02000FF8 RID: 4088
			public enum TRIBE
			{
				// Token: 0x0400B2AB RID: 45739
				SpellCaster,
				// Token: 0x0400B2AC RID: 45740
				Dragon,
				// Token: 0x0400B2AD RID: 45741
				Zombie,
				// Token: 0x0400B2AE RID: 45742
				Warrior,
				// Token: 0x0400B2AF RID: 45743
				BeastWarrior,
				// Token: 0x0400B2B0 RID: 45744
				Beast,
				// Token: 0x0400B2B1 RID: 45745
				WingedBeast,
				// Token: 0x0400B2B2 RID: 45746
				Machine,
				// Token: 0x0400B2B3 RID: 45747
				Fiend,
				// Token: 0x0400B2B4 RID: 45748
				Fairy,
				// Token: 0x0400B2B5 RID: 45749
				Insect,
				// Token: 0x0400B2B6 RID: 45750
				Dinosaur,
				// Token: 0x0400B2B7 RID: 45751
				Reptile,
				// Token: 0x0400B2B8 RID: 45752
				Fish,
				// Token: 0x0400B2B9 RID: 45753
				SeaSerpent,
				// Token: 0x0400B2BA RID: 45754
				Aqua,
				// Token: 0x0400B2BB RID: 45755
				Pyro,
				// Token: 0x0400B2BC RID: 45756
				Thunder,
				// Token: 0x0400B2BD RID: 45757
				Rock,
				// Token: 0x0400B2BE RID: 45758
				Plant,
				// Token: 0x0400B2BF RID: 45759
				Psychic,
				// Token: 0x0400B2C0 RID: 45760
				Wyrm,
				// Token: 0x0400B2C1 RID: 45761
				Cyberse,
				// Token: 0x0400B2C2 RID: 45762
				DivineBeast,
				// Token: 0x0400B2C3 RID: 45763
				SIZE
			}

			// Token: 0x02000FF9 RID: 4089
			public enum LEVEL
			{
				// Token: 0x0400B2C5 RID: 45765
				Lvl0,
				// Token: 0x0400B2C6 RID: 45766
				Lvl1,
				// Token: 0x0400B2C7 RID: 45767
				Lvl2,
				// Token: 0x0400B2C8 RID: 45768
				Lvl3,
				// Token: 0x0400B2C9 RID: 45769
				Lvl4,
				// Token: 0x0400B2CA RID: 45770
				Lvl5,
				// Token: 0x0400B2CB RID: 45771
				Lvl6,
				// Token: 0x0400B2CC RID: 45772
				Lvl7,
				// Token: 0x0400B2CD RID: 45773
				Lvl8,
				// Token: 0x0400B2CE RID: 45774
				Lvl9,
				// Token: 0x0400B2CF RID: 45775
				Lvl10,
				// Token: 0x0400B2D0 RID: 45776
				Lvl11,
				// Token: 0x0400B2D1 RID: 45777
				Lvl12,
				// Token: 0x0400B2D2 RID: 45778
				Lvl13,
				// Token: 0x0400B2D3 RID: 45779
				SIZE
			}

			// Token: 0x02000FFA RID: 4090
			public enum NOTMONSTER
			{
				// Token: 0x0400B2D5 RID: 45781
				NormalSpell,
				// Token: 0x0400B2D6 RID: 45782
				FieldSpell,
				// Token: 0x0400B2D7 RID: 45783
				EquipSpell,
				// Token: 0x0400B2D8 RID: 45784
				ContinuousSpell,
				// Token: 0x0400B2D9 RID: 45785
				QuickPlaySpell,
				// Token: 0x0400B2DA RID: 45786
				RitualSpell,
				// Token: 0x0400B2DB RID: 45787
				NormalTrap,
				// Token: 0x0400B2DC RID: 45788
				ContinuousTrap,
				// Token: 0x0400B2DD RID: 45789
				CounterTrap,
				// Token: 0x0400B2DE RID: 45790
				SIZE
			}

			// Token: 0x02000FFB RID: 4091
			public enum RARITY
			{
				// Token: 0x0400B2E0 RID: 45792
				Normal,
				// Token: 0x0400B2E1 RID: 45793
				Rare,
				// Token: 0x0400B2E2 RID: 45794
				SuperRare,
				// Token: 0x0400B2E3 RID: 45795
				UltraRare,
				// Token: 0x0400B2E4 RID: 45796
				SIZE
			}

			// Token: 0x02000FFC RID: 4092
			public enum STYLE
			{
				// Token: 0x0400B2E6 RID: 45798
				Normal,
				// Token: 0x0400B2E7 RID: 45799
				Shine,
				// Token: 0x0400B2E8 RID: 45800
				Royal,
				// Token: 0x0400B2E9 RID: 45801
				SIZE
			}

			// Token: 0x02000FFD RID: 4093
			public enum LIMIT
			{
				// Token: 0x0400B2EB RID: 45803
				Limit0,
				// Token: 0x0400B2EC RID: 45804
				Limit1,
				// Token: 0x0400B2ED RID: 45805
				Limit2,
				// Token: 0x0400B2EE RID: 45806
				Limit3,
				// Token: 0x0400B2EF RID: 45807
				SIZE
			}

			// Token: 0x02000FFE RID: 4094
			public enum CUTIN
			{
				// Token: 0x0400B2F1 RID: 45809
				Exist,
				// Token: 0x0400B2F2 RID: 45810
				NotExist,
				// Token: 0x0400B2F3 RID: 45811
				SIZE
			}

			// Token: 0x02000FFF RID: 4095
			public enum ABILITY
			{
				// Token: 0x0400B2F5 RID: 45813
				Toon,
				// Token: 0x0400B2F6 RID: 45814
				Dual,
				// Token: 0x0400B2F7 RID: 45815
				Union,
				// Token: 0x0400B2F8 RID: 45816
				Spirit,
				// Token: 0x0400B2F9 RID: 45817
				Tuner,
				// Token: 0x0400B2FA RID: 45818
				Reverse,
				// Token: 0x0400B2FB RID: 45819
				SpSummon,
				// Token: 0x0400B2FC RID: 45820
				SIZE
			}

			// Token: 0x02001000 RID: 4096
			public enum REGULATION
			{
				// Token: 0x0400B2FE RID: 45822
				Forbidden,
				// Token: 0x0400B2FF RID: 45823
				Limited,
				// Token: 0x0400B300 RID: 45824
				SemiLimited,
				// Token: 0x0400B301 RID: 45825
				None,
				// Token: 0x0400B302 RID: 45826
				SIZE
			}
		}

		// Token: 0x02001001 RID: 4097
		private enum AbilityMask
		{
			// Token: 0x0400B304 RID: 45828
			Toon = 1,
			// Token: 0x0400B305 RID: 45829
			Dual,
			// Token: 0x0400B306 RID: 45830
			Union = 4,
			// Token: 0x0400B307 RID: 45831
			Spirit = 8,
			// Token: 0x0400B308 RID: 45832
			Tuner = 16,
			// Token: 0x0400B309 RID: 45833
			Reverse = 32,
			// Token: 0x0400B30A RID: 45834
			SpSummon = 64
		}
	}
}
