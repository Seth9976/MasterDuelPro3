using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.Duel
{
	// Token: 0x02001503 RID: 5379
	public class LogMessage : MessageProcessor
	{
		// Token: 0x06009C39 RID: 39993 RVA: 0x0018D15A File Offset: 0x0018B35A
		public LogMessage(MessageDispatcher dispatcher)
			: base(dispatcher)
		{
		}

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x06009C3A RID: 39994 RVA: 0x0018D179 File Offset: 0x0018B379
		private DuelLog DuelLog
		{
			get
			{
				return base.Core.GetUI<OcgCoreUI>().DuelLog;
			}
		}

		// Token: 0x06009C3B RID: 39995 RVA: 0x001810FF File Offset: 0x0017F2FF
		private void LogDebug(string text)
		{
			Debug.LogError(text);
			MessageManager.Cast(text);
		}

		// Token: 0x06009C3C RID: 39996 RVA: 0x0018D18B File Offset: 0x0018B38B
		private void DebugNoCard()
		{
			this.LogDebug(string.Format("[Duel Log]: Not found card for {0}.", OcgCore.currentMessage));
		}

		// Token: 0x06009C3D RID: 39997 RVA: 0x0018D1A7 File Offset: 0x0018B3A7
		private void ResetState()
		{
			LogMessage.chainSolvingIndex = 0;
			this.inPendulumSummon = false;
			this.cardsInChain.Clear();
			this.cardsBeTarget.Clear();
		}

		// Token: 0x06009C3E RID: 39998 RVA: 0x0018D1CC File Offset: 0x0018B3CC
		private int LocalPlayer(int player)
		{
			if (player != 0 && player != 1)
			{
				return player;
			}
			if (this.isFirst)
			{
				return player;
			}
			return 1 - player;
		}

		// Token: 0x06009C3F RID: 39999 RVA: 0x0018D1E4 File Offset: 0x0018B3E4
		protected override UniTask GameMessage_Start(BinaryReader reader)
		{
			this.ResetState();
			this.isFirst = (reader.ReadByte() & 15) == 0;
			if (reader.BaseStream.Length > 17L)
			{
				this.MasterRule = (int)(reader.ReadByte() + 1);
			}
			LogMessage.life0 = reader.ReadInt32();
			LogMessage.life1 = reader.ReadInt32();
			this.turns = 0;
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C40 RID: 40000 RVA: 0x0018D24C File Offset: 0x0018B44C
		protected override UniTask GameMessage_ReloadField(BinaryReader reader)
		{
			this.ResetState();
			if (OcgCore.inPuzzle)
			{
				this.isFirst = true;
				this.myTurn = true;
			}
			this.MasterRule = (int)(reader.ReadByte() + 1);
			if (this.MasterRule > 255)
			{
				this.MasterRule -= 255;
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C41 RID: 40001 RVA: 0x0018D2A8 File Offset: 0x0018B4A8
		protected override UniTask GameMessage_NewTurn(BinaryReader reader)
		{
			this.turns++;
			this.myTurn = (this.isFirst ? (this.turns % 2 != 0) : (this.turns % 2 == 0));
			GameObject item = ABLoader.LoadMasterDuelGameObject("DuelLogNewTurn");
			item.transform.GetChild(1).GetComponent<Image>().color = (this.myTurn ? DuelLog.myColor : DuelLog.opColor);
			item.transform.GetChild(2).GetComponent<Text>().text = InterString.Get("第[?]回合", this.turns.ToString(), 0);
			item.transform.GetChild(3).GetComponent<Image>().material = base.Core.GetUI<OcgCoreUI>().AvatarPlayer0.material;
			item.transform.GetChild(4).GetComponent<Image>().material = base.Core.GetUI<OcgCoreUI>().AvatarPlayer1.material;
			item.transform.GetChild(3).GetComponent<Image>().sprite = base.Core.GetUI<OcgCoreUI>().AvatarPlayer0.sprite;
			item.transform.GetChild(4).GetComponent<Image>().sprite = base.Core.GetUI<OcgCoreUI>().AvatarPlayer1.sprite;
			item.transform.GetChild(7).GetComponent<Text>().text = LogMessage.life0.ToString();
			item.transform.GetChild(8).GetComponent<Text>().text = LogMessage.life1.ToString();
			this.DuelLog.AddLog(item, false);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C42 RID: 40002 RVA: 0x0018D448 File Offset: 0x0018B648
		protected override UniTask GameMessage_NewPhase(BinaryReader reader)
		{
			this.duelPhase = (DuelPhase)reader.ReadInt16();
			DuelPhase duelPhase = this.duelPhase;
			string text;
			if (duelPhase <= DuelPhase.Battle)
			{
				switch (duelPhase)
				{
				case DuelPhase.Draw:
					text = InterString.Get("抽卡阶段", 0);
					goto IL_00A9;
				case DuelPhase.Standby:
					text = InterString.Get("准备阶段", 0);
					goto IL_00A9;
				case (DuelPhase)3:
					break;
				case DuelPhase.Main1:
					text = InterString.Get("主要阶段1", 0);
					goto IL_00A9;
				default:
					if (duelPhase == DuelPhase.Battle)
					{
						text = InterString.Get("战斗阶段", 0);
						goto IL_00A9;
					}
					break;
				}
			}
			else
			{
				if (duelPhase == DuelPhase.Main2)
				{
					text = InterString.Get("主要阶段2", 0);
					goto IL_00A9;
				}
				if (duelPhase == DuelPhase.End)
				{
					text = InterString.Get("结束阶段", 0);
					goto IL_00A9;
				}
			}
			text = string.Empty;
			IL_00A9:
			string textPhase = text;
			if (textPhase != string.Empty)
			{
				GameObject item = ABLoader.LoadMasterDuelGameObject("DuelLogNewPhase");
				item.transform.GetChild(1).GetComponent<Text>().text = textPhase;
				this.DuelLog.AddLog(item, false);
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C43 RID: 40003 RVA: 0x0018D544 File Offset: 0x0018B744
		protected override UniTask GameMessage_Move(BinaryReader reader)
		{
			int code = (int)reader.ReadUInt32();
			GPS from = reader.ReadGPS();
			GPS to = reader.ReadGPS();
			uint reason = reader.ReadUInt32();
			LogMessage.lastMoveReason = reason;
			GameCard card = base.Core.GCS_Get(from);
			if (card == null)
			{
				if (from.location != 0U)
				{
					Debug.Log(string.Format("[Duel Log]: Unexpect no GameCard, code: {0}, moveReason: {1}", code, reason));
				}
				return UniTask.CompletedTask;
			}
			this.lastMoveCard = card;
			Card data;
			if (code > 0)
			{
				data = CardsManager.Get(code, false);
			}
			else
			{
				data = card.GetData();
			}
			if (to.InPosition(CardPosition.FaceUp) && to.InLocation(CardLocation.MonsterZone) && from.InLocation(CardLocation.Hand) && !to.InLocation(CardLocation.Overlay))
			{
				return UniTask.CompletedTask;
			}
			if ((reason & 2048U) > 0U && to.InLocation(CardLocation.MonsterZone) && !from.InLocation(CardLocation.MonsterZone) && !to.InLocation(CardLocation.Overlay))
			{
				this.lastSpSummonReason = reason;
				return UniTask.CompletedTask;
			}
			if (from.InLocation(CardLocation.Hand) && to.InLocation(CardLocation.SpellZone) && to.InPosition(CardPosition.FaceUp) && !data.HasType(CardType.Monster))
			{
				return UniTask.CompletedTask;
			}
			if (from.InLocation(CardLocation.Hand) && to.InLocation(CardLocation.SpellZone) && to.InPosition(CardPosition.FaceUp) && data.HasType(CardType.Pendulum) && to.InPendulumSequence())
			{
				return UniTask.CompletedTask;
			}
			if (from.InLocation(CardLocation.SpellZone) && !to.InLocation(CardLocation.SpellZone) && !data.HasType(CardType.Monster) && (reason & 1024U) > 0U)
			{
				return UniTask.CompletedTask;
			}
			if (from.InLocation(CardLocation.Overlay) && to.InLocation(CardLocation.Overlay))
			{
				return UniTask.CompletedTask;
			}
			if (from.InLocation(CardLocation.Deck) && to.InLocation(CardLocation.Deck))
			{
				return UniTask.CompletedTask;
			}
			if (to.InLocation(CardLocation.Onfield) && to.InPosition(CardPosition.FaceDown))
			{
				return UniTask.CompletedTask;
			}
			bool indent = false;
			string textReason;
			if ((reason & 128U) > 0U)
			{
				textReason = InterString.Get("代价", 0);
				indent = true;
			}
			else if ((reason & 1U) > 0U)
			{
				textReason = InterString.Get("破坏", 0);
			}
			else if ((reason & 2U) > 0U)
			{
				textReason = InterString.Get("解放", 0);
			}
			else if ((reason & 32U) > 0U)
			{
				textReason = InterString.Get("战斗破坏", 0);
			}
			else if ((reason & 8192U) > 0U)
			{
				textReason = InterString.Get("反转", 0);
			}
			else if (to.InLocation(CardLocation.Hand))
			{
				textReason = InterString.Get("回到", 0);
				if (from.InLocation(CardLocation.Deck, CardLocation.Extra))
				{
					textReason = InterString.Get("加入", 0);
				}
				if (from.InLocation(CardLocation.Grave, CardLocation.Removed))
				{
					textReason = InterString.Get("回收", 0);
				}
			}
			else if (to.InLocation(CardLocation.SpellZone) && !from.InLocation(CardLocation.SpellZone) && !from.InLocation(CardLocation.Hand))
			{
				textReason = InterString.Get("放置", 0);
			}
			else if (from.InLocation(CardLocation.SpellZone) && to.InLocation(CardLocation.SpellZone))
			{
				textReason = InterString.Get("移动", 0);
			}
			else if (from.InLocation(CardLocation.MonsterZone) && to.InLocation(CardLocation.MonsterZone))
			{
				textReason = InterString.Get("移动", 0);
			}
			else if (to.InLocation(CardLocation.MonsterZone) && !from.InLocation(CardLocation.MonsterZone))
			{
				textReason = InterString.Get("回到", 0);
			}
			else
			{
				textReason = InterString.Get("送至", 0);
			}
			if (data.HasType(CardType.Token))
			{
				this.DuelLog.AddSingleCardMessageToLog(data.Id, null, from, textReason, indent);
			}
			else
			{
				this.DuelLog.AddSingleCardMessageToLog(data.Id, from, to, textReason, indent);
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C44 RID: 40004 RVA: 0x0018D8D0 File Offset: 0x0018BAD0
		protected override UniTask GameMessage_Summoning(BinaryReader reader)
		{
			int code = (int)reader.ReadUInt32();
			GPS gps = reader.ReadGPS();
			GameCard card = base.Core.GCS_Get(gps);
			if (card == null)
			{
				this.DebugNoCard();
				return UniTask.CompletedTask;
			}
			this.lastConfirmedCard = card;
			Card data = card.GetData();
			string textReason;
			if (OcgCore.currentMessage == GameMessage.Summoning)
			{
				textReason = InterString.Get("召唤", 0);
			}
			else if (OcgCore.currentMessage == GameMessage.SpSummoning)
			{
				if ((this.lastSpSummonReason & 1048576U) > 0U)
				{
					textReason = InterString.Get("仪式召唤", 0);
				}
				else if ((this.lastSpSummonReason & 262144U) > 0U)
				{
					textReason = InterString.Get("融合召唤", 0);
				}
				else if ((this.lastSpSummonReason & 524288U) > 0U)
				{
					textReason = InterString.Get("同调召唤", 0);
				}
				else if ((this.lastSpSummonReason & 2097152U) > 0U)
				{
					textReason = InterString.Get("超量召唤", 0);
				}
				else if ((this.lastSpSummonReason & 268435456U) > 0U)
				{
					textReason = InterString.Get("连接召唤", 0);
				}
				else if ((this.lastSpSummonReason & 4194304U) > 0U)
				{
					textReason = InterString.Get("灵摆召唤", 0);
				}
				else
				{
					textReason = InterString.Get("特殊召唤", 0);
				}
			}
			else if (OcgCore.currentMessage == GameMessage.FlipSummoning)
			{
				textReason = InterString.Get("反转召唤", 0);
			}
			else
			{
				textReason = InterString.Get("送至", 0);
			}
			if (data.HasType(CardType.Token))
			{
				this.DuelLog.AddSingleCardMessageToLog(code, null, gps, textReason, false);
			}
			else
			{
				this.DuelLog.AddSingleCardMessageToLog(code, card.p, gps, textReason, false);
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C45 RID: 40005 RVA: 0x0018DA60 File Offset: 0x0018BC60
		protected override UniTask GameMessage_SpSummoning(BinaryReader reader)
		{
			return this.GameMessage_Summoning(reader);
		}

		// Token: 0x06009C46 RID: 40006 RVA: 0x0018DA60 File Offset: 0x0018BC60
		protected override UniTask GameMessage_FlipSummoning(BinaryReader reader)
		{
			return this.GameMessage_Summoning(reader);
		}

		// Token: 0x06009C47 RID: 40007 RVA: 0x0018DA6C File Offset: 0x0018BC6C
		protected override UniTask GameMessage_Set(BinaryReader reader)
		{
			GameCard card = this.lastMoveCard;
			if (card == null)
			{
				this.DebugNoCard();
				return UniTask.CompletedTask;
			}
			string textReason = InterString.Get("盖放", 0);
			this.DuelLog.AddSingleCardMessageToLog(card.GetData().Id, card.cacheP, card.p, textReason, false);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C48 RID: 40008 RVA: 0x0018DACC File Offset: 0x0018BCCC
		protected override UniTask GameMessage_Chaining(BinaryReader reader)
		{
			int code = (int)reader.ReadUInt32();
			GPS gps = reader.ReadGPS();
			GameCard card = base.Core.GCS_Get(gps);
			if (card == null)
			{
				this.DebugNoCard();
				return UniTask.CompletedTask;
			}
			Card data = card.GetData();
			this.cardsInChain.Add(card);
			if (this.cardsInChain.Count > 1)
			{
				GameObject item = ABLoader.LoadMasterDuelGameObject("DuelLogChaining");
				item.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
				{
					this.Core.GetUI<OcgCoreUI>().CardDescription.Show(card, null, -1, null);
				});
				item.transform.GetChild(1).GetChild(0).GetComponent<Text>()
					.text = this.cardsInChain.Count.ToString();
				item.transform.GetChild(1).GetChild(0).GetComponent<Text>()
					.color = ((card.p.controller == 0U) ? DuelLog.myChainColor : DuelLog.opChainColor);
				item.transform.GetChild(2).GetComponent<Text>().text = InterString.Get("连锁", 0);
				Program.instance.texture_.LoadCardToRawImageWithoutMaterialAsync(item.transform.GetChild(3).GetComponent<RawImage>(), code, true);
				this.DuelLog.AddLog(item, false);
			}
			string textReason = InterString.Get("发动", 0);
			if (card.cacheP != null && card.cacheP.InLocation(CardLocation.Hand) && gps.InLocation(CardLocation.SpellZone) && gps.InPosition(CardPosition.FaceUp) && data.HasType(CardType.Pendulum) && gps.InPendulumSequence() && card == this.lastMoveCard && card != this.lastConfirmedCard)
			{
				textReason = InterString.Get("灵摆发动", 0);
			}
			if (card == this.lastMoveCard && card != this.lastConfirmedCard && gps.InLocation(CardLocation.SpellZone) && card.cacheP.InLocation(CardLocation.Hand))
			{
				this.lastConfirmedCard = card;
				this.DuelLog.AddSingleCardMessageToLog(code, card.cacheP, gps, textReason, false);
			}
			else
			{
				this.DuelLog.AddSingleCardMessageToLog(code, null, gps, textReason, false);
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C49 RID: 40009 RVA: 0x0018DD4C File Offset: 0x0018BF4C
		protected override UniTask GameMessage_ChainNegated(BinaryReader reader)
		{
			byte index = reader.ReadByte();
			if ((int)index > this.cardsInChain.Count)
			{
				this.LogDebug("[Duel Log]: Chain index overflow.");
				return UniTask.CompletedTask;
			}
			GameCard card = this.cardsInChain[(int)(index - 1)];
			if (card == null)
			{
				this.DebugNoCard();
				return UniTask.CompletedTask;
			}
			string textReason = InterString.Get("发动无效", 0);
			if (OcgCore.currentMessage == GameMessage.ChainDisabled)
			{
				textReason = InterString.Get("效果无效", 0);
				if (card.negated)
				{
					return UniTask.CompletedTask;
				}
			}
			this.DuelLog.AddSingleCardMessageToLog(card.GetData().Id, null, card.p, textReason, false);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C4A RID: 40010 RVA: 0x0018DDF6 File Offset: 0x0018BFF6
		protected override UniTask GameMessage_ChainDisabled(BinaryReader reader)
		{
			return this.GameMessage_ChainNegated(reader);
		}

		// Token: 0x06009C4B RID: 40011 RVA: 0x0018DE00 File Offset: 0x0018C000
		protected override UniTask GameMessage_Draw(BinaryReader reader)
		{
			int player = this.LocalPlayer((int)reader.ReadByte());
			byte count = reader.ReadByte();
			GPS gps = new GPS
			{
				location = 2U,
				controller = (uint)player
			};
			List<int> codes = new List<int>();
			for (int i = 0; i < (int)count; i++)
			{
				int code = reader.ReadInt32() & int.MaxValue;
				codes.Add(code);
			}
			if (codes.All((int x) => x == 0))
			{
				string textReason = InterString.Get("抽卡", 0) + " x " + count.ToString();
				this.DuelLog.AddSingleCardMessageToLog(0, null, gps, textReason, false);
			}
			else
			{
				for (int j = 0; j < (int)count; j++)
				{
					int code2 = codes[j];
					string textReason2 = InterString.Get("抽卡", 0);
					this.DuelLog.AddSingleCardMessageToLog(code2, null, gps, textReason2, false);
				}
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C4C RID: 40012 RVA: 0x0018DEF8 File Offset: 0x0018C0F8
		protected override UniTask GameMessage_RandomSelected(BinaryReader reader)
		{
			reader.ReadByte();
			byte count = reader.ReadByte();
			GameObject item = ABLoader.LoadMasterDuelGameObject((this.cardsInChain.Count > 0) ? "DuelLogText2" : "DuelLogText");
			item.transform.GetChild(1).GetComponent<Text>().text = InterString.Get("对象", 0);
			this.DuelLog.AddLog(item, false);
			for (int i = 0; i < (int)count; i++)
			{
				GPS tempGPS = reader.ReadGPS();
				GameCard card = base.Core.GCS_Get(tempGPS);
				if (card == null)
				{
					this.DebugNoCard();
				}
				else
				{
					this.cardsBeTarget.Add(card);
					this.DuelLog.AddSingleCardMessageToLog(card.GetData().Id, null, tempGPS, string.Empty, true);
				}
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C4D RID: 40013 RVA: 0x0018DFC8 File Offset: 0x0018C1C8
		protected override UniTask GameMessage_BecomeTarget(BinaryReader reader)
		{
			byte count = reader.ReadByte();
			List<GameCard> tempList = new List<GameCard>();
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
					tempList.Add(card);
					this.cardsBeTarget.Add(card);
					if ((this.duelPhase == DuelPhase.Main1 || this.duelPhase == DuelPhase.Main2) && this.cardsInChain.Count == 0 && this.cardsBeTarget.Count == 2 && this.cardsBeTarget[0].InPendulumZone() && this.cardsBeTarget[1].InPendulumZone() && this.cardsBeTarget[0].p.controller == this.cardsBeTarget[1].p.controller)
					{
						this.inPendulumSummon = true;
					}
				}
			}
			if (this.cardsInChain.Count == 0 && this.cardsBeTarget.Count == 1 && this.cardsBeTarget[0].InPendulumZone())
			{
				return UniTask.CompletedTask;
			}
			if (this.inPendulumSummon)
			{
				return UniTask.CompletedTask;
			}
			GameObject item = ABLoader.LoadMasterDuelGameObject((this.cardsInChain.Count > 0) ? "DuelLogText2" : "DuelLogText");
			item.transform.GetChild(1).GetComponent<Text>().text = InterString.Get("对象", 0);
			this.DuelLog.AddLog(item, false);
			for (int j = 0; j < tempList.Count; j++)
			{
				this.DuelLog.AddSingleCardMessageToLog(tempList[j].GetData().Id, null, tempList[j].p, string.Empty, true);
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C4E RID: 40014 RVA: 0x0018E19C File Offset: 0x0018C39C
		protected override UniTask GameMessage_AttackDisabled(BinaryReader reader)
		{
			if (this.attackingCard == null)
			{
				this.DebugNoCard();
				return UniTask.CompletedTask;
			}
			string textReason = InterString.Get("攻击被无效", 0);
			this.DuelLog.AddSingleCardMessageToLog(this.attackingCard.GetData().Id, null, this.attackingCard.p, textReason, false);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C4F RID: 40015 RVA: 0x0018E200 File Offset: 0x0018C400
		protected override UniTask GameMessage_ConfirmDecktop(BinaryReader reader)
		{
			this.LocalPlayer((int)reader.ReadByte());
			byte count = reader.ReadByte();
			for (int i = 0; i < (int)count; i++)
			{
				int code = reader.ReadInt32();
				GPS gps = reader.ReadShortGPS();
				string textReason = InterString.Get("公开卡组", 0);
				this.DuelLog.AddSingleCardMessageToLog(code, null, gps, textReason, false);
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C50 RID: 40016 RVA: 0x0018E260 File Offset: 0x0018C460
		protected override UniTask GameMessage_ConfirmCards(BinaryReader reader)
		{
			UniTask uniTask;
			try
			{
				uniTask = this.GameMessage_ConfirmCardsForTry(reader);
			}
			catch
			{
				OcgCore.CurrentReplayUseYRP2 = !OcgCore.CurrentReplayUseYRP2;
				uniTask = this.GameMessage_ConfirmCardsForTry(reader);
			}
			return uniTask;
		}

		// Token: 0x06009C51 RID: 40017 RVA: 0x0018E2A0 File Offset: 0x0018C4A0
		private UniTask GameMessage_ConfirmCardsForTry(BinaryReader reader)
		{
			reader.BaseStream.Position = 0L;
			reader.ReadByte();
			if (OcgCore.condition != OcgCore.Condition.Replay || OcgCore.CurrentReplayUseYRP2)
			{
				reader.ReadByte();
			}
			byte count = reader.ReadByte();
			if (count == 0)
			{
				throw new Exception();
			}
			for (int i = 0; i < (int)count; i++)
			{
				int code = reader.ReadInt32();
				GPS gps = reader.ReadShortGPS();
				string textReason = InterString.Get("公开", 0);
				if (gps.InLocation(CardLocation.Hand))
				{
					textReason = InterString.Get("公开手卡", 0);
				}
				else if (gps.InLocation(CardLocation.Onfield))
				{
					textReason = InterString.Get("公开盖卡", 0);
				}
				this.DuelLog.AddSingleCardMessageToLog(code, null, gps, textReason, false);
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C52 RID: 40018 RVA: 0x0018E354 File Offset: 0x0018C554
		protected override UniTask GameMessage_PosChange(BinaryReader reader)
		{
			int code = reader.ReadInt32();
			GPS from = reader.ReadGPS();
			string textReason = InterString.Get("更改表示形式", 0);
			if (from.InLocation(CardLocation.SpellZone) && from.InPosition(CardPosition.FaceUp))
			{
				textReason = InterString.Get("盖放", 0);
			}
			if (from.InLocation(CardLocation.SpellZone) && from.InPosition(CardPosition.FaceDown))
			{
				textReason = InterString.Get("盖放", 0);
			}
			if (code == 0)
			{
				GameCard card = base.Core.GCS_Get(from);
				if (card != null)
				{
					code = card.GetData().Id;
				}
			}
			GPS to = from;
			to.position = (int)reader.ReadByte();
			this.DuelLog.AddSingleCardMessageToLog(code, null, to, textReason, false);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C53 RID: 40019 RVA: 0x0018E404 File Offset: 0x0018C604
		protected override UniTask GameMessage_Swap(BinaryReader reader)
		{
			int code = reader.ReadInt32();
			GPS from = reader.ReadGPS();
			int code2 = reader.ReadInt32();
			GPS to = reader.ReadGPS();
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
			string textReason = InterString.Get((from.controller == to.controller) ? "移动" : "转移控制权", 0);
			this.DuelLog.AddSingleCardMessageToLog(code, from, to2, textReason, false);
			this.DuelLog.AddSingleCardMessageToLog(code2, to, from2, textReason, false);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C54 RID: 40020 RVA: 0x0018E4E8 File Offset: 0x0018C6E8
		protected override UniTask GameMessage_ChainSolving(BinaryReader reader)
		{
			LogMessage.chainSolvingIndex = (int)reader.ReadByte();
			if (LogMessage.chainSolvingIndex > this.cardsInChain.Count)
			{
				this.LogDebug("[Duel Log]: Chain index overflow.");
				return UniTask.CompletedTask;
			}
			GameCard card = this.cardsInChain[LogMessage.chainSolvingIndex - 1];
			if (card == null)
			{
				this.DebugNoCard();
				return UniTask.CompletedTask;
			}
			GameObject item = ABLoader.LoadMasterDuelGameObject("DuelLogChaining");
			item.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
			{
				this.Core.GetUI<OcgCoreUI>().CardDescription.Show(card, null, -1, null);
			});
			item.transform.GetChild(1).GetChild(0).GetComponent<Text>()
				.text = LogMessage.chainSolvingIndex.ToString();
			item.transform.GetChild(1).GetChild(0).GetComponent<Text>()
				.color = ((card.p.controller == 0U) ? DuelLog.myChainColor : DuelLog.opChainColor);
			item.transform.GetChild(2).GetComponent<Text>().text = InterString.Get("效果处理", 0);
			Program.instance.texture_.LoadCardToRawImageWithoutMaterialAsync(item.transform.GetChild(3).GetComponent<RawImage>(), card.GetData().Id, true);
			this.DuelLog.AddLog(item, false);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C55 RID: 40021 RVA: 0x0018E65C File Offset: 0x0018C85C
		protected override UniTask GameMessage_ChainEnd(BinaryReader reader)
		{
			this.cardsInChain.Clear();
			this.cardsBeTarget.Clear();
			GameObject item = ABLoader.LoadMasterDuelGameObject("DuelLogChaining");
			item.transform.GetChild(1).gameObject.SetActive(false);
			item.transform.GetChild(3).gameObject.SetActive(false);
			string textReason = InterString.Get((LogMessage.chainSolvingIndex > 1) ? "连锁结束" : "处理结束", 0);
			item.transform.GetChild(2).GetComponent<Text>().text = textReason;
			LogMessage.chainSolvingIndex = 0;
			this.DuelLog.AddLog(item, false);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C56 RID: 40022 RVA: 0x0018E704 File Offset: 0x0018C904
		protected override UniTask GameMessage_Attack(BinaryReader reader)
		{
			GPS from = reader.ReadGPS();
			GPS to = reader.ReadGPS();
			this.attackingCard = base.Core.GCS_Get(from);
			if (this.attackingCard == null)
			{
				this.DebugNoCard();
				return UniTask.CompletedTask;
			}
			int code = this.attackingCard.GetData().Id;
			GameObject item = ABLoader.LoadMasterDuelGameObject("DuelLogAttack");
			Color targetColor = (from.InMyControl() ? DuelLog.myColor : DuelLog.opColor);
			targetColor.a = 0.75f;
			item.transform.GetChild(1).GetComponent<Image>().color = targetColor;
			item.transform.GetChild(2).GetComponent<Text>().text = InterString.Get("攻击", 0);
			RawImage cardFace = item.transform.GetChild(3).GetComponent<RawImage>();
			Program.instance.texture_.LoadCardToRawImageWithoutMaterialAsync(cardFace, code, true);
			if (from.InPosition(CardPosition.Defence))
			{
				cardFace.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
			}
			cardFace.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
			{
				this.Core.GetUI<OcgCoreUI>().CardDescription.Show(null, null, code, from);
			});
			List<Sprite> icons = TextureManager.container.GetLocationIcons(from);
			item.transform.GetChild(4).GetComponent<Image>().sprite = icons[0];
			targetColor = (from.InMyControl() ? DuelLog.myArrowColor : DuelLog.opArrowColor);
			item.transform.GetChild(5).GetComponent<Image>().color = targetColor;
			GameCard attackedCard = base.Core.GCS_Get(to);
			if (attackedCard == null)
			{
				item.transform.GetChild(6).gameObject.SetActive(false);
				item.transform.GetChild(7).gameObject.SetActive(false);
				item.transform.GetChild(8).GetComponent<Image>().material = (from.InMyControl() ? base.Core.GetUI<OcgCoreUI>().AvatarPlayer1.material : base.Core.GetUI<OcgCoreUI>().AvatarPlayer0.material);
				item.transform.GetChild(8).GetComponent<Image>().sprite = (from.InMyControl() ? base.Core.GetUI<OcgCoreUI>().AvatarPlayer1.sprite : base.Core.GetUI<OcgCoreUI>().AvatarPlayer0.sprite);
			}
			else
			{
				item.transform.GetChild(8).gameObject.SetActive(false);
				icons = TextureManager.container.GetLocationIcons(to);
				item.transform.GetChild(6).GetComponent<Image>().sprite = icons[0];
				RawImage cardFace2 = item.transform.GetChild(7).GetComponent<RawImage>();
				int code2 = attackedCard.GetData().Id;
				if (code2 > 0)
				{
					Program.instance.texture_.LoadCardToRawImageWithoutMaterialAsync(cardFace2, code2, true);
				}
				else
				{
					cardFace2.texture = null;
					cardFace2.material = (from.InMyControl() ? OcgCore.opProtector : OcgCore.myProtector);
					cardFace2.transform.GetChild(0).gameObject.SetActive(false);
				}
				if (((long)to.position & 12L) > 0L)
				{
					cardFace2.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
				}
				if (((long)to.position & 5L) > 0L)
				{
					cardFace2.transform.GetChild(0).gameObject.SetActive(false);
				}
				cardFace2.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
				{
					this.Core.GetUI<OcgCoreUI>().CardDescription.Show(null, null, code2, to);
				});
			}
			this.DuelLog.AddLog(item, false);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C57 RID: 40023 RVA: 0x0018EB4C File Offset: 0x0018CD4C
		protected override UniTask GameMessage_UpdateData(BinaryReader reader)
		{
			if (this.inPendulumSummon)
			{
				this.inPendulumSummon = false;
				this.cardsBeTarget.Clear();
				GameObject item = ABLoader.LoadMasterDuelGameObject((LogMessage.chainSolvingIndex > 0) ? "DuelLogText2" : "DuelLogText");
				item.transform.GetChild(1).GetComponent<Text>().text = InterString.Get("灵摆召唤结束", 0);
				this.DuelLog.AddLog(item, false);
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C58 RID: 40024 RVA: 0x0018EBC0 File Offset: 0x0018CDC0
		protected override UniTask GameMessage_Hint(BinaryReader reader)
		{
			char type = reader.ReadChar();
			int player = this.LocalPlayer((int)reader.ReadChar());
			int data = reader.ReadInt32();
			string hint = string.Empty;
			GameObject item = null;
			if (type == '\b' || type == '\n')
			{
				item = ABLoader.LoadMasterDuelGameObject((this.cardsInChain.Count > 0) ? "DuelLogTextWithCard2" : "DuelLogTextWithCard");
				if (type == '\b')
				{
					hint = InterString.Get("宣言卡片：[?]", CardsManager.Get(data, false).Name, 0);
				}
				else if (type == '\n')
				{
					hint = InterString.Get("效果适用：[?]", CardsManager.Get(data, false).Name, 0);
				}
			}
			else if (type >= '\u0002' && type <= '\v' && type != '\u0003')
			{
				item = ABLoader.LoadMasterDuelGameObject((this.cardsInChain.Count > 0) ? "DuelLogText2" : "DuelLogText");
				if (type == '\u0002')
				{
					hint = StringHelper.Get(data);
				}
				else if (type == '\u0004')
				{
					hint = InterString.Get("效果选择：[?]", StringHelper.Get(data), 0);
				}
				else if (type == '\u0005')
				{
					hint = StringHelper.Get(data);
				}
				else if (type == '\u0006')
				{
					hint = InterString.Get("种族选择：[?]", StringHelper.Race((long)data, 0), 0);
				}
				else if (type == '\a')
				{
					hint = InterString.Get("属性选择：[?]", StringHelper.Attribute((long)data, 0), 0);
				}
				else if (type == '\t')
				{
					hint = InterString.Get("数字选择：[?]", data.ToString(), 0);
				}
				else if (type == '\v')
				{
					if (player == 1)
					{
						data = (data >> 16) | (data << 16);
					}
					hint = InterString.Get("区域选择：[?]", StringHelper.Zone((long)data), 0);
				}
			}
			if (item != null)
			{
				item.transform.GetChild(1).GetComponent<Text>().text = hint;
				if (item.transform.childCount > 2)
				{
					RawImage cardFace = item.transform.GetChild(2).GetComponent<RawImage>();
					Program.instance.texture_.LoadCardToRawImageWithoutMaterialAsync(cardFace, data, true);
					item.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
					{
						this.Core.GetUI<OcgCoreUI>().CardDescription.Show(null, null, data, new GPS());
					});
				}
				this.DuelLog.AddLog(item, false);
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C59 RID: 40025 RVA: 0x0018EE30 File Offset: 0x0018D030
		protected override UniTask GameMessage_AddCounter(BinaryReader reader)
		{
			ushort type = reader.ReadUInt16();
			GPS gps = reader.ReadShortGPS();
			GameCard card = base.Core.GCS_Get(gps);
			short count = reader.ReadInt16();
			if (card == null)
			{
				this.DebugNoCard();
				return UniTask.CompletedTask;
			}
			int counterBefore = card.GetCounterCount((int)type);
			int counterNow = counterBefore + (int)count;
			if (OcgCore.currentMessage == GameMessage.RemoveCounter)
			{
				counterNow = counterBefore - (int)count;
			}
			GameObject item = ABLoader.LoadMasterDuelGameObject("DuelLogCounter");
			Color targetColor = (gps.InMyControl() ? DuelLog.myColor : DuelLog.opColor);
			item.transform.GetChild(1).GetComponent<Image>().color = targetColor;
			item.transform.GetChild(2).GetComponent<Text>().text = card.GetData().Name;
			RawImage cardFace = item.transform.GetChild(3).GetComponent<RawImage>();
			Program.instance.texture_.LoadCardToRawImageWithoutMaterialAsync(cardFace, card.GetData().Id, true);
			cardFace.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
			{
				this.Core.GetUI<OcgCoreUI>().CardDescription.Show(null, null, card.GetData().Id, gps);
			});
			List<Sprite> icons = TextureManager.container.GetLocationIcons(gps);
			if (icons.Count == 2)
			{
				item.transform.GetChild(4).GetComponent<Image>().sprite = icons[1];
				item.transform.GetChild(4).GetChild(0).GetComponent<Image>()
					.sprite = icons[0];
			}
			else
			{
				item.transform.GetChild(4).GetComponent<Image>().sprite = icons[0];
				item.transform.GetChild(4).GetChild(0).gameObject.SetActive(false);
			}
			Text component = item.transform.GetChild(5).GetComponent<Text>();
			component.text = counterNow.ToString();
			Text component2 = component.transform.GetChild(0).GetChild(0).GetComponent<Text>();
			component2.text = counterBefore.ToString();
			component2.transform.GetChild(0).GetComponent<Image>().sprite = TextureManager.GetCardCounterIcon((int)type);
			item.transform.GetChild(6).GetComponent<Text>().text = StringHelper.Get("counter", (int)type, 0);
			this.DuelLog.AddLog(item, false);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C5A RID: 40026 RVA: 0x00182DA9 File Offset: 0x00180FA9
		protected override UniTask GameMessage_RemoveCounter(BinaryReader reader)
		{
			return this.GameMessage_AddCounter(reader);
		}

		// Token: 0x06009C5B RID: 40027 RVA: 0x0018F0B0 File Offset: 0x0018D2B0
		protected override UniTask GameMessage_Damage(BinaryReader reader)
		{
			int player = this.LocalPlayer((int)reader.ReadByte());
			int value = reader.ReadInt32();
			string textReason = InterString.Get("伤害", 0);
			if (player == 0)
			{
				LogMessage.life0 -= value;
			}
			else
			{
				LogMessage.life1 -= value;
			}
			this.DuelLog.AddLpPChangeMessageToLog(player, textReason, value, true, false);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C5C RID: 40028 RVA: 0x0018F110 File Offset: 0x0018D310
		protected override UniTask GameMessage_PayLpCost(BinaryReader reader)
		{
			int player = this.LocalPlayer((int)reader.ReadByte());
			int value = reader.ReadInt32();
			string textReason = InterString.Get("代价", 0);
			if (player == 0)
			{
				LogMessage.life0 -= value;
			}
			else
			{
				LogMessage.life1 -= value;
			}
			this.DuelLog.AddLpPChangeMessageToLog(player, textReason, value, true, true);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C5D RID: 40029 RVA: 0x0018F170 File Offset: 0x0018D370
		protected override UniTask GameMessage_Recover(BinaryReader reader)
		{
			int player = this.LocalPlayer((int)reader.ReadByte());
			int value = reader.ReadInt32();
			string textReason = InterString.Get("回复", 0);
			if (player == 0)
			{
				LogMessage.life0 += value;
			}
			else
			{
				LogMessage.life1 += value;
			}
			this.DuelLog.AddLpPChangeMessageToLog(player, textReason, value, false, false);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C5E RID: 40030 RVA: 0x0018F1D0 File Offset: 0x0018D3D0
		protected override UniTask GameMessage_LpUpdate(BinaryReader reader)
		{
			int player = this.LocalPlayer((int)reader.ReadByte());
			int value = reader.ReadInt32();
			string textReason = InterString.Get("基本分改变", 0);
			int diff = ((player == 0) ? (value - LogMessage.life0) : (value - LogMessage.life1));
			if (player == 0)
			{
				LogMessage.life0 = value;
			}
			else
			{
				LogMessage.life1 = value;
			}
			this.DuelLog.AddLpPChangeMessageToLog(player, textReason, Mathf.Abs(diff), diff < 0, false);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C5F RID: 40031 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected override UniTask GameMessage_TossCoin(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C60 RID: 40032 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected override UniTask GameMessage_TossDice(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x0400DA86 RID: 55942
		public static int chainSolvingIndex;

		// Token: 0x0400DA87 RID: 55943
		public static int life0;

		// Token: 0x0400DA88 RID: 55944
		public static int life1;

		// Token: 0x0400DA89 RID: 55945
		private bool isFirst;

		// Token: 0x0400DA8A RID: 55946
		private int MasterRule;

		// Token: 0x0400DA8B RID: 55947
		private int turns;

		// Token: 0x0400DA8C RID: 55948
		private bool myTurn;

		// Token: 0x0400DA8D RID: 55949
		private DuelPhase duelPhase;

		// Token: 0x0400DA8E RID: 55950
		private bool inPendulumSummon;

		// Token: 0x0400DA8F RID: 55951
		private GameCard lastMoveCard;

		// Token: 0x0400DA90 RID: 55952
		public static uint lastMoveReason;

		// Token: 0x0400DA91 RID: 55953
		private readonly List<GameCard> cardsInChain = new List<GameCard>();

		// Token: 0x0400DA92 RID: 55954
		private readonly List<GameCard> cardsBeTarget = new List<GameCard>();

		// Token: 0x0400DA93 RID: 55955
		private uint lastSpSummonReason;

		// Token: 0x0400DA94 RID: 55956
		private GameCard lastConfirmedCard;

		// Token: 0x0400DA95 RID: 55957
		private GameCard attackingCard;
	}
}
