using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomGame.Card;
using YgomGame.TextIDs;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;
using YgomSystem.YGomTMPro;

namespace YgomGame.Deck
{
	// Token: 0x02000FDE RID: 4062
	public class DeckView : MonoBehaviour
	{
		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x060079C1 RID: 31169 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060079C2 RID: 31170 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isMobileLayout
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x060079C3 RID: 31171 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060079C4 RID: 31172 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isLoading
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x060079C5 RID: 31173 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060079C6 RID: 31174 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isDismantleBatchMode
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x060079C7 RID: 31175 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060079C8 RID: 31176 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isSelected
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x060079C9 RID: 31177 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079CA RID: 31178 RVA: 0x0000216D File Offset: 0x0000036D
		private RectTransform m_MainDeckContent
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x060079CB RID: 31179 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079CC RID: 31180 RVA: 0x0000216D File Offset: 0x0000036D
		private ExtendedTextMeshProUGUI m_MainDeckCardSumText
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x060079CD RID: 31181 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079CE RID: 31182 RVA: 0x0000216D File Offset: 0x0000036D
		private RectTransform m_ExtraDeckContent
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x060079CF RID: 31183 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079D0 RID: 31184 RVA: 0x0000216D File Offset: 0x0000036D
		private ExtendedTextMeshProUGUI m_ExtraDeckCardSumText
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x060079D1 RID: 31185 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079D2 RID: 31186 RVA: 0x0000216D File Offset: 0x0000036D
		private ElementObjectManager m_DismantleBatchEom
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x060079D3 RID: 31187 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079D4 RID: 31188 RVA: 0x0000216D File Offset: 0x0000036D
		private RectTransform m_DismantleBatchContent
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x060079D5 RID: 31189 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079D6 RID: 31190 RVA: 0x0000216D File Offset: 0x0000036D
		private ElementObjectManager m_ScrollEom
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x060079D7 RID: 31191 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079D8 RID: 31192 RVA: 0x0000216D File Offset: 0x0000036D
		private ExtendedScrollRect m_Scroll
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x060079D9 RID: 31193 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079DA RID: 31194 RVA: 0x0000216D File Offset: 0x0000036D
		private InfinityScrollView m_InfinityScroll
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x060079DB RID: 31195 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079DC RID: 31196 RVA: 0x0000216D File Offset: 0x0000036D
		public RectTransform m_ScrollContent
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

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x060079DD RID: 31197 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079DE RID: 31198 RVA: 0x0000216D File Offset: 0x0000036D
		private ElementObjectManager m_DeckNumCounterEom
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x060079DF RID: 31199 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079E0 RID: 31200 RVA: 0x0000216D File Offset: 0x0000036D
		private TMP_Text m_DeckNumCounterTextMain
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x060079E1 RID: 31201 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079E2 RID: 31202 RVA: 0x0000216D File Offset: 0x0000036D
		private TMP_Text m_DeckNumCounterTextExtra
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x060079E3 RID: 31203 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079E4 RID: 31204 RVA: 0x0000216D File Offset: 0x0000036D
		private GameObject m_ScrollBlocker
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x060079E5 RID: 31205 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079E6 RID: 31206 RVA: 0x0000216D File Offset: 0x0000036D
		private SelectionButton m_NoItemButton
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x060079E7 RID: 31207 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079E8 RID: 31208 RVA: 0x0000216D File Offset: 0x0000036D
		private ExtendedTextMeshProUGUI m_NoItemButtonText
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x060079E9 RID: 31209 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079EA RID: 31210 RVA: 0x0000216D File Offset: 0x0000036D
		private RectTransform m_Loading
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x060079EB RID: 31211 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079EC RID: 31212 RVA: 0x0000216D File Offset: 0x0000036D
		private RectTransform m_Viewport
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x060079ED RID: 31213 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079EE RID: 31214 RVA: 0x0000216D File Offset: 0x0000036D
		public DropArea m_DropArea
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

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x060079EF RID: 31215 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079F0 RID: 31216 RVA: 0x0000216D File Offset: 0x0000036D
		private RectTransform m_DropAreaOver
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x060079F1 RID: 31217 RVA: 0x0000216A File Offset: 0x0000036A
		private static Content m_cci
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x060079F2 RID: 31218 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060079F3 RID: 31219 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isPremiumCheckEnable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x060079F4 RID: 31220 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079F5 RID: 31221 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<DeckCard, bool, int> onCreateDeckCardIdx
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x060079F6 RID: 31222 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079F7 RID: 31223 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<DeckCard, bool> onCreateDeckCard
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x060079F8 RID: 31224 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079F9 RID: 31225 RVA: 0x0000216D File Offset: 0x0000036D
		public Action<DeckCard, int> onCreateDeckCard2
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x060079FA RID: 31226 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079FB RID: 31227 RVA: 0x0000216D File Offset: 0x0000036D
		public Action onNoItemButtonSelectedCallback
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x060079FC RID: 31228 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060079FD RID: 31229 RVA: 0x0000216D File Offset: 0x0000036D
		public Action onNoItemButtonInputCallback
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x060079FE RID: 31230 RVA: 0x0000216A File Offset: 0x0000036A
		public List<CardBaseData> dismantleList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060079FF RID: 31231 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDeckName(string s)
		{
		}

		// Token: 0x06007A00 RID: 31232 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEventDeckName(string s)
		{
		}

		// Token: 0x06007A01 RID: 31233 RVA: 0x0000216D File Offset: 0x0000036D
		public void ActivateEventDeckName(bool isActive)
		{
		}

		// Token: 0x06007A02 RID: 31234 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSubmitDeckName(UnityAction<string> callback)
		{
		}

		// Token: 0x06007A03 RID: 31235 RVA: 0x0000216D File Offset: 0x0000036D
		public void ActivateDeckNameInput()
		{
		}

		// Token: 0x06007A04 RID: 31236 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDeckCaseIcon(int deckcaseId)
		{
		}

		// Token: 0x06007A05 RID: 31237 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDeckNameButtonActive(bool b)
		{
		}

		// Token: 0x06007A06 RID: 31238 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDismantlePoolCount()
		{
		}

		// Token: 0x06007A07 RID: 31239 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveMultiDismantleButton(bool active)
		{
		}

		// Token: 0x06007A08 RID: 31240 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCurrentView(bool current)
		{
		}

		// Token: 0x06007A09 RID: 31241 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickMultiDismantleButton(UnityAction callback)
		{
		}

		// Token: 0x06007A0A RID: 31242 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectMultiDismantleButton(UnityAction callback)
		{
		}

		// Token: 0x06007A0B RID: 31243 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnInputRightMultiDismantleButton(UnityAction<SelectionItem> callback)
		{
		}

		// Token: 0x06007A0C RID: 31244 RVA: 0x0000216D File Offset: 0x0000036D
		private void sortMainDeckCards()
		{
		}

		// Token: 0x06007A0D RID: 31245 RVA: 0x0000216D File Offset: 0x0000036D
		private void sortExtraDeckCards()
		{
		}

		// Token: 0x06007A0E RID: 31246 RVA: 0x0000216D File Offset: 0x0000036D
		private void sortDismantlePoolCards()
		{
		}

		// Token: 0x06007A0F RID: 31247 RVA: 0x0000216D File Offset: 0x0000036D
		private void SortbyLoc(DeckCard.LocationInDeck loc)
		{
		}

		// Token: 0x06007A10 RID: 31248 RVA: 0x0000216D File Offset: 0x0000036D
		public void SortInDeckCards()
		{
		}

		// Token: 0x06007A11 RID: 31249 RVA: 0x0000216D File Offset: 0x0000036D
		public void SortInDeckCards(SortComparer.Sorter sorter)
		{
		}

		// Token: 0x06007A12 RID: 31250 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeDeckContents()
		{
		}

		// Token: 0x06007A13 RID: 31251 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06007A14 RID: 31252 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitMobileLayout()
		{
		}

		// Token: 0x06007A15 RID: 31253 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitilaizeConsoleElements()
		{
		}

		// Token: 0x06007A16 RID: 31254 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06007A17 RID: 31255 RVA: 0x0000216A File Offset: 0x0000036A
		private DeckCard InstantiateDeckCard(CardBaseData data, RectTransform parent, int regID, DeckEditViewController2.DisplayMode mode = DeckEditViewController2.DisplayMode.Simple)
		{
			return null;
		}

		// Token: 0x06007A18 RID: 31256 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float calculateSpacing(float length, float width, float columns)
		{
			return 0f;
		}

		// Token: 0x06007A19 RID: 31257 RVA: 0x000029CC File Offset: 0x00000BCC
		private int getSizeOfColumn(Vector2 aspect, int num, int minSum)
		{
			return 0;
		}

		// Token: 0x06007A1A RID: 31258 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetCurrentColumnNum(DeckCard.LocationInDeck loc)
		{
			return 0;
		}

		// Token: 0x06007A1B RID: 31259 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetIndexInDeck(DeckCard card, DeckCard.LocationInDeck loc)
		{
			return 0;
		}

		// Token: 0x06007A1C RID: 31260 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetIndexInDeck(DeckCard card)
		{
			return 0;
		}

		// Token: 0x06007A1D RID: 31261 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsLeftmostCard(DeckCard card, DeckCard.LocationInDeck loc)
		{
			return false;
		}

		// Token: 0x06007A1E RID: 31262 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsRightmostCard(DeckCard card, DeckCard.LocationInDeck loc)
		{
			return false;
		}

		// Token: 0x06007A1F RID: 31263 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsBottommostCard(DeckCard card, DeckCard.LocationInDeck loc)
		{
			return false;
		}

		// Token: 0x06007A20 RID: 31264 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsTopmostCard(DeckCard card, DeckCard.LocationInDeck loc)
		{
			return false;
		}

		// Token: 0x06007A21 RID: 31265 RVA: 0x0000216A File Offset: 0x0000036A
		private DeckCard GetCardAt(int index, DeckCard.LocationInDeck loc)
		{
			return null;
		}

		// Token: 0x06007A22 RID: 31266 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetCardNum(DeckCard.LocationInDeck loc)
		{
			return 0;
		}

		// Token: 0x06007A23 RID: 31267 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetCardSumInDeck(int cardID)
		{
			return 0;
		}

		// Token: 0x06007A24 RID: 31268 RVA: 0x0000216D File Offset: 0x0000036D
		private void setMainDeckGridSpacing()
		{
		}

		// Token: 0x06007A25 RID: 31269 RVA: 0x0000216D File Offset: 0x0000036D
		private void setExtraDeckGridSpacing()
		{
		}

		// Token: 0x06007A26 RID: 31270 RVA: 0x0000216D File Offset: 0x0000036D
		private void setCardSum(DeckCard.LocationInDeck loc)
		{
		}

		// Token: 0x06007A27 RID: 31271 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetInDeckCardSum()
		{
		}

		// Token: 0x06007A28 RID: 31272 RVA: 0x000029CC File Offset: 0x00000BCC
		public DeckView.AddableType GetAddableType(int cardID, int regulation)
		{
			return DeckView.AddableType.Addable;
		}

		// Token: 0x06007A29 RID: 31273 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsAddable(int cardID, int regulation)
		{
			return false;
		}

		// Token: 0x06007A2A RID: 31274 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool CheckNumLimit(int regulation)
		{
			return false;
		}

		// Token: 0x06007A2B RID: 31275 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCard AddToDeck(int id, int prem = 1, bool owned = true, bool isRental = false, int reg = -1, bool sort = true, bool isIni = false, DeckCard.LocationInDeck location = DeckCard.LocationInDeck.NA, bool noAdd = false)
		{
			return null;
		}

		// Token: 0x06007A2C RID: 31276 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsRemovable(int id, int premiumID)
		{
			return false;
		}

		// Token: 0x06007A2D RID: 31277 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDeckNumCounter()
		{
		}

		// Token: 0x06007A2E RID: 31278 RVA: 0x0000216A File Offset: 0x0000036A
		private List<DeckCard> getTargetCardsInDeck(int cardID)
		{
			return null;
		}

		// Token: 0x06007A2F RID: 31279 RVA: 0x0000216A File Offset: 0x0000036A
		private List<DeckCard> getTargetCardsInDeck(int cardID, int premiumID, bool isRental = false)
		{
			return null;
		}

		// Token: 0x06007A30 RID: 31280 RVA: 0x0000216A File Offset: 0x0000036A
		private List<DeckCard> getTargetCardsInDismantlePool(CardBaseData data)
		{
			return null;
		}

		// Token: 0x06007A31 RID: 31281 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispNoItemButton(bool disp)
		{
		}

		// Token: 0x06007A32 RID: 31282 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isMainDeckFull()
		{
			return false;
		}

		// Token: 0x06007A33 RID: 31283 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isExtraDeckFull()
		{
			return false;
		}

		// Token: 0x06007A34 RID: 31284 RVA: 0x000F6674 File Offset: 0x000F4874
		public ValueTuple<CardCollectionInfo.Premium, bool, bool> GetLowPremiumInDeck(int cardID)
		{
			return default(ValueTuple<CardCollectionInfo.Premium, bool, bool>);
		}

		// Token: 0x06007A35 RID: 31285 RVA: 0x0000216A File Offset: 0x0000036A
		public List<CardBaseData> GetInDeckCards(DeckInfo.DeckType type)
		{
			return null;
		}

		// Token: 0x06007A36 RID: 31286 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetCardTotalInDeck(DeckInfo.DeckType type)
		{
			return 0;
		}

		// Token: 0x06007A37 RID: 31287 RVA: 0x0000216A File Offset: 0x0000036A
		public List<CardBaseData> GetInDeckAll()
		{
			return null;
		}

		// Token: 0x06007A38 RID: 31288 RVA: 0x0000216A File Offset: 0x0000036A
		public List<DeckCard> GetDeckCards()
		{
			return null;
		}

		// Token: 0x06007A39 RID: 31289 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetPremiumCardSumInDeck(int id, CardCollectionInfo.Premium premiumID, bool isRental = false)
		{
			return 0;
		}

		// Token: 0x06007A3A RID: 31290 RVA: 0x0000216A File Offset: 0x0000036A
		public InDeckNumInfo GetInDeckInfo(int cardID)
		{
			return null;
		}

		// Token: 0x06007A3B RID: 31291 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetAlterCardSumInDeck(int id, CardCollectionInfo.Premium premiumID, bool isRental = false)
		{
			return 0;
		}

		// Token: 0x06007A3C RID: 31292 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetPremiumCardSumInDismantlePool(CardBaseData data)
		{
			return 0;
		}

		// Token: 0x06007A3D RID: 31293 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsContainsUncraftedCard()
		{
			return false;
		}

		// Token: 0x06007A3E RID: 31294 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateIsOwned(bool styleFilling = true)
		{
		}

		// Token: 0x06007A3F RID: 31295 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateIsOwned(int cardID, bool isRental, bool styleFilling = true)
		{
		}

		// Token: 0x06007A40 RID: 31296 RVA: 0x000F668C File Offset: 0x000F488C
		private CardBaseData UpdateIsOwned(CardBaseData baseData, ref int numN, ref int numP1, ref int numP2, bool styleFilling = true)
		{
			return default(CardBaseData);
		}

		// Token: 0x06007A41 RID: 31297 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsModified(List<CardBaseData> oldMainCards, List<CardBaseData> oldExtraCards)
		{
			return false;
		}

		// Token: 0x06007A42 RID: 31298 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRegulation(int regulationID)
		{
		}

		// Token: 0x06007A43 RID: 31299 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRentalID(int rentalID)
		{
		}

		// Token: 0x06007A44 RID: 31300 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateRegulation(int regulationID)
		{
		}

		// Token: 0x06007A45 RID: 31301 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDisplayMode(DeckEditViewController2.DisplayMode mode, bool updateScroll = true)
		{
		}

		// Token: 0x06007A46 RID: 31302 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDisplayModeForDeckBrowser(bool regulationVisible, int regulationID, bool rarityVisble, bool hasCard)
		{
		}

		// Token: 0x06007A47 RID: 31303 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateDeckCard(DeckCard deckCard, bool isMonocro)
		{
		}

		// Token: 0x06007A48 RID: 31304 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateView(bool updateDataCount)
		{
		}

		// Token: 0x06007A49 RID: 31305 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopScroll()
		{
		}

		// Token: 0x06007A4A RID: 31306 RVA: 0x0000216A File Offset: 0x0000036A
		private DeckCard GetHeadDeckCard(DeckCard.LocationInDeck loc)
		{
			return null;
		}

		// Token: 0x06007A4B RID: 31307 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetDefaultFocusItem()
		{
			return null;
		}

		// Token: 0x06007A4C RID: 31308 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectDefaultFocusedItem()
		{
		}

		// Token: 0x06007A4D RID: 31309 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCurrentCard(DeckCard card)
		{
		}

		// Token: 0x06007A4E RID: 31310 RVA: 0x0000216D File Offset: 0x0000036D
		public void CursorJumpUp()
		{
		}

		// Token: 0x06007A4F RID: 31311 RVA: 0x0000216D File Offset: 0x0000036D
		public void CursorJumpDown()
		{
		}

		// Token: 0x06007A50 RID: 31312 RVA: 0x0000216D File Offset: 0x0000036D
		public void CursorJumpLeft()
		{
		}

		// Token: 0x06007A51 RID: 31313 RVA: 0x0000216D File Offset: 0x0000036D
		public void CursorJumpRight()
		{
		}

		// Token: 0x06007A52 RID: 31314 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsTailCard(DeckCard card, DeckCard.LocationInDeck location)
		{
			return false;
		}

		// Token: 0x06007A53 RID: 31315 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectRightEdgeClosestItem(Vector2 screenPoint, float angleDot)
		{
		}

		// Token: 0x06007A54 RID: 31316 RVA: 0x0000216A File Offset: 0x0000036A
		private List<DeckCard> GetRightEdgeCards(List<DeckCard> cardList, DeckCard.LocationInDeck loc)
		{
			return null;
		}

		// Token: 0x06007A55 RID: 31317 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectTopEdgeItem(Vector2 screenPoint, float angleDot)
		{
		}

		// Token: 0x06007A56 RID: 31318 RVA: 0x0000216A File Offset: 0x0000036A
		private List<DeckCard> GetTopEdgeCards(List<DeckCard> cardList, DeckCard.LocationInDeck loc)
		{
			return null;
		}

		// Token: 0x06007A57 RID: 31319 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCreateEntity(GameObject obj)
		{
		}

		// Token: 0x06007A58 RID: 31320 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnUpdateEntity(GameObject obj, int idx)
		{
		}

		// Token: 0x06007A59 RID: 31321 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveScroll(bool condition)
		{
		}

		// Token: 0x06007A5A RID: 31322 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator waitDecrementDragCounter()
		{
			return null;
		}

		// Token: 0x06007A5B RID: 31323 RVA: 0x000F66A4 File Offset: 0x000F48A4
		private ValueTuple<int, int> GetIndexMainDeckCard(List<CardBaseData> cardList, int cardID, int premiumID)
		{
			return default(ValueTuple<int, int>);
		}

		// Token: 0x06007A5C RID: 31324 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetMaxCol(int col)
		{
		}

		// Token: 0x06007A5D RID: 31325 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispDropAreaOver(bool disp)
		{
		}

		// Token: 0x06007A5E RID: 31326 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeDismantlePool()
		{
		}

		// Token: 0x06007A5F RID: 31327 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDismantleMode(bool b)
		{
		}

		// Token: 0x06007A60 RID: 31328 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowLoading()
		{
		}

		// Token: 0x06007A61 RID: 31329 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideLoading()
		{
		}

		// Token: 0x06007A62 RID: 31330 RVA: 0x0000216D File Offset: 0x0000036D
		public void PostMultiDismantle()
		{
		}

		// Token: 0x06007A63 RID: 31331 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearAllCards()
		{
		}

		// Token: 0x06007A64 RID: 31332 RVA: 0x0000216A File Offset: 0x0000036A
		private List<CardBaseData> GetDataList(DeckCard.LocationInDeck loc)
		{
			return null;
		}

		// Token: 0x06007A65 RID: 31333 RVA: 0x000029CC File Offset: 0x00000BCC
		private DeckView.DeckContentWidget.DeckType GetDeckType(DeckCard.LocationInDeck loc)
		{
			return DeckView.DeckContentWidget.DeckType.Main;
		}

		// Token: 0x06007A66 RID: 31334 RVA: 0x0000216A File Offset: 0x0000036A
		private List<DeckCard> GetCards(DeckCard.LocationInDeck loc)
		{
			return null;
		}

		// Token: 0x06007A67 RID: 31335 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform GetContent(DeckCard.LocationInDeck loc)
		{
			return null;
		}

		// Token: 0x06007A68 RID: 31336 RVA: 0x0000216A File Offset: 0x0000036A
		private DeckCard AddCard(CardBaseData cbd, DeckCard.LocationInDeck loc, int reg, bool sort = true, bool isIni = false, bool noAdd = false)
		{
			return null;
		}

		// Token: 0x06007A69 RID: 31337 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCard AddToMainDeckByID(int id, int prem = 1, bool owned = true, bool isRental = false, int reg = -1, bool sort = true, bool isIni = false, bool noAdd = false)
		{
			return null;
		}

		// Token: 0x06007A6A RID: 31338 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCard AddToExtraDeckByID(int id, int prem = 1, bool owned = true, bool isRental = false, int reg = -1, bool sort = true, bool isIni = false, bool noAdd = false)
		{
			return null;
		}

		// Token: 0x06007A6B RID: 31339 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCard AddToDismantlePool(CardBaseData cbd, int reg = -1, bool sort = true)
		{
			return null;
		}

		// Token: 0x06007A6C RID: 31340 RVA: 0x0000216A File Offset: 0x0000036A
		private DeckCard RemoveCard(CardBaseData cbd, DeckCard.LocationInDeck loc, bool sort = true)
		{
			return null;
		}

		// Token: 0x06007A6D RID: 31341 RVA: 0x000F5DAA File Offset: 0x000F3FAA
		public void RemoveCardFromMainOrExtra(int id, int premiumID, out DeckCard removedCard)
		{
			removedCard = null;
		}

		// Token: 0x06007A6E RID: 31342 RVA: 0x000F16E9 File Offset: 0x000EF8E9
		public void RemoveFromDismantlePool(CardBaseData cbd, out DeckCard removedCard, bool sort = true)
		{
			removedCard = null;
		}

		// Token: 0x0400B127 RID: 45351
		private float removedWait;

		// Token: 0x0400B128 RID: 45352
		private ElementObjectManager m_Eom;

		// Token: 0x0400B129 RID: 45353
		private const string k_ELabelHeaderArea = "HeaderArea";

		// Token: 0x0400B12A RID: 45354
		private DeckView.HeaderArea m_HeaderArea;

		// Token: 0x0400B12B RID: 45355
		private ElementObjectManager m_MainDeckEom;

		// Token: 0x0400B12C RID: 45356
		private ElementObjectManager m_ExtraDeckEom;

		// Token: 0x0400B12D RID: 45357
		private RectTransform m_SelectedWindowCursor;

		// Token: 0x0400B12E RID: 45358
		private const string k_ELabelMobileScroll = "Scroll";

		// Token: 0x0400B12F RID: 45359
		private bool isScrollReady;

		// Token: 0x0400B130 RID: 45360
		private const string k_ELabelDeckNumCounter = "DeckNumCounter";

		// Token: 0x0400B131 RID: 45361
		private List<DeckCard> mainDeckCards;

		// Token: 0x0400B132 RID: 45362
		private List<DeckCard> extraDeckCards;

		// Token: 0x0400B133 RID: 45363
		private List<DeckCard> dismantlePoolCards;

		// Token: 0x0400B134 RID: 45364
		private const int MAX_CARDS_MAIN = 65;

		// Token: 0x0400B135 RID: 45365
		private const int THRESHOLD_CARDS_MAIN = 50;

		// Token: 0x0400B136 RID: 45366
		private const int DEFAULT_ROWS_MAIN = 5;

		// Token: 0x0400B137 RID: 45367
		private const int DEFAULT_COLUMNS_MAIN = 10;

		// Token: 0x0400B138 RID: 45368
		private const int MIN_COLUMNS_MAIN = 15;

		// Token: 0x0400B139 RID: 45369
		private const int MAX_COLUMNS_MAIN = 15;

		// Token: 0x0400B13A RID: 45370
		private const int MAX_CARDS_EX = 20;

		// Token: 0x0400B13B RID: 45371
		private const int THRESHOLD_CARDS_EX = 20;

		// Token: 0x0400B13C RID: 45372
		private const int DEFAULT_ROWS_EX = 2;

		// Token: 0x0400B13D RID: 45373
		private const int DEFAULT_COLUMNS_EX = 10;

		// Token: 0x0400B13E RID: 45374
		private const int MIN_COLUMNS_EX = 20;

		// Token: 0x0400B13F RID: 45375
		private const int MAX_COLUMNS_EX = 15;

		// Token: 0x0400B140 RID: 45376
		private const int MAX_CARDS_DISMANTLE_POOL = 60;

		// Token: 0x0400B141 RID: 45377
		private const int MAX_CARDS_DECKCONTENT_CARDGROUP = 5;

		// Token: 0x0400B142 RID: 45378
		public static int m_MaxCol;

		// Token: 0x0400B143 RID: 45379
		private bool m_RegulationVisible;

		// Token: 0x0400B144 RID: 45380
		private int m_RegulationID;

		// Token: 0x0400B145 RID: 45381
		private int m_RentalCardID;

		// Token: 0x0400B146 RID: 45382
		private bool m_RarityVisble;

		// Token: 0x0400B147 RID: 45383
		private bool m_MonochromeEnable;

		// Token: 0x0400B148 RID: 45384
		private bool m_PremiumCheckEnable;

		// Token: 0x0400B149 RID: 45385
		private Vector2Int AspectRatioMainDeck;

		// Token: 0x0400B14A RID: 45386
		private Vector2Int AspectRatioExDeck;

		// Token: 0x0400B14B RID: 45387
		private DeckCard template_DeckCard;

		// Token: 0x0400B14C RID: 45388
		private SortComparer.Sorter deckSorter;

		// Token: 0x0400B14D RID: 45389
		private DeckCard currentCard;

		// Token: 0x0400B14E RID: 45390
		private DeckView.DeckContentWidget deckContents;

		// Token: 0x0400B14F RID: 45391
		public List<CardBaseData> mainCardDataList;

		// Token: 0x0400B150 RID: 45392
		public List<CardBaseData> extraCardDataList;

		// Token: 0x0400B151 RID: 45393
		private int currentSelectIndex;

		// Token: 0x0400B152 RID: 45394
		private DeckEditViewController2.DisplayMode displayMode;

		// Token: 0x0400B153 RID: 45395
		private EntityPoolController entityPoolController;

		// Token: 0x0400B154 RID: 45396
		private List<CardBaseData> dismantlePoolDataList;

		// Token: 0x0400B155 RID: 45397
		private SortComparer.Sorter dismantlePoolSorter;

		// Token: 0x02000FDF RID: 4063
		private class HeaderArea : MonoBehaviour
		{
			// Token: 0x17000F7F RID: 3967
			// (get) Token: 0x06007A70 RID: 31344 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isIni
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F80 RID: 3968
			// (get) Token: 0x06007A71 RID: 31345 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007A72 RID: 31346 RVA: 0x0000216D File Offset: 0x0000036D
			public InputFieldWidget m_DeckNameInput
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

			// Token: 0x17000F81 RID: 3969
			// (get) Token: 0x06007A73 RID: 31347 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007A74 RID: 31348 RVA: 0x0000216D File Offset: 0x0000036D
			public Image m_DeckIcon
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

			// Token: 0x17000F82 RID: 3970
			// (get) Token: 0x06007A75 RID: 31349 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007A76 RID: 31350 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedTextMeshProUGUI m_EventDeckName
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

			// Token: 0x17000F83 RID: 3971
			// (get) Token: 0x06007A77 RID: 31351 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007A78 RID: 31352 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedTextMeshProUGUI m_DismantleBatchCount
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

			// Token: 0x17000F84 RID: 3972
			// (get) Token: 0x06007A79 RID: 31353 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007A7A RID: 31354 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_DismantleBatchButton
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

			// Token: 0x17000F85 RID: 3973
			// (get) Token: 0x06007A7B RID: 31355 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007A7C RID: 31356 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedTextMeshProUGUI m_DismantleCardTitle
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

			// Token: 0x17000F86 RID: 3974
			// (get) Token: 0x06007A7D RID: 31357 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007A7E RID: 31358 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_DismantleResetButton
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

			// Token: 0x17000F87 RID: 3975
			// (get) Token: 0x06007A7F RID: 31359 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007A80 RID: 31360 RVA: 0x0000216D File Offset: 0x0000036D
			public DeviceIcon m_DismantleBatchButtonShortcutIcon
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

			// Token: 0x17000F88 RID: 3976
			// (get) Token: 0x06007A81 RID: 31361 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007A82 RID: 31362 RVA: 0x0000216D File Offset: 0x0000036D
			public UnityAction<string> onSubmitDeckNameAction
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000F89 RID: 3977
			// (get) Token: 0x06007A83 RID: 31363 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007A84 RID: 31364 RVA: 0x0000216D File Offset: 0x0000036D
			public UnityAction onClickMultiDismantle
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000F8A RID: 3978
			// (get) Token: 0x06007A85 RID: 31365 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007A86 RID: 31366 RVA: 0x0000216D File Offset: 0x0000036D
			public UnityAction onSelectMultiDismantle
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000F8B RID: 3979
			// (get) Token: 0x06007A87 RID: 31367 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007A88 RID: 31368 RVA: 0x0000216D File Offset: 0x0000036D
			public UnityAction<SelectionItem> onInputRightMultiDismantle
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x17000F8C RID: 3980
			// (get) Token: 0x06007A89 RID: 31369 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007A8A RID: 31370 RVA: 0x0000216D File Offset: 0x0000036D
			public UnityAction<SelectionItem> onInputDownMultiDismantle
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x06007A8B RID: 31371 RVA: 0x0000216D File Offset: 0x0000036D
			private void Awake()
			{
			}

			// Token: 0x06007A8C RID: 31372 RVA: 0x0000216D File Offset: 0x0000036D
			public void InitializeElements()
			{
			}

			// Token: 0x06007A8D RID: 31373 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitilaizeDeckNameElements()
			{
			}

			// Token: 0x06007A8E RID: 31374 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetDeckName(string str)
			{
			}

			// Token: 0x06007A8F RID: 31375 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetEventDeckName(string str)
			{
			}

			// Token: 0x06007A90 RID: 31376 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetDeckCaseIcon(int deckcaseId)
			{
			}

			// Token: 0x06007A91 RID: 31377 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeDismantleElements()
			{
			}

			// Token: 0x06007A92 RID: 31378 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetDismantleCount(int count)
			{
			}

			// Token: 0x06007A93 RID: 31379 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetActiveDismantleButton(bool active)
			{
			}

			// Token: 0x06007A94 RID: 31380 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetDismantleMode(bool isDismantleMode)
			{
			}

			// Token: 0x0400B156 RID: 45398
			private ElementObjectManager m_Eom;

			// Token: 0x0400B157 RID: 45399
			private bool isInitialized;
		}

		// Token: 0x02000FE0 RID: 4064
		public enum AddableType
		{
			// Token: 0x0400B159 RID: 45401
			Addable,
			// Token: 0x0400B15A RID: 45402
			OverCapacity,
			// Token: 0x0400B15B RID: 45403
			OverCardNum,
			// Token: 0x0400B15C RID: 45404
			OverLimit0,
			// Token: 0x0400B15D RID: 45405
			OverLimit1,
			// Token: 0x0400B15E RID: 45406
			OverLimit2
		}

		// Token: 0x02000FE1 RID: 4065
		private class DeckContentWidget
		{
			// Token: 0x17000F8D RID: 3981
			// (get) Token: 0x06007A96 RID: 31382 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007A97 RID: 31383 RVA: 0x0000216D File Offset: 0x0000036D
			public List<int> templateIndexer
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

			// Token: 0x06007A98 RID: 31384 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetCardGroupHeadIndex(DeckView.DeckContentWidget.DeckType deckType)
			{
				return 0;
			}

			// Token: 0x06007A99 RID: 31385 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetCardGroupTailIndex(DeckView.DeckContentWidget.DeckType deckType)
			{
				return 0;
			}

			// Token: 0x06007A9A RID: 31386 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddCardGroup(DeckView.DeckContentWidget.DeckType deckType)
			{
			}

			// Token: 0x06007A9B RID: 31387 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddCardGroup(DeckView.DeckContentWidget.DeckType deckType, int index)
			{
			}

			// Token: 0x06007A9C RID: 31388 RVA: 0x0000216D File Offset: 0x0000036D
			public void AddHeader(DeckView.DeckContentWidget.DeckType deckType, IDS_DECKEDIT title, int index)
			{
			}

			// Token: 0x06007A9D RID: 31389 RVA: 0x0000216D File Offset: 0x0000036D
			public void RemoveHeader(DeckView.DeckContentWidget.DeckType deckType)
			{
			}

			// Token: 0x06007A9E RID: 31390 RVA: 0x0000216D File Offset: 0x0000036D
			private void AddDeckContent(DeckView.DeckContentWidget.DeckContent deckContent, int index)
			{
			}

			// Token: 0x06007A9F RID: 31391 RVA: 0x0000216D File Offset: 0x0000036D
			public void RemoveCardGroup(DeckView.DeckContentWidget.DeckType deckType)
			{
			}

			// Token: 0x06007AA0 RID: 31392 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetMaxCardNum(DeckView.DeckContentWidget.DeckType deckType)
			{
				return 0;
			}

			// Token: 0x06007AA1 RID: 31393 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetContentNum()
			{
				return 0;
			}

			// Token: 0x06007AA2 RID: 31394 RVA: 0x000F66BC File Offset: 0x000F48BC
			public ValueTuple<int, int> GetDisplayCardIndex(int index)
			{
				return default(ValueTuple<int, int>);
			}

			// Token: 0x06007AA3 RID: 31395 RVA: 0x000029CC File Offset: 0x00000BCC
			public DeckView.DeckContentWidget.Type GetContentType(int index)
			{
				return DeckView.DeckContentWidget.Type.Header;
			}

			// Token: 0x06007AA4 RID: 31396 RVA: 0x000029CC File Offset: 0x00000BCC
			public DeckView.DeckContentWidget.DeckType GetContentDeckType(int index)
			{
				return DeckView.DeckContentWidget.DeckType.Main;
			}

			// Token: 0x06007AA5 RID: 31397 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetHeaderTitle(int index)
			{
				return null;
			}

			// Token: 0x06007AA6 RID: 31398 RVA: 0x0000216D File Offset: 0x0000036D
			public void ClearContent()
			{
			}

			// Token: 0x0400B15F RID: 45407
			private List<DeckView.DeckContentWidget.DeckContent> deckContents;

			// Token: 0x02000FE2 RID: 4066
			public enum Type
			{
				// Token: 0x0400B161 RID: 45409
				Header,
				// Token: 0x0400B162 RID: 45410
				CardGroup
			}

			// Token: 0x02000FE3 RID: 4067
			public enum DeckType
			{
				// Token: 0x0400B164 RID: 45412
				Main,
				// Token: 0x0400B165 RID: 45413
				Extra,
				// Token: 0x0400B166 RID: 45414
				Dismantle
			}

			// Token: 0x02000FE4 RID: 4068
			private class DeckContent
			{
				// Token: 0x06007AA8 RID: 31400 RVA: 0x0000216A File Offset: 0x0000036A
				public static DeckView.DeckContentWidget.DeckContent CreateHeader(DeckView.DeckContentWidget.DeckType deckType, IDS_DECKEDIT title)
				{
					return null;
				}

				// Token: 0x06007AA9 RID: 31401 RVA: 0x0000216A File Offset: 0x0000036A
				public static DeckView.DeckContentWidget.DeckContent CreateCardGroup(DeckView.DeckContentWidget.DeckType deckType)
				{
					return null;
				}

				// Token: 0x06007AAA RID: 31402 RVA: 0x000029CC File Offset: 0x00000BCC
				public bool CheckType(DeckView.DeckContentWidget.DeckType deckType, DeckView.DeckContentWidget.Type type)
				{
					return false;
				}

				// Token: 0x06007AAB RID: 31403 RVA: 0x000029CC File Offset: 0x00000BCC
				public int GetTemplateIndex()
				{
					return 0;
				}

				// Token: 0x0400B167 RID: 45415
				public DeckView.DeckContentWidget.Type type;

				// Token: 0x0400B168 RID: 45416
				public DeckView.DeckContentWidget.DeckType deckType;

				// Token: 0x0400B169 RID: 45417
				public string headerTitle;
			}
		}
	}
}
