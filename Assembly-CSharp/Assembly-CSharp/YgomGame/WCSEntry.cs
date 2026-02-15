using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame
{
	// Token: 0x020007E8 RID: 2024
	public class WCSEntry : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06003ECC RID: 16076 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeElement()
		{
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializePlayers()
		{
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeShareCards()
		{
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenCautiun()
		{
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClick(int index, int slot, DeckSelectViewController2.DeckReference deckRef)
		{
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenConfirmBrowser(int index, int slot, DeckSelectViewController2.DeckReference deckRef)
		{
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x0000216D File Offset: 0x0000036D
		private void SaveShareCard(int index, int cardID, bool isFirst = false)
		{
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetShareCards()
		{
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool CheckShareCard(int cardID)
		{
			return false;
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckRegulation()
		{
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x0000216D File Offset: 0x0000036D
		private void Entry()
		{
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetEntryData()
		{
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenRegErrorDialogue(Dictionary<string, object> overLimitDic, Action action = null)
		{
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenMyDeck(int index, int slot)
		{
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenNeuronDeckSearch(int index, int slot, bool isFirst = false)
		{
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x0000216D File Offset: 0x0000036D
		public void GetNeuronToken(int index, int slot)
		{
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x040037BB RID: 14267
		private bool m_firstFocusPassed;

		// Token: 0x040037BC RID: 14268
		private ElementObjectManager m_ShareCardEom;

		// Token: 0x040037BD RID: 14269
		private static string k_ELabelButtonCaution;

		// Token: 0x040037BE RID: 14270
		private static string k_ELabelTextDate;

		// Token: 0x040037BF RID: 14271
		private static string k_ELabelButtonCheckRegulation;

		// Token: 0x040037C0 RID: 14272
		private static string k_ELabelButtonEntry;

		// Token: 0x040037C1 RID: 14273
		private static string k_ELabelShareCards;

		// Token: 0x040037C2 RID: 14274
		private static string k_ELabelImageCard;

		// Token: 0x040037C3 RID: 14275
		private int m_wcsId;

		// Token: 0x040037C4 RID: 14276
		private bool m_IsRegistered;

		// Token: 0x040037C5 RID: 14277
		private bool m_IsOverDate;

		// Token: 0x040037C6 RID: 14278
		private SelectionButton m_ButtonCaution;

		// Token: 0x040037C7 RID: 14279
		private SelectionButton m_ButtonCheckRegulation;

		// Token: 0x040037C8 RID: 14280
		private SelectionButton m_ButtonEntry;

		// Token: 0x040037C9 RID: 14281
		private ExtendedTextMeshProUGUI m_ButtonText;

		// Token: 0x040037CA RID: 14282
		private SelectionButton m_ButtonResetShareCards;

		// Token: 0x040037CB RID: 14283
		private ExtendedTextMeshProUGUI m_HeaderText;

		// Token: 0x040037CC RID: 14284
		private List<Tween> m_HideTween;

		// Token: 0x040037CD RID: 14285
		private List<Tween> m_ShowTween;

		// Token: 0x040037CE RID: 14286
		private List<WCSEntry.Player> m_Players;

		// Token: 0x040037CF RID: 14287
		private List<WCSEntry.ShareCard> m_ShareCards;

		// Token: 0x040037D0 RID: 14288
		private string actionSheetName;

		// Token: 0x040037D1 RID: 14289
		private List<string> m_DeckActionDialogButtonLabels;

		// Token: 0x040037D2 RID: 14290
		private Dictionary<string, Action<int, int>> m_DeckActionDialogCallBacks;

		// Token: 0x020007E9 RID: 2025
		private class DeckObject
		{
			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x06003EE2 RID: 16098 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06003EE3 RID: 16099 RVA: 0x0000216D File Offset: 0x0000036D
			public bool Usable
			{
				get
				{
					return false;
				}
				private set
				{
				}
			}

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x06003EE4 RID: 16100 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06003EE5 RID: 16101 RVA: 0x0000216D File Offset: 0x0000036D
			private bool IsSet
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x170004D0 RID: 1232
			// (set) Token: 0x06003EE6 RID: 16102 RVA: 0x0000216D File Offset: 0x0000036D
			public UnityAction OnClick
			{
				set
				{
				}
			}

			// Token: 0x06003EE7 RID: 16103 RVA: 0x00002739 File Offset: 0x00000939
			public DeckObject(ElementObjectManager eom, DeckSelectViewController2.DeckReference reference, int wcsId, int slot, int memIdx)
			{
			}

			// Token: 0x06003EE8 RID: 16104 RVA: 0x0000216D File Offset: 0x0000036D
			public void UpdateDeckData()
			{
			}

			// Token: 0x06003EE9 RID: 16105 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsContainCard(int shareCardId)
			{
				return false;
			}

			// Token: 0x040037D3 RID: 14291
			private ElementObjectManager Eom;

			// Token: 0x040037D4 RID: 14292
			public DeckSelectViewController2.DeckReference deckRef;

			// Token: 0x040037D5 RID: 14293
			private int wcsID;

			// Token: 0x040037D6 RID: 14294
			private int memberIdx;

			// Token: 0x040037D7 RID: 14295
			public int slotNum;

			// Token: 0x040037D8 RID: 14296
			private bool valid;

			// Token: 0x040037D9 RID: 14297
			private List<int> cardIDs;

			// Token: 0x040037DA RID: 14298
			private ElementObjectManager body;

			// Token: 0x040037DB RID: 14299
			private GameObject tornamentGroup;

			// Token: 0x040037DC RID: 14300
			private GameObject iconAddDeck;

			// Token: 0x040037DD RID: 14301
			private ExtendedTextMeshProUGUI textBaseDeckName;

			// Token: 0x040037DE RID: 14302
			private SelectionButton button;
		}

		// Token: 0x020007EA RID: 2026
		private class Player
		{
			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x06003EEA RID: 16106 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06003EEB RID: 16107 RVA: 0x0000216D File Offset: 0x0000036D
			public string name
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			// Token: 0x170004D2 RID: 1234
			// (get) Token: 0x06003EEC RID: 16108 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06003EED RID: 16109 RVA: 0x0000216D File Offset: 0x0000036D
			public string id
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			// Token: 0x170004D3 RID: 1235
			// (get) Token: 0x06003EEE RID: 16110 RVA: 0x0000216A File Offset: 0x0000036A
			public List<WCSEntry.DeckObject> Decks
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170004D4 RID: 1236
			// (set) Token: 0x06003EEF RID: 16111 RVA: 0x0000216D File Offset: 0x0000036D
			public Action<int, int, DeckSelectViewController2.DeckReference> deckClickAction
			{
				set
				{
				}
			}

			// Token: 0x06003EF0 RID: 16112 RVA: 0x00002739 File Offset: 0x00000939
			public Player(ElementObjectManager eom, int idx, int wcsId)
			{
			}

			// Token: 0x06003EF1 RID: 16113 RVA: 0x0000216A File Offset: 0x0000036A
			private DeckSelectViewController2.DeckReference GetDeckReference(int slot)
			{
				return null;
			}

			// Token: 0x06003EF2 RID: 16114 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsContainInDecks(int cardID)
			{
				return false;
			}

			// Token: 0x040037DF RID: 14303
			public int index;

			// Token: 0x040037E0 RID: 14304
			private List<WCSEntry.DeckObject> decks;

			// Token: 0x040037E1 RID: 14305
			private ElementObjectManager playerEom;

			// Token: 0x040037E2 RID: 14306
			private ExtendedTextMeshProUGUI nameText;

			// Token: 0x040037E3 RID: 14307
			private ExtendedTextMeshProUGUI IdText;
		}

		// Token: 0x020007EB RID: 2027
		private class ShareCard
		{
			// Token: 0x170004D5 RID: 1237
			// (set) Token: 0x06003EF3 RID: 16115 RVA: 0x0000216D File Offset: 0x0000036D
			public Action<int, int> SaveShareCards
			{
				set
				{
				}
			}

			// Token: 0x06003EF4 RID: 16116 RVA: 0x00002739 File Offset: 0x00000939
			public ShareCard(ElementObjectManager manager, int index, bool isSelectable)
			{
			}

			// Token: 0x06003EF5 RID: 16117 RVA: 0x0000216D File Offset: 0x0000036D
			private void onClick()
			{
			}

			// Token: 0x06003EF6 RID: 16118 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetShareCard(int id)
			{
			}

			// Token: 0x040037E4 RID: 14308
			private int index;

			// Token: 0x040037E5 RID: 14309
			private ElementObjectManager eom;

			// Token: 0x040037E6 RID: 14310
			private SelectionButton button;

			// Token: 0x040037E7 RID: 14311
			private GameObject selectedCursor;

			// Token: 0x040037E8 RID: 14312
			public int cardID;

			// Token: 0x040037E9 RID: 14313
			private RawImage imageCard;

			// Token: 0x040037EA RID: 14314
			private Action<int, int> SaveShareCard;
		}
	}
}
