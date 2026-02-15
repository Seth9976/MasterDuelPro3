using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using DG.Tweening;
using MDPro3.Duel.YGOSharp;
using MDPro3.Net;
using MDPro3.UI;
using MDPro3.UI.Popup;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MDPro3.Servant
{
	// Token: 0x020012FE RID: 4862
	public class RoomServant : Servant
	{
		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06008E3C RID: 36412 RVA: 0x0012FB11 File Offset: 0x0012DD11
		public override int Depth
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06008E3D RID: 36413 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool ShowLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008E3E RID: 36414 RVA: 0x0012FB14 File Offset: 0x0012DD14
		public override void Initialize()
		{
			this.returnServant = Program.instance.online;
			base.Initialize();
		}

		// Token: 0x06008E3F RID: 36415 RVA: 0x0012FB2C File Offset: 0x0012DD2C
		protected override void ApplyShowArrangement(int preDepth)
		{
			base.ApplyShowArrangement(preDepth);
			RoomServant.CoreShowing = 0;
			Program.instance.ui_.chatPanel.Show(false);
			OcgCore.handler = new OcgCore.ResponseHandler(this.Handler);
			this.GetUI<RoomServantUI>().RefreshDeckSelector();
		}

		// Token: 0x06008E40 RID: 36416 RVA: 0x0012FB6C File Offset: 0x0012DD6C
		protected override void ApplyHideArrangement(int preDepth)
		{
			Program.instance.ui_.chatPanel.Hide();
			base.ApplyHideArrangement(preDepth);
		}

		// Token: 0x06008E41 RID: 36417 RVA: 0x0012FB8C File Offset: 0x0012DD8C
		public override void OnExit()
		{
			if (RoomServant.FromSolo)
			{
				this.returnServant = Program.instance.solo;
			}
			else
			{
				this.returnServant = Program.instance.online;
				if (RoomServant.FromLocalHost)
				{
					YgoServer.StopServer();
				}
			}
			base.OnExit();
			Program.instance.ocgcore.CloseConnection();
		}

		// Token: 0x06008E42 RID: 36418 RVA: 0x0012FBE4 File Offset: 0x0012DDE4
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			GameObject selected = EventSystem.current.currentSelectedGameObject;
			if (selected == null || selected != this.lastSelectable)
			{
				if (this.lastSelectable != null && this.lastSelectable.gameObject.activeInHierarchy)
				{
					this.lastSelectable.Select();
					return;
				}
				this.servantUI.SelectDefaultSelectable();
			}
		}

		// Token: 0x06008E43 RID: 36419 RVA: 0x0012FC55 File Offset: 0x0012DE55
		public void Handler(byte[] buffer)
		{
			TcpHelper.CtosMessage_Response(buffer);
		}

		// Token: 0x06008E44 RID: 36420 RVA: 0x0012FC60 File Offset: 0x0012DE60
		public bool RoomIsFull()
		{
			int playerSeats = 2;
			if (RoomServant.Mode == 2)
			{
				playerSeats = 4;
			}
			for (int i = 0; i < playerSeats; i++)
			{
				if (RoomServant.players[i] == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06008E45 RID: 36421 RVA: 0x0012FC91 File Offset: 0x0012DE91
		public bool CanChangeDeck()
		{
			if (RoomServant.players[RoomServant.SelfType] != null && RoomServant.players[RoomServant.SelfType].ready)
			{
				MessageManager.Toast(InterString.Get("请先取消准备，再选择卡组。", 0));
				return false;
			}
			return true;
		}

		// Token: 0x06008E46 RID: 36422 RVA: 0x0012FCC8 File Offset: 0x0012DEC8
		private void ShowOcgCore()
		{
			if (RoomServant.CoreShowing == 0)
			{
				RoomServant.CoreShowing = 1;
			}
			if (Program.instance.ocgcore.showing)
			{
				return;
			}
			if (RoomServant.Mode != 2)
			{
				if (RoomServant.SelfType == 7)
				{
					OcgCore.name_0 = ChatPanel.GetPlayerName(0);
					OcgCore.name_1 = ChatPanel.GetPlayerName(1);
				}
				else
				{
					OcgCore.name_0 = ChatPanel.GetPlayerName(RoomServant.SelfType);
					OcgCore.name_1 = ChatPanel.GetPlayerName(1 - RoomServant.SelfType);
				}
				OcgCore.name_0_c = OcgCore.name_0;
				OcgCore.name_1_c = OcgCore.name_1;
				OcgCore.name_0_tag = "---";
				OcgCore.name_1_tag = "---";
			}
			else if (RoomServant.SelfType == 7)
			{
				OcgCore.name_0 = ChatPanel.GetPlayerName(0);
				OcgCore.name_0_tag = ChatPanel.GetPlayerName(1);
				OcgCore.name_1 = ChatPanel.GetPlayerName(2);
				OcgCore.name_1_tag = ChatPanel.GetPlayerName(3);
			}
			else
			{
				int op = 0;
				int opTag = 0;
				int selfType = RoomServant.SelfType;
				if (selfType > 1)
				{
					if (selfType - 2 <= 1)
					{
						op = 0;
						opTag = 1;
					}
				}
				else
				{
					op = 2;
					opTag = 3;
				}
				OcgCore.name_0 = ChatPanel.GetPlayerName((RoomServant.SelfType == 0 || RoomServant.SelfType == 2) ? RoomServant.SelfType : (RoomServant.SelfType - 1));
				OcgCore.name_0_tag = ChatPanel.GetPlayerName((RoomServant.SelfType == 0 || RoomServant.SelfType == 2) ? (RoomServant.SelfType + 1) : RoomServant.SelfType);
				OcgCore.name_1 = ChatPanel.GetPlayerName(op);
				OcgCore.name_1_tag = ChatPanel.GetPlayerName(opTag);
			}
			OcgCore.timeLimit = (int)RoomServant.TimeLimit;
			OcgCore.lpLimit = RoomServant.StartLp;
			if (RoomServant.FromSolo)
			{
				Program.instance.ocgcore.returnServant = Program.instance.solo;
			}
			else if (RoomServant.FromHandTest)
			{
				Program.instance.ocgcore.returnServant = Program.instance.deckEditor;
			}
			else
			{
				Program.instance.ocgcore.returnServant = Program.instance.online;
			}
			if (RoomServant.SelfType == 7)
			{
				OcgCore.condition = OcgCore.Condition.Watch;
			}
			else
			{
				OcgCore.condition = OcgCore.Condition.Duel;
			}
			OcgCore.inPuzzle = false;
			Program.instance.ShiftToServant(Program.instance.ocgcore);
		}

		// Token: 0x06008E47 RID: 36423 RVA: 0x0012FEC7 File Offset: 0x0012E0C7
		public void Realize()
		{
			if (this.servantUI == null)
			{
				return;
			}
			if (RoomServant.FromHandTest)
			{
				return;
			}
			this.GetUI<RoomServantUI>().Realize();
		}

		// Token: 0x06008E48 RID: 36424 RVA: 0x0012FEEB File Offset: 0x0012E0EB
		private void GoFirst(bool first)
		{
			TcpHelper.CtosMessage_TpResult(first);
		}

		// Token: 0x06008E49 RID: 36425 RVA: 0x0012FEF4 File Offset: 0x0012E0F4
		public void StocMessage_GameMsg(BinaryReader r)
		{
			this.ShowOcgCore();
			Package p = new Package
			{
				Function = (int)r.ReadByte(),
				Data = new BinaryMaster(r.ReadToEnd())
			};
			Program.instance.ocgcore.AddPackage(p);
		}

		// Token: 0x06008E4A RID: 36426 RVA: 0x0012FF3C File Offset: 0x0012E13C
		public void StocMessage_ErrorMsg(BinaryReader r)
		{
			int msg = (int)r.ReadByte();
			r.ReadByte();
			r.ReadByte();
			r.ReadByte();
			int code = r.ReadInt32();
			switch (msg)
			{
			case 1:
				switch (code)
				{
				case 0:
					MessageManager.Cast(StringHelper.GetUnsafe(1403, 0));
					return;
				case 1:
					MessageManager.Cast(StringHelper.GetUnsafe(1404, 0));
					return;
				case 2:
					MessageManager.Cast(StringHelper.GetUnsafe(1405, 0));
					return;
				default:
					return;
				}
				break;
			case 2:
			{
				int flag = code >> 28;
				code &= 268435455;
				string cardName = CardsManager.Get(code, false).Name;
				List<string> tasks = new List<string> { StringHelper.GetUnsafe(1406, 0) };
				string task;
				switch (flag)
				{
				case 1:
				{
					task = StringHelper.GetUnsafe(1407, 0);
					Regex replace = new Regex("%ls");
					task = replace.Replace(task, cardName);
					break;
				}
				case 2:
				case 3:
				case 4:
				case 5:
				{
					task = StringHelper.GetUnsafe(1411 + flag, 0);
					Regex replace = new Regex("%ls");
					task = replace.Replace(task, cardName);
					if (flag == 4)
					{
						replace = new Regex("%d");
						task = replace.Replace(task, code.ToString());
					}
					break;
				}
				case 6:
				case 7:
				case 8:
				case 9:
				{
					task = StringHelper.GetUnsafe(1411 + flag, 0);
					Regex replace = new Regex("%d");
					this.deck = new Deck("Deck/" + Config.GetConfigDeckName(true) + ".ydk");
					if (this.deck != null)
					{
						string target;
						if (flag == 6)
						{
							target = this.deck.Main.Count.ToString();
						}
						else if (flag == 7)
						{
							target = this.deck.Extra.Count.ToString();
						}
						else
						{
							if (flag != 8)
							{
								break;
							}
							target = this.deck.Side.Count.ToString();
						}
						task = replace.Replace(task, target);
					}
					break;
				}
				default:
					task = StringHelper.GetUnsafe(1406, 0);
					break;
				}
				tasks.Add(task);
				UIManager.ShowPopupConfirm(tasks);
				return;
			}
			case 3:
			{
				List<string> tasks = new List<string>
				{
					StringHelper.GetUnsafe(1408, 0),
					StringHelper.GetUnsafe(1410, 0)
				};
				UIManager.ShowPopupConfirm(tasks);
				return;
			}
			case 4:
				Debug.Log("ERROR 4: " + code.ToString());
				return;
			default:
				return;
			}
		}

		// Token: 0x06008E4B RID: 36427 RVA: 0x001301C4 File Offset: 0x0012E3C4
		public void StocMessage_SelectHand(BinaryReader r)
		{
			if (RoomServant.SoloLockHand || Config.Get("AutoRPS", "0") == "0")
			{
				Addressables.InstantiateAsync("Popup/PopupRockPaperScissors.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
				{
					result.Result.transform.SetParent(Program.instance.ui_.popup, false);
					MDPro3.UI.Popup.PopupRockPaperScissors component = result.Result.GetComponent<MDPro3.UI.Popup.PopupRockPaperScissors>();
					component.args = new List<string> { InterString.Get("猜拳", 0) };
					component.Show();
				};
				return;
			}
			TcpHelper.CtosMessage_HandResult(global::UnityEngine.Random.Range(1, 4));
		}

		// Token: 0x06008E4C RID: 36428 RVA: 0x00130234 File Offset: 0x0012E434
		public void StocMessage_SelectTp(BinaryReader r)
		{
			UIManager.ShowPopupYesOrNo(new List<string>
			{
				(Program.instance.currentServant == Program.instance.room) ? InterString.Get("猜拳获胜", 0) : InterString.Get("选择先后手", 0),
				InterString.Get("选择是否由我方先手？", 0),
				InterString.Get("先攻", 0),
				InterString.Get("后攻", 0)
			}, delegate
			{
				this.GoFirst(true);
			}, delegate
			{
				this.GoFirst(false);
			});
		}

		// Token: 0x06008E4D RID: 36429 RVA: 0x001302D0 File Offset: 0x0012E4D0
		public void StocMessage_HandResult(BinaryReader r)
		{
			if (RoomServant.SelfType == 7)
			{
				return;
			}
			int meResult = (int)r.ReadByte();
			int opResult = (int)r.ReadByte();
			if (meResult == opResult)
			{
				MessageManager.Cast(InterString.Get("猜拳平局。", 0));
				return;
			}
			if ((meResult == 1 && opResult == 2) || (meResult == 2 && opResult == 3) || (meResult == 3 && opResult == 1))
			{
				MessageManager.Cast(InterString.Get("猜拳落败。", 0));
			}
		}

		// Token: 0x06008E4E RID: 36430 RVA: 0x0000216D File Offset: 0x0000036D
		public void StocMessage_TpResult(BinaryReader r)
		{
		}

		// Token: 0x06008E4F RID: 36431 RVA: 0x00130331 File Offset: 0x0012E531
		public void StocMessage_ChangeSide(BinaryReader r)
		{
			RoomServant.NeedSide = true;
			if (OcgCore.condition != OcgCore.Condition.Duel || RoomServant.JoinWithReconnect)
			{
				Program.instance.ocgcore.OnDuelResultConfirmed(false);
			}
		}

		// Token: 0x06008E50 RID: 36432 RVA: 0x00130358 File Offset: 0x0012E558
		public void StocMessage_WaitingSide(BinaryReader r)
		{
			RoomServant.SideWaitingObserver = true;
			MessageManager.Cast(InterString.Get("请耐心等待双方玩家更换副卡组。", 0));
		}

		// Token: 0x06008E51 RID: 36433 RVA: 0x0000216D File Offset: 0x0000036D
		public void StocMessage_DeckCount(BinaryReader r)
		{
		}

		// Token: 0x06008E52 RID: 36434 RVA: 0x0000216D File Offset: 0x0000036D
		public void StocMessage_CreateGame(BinaryReader r)
		{
		}

		// Token: 0x06008E53 RID: 36435 RVA: 0x00130370 File Offset: 0x0012E570
		public void StocMessage_JoinGame(BinaryReader r)
		{
			RoomServant.LFList = r.ReadUInt32();
			RoomServant.Rule = r.ReadByte();
			RoomServant.Mode = r.ReadByte();
			OcgCore.MasterRule = (int)r.ReadChar();
			RoomServant.NoCheckDeck = r.ReadBoolean();
			RoomServant.NoShuffleDeck = r.ReadBoolean();
			r.ReadByte();
			r.ReadByte();
			r.ReadByte();
			RoomServant.StartLp = r.ReadInt32();
			RoomServant.StartHand = r.ReadByte();
			RoomServant.DrawCount = r.ReadByte();
			RoomServant.TimeLimit = r.ReadInt16();
			for (int i = 0; i < 4; i++)
			{
				RoomServant.players[i] = null;
			}
			if (!RoomServant.FromHandTest)
			{
				Program.instance.ShiftToServant(Program.instance.room);
			}
		}

		// Token: 0x06008E54 RID: 36436 RVA: 0x00130430 File Offset: 0x0012E630
		public void StocMessage_TypeChange(BinaryReader r)
		{
			byte b = r.ReadByte();
			RoomServant.SelfType = (int)(b & 15);
			RoomServant.IsHost = ((b >> 4) & 15) != 0;
			if (RoomServant.SelfType < 4 && RoomServant.players[RoomServant.SelfType] != null)
			{
				RoomServant.players[RoomServant.SelfType].ready = false;
			}
			this.Realize();
		}

		// Token: 0x06008E55 RID: 36437 RVA: 0x0000216D File Offset: 0x0000036D
		public void StocMessage_LeaveGame(BinaryReader r)
		{
		}

		// Token: 0x06008E56 RID: 36438 RVA: 0x00130488 File Offset: 0x0012E688
		public void StocMessage_DuelStart(BinaryReader r)
		{
			RoomServant.NeedSide = false;
			RoomServant.JoinWithReconnect = true;
			if (Program.instance.deckEditor.showing)
			{
				Program.instance.deckEditor.Hide(0);
				if (!RoomServant.FromHandTest)
				{
					MessageManager.Cast(InterString.Get("更换副卡组成功，请等待对手更换副卡组。", 0));
				}
			}
			if (this.showing)
			{
				base.Hide(0);
			}
			UIManager.HideExitButton(this.TransitionTime, Ease.Linear);
		}

		// Token: 0x06008E57 RID: 36439 RVA: 0x001304F4 File Offset: 0x0012E6F4
		public void StocMessage_DuelEnd(BinaryReader r)
		{
			this.duelEnded = true;
			Program.instance.ocgcore.ForceMSquit();
		}

		// Token: 0x06008E58 RID: 36440 RVA: 0x0013050C File Offset: 0x0012E70C
		public void StocMessage_Replay(BinaryReader r)
		{
			byte[] data = r.ReadToEnd();
			Package package = new Package();
			package.Function = 231;
			package.Data.writer.Write(data);
			TcpHelper.AddRecordLine(package);
		}

		// Token: 0x06008E59 RID: 36441 RVA: 0x00130548 File Offset: 0x0012E748
		public void StocMessage_Chat(BinaryReader r)
		{
			int player = (int)r.ReadInt16();
			long length = r.BaseStream.Length - 3L;
			string content = r.ReadUnicode((int)length);
			Program.instance.ui_.chatPanel.AddChatItem(player, content);
		}

		// Token: 0x06008E5A RID: 36442 RVA: 0x0013058C File Offset: 0x0012E78C
		public void StocMessage_HsPlayerEnter(BinaryReader r)
		{
			if (!RoomServant.FromHandTest)
			{
				AudioManager.PlaySE("SE_ROOM_SITDOWN", 1f);
			}
			string name = r.ReadUnicode(20);
			int pos = (int)(r.ReadByte() & 3);
			RoomServant.Player player = new RoomServant.Player();
			player.name = name;
			player.ready = false;
			RoomServant.players[pos] = player;
			this.Realize();
		}

		// Token: 0x06008E5B RID: 36443 RVA: 0x001305E4 File Offset: 0x0012E7E4
		public void StocMessage_HsPlayerChange(BinaryReader r)
		{
			byte b = r.ReadByte();
			int pos = (b >> 4) & 15;
			int state = (int)(b & 15);
			if (pos < 4)
			{
				if (state < 8)
				{
					RoomServant.players[state] = RoomServant.players[pos];
					RoomServant.players[pos] = null;
				}
				if (state == 9)
				{
					RoomServant.players[pos].ready = true;
				}
				if (state == 10)
				{
					RoomServant.players[pos].ready = false;
				}
				if (state == 11)
				{
					RoomServant.players[pos] = null;
				}
				if (state == 8)
				{
					RoomServant.players[pos] = null;
					RoomServant.ObserverCount++;
				}
				this.Realize();
			}
		}

		// Token: 0x06008E5C RID: 36444 RVA: 0x0013066F File Offset: 0x0012E86F
		public void StocMessage_HsWatchChange(BinaryReader r)
		{
			RoomServant.ObserverCount = (int)r.ReadUInt16();
			this.Realize();
		}

		// Token: 0x0400CC32 RID: 52274
		[HideInInspector]
		public bool duelEnded;

		// Token: 0x0400CC33 RID: 52275
		public static uint LFList;

		// Token: 0x0400CC34 RID: 52276
		public static byte Rule;

		// Token: 0x0400CC35 RID: 52277
		public static byte Mode;

		// Token: 0x0400CC36 RID: 52278
		public static bool NoCheckDeck;

		// Token: 0x0400CC37 RID: 52279
		public static bool NoShuffleDeck;

		// Token: 0x0400CC38 RID: 52280
		public static int StartLp = 8000;

		// Token: 0x0400CC39 RID: 52281
		public static byte StartHand;

		// Token: 0x0400CC3A RID: 52282
		public static byte DrawCount;

		// Token: 0x0400CC3B RID: 52283
		public static short TimeLimit = 180;

		// Token: 0x0400CC3C RID: 52284
		public static int ObserverCount;

		// Token: 0x0400CC3D RID: 52285
		public static int SelfType;

		// Token: 0x0400CC3E RID: 52286
		public static bool IsHost;

		// Token: 0x0400CC3F RID: 52287
		public static bool NeedSide;

		// Token: 0x0400CC40 RID: 52288
		public static bool JoinWithReconnect;

		// Token: 0x0400CC41 RID: 52289
		public static bool SideWaitingObserver;

		// Token: 0x0400CC42 RID: 52290
		public static bool FromSolo;

		// Token: 0x0400CC43 RID: 52291
		public static bool SoloLockHand;

		// Token: 0x0400CC44 RID: 52292
		public static bool FromLocalHost;

		// Token: 0x0400CC45 RID: 52293
		public static bool FromHandTest;

		// Token: 0x0400CC46 RID: 52294
		public static int CoreShowing = 0;

		// Token: 0x0400CC47 RID: 52295
		public static RoomServant.Player[] players = new RoomServant.Player[32];

		// Token: 0x0400CC48 RID: 52296
		private Deck deck;

		// Token: 0x020012FF RID: 4863
		public class Player
		{
			// Token: 0x0400CC49 RID: 52297
			public string name;

			// Token: 0x0400CC4A RID: 52298
			public bool ready;
		}
	}
}
