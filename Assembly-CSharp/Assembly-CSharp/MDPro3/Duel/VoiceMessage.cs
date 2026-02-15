using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI;
using UnityEngine;

namespace MDPro3.Duel
{
	// Token: 0x02001510 RID: 5392
	public class VoiceMessage : MessageProcessor
	{
		// Token: 0x06009CE4 RID: 40164 RVA: 0x001926DE File Offset: 0x001908DE
		public VoiceMessage(MessageDispatcher dispatcher)
			: base(dispatcher)
		{
		}

		// Token: 0x06009CE5 RID: 40165 RVA: 0x00192708 File Offset: 0x00190908
		private void LogDebug(string text)
		{
			Program.Debug(text);
		}

		// Token: 0x06009CE6 RID: 40166 RVA: 0x00192710 File Offset: 0x00190910
		private void DebugNoCard()
		{
			this.LogDebug(string.Format("[Duel Voice]: Not found card for {0}.", OcgCore.currentMessage));
		}

		// Token: 0x06009CE7 RID: 40167 RVA: 0x0019272C File Offset: 0x0019092C
		private void ResetState()
		{
			this.ignoreNextChaining = false;
			this.inPendulumSummon = false;
			this.ignoreNextPendulumSummon = false;
			this.lastVoiceIsRelease = false;
			this.cardsInChain.Clear();
			this.cardsBeTarget.Clear();
		}

		// Token: 0x06009CE8 RID: 40168 RVA: 0x00192760 File Offset: 0x00190960
		private async UniTask PlayVoiceAsync()
		{
			List<string>[] paths = VoicePlayer.GetVoicePaths(this.voiceData);
			List<AudioClip>[] clips = new List<AudioClip>[paths.Length];
			for (int i = 0; i < clips.Length; i++)
			{
				clips[i] = new List<AudioClip>();
			}
			for (int j = 0; j < paths.Length; j++)
			{
				for (int k = 0; k < paths[j].Count; k++)
				{
					try
					{
						AudioClip clip = await AudioManager.LoadAudioFileUniAsync(paths[j][k], AudioType.OGGVORBIS);
						clips[j].Add(clip);
					}
					catch (Exception ex)
					{
						Debug.LogException(ex);
					}
				}
			}
			for (int j = 0; j < clips.Length; j++)
			{
				for (int k = 0; k < clips[j].Count; k++)
				{
					if (k == 0)
					{
						await UniTask.WaitForSeconds(this.voiceData[j].delay, false, PlayerLoopTiming.Update, default(CancellationToken), false);
					}
					LineInfo line = VoicePlayer.GetLine(Path.GetFileNameWithoutExtension(paths[j][k]), this.voiceData[j].isHero);
					if (line != null)
					{
						GameObject gameObject = ABLoader.LoadMasterDuelGameObject(this.voiceData[j].isHero ? "DuelChatItemMe" : "DuelChatItemOp");
						gameObject.transform.SetParent(base.Core.transform.GetChild(0), false);
						ChatItemHandler handler = gameObject.GetComponent<ChatItemHandler>();
						handler.text = line.text;
						if (clips[j][k] == null)
						{
							Debug.LogError("Voice File " + paths[j][k] + " not Found!");
							return;
						}
						handler.time = clips[j][k].length;
						handler.frame = line.frame;
						if (this.voiceData[j].isHero)
						{
							if (base.Core.duelChat0 != null)
							{
								base.Core.duelChat0.BeGray();
							}
							base.Core.duelChat0 = handler;
						}
						else
						{
							if (base.Core.duelChat1 != null)
							{
								base.Core.duelChat1.BeGray();
							}
							base.Core.duelChat1 = handler;
						}
						base.Core.SetCharacterFace(this.voiceData[j].isHero ? VoicePlayer.heroString : VoicePlayer.rivalString, line.face, this.voiceData[j].isHero, 0f);
						base.Core.SetCharacterFace(this.voiceData[j].isHero ? VoicePlayer.heroString : VoicePlayer.rivalString, 1, this.voiceData[j].isHero, clips[j][k].length - 0.1f);
					}
					AudioManager.PlayVoice(clips[j][k]);
					if (this.voiceData[j].wait)
					{
						await UniTask.WaitForSeconds(clips[j][k].length, false, PlayerLoopTiming.Update, default(CancellationToken), false);
					}
				}
			}
		}

		// Token: 0x06009CE9 RID: 40169 RVA: 0x0012D498 File Offset: 0x0012B698
		private bool NeedVoice()
		{
			return Config.GetBool(OcgCore.condition.ToString() + "Voice", false);
		}

		// Token: 0x06009CEA RID: 40170 RVA: 0x001927A3 File Offset: 0x001909A3
		private static bool DamageIsBig(int damage)
		{
			return damage >= 2000;
		}

		// Token: 0x06009CEB RID: 40171 RVA: 0x001927B0 File Offset: 0x001909B0
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

		// Token: 0x06009CEC RID: 40172 RVA: 0x001927C8 File Offset: 0x001909C8
		public override async UniTask Process(Package p)
		{
			if (this.NeedVoice())
			{
				if (!base.Core.charaFaceSetting)
				{
					base.Core.SetCharacterDefaultFace();
				}
				VoicePlayer.LoadData();
				this.voiceData.Clear();
				await base.Process(p);
				if (this.voiceData.Count != 0)
				{
					UniTask voiceTask = this.PlayVoiceAsync();
					UniTask clickTask = UniTask.WaitUntil(() => UserInput.MouseLeftDown, PlayerLoopTiming.Update, default(CancellationToken), false);
					await UniTask.WhenAny(new UniTask[] { voiceTask, clickTask });
				}
			}
			else
			{
				base.Core.CloseCharaFace();
			}
		}

		// Token: 0x06009CED RID: 40173 RVA: 0x00192814 File Offset: 0x00190A14
		protected override UniTask GameMessage_Start(BinaryReader reader)
		{
			this.ResetState();
			this.isFirst = (reader.ReadByte() & 15) == 0;
			if (reader.BaseStream.Length > 17L)
			{
				this.MasterRule = (int)(reader.ReadByte() + 1);
			}
			this.life0 = reader.ReadInt32();
			this.life1 = reader.ReadInt32();
			this.turns = 0;
			VoicePlayer.VoiceData data = default(VoicePlayer.VoiceData);
			data.name = VoicePlayer.GetVoiceByDuelist(VoicePlayer.heroVoices.BeforeDuel, VoicePlayer.heroVoices.BeforeDuelSp, VoicePlayer.rivalCode);
			data.num = VoicePlayer.GetVoiceNum(VoicePlayer.heroVoices, data.name);
			data.isHero = true;
			data.wait = true;
			data.delay = 0f;
			this.voiceData.Add(data);
			VoicePlayer.VoiceData data2 = default(VoicePlayer.VoiceData);
			data2.name = VoicePlayer.GetVoiceByDuelist(VoicePlayer.rivalVoices.BeforeDuel, VoicePlayer.rivalVoices.BeforeDuelSp, VoicePlayer.heroCode);
			data2.num = VoicePlayer.GetVoiceNum(VoicePlayer.rivalVoices, data2.name);
			data2.isHero = false;
			data2.wait = true;
			data2.delay = 0f;
			this.voiceData.Add(data2);
			VoicePlayer.VoiceData data3 = default(VoicePlayer.VoiceData);
			data3.name = Tools.GetRandomDictionaryElement<string, VoiceInfo>(VoicePlayer.heroVoices.DuelStart.rawKvp).Value.shortName;
			data3.num = VoicePlayer.GetVoiceNum(VoicePlayer.heroVoices, data3.name);
			data3.isHero = true;
			data3.wait = false;
			data3.delay = 1f;
			this.voiceData.Add(data3);
			VoicePlayer.VoiceData data4 = default(VoicePlayer.VoiceData);
			data4.name = Tools.GetRandomDictionaryElement<string, VoiceInfo>(VoicePlayer.rivalVoices.DuelStart.rawKvp).Value.shortName;
			data4.num = VoicePlayer.GetVoiceNum(VoicePlayer.rivalVoices, data4.name);
			data4.isHero = false;
			data4.wait = true;
			data4.delay = 0f;
			this.voiceData.Add(data4);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CEE RID: 40174 RVA: 0x00192A34 File Offset: 0x00190C34
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

		// Token: 0x06009CEF RID: 40175 RVA: 0x00192A90 File Offset: 0x00190C90
		protected override UniTask GameMessage_NewTurn(BinaryReader reader)
		{
			this.turns++;
			this.myTurn = (this.isFirst ? (this.turns % 2 != 0) : (this.turns % 2 == 0));
			if (this.turns == 1)
			{
				return UniTask.CompletedTask;
			}
			VoicesData targetData = (this.myTurn ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices);
			int leadingState = (this.myTurn ? VoicePlayer.LeadingStateOfHero() : VoicePlayer.LeadingStateOfRival());
			VoicePlayer.VoiceData data = default(VoicePlayer.VoiceData);
			data.name = VoicePlayer.GetVoiceBySituation(targetData.TurnStart, leadingState);
			data.num = VoicePlayer.GetVoiceNum(targetData, data.name);
			data.isHero = this.myTurn;
			data.wait = true;
			data.delay = 0f;
			this.voiceData.Add(data);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CF0 RID: 40176 RVA: 0x00192B6C File Offset: 0x00190D6C
		protected override UniTask GameMessage_NewPhase(BinaryReader reader)
		{
			this.duelPhase = (DuelPhase)reader.ReadUInt16();
			if (this.duelPhase != DuelPhase.BattleStart && this.duelPhase != DuelPhase.End)
			{
				return UniTask.CompletedTask;
			}
			VoicesData targetData = (this.myTurn ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices);
			VoicePlayer.VoiceData data = default(VoicePlayer.VoiceData);
			if (this.duelPhase == DuelPhase.BattleStart)
			{
				data.name = Tools.GetRandomDictionaryElement<string, VoiceInfo>(targetData.BattleStart.rawKvp).Value.shortName;
			}
			else if (this.duelPhase == DuelPhase.End)
			{
				int leadingState = (this.myTurn ? VoicePlayer.LeadingStateOfHero() : VoicePlayer.LeadingStateOfRival());
				data.name = VoicePlayer.GetVoiceBySituation(targetData.TurnEnd, leadingState);
			}
			data.num = VoicePlayer.GetVoiceNum(targetData, data.name);
			data.isHero = this.myTurn;
			data.wait = true;
			data.delay = 0f;
			this.voiceData.Add(data);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CF1 RID: 40177 RVA: 0x00192C68 File Offset: 0x00190E68
		protected override UniTask GameMessage_Win(BinaryReader reader)
		{
			int player = this.LocalPlayer((int)reader.ReadByte());
			if (player == 2)
			{
				return UniTask.CompletedTask;
			}
			VoicesData targetData;
			VoicesData targetData2;
			if (player == 0)
			{
				targetData = VoicePlayer.heroVoices;
				targetData2 = VoicePlayer.rivalVoices;
			}
			else
			{
				targetData = VoicePlayer.rivalVoices;
				targetData2 = VoicePlayer.heroVoices;
			}
			VoicePlayer.VoiceData data = default(VoicePlayer.VoiceData);
			data.name = VoicePlayer.GetVoiceByDuelist(targetData.Win, targetData.WinSp, (player == 0) ? VoicePlayer.rivalCode : VoicePlayer.heroCode);
			data.num = VoicePlayer.GetVoiceNum(targetData, data.name);
			data.isHero = player == 0;
			data.wait = true;
			data.delay = 0f;
			this.voiceData.Add(data);
			VoicePlayer.VoiceData data2 = default(VoicePlayer.VoiceData);
			data2.name = VoicePlayer.GetVoiceByDuelist(targetData2.Lose, targetData2.LoseSp, (player == 0) ? VoicePlayer.heroCode : VoicePlayer.rivalCode);
			data2.num = VoicePlayer.GetVoiceNum(targetData2, data2.name);
			data2.isHero = !data.isHero;
			data2.wait = true;
			data2.delay = 0f;
			this.voiceData.Add(data2);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CF2 RID: 40178 RVA: 0x00192D90 File Offset: 0x00190F90
		protected override UniTask GameMessage_PosChange(BinaryReader reader)
		{
			Package nextPack = OcgCore.GetNextPackage();
			if (nextPack == null)
			{
				return UniTask.CompletedTask;
			}
			if (nextPack.Function != 70)
			{
				return UniTask.CompletedTask;
			}
			reader.ReadUInt32();
			GPS gps = reader.ReadGPS();
			VoicesData target = (gps.InMyControl() ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices);
			if (VoicePlayer.NeedBeforeCardEffect(gps.InMyControl()))
			{
				this.voiceData.Add(VoicePlayer.GetBeforeCardEffectData(target, gps.InMyControl()));
			}
			VoicePlayer.VoiceData data = default(VoicePlayer.VoiceData);
			data.name = VoicePlayer.GetVoiceBySubCategory(target.CardEffect, 9, 9, 0, false);
			data.num = VoicePlayer.GetVoiceNum(target, data.name);
			data.isHero = gps.InMyControl();
			data.wait = true;
			data.delay = 0f;
			this.voiceData.Add(data);
			SimpleVoiceData simple = SimpleVoiceData.GetCardEffectSubCategory(nextPack.Data.reader, false);
			VoicePlayer.VoiceData data2 = default(VoicePlayer.VoiceData);
			data2.name = VoicePlayer.GetVoiceBySubCategory(target.CardEffect, simple.subCategory, 1, 0, false);
			data2.num = VoicePlayer.GetVoiceNum(target, data2.name);
			data2.isHero = data.isHero;
			data2.wait = true;
			data2.delay = 0f;
			this.voiceData.Add(data2);
			this.ignoreNextChaining = true;
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CF3 RID: 40179 RVA: 0x00192EE8 File Offset: 0x001910E8
		protected override UniTask GameMessage_Move(BinaryReader reader)
		{
			int code = (int)reader.ReadUInt32();
			GPS from = reader.ReadGPS();
			GPS to = reader.ReadGPS();
			uint reason = reader.ReadUInt32();
			GameCard card = base.Core.GCS_Get(from);
			if (card == null)
			{
				this.DebugNoCard();
				return UniTask.CompletedTask;
			}
			Card cardData = card.GetData();
			Package nextPack = OcgCore.GetNextPackage();
			if (nextPack == null)
			{
				return UniTask.CompletedTask;
			}
			nextPack.Data.reader.BaseStream.Seek(0L, SeekOrigin.Begin);
			int category = 0;
			int subCategory = 0;
			int subInCase = 0;
			int patternIndex = 0;
			bool fromHand = false;
			bool isMe = to.InMyControl();
			if ((reason & 2U) > 0U && card.GetData().HasType(CardType.Monster))
			{
				if (this.lastVoiceIsRelease)
				{
					return UniTask.CompletedTask;
				}
				category = 11;
				subCategory = 3;
				subInCase = subCategory;
				this.lastVoiceIsRelease = true;
			}
			else
			{
				this.lastVoiceIsRelease = false;
			}
			if (nextPack.Function == 60)
			{
				code = nextPack.Data.reader.ReadInt32();
				category = 11;
				subCategory = 0;
				isMe = from.controller == 0U;
				VoicesData targetDataT = (isMe ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices);
				VoicePlayer.VoiceData data = VoicePlayer.GetVoiceByCard(isMe ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices, targetDataT.MainMonsterSummon, code, 0, isMe);
				if (data.name != string.Empty)
				{
					this.voiceData.Add(data);
					return UniTask.CompletedTask;
				}
				this.voiceData.Add(VoicePlayer.GetBeforeSummonData(targetDataT, isMe));
			}
			if (nextPack.Function == 62)
			{
				code = nextPack.Data.reader.ReadInt32();
				cardData = CardsManager.Get(code, false);
				category = 11;
				subCategory = 2;
				subInCase = subCategory;
				if (OcgCore.materialCards.Count > 0)
				{
					patternIndex = -1;
					if (cardData.HasType(CardType.Link))
					{
						subCategory = 10;
					}
					else if (cardData.HasType(CardType.Fusion))
					{
						subCategory = 5;
					}
					else if (cardData.HasType(CardType.Synchro))
					{
						subCategory = 7;
					}
					else if (cardData.HasType(CardType.Xyz))
					{
						subCategory = 8;
					}
					else if (cardData.HasType(CardType.Ritual))
					{
						subCategory = 6;
					}
				}
				else if (this.inPendulumSummon)
				{
					if (this.ignoreNextPendulumSummon)
					{
						return UniTask.CompletedTask;
					}
					subCategory = 9;
				}
				if (from.InLocation(CardLocation.Hand) && subCategory == 2)
				{
					fromHand = true;
					isMe = from.InMyControl();
				}
				VoicesData targetDataT2 = (isMe ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices);
				if (subCategory != 2)
				{
					VoicePlayer.VoiceData data2 = VoicePlayer.GetVoiceByCard(isMe ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices, targetDataT2.BeforeMainSummon, code, 0, isMe);
					if (data2.name != string.Empty)
					{
						this.voiceData.Add(data2);
					}
				}
				VoicePlayer.VoiceData dataT = VoicePlayer.GetVoiceByCard(isMe ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices, targetDataT2.MainMonsterSummon, code, 0, isMe);
				if (dataT.name != string.Empty)
				{
					if (subCategory != 2 && this.voiceData.Count == 0)
					{
						string voiceShortName = VoicePlayer.GetVoiceBySubCategory(targetDataT2.Summon, subCategory, 2, 1, true);
						if (voiceShortName != null)
						{
							VoicePlayer.VoiceData data3 = default(VoicePlayer.VoiceData);
							data3.name = voiceShortName;
							data3.num = VoicePlayer.GetVoiceNum(targetDataT2, data3.name);
							data3.isHero = isMe;
							data3.wait = true;
							data3.delay = 0f;
							this.voiceData.Add(data3);
						}
					}
					this.voiceData.Add(dataT);
					return UniTask.CompletedTask;
				}
			}
			if (nextPack.Function == 54)
			{
				category = 20;
				if (to.InLocation(CardLocation.MonsterZone))
				{
					subCategory = 1;
				}
				else
				{
					subCategory = 0;
				}
				subInCase = subCategory;
			}
			if (nextPack.Function == 70)
			{
				if (!to.InLocation(CardLocation.Onfield))
				{
					return UniTask.CompletedTask;
				}
				this.ignoreNextChaining = true;
				code = nextPack.Data.reader.ReadInt32();
				GPS gps = nextPack.Data.reader.ReadGPS();
				VoicesData targetDataT3 = (gps.InMyControl() ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices);
				VoicePlayer.VoiceData data4 = VoicePlayer.GetVoiceByCard(targetDataT3, targetDataT3.MainMonsterEffect, code, 0, gps.InMyControl());
				if (data4.name != string.Empty)
				{
					this.voiceData.Add(data4);
					return UniTask.CompletedTask;
				}
				data4 = VoicePlayer.GetVoiceByCard(targetDataT3, targetDataT3.MainMagicTrap, code, 0, gps.InMyControl());
				if (data4.name != string.Empty)
				{
					this.voiceData.Add(data4);
					return UniTask.CompletedTask;
				}
				if (from.InLocation(CardLocation.Hand))
				{
					fromHand = true;
					isMe = from.InMyControl();
				}
				if (VoicePlayer.NeedBeforeCardEffect(from.InMyControl()))
				{
					this.voiceData.Add(VoicePlayer.GetBeforeCardEffectData(isMe ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices, from.InMyControl()));
				}
				SimpleVoiceData cardEffectSubCategory = SimpleVoiceData.GetCardEffectSubCategory(nextPack.Data.reader, fromHand);
				category = cardEffectSubCategory.category;
				subCategory = cardEffectSubCategory.subCategory;
				subInCase = 1;
				if (subCategory == 13)
				{
					fromHand = false;
				}
			}
			VoicesData targetData = (isMe ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices);
			if (category == 0)
			{
				return UniTask.CompletedTask;
			}
			if (fromHand)
			{
				VoicePlayer.VoiceData data5 = default(VoicePlayer.VoiceData);
				data5.name = VoicePlayer.GetVoiceBySubCategory(targetData.CardEffect, 0, 0, 0, false);
				data5.num = VoicePlayer.GetVoiceNum(targetData, data5.name);
				data5.isHero = from.controller == 0U;
				data5.wait = true;
				data5.delay = 0f;
				this.voiceData.Add(data5);
			}
			VoicePlayer.VoiceData data6 = default(VoicePlayer.VoiceData);
			data6.name = VoicePlayer.GetVoiceBySubCategory(targetData.GetCategoryEntry((VoiceController.Category)category), subCategory, subInCase, patternIndex, false);
			data6.num = VoicePlayer.GetVoiceNum(targetData, data6.name);
			data6.isHero = from.controller == 0U;
			data6.wait = true;
			data6.delay = 0f;
			this.voiceData.Add(data6);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CF4 RID: 40180 RVA: 0x001934BC File Offset: 0x001916BC
		protected override UniTask GameMessage_Chaining(BinaryReader reader)
		{
			if (this.ignoreNextChaining)
			{
				this.ignoreNextChaining = false;
				return UniTask.CompletedTask;
			}
			int code = (int)reader.ReadUInt32();
			GPS gps = reader.ReadGPS();
			GameCard card = base.Core.GCS_Get(gps);
			if (card == null)
			{
				this.DebugNoCard();
			}
			this.cardsInChain.Add(card);
			if (code == 0)
			{
				code = card.GetData().Id;
			}
			SimpleVoiceData simple = SimpleVoiceData.GetCardEffectSubCategory(reader, false);
			VoicesData targetData = (simple.isMe ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices);
			if (VoicePlayer.NeedBeforeCardEffect(simple.isMe))
			{
				this.voiceData.Add(VoicePlayer.GetBeforeCardEffectData(targetData, simple.isMe));
			}
			VoicePlayer.VoiceData data = VoicePlayer.GetVoiceByCard(targetData, targetData.MainMonsterEffect, code, 0, simple.isMe);
			if (data.name != string.Empty)
			{
				this.voiceData.Add(data);
				return UniTask.CompletedTask;
			}
			if (simple.inHand)
			{
				VoicePlayer.VoiceData data2 = default(VoicePlayer.VoiceData);
				data2.name = VoicePlayer.GetVoiceBySubCategory(targetData.CardEffect, 0, 0, 0, false);
				data2.num = VoicePlayer.GetVoiceNum(targetData, data2.name);
				data2.isHero = simple.isMe;
				data2.wait = true;
				data2.delay = 0f;
				this.voiceData.Add(data2);
			}
			VoicePlayer.VoiceData data3 = default(VoicePlayer.VoiceData);
			data3.name = VoicePlayer.GetVoiceBySubCategory(targetData.CardEffect, simple.subCategory, 1, 0, false);
			data3.num = VoicePlayer.GetVoiceNum(targetData, data3.name);
			data3.isHero = simple.isMe;
			data3.wait = true;
			data3.delay = 0f;
			this.voiceData.Add(data3);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CF5 RID: 40181 RVA: 0x0019367C File Offset: 0x0019187C
		protected override UniTask GameMessage_Draw(BinaryReader reader)
		{
			if (this.duelPhase != DuelPhase.Draw)
			{
				return UniTask.CompletedTask;
			}
			if (this.turns == 0)
			{
				return UniTask.CompletedTask;
			}
			if (this.turns == 1 && this.MasterRule > 2)
			{
				return UniTask.CompletedTask;
			}
			int player = this.LocalPlayer((int)reader.ReadByte());
			VoicesData targetData = ((player == 0) ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices);
			int leadingState = ((player == 0) ? VoicePlayer.LeadingStateOfHero() : VoicePlayer.LeadingStateOfRival());
			VoicePlayer.VoiceData data = default(VoicePlayer.VoiceData);
			data.name = VoicePlayer.GetVoiceBySituation(targetData.Draw, leadingState);
			data.num = VoicePlayer.GetVoiceNum(targetData, data.name);
			data.isHero = player == 0;
			data.wait = true;
			data.delay = 0f;
			this.voiceData.Add(data);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CF6 RID: 40182 RVA: 0x0019374C File Offset: 0x0019194C
		protected override UniTask GameMessage_Damage(BinaryReader reader)
		{
			int player = this.LocalPlayer((int)reader.ReadByte());
			int value = reader.ReadInt32();
			VoicesData targetData = ((player == 0) ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices);
			int cacheLP = ((player == 0) ? this.life0 : this.life1);
			if (player == 0)
			{
				this.life0 -= value;
			}
			else
			{
				this.life1 -= value;
			}
			if (value >= cacheLP)
			{
				this.voiceData.Add(VoicePlayer.GetFinishDamageVoiceData(targetData, player == 0));
				return UniTask.CompletedTask;
			}
			VoicePlayer.VoiceData data = default(VoicePlayer.VoiceData);
			if (VoiceMessage.DamageIsBig(value))
			{
				data.name = Tools.GetRandomDictionaryElement<string, VoiceInfo>(targetData.BigDamage.rawKvp).Value.shortName;
			}
			else
			{
				data.name = Tools.GetRandomDictionaryElement<string, VoiceInfo>(targetData.Damage.rawKvp).Value.shortName;
			}
			data.num = VoicePlayer.GetVoiceNum(targetData, data.name);
			data.isHero = player == 0;
			data.wait = false;
			data.delay = 0f;
			this.voiceData.Add(data);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CF7 RID: 40183 RVA: 0x00193870 File Offset: 0x00191A70
		protected override UniTask GameMessage_PayLpCost(BinaryReader reader)
		{
			int player = this.LocalPlayer((int)reader.ReadByte());
			int value = reader.ReadInt32();
			VoicesData targetData = ((player == 0) ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices);
			int cacheLP = ((player == 0) ? this.life0 : this.life1);
			if (player == 0)
			{
				this.life0 -= value;
			}
			else
			{
				this.life1 -= value;
			}
			if (value >= cacheLP)
			{
				this.voiceData.Add(VoicePlayer.GetFinishDamageVoiceData(targetData, player == 0));
				return UniTask.CompletedTask;
			}
			VoicePlayer.VoiceData data = default(VoicePlayer.VoiceData);
			data.name = Tools.GetRandomDictionaryElement<string, VoiceInfo>(targetData.CostDamage.rawKvp).Value.shortName;
			data.num = VoicePlayer.GetVoiceNum(targetData, data.name);
			data.isHero = player == 0;
			data.wait = false;
			data.delay = 0f;
			this.voiceData.Add(data);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CF8 RID: 40184 RVA: 0x00193968 File Offset: 0x00191B68
		protected override UniTask GameMessage_Recover(BinaryReader reader)
		{
			bool flag = this.LocalPlayer((int)reader.ReadByte()) != 0;
			int value = reader.ReadInt32();
			if (!flag)
			{
				this.life0 += value;
			}
			else
			{
				this.life1 += value;
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CF9 RID: 40185 RVA: 0x001939B0 File Offset: 0x00191BB0
		protected override UniTask GameMessage_LpUpdate(BinaryReader reader)
		{
			int player = this.LocalPlayer((int)reader.ReadByte());
			int value = reader.ReadInt32();
			VoicesData targetData = ((player == 0) ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices);
			int diff = ((player == 0) ? this.life0 : this.life1) - value;
			if (player == 0)
			{
				this.life0 = value;
			}
			else
			{
				this.life1 = value;
			}
			if (value == 0)
			{
				this.voiceData.Add(VoicePlayer.GetFinishDamageVoiceData(targetData, player == 0));
				return UniTask.CompletedTask;
			}
			if (diff <= 0)
			{
				return UniTask.CompletedTask;
			}
			VoicePlayer.VoiceData data = default(VoicePlayer.VoiceData);
			if (VoiceMessage.DamageIsBig(diff))
			{
				data.name = Tools.GetRandomDictionaryElement<string, VoiceInfo>(targetData.BigDamage.rawKvp).Value.shortName;
			}
			else
			{
				data.name = Tools.GetRandomDictionaryElement<string, VoiceInfo>(targetData.Damage.rawKvp).Value.shortName;
			}
			data.num = VoicePlayer.GetVoiceNum(targetData, data.name);
			data.isHero = player == 0;
			data.wait = false;
			data.delay = 0f;
			this.voiceData.Add(data);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CFA RID: 40186 RVA: 0x00193AD4 File Offset: 0x00191CD4
		protected override UniTask GameMessage_Attack(BinaryReader reader)
		{
			GPS from = reader.ReadGPS();
			GPS to = reader.ReadGPS();
			GameCard attackCard = base.Core.GCS_Get(from);
			if (attackCard == null)
			{
				this.DebugNoCard();
				return UniTask.CompletedTask;
			}
			bool directAttack = true;
			int value = attackCard.GetData().Attack;
			GameCard attackedCard = base.Core.GCS_Get(to);
			if (attackedCard != null)
			{
				directAttack = false;
				if (attackedCard.p.InPosition(CardPosition.Attack))
				{
					value = attackCard.GetData().Attack - attackedCard.GetData().Attack;
				}
				else
				{
					value = 0;
				}
			}
			bool finalBlow = value >= (from.InMyControl() ? this.life1 : this.life0);
			VoicesData targetData = (from.InMyControl() ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices);
			VoicePlayer.VoiceData data = default(VoicePlayer.VoiceData);
			data.name = Tools.GetRandomDictionaryElement<string, VoiceInfo>(finalBlow ? targetData.BeforeAttackFinish.rawKvp : targetData.BeforeAttackNormal.rawKvp).Value.shortName;
			data.num = VoicePlayer.GetVoiceNum(targetData, data.name);
			data.isHero = from.controller == 0U;
			data.wait = true;
			data.delay = 0f;
			this.voiceData.Add(data);
			VoicePlayer.VoiceData data2 = default(VoicePlayer.VoiceData);
			data2.name = Tools.GetRandomDictionaryElement<string, VoiceInfo>(directAttack ? targetData.DirectAttack.rawKvp : targetData.Attack.rawKvp).Value.shortName;
			data2.num = VoicePlayer.GetVoiceNum(targetData, data2.name);
			data2.isHero = data.isHero;
			data2.wait = true;
			data2.delay = 0f;
			this.voiceData.Add(data2);
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CFB RID: 40187 RVA: 0x00193CAC File Offset: 0x00191EAC
		protected override UniTask GameMessage_BecomeTarget(BinaryReader reader)
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
					this.cardsBeTarget.Add(card);
					if ((this.duelPhase == DuelPhase.Main1 || this.duelPhase == DuelPhase.Main2) && this.cardsInChain.Count == 0 && this.cardsBeTarget.Count == 2 && this.cardsBeTarget[0].InPendulumZone() && this.cardsBeTarget[1].InPendulumZone() && this.cardsBeTarget[0].p.controller == this.cardsBeTarget[1].p.controller)
					{
						this.inPendulumSummon = true;
					}
				}
			}
			if (!this.inPendulumSummon)
			{
				return UniTask.CompletedTask;
			}
			VoicesData targetData = (this.myTurn ? VoicePlayer.heroVoices : VoicePlayer.rivalVoices);
			VoicePlayer.VoiceData data = new VoicePlayer.VoiceData
			{
				name = VoicePlayer.GetVoiceBySubCategory(targetData.Summon, 9, 9, 1, true)
			};
			if (data.name == null)
			{
				return UniTask.CompletedTask;
			}
			data.num = VoicePlayer.GetVoiceNum(targetData, data.name);
			data.isHero = this.myTurn;
			data.wait = false;
			data.delay = 0f;
			this.voiceData.Add(data);
			this.ignoreNextPendulumSummon = true;
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CFC RID: 40188 RVA: 0x00193E34 File Offset: 0x00192034
		protected override UniTask GameMessage_UpdateData(BinaryReader reader)
		{
			if (this.inPendulumSummon)
			{
				this.inPendulumSummon = false;
				this.ignoreNextPendulumSummon = false;
				this.cardsBeTarget.Clear();
			}
			return UniTask.CompletedTask;
		}

		// Token: 0x0400DABB RID: 55995
		private bool isFirst;

		// Token: 0x0400DABC RID: 55996
		private int MasterRule;

		// Token: 0x0400DABD RID: 55997
		private int life0;

		// Token: 0x0400DABE RID: 55998
		private int life1;

		// Token: 0x0400DABF RID: 55999
		private int turns;

		// Token: 0x0400DAC0 RID: 56000
		private bool myTurn;

		// Token: 0x0400DAC1 RID: 56001
		private DuelPhase duelPhase;

		// Token: 0x0400DAC2 RID: 56002
		private readonly List<GameCard> cardsInChain = new List<GameCard>();

		// Token: 0x0400DAC3 RID: 56003
		private readonly List<GameCard> cardsBeTarget = new List<GameCard>();

		// Token: 0x0400DAC4 RID: 56004
		private bool ignoreNextChaining;

		// Token: 0x0400DAC5 RID: 56005
		private bool inPendulumSummon;

		// Token: 0x0400DAC6 RID: 56006
		private bool ignoreNextPendulumSummon;

		// Token: 0x0400DAC7 RID: 56007
		private bool lastVoiceIsRelease;

		// Token: 0x0400DAC8 RID: 56008
		private readonly List<VoicePlayer.VoiceData> voiceData = new List<VoicePlayer.VoiceData>();
	}
}
