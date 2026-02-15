using System;
using System.Collections.Generic;
using System.IO;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.Utility;
using TMPro;
using UnityEngine;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x0200147F RID: 5247
	public class RoomServantUI : ServantUI
	{
		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x0600988F RID: 39055 RVA: 0x0016840C File Offset: 0x0016660C
		private TextMeshProUGUI TextRoomInfo
		{
			get
			{
				return this.m_TextRoomInfo = ((this.m_TextRoomInfo != null) ? this.m_TextRoomInfo : base.Manager.GetElement<TextMeshProUGUI>("TextRoomInfo"));
			}
		}

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x06009890 RID: 39056 RVA: 0x00168448 File Offset: 0x00166648
		private SelectionButton_DeckSelector ButtonDeckSelector
		{
			get
			{
				return this.m_ButtonDeckSelector = ((this.m_ButtonDeckSelector != null) ? this.m_ButtonDeckSelector : base.Manager.GetElement<SelectionButton_DeckSelector>("DeckSelector"));
			}
		}

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x06009891 RID: 39057 RVA: 0x00168484 File Offset: 0x00166684
		private SelectionButton ButtonAddBot
		{
			get
			{
				return this.m_ButtonAddBot = ((this.m_ButtonAddBot != null) ? this.m_ButtonAddBot : base.Manager.GetElement<SelectionButton>("ButtonAddBot"));
			}
		}

		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x06009892 RID: 39058 RVA: 0x001684C0 File Offset: 0x001666C0
		private SelectionButton_RoomPlayer ButtonPlayer0
		{
			get
			{
				return this.m_ButtonPlayer0 = ((this.m_ButtonPlayer0 != null) ? this.m_ButtonPlayer0 : base.Manager.GetElement<SelectionButton_RoomPlayer>("ButtonPlayer0"));
			}
		}

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x06009893 RID: 39059 RVA: 0x001684FC File Offset: 0x001666FC
		private SelectionButton_RoomPlayer ButtonPlayer1
		{
			get
			{
				return this.m_ButtonPlayer1 = ((this.m_ButtonPlayer1 != null) ? this.m_ButtonPlayer1 : base.Manager.GetElement<SelectionButton_RoomPlayer>("ButtonPlayer1"));
			}
		}

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x06009894 RID: 39060 RVA: 0x00168538 File Offset: 0x00166738
		private SelectionButton_RoomPlayer ButtonPlayer2
		{
			get
			{
				return this.m_ButtonPlayer2 = ((this.m_ButtonPlayer2 != null) ? this.m_ButtonPlayer2 : base.Manager.GetElement<SelectionButton_RoomPlayer>("ButtonPlayer2"));
			}
		}

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x06009895 RID: 39061 RVA: 0x00168574 File Offset: 0x00166774
		private SelectionButton_RoomPlayer ButtonPlayer3
		{
			get
			{
				return this.m_ButtonPlayer3 = ((this.m_ButtonPlayer3 != null) ? this.m_ButtonPlayer3 : base.Manager.GetElement<SelectionButton_RoomPlayer>("ButtonPlayer3"));
			}
		}

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x06009896 RID: 39062 RVA: 0x001685B0 File Offset: 0x001667B0
		private SelectionButton ButtonToDuel
		{
			get
			{
				return this.m_ButtonToDuel = ((this.m_ButtonToDuel != null) ? this.m_ButtonToDuel : base.Manager.GetElement<SelectionButton>("ButtonToDuel"));
			}
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x06009897 RID: 39063 RVA: 0x001685EC File Offset: 0x001667EC
		private SelectionButton ButtonReady
		{
			get
			{
				return this.m_ButtonReady = ((this.m_ButtonReady != null) ? this.m_ButtonReady : base.Manager.GetElement<SelectionButton>("ButtonReady"));
			}
		}

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x06009898 RID: 39064 RVA: 0x00168628 File Offset: 0x00166828
		private SelectionButton ButtonToWatch
		{
			get
			{
				return this.m_ButtonToWatch = ((this.m_ButtonToWatch != null) ? this.m_ButtonToWatch : base.Manager.GetElement<SelectionButton>("ButtonToWatch"));
			}
		}

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06009899 RID: 39065 RVA: 0x00168664 File Offset: 0x00166864
		private SelectionButton ButtonStart
		{
			get
			{
				return this.m_ButtonStart = ((this.m_ButtonStart != null) ? this.m_ButtonStart : base.Manager.GetElement<SelectionButton>("ButtonStart"));
			}
		}

		// Token: 0x0600989A RID: 39066 RVA: 0x001686A0 File Offset: 0x001668A0
		private void Awake()
		{
			this.roomPlayers = new List<SelectionButton_RoomPlayer> { this.ButtonPlayer0, this.ButtonPlayer1, this.ButtonPlayer2, this.ButtonPlayer3 };
			this.Realize();
		}

		// Token: 0x0600989B RID: 39067 RVA: 0x001686EE File Offset: 0x001668EE
		public override void AfterShowEvent()
		{
			base.AfterShowEvent();
			if (Program.instance.currentServant != Program.instance.room)
			{
				this.ShutDown();
				Program.instance.ui_.chatPanel.Hide();
			}
		}

		// Token: 0x0600989C RID: 39068 RVA: 0x0016872C File Offset: 0x0016692C
		public void SelectMiddleSelectableFromRight()
		{
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			UserInput.NextSelectionIsAxis = true;
			if (this.ButtonStart.gameObject.activeSelf)
			{
				this.ButtonStart.GetSelectable().Select();
				return;
			}
			this.ButtonToWatch.GetSelectable().Select();
		}

		// Token: 0x0600989D RID: 39069 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectMiddleFromLeft()
		{
		}

		// Token: 0x0600989E RID: 39070 RVA: 0x00168780 File Offset: 0x00166980
		public void Realize()
		{
			string roomInfo = string.Empty;
			string rn = "\r\n";
			if (RoomServant.FromLocalHost)
			{
				foreach (string ip in Tools.GetLocalIPv4())
				{
					roomInfo = string.Concat(new string[]
					{
						roomInfo,
						InterString.Get("本机地址：", 0),
						Language.GetBlankIfNeed(),
						ip,
						rn
					});
				}
				roomInfo = string.Concat(new string[]
				{
					roomInfo,
					InterString.Get("端口：", 0),
					Language.GetBlankIfNeed(),
					"7911",
					rn
				});
			}
			roomInfo = string.Concat(new string[]
			{
				roomInfo,
				StringHelper.GetUnsafe(1227, 0),
				Language.GetBlankIfNeed(),
				StringHelper.GetUnsafe(1244 + (int)RoomServant.Mode, 0),
				rn
			});
			roomInfo = string.Concat(new string[]
			{
				roomInfo,
				StringHelper.GetUnsafe(1236, 0),
				Language.GetBlankIfNeed(),
				StringHelper.GetUnsafe(1259 + OcgCore.MasterRule, 0),
				rn
			});
			roomInfo = string.Concat(new string[]
			{
				roomInfo,
				StringHelper.GetUnsafe(1225, 0),
				Language.GetBlankIfNeed(),
				StringHelper.GetUnsafe(1481 + (int)RoomServant.Rule, 0),
				rn
			});
			roomInfo = string.Concat(new string[]
			{
				roomInfo,
				StringHelper.GetUnsafe(1226, 0),
				Language.GetBlankIfNeed(),
				BanlistManager.GetName(RoomServant.LFList),
				rn
			});
			roomInfo = string.Concat(new string[]
			{
				roomInfo,
				StringHelper.GetUnsafe(1231, 0),
				Language.GetBlankIfNeed(),
				RoomServant.StartLp.ToString(),
				rn
			});
			roomInfo = string.Concat(new string[]
			{
				roomInfo,
				StringHelper.GetUnsafe(1232, 0),
				Language.GetBlankIfNeed(),
				RoomServant.StartHand.ToString(),
				rn
			});
			roomInfo = string.Concat(new string[]
			{
				roomInfo,
				StringHelper.GetUnsafe(1233, 0),
				Language.GetBlankIfNeed(),
				RoomServant.DrawCount.ToString(),
				rn
			});
			roomInfo = string.Concat(new string[]
			{
				roomInfo,
				StringHelper.GetUnsafe(1237, 0),
				Language.GetBlankIfNeed(),
				RoomServant.TimeLimit.ToString(),
				rn
			});
			roomInfo = string.Concat(new string[]
			{
				roomInfo,
				StringHelper.GetUnsafe(1253, 0),
				Language.GetBlankIfNeed(),
				RoomServant.ObserverCount.ToString(),
				rn
			});
			if (RoomServant.NoCheckDeck)
			{
				roomInfo = roomInfo + StringHelper.GetUnsafe(1229, 0) + rn;
			}
			if (RoomServant.NoShuffleDeck)
			{
				roomInfo += StringHelper.GetUnsafe(1230, 0);
			}
			this.TextRoomInfo.text = roomInfo;
			if (!Appearance.loaded)
			{
				foreach (SelectionButton_RoomPlayer selectionButton_RoomPlayer in this.roomPlayers)
				{
					selectionButton_RoomPlayer.gameObject.SetActive(false);
				}
				return;
			}
			for (int i = 0; i < 4; i++)
			{
				if (RoomServant.players[i] == null)
				{
					this.roomPlayers[i].gameObject.SetActive(false);
				}
				else
				{
					this.roomPlayers[i].gameObject.SetActive(true);
					this.roomPlayers[i].SetButtonText(RoomServant.players[i].name);
					this.roomPlayers[i].SetReadyIcon(RoomServant.players[i].ready);
					this.roomPlayers[i].SetButtonTextColor((RoomServant.SelfType == i) ? Color.cyan : Color.white);
					switch (ChatPanel.GetPlayerPosition(i))
					{
					case ChatPanel.PlayerPosition.Me:
						this.roomPlayers[i].GetAvatar().material = Appearance.duelFrameMat0;
						this.roomPlayers[i].GetAvatar().sprite = Appearance.duelFace0;
						break;
					case ChatPanel.PlayerPosition.MyTag:
						this.roomPlayers[i].GetAvatar().material = Appearance.duelFrameMat0Tag;
						this.roomPlayers[i].GetAvatar().sprite = Appearance.duelFace0Tag;
						break;
					case ChatPanel.PlayerPosition.Op:
						this.roomPlayers[i].GetAvatar().material = Appearance.duelFrameMat1;
						this.roomPlayers[i].GetAvatar().sprite = Appearance.duelFace1;
						break;
					case ChatPanel.PlayerPosition.OpTag:
						this.roomPlayers[i].GetAvatar().material = Appearance.duelFrameMat1Tag;
						this.roomPlayers[i].GetAvatar().sprite = Appearance.duelFace1Tag;
						break;
					case ChatPanel.PlayerPosition.WatchMe:
						this.roomPlayers[i].GetAvatar().material = Appearance.watchFrameMat0;
						this.roomPlayers[i].GetAvatar().sprite = Appearance.watchFace0;
						break;
					case ChatPanel.PlayerPosition.WatchMyTag:
						this.roomPlayers[i].GetAvatar().material = Appearance.watchFrameMat0Tag;
						this.roomPlayers[i].GetAvatar().sprite = Appearance.watchFace0Tag;
						break;
					case ChatPanel.PlayerPosition.WatchOp:
						this.roomPlayers[i].GetAvatar().material = Appearance.watchFrameMat1;
						this.roomPlayers[i].GetAvatar().sprite = Appearance.watchFace1;
						break;
					case ChatPanel.PlayerPosition.WatchOpTag:
						this.roomPlayers[i].GetAvatar().material = Appearance.watchFrameMat1Tag;
						this.roomPlayers[i].GetAvatar().sprite = Appearance.watchFace1Tag;
						break;
					}
				}
			}
			if (RoomServant.IsHost)
			{
				this.ButtonStart.gameObject.SetActive(true);
				this.ButtonAddBot.gameObject.SetActive(true);
			}
			else
			{
				this.ButtonStart.gameObject.SetActive(false);
				this.ButtonAddBot.gameObject.SetActive(false);
			}
			if (RoomServant.FromSolo)
			{
				this.ButtonAddBot.gameObject.SetActive(false);
			}
			if (RoomServant.SelfType == 7)
			{
				this.ButtonReady.gameObject.SetActive(false);
				return;
			}
			this.ButtonReady.gameObject.SetActive(true);
		}

		// Token: 0x0600989F RID: 39071 RVA: 0x00168E30 File Offset: 0x00167030
		public void RefreshDeckSelector()
		{
			this.ButtonDeckSelector.SetConfigDeck(InterString.Get("请点击此处选择卡组", 0));
		}

		// Token: 0x060098A0 RID: 39072 RVA: 0x00168E48 File Offset: 0x00167048
		public void OnReady()
		{
			if (RoomServant.players[RoomServant.SelfType] == null)
			{
				return;
			}
			if (RoomServant.players[RoomServant.SelfType].ready)
			{
				TcpHelper.CtosMessage_HsNotReady();
				return;
			}
			string deckPath = "Deck/" + Config.GetConfigDeckName(true) + ".ydk";
			if (File.Exists(deckPath))
			{
				TcpHelper.CtosMessage_UpdateDeck(new Deck(deckPath));
				TcpHelper.CtosMessage_HsReady();
				return;
			}
			MessageManager.Cast(InterString.Get("请先选择有效的卡组。", 0));
		}

		// Token: 0x060098A1 RID: 39073 RVA: 0x00168EBA File Offset: 0x001670BA
		public void OnToDuel()
		{
			TcpHelper.CtosMessage_HsToDuelist();
		}

		// Token: 0x060098A2 RID: 39074 RVA: 0x00168EC1 File Offset: 0x001670C1
		public void OnToObserver()
		{
			TcpHelper.CtosMessage_HsToObserver();
		}

		// Token: 0x060098A3 RID: 39075 RVA: 0x00168EC8 File Offset: 0x001670C8
		public void OnStart()
		{
			TcpHelper.CtosMessage_HsStart();
		}

		// Token: 0x060098A4 RID: 39076 RVA: 0x00168ECF File Offset: 0x001670CF
		public void OnKick(int player)
		{
			TcpHelper.CtosMessage_HsKick(player);
		}

		// Token: 0x060098A5 RID: 39077 RVA: 0x00168ED8 File Offset: 0x001670D8
		public void OnAddBot()
		{
			if (Program.instance.room.RoomIsFull())
			{
				MessageManager.Toast(InterString.Get("房间已满，无法继续添加AI。", 0));
				return;
			}
			Program.instance.solo.SwitchCondition(SoloSelector.Condition.ForRoom);
			Program.instance.ShiftToServant(Program.instance.solo);
		}

		// Token: 0x060098A6 RID: 39078 RVA: 0x00168F2B File Offset: 0x0016712B
		public void OnSelectDeck()
		{
			if (!Program.instance.room.CanChangeDeck())
			{
				return;
			}
			Program.instance.deckSelector.SwitchCondition(DeckSelector.Condition.ForDuel);
			Program.instance.ShiftToServant(Program.instance.deckSelector);
		}

		// Token: 0x0400D6AC RID: 54956
		private const string LABEL_TXT_ROOMINFO = "TextRoomInfo";

		// Token: 0x0400D6AD RID: 54957
		private TextMeshProUGUI m_TextRoomInfo;

		// Token: 0x0400D6AE RID: 54958
		private const string LABEL_SBN_DECKSELECTOR = "DeckSelector";

		// Token: 0x0400D6AF RID: 54959
		private SelectionButton_DeckSelector m_ButtonDeckSelector;

		// Token: 0x0400D6B0 RID: 54960
		private const string LABEL_SBN_ADDBOT = "ButtonAddBot";

		// Token: 0x0400D6B1 RID: 54961
		private SelectionButton m_ButtonAddBot;

		// Token: 0x0400D6B2 RID: 54962
		private const string LABEL_SBN_PLAYER0 = "ButtonPlayer0";

		// Token: 0x0400D6B3 RID: 54963
		private SelectionButton_RoomPlayer m_ButtonPlayer0;

		// Token: 0x0400D6B4 RID: 54964
		private const string LABEL_SBN_PLAYER1 = "ButtonPlayer1";

		// Token: 0x0400D6B5 RID: 54965
		private SelectionButton_RoomPlayer m_ButtonPlayer1;

		// Token: 0x0400D6B6 RID: 54966
		private const string LABEL_SBN_PLAYER2 = "ButtonPlayer2";

		// Token: 0x0400D6B7 RID: 54967
		private SelectionButton_RoomPlayer m_ButtonPlayer2;

		// Token: 0x0400D6B8 RID: 54968
		private const string LABEL_SBN_PLAYER3 = "ButtonPlayer3";

		// Token: 0x0400D6B9 RID: 54969
		private SelectionButton_RoomPlayer m_ButtonPlayer3;

		// Token: 0x0400D6BA RID: 54970
		private const string LABEL_SBN_TODUEL = "ButtonToDuel";

		// Token: 0x0400D6BB RID: 54971
		private SelectionButton m_ButtonToDuel;

		// Token: 0x0400D6BC RID: 54972
		private const string LABEL_SBN_READY = "ButtonReady";

		// Token: 0x0400D6BD RID: 54973
		private SelectionButton m_ButtonReady;

		// Token: 0x0400D6BE RID: 54974
		private const string LABEL_SBN_TOWATCH = "ButtonToWatch";

		// Token: 0x0400D6BF RID: 54975
		private SelectionButton m_ButtonToWatch;

		// Token: 0x0400D6C0 RID: 54976
		private const string LABEL_SBN_START = "ButtonStart";

		// Token: 0x0400D6C1 RID: 54977
		private SelectionButton m_ButtonStart;

		// Token: 0x0400D6C2 RID: 54978
		private List<SelectionButton_RoomPlayer> roomPlayers;
	}
}
