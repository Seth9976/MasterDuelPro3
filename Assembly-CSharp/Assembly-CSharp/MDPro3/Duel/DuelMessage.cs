using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace MDPro3.Duel
{
	// Token: 0x020014C5 RID: 5317
	public class DuelMessage : MessageProcessor
	{
		// Token: 0x06009B3F RID: 39743 RVA: 0x001810B7 File Offset: 0x0017F2B7
		public DuelMessage(MessageDispatcher dispatcher)
			: base(dispatcher)
		{
			this.duelBGManager = new DuelBGManager
			{
				processor = this
			};
		}

		// Token: 0x06009B40 RID: 39744 RVA: 0x001810EC File Offset: 0x0017F2EC
		public override void Dispose()
		{
			base.Dispose();
			this.duelBGManager.Dispose();
		}

		// Token: 0x06009B41 RID: 39745 RVA: 0x001810FF File Offset: 0x0017F2FF
		private void LogDebug(string text)
		{
			Debug.LogError(text);
			MessageManager.Cast(text);
		}

		// Token: 0x06009B42 RID: 39746 RVA: 0x0018110D File Offset: 0x0017F30D
		private void DebugNoCard()
		{
			this.LogDebug(string.Format("[Duel]: Not found card for {0}.", OcgCore.currentMessage));
		}

		// Token: 0x06009B43 RID: 39747 RVA: 0x0018112C File Offset: 0x0017F32C
		private void ResetState()
		{
			if (OcgCore.cards.Count > 0)
			{
				foreach (GameCard gameCard in OcgCore.cards)
				{
					gameCard.Dispose();
				}
			}
			OcgCore.cards.Clear();
			OcgCore.tempCards.Clear();
			OcgCore.sideReference = new Deck();
			OcgCore.pause = false;
			OcgCore.duelEnded = false;
			OcgCore.turns = 0;
			OcgCore.handOffset = 0f;
			OcgCore.lastHandOffset = 0f;
			OcgCore.myPreHandCards.Clear();
			OcgCore.opPreHandCards.Clear();
			OcgCore.needRefreshMyHand = true;
			OcgCore.needRefreshOpHand = true;
			OcgCore.materialCards.Clear();
			OcgCore.cardsInChain.Clear();
			OcgCore.codesInChain.Clear();
			OcgCore.controllerInChain.Clear();
			OcgCore.negatedInChain.Clear();
			OcgCore.cardsBeTarget.Clear();
			OcgCore.cardsInSelection.Clear();
			OcgCore.cardsMustBeSelected.Clear();
			OcgCore.myActivated.Clear();
			OcgCore.opActivated.Clear();
			Program.instance.ocgcore.GetUI<OcgCoreUI>().CardDescription.Hide();
			Program.instance.ocgcore.GetUI<OcgCoreUI>().CardList.Hide();
			OcgCore.surrendered = false;
			OcgCore.tagSurrendered = false;
			OcgCore.deckReserved = false;
			OcgCore.cantCheckGrave = false;
			OcgCore.cookie_matchKill = 0;
			Config.Set("MateViewTips", "0");
			if (OcgCore.condition == OcgCore.Condition.Duel)
			{
				if (Config.GetBool("Timing", false))
				{
					OcgCore.chainCondition = OcgCore.ChainCondition.No;
					Program.instance.ocgcore.GetUI<OcgCoreUI>().OnTiming();
				}
				else
				{
					OcgCore.chainCondition = OcgCore.ChainCondition.All;
					Program.instance.ocgcore.GetUI<OcgCoreUI>().OnTiming();
				}
			}
			Program.instance.ocgcore.GetUI<OcgCoreUI>().HidePlaceCount();
			OcgCore.mySummonCount = 0;
			OcgCore.mySpSummonCount = 0;
			OcgCore.opSummonCount = 0;
			OcgCore.opSpSummonCount = 0;
			RoomServant.JoinWithReconnect = false;
			OcgCore.endingAction = null;
			OcgCore.nextMoveAction = null;
			Program.instance.ocgcore.GetUI<OcgCoreUI>().DuelLog.ClearLog();
			Program.instance.ocgcore.GetUI<OcgCoreUI>().DuelLog.showing = true;
			Program.instance.ocgcore.GetUI<OcgCoreUI>().OnLog(true);
			Program.instance.ocgcore.greenBackground.gameObject.SetActive(false);
			OcgCore.inputMode = false;
			Program.instance.ocgcore.returnAction = null;
			OcgCore.movingToMyGrave = 0;
			OcgCore.movingToMyExclude = 0;
			OcgCore.movingToOpGrave = 0;
			OcgCore.movingToMyExclude = 0;
		}

		// Token: 0x06009B44 RID: 39748 RVA: 0x001813C8 File Offset: 0x0017F5C8
		public async UniTask PreloadPlayerNames()
		{
			this.preload = true;
			base.Core.GetUI<OcgCoreUI>().TextPlayer0LP.text = string.Empty;
			base.Core.GetUI<OcgCoreUI>().TextPlayer1LP.text = string.Empty;
			for (int i = 0; i < OcgCore.packages.Count; i++)
			{
				if (OcgCore.packages[i].Function == 4 || OcgCore.packages[i].Function == 163 || OcgCore.packages[i].Function == 235)
				{
					await this.Process(OcgCore.packages[i]);
					break;
				}
			}
			this.preload = false;
		}

		// Token: 0x06009B45 RID: 39749 RVA: 0x0018140C File Offset: 0x0017F60C
		private async UniTask<int> PreloadUpdateCardMessages()
		{
			int num;
			if (OcgCore.packages.Count < 2)
			{
				num = 0;
			}
			else
			{
				for (int i = 1; i < OcgCore.packages.Count; i++)
				{
					if (OcgCore.packages[i].Function != 6)
					{
						return i;
					}
					await this.Process(OcgCore.packages[i]);
				}
				num = 0;
			}
			return num;
		}

		// Token: 0x06009B46 RID: 39750 RVA: 0x00181450 File Offset: 0x0017F650
		protected override async UniTask GameMessage_Start(BinaryReader reader)
		{
			this.ResetState();
			this.duelBGManager.BackgroundFieldInitialize();
			OcgCore.playerType = (int)reader.ReadByte();
			OcgCore.isFirst = (OcgCore.playerType & 15) == 0;
			OcgCore.isObserver = (OcgCore.playerType & 240) > 0;
			if (reader.BaseStream.Length > 17L)
			{
				OcgCore.MasterRule = (int)(reader.ReadByte() + 1);
			}
			OcgCore.life0 = reader.ReadInt32();
			OcgCore.life1 = reader.ReadInt32();
			OcgCore.lpLimit = Mathf.Max(OcgCore.life0, OcgCore.life1);
			OcgCore.turns = 0;
			RoomServant.CoreShowing = 2;
			base.Core.GetUI<OcgCoreUI>().TextPlayer0Name.text = OcgCore.name_0;
			base.Core.GetUI<OcgCoreUI>().TextPlayer1Name.text = OcgCore.name_1;
			if (RoomServant.Mode == 2)
			{
				if (OcgCore.isFirst)
				{
					base.Core.GetUI<OcgCoreUI>().TextPlayer1Name.text = OcgCore.name_1_tag;
				}
				else
				{
					base.Core.GetUI<OcgCoreUI>().TextPlayer0Name.text = OcgCore.name_0_tag;
				}
				OcgCore.isTag = true;
			}
			else
			{
				OcgCore.isTag = false;
			}
			base.Core.SetFace();
			if (!this.preload)
			{
				base.Core.GCS_CreateBundle((int)reader.ReadInt16(), OcgCore.LocalPlayer(0), CardLocation.Deck);
				base.Core.GCS_CreateBundle((int)reader.ReadInt16(), OcgCore.LocalPlayer(0), CardLocation.Extra);
				base.Core.GCS_CreateBundle((int)reader.ReadInt16(), OcgCore.LocalPlayer(1), CardLocation.Deck);
				base.Core.GCS_CreateBundle((int)reader.ReadInt16(), OcgCore.LocalPlayer(1), CardLocation.Extra);
				base.Core.ArrangeCards();
				this.duelBGManager.RefreshBgState();
				base.Core.SetLP(0, 0, true);
				await UniTask.WaitForSeconds(base.Core.TransitionTime, false, PlayerLoopTiming.Update, default(CancellationToken), false);
				await this.duelBGManager.ShowDecksWithDuelStartTextAsync();
			}
		}

		// Token: 0x06009B47 RID: 39751 RVA: 0x0018149C File Offset: 0x0017F69C
		protected override async UniTask GameMessage_ReloadField(BinaryReader reader)
		{
			this.ResetState();
			OcgCore.MasterRule = (int)(reader.ReadByte() + 1);
			if (OcgCore.MasterRule > 255)
			{
				OcgCore.MasterRule -= 255;
			}
			if (OcgCore.inPuzzle)
			{
				OcgCore.isFirst = true;
				OcgCore.myTurn = true;
			}
			PhaseButtonHandler.TurnChange(OcgCore.myTurn, 1);
			for (int p = 0; p < 2; p++)
			{
				int player = OcgCore.LocalPlayer(p);
				if (player == 0)
				{
					OcgCore.life0 = reader.ReadInt32();
				}
				else
				{
					OcgCore.life1 = reader.ReadInt32();
				}
				for (int i = 0; i < 7; i++)
				{
					byte count = reader.ReadByte();
					if (count > 0)
					{
						GPS gps = new GPS
						{
							controller = (uint)player,
							location = 4U,
							position = (int)reader.ReadByte(),
							sequence = (uint)i
						};
						base.Core.GCS_Create(gps, false);
						count = reader.ReadByte();
						for (int xyz = 0; xyz < (int)count; xyz++)
						{
							GPS overlay = new GPS
							{
								controller = gps.controller,
								location = 132U,
								position = xyz,
								sequence = gps.sequence
							};
							base.Core.GCS_Create(overlay, false);
						}
					}
				}
				for (int j = 0; j < 8; j++)
				{
					if (reader.ReadByte() > 0)
					{
						GPS gps2 = new GPS
						{
							controller = (uint)player,
							location = 8U,
							position = (int)reader.ReadByte(),
							sequence = (uint)j
						};
						base.Core.GCS_Create(gps2, false);
					}
				}
				byte value = reader.ReadByte();
				for (int k = 0; k < (int)value; k++)
				{
					GPS gps3 = new GPS
					{
						controller = (uint)player,
						location = 1U,
						position = 2,
						sequence = (uint)k
					};
					base.Core.GCS_Create(gps3, false);
				}
				value = reader.ReadByte();
				for (int l = 0; l < (int)value; l++)
				{
					GPS gps4 = new GPS
					{
						controller = (uint)player,
						location = 2U,
						position = 2,
						sequence = (uint)l
					};
					base.Core.GCS_Create(gps4, false);
				}
				value = reader.ReadByte();
				for (int m = 0; m < (int)value; m++)
				{
					GPS gps5 = new GPS
					{
						controller = (uint)player,
						location = 16U,
						position = 1,
						sequence = (uint)m
					};
					base.Core.GCS_Create(gps5, false);
				}
				value = reader.ReadByte();
				for (int n = 0; n < (int)value; n++)
				{
					GPS gps6 = new GPS
					{
						controller = (uint)player,
						location = 32U,
						position = 1,
						sequence = (uint)n
					};
					base.Core.GCS_Create(gps6, false);
				}
				value = reader.ReadByte();
				byte valueUp = reader.ReadByte();
				for (int i2 = 0; i2 < (int)(value - valueUp); i2++)
				{
					GPS gps7 = new GPS
					{
						controller = (uint)player,
						location = 64U,
						position = 2,
						sequence = (uint)i2
					};
					base.Core.GCS_Create(gps7, false);
				}
				for (int i3 = 0; i3 < (int)valueUp; i3++)
				{
					GPS gps8 = new GPS
					{
						controller = (uint)player,
						location = 64U,
						position = 1,
						sequence = (uint)i3
					};
					base.Core.GCS_Create(gps8, false);
				}
			}
			this.duelBGManager.UpdateBgEffects(0, true);
			this.duelBGManager.UpdateBgEffects(1, true);
			base.Core.SetLP(0, 0, true);
			base.Core.ArrangeCards();
			this.duelBGManager.RefreshBgState();
			for (int i4 = 0; i4 < OcgCore.cards.Count; i4++)
			{
				OcgCore.cards[i4].MoveAsync(OcgCore.cards[i4].p, true, 0f, 0f);
			}
			await this.PreloadUpdateCardMessages();
			await this.duelBGManager.ShowDecksAsync();
		}

		// Token: 0x06009B48 RID: 39752 RVA: 0x001814E8 File Offset: 0x0017F6E8
		protected override UniTask GameMessage_sibyl_chat(BinaryReader reader)
		{
			int player = (int)reader.ReadUInt32();
			if (!base.Core.GetMessageConfig(player))
			{
				return UniTask.CompletedTask;
			}
			string name = string.Empty;
			if (OcgCore.isTag)
			{
				switch (player)
				{
				case 0:
					name = OcgCore.name_0;
					if (OcgCore.playerType < 7 && ((OcgCore.playerType < 2 && !OcgCore.isFirst) || (OcgCore.playerType >= 2 && OcgCore.isFirst)))
					{
						name = OcgCore.name_1;
					}
					break;
				case 1:
					name = OcgCore.name_0_tag;
					if (OcgCore.playerType < 7 && ((OcgCore.playerType < 2 && !OcgCore.isFirst) || (OcgCore.playerType >= 2 && OcgCore.isFirst)))
					{
						name = OcgCore.name_1_tag;
					}
					break;
				case 2:
					name = OcgCore.name_1;
					if (OcgCore.playerType < 7 && ((OcgCore.playerType < 2 && !OcgCore.isFirst) || (OcgCore.playerType >= 2 && OcgCore.isFirst)))
					{
						name = OcgCore.name_0;
					}
					break;
				case 3:
					name = OcgCore.name_1_tag;
					if (OcgCore.playerType < 7 && ((OcgCore.playerType < 2 && !OcgCore.isFirst) || (OcgCore.playerType >= 2 && OcgCore.isFirst)))
					{
						name = OcgCore.name_0_tag;
					}
					break;
				}
			}
			else if (player != 0)
			{
				if (player == 1)
				{
					name = OcgCore.name_1;
					if (OcgCore.playerType < 7 && ((OcgCore.playerType == 0 && !OcgCore.isFirst) || (OcgCore.playerType == 1 && OcgCore.isFirst)))
					{
						name = OcgCore.name_0;
					}
				}
			}
			else
			{
				name = OcgCore.name_0;
				if (OcgCore.playerType < 7 && ((OcgCore.playerType == 0 && !OcgCore.isFirst) || (OcgCore.playerType == 1 && OcgCore.isFirst)))
				{
					name = OcgCore.name_1;
				}
			}
			if (player == 7)
			{
				name = InterString.Get("观战者", 0);
			}
			if (name != string.Empty)
			{
				name += ": ";
			}
			string content = reader.ReadALLUnicode();
			MessageManager.Cast(name + content);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B49 RID: 39753 RVA: 0x001816E0 File Offset: 0x0017F8E0
		protected override UniTask GameMessage_sibyl_name(BinaryReader reader)
		{
			OcgCore.name_0 = reader.ReadUnicode(50);
			OcgCore.name_0_tag = reader.ReadUnicode(50);
			OcgCore.name_0_c = reader.ReadUnicode(50);
			OcgCore.name_1 = reader.ReadUnicode(50);
			OcgCore.name_1_tag = reader.ReadUnicode(50);
			OcgCore.name_1_c = reader.ReadUnicode(50);
			OcgCore.isTag = !(OcgCore.name_0_tag == "---") || !(OcgCore.name_1_tag == "---") || !(OcgCore.name_0 == OcgCore.name_0_c) || !(OcgCore.name_1 == OcgCore.name_1_c);
			if (Config.Get("ReplayPlayerName0", "@ui").Length > 0)
			{
				OcgCore.name_0 = Config.Get("ReplayPlayerName0", "@ui");
			}
			if (Config.Get("ReplayPlayerName1", "@ui").Length > 0)
			{
				OcgCore.name_1 = Config.Get("ReplayPlayerName1", "@ui");
			}
			if (Config.Get("ReplayPlayerName0Tag", "@ui").Length > 0)
			{
				OcgCore.name_0_tag = Config.Get("ReplayPlayerName0Tag", "@ui");
			}
			if (Config.Get("ReplayPlayerName1Tag", "@ui").Length > 0)
			{
				OcgCore.name_1_tag = Config.Get("ReplayPlayerName1Tag", "@ui");
			}
			if (OcgCore.isTag)
			{
				if (OcgCore.isFirst)
				{
					OcgCore.name_0_c = OcgCore.name_0;
					OcgCore.name_1_c = OcgCore.name_1_tag;
				}
				else
				{
					OcgCore.name_0_c = OcgCore.name_0_tag;
					OcgCore.name_1_c = OcgCore.name_1;
				}
			}
			else
			{
				OcgCore.name_0_c = OcgCore.name_0;
				OcgCore.name_1_c = OcgCore.name_1;
			}
			base.Core.GetUI<OcgCoreUI>().TextPlayer0Name.text = OcgCore.name_0_c;
			base.Core.GetUI<OcgCoreUI>().TextPlayer1Name.text = OcgCore.name_1_c;
			base.Core.SetFace();
			if (reader.BaseStream.Position < reader.BaseStream.Length)
			{
				OcgCore.MasterRule = reader.ReadInt32();
			}
			else
			{
				OcgCore.MasterRule = 3;
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B4A RID: 39754 RVA: 0x001818F1 File Offset: 0x0017FAF1
		protected override UniTask GameMessage_sibyl_quit(BinaryReader reader)
		{
			OcgCore.duelEnded = true;
			OcgCore.duelResult = OcgCore.DuelResult.DisLink;
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B4B RID: 39755 RVA: 0x00181904 File Offset: 0x0017FB04
		protected override async UniTask GameMessage_Retry(BinaryReader reader)
		{
			MessageManager.Cast(InterString.Get("游戏出错，请重试。或者保存回放并联系开发者。", 0));
			await this.dispatcher.RetryMessage();
		}

		// Token: 0x06009B4C RID: 39756 RVA: 0x00181948 File Offset: 0x0017FB48
		protected override UniTask GameMessage_ShowHint(BinaryReader reader)
		{
			reader.ReadInt16();
			byte[] buffer = reader.ReadToEnd();
			MessageManager.Cast(Encoding.UTF8.GetString(buffer, 0, buffer.Length));
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B4D RID: 39757 RVA: 0x0018197C File Offset: 0x0017FB7C
		protected override UniTask GameMessage_AiName(BinaryReader reader)
		{
			short length = reader.ReadInt16();
			byte[] buffer = reader.ReadBytes((int)(length + 1));
			string @string = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
			OcgCore.name_0 = Config.Get("DuelPlayerName0", "@ui");
			OcgCore.name_0_c = OcgCore.name_0;
			OcgCore.name_1 = @string;
			OcgCore.name_1_c = OcgCore.name_1;
			base.Core.GetUI<OcgCoreUI>().TextPlayer0Name.text = OcgCore.name_0_c;
			base.Core.GetUI<OcgCoreUI>().TextPlayer1Name.text = OcgCore.name_1_c;
			OcgCore.isTag = false;
			base.Core.SetFace();
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B4E RID: 39758 RVA: 0x00181A20 File Offset: 0x0017FC20
		protected override async UniTask GameMessage_Win(BinaryReader reader)
		{
			OcgCore.deckReserved = false;
			OcgCore.cantCheckGrave = false;
			OcgCore.duelEnded = true;
			base.Core.GetUI<OcgCoreUI>().CardDescription.Hide();
			this.duelBGManager.ClearResponse();
			AudioManager.StopBGM();
			if (base.Core.currentPopup != null)
			{
				base.Core.currentPopup.whenQuitDo = null;
				base.Core.currentPopup.Hide();
				base.Core.returnAction = null;
			}
			int player = OcgCore.LocalPlayer((int)reader.ReadByte());
			byte winType = reader.ReadByte();
			string duelText = string.Empty;
			string endingReason = string.Empty;
			if (player == 2)
			{
				OcgCore.duelResult = OcgCore.DuelResult.Draw;
				duelText = "DuelTextDraw";
			}
			else if (player == 0 || winType == 4)
			{
				OcgCore.duelResult = OcgCore.DuelResult.Win;
				duelText = "DuelTextWin";
				if (OcgCore.cookie_matchKill > 0)
				{
					OcgCore.winReason = CardsManager.Get(OcgCore.cookie_matchKill, false).Name;
					endingReason = InterString.Get("比赛胜利，卡片：[?]", OcgCore.winReason, 0);
				}
				else
				{
					OcgCore.winReason = StringHelper.Get("victory", (int)winType, 0);
					endingReason = InterString.Get("游戏胜利，原因：[?]", OcgCore.winReason, 0);
				}
			}
			else
			{
				OcgCore.duelResult = OcgCore.DuelResult.Lose;
				duelText = "DuelTextLose";
				if (OcgCore.cookie_matchKill > 0)
				{
					OcgCore.winReason = CardsManager.Get(OcgCore.cookie_matchKill, false).Name;
					endingReason = InterString.Get("比赛败北，卡片：[?]", OcgCore.winReason, 0);
				}
				else
				{
					OcgCore.winReason = StringHelper.Get("victory", (int)winType, 0);
					endingReason = InterString.Get("游戏败北，原因：[?]", OcgCore.winReason, 0);
				}
			}
			this.duelBGManager.DuelEndEvent();
			UIManager.UIBlackOut(base.Core.TransitionTime);
			base.Core.GetUI<OcgCoreUI>().CG.blocksRaycasts = true;
			base.Core.GetUI<OcgCoreUI>().CG.alpha = 1f;
			base.Core.GetUI<OcgCoreUI>().Buttons.SetActive(true);
			if (OcgCore.cookie_matchKill > 0)
			{
				await this.duelBGManager.PlayCommonSpecialWin(new int[] { OcgCore.cookie_matchKill });
			}
			else if (winType >= 16)
			{
				if (winType == 16)
				{
					await this.duelBGManager.PlaySpecialWin("33396948", new Action<ElementObjectManager>(DuelMessage.<GameMessage_Win>g__Action|18_0));
				}
				else if (winType == 17)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 95308449 });
				}
				else if (winType == 18)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 8062132 });
				}
				else if (winType == 19)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 10000040 });
				}
				else if (winType == 20)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 13893596 });
				}
				else if (winType == 21)
				{
					await this.duelBGManager.PlaySpecialWin("40771118", null);
				}
				else if (winType == 22)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 28566710 });
				}
				else if (winType == 23)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 48995978 });
				}
				else if (winType == 24)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 6165656 });
				}
				else if (winType == 25)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 81171949, 81171949, 81171949 });
				}
				else if (winType == 26)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 42776960 });
				}
				else if (winType == 27)
				{
					await this.duelBGManager.PlaySpecialWin("53334641", null);
				}
				else if (winType == 28)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 97795930 });
				}
				else if (winType == 29)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 69553552 });
				}
				else if (winType == 30)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 66765023 });
				}
				else if (winType == 31)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 5008836 });
				}
				else if (winType == 32)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 37984331 });
				}
				else if (winType == 33)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 15862758 });
				}
				else if (winType == 34)
				{
					await this.duelBGManager.PlaySpecialWin("96637156", null);
				}
				else if (winType == 35)
				{
					await this.duelBGManager.PlayCommonSpecialWin(new int[] { 77751766 });
				}
				else
				{
					MessageManager.Cast(InterString.Get("请联系开发者修复这张特殊胜利的卡。", 0));
				}
			}
			await this.duelBGManager.ShowDuelResultText(duelText);
			MessageManager.Cast(endingReason);
			if (OcgCore.condition != OcgCore.Condition.Replay)
			{
				base.Core.GetUI<OcgCoreUI>().ShowSaveReplay();
			}
			this.duelBGManager.ShowBGEnd(OcgCore.duelResult);
		}

		// Token: 0x06009B4F RID: 39759 RVA: 0x00181A6C File Offset: 0x0017FC6C
		protected override UniTask GameMessage_UpdateData(BinaryReader reader)
		{
			OcgCore.LocalPlayer((int)reader.ReadChar());
			reader.ReadChar();
			try
			{
				for (;;)
				{
					int len = reader.ReadInt32();
					if (len != 4)
					{
						long pos = reader.BaseStream.Position;
						reader.ReadCardData(null);
						reader.BaseStream.Position = pos + (long)len - 4L;
					}
				}
			}
			catch
			{
			}
			OcgCore.inPendulumSummon = false;
			OcgCore.myPreHandCards.Clear();
			OcgCore.opPreHandCards.Clear();
			base.Core.RefreshHandCardPosition();
			this.duelBGManager.RefreshBgState();
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B50 RID: 39760 RVA: 0x00181B08 File Offset: 0x0017FD08
		protected override UniTask GameMessage_UpdateCard(BinaryReader reader)
		{
			GPS gps = reader.ReadShortGPS();
			GameCard card = base.Core.GCS_Get(gps);
			reader.ReadUInt32();
			reader.ReadCardData(card);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B51 RID: 39761 RVA: 0x00181B3C File Offset: 0x0017FD3C
		protected override async UniTask GameMessage_Move(BinaryReader reader)
		{
			int code = reader.ReadInt32();
			GPS from = reader.ReadGPS();
			GPS to = reader.ReadGPS();
			uint reason = reader.ReadUInt32();
			GameCard card = base.Core.GCS_Get(from);
			if (card == null)
			{
				card = base.Core.GCS_Create(from, false);
			}
			OcgCore.lastMoveCard = card;
			card.SetCode(code);
			to.reason = reason;
			OcgCore.nextMoveNeedCode = false;
			if (OcgCore.CanSyncNextMove(from, to))
			{
				card.MoveAsync(to, false, 0f, 0f);
			}
			else
			{
				await card.MoveAsync(to, false, 0f, 0f);
			}
		}

		// Token: 0x06009B52 RID: 39762 RVA: 0x00181B88 File Offset: 0x0017FD88
		protected override async UniTask GameMessage_PosChange(BinaryReader reader)
		{
			OcgCore.ES_hint = StringHelper.GetUnsafe(1600, 0);
			int code = reader.ReadInt32();
			GPS from = reader.ReadGPS();
			GameCard card = base.Core.GCS_Get(from);
			if (card == null)
			{
				this.DebugNoCard();
			}
			else
			{
				GPS to = from;
				to.position = (int)reader.ReadByte();
				card.ShowFaceDownCardOrNot(false);
				card.SetCode(code);
				await card.MoveAsync(to, false, 0f, 0f);
				if (to.InPosition(CardPosition.FaceUp) && to.InLocation(CardLocation.MonsterZone))
				{
					card.AnimationPositon(0f);
					await UniTask.WaitForSeconds(0.3f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
				}
			}
		}

		// Token: 0x06009B53 RID: 39763 RVA: 0x00181BD4 File Offset: 0x0017FDD4
		protected override UniTask GameMessage_Set(BinaryReader reader)
		{
			OcgCore.ES_hint = StringHelper.GetUnsafe(1601, 0);
			GameObject gameObject = ABLoader.LoadMasterDuelGameObject("fxp_som_mgctrpfld_001");
			gameObject.transform.position = OcgCore.lastMoveCard.model.transform.position;
			global::UnityEngine.Object.Destroy(gameObject, 3f);
			AudioManager.PlaySE("SE_LAND_MT_SET", 1f);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B54 RID: 39764 RVA: 0x00181C38 File Offset: 0x0017FE38
		protected override async UniTask GameMessage_Swap(BinaryReader reader)
		{
			reader.ReadInt32();
			GPS from = reader.ReadGPS();
			reader.ReadInt32();
			GPS to = reader.ReadGPS();
			if (from.controller != to.controller)
			{
				OcgCore.ES_hint = StringHelper.GetUnsafe(1602, 0);
			}
			GPS from2 = new GPS
			{
				controller = from.controller,
				location = from.location,
				sequence = from.sequence,
				position = to.position
			};
			GPS to2 = new GPS
			{
				controller = to.controller,
				location = to.location,
				sequence = to.sequence,
				position = from.position
			};
			GameCard card = base.Core.GCS_Get(from);
			GameCard card2 = base.Core.GCS_Get(to);
			if (card == null || card2 == null)
			{
				this.DebugNoCard();
			}
			else
			{
				card.MoveAsync(to2, false, 0f, 0f);
				await card2.MoveAsync(from2, false, 0f, 0f);
			}
		}

		// Token: 0x06009B55 RID: 39765 RVA: 0x00181C84 File Offset: 0x0017FE84
		protected override async UniTask GameMessage_Summoning(BinaryReader reader)
		{
			OcgCore.cardsInSelection.Clear();
			int code = reader.ReadInt32();
			GPS gps = reader.ReadGPS();
			GameCard card = base.Core.GCS_Get(gps);
			if (card == null)
			{
				this.DebugNoCard();
			}
			else
			{
				if (gps.InMyControl())
				{
					OcgCore.mySummonCount++;
				}
				else
				{
					OcgCore.opSummonCount++;
				}
				string se = "SE_LAND_NORMAL";
				card.SetCode(code);
				card.AddStringTail(InterString.Get("通常召唤登场", 0));
				card.AnimationPositon(0f);
				OcgCore.ES_hint = InterString.Get("「[?]」通常召唤宣言时", card.GetData().Name, 0);
				if (card.GetData().Level > 6)
				{
					GameObject effect = ABLoader.LoadMasterDuelGameObject("fxp_somldg_Advance_S2_001");
					effect.transform.position = GameCard.GetCardPosition(gps, null, null);
					global::UnityEngine.Object.Destroy(effect, 5f);
					GameObject effect2 = ABLoader.LoadMasterDuelGameObject("fxp_somldg_Advance_S2imp_001");
					effect2.transform.position = effect.transform.position;
					if (card.p.InPosition(CardPosition.Defence))
					{
						effect2.transform.eulerAngles = this.effectDefenceAngle;
					}
					se = "SE_LAND_ADVANCE_HIGH";
					CameraManager.ShakeCamera(true);
				}
				else if (card.GetData().Level > 4)
				{
					GameObject effect3 = ABLoader.LoadMasterDuelGameObject("fxp_somldg_Advance_S1_001");
					effect3.transform.localPosition = GameCard.GetCardPosition(gps, null, null);
					global::UnityEngine.Object.Destroy(effect3, 10f);
					GameObject effect4 = ABLoader.LoadMasterDuelGameObject("fxp_somldg_Advance_S1imp_001");
					effect4.transform.position = effect3.transform.position;
					if (card.p.InPosition(CardPosition.Defence))
					{
						effect4.transform.eulerAngles = this.effectDefenceAngle;
					}
					se = "SE_LAND_ADVANCE_MIDDLE";
					CameraManager.ShakeCamera(false);
				}
				else
				{
					GameObject effect5 = ABLoader.LoadMasterDuelGameObject("fxp_somldg_Hand_001");
					effect5.transform.localPosition = GameCard.GetCardPosition(gps, null, null);
					if (gps.InPosition(CardPosition.Attack))
					{
						global::UnityEngine.Object.Destroy(effect5.transform.GetChild(1).gameObject);
					}
					else
					{
						global::UnityEngine.Object.Destroy(effect5.transform.GetChild(0).gameObject);
					}
					global::UnityEngine.Object.Destroy(effect5, 5f);
				}
				if (base.Core.GetAutoInfo())
				{
					base.Core.GetUI<OcgCoreUI>().CardDescription.Show(card, card.GetMaterial(), -1, null);
				}
				AudioManager.PlaySE(se, 1f);
				int shakeLevel = 0;
				if (CutinViewer.HasCutin(card.GetData().Id))
				{
					shakeLevel = 1;
				}
				if (card.GetData().Level > 4)
				{
					shakeLevel = 1;
				}
				if (card.GetData().Level > 6)
				{
					shakeLevel = 2;
				}
				foreach (GameCard gameCard in OcgCore.cards)
				{
					gameCard.AnimationLandShake(card, shakeLevel);
				}
				OcgCore.materialCards.Clear();
				await UniTask.WaitForSeconds(1f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B56 RID: 39766 RVA: 0x00181CCF File Offset: 0x0017FECF
		protected override UniTask GameMessage_Summoned(BinaryReader reader)
		{
			OcgCore.ES_hint = StringHelper.GetUnsafe(1604, 0);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B57 RID: 39767 RVA: 0x00181CE8 File Offset: 0x0017FEE8
		protected override async UniTask GameMessage_SpSummoning(BinaryReader reader)
		{
			OcgCore.cardsInSelection.Clear();
			int code = reader.ReadInt32();
			GPS gps = reader.ReadGPS();
			GameCard card = base.Core.GCS_Get(gps);
			if (card == null)
			{
				this.DebugNoCard();
			}
			else
			{
				if (gps.InMyControl())
				{
					OcgCore.mySpSummonCount++;
				}
				else
				{
					OcgCore.opSpSummonCount++;
				}
				if (!card.GetData().HasType(CardType.Token))
				{
					string eff = string.Empty;
					string imp = string.Empty;
					string se = "SE_LAND_NORMAL";
					card.SetCode(code);
					card.AnimationPositon(0f);
					OcgCore.ES_hint = InterString.Get("「[?]」特殊召唤宣言时", card.GetData().Name, 0);
					if (OcgCore.materialCards.Count > 0)
					{
						if (card.GetData().HasType(CardType.Link))
						{
							eff = "fxp_somldg_Link_S1_001";
							imp = "fxp_somldg_Link_S1imp_001";
							se = "SE_LAND_LINK_MIDDLE";
						}
						else if (card.GetData().HasType(CardType.Fusion))
						{
							eff = "fxp_somldg_Fusion_S1_001";
							imp = "fxp_somldg_Fusion_S1imp_001";
							se = "SE_LAND_FUSION_MIDDLE";
						}
						else if (card.GetData().HasType(CardType.Synchro))
						{
							eff = "fxp_somldg_Synchro_S1_001";
							imp = "fxp_somldg_Synchro_S1imp_001";
							se = "SE_LAND_SYNCHRO_MIDDLE";
						}
						else if (card.GetData().HasType(CardType.Xyz))
						{
							eff = "fxp_somldg_Xyz_S1_001";
							imp = "fxp_somldg_Xyz_S1imp_001";
							se = "SE_LAND_XYZ_MIDDLE";
						}
						else if (card.GetData().HasType(CardType.Ritual))
						{
							eff = "fxp_somldg_Ritual_S1_001";
							imp = "fxp_somldg_Ritual_S1imp_001";
							se = "SE_LAND_RITUAL_MIDDLE";
						}
					}
					else if (OcgCore.inPendulumSummon)
					{
						eff = "fxp_somldg_Pendulum_S1_001";
						imp = "fxp_somldg_Pendulum_S1imp_001";
						se = "SE_LAND_PENDULUM_MIDDLE";
					}
					else
					{
						eff = "fxp_somldg_Special_S1_001";
						imp = "fxp_somldg_Special_S1imp_001";
						se = "SE_LAND_NORMAL_MIDDLE";
					}
					if (card.GetData().IsHighLevel())
					{
						eff = eff.Replace("S1", "S2");
						imp = imp.Replace("S1", "S2");
						se = se.Replace("MIDDLE", "HIGH");
					}
					CameraManager.Overlay3DReset();
					GameObject effect = ABLoader.LoadMasterDuelGameObject(eff);
					effect.transform.position = GameCard.GetCardPosition(gps, null, null);
					global::UnityEngine.Object.Destroy(effect, 5f);
					GameObject impact = ABLoader.LoadMasterDuelGameObject(imp);
					impact.transform.position = effect.transform.position;
					global::UnityEngine.Object.Destroy(impact, 5f);
					if (gps.InPosition(CardPosition.Defence))
					{
						effect.transform.eulerAngles = this.effectDefenceAngle;
						impact.transform.eulerAngles = this.effectDefenceAngle;
					}
					AudioManager.PlaySE(se, 1f);
					if (se.EndsWith("HIGH"))
					{
						CameraManager.ShakeCamera(true);
					}
					else
					{
						CameraManager.ShakeCamera(false);
					}
					if (base.Core.GetAutoInfo())
					{
						base.Core.GetUI<OcgCoreUI>().CardDescription.Show(card, card.GetMaterial(), -1, null);
					}
					int shakeLevel = 0;
					if (CutinViewer.HasCutin(card.GetData().Id))
					{
						shakeLevel = 1;
					}
					if (card.GetData().IsHighLevel())
					{
						shakeLevel = 2;
					}
					foreach (GameCard gameCard in OcgCore.cards)
					{
						gameCard.AnimationLandShake(card, shakeLevel);
					}
				}
				if (card.GetData().HasType(CardType.Token))
				{
					await UniTask.WaitForSeconds(0.2f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
				}
				else
				{
					await UniTask.WaitForSeconds(1f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
				}
				OcgCore.materialCards.Clear();
			}
		}

		// Token: 0x06009B58 RID: 39768 RVA: 0x00181D33 File Offset: 0x0017FF33
		protected override UniTask GameMessage_SpSummoned(BinaryReader reader)
		{
			OcgCore.ES_hint = StringHelper.GetUnsafe(1606, 0);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B59 RID: 39769 RVA: 0x00181D4C File Offset: 0x0017FF4C
		protected override async UniTask GameMessage_FlipSummoning(BinaryReader reader)
		{
			OcgCore.cardsInSelection.Clear();
			int code = reader.ReadInt32();
			GPS gps = reader.ReadShortGPS();
			GameCard card = base.Core.GCS_Get(gps);
			if (card == null)
			{
				this.DebugNoCard();
			}
			else
			{
				card.SetCode(code);
				gps.position = 1;
				card.RefreshData();
				OcgCore.ES_hint = InterString.Get("「[?]」反转召唤宣言时", card.GetData().Name, 0);
				if (base.Core.GetAutoInfo())
				{
					base.Core.GetUI<OcgCoreUI>().CardDescription.Show(card, card.GetMaterial(), -1, null);
				}
				OcgCore.materialCards.Clear();
				await card.MoveAsync(gps, false, 0f, 0f);
				card.AnimationPositon(0f);
			}
		}

		// Token: 0x06009B5A RID: 39770 RVA: 0x00181D97 File Offset: 0x0017FF97
		protected override UniTask GameMessage_FlipSummoned(BinaryReader reader)
		{
			OcgCore.ES_hint = StringHelper.GetUnsafe(1608, 0);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B5B RID: 39771 RVA: 0x00181DB0 File Offset: 0x0017FFB0
		protected override async UniTask GameMessage_Chaining(BinaryReader reader)
		{
			int code = reader.ReadInt32();
			GPS gps = reader.ReadGPS();
			GameCard card = base.Core.GCS_Get(gps);
			if (card == null)
			{
				this.DebugNoCard();
			}
			else
			{
				card.SetCode(code);
				OcgCore.cardsInChain.Add(card);
				OcgCore.codesInChain.Add(code);
				OcgCore.controllerInChain.Add(gps.controller);
				OcgCore.ES_hint = InterString.Get("「[?]」被发动时", card.GetData().Name, 0);
				if (gps.InMyControl())
				{
					if (!OcgCore.myActivated.Contains(code))
					{
						OcgCore.myActivated.Add(code);
					}
				}
				else if (!OcgCore.opActivated.Contains(code))
				{
					OcgCore.opActivated.Add(code);
				}
				if (base.Core.GetAutoInfo())
				{
					base.Core.GetUI<OcgCoreUI>().CardDescription.Show(card, null, -1, null);
				}
				await card.AnimationActivate().WaitAsync(default(CancellationToken));
			}
		}

		// Token: 0x06009B5C RID: 39772 RVA: 0x00181DFC File Offset: 0x0017FFFC
		protected override async UniTask GameMessage_Chained(BinaryReader reader)
		{
			List<GameCard> cardsInChain = OcgCore.cardsInChain;
			cardsInChain[cardsInChain.Count - 1].AddChain(OcgCore.cardsInChain.Count);
			await this.duelBGManager.ShowChainStack();
		}

		// Token: 0x06009B5D RID: 39773 RVA: 0x00181E40 File Offset: 0x00180040
		protected override async UniTask GameMessage_ChainSolving(BinaryReader reader)
		{
			OcgCore.chainSolvingIndex = (int)reader.ReadByte();
			if (OcgCore.chainSolvingIndex > OcgCore.cardsInChain.Count)
			{
				this.LogDebug("[Duel]: Chain index overflow.");
			}
			else
			{
				OcgCore.chainSolvingCard = OcgCore.cardsInChain[OcgCore.chainSolvingIndex - 1];
				await this.duelBGManager.ShowChainResolve();
				await this.duelBGManager.ShowCardEffectAnimation();
			}
		}

		// Token: 0x06009B5E RID: 39774 RVA: 0x00181E8C File Offset: 0x0018008C
		protected override UniTask GameMessage_ChainSolved(BinaryReader reader)
		{
			OcgCore.materialCards.Clear();
			byte id = reader.ReadByte();
			if ((int)id > OcgCore.cardsInChain.Count)
			{
				this.LogDebug("[Duel]: Chain index overflow.");
				return UniTask.CompletedTask;
			}
			OcgCore.cardsInChain[(int)(id - 1)].RemoveChain((int)id);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B5F RID: 39775 RVA: 0x00181EE0 File Offset: 0x001800E0
		protected override UniTask GameMessage_ChainEnd(BinaryReader reader)
		{
			foreach (GameCard gameCard in OcgCore.cardsInChain)
			{
				gameCard.negated = false;
				gameCard.disabledInChain = false;
				gameCard.RemoveAllChain();
				gameCard.effectTargets.Clear();
			}
			OcgCore.cardsBeTarget.Clear();
			OcgCore.cardsInChain.Clear();
			OcgCore.codesInChain.Clear();
			OcgCore.controllerInChain.Clear();
			OcgCore.negatedInChain.Clear();
			OcgCore.materialCards.Clear();
			OcgCore.chainSolvingCard = null;
			foreach (GameCard gameCard2 in OcgCore.tempCards)
			{
				gameCard2.Dispose();
			}
			OcgCore.tempCards.Clear();
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B60 RID: 39776 RVA: 0x00181FD8 File Offset: 0x001801D8
		protected override async UniTask GameMessage_ChainNegated(BinaryReader reader)
		{
			byte id = reader.ReadByte();
			if ((int)id > OcgCore.cardsInChain.Count)
			{
				this.LogDebug("[Duel]: Chain index overflow.");
			}
			else
			{
				OcgCore.negatedInChain.Add((int)id);
				GameCard gameCard = OcgCore.cardsInChain[(int)(id - 1)];
				gameCard.negated = true;
				await gameCard.AnimationNegate().WaitAsync(default(CancellationToken));
			}
		}

		// Token: 0x06009B61 RID: 39777 RVA: 0x00182024 File Offset: 0x00180224
		protected override async UniTask GameMessage_ChainDisabled(BinaryReader reader)
		{
			byte id = reader.ReadByte();
			if ((int)id > OcgCore.cardsInChain.Count)
			{
				this.LogDebug("[Duel]: Chain index overflow.");
			}
			else
			{
				GameCard card = OcgCore.cardsInChain[(int)(id - 1)];
				if (!card.disabledInChain)
				{
					card.disabledInChain = true;
					await card.AnimationNegate().WaitAsync(default(CancellationToken));
				}
			}
		}

		// Token: 0x06009B62 RID: 39778 RVA: 0x00182070 File Offset: 0x00180270
		protected override async UniTask GameMessage_Attack(BinaryReader reader)
		{
			GPS from = reader.ReadGPS();
			GPS to = reader.ReadGPS();
			GameCard attackCard = base.Core.GCS_Get(from);
			if (attackCard == null)
			{
				this.DebugNoCard();
			}
			else
			{
				OcgCore.attackingCard = attackCard;
				OcgCore.ES_hint = InterString.Get("「[?]」攻击时", attackCard.GetData().Name, 0);
				GameCard attackedCard = base.Core.GCS_Get(to);
				bool finalBlow = false;
				Vector3 endPosition;
				if (attackedCard != null)
				{
					endPosition = attackedCard.model.transform.position;
					if (attackedCard.p.InPosition(CardPosition.Attack))
					{
						int diff = attackCard.GetData().Attack - attackedCard.GetData().Attack;
						if (attackCard.p.InMyControl())
						{
							if (diff >= OcgCore.life1)
							{
								finalBlow = true;
							}
						}
						else if (diff >= OcgCore.life0)
						{
							finalBlow = true;
						}
					}
				}
				else
				{
					if (attackCard.p.InMyControl())
					{
						endPosition = OcgCore.opPosition;
						if (attackCard.GetData().Attack >= OcgCore.life1)
						{
							finalBlow = true;
						}
					}
					else
					{
						endPosition = OcgCore.myPosition;
						if (attackCard.GetData().Attack >= OcgCore.life0)
						{
							finalBlow = true;
						}
					}
					global::UnityEngine.Object.Destroy(ABLoader.LoadMasterDuelGameObject("DuelDirectAtk00"), 1f);
					AudioManager.PlaySE("SE_DA_TEXT", 1f);
				}
				this.duelBGManager.ShowAttackLine(attackCard.model.transform.position, endPosition);
				if (finalBlow)
				{
					this.duelBGManager.ShowDuelFinalBlowText();
				}
				await UniTask.WaitForSeconds(0.2f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B63 RID: 39779 RVA: 0x001820BB File Offset: 0x001802BB
		protected override UniTask GameMessage_AttackDisabled(BinaryReader reader)
		{
			OcgCore.ES_hint = InterString.Get("攻击被无效时", 0);
			this.duelBGManager.HideAttackLine();
			this.duelBGManager.HideDuelFinalBlowText();
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B64 RID: 39780 RVA: 0x001820E8 File Offset: 0x001802E8
		protected override UniTask GameMessage_DamageStepStart(BinaryReader reader)
		{
			PhaseButtonHandler.SetTextBelow("03");
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B65 RID: 39781 RVA: 0x001820F9 File Offset: 0x001802F9
		protected override UniTask GameMessage_DamageStepEnd(BinaryReader reader)
		{
			PhaseButtonHandler.SetTextBelow("04");
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B66 RID: 39782 RVA: 0x0018210C File Offset: 0x0018030C
		protected override async UniTask GameMessage_Battle(BinaryReader reader)
		{
			GPS from = reader.ReadShortGPS();
			reader.ReadByte();
			GameCard attackCard = base.Core.GCS_Get(from);
			if (attackCard != null)
			{
				Card data = attackCard.GetData();
				data.Attack = reader.ReadInt32();
				data.Defense = reader.ReadInt32();
				attackCard.SetData(data);
			}
			else
			{
				reader.ReadInt32();
				reader.ReadInt32();
			}
			reader.ReadByte();
			GPS to = reader.ReadShortGPS();
			reader.ReadByte();
			GameCard attackedCard = base.Core.GCS_Get(to);
			if (attackedCard != null && to.location != 0U)
			{
				Card data2 = attackedCard.GetData();
				data2.Attack = reader.ReadInt32();
				data2.Defense = reader.ReadInt32();
				attackedCard.SetData(data2);
			}
			else
			{
				reader.ReadInt32();
				reader.ReadInt32();
			}
			reader.ReadByte();
			Transform attackTransform = attackCard.manager.GetElement<Transform>("CardPlane");
			Vector3 attackPosition = attackTransform.position;
			Vector3 attackAngle = attackTransform.eulerAngles;
			int directAttack = 0;
			Vector3 attackedPosition;
			if (attackedCard == null || to.location == 0U)
			{
				if (from.InMyControl())
				{
					attackedPosition = OcgCore.opPosition;
					directAttack = 1;
				}
				else
				{
					attackedPosition = OcgCore.myPosition;
					directAttack = -1;
				}
			}
			else
			{
				attackedPosition = attackedCard.model.transform.position;
			}
			bool flag = this.duelBGManager.IsFinalBlow();
			this.duelBGManager.HideAttackLine();
			this.duelBGManager.HideDuelFinalBlowText();
			bool flag2 = flag;
			if (flag2)
			{
				flag2 = await this.duelBGManager.NeedSpecialFinalAttackAsync(attackCard, attackedPosition);
			}
			if (!flag2)
			{
				string hit;
				string sound2;
				string tail;
				string sound;
				if (attackCard.GetData().IsAttribute(CardAttribute.Dark))
				{
					tail = "fxp_atkdak_S2_001";
					hit = "fxp_hitdak_S2_001";
					sound = "SE_ATTACK_A_DARK_SPECIAL_01";
					sound2 = "SE_ATTACK_A_DARK_SPECIAL_02";
				}
				else if (attackCard.GetData().IsAttribute(CardAttribute.Earth))
				{
					tail = "fxp_atkeah_S2_001";
					hit = "fxp_hiteah_S2_001";
					sound = "SE_ATTACK_A_EARTH_SPECIAL_01";
					sound2 = "SE_ATTACK_A_EARTH_SPECIAL_02";
				}
				else if (attackCard.GetData().IsAttribute(CardAttribute.Fire))
				{
					tail = "fxp_atkfie_S2_001";
					hit = "fxp_hitfie_S2_001";
					sound = "SE_ATTACK_A_FIRE_SPECIAL_01";
					sound2 = "SE_ATTACK_A_FIRE_SPECIAL_02";
				}
				else if (attackCard.GetData().IsAttribute(CardAttribute.Light))
				{
					tail = "fxp_atklit_S2_001";
					hit = "fxp_hitlit_S2_001";
					sound = "SE_ATTACK_A_LIGHT_SPECIAL_01";
					sound2 = "SE_ATTACK_A_LIGHT_SPECIAL_02";
				}
				else if (attackCard.GetData().IsAttribute(CardAttribute.Water))
				{
					tail = "fxp_atkwtr_S2_001";
					hit = "fxp_hitwtr_S2_001";
					sound = "SE_ATTACK_A_WATER_SPECIAL_01";
					sound2 = "SE_ATTACK_A_WATER_SPECIAL_02";
				}
				else if (attackCard.GetData().IsAttribute(CardAttribute.Wind))
				{
					tail = "fxp_atkwid_S2_001";
					hit = "fxp_hitwid_S2_001";
					sound = "SE_ATTACK_A_WIND_SPECIAL_01";
					sound2 = "SE_ATTACK_A_WIND_SPECIAL_02";
				}
				else
				{
					tail = "fxp_atkdve_S2_001";
					hit = "fxp_hitdve_S2_001";
					sound = "SE_ATTACK_A_DIVINE_SPECIAL_01";
					sound2 = "SE_ATTACK_A_DIVINE_SPECIAL_02";
				}
				if (attackCard.GetData().Attack < 2000)
				{
					tail = tail.Replace("S2", "S1");
					hit = hit.Replace("S2", "S1");
					sound = sound.Replace("SPECIAL", "DEFAULT");
					sound2 = sound2.Replace("SPECIAL", "DEFAULT");
				}
				attackTransform.LookAt(attackedPosition);
				if (directAttack == 0)
				{
					if (attackedCard.p.InPosition(CardPosition.Defence) && attackedCard.GetData().Defense >= attackCard.GetData().Attack)
					{
						hit = "fxp_hit_guard_001";
						sound2 = "SE_ATTACK_GUARD";
					}
					if (attackedCard.p.InPosition(CardPosition.Attack) && attackedCard.GetData().Attack >= attackCard.GetData().Attack)
					{
						hit = "fxp_hit_guard_001";
						sound2 = "SE_ATTACK_GUARD";
					}
				}
				else if (directAttack == 1)
				{
					hit = "fxp_dithit_far_001";
					sound2 = "SE_DIRECT_ATTACK_RIVAL";
				}
				else
				{
					hit = "fxp_dithit_near_001";
					sound2 = "SE_DIRECT_ATTACK_PLAYER";
				}
				GameObject tailObj = ABLoader.LoadMasterDuelGameObject(tail);
				tailObj.transform.SetParent(attackTransform, false);
				tailObj.SetActive(false);
				Vector3 v = attackedPosition - attackPosition;
				v.y = 0f;
				Vector3 faceAngle = attackTransform.eulerAngles;
				faceAngle.x = 0f;
				attackTransform.eulerAngles = attackAngle;
				Sequence sequence = DOTween.Sequence();
				if (attackCard.GetData().Attack < 2000)
				{
					faceAngle.z = ((faceAngle.y >= 0f && faceAngle.y < 180f) ? (-20f) : 20f);
					sequence.Append(attackTransform.DOMove(attackPosition + new Vector3(0f, 10f, 0f) - v * 0.3f, 0.3f, false).SetEase(Ease.InOutCubic).OnComplete(delegate
					{
						tailObj.SetActive(true);
						Transform[] componentsInChildren = tailObj.GetComponentsInChildren<Transform>(true);
						for (int i = 0; i < componentsInChildren.Length; i++)
						{
							componentsInChildren[i].gameObject.SetActive(true);
						}
					}));
					sequence.Join(attackTransform.DORotate(faceAngle, 0.3f, RotateMode.Fast).SetEase(Ease.InOutCubic));
					sequence.Append(attackTransform.DOMove(attackPosition + (attackedPosition - attackPosition) * 0.8f + new Vector3(0f, 0f, 0f), 0.1f, false).SetEase(Ease.InSine));
					faceAngle.z = 0f;
					sequence.Join(attackTransform.DORotate(faceAngle, 0.1f, RotateMode.Fast).SetEase(Ease.InSine));
					sequence.Join(Program.instance.camera_.cameraMain.transform.DOMove(new Vector3(0f, 95f, (float)(-37 + directAttack * 5)), 0.1f, false));
					sequence.AppendCallback(delegate
					{
						CameraManager.ShakeCamera(false);
						if (OcgCore.NextMessageIs(GameMessage.Damage))
						{
							OcgCore.NoMoreWait = true;
						}
						GameObject gameObject = ABLoader.LoadMasterDuelGameObject(hit);
						attackedPosition.y += 5f;
						gameObject.transform.position = attackedPosition;
						global::UnityEngine.Object.Destroy(gameObject, 5f);
						AudioManager.PlaySE(sound2, 1f);
					});
					sequence.AppendInterval(0.3f);
					sequence.Append(attackTransform.DOMove(attackPosition, 0.3f, false).SetEase(Ease.InQuad));
					sequence.Join(Program.instance.camera_.cameraMain.transform.DOMove(new Vector3(0f, 95f, -37f), 0.3f, false));
					sequence.Join(attackTransform.DORotate(attackAngle, 0.3f, RotateMode.Fast).SetEase(Ease.InQuad));
				}
				else
				{
					faceAngle.z = ((faceAngle.y >= 0f && faceAngle.y < 180f) ? (-30f) : 30f);
					sequence.Append(attackTransform.DOMove(attackPosition + new Vector3(0f, 10f, 0f) - v * 0.4f, 0.5f, false).SetEase(Ease.InOutCubic));
					sequence.Join(attackTransform.DORotate(faceAngle + new Vector3(45f, 0f, 0f), 0.5f, RotateMode.Fast).SetEase(Ease.InOutCubic));
					sequence.InsertCallback(0.4f, delegate
					{
						tailObj.SetActive(true);
						Transform[] componentsInChildren2 = tailObj.GetComponentsInChildren<Transform>(true);
						for (int j = 0; j < componentsInChildren2.Length; j++)
						{
							componentsInChildren2[j].gameObject.SetActive(true);
						}
					});
					sequence.Append(attackTransform.DOMove(attackPosition + (attackedPosition - attackPosition) * 0.8f + new Vector3(0f, 0f, 0f), 0.15f, false).SetEase(Ease.InSine));
					faceAngle.z = 0f;
					sequence.Join(attackTransform.DORotate(faceAngle, 0.15f, RotateMode.Fast));
					sequence.Join(Program.instance.camera_.cameraMain.transform.DOMove(new Vector3(0f, 95f, (float)(-37 + directAttack * 5)), 0.15f, false));
					sequence.AppendCallback(delegate
					{
						CameraManager.ShakeCamera(true);
						if (OcgCore.NextMessageIs(GameMessage.Damage))
						{
							OcgCore.NoMoreWait = true;
						}
						GameObject gameObject2 = ABLoader.LoadMasterDuelGameObject(hit);
						attackedPosition.y += 5f;
						gameObject2.transform.position = attackedPosition;
						global::UnityEngine.Object.Destroy(gameObject2, 5f);
						AudioManager.PlaySE(sound2, 1f);
					});
					sequence.AppendInterval(0.3f);
					sequence.Append(attackTransform.DOMove(attackPosition, 0.3f, false).SetEase(Ease.InQuad));
					sequence.Join(Program.instance.camera_.cameraMain.transform.DOMove(new Vector3(0f, 95f, -37f), 0.3f, false));
					sequence.Join(attackTransform.DORotate(attackAngle, 0.3f, RotateMode.Fast).SetEase(Ease.InQuad));
				}
				AudioManager.PlaySE(sound, 1f);
				global::UnityEngine.Object.Destroy(tailObj, 3f);
				await sequence.WaitAsync(default(CancellationToken));
			}
		}

		// Token: 0x06009B67 RID: 39783 RVA: 0x00182158 File Offset: 0x00180358
		protected override async UniTask GameMessage_Damage(BinaryReader reader)
		{
			int player = OcgCore.LocalPlayer((int)reader.ReadByte());
			int value = reader.ReadInt32();
			if (player == 0)
			{
				OcgCore.life0 -= value;
				if (OcgCore.currentMessage == GameMessage.Damage)
				{
					OcgCore.ES_hint = InterString.Get("我方受到伤害时", 0);
				}
			}
			else
			{
				OcgCore.life1 -= value;
				if (OcgCore.currentMessage == GameMessage.Damage)
				{
					OcgCore.ES_hint = InterString.Get("对方受到伤害时", 0);
				}
			}
			if (OcgCore.life0 <= 0 || OcgCore.life1 <= 0)
			{
				this.duelBGManager.FinishDamageEffect();
			}
			this.duelBGManager.UpdateBgEffects(player, false);
			AudioManager.PlaySE("SE_COST_DAMAGE", 1f);
			base.Core.SetLP(player, -value, false);
			await UniTask.WaitForSeconds(0.5f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
		}

		// Token: 0x06009B68 RID: 39784 RVA: 0x001821A4 File Offset: 0x001803A4
		protected override async UniTask GameMessage_PayLpCost(BinaryReader reader)
		{
			await this.GameMessage_Damage(reader);
		}

		// Token: 0x06009B69 RID: 39785 RVA: 0x001821F0 File Offset: 0x001803F0
		protected override async UniTask GameMessage_Recover(BinaryReader reader)
		{
			int player = OcgCore.LocalPlayer((int)reader.ReadByte());
			int value = reader.ReadInt32();
			if (player == 0)
			{
				OcgCore.life0 += value;
				OcgCore.ES_hint = InterString.Get("我方生命值回复时", 0);
			}
			else
			{
				OcgCore.life1 += value;
				OcgCore.ES_hint = InterString.Get("对方生命值回复时", 0);
			}
			base.Core.SetLP(player, value, false);
			await UniTask.WaitForSeconds(0.5f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
		}

		// Token: 0x06009B6A RID: 39786 RVA: 0x0018223C File Offset: 0x0018043C
		protected override async UniTask GameMessage_LpUpdate(BinaryReader reader)
		{
			int player = OcgCore.LocalPlayer((int)reader.ReadByte());
			int value = reader.ReadInt32();
			int diff;
			if (player == 0)
			{
				diff = value - OcgCore.life0;
				OcgCore.life0 = value;
			}
			else
			{
				diff = value - OcgCore.life1;
				OcgCore.life1 = value;
			}
			if (OcgCore.life0 <= 0 || OcgCore.life1 <= 0)
			{
				this.duelBGManager.FinishDamageEffect();
			}
			this.duelBGManager.UpdateBgEffects(player, false);
			if (diff < 0)
			{
				AudioManager.PlaySE("SE_COST_DAMAGE", 1f);
			}
			base.Core.SetLP(player, diff, false);
			await UniTask.WaitForSeconds(0.5f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
		}

		// Token: 0x06009B6B RID: 39787 RVA: 0x00182288 File Offset: 0x00180488
		protected override async UniTask GameMessage_TossCoin(BinaryReader reader)
		{
			int player = OcgCore.LocalPlayer((int)reader.ReadByte());
			byte count = reader.ReadByte();
			bool config = true;
			if (OcgCore.condition == OcgCore.Condition.Duel && !Config.GetBool("DuelCoin", true))
			{
				config = false;
			}
			if (OcgCore.condition == OcgCore.Condition.Watch && !Config.GetBool("DuelWatch", true))
			{
				config = false;
			}
			if (OcgCore.condition == OcgCore.Condition.Replay && !Config.GetBool("DuelReplay", true))
			{
				config = false;
			}
			if (config)
			{
				AudioManager.PlaySE("SE_COIN_THROW", 1f);
				int random = global::UnityEngine.Random.Range(1, 3);
				for (int i = 0; i < (int)count; i++)
				{
					GameObject gameObject = ABLoader.LoadMasterDuelGameObject("DuelCoinToss118000" + random.ToString());
					ElementObjectManager manager = gameObject.GetComponent<ElementObjectManager>();
					global::UnityEngine.Object.Destroy(gameObject, 3f);
					int x = (int)(-(count - 1) * 8) + i * 16;
					gameObject.transform.position = new Vector3((float)x, 0f, 0f);
					GameObject targetCoin;
					if (player == 0)
					{
						targetCoin = manager.GetElement("Blue");
					}
					else
					{
						targetCoin = manager.GetElement("Red");
					}
					targetCoin.SetActive(true);
					if (reader.ReadByte() == 0)
					{
						DOTween.To(delegate(float v)
						{
						}, 0f, 0f, 2f).OnComplete(delegate
						{
							AudioManager.PlaySE("SE_COIN_DECIDE_02", 1f);
						});
						Sequence sequence = DOTween.Sequence();
						sequence.AppendInterval(1.2f);
						sequence.Append(targetCoin.transform.DOLocalRotate(new Vector3(0f, 180f, 0f), 0f, RotateMode.Fast));
					}
					else
					{
						DOTween.To(delegate(float v)
						{
						}, 0f, 0f, 2f).OnComplete(delegate
						{
							AudioManager.PlaySE("SE_COIN_DECIDE", 1f);
						});
					}
				}
				await UniTask.WaitForSeconds(3f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
			else
			{
				for (int j = 0; j < (int)count; j++)
				{
					if (reader.ReadByte() == 1)
					{
						MessageManager.Cast(InterString.Get("硬币正面", 0));
					}
					else
					{
						MessageManager.Cast(InterString.Get("硬币反面", 0));
					}
				}
			}
		}

		// Token: 0x06009B6C RID: 39788 RVA: 0x001822CC File Offset: 0x001804CC
		protected override async UniTask GameMessage_TossDice(BinaryReader reader)
		{
			int player = OcgCore.LocalPlayer((int)reader.ReadByte());
			byte count = reader.ReadByte();
			bool config = true;
			if (OcgCore.condition == OcgCore.Condition.Duel && !Config.GetBool("DuelDice", true))
			{
				config = false;
			}
			if (OcgCore.condition == OcgCore.Condition.Watch && !Config.GetBool("DuelDice", true))
			{
				config = false;
			}
			if (OcgCore.condition == OcgCore.Condition.Replay && !Config.GetBool("DuelDice", true))
			{
				config = false;
			}
			if (config)
			{
				AudioManager.PlaySE("SE_DICE_ROLL", 1f);
				DOTween.To(delegate(float v)
				{
				}, 0f, 0f, 0.6f).OnComplete(delegate
				{
					AudioManager.PlaySE("SE_DICE_DECIDE", 1f);
				});
				for (int i = 0; i < (int)count; i++)
				{
					GameObject dice = ABLoader.LoadMasterDuelGameObject((player == 0) ? "DuelDice" : "DuelDiceEn");
					global::UnityEngine.Object.Destroy(dice, 2f);
					Transform diceNumber = dice.GetComponent<ElementObjectManager>().GetElement<Transform>("DiceNumber");
					switch (reader.ReadByte())
					{
					case 1:
						diceNumber.localEulerAngles = Vector3.zero;
						break;
					case 2:
						diceNumber.localEulerAngles = new Vector3(270f, 0f, 0f);
						break;
					case 3:
						diceNumber.localEulerAngles = new Vector3(0f, 0f, 270f);
						break;
					case 4:
						diceNumber.localEulerAngles = new Vector3(0f, 0f, 90f);
						break;
					case 5:
						diceNumber.localEulerAngles = new Vector3(90f, 0f, 0f);
						break;
					case 6:
						diceNumber.localEulerAngles = new Vector3(180f, 90f, 0f);
						break;
					}
					int x = (int)(-(count - 1) * 5) + i * 10;
					dice.transform.position = new Vector3((float)x, 0f, 0f);
				}
				await UniTask.WaitForSeconds(2f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
			else
			{
				for (int j = 0; j < (int)count; j++)
				{
					MessageManager.Cast(InterString.Get("骰子结果：[?]", reader.ReadByte().ToString(), 0));
				}
			}
		}

		// Token: 0x06009B6D RID: 39789 RVA: 0x00182310 File Offset: 0x00180510
		protected override async UniTask GameMessage_Draw(BinaryReader reader)
		{
			int player = OcgCore.LocalPlayer((int)reader.ReadByte());
			OcgCore.ES_hint = ((player == 0) ? InterString.Get("我方抽卡时", 0) : InterString.Get("对方抽卡时", 0));
			byte count = reader.ReadByte();
			int deckCount = base.Core.GetLocationCardCount(CardLocation.Deck, (uint)player);
			int handCount = base.Core.GetLocationCardCount(CardLocation.Hand, (uint)player);
			List<GameCard> preHands = new List<GameCard>();
			for (int i = 0; i < (int)count; i++)
			{
				GameCard card = base.Core.GCS_Get(new GPS
				{
					controller = (uint)player,
					location = 1U,
					sequence = (uint)(deckCount - 1 - i)
				});
				card.SetCode(reader.ReadInt32() & int.MaxValue);
				preHands.Add(card);
			}
			if (player == 0)
			{
				OcgCore.needRefreshMyHand = true;
				OcgCore.myPreHandCards = preHands;
			}
			else
			{
				OcgCore.needRefreshOpHand = true;
				OcgCore.opPreHandCards = preHands;
			}
			for (int j = 0; j < preHands.Count; j++)
			{
				preHands[j].MoveAsync(new GPS
				{
					controller = (uint)player,
					location = 2U,
					sequence = (uint)(handCount + j)
				}, false, 0f, 0f);
			}
			await UniTask.WaitForSeconds((player == 0) ? 0.5f : 0.3f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
		}

		// Token: 0x06009B6E RID: 39790 RVA: 0x0018235C File Offset: 0x0018055C
		protected override async UniTask GameMessage_TagSwap(BinaryReader reader)
		{
			int player = OcgCore.LocalPlayer((int)reader.ReadByte());
			if (player == 0)
			{
				if (base.Core.GetUI<OcgCoreUI>().TextPlayer0Name.text == OcgCore.name_0)
				{
					base.Core.GetUI<OcgCoreUI>().TextPlayer0Name.text = OcgCore.name_0_tag;
				}
				else
				{
					base.Core.GetUI<OcgCoreUI>().TextPlayer0Name.text = OcgCore.name_0;
				}
			}
			else if (base.Core.GetUI<OcgCoreUI>().TextPlayer1Name.text == OcgCore.name_1)
			{
				base.Core.GetUI<OcgCoreUI>().TextPlayer1Name.text = OcgCore.name_1_tag;
			}
			else
			{
				base.Core.GetUI<OcgCoreUI>().TextPlayer1Name.text = OcgCore.name_1;
			}
			base.Core.SetFace();
			int mainCount = (int)reader.ReadByte();
			int extraCount = (int)reader.ReadByte();
			int pendulumCount = (int)reader.ReadByte();
			int handsCount = (int)reader.ReadByte();
			List<GameCard> cardsInDeck = base.Core.GCS_ResizeBundle(mainCount, player, CardLocation.Deck);
			List<GameCard> cardsInExtra = base.Core.GCS_ResizeBundle(extraCount, player, CardLocation.Extra);
			List<GameCard> cardsInHand = base.Core.GCS_ResizeBundle(handsCount, player, CardLocation.Hand);
			if (cardsInDeck.Count > 0)
			{
				List<GameCard> list = cardsInDeck;
				list[list.Count - 1].SetCode(reader.ReadInt32());
			}
			for (int i = 0; i < cardsInHand.Count; i++)
			{
				cardsInHand[i].SetCode(reader.ReadInt32());
			}
			for (int j = 0; j < cardsInExtra.Count; j++)
			{
				cardsInExtra[j].SetCode(reader.ReadInt32() & int.MaxValue);
			}
			for (int k = 0; k < pendulumCount; k++)
			{
				if (cardsInExtra.Count - 1 - k > 0)
				{
					cardsInExtra[cardsInExtra.Count - 1 - k].p.position = 1;
				}
			}
			base.Core.ArrangeCards();
			OcgCore.needRefreshMyHand = true;
			OcgCore.needRefreshOpHand = true;
			this.duelBGManager.RefreshBgState();
			foreach (GameCard gameCard in cardsInHand)
			{
				gameCard.AnimationShuffle(0.15f);
				gameCard.EraseData();
			}
			await UniTask.WaitForSeconds(0.14f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			await this.PreloadUpdateCardMessages();
			await UniTask.WaitForSeconds(0.16f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
		}

		// Token: 0x06009B6F RID: 39791 RVA: 0x001823A7 File Offset: 0x001805A7
		protected override UniTask GameMessage_MatchKill(BinaryReader reader)
		{
			OcgCore.cookie_matchKill = reader.ReadInt32();
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B70 RID: 39792 RVA: 0x001823BC File Offset: 0x001805BC
		protected override UniTask GameMessage_PlayerHint(BinaryReader reader)
		{
			int player = OcgCore.LocalPlayer((int)reader.ReadByte());
			int pType = (int)reader.ReadByte();
			int num = reader.ReadInt32();
			string valString = StringHelper.Get(num);
			if (num == 38723936)
			{
				valString = InterString.Get("不能确认墓地中的卡", 0);
				if (player == 0)
				{
					if (pType == 6)
					{
						OcgCore.cantCheckGrave = true;
						base.Core.GetUI<OcgCoreUI>().CardList.Hide();
					}
					if (pType == 7)
					{
						OcgCore.cantCheckGrave = false;
					}
				}
			}
			if (pType == 6)
			{
				if (player == 0)
				{
					OcgCore.PrintDuelLog(InterString.Get("我方状态：[?]", valString, 0));
				}
				else
				{
					OcgCore.PrintDuelLog(InterString.Get("对方状态：[?]", valString, 0));
				}
			}
			else if (pType == 7)
			{
				if (player == 0)
				{
					OcgCore.PrintDuelLog(InterString.Get("我方状态结束：[?]", valString, 0));
				}
				else
				{
					OcgCore.PrintDuelLog(InterString.Get("对方状态结束：[?]", valString, 0));
				}
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B71 RID: 39793 RVA: 0x00182488 File Offset: 0x00180688
		protected override UniTask GameMessage_CardHint(BinaryReader reader)
		{
			GameCard card = base.Core.GCS_Get(reader.ReadGPS());
			int cType = (int)reader.ReadByte();
			int value = reader.ReadInt32();
			if (card == null)
			{
				this.DebugNoCard();
				return UniTask.CompletedTask;
			}
			switch (cType)
			{
			case 1:
				card.RemoveStringTail(InterString.Get("数字记录：", 0), false);
				card.AddStringTail(InterString.Get("数字记录：", 0) + value.ToString());
				break;
			case 2:
				card.RemoveStringTail(InterString.Get("卡片记录：", 0), false);
				card.AddStringTail(InterString.Get("卡片记录：", 0) + CardsManager.Get(value, false).Name);
				break;
			case 3:
				card.RemoveStringTail(InterString.Get("种族记录：", 0), false);
				card.AddStringTail(InterString.Get("种族记录：", 0) + StringHelper.Race((long)value, 0));
				break;
			case 4:
				card.RemoveStringTail(InterString.Get("属性记录：", 0), false);
				card.AddStringTail(InterString.Get("属性记录：", 0) + StringHelper.Attribute((long)value, 0));
				break;
			case 5:
				card.RemoveStringTail(InterString.Get("数字记录：", 0), false);
				card.AddStringTail(InterString.Get("数字记录：", 0) + value.ToString());
				break;
			case 6:
				card.AddStringTail(StringHelper.Get(value));
				break;
			case 7:
				card.RemoveStringTail(StringHelper.Get(value), false);
				break;
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B72 RID: 39794 RVA: 0x00182618 File Offset: 0x00180818
		protected override UniTask GameMessage_Hint(BinaryReader reader)
		{
			OcgCore.Es_selectMSGHintType = (int)reader.ReadChar();
			OcgCore.Es_selectMSGHintPlayer = OcgCore.LocalPlayer((int)reader.ReadChar());
			OcgCore.Es_selectMSGHintData = reader.ReadInt32();
			int type = OcgCore.Es_selectMSGHintType;
			int player = OcgCore.Es_selectMSGHintPlayer;
			int data = OcgCore.Es_selectMSGHintData;
			if (type == 1)
			{
				OcgCore.ES_hint = StringHelper.Get(data);
			}
			else if (type == 2)
			{
				OcgCore.PrintDuelLog(StringHelper.Get(data));
			}
			else if (type == 3)
			{
				OcgCore.ES_selectHint = StringHelper.Get(data);
			}
			else if (type == 4)
			{
				OcgCore.PrintDuelLog(InterString.Get("效果选择：[?]", StringHelper.Get(data), 0));
			}
			else if (type == 5)
			{
				OcgCore.PrintDuelLog(StringHelper.Get(data));
			}
			else if (type == 6)
			{
				OcgCore.PrintDuelLog(InterString.Get("种族选择：[?]", StringHelper.Race((long)data, 0), 0));
			}
			else if (type == 7)
			{
				OcgCore.PrintDuelLog(InterString.Get("属性选择：[?]", StringHelper.Attribute((long)data, 0), 0));
			}
			else if (type == 8)
			{
				Program.instance.message_.CastCard(data);
				OcgCore.lastDuelLog = InterString.Get("宣言卡片：[?]", CardsManager.Get(data, false).Name, 0);
			}
			else if (type == 9)
			{
				OcgCore.PrintDuelLog(InterString.Get("数字选择：[?]", data.ToString(), 0));
			}
			else if (type == 10)
			{
				Program.instance.message_.CastCard(data);
				OcgCore.lastDuelLog = InterString.Get("效果适用：[?]", CardsManager.Get(data, false).Name, 0);
			}
			else if (type == 11)
			{
				if (player == 1)
				{
					data = (data >> 16) | (data << 16);
				}
				OcgCore.PrintDuelLog(InterString.Get("区域选择：[?]", StringHelper.Zone((long)data), 0));
			}
			OcgCore.ES_selectCardFromFieldFirstFlag = type == 3 && data == 575;
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B73 RID: 39795 RVA: 0x001827D4 File Offset: 0x001809D4
		protected override async UniTask GameMessage_NewTurn(BinaryReader reader)
		{
			OcgCore.cardsInSelection.Clear();
			OcgCore.myActivated.Clear();
			OcgCore.opActivated.Clear();
			OcgCore.mySummonCount = 0;
			OcgCore.mySpSummonCount = 0;
			OcgCore.opSummonCount = 0;
			OcgCore.opSpSummonCount = 0;
			OcgCore.turns++;
			OcgCore.myTurn = (OcgCore.isFirst ? (OcgCore.turns % 2 != 0) : (OcgCore.turns % 2 == 0));
			PhaseButtonHandler.TurnChange(OcgCore.myTurn, OcgCore.turns);
			PhaseButtonHandler.SetTextMain(string.Empty);
			PhaseButtonHandler.SetTextBelow(string.Empty);
			foreach (GameCard gameCard in OcgCore.cards)
			{
				gameCard.ShowDisquiet();
			}
			this.duelBGManager.ReleaseTurnObjects();
			this.duelBGManager.SetPlayableGuide(OcgCore.myTurn);
			await this.duelBGManager.ShowTurnChangeBanner(OcgCore.myTurn ? 0 : 1);
		}

		// Token: 0x06009B74 RID: 39796 RVA: 0x00182818 File Offset: 0x00180A18
		protected override async UniTask GameMessage_NewPhase(BinaryReader reader)
		{
			this.duelBGManager.HideAttackLine();
			this.duelBGManager.HideDuelFinalBlowText();
			OcgCore.cardsInSelection.Clear();
			OcgCore.duelPhase = (DuelPhase)reader.ReadUInt16();
			int player = (OcgCore.myTurn ? 0 : 1);
			PhaseButtonHandler.SetTextBelow(string.Empty);
			if (OcgCore.duelPhase == DuelPhase.Draw)
			{
				PhaseButtonHandler.SetTextMain("Draw");
			}
			else if (OcgCore.duelPhase == DuelPhase.Standby)
			{
				PhaseButtonHandler.SetTextMain("Standby");
			}
			else if (OcgCore.duelPhase == DuelPhase.Main1)
			{
				PhaseButtonHandler.SetTextMain("Main1");
			}
			else if (OcgCore.duelPhase == DuelPhase.BattleStart)
			{
				PhaseButtonHandler.SetTextMain("Battle");
				if (OcgCore.myTurn && base.Core.GetAllAtk(true) >= OcgCore.life1)
				{
					AudioManager.PlayBgmClimax();
				}
				if (!OcgCore.myTurn && base.Core.GetAllAtk(false) >= OcgCore.life0)
				{
					AudioManager.PlayBgmClimax();
				}
			}
			else if (OcgCore.duelPhase == DuelPhase.BattleStep)
			{
				PhaseButtonHandler.SetTextBelow("01");
			}
			else if (OcgCore.duelPhase == DuelPhase.Damage)
			{
				PhaseButtonHandler.SetTextBelow("02");
			}
			else if (OcgCore.duelPhase == DuelPhase.DamageCal)
			{
				PhaseButtonHandler.SetTextBelow("03");
			}
			else if (OcgCore.duelPhase == DuelPhase.Battle)
			{
				PhaseButtonHandler.SetTextBelow(string.Empty);
			}
			else if (OcgCore.duelPhase == DuelPhase.Main2)
			{
				PhaseButtonHandler.SetTextMain("Main2");
			}
			else if (OcgCore.duelPhase == DuelPhase.End)
			{
				PhaseButtonHandler.SetTextMain("End");
			}
			await this.duelBGManager.ShowPhaseBanner(player, OcgCore.duelPhase);
		}

		// Token: 0x06009B75 RID: 39797 RVA: 0x00182864 File Offset: 0x00180A64
		protected override async UniTask GameMessage_ConfirmDecktop(BinaryReader reader)
		{
			int player = OcgCore.LocalPlayer((int)reader.ReadByte());
			byte count = reader.ReadByte();
			int countOfDeck = base.Core.GetLocationCardCount(CardLocation.Deck, (uint)player);
			for (int i = 0; i < (int)count; i++)
			{
				int code = reader.ReadInt32();
				reader.ReadShortGPS();
				GPS gps = new GPS
				{
					controller = (uint)player,
					location = 1U,
					sequence = (uint)(countOfDeck - 1 - i)
				};
				GameCard card = base.Core.GCS_Get(gps);
				if (card == null)
				{
					this.DebugNoCard();
				}
				else
				{
					card.SetCode(code);
					card.AnimationConfirmDeckTop(i);
				}
			}
			Transform camera = Program.instance.camera_.cameraMain.transform;
			Sequence sequence = DOTween.Sequence();
			if (player == 0)
			{
				sequence.Append(camera.DOLocalMove(new Vector3(0f, 95f, -40f), 0.25f, false));
			}
			else
			{
				sequence.Append(camera.DOLocalMove(new Vector3(0f, 95f, -31f), 0.25f, false));
			}
			sequence.Join(camera.DOLocalRotate(new Vector3(70f, 0f, 0f), 0.25f, RotateMode.Fast));
			sequence.AppendInterval((float)count);
			sequence.Append(camera.DOLocalMove(new Vector3(0f, 95f, -37f), 0.25f, false));
			sequence.Join(camera.DOLocalRotate(new Vector3(70f, 0f, 0f), 0.25f, RotateMode.Fast));
			await sequence.WaitAsync(default(CancellationToken));
		}

		// Token: 0x06009B76 RID: 39798 RVA: 0x001828B0 File Offset: 0x00180AB0
		protected override async UniTask GameMessage_ConfirmCards(BinaryReader reader)
		{
			reader.ReadByte();
			if (OcgCore.condition != OcgCore.Condition.Replay || OcgCore.CurrentReplayUseYRP2)
			{
				reader.ReadByte();
			}
			byte count = reader.ReadByte();
			bool listShow = false;
			if (count > 3 && OcgCore.condition == OcgCore.Condition.Duel)
			{
				listShow = true;
			}
			List<GameCard> confirmCards = new List<GameCard>();
			Sequence sequence = null;
			for (int i = 0; i < (int)count; i++)
			{
				int code = reader.ReadInt32();
				GPS gps = reader.ReadShortGPS();
				GameCard card = base.Core.GCS_Get(gps);
				if (card == null)
				{
					this.DebugNoCard();
				}
				else
				{
					card.SetCode(code);
					if (listShow)
					{
						confirmCards.Add(card);
					}
					else
					{
						sequence = card.AnimationConfirm(i);
					}
				}
			}
			if (listShow)
			{
				base.Core.GetUI<OcgCoreUI>().ShowPopupSelectCard(InterString.Get("确认卡片：[?]张。", count.ToString(), 0), confirmCards, 0, 0, true, true);
				await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
			else
			{
				await UniTask.WaitForSeconds(sequence.Duration(true) + 0.1f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B77 RID: 39799 RVA: 0x001828FC File Offset: 0x00180AFC
		protected override async UniTask GameMessage_DeckTop(BinaryReader reader)
		{
			int player = OcgCore.LocalPlayer((int)reader.ReadByte());
			int countOfDeck = base.Core.GetLocationCardCount(CardLocation.Deck, (uint)player);
			GPS gps = new GPS
			{
				controller = (uint)player,
				location = 1U,
				sequence = (uint)(countOfDeck - 1 - (int)reader.ReadByte())
			};
			int code = reader.ReadInt32();
			GameCard card = base.Core.GCS_Get(gps);
			if (card == null)
			{
				this.DebugNoCard();
			}
			else
			{
				card.SetCode(code);
				OcgCore.PrintDuelLog(InterString.Get("确认卡片：[?]", CardsManager.Get(code, false).Name, 0));
				await card.AnimationConfirm(0).WaitAsync(default(CancellationToken));
			}
		}

		// Token: 0x06009B78 RID: 39800 RVA: 0x00182948 File Offset: 0x00180B48
		protected override async UniTask GameMessage_ShuffleDeck(BinaryReader reader)
		{
			int player = OcgCore.LocalPlayer((int)reader.ReadByte());
			if (base.Core.GetLocationCardCount(CardLocation.Deck, (uint)player) > 0)
			{
				for (int i = 0; i < OcgCore.cards.Count; i++)
				{
					if (OcgCore.cards[i].gameObject.activeInHierarchy && (OcgCore.cards[i].p.location & 1U) > 0U && (ulong)OcgCore.cards[i].p.controller == (ulong)((long)player))
					{
						OcgCore.cards[i].EraseData();
					}
				}
				await this.duelBGManager.PlayShuffleDeckAsync(player);
			}
		}

		// Token: 0x06009B79 RID: 39801 RVA: 0x00182994 File Offset: 0x00180B94
		protected override async UniTask GameMessage_RefreshDeck(BinaryReader reader)
		{
			await this.GameMessage_ShuffleDeck(reader);
		}

		// Token: 0x06009B7A RID: 39802 RVA: 0x001829E0 File Offset: 0x00180BE0
		protected override async UniTask GameMessage_ShuffleHand(BinaryReader reader)
		{
			int player = OcgCore.LocalPlayer((int)reader.ReadByte());
			for (int i = 0; i < OcgCore.cards.Count; i++)
			{
				if (OcgCore.cards[i].gameObject.activeInHierarchy && (OcgCore.cards[i].p.location & 2U) > 0U && (ulong)OcgCore.cards[i].p.controller == (ulong)((long)player))
				{
					OcgCore.cards[i].AnimationShuffle(0.15f);
					OcgCore.cards[i].EraseData();
				}
			}
			Program.instance.audio_.PlayShuffleSE();
			await UniTask.WaitForSeconds(0.14f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			await this.PreloadUpdateCardMessages();
			await UniTask.WaitForSeconds(0.16f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
		}

		// Token: 0x06009B7B RID: 39803 RVA: 0x00182A2C File Offset: 0x00180C2C
		protected override UniTask GameMessage_SwapGraveDeck(BinaryReader reader)
		{
			int player = OcgCore.LocalPlayer((int)reader.ReadByte());
			foreach (GameCard c in OcgCore.cards)
			{
				if ((ulong)c.p.controller == (ulong)((long)player))
				{
					if ((c.p.location & 1U) > 0U)
					{
						c.p.location = 16U;
					}
					else if ((c.p.location & 16U) > 0U)
					{
						if (c.GetData().IsExtraCard())
						{
							c.p.location = 64U;
						}
						else
						{
							c.p.location = 1U;
						}
					}
				}
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B7C RID: 39804 RVA: 0x00182AF0 File Offset: 0x00180CF0
		protected override async UniTask GameMessage_ShuffleSetCard(BinaryReader reader)
		{
			reader.ReadByte();
			byte count = reader.ReadByte();
			List<GPS> gpss = new List<GPS>();
			List<GameCard> cardsToShuffle = new List<GameCard>();
			for (int i = 0; i < (int)count; i++)
			{
				GPS gps = reader.ReadGPS();
				gpss.Add(gps);
				GameCard card2 = base.Core.GCS_Get(gps);
				if (card2 == null)
				{
					this.DebugNoCard();
				}
				else
				{
					card2.EraseData();
					cardsToShuffle.Add(card2);
				}
			}
			for (int j = 0; j < cardsToShuffle.Count; j++)
			{
				GameCard card = cardsToShuffle[j];
				GPS newGPS = reader.ReadGPS();
				GPS oldGPS = card.p;
				oldGPS.reason = 0U;
				card.model.transform.DOLocalMove(new Vector3(0f, 5f, (float)(card.p.InMyControl() ? (-12) : 12)), 0.2f, false).OnComplete(delegate
				{
					card.MoveAsync((newGPS.location > 0U) ? newGPS : oldGPS, false, 0f, 0f);
				});
			}
			await UniTask.WaitForSeconds(0.4f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
		}

		// Token: 0x06009B7D RID: 39805 RVA: 0x00182B3B File Offset: 0x00180D3B
		protected override UniTask GameMessage_ReverseDeck(BinaryReader reader)
		{
			OcgCore.deckReserved = !OcgCore.deckReserved;
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B7E RID: 39806 RVA: 0x00182B50 File Offset: 0x00180D50
		protected override UniTask GameMessage_FieldDisabled(BinaryReader reader)
		{
			uint disabledFields = reader.ReadUInt32();
			foreach (PlaceSelector placeSelector in this.duelBGManager.places)
			{
				placeSelector.SetDisabled(disabledFields);
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B7F RID: 39807 RVA: 0x00182BB4 File Offset: 0x00180DB4
		protected override async UniTask GameMessage_RandomSelected(BinaryReader reader)
		{
			reader.ReadByte();
			byte count = reader.ReadByte();
			for (int i = 0; i < (int)count; i++)
			{
				GPS gps = reader.ReadGPS();
				GameCard card = base.Core.GCS_Get(gps);
				if (card == null)
				{
					this.DebugNoCard();
				}
				else
				{
					OcgCore.cardsBeTarget.Add(card);
					card.AnimationTarget();
					await UniTask.WaitForSeconds(0.5f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
				}
			}
		}

		// Token: 0x06009B80 RID: 39808 RVA: 0x00182C00 File Offset: 0x00180E00
		protected override async UniTask GameMessage_BecomeTarget(BinaryReader reader)
		{
			byte count = reader.ReadByte();
			for (int i = 0; i < (int)count; i++)
			{
				GPS gps = reader.ReadGPS();
				GameCard card = base.Core.GCS_Get(gps);
				if (card == null)
				{
					this.DebugNoCard();
				}
				else
				{
					OcgCore.cardsBeTarget.Add(card);
					if (OcgCore.cardsInChain.Count > 0)
					{
						List<GameCard> cardsInChain = OcgCore.cardsInChain;
						cardsInChain[cardsInChain.Count - 1].AddEffectTarget(card);
					}
					if (OcgCore.cardsInChain.Count != 0 || !card.InPendulumZone())
					{
						card.AnimationTarget();
						await UniTask.WaitForSeconds(0.5f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
					}
				}
			}
			if ((OcgCore.duelPhase == DuelPhase.Main1 || OcgCore.duelPhase == DuelPhase.Main2) && OcgCore.cardsInChain.Count == 0 && OcgCore.cardsBeTarget.Count == 2 && OcgCore.cardsBeTarget[0].InPendulumZone() && OcgCore.cardsBeTarget[1].InPendulumZone() && OcgCore.cardsBeTarget[0].p.controller == OcgCore.cardsBeTarget[1].p.controller)
			{
				OcgCore.inPendulumSummon = true;
			}
			if (OcgCore.inPendulumSummon)
			{
				if (OcgCore.condition != OcgCore.Condition.Duel || Config.GetBool("DuelPendulum", true))
				{
					if (OcgCore.condition != OcgCore.Condition.Watch || Config.GetBool("WatchPendulum", true))
					{
						if (OcgCore.condition != OcgCore.Condition.Replay || Config.GetBool("ReplayPendulum", true))
						{
							await this.duelBGManager.PlaySummonPendulum();
						}
					}
				}
			}
		}

		// Token: 0x06009B81 RID: 39809 RVA: 0x00182C4C File Offset: 0x00180E4C
		protected override UniTask GameMessage_CardTarget(BinaryReader reader)
		{
			GPS from = reader.ReadGPS();
			GPS to = reader.ReadGPS();
			GameCard cardFrom = base.Core.GCS_Get(from);
			GameCard cardTo = base.Core.GCS_Get(to);
			if (cardFrom == null || cardTo == null)
			{
				this.DebugNoCard();
				return UniTask.CompletedTask;
			}
			if (OcgCore.currentMessage == GameMessage.CardTarget)
			{
				cardFrom.AddTarget(cardTo);
			}
			else if (OcgCore.currentMessage == GameMessage.Equip)
			{
				cardFrom.equipedCard = cardTo;
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B82 RID: 39810 RVA: 0x00182CC6 File Offset: 0x00180EC6
		protected override UniTask GameMessage_Equip(BinaryReader reader)
		{
			return this.GameMessage_CardTarget(reader);
		}

		// Token: 0x06009B83 RID: 39811 RVA: 0x00182CD0 File Offset: 0x00180ED0
		protected override UniTask GameMessage_CancelTarget(BinaryReader reader)
		{
			GPS from = reader.ReadGPS();
			GameCard card = base.Core.GCS_Get(from);
			if (card == null)
			{
				this.DebugNoCard();
				return UniTask.CompletedTask;
			}
			if (OcgCore.currentMessage == GameMessage.CancelTarget)
			{
				card.targets.Clear();
			}
			else if (OcgCore.currentMessage == GameMessage.Unequip)
			{
				card.equipedCard = null;
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B84 RID: 39812 RVA: 0x00182D31 File Offset: 0x00180F31
		protected override UniTask GameMessage_Unequip(BinaryReader reader)
		{
			return this.GameMessage_CancelTarget(reader);
		}

		// Token: 0x06009B85 RID: 39813 RVA: 0x00182D3C File Offset: 0x00180F3C
		protected override UniTask GameMessage_AddCounter(BinaryReader reader)
		{
			ushort type = reader.ReadUInt16();
			GPS gps = reader.ReadShortGPS();
			GameCard card = base.Core.GCS_Get(gps);
			if (card == null)
			{
				this.DebugNoCard();
				return UniTask.CompletedTask;
			}
			ushort count = reader.ReadUInt16();
			if (OcgCore.currentMessage == GameMessage.AddCounter)
			{
				card.AddCounter((int)type, (int)count);
			}
			else if (OcgCore.currentMessage == GameMessage.RemoveCounter)
			{
				card.RemoveCounter((int)type, (int)count);
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B86 RID: 39814 RVA: 0x00182DA9 File Offset: 0x00180FA9
		protected override UniTask GameMessage_RemoveCounter(BinaryReader reader)
		{
			return this.GameMessage_AddCounter(reader);
		}

		// Token: 0x06009B87 RID: 39815 RVA: 0x00182DB2 File Offset: 0x00180FB2
		protected override UniTask GameMessage_Waiting(BinaryReader reader)
		{
			if (base.Core.InIgnoranceReplay())
			{
				return UniTask.CompletedTask;
			}
			this.duelBGManager.SetPlayableGuide(false);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009B88 RID: 39816 RVA: 0x00182DD8 File Offset: 0x00180FD8
		protected override async UniTask GameMessage_AnnounceRace(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadByte();
				OcgCore.ES_min = (int)reader.ReadByte();
				uint available = reader.ReadUInt32();
				List<string> selections = new List<string> { InterString.Get("宣言种族", 0) };
				List<int> responses = new List<int>();
				int i = 0;
				while ((long)i < 26L)
				{
					if (((ulong)available & (ulong)(1L << (i & 31))) > 0UL)
					{
						selections.Add(StringHelper.GetUnsafe(1020 + i, 0));
						responses.Add(1 << i);
					}
					i++;
				}
				base.Core.GetUI<OcgCoreUI>().ShowPopupSelection(selections, responses);
				await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B89 RID: 39817 RVA: 0x00182E24 File Offset: 0x00181024
		protected override async UniTask GameMessage_AnnounceAttrib(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadByte();
				OcgCore.ES_min = (int)reader.ReadByte();
				uint available = reader.ReadUInt32();
				List<string> selections = new List<string> { InterString.Get("宣言属性", 0) };
				List<int> responses = new List<int>();
				int i = 0;
				while ((long)i < 7L)
				{
					if (((ulong)available & (ulong)(1L << (i & 31))) > 0UL)
					{
						selections.Add(StringHelper.GetUnsafe(1010 + i, 0));
						responses.Add(1 << i);
					}
					i++;
				}
				base.Core.GetUI<OcgCoreUI>().ShowPopupSelection(selections, responses);
				await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B8A RID: 39818 RVA: 0x00182E70 File Offset: 0x00181070
		protected override async UniTask GameMessage_AnnounceNumber(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadByte();
				byte count = reader.ReadByte();
				OcgCore.ES_min = 1;
				List<string> selections = new List<string> { InterString.Get("宣言数字", 0) };
				List<int> responses = new List<int>();
				for (int i = 0; i < (int)count; i++)
				{
					selections.Add(reader.ReadUInt32().ToString());
					responses.Add(i);
				}
				base.Core.GetUI<OcgCoreUI>().ShowPopupSelection(selections, responses);
				await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B8B RID: 39819 RVA: 0x00182EBC File Offset: 0x001810BC
		protected override async UniTask GameMessage_AnnounceCard(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadByte();
				OcgCore.ES_searchCodes.Clear();
				byte count = reader.ReadByte();
				for (int i = 0; i < (int)count; i++)
				{
					OcgCore.ES_searchCodes.Add(reader.ReadInt32());
				}
				List<string> selections = new List<string>
				{
					InterString.Get("请输入关键字：", 0),
					InterString.Get("搜索", 0),
					string.Empty,
					string.Empty
				};
				base.Core.GetUI<OcgCoreUI>().ShowPopupInput(selections, new Action<string>(base.Core.OnAnnounceCard), null, InputValidation.ValidationType.None);
				await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B8C RID: 39820 RVA: 0x00182F08 File Offset: 0x00181108
		protected override async UniTask GameMessage_SelectIdleCmd(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadChar();
				byte count = reader.ReadByte();
				for (int i = 0; i < (int)count; i++)
				{
					int code = reader.ReadInt32();
					GPS gps = reader.ReadShortGPS();
					GameCard card = base.Core.GCS_Get(gps);
					if (card == null)
					{
						this.DebugNoCard();
					}
					else
					{
						card.SetCode(code);
						card.AddButton(i << 16, InterString.Get("召唤", 0), ButtonType.Summon);
					}
				}
				count = reader.ReadByte();
				for (int j = 0; j < (int)count; j++)
				{
					int code2 = reader.ReadInt32();
					GPS gps2 = reader.ReadShortGPS();
					GameCard card2 = base.Core.GCS_Get(gps2);
					if (card2 == null)
					{
						this.DebugNoCard();
					}
					else
					{
						card2.SetCode(code2);
						if (card2.InPendulumZone())
						{
							card2.AddButton((j << 16) + 1, InterString.Get("灵摆召唤", 0), ButtonType.PenSummon);
						}
						else
						{
							card2.AddButton((j << 16) + 1, InterString.Get("特殊召唤", 0), ButtonType.SpSummon);
						}
					}
				}
				count = reader.ReadByte();
				for (int k = 0; k < (int)count; k++)
				{
					int code3 = reader.ReadInt32();
					GPS gps3 = reader.ReadShortGPS();
					GameCard card3 = base.Core.GCS_Get(gps3);
					if (card3 == null)
					{
						this.DebugNoCard();
					}
					else
					{
						card3.SetCode(code3);
						if (card3.p.InPosition(CardPosition.Defence))
						{
							card3.AddButton((k << 16) + 2, InterString.Get("变为攻击表示", 0), ButtonType.ToAttackPosition);
						}
						else
						{
							card3.AddButton((k << 16) + 2, InterString.Get("变为守备表示", 0), ButtonType.ToDefensePosition);
						}
					}
				}
				count = reader.ReadByte();
				for (int l = 0; l < (int)count; l++)
				{
					int code4 = reader.ReadInt32();
					GPS gps4 = reader.ReadShortGPS();
					GameCard card4 = base.Core.GCS_Get(gps4);
					if (card4 != null)
					{
						card4.SetCode(code4);
						card4.AddButton((l << 16) + 3, InterString.Get("设置", 0), ButtonType.SetMonster);
					}
				}
				count = reader.ReadByte();
				for (int m = 0; m < (int)count; m++)
				{
					int code5 = reader.ReadInt32();
					GPS gps5 = reader.ReadShortGPS();
					GameCard card5 = base.Core.GCS_Get(gps5);
					if (card5 != null)
					{
						card5.SetCode(code5);
						card5.AddButton((m << 16) + 4, InterString.Get("设置", 0), ButtonType.SetSpell);
					}
				}
				count = reader.ReadByte();
				for (int n = 0; n < (int)count; n++)
				{
					int code6 = reader.ReadInt32();
					GPS gps6 = reader.ReadShortGPS();
					int descP = reader.ReadInt32();
					string desc = StringHelper.Get(descP);
					GameCard card6 = base.Core.GCS_Get(gps6);
					if (card6 != null)
					{
						card6.SetCode(code6);
						if (descP == 1160)
						{
							card6.AddButton((n << 16) + 5, InterString.Get("灵摆发动", 0), ButtonType.SetPendulum);
						}
						else
						{
							Effect eff = new Effect
							{
								ptr = (n << 16) + 5,
								desc = desc
							};
							card6.effects.Add(eff);
							card6.AddButton((n << 16) + 5, InterString.Get("发动效果", 0), ButtonType.Activate);
						}
					}
				}
				foreach (GameCard gameCard in OcgCore.cards)
				{
					gameCard.CreateButtons();
				}
				int buttonsCount = 0;
				foreach (GameCard c in OcgCore.cards)
				{
					buttonsCount += c.buttons.Count;
				}
				if (buttonsCount == 0)
				{
					PhaseButtonHandler.SetHint();
				}
				int num = (int)reader.ReadByte();
				byte ep = reader.ReadByte();
				reader.ReadByte();
				if (num == 1)
				{
					PhaseButtonHandler.battlePhase = true;
				}
				if (ep == 1)
				{
					PhaseButtonHandler.endPhase = true;
				}
				this.duelBGManager.ShowBgHint();
				await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B8D RID: 39821 RVA: 0x00182F54 File Offset: 0x00181154
		protected override async UniTask GameMessage_SelectBattleCmd(BinaryReader reader)
		{
			this.duelBGManager.HideAttackLine();
			this.duelBGManager.HideDuelFinalBlowText();
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadChar();
				byte count = reader.ReadByte();
				for (int i = 0; i < (int)count; i++)
				{
					int code = reader.ReadInt32();
					GPS gps = reader.ReadShortGPS();
					string desc = StringHelper.Get(reader.ReadInt32());
					GameCard card = base.Core.GCS_Get(gps);
					if (card != null)
					{
						card.SetCode(code);
						Effect eff = new Effect
						{
							ptr = i << 16,
							desc = desc
						};
						card.effects.Add(eff);
						card.AddButton(i << 16, InterString.Get("发动效果", 0), ButtonType.Activate);
					}
				}
				count = reader.ReadByte();
				for (int j = 0; j < (int)count; j++)
				{
					int code2 = reader.ReadInt32();
					GPS gps2 = reader.ReadShortGPS();
					reader.ReadByte();
					GameCard card2 = base.Core.GCS_Get(gps2);
					if (card2 != null)
					{
						card2.SetCode(code2);
						card2.AddButton((j << 16) + 1, InterString.Get("攻击", 0), ButtonType.Battle);
					}
				}
				foreach (GameCard gameCard in OcgCore.cards)
				{
					gameCard.CreateButtons();
				}
				int buttonsCount = 0;
				foreach (GameCard c in OcgCore.cards)
				{
					buttonsCount += c.buttons.Count;
				}
				if (buttonsCount == 0)
				{
					PhaseButtonHandler.SetHint();
				}
				int num = (int)reader.ReadByte();
				byte ep = reader.ReadByte();
				if (num == 1)
				{
					PhaseButtonHandler.main2Phase = true;
				}
				if (ep == 1)
				{
					PhaseButtonHandler.endPhase = true;
				}
				this.duelBGManager.ShowBgHint();
				await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B8E RID: 39822 RVA: 0x00182FA0 File Offset: 0x001811A0
		protected override async UniTask GameMessage_SelectYesNo(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadByte();
				string desc = StringHelper.Get(reader.ReadInt32());
				string title = InterString.Get("选择", 0);
				List<string> selections = new List<string>
				{
					title,
					desc,
					InterString.Get("是", 0),
					InterString.Get("否", 0)
				};
				base.Core.GetUI<OcgCoreUI>().ShowPopupYesOrNo(selections, new Action(this.<GameMessage_SelectYesNo>g__yes|82_0), new Action(this.<GameMessage_SelectYesNo>g__no|82_1));
				await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B8F RID: 39823 RVA: 0x00182FEC File Offset: 0x001811EC
		protected override async UniTask GameMessage_SelectEffectYn(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadByte();
				reader.ReadInt32();
				GPS gps = reader.ReadShortGPS();
				reader.ReadByte();
				int cr = reader.ReadInt32();
				GameCard card = base.Core.GCS_Get(gps);
				if (card == null)
				{
					this.DebugNoCard();
				}
				else
				{
					string desc = string.Empty;
					string displayname = "「" + card.GetData().Name + "」";
					Regex forReplaceFirst = new Regex("\\[%ls\\]");
					if (cr == 0)
					{
						desc = StringHelper.Get(200);
						desc = forReplaceFirst.Replace(desc, StringHelper.FormatLocation(gps), 1);
						desc = OcgCore.ES_hint + "，" + forReplaceFirst.Replace(desc, displayname, 1);
					}
					else if (cr == 221)
					{
						desc = StringHelper.Get(221);
						desc = forReplaceFirst.Replace(desc, StringHelper.FormatLocation(gps), 1);
						desc = forReplaceFirst.Replace(desc, displayname, 1);
						desc = string.Concat(new string[]
						{
							OcgCore.ES_hint,
							"，",
							desc,
							"\n",
							StringHelper.Get(223)
						});
					}
					else
					{
						desc = StringHelper.Get(cr);
						desc = forReplaceFirst.Replace(desc, displayname, 1);
					}
					List<GameCard> oneCardToSend = new List<GameCard> { card };
					base.Core.GetUI<OcgCoreUI>().ShowPopupSelectCard(desc, oneCardToSend, 1, 1, true, false);
					await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
				}
			}
		}

		// Token: 0x06009B90 RID: 39824 RVA: 0x00183038 File Offset: 0x00181238
		protected override async UniTask GameMessage_SelectChain(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadChar();
				byte count = reader.ReadByte();
				int spcount = (int)reader.ReadByte();
				reader.ReadInt32();
				reader.ReadInt32();
				List<GameCard> chainCards = new List<GameCard>();
				int forceCount = 0;
				for (int i = 0; i < (int)count; i++)
				{
					char flag = reader.ReadChar();
					byte forced = reader.ReadByte();
					forceCount += (int)forced;
					int code = reader.ReadInt32() % 1000000000;
					GPS gps = reader.ReadGPS();
					string desc = StringHelper.Get(reader.ReadInt32());
					GameCard card = base.Core.GCS_Get(gps);
					if (card == null)
					{
						card = base.Core.GCS_Create(gps, true);
					}
					if (!chainCards.Contains(card))
					{
						chainCards.Add(card);
					}
					card.SetCode(code);
					Effect eff = new Effect
					{
						flag = (int)flag,
						ptr = i,
						desc = desc,
						forced = (forced > 0)
					};
					card.effects.Add(eff);
				}
				int handleFlag = 0;
				if (forceCount == 0)
				{
					if (spcount == 0)
					{
						if (OcgCore.chainCondition == OcgCore.ChainCondition.No)
						{
							handleFlag = 0;
						}
						else if (OcgCore.chainCondition == OcgCore.ChainCondition.All)
						{
							if (chainCards.Count == 0)
							{
								handleFlag = -1;
							}
							else if (chainCards.Count == 1 && chainCards[0].effects.Count == 1)
							{
								handleFlag = 1;
							}
							else
							{
								handleFlag = 2;
							}
						}
						else if (OcgCore.chainCondition == OcgCore.ChainCondition.Smart)
						{
							handleFlag = 0;
						}
					}
					else if (chainCards.Count == 0)
					{
						handleFlag = 0;
						if (OcgCore.chainCondition == OcgCore.ChainCondition.All)
						{
							handleFlag = -1;
						}
					}
					else if (OcgCore.chainCondition == OcgCore.ChainCondition.No)
					{
						handleFlag = 0;
					}
					else if (chainCards.Count == 1 && chainCards[0].effects.Count == 1)
					{
						handleFlag = 1;
					}
					else
					{
						handleFlag = 2;
					}
				}
				else if (chainCards.Count == 1 && chainCards[0].effects.Count == 1)
				{
					handleFlag = 3;
				}
				else
				{
					handleFlag = 4;
				}
				if (handleFlag - 1 > 1)
				{
					if (handleFlag - 3 > 1)
					{
						base.Core.OnResend();
						return;
					}
					base.Core.GetUI<OcgCoreUI>().ShowPopupSelectCard(InterString.Get("[?]，请选择效果发动。", OcgCore.ES_hint, 0), chainCards, 1, 1, false, false);
				}
				else
				{
					base.Core.GetUI<OcgCoreUI>().ShowPopupSelectCard(InterString.Get("[?]，是否连锁？", OcgCore.ES_hint, 0), chainCards, 1, 1, true, false);
				}
				await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B91 RID: 39825 RVA: 0x00183084 File Offset: 0x00181284
		protected override async UniTask GameMessage_SelectCard(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadByte();
				bool cancelable = reader.ReadByte() > 0;
				OcgCore.ES_min = (int)reader.ReadByte();
				OcgCore.ES_max = (int)reader.ReadByte();
				OcgCore.ES_level = 0;
				byte count = reader.ReadByte();
				OcgCore.cardsInSelection.Clear();
				for (int i = 0; i < (int)count; i++)
				{
					int code = reader.ReadInt32();
					GPS gps = reader.ReadGPS();
					GameCard card = base.Core.GCS_Get(gps);
					if (card == null)
					{
						this.DebugNoCard();
					}
					else
					{
						card.SetCode(code);
						card.selectPtr = i;
						OcgCore.cardsInSelection.Add(card);
					}
				}
				if (OcgCore.ES_selectCardFromFieldFirstFlag && cancelable)
				{
					OcgCore.ES_selectCardFromFieldFirstFlag = false;
					base.Core.OnResend();
				}
				else if (OcgCore.ES_min == 1 && count == 1)
				{
					BinaryMaster binaryMaster = new BinaryMaster(null);
					binaryMaster.writer.Write(count);
					foreach (GameCard c2 in OcgCore.cardsInSelection)
					{
						binaryMaster.writer.Write(c2.selectPtr);
					}
					base.Core.SendReturn(binaryMaster.Get(), 0f);
				}
				else
				{
					if (OcgCore.cardsInSelection.All((GameCard c) => c.p.InLocation(CardLocation.Onfield) && !c.p.InLocation(CardLocation.Overlay)))
					{
						base.Core.FieldSelect(OcgCore.ES_selectHint, OcgCore.cardsInSelection, OcgCore.ES_min, OcgCore.ES_max, cancelable, OcgCore.ES_min == 0);
					}
					else
					{
						base.Core.GetUI<OcgCoreUI>().ShowPopupSelectCard(OcgCore.ES_selectHint, OcgCore.cardsInSelection, OcgCore.ES_min, OcgCore.ES_max, cancelable, OcgCore.ES_min == 0);
					}
					await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
				}
			}
		}

		// Token: 0x06009B92 RID: 39826 RVA: 0x001830D0 File Offset: 0x001812D0
		protected override async UniTask GameMessage_SelectUnselect(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadByte();
				bool finishable = reader.ReadByte() > 0;
				bool cancelable = reader.ReadByte() > 0 || finishable;
				OcgCore.ES_min = (int)reader.ReadByte();
				OcgCore.ES_max = (int)reader.ReadByte();
				OcgCore.ES_level = 0;
				byte count = reader.ReadByte();
				OcgCore.cardsInSelection.Clear();
				for (int i = 0; i < (int)count; i++)
				{
					int code = reader.ReadInt32();
					GPS gps = reader.ReadGPS();
					GameCard card = base.Core.GCS_Get(gps);
					if (card == null)
					{
						this.DebugNoCard();
					}
					else
					{
						card.SetCode(code);
						card.selectPtr = i;
						OcgCore.cardsInSelection.Add(card);
					}
				}
				bool flag = OcgCore.cardsInSelection.All((GameCard c) => c.p.InLocation(CardLocation.Onfield) && !c.p.InLocation(CardLocation.Overlay));
				if (!string.IsNullOrEmpty(OcgCore.ES_selectHint))
				{
					OcgCore.ES_selectUnselectHint = OcgCore.ES_selectHint;
				}
				if (string.IsNullOrEmpty(OcgCore.ES_selectUnselectHint))
				{
					OcgCore.ES_selectUnselectHint = InterString.Get("请选择卡片", 0);
				}
				if (flag)
				{
					base.Core.FieldSelect(OcgCore.ES_selectUnselectHint, OcgCore.cardsInSelection, 1, 1, cancelable, finishable);
				}
				else
				{
					base.Core.GetUI<OcgCoreUI>().ShowPopupSelectCard(OcgCore.ES_selectUnselectHint, OcgCore.cardsInSelection, 1, 1, cancelable, finishable);
				}
				await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B93 RID: 39827 RVA: 0x0018311C File Offset: 0x0018131C
		protected override async UniTask GameMessage_SelectSum(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				OcgCore.ES_overFlow = reader.ReadByte() > 0;
				reader.ReadByte();
				OcgCore.ES_level = reader.ReadInt32();
				OcgCore.ES_min = (int)reader.ReadByte();
				OcgCore.ES_max = (int)reader.ReadByte();
				if (OcgCore.ES_min < 1)
				{
					OcgCore.ES_min = 1;
				}
				if (OcgCore.ES_max < 1)
				{
					OcgCore.ES_max = 99;
				}
				OcgCore.cardsInSelection.Clear();
				OcgCore.cardsMustBeSelected.Clear();
				byte count = reader.ReadByte();
				for (int i = 0; i < (int)count; i++)
				{
					int code = reader.ReadInt32();
					GPS gps = reader.ReadShortGPS();
					int para = reader.ReadInt32();
					GameCard card = base.Core.GCS_Get(gps);
					if (card == null)
					{
						this.DebugNoCard();
					}
					else
					{
						card.SetCode(code);
						card.selectPtr = i;
						card.levelForSelect_1 = para & 65535;
						card.levelForSelect_2 = para >> 16;
						if (((long)para & (long)((ulong)(-2147483648))) > 0L)
						{
							card.levelForSelect_1 = para & int.MaxValue;
							card.levelForSelect_2 = card.levelForSelect_1;
						}
						if (card.levelForSelect_2 == 0)
						{
							card.levelForSelect_2 = card.levelForSelect_1;
						}
						OcgCore.cardsInSelection.Add(card);
						OcgCore.cardsMustBeSelected.Add(card);
					}
				}
				bool sendable = false;
				int level = 0;
				foreach (GameCard c7 in OcgCore.cardsMustBeSelected)
				{
					level += c7.levelForSelect_1;
				}
				if (level == OcgCore.ES_level)
				{
					sendable = true;
				}
				if (!sendable)
				{
					level = 0;
					foreach (GameCard c2 in OcgCore.cardsMustBeSelected)
					{
						level += c2.levelForSelect_2;
					}
					if (level == OcgCore.ES_level)
					{
						sendable = true;
					}
				}
				if (sendable)
				{
					BinaryMaster binaryMaster = new BinaryMaster(null);
					binaryMaster.writer.Write(OcgCore.cardsMustBeSelected.Count);
					for (int j = 0; j < OcgCore.cardsMustBeSelected.Count; j++)
					{
						binaryMaster.writer.Write(j);
					}
					base.Core.SendReturn(binaryMaster.Get(), 0f);
				}
				else
				{
					count = reader.ReadByte();
					for (int k = 0; k < (int)count; k++)
					{
						int code2 = reader.ReadInt32();
						GPS gps2 = reader.ReadShortGPS();
						int para2 = reader.ReadInt32();
						GameCard card2 = base.Core.GCS_Get(gps2);
						if (card2 == null)
						{
							this.DebugNoCard();
						}
						else
						{
							card2.SetCode(code2);
							card2.selectPtr = k;
							card2.levelForSelect_1 = para2 & 65535;
							card2.levelForSelect_2 = para2 >> 16;
							if (((long)para2 & (long)((ulong)(-2147483648))) > 0L)
							{
								card2.levelForSelect_1 = para2 & int.MaxValue;
								card2.levelForSelect_2 = card2.levelForSelect_1;
							}
							if (card2.levelForSelect_2 == 0)
							{
								card2.levelForSelect_2 = card2.levelForSelect_1;
							}
							OcgCore.cardsInSelection.Add(card2);
						}
					}
					level = 0;
					foreach (GameCard c3 in OcgCore.cardsInSelection)
					{
						level += c3.levelForSelect_1;
					}
					if (level == OcgCore.ES_level)
					{
						sendable = true;
					}
					if (!sendable)
					{
						level = 0;
						foreach (GameCard c4 in OcgCore.cardsInSelection)
						{
							level += c4.levelForSelect_2;
						}
						if (level == OcgCore.ES_level)
						{
							sendable = true;
						}
					}
					if (sendable)
					{
						BinaryMaster binaryMaster2 = new BinaryMaster(null);
						binaryMaster2.writer.Write((byte)OcgCore.cardsInSelection.Count);
						foreach (GameCard c5 in OcgCore.cardsMustBeSelected)
						{
							binaryMaster2.writer.Write((byte)c5.selectPtr);
						}
						foreach (GameCard c6 in OcgCore.cardsInSelection)
						{
							if (!OcgCore.cardsMustBeSelected.Contains(c6))
							{
								binaryMaster2.writer.Write((byte)c6.selectPtr);
							}
						}
						base.Core.SendReturn(binaryMaster2.Get(), 0f);
					}
					else
					{
						if (OcgCore.cardsInSelection.All((GameCard c) => c.p.InLocation(CardLocation.Onfield) && !c.p.InLocation(CardLocation.Overlay)))
						{
							base.Core.FieldSelect(OcgCore.ES_selectHint, OcgCore.cardsInSelection, OcgCore.ES_min, OcgCore.ES_max, false, false);
						}
						else
						{
							base.Core.GetUI<OcgCoreUI>().ShowPopupSelectCard(OcgCore.ES_selectHint, OcgCore.cardsInSelection, OcgCore.ES_min, OcgCore.ES_max, false, false);
						}
						await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
					}
				}
			}
		}

		// Token: 0x06009B94 RID: 39828 RVA: 0x00183168 File Offset: 0x00181368
		protected override async UniTask GameMessage_SelectTribute(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadByte();
				bool cancelable = reader.ReadByte() > 0;
				OcgCore.ES_min = (int)reader.ReadByte();
				OcgCore.ES_max = (int)reader.ReadByte();
				OcgCore.ES_level = 0;
				byte count = reader.ReadByte();
				OcgCore.cardsInSelection.Clear();
				for (int i = 0; i < (int)count; i++)
				{
					int code = reader.ReadInt32();
					GPS gps = reader.ReadShortGPS();
					GameCard card = base.Core.GCS_Get(gps);
					if (card == null)
					{
						this.DebugNoCard();
					}
					else
					{
						card.SetCode(code);
						card.selectPtr = i;
						int para = (int)reader.ReadByte();
						card.levelForSelect_1 = para;
						card.levelForSelect_2 = para;
						OcgCore.cardsInSelection.Add(card);
					}
				}
				if (OcgCore.cardsInSelection.All((GameCard c) => c.p.InLocation(CardLocation.Onfield) && !c.p.InLocation(CardLocation.Overlay)))
				{
					base.Core.FieldSelect(OcgCore.ES_selectHint, OcgCore.cardsInSelection, OcgCore.ES_min, OcgCore.ES_max, cancelable, false);
				}
				else
				{
					base.Core.GetUI<OcgCoreUI>().ShowPopupSelectCard(OcgCore.ES_selectHint, OcgCore.cardsInSelection, OcgCore.ES_min, OcgCore.ES_max, cancelable, false);
				}
				await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B95 RID: 39829 RVA: 0x001831B4 File Offset: 0x001813B4
		protected override async UniTask GameMessage_SelectOption(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadByte();
				byte count = reader.ReadByte();
				if (count > 1)
				{
					List<string> selections = new List<string> { InterString.Get("效果选择", 0) };
					List<int> responses = new List<int>();
					for (int i = 0; i < (int)count; i++)
					{
						string desc = StringHelper.Get(reader.ReadInt32());
						selections.Add(desc);
						responses.Add(i);
					}
					base.Core.GetUI<OcgCoreUI>().ShowPopupSelection(selections, responses);
					await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
				}
				else
				{
					BinaryMaster binaryMaster = new BinaryMaster(null);
					binaryMaster.writer.Write(0);
					base.Core.SendReturn(binaryMaster.Get(), 0f);
				}
			}
		}

		// Token: 0x06009B96 RID: 39830 RVA: 0x00183200 File Offset: 0x00181400
		protected override async UniTask GameMessage_SelectPlace(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadByte();
				byte min = reader.ReadByte();
				if (min == 0)
				{
					min = 1;
				}
				OcgCore.ES_min = (int)min;
				uint filter = ~reader.ReadUInt32();
				bool haveMySpellZone = false;
				bool haveOpSpellZone = false;
				foreach (PlaceSelector placeSelector in this.duelBGManager.places)
				{
					GPS p = placeSelector.HighlightThisZone(filter, (int)min);
					if (p != null && p.InLocation(CardLocation.SpellZone))
					{
						if (p.InMyControl())
						{
							haveMySpellZone = true;
						}
						else
						{
							haveOpSpellZone = true;
						}
					}
				}
				if (haveMySpellZone)
				{
					OcgCore.HideMyHandCard = true;
				}
				else if (haveOpSpellZone)
				{
					OcgCore.HideOpHandCard = true;
				}
				if (OcgCore.currentMessage == GameMessage.SelectPlace)
				{
					if (OcgCore.Es_selectMSGHintType == 3)
					{
						if (OcgCore.Es_selectMSGHintPlayer == 0)
						{
							OcgCore.ES_selectHint = InterString.Get("请为我方的「[?]」选择位置。", CardsManager.Get(OcgCore.Es_selectMSGHintData, false).Name, 0);
						}
						else
						{
							OcgCore.ES_selectHint = InterString.Get("请为对方的「[?]」选择位置。", CardsManager.Get(OcgCore.Es_selectMSGHintData, false).Name, 0);
						}
					}
				}
				else if (OcgCore.ES_selectHint == string.Empty)
				{
					OcgCore.ES_selectHint = StringHelper.GetUnsafe(570, 0);
				}
				base.Core.GetUI<OcgCoreUI>().SetHint(OcgCore.ES_selectHint);
				await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B97 RID: 39831 RVA: 0x0018324C File Offset: 0x0018144C
		protected override async UniTask GameMessage_SelectDisfield(BinaryReader reader)
		{
			await this.GameMessage_SelectPlace(reader);
		}

		// Token: 0x06009B98 RID: 39832 RVA: 0x00183298 File Offset: 0x00181498
		protected override async UniTask GameMessage_SelectPosition(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadByte();
				int code = reader.ReadInt32();
				int positions = (int)reader.ReadByte();
				int op = 1;
				int op2 = 4;
				if (positions == 1 || positions == 2 || positions == 4 || positions == 8)
				{
					BinaryMaster binaryMaster = new BinaryMaster(null);
					binaryMaster.writer.Write(positions);
					base.Core.SendReturn(binaryMaster.Get(), 0f);
				}
				else
				{
					if (positions == 13)
					{
						base.Core.GetUI<OcgCoreUI>().ShowPopupPosition(code, 3, 1, 2);
					}
					else
					{
						if ((positions & 1) > 0)
						{
							op = 1;
						}
						if ((positions & 2) > 0)
						{
							op = 2;
						}
						if ((positions & 4) > 0)
						{
							op2 = 4;
						}
						if ((positions & 8) > 0)
						{
							if ((positions & 4) > 0)
							{
								op = 4;
							}
							op2 = 8;
						}
						base.Core.GetUI<OcgCoreUI>().ShowPopupPosition(code, 2, op, op2);
					}
					await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
				}
			}
		}

		// Token: 0x06009B99 RID: 39833 RVA: 0x001832E4 File Offset: 0x001814E4
		protected override async UniTask GameMessage_SelectCounter(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				bool version1033b = (reader.BaseStream.Length - 5L) % 8L == 0L;
				reader.ReadByte();
				reader.ReadInt16();
				if (version1033b)
				{
					OcgCore.ES_min = (int)reader.ReadByte();
				}
				else
				{
					OcgCore.ES_min = (int)reader.ReadUInt16();
				}
				byte count = reader.ReadByte();
				OcgCore.cardsInSelection.Clear();
				for (int i = 0; i < (int)count; i++)
				{
					int code = reader.ReadInt32();
					GPS gps = reader.ReadShortGPS();
					GameCard card = base.Core.GCS_Get(gps);
					int pew;
					if (version1033b)
					{
						pew = (int)reader.ReadByte();
					}
					else
					{
						pew = (int)reader.ReadUInt16();
					}
					if (card != null)
					{
						card.SetCode(code);
						card.counterCanCount = pew;
						card.counterSelected = 0;
						card.selectPtr = i;
						OcgCore.cardsInSelection.Add(card);
					}
				}
				base.Core.FieldSelect(InterString.Get("请取除指示物", 0), OcgCore.cardsInSelection, OcgCore.ES_min, OcgCore.ES_min, true, false);
				await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B9A RID: 39834 RVA: 0x00183330 File Offset: 0x00181530
		protected override async UniTask GameMessage_SortChain(BinaryReader reader)
		{
			if (!base.Core.InIgnoranceReplay())
			{
				this.duelBGManager.SetPlayableGuide(true);
				reader.ReadByte();
				int ES_sortSum = 0;
				byte count = reader.ReadByte();
				List<GameCard> sortingCards = new List<GameCard>();
				for (int i = 0; i < (int)count; i++)
				{
					int code = reader.ReadInt32();
					GPS gps = reader.ReadShortGPS();
					GameCard card = base.Core.GCS_Get(gps);
					if (card != null)
					{
						card.SetCode(code);
						if (!sortingCards.Contains(card))
						{
							sortingCards.Add(card);
						}
						ES_sortSum++;
					}
				}
				base.Core.GetUI<OcgCoreUI>().ShowPopupSelectCard(InterString.Get("请为卡片排序。", 0), sortingCards, sortingCards.Count, sortingCards.Count, false, false);
				await UniTask.WaitUntil(() => this.dispatcher.playerResponed, PlayerLoopTiming.Update, default(CancellationToken), false);
			}
		}

		// Token: 0x06009B9B RID: 39835 RVA: 0x0018337C File Offset: 0x0018157C
		protected override async UniTask GameMessage_SortCard(BinaryReader reader)
		{
			await this.GameMessage_SortChain(reader);
		}

		// Token: 0x06009B9C RID: 39836 RVA: 0x001833C8 File Offset: 0x001815C8
		[CompilerGenerated]
		internal static void <GameMessage_Win>g__Action|18_0(ElementObjectManager mner)
		{
			Program.instance.texture_.LoadDummyCard(mner.GetElement<ElementObjectManager>("DummyCard"), 33396948, 0U, true, null, null);
			Program.instance.texture_.LoadDummyCard(mner.GetElement<ElementObjectManager>("DummyCard2"), 7902349, 0U, true, null, null);
			Program.instance.texture_.LoadDummyCard(mner.GetElement<ElementObjectManager>("DummyCard3"), 70903634, 0U, true, null, null);
			Program.instance.texture_.LoadDummyCard(mner.GetElement<ElementObjectManager>("DummyCard4"), 44519536, 0U, true, null, null);
			Program.instance.texture_.LoadDummyCard(mner.GetElement<ElementObjectManager>("DummyCard5"), 8124921, 0U, true, null, null);
		}

		// Token: 0x06009BA4 RID: 39844 RVA: 0x00183498 File Offset: 0x00181698
		[CompilerGenerated]
		private void <GameMessage_SelectYesNo>g__yes|82_0()
		{
			BinaryMaster binaryMaster = new BinaryMaster(null);
			binaryMaster.writer.Write(1);
			base.Core.SendReturn(binaryMaster.Get(), 0f);
		}

		// Token: 0x06009BA5 RID: 39845 RVA: 0x001834D0 File Offset: 0x001816D0
		[CompilerGenerated]
		private void <GameMessage_SelectYesNo>g__no|82_1()
		{
			BinaryMaster binaryMaster = new BinaryMaster(null);
			binaryMaster.writer.Write(0);
			base.Core.SendReturn(binaryMaster.Get(), 0f);
		}

		// Token: 0x0400D941 RID: 55617
		public readonly DuelBGManager duelBGManager;

		// Token: 0x0400D942 RID: 55618
		private bool preload;

		// Token: 0x0400D943 RID: 55619
		private Vector3 effectDefenceAngle = new Vector3(0f, 90f, 0f);
	}
}
