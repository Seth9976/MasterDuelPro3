using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Card;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000CD1 RID: 3281
	public class CardInfoBase : MonoBehaviour
	{
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06005D6D RID: 23917 RVA: 0x0000216A File Offset: 0x0000036A
		protected static Content m_CCI
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06005D6E RID: 23918 RVA: 0x0000216A File Offset: 0x0000036A
		protected DuelIconSprites m_DuelIconSprites
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06005D6F RID: 23919 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06005D70 RID: 23920 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_Window
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06005D71 RID: 23921 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_LinkArrows
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06005D72 RID: 23922 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_PenScale
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06005D73 RID: 23923 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_TextArea
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06005D74 RID: 23924 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_NameArea
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06005D75 RID: 23925 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_StatueIcons
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06005D76 RID: 23926 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_SpTrTypeRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06005D77 RID: 23927 RVA: 0x0000216A File Offset: 0x0000036A
		protected RubyTextGX m_CardName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06005D78 RID: 23928 RVA: 0x0000216A File Offset: 0x0000036A
		protected RawImage m_CardImage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06005D79 RID: 23929 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_GachaRareIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06005D7A RID: 23930 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_AttributeIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06005D7B RID: 23931 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_AttrOutline
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06005D7C RID: 23932 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_TunerIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06005D7D RID: 23933 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_TunerGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06005D7E RID: 23934 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_TunerOutline
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06005D7F RID: 23935 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_LevelIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06005D80 RID: 23936 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_LinkIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06005D81 RID: 23937 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_RankIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06005D82 RID: 23938 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_XyzMatIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06005D83 RID: 23939 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_TypeIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06005D84 RID: 23940 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_TypeGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06005D85 RID: 23941 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_ActivatedGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06005D86 RID: 23942 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_TypeOutline
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06005D87 RID: 23943 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_SpTrTypeIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06005D88 RID: 23944 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_AtkIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06005D89 RID: 23945 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_DefIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06005D8A RID: 23946 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_NameAreaBg
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06005D8B RID: 23947 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_TypeAreaBg
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06005D8C RID: 23948 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_TurnElapsedIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06005D8D RID: 23949 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_ActivatedIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06005D8E RID: 23950 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_ActivatedPlate
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06005D8F RID: 23951 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_XyzMatNum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06005D90 RID: 23952 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_TurnElapsedNum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06005D91 RID: 23953 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_PenScaleNum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06005D92 RID: 23954 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_LinkNum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06005D93 RID: 23955 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_LevelNum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06005D94 RID: 23956 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_RankNum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06005D95 RID: 23957 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_SpTrType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06005D96 RID: 23958 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_AtkValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06005D97 RID: 23959 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_DefValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06005D98 RID: 23960 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_DspTitle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06005D99 RID: 23961 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_DspContent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06005D9A RID: 23962 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionButton m_CardArea
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06005D9B RID: 23963 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionItem m_TextAreaItem
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06005D9C RID: 23964 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_ColorBarTeam0
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06005D9D RID: 23965 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_ColorBarTeam1
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06005D9E RID: 23966 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005D9F RID: 23967 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isShowing
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x06005DA0 RID: 23968 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsTextResourceLoaded()
		{
			return false;
		}

		// Token: 0x06005DA1 RID: 23969 RVA: 0x0000216D File Offset: 0x0000036D
		protected static void LoadCardInfoBaseResource()
		{
		}

		// Token: 0x06005DA2 RID: 23970 RVA: 0x0000216D File Offset: 0x0000036D
		protected static void UnloadCardInfoBaseResource()
		{
		}

		// Token: 0x06005DA3 RID: 23971 RVA: 0x000F50AC File Offset: 0x000F32AC
		protected CardInfoData UpdateCardInfoDataByUniqueId(int uniqueid)
		{
			return default(CardInfoData);
		}

		// Token: 0x06005DA4 RID: 23972 RVA: 0x000F50C4 File Offset: 0x000F32C4
		protected CardInfoData UpdateCardInfoDataByCardId(int cardid, int styleid, int player = -1)
		{
			return default(CardInfoData);
		}

		// Token: 0x06005DA5 RID: 23973 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetRubyName(int cardidorg, int cardidDisp)
		{
		}

		// Token: 0x06005DA6 RID: 23974 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetAttribute(int cardidorg, Content.Attribute attrdisp)
		{
		}

		// Token: 0x06005DA7 RID: 23975 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetBgColor(Image bg, Content.Frame type)
		{
		}

		// Token: 0x06005DA8 RID: 23976 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetOwner()
		{
		}

		// Token: 0x06005DA9 RID: 23977 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCardImage(int cardidorg, int styleid)
		{
		}

		// Token: 0x06005DAA RID: 23978 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetLinkArrows(int cardidorg)
		{
		}

		// Token: 0x06005DAB RID: 23979 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetOverlayNum(int cardidorg, int owner, int locate, int index, int overlaynum)
		{
		}

		// Token: 0x06005DAC RID: 23980 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetPendulumScale(int cardidorg, int owner, int locate, int index, int scale, int orgscale)
		{
		}

		// Token: 0x06005DAD RID: 23981 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTunerIcon(int cardidorg, int owner, int locate, int index)
		{
		}

		// Token: 0x06005DAE RID: 23982 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTurnElapsed(int owner, int locate, int index, int turncounter)
		{
		}

		// Token: 0x06005DAF RID: 23983 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetLevel(int leveldisp, int levelorg)
		{
		}

		// Token: 0x06005DB0 RID: 23984 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetRank(int rankdisp, int rankorg)
		{
		}

		// Token: 0x06005DB1 RID: 23985 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetLinkNum(int cardidorg)
		{
		}

		// Token: 0x06005DB2 RID: 23986 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetType(int typedispid, int typeorg)
		{
		}

		// Token: 0x06005DB3 RID: 23987 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetAtk(int cardidorg, int atkdisp, int atkorigin, bool ismonsternow)
		{
		}

		// Token: 0x06005DB4 RID: 23988 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetDef(int cardidorg, int defdisp, int deforigin, bool ismonsternow)
		{
		}

		// Token: 0x06005DB5 RID: 23989 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCounters(int owner, int locate, int index, int maxcounternum, List<KeyValuePair<Engine.CounterType, int>> countertable)
		{
		}

		// Token: 0x06005DB6 RID: 23990 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetSpTrType(int cardidorg, int player, int position, bool hasinstance)
		{
		}

		// Token: 0x06005DB7 RID: 23991 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetDspTitle(int cardidorg, int typeid, int effectid, int player, int position, bool istuner, bool hasinstance)
		{
		}

		// Token: 0x06005DB8 RID: 23992 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetActivatedIcon(int cardId, int owner)
		{
		}

		// Token: 0x06005DB9 RID: 23993 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image GetLinkArrow(int index)
		{
			return null;
		}

		// Token: 0x06005DBA RID: 23994 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddTypeText(ref string origin, Content.Type type, bool typechanged)
		{
		}

		// Token: 0x06005DBB RID: 23995 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddKindText(ref string origin, Content.Kind kind, bool effectchanged, bool tunerchanged, bool isTrap, bool isTrapMonster)
		{
		}

		// Token: 0x06005DBC RID: 23996 RVA: 0x000F50DC File Offset: 0x000F32DC
		protected Color GetFontColor(CardInfoBase.ValueState valuestate)
		{
			return default(Color);
		}

		// Token: 0x06005DBD RID: 23997 RVA: 0x0000216A File Offset: 0x0000036A
		protected string AddColorTag(string origin, Color color)
		{
			return null;
		}

		// Token: 0x06005DBE RID: 23998 RVA: 0x0000216A File Offset: 0x0000036A
		protected string AddColorTag(string origin, string colorcode)
		{
			return null;
		}

		// Token: 0x06005DBF RID: 23999 RVA: 0x0000216A File Offset: 0x0000036A
		protected string AddAlphaTag(string origin, string alphacode)
		{
			return null;
		}

		// Token: 0x06005DC0 RID: 24000 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetRareIcon(int cardid, RarityIconBinder.Type type)
		{
		}

		// Token: 0x06005DC1 RID: 24001 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetStatueBar(bool visible)
		{
		}

		// Token: 0x06005DC2 RID: 24002 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image GetCounterImage(int index)
		{
			return null;
		}

		// Token: 0x06005DC3 RID: 24003 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI GetCounterText(int index)
		{
			return null;
		}

		// Token: 0x06005DC4 RID: 24004 RVA: 0x0000216D File Offset: 0x0000036D
		protected void InitStatueIconTable()
		{
		}

		// Token: 0x06005DC5 RID: 24005 RVA: 0x0000216D File Offset: 0x0000036D
		protected void InitCardInfoData()
		{
		}

		// Token: 0x06005DC6 RID: 24006 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetStatueIconPopUpText(int index, string text)
		{
		}

		// Token: 0x06005DC7 RID: 24007 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetHighlightEffectHappening(int uniqueid, int textid)
		{
		}

		// Token: 0x06005DC8 RID: 24008 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateHighLightTextIdTableForToHappen(int index, int textid)
		{
		}

		// Token: 0x06005DC9 RID: 24009 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveHightEffect()
		{
		}

		// Token: 0x06005DCA RID: 24010 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAttributeFlag(int flagbit, int cardid, int owner = -1, int locate = -1, int index = -1, bool isfightableoneffect = false)
		{
		}

		// Token: 0x06005DCB RID: 24011 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void InitializeBase()
		{
		}

		// Token: 0x06005DCC RID: 24012 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06005DCD RID: 24013 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckHighLightTextIdValid(int textid)
		{
			return false;
		}

		// Token: 0x040098D2 RID: 39122
		protected Color m_FontColorNormal;

		// Token: 0x040098D3 RID: 39123
		protected Color m_FontColorChanged;

		// Token: 0x040098D4 RID: 39124
		protected const int MAXLINKNUM = 8;

		// Token: 0x040098D5 RID: 39125
		protected const int BIT_CANTATTACK = 512;

		// Token: 0x040098D6 RID: 39126
		protected const int BIT_HANDOPEN = 2048;

		// Token: 0x040098D7 RID: 39127
		public const int INVALID_HIGHLIGHT_TEXTID = 0;

		// Token: 0x040098D8 RID: 39128
		protected const string SIGN_BRACKET_PRE = "\ufffd";

		// Token: 0x040098D9 RID: 39129
		protected const string SIGN_BRACKET_SUF = "\ufffd";

		// Token: 0x040098DA RID: 39130
		protected const string SIGN_SLASH = "\ufffd";

		// Token: 0x040098DB RID: 39131
		protected const string LABEL_TWEEN_SCROLL = "AutoScroll";

		// Token: 0x040098DC RID: 39132
		protected const string LABEL_RT_WINDOW = "Window";

		// Token: 0x040098DD RID: 39133
		protected const string LABEL_RT_PENDULUMSCALE = "PendulumScale";

		// Token: 0x040098DE RID: 39134
		protected const string LABEL_RT_LINKARROWS = "LinkArrows";

		// Token: 0x040098DF RID: 39135
		protected const string LABEL_RT_NAMEAREA = "NameArea";

		// Token: 0x040098E0 RID: 39136
		protected const string LABEL_RT_TEXTAREA = "TextArea";

		// Token: 0x040098E1 RID: 39137
		protected const string LABEL_RT_STATUEICONS = "StatueIcons";

		// Token: 0x040098E2 RID: 39138
		protected const string LABEL_RT_ACTIVATEDGROUP = "ActivatedGroup";

		// Token: 0x040098E3 RID: 39139
		protected const string LABEL_RTXT_CARDNAME = "TextCardName";

		// Token: 0x040098E4 RID: 39140
		protected const string LABEL_RIMG_CARDIMAGE = "ImageCard";

		// Token: 0x040098E5 RID: 39141
		protected const string LABEL_IMG_ICONRARITY = "IconRarity";

		// Token: 0x040098E6 RID: 39142
		protected const string LABEL_IMG_ATTRIBUTEICON = "IconAttribute";

		// Token: 0x040098E7 RID: 39143
		protected const string LABEL_IMG_ATTROUTLINE = "AttrOutline";

		// Token: 0x040098E8 RID: 39144
		protected const string LABEL_IMG_TUNERGROUP = "TunerGroup";

		// Token: 0x040098E9 RID: 39145
		protected const string LABEL_IMG_TUNERICON = "IconTuner";

		// Token: 0x040098EA RID: 39146
		protected const string LABEL_IMG_TUNEROUTLINE = "TunerOutline";

		// Token: 0x040098EB RID: 39147
		protected const string LABEL_IMG_LINKARROW = "LinkArrow";

		// Token: 0x040098EC RID: 39148
		protected const string LABEL_IMG_LINKICON = "IconLink";

		// Token: 0x040098ED RID: 39149
		protected const string LABEL_IMG_LEVELICON = "IconLevel";

		// Token: 0x040098EE RID: 39150
		protected const string LABEL_IMG_RANKICON = "IconRank";

		// Token: 0x040098EF RID: 39151
		protected const string LABEL_IMG_XYZMATICON = "IconXyzMaterial";

		// Token: 0x040098F0 RID: 39152
		protected const string LABEL_IMG_TYPEGROUP = "TypeGroup";

		// Token: 0x040098F1 RID: 39153
		protected const string LABEL_IMG_TYPEICON = "IconType";

		// Token: 0x040098F2 RID: 39154
		protected const string LABEL_IMG_TYPEOUTLINE = "TypeOutline";

		// Token: 0x040098F3 RID: 39155
		protected const string LABEL_IMG_SPTRTYPEROOT = "SpellTrapType";

		// Token: 0x040098F4 RID: 39156
		protected const string LABEL_IMG_SPTRTYPE = "IconSpellTrapType";

		// Token: 0x040098F5 RID: 39157
		protected const string LABEL_IMG_ATKICON = "IconAtk";

		// Token: 0x040098F6 RID: 39158
		protected const string LABEL_IMG_DEFICON = "IconDef";

		// Token: 0x040098F7 RID: 39159
		protected const string LABEL_IMG_TURNNUM = "IconTurnCounter";

		// Token: 0x040098F8 RID: 39160
		protected const string LABEL_IMG_COUNTER = "IconCounter";

		// Token: 0x040098F9 RID: 39161
		protected const string LABEL_IMG_PLATETITLE = "PlateTitle";

		// Token: 0x040098FA RID: 39162
		protected const string LABEL_IMG_PLATEDESCRIPTION = "PlateDescription";

		// Token: 0x040098FB RID: 39163
		protected const string LABEL_IMG_ACTIVATEDICON = "IconActivated";

		// Token: 0x040098FC RID: 39164
		protected const string LABEL_IMG_PLATEACTIVATED = "PlateActivated";

		// Token: 0x040098FD RID: 39165
		protected const string LABEL_TXT_LEVELNUM = "TextLevel";

		// Token: 0x040098FE RID: 39166
		protected const string LABEL_TXT_RANKNUM = "TextRank";

		// Token: 0x040098FF RID: 39167
		protected const string LABEL_TXT_XYZMATNUM = "TextXyzMaterial";

		// Token: 0x04009900 RID: 39168
		protected const string LABEL_TXT_TURNCOUNTERNUM = "TextTurnCounter";

		// Token: 0x04009901 RID: 39169
		protected const string LABEL_TXT_PENDULUMSCALENUM = "TextPendulumScale";

		// Token: 0x04009902 RID: 39170
		protected const string LABEL_TXT_LINKNUM = "TextLink";

		// Token: 0x04009903 RID: 39171
		protected const string LABEL_TXT_SPTRTYPE = "TextSpellTrapType";

		// Token: 0x04009904 RID: 39172
		protected const string LABEL_TXT_ATKVALUE = "TextAtk";

		// Token: 0x04009905 RID: 39173
		protected const string LABEL_TXT_DEFVALUE = "TextDef";

		// Token: 0x04009906 RID: 39174
		protected const string LABEL_TXT_COUNTERNUM = "CounterNum";

		// Token: 0x04009907 RID: 39175
		protected const string LABEL_TXT_DSPTITLE = "TextDescriptionItem";

		// Token: 0x04009908 RID: 39176
		protected const string LABEL_TXT_DSPCONTENT = "TextDescriptionValue";

		// Token: 0x04009909 RID: 39177
		protected const string LABEL_GO_STATUEICON_DISABLEFFECT = "IconStatueDiable";

		// Token: 0x0400990A RID: 39178
		protected const string LABEL_GO_STATUEICON_CANTREVIVE = "IconStatueCantRevive";

		// Token: 0x0400990B RID: 39179
		protected const string LABEL_GO_STATUEICON_CANTATTACK = "IconStatueCantAttack";

		// Token: 0x0400990C RID: 39180
		protected const string LABEL_GO_STATUEICON_HANDOPEN = "IconStatueHandOpen";

		// Token: 0x0400990D RID: 39181
		protected const string LABEL_GO_STATUEICON_FUSIONMATERIAL = "IconStatueFusionMat";

		// Token: 0x0400990E RID: 39182
		protected const string LABEL_GO_STATUEICON_SYNCMATERIAL = "IconStatueSyncMat";

		// Token: 0x0400990F RID: 39183
		protected const string LABEL_GO_STATUEICON_DEMENSIONHOLE = "IconStatueDemensionHole";

		// Token: 0x04009910 RID: 39184
		protected const string LABEL_GO_STATUEICON_BYBATTLE = "IconStatueByBattle";

		// Token: 0x04009911 RID: 39185
		protected const string LABEL_BTN_CARDAREA = "CardArea";

		// Token: 0x04009912 RID: 39186
		protected const string LABEL_ICON_CARDAREA_SHORTCUT = "CardAreaShortcut";

		// Token: 0x04009913 RID: 39187
		protected const string LABEL_BAR_TEAM0 = "ColorBarTeam0";

		// Token: 0x04009914 RID: 39188
		protected const string LABEL_BAR_TEAM1 = "ColorBarTeam1";

		// Token: 0x04009915 RID: 39189
		protected const string HIGHLIGHTTEXTCOLORCODE = "00D2FF";

		// Token: 0x04009916 RID: 39190
		protected Dictionary<int, GameObject> m_StatueIconTable;

		// Token: 0x04009917 RID: 39191
		protected ElementObjectManager m_EOManager_Property;

		// Token: 0x04009918 RID: 39192
		protected RectTransform m_Window_Property;

		// Token: 0x04009919 RID: 39193
		protected RectTransform m_LinkArrows_Property;

		// Token: 0x0400991A RID: 39194
		protected RectTransform m_PenScale_Property;

		// Token: 0x0400991B RID: 39195
		protected RectTransform m_TextArea_Property;

		// Token: 0x0400991C RID: 39196
		protected RectTransform m_NameArea_Property;

		// Token: 0x0400991D RID: 39197
		protected RectTransform m_StatueIcons_Property;

		// Token: 0x0400991E RID: 39198
		protected RectTransform m_SpTrTypeRoot_Property;

		// Token: 0x0400991F RID: 39199
		protected RubyTextGX m_CardName_Property;

		// Token: 0x04009920 RID: 39200
		protected RawImage m_CardImage_Property;

		// Token: 0x04009921 RID: 39201
		protected Image m_GachaRareIcon_Property;

		// Token: 0x04009922 RID: 39202
		protected Image m_AttributeIcon_Property;

		// Token: 0x04009923 RID: 39203
		protected Image m_AttrOutline_Property;

		// Token: 0x04009924 RID: 39204
		protected Image m_TunerIcon_Property;

		// Token: 0x04009925 RID: 39205
		protected RectTransform m_TunerGroup_Property;

		// Token: 0x04009926 RID: 39206
		protected Image m_TunerOutline_Property;

		// Token: 0x04009927 RID: 39207
		protected Image m_LevelIcon_Property;

		// Token: 0x04009928 RID: 39208
		protected Image m_LinkIcon_Property;

		// Token: 0x04009929 RID: 39209
		protected Image m_RankIcon_Property;

		// Token: 0x0400992A RID: 39210
		protected Image m_XyzMatIcon_Property;

		// Token: 0x0400992B RID: 39211
		protected Image m_TypeIcon_Property;

		// Token: 0x0400992C RID: 39212
		protected RectTransform m_TypeGroup_Property;

		// Token: 0x0400992D RID: 39213
		protected RectTransform m_ActivatedGroup_Property;

		// Token: 0x0400992E RID: 39214
		protected Image m_TypeOutline_Property;

		// Token: 0x0400992F RID: 39215
		protected Image m_SpTrTypeIcon_Property;

		// Token: 0x04009930 RID: 39216
		protected Image m_AtkIcon_Property;

		// Token: 0x04009931 RID: 39217
		protected Image m_DefIcon_Property;

		// Token: 0x04009932 RID: 39218
		protected Image m_NameAreaBg_Property;

		// Token: 0x04009933 RID: 39219
		protected Image m_TypeAreaBg_Property;

		// Token: 0x04009934 RID: 39220
		protected Image m_TurnElapsedIcon_Property;

		// Token: 0x04009935 RID: 39221
		protected Image m_ActivatedIcon_Property;

		// Token: 0x04009936 RID: 39222
		protected Image m_ActivatedPlate_Property;

		// Token: 0x04009937 RID: 39223
		protected ExtendedTextMeshProUGUI m_XyzMatNum_Property;

		// Token: 0x04009938 RID: 39224
		protected ExtendedTextMeshProUGUI m_TurnElapsedNum_Property;

		// Token: 0x04009939 RID: 39225
		protected ExtendedTextMeshProUGUI m_PenScaleNum_Property;

		// Token: 0x0400993A RID: 39226
		protected ExtendedTextMeshProUGUI m_LinkNum_Property;

		// Token: 0x0400993B RID: 39227
		protected ExtendedTextMeshProUGUI m_LevelNum_Property;

		// Token: 0x0400993C RID: 39228
		protected ExtendedTextMeshProUGUI m_RankNum_Property;

		// Token: 0x0400993D RID: 39229
		protected ExtendedTextMeshProUGUI m_SpTrType_Property;

		// Token: 0x0400993E RID: 39230
		protected ExtendedTextMeshProUGUI m_AtkValue_Property;

		// Token: 0x0400993F RID: 39231
		protected ExtendedTextMeshProUGUI m_DefValue_Property;

		// Token: 0x04009940 RID: 39232
		protected ExtendedTextMeshProUGUI m_DspTitle_Property;

		// Token: 0x04009941 RID: 39233
		protected ExtendedTextMeshProUGUI m_DspContent_Property;

		// Token: 0x04009942 RID: 39234
		protected SelectionButton m_CardArea_Property;

		// Token: 0x04009943 RID: 39235
		protected SelectionItem m_TextAreaItem_Property;

		// Token: 0x04009944 RID: 39236
		protected RectTransform m_ColorBarTeam0_Property;

		// Token: 0x04009945 RID: 39237
		protected RectTransform m_ColorBarTeam1_Property;

		// Token: 0x04009946 RID: 39238
		public bool AlwaysDisp;

		// Token: 0x04009947 RID: 39239
		public bool EnableAttributeIcon;

		// Token: 0x04009948 RID: 39240
		protected CardInfoData m_CardInfoData;

		// Token: 0x04009949 RID: 39241
		protected CardInfoData m_CardInfoDataOld;

		// Token: 0x0400994A RID: 39242
		protected Dictionary<int, int> m_HighLightEfxTableForHappening;

		// Token: 0x0400994B RID: 39243
		protected Dictionary<int, int> m_HighLightEfxTableForToHappen;

		// Token: 0x0400994C RID: 39244
		protected PopUpTextManager m_PutManager;

		// Token: 0x02000CD2 RID: 3282
		protected enum ValueState
		{
			// Token: 0x0400994E RID: 39246
			NORMAL,
			// Token: 0x0400994F RID: 39247
			CHANGED,
			// Token: 0x04009950 RID: 39248
			UP,
			// Token: 0x04009951 RID: 39249
			DOWN
		}
	}
}
