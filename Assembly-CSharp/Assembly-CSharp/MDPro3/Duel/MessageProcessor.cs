using System;
using System.IO;
using Cysharp.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;

namespace MDPro3.Duel
{
	// Token: 0x0200150E RID: 5390
	public abstract class MessageProcessor
	{
		// Token: 0x06009C78 RID: 40056 RVA: 0x0018F76E File Offset: 0x0018D96E
		public MessageProcessor(MessageDispatcher dispatcher)
		{
			this.dispatcher = dispatcher;
		}

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x06009C79 RID: 40057 RVA: 0x00170DA0 File Offset: 0x0016EFA0
		protected OcgCore Core
		{
			get
			{
				return Program.instance.ocgcore;
			}
		}

		// Token: 0x06009C7A RID: 40058 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Dispose()
		{
		}

		// Token: 0x06009C7B RID: 40059 RVA: 0x0018F780 File Offset: 0x0018D980
		public virtual async UniTask Process(Package p)
		{
			BinaryReader reader = p.Data.reader;
			reader.BaseStream.Seek(0L, SeekOrigin.Begin);
			GameMessage message = (GameMessage)p.Function;
			switch (message)
			{
			case GameMessage.Retry:
				await this.GameMessage_Retry(reader);
				break;
			case GameMessage.Hint:
				await this.GameMessage_Hint(reader);
				break;
			case GameMessage.Waiting:
				await this.GameMessage_Waiting(reader);
				break;
			case GameMessage.Start:
				await this.GameMessage_Start(reader);
				break;
			case GameMessage.Win:
				await this.GameMessage_Win(reader);
				break;
			case GameMessage.UpdateData:
				await this.GameMessage_UpdateData(reader);
				break;
			case GameMessage.UpdateCard:
				await this.GameMessage_UpdateCard(reader);
				break;
			case GameMessage.RequestDeck:
				await this.GameMessage_RequestDeck(reader);
				break;
			case (GameMessage)9:
			case (GameMessage)17:
			case (GameMessage)27:
			case (GameMessage)28:
			case (GameMessage)29:
			case (GameMessage)43:
			case (GameMessage)44:
			case (GameMessage)45:
			case (GameMessage)46:
			case (GameMessage)47:
			case (GameMessage)48:
			case (GameMessage)49:
			case (GameMessage)51:
			case (GameMessage)52:
			case (GameMessage)57:
			case (GameMessage)58:
			case (GameMessage)59:
			case (GameMessage)66:
			case (GameMessage)67:
			case (GameMessage)68:
			case (GameMessage)69:
			case (GameMessage)77:
			case (GameMessage)78:
			case (GameMessage)79:
			case (GameMessage)82:
			case (GameMessage)84:
			case (GameMessage)85:
			case (GameMessage)86:
			case (GameMessage)87:
			case (GameMessage)88:
			case (GameMessage)89:
			case (GameMessage)98:
			case (GameMessage)99:
			case (GameMessage)103:
			case (GameMessage)104:
			case (GameMessage)105:
			case (GameMessage)106:
			case (GameMessage)107:
			case (GameMessage)108:
			case (GameMessage)109:
			case (GameMessage)115:
			case (GameMessage)116:
			case (GameMessage)117:
			case (GameMessage)118:
			case (GameMessage)119:
			case (GameMessage)124:
			case (GameMessage)125:
			case (GameMessage)126:
			case (GameMessage)127:
			case (GameMessage)128:
			case (GameMessage)129:
			case (GameMessage)134:
			case (GameMessage)135:
			case (GameMessage)136:
			case (GameMessage)137:
			case (GameMessage)138:
			case (GameMessage)139:
			case (GameMessage)144:
			case (GameMessage)145:
			case (GameMessage)146:
			case (GameMessage)147:
			case (GameMessage)148:
			case (GameMessage)149:
			case (GameMessage)150:
			case (GameMessage)151:
			case (GameMessage)152:
			case (GameMessage)153:
			case (GameMessage)154:
			case (GameMessage)155:
			case (GameMessage)156:
			case (GameMessage)157:
			case (GameMessage)158:
			case (GameMessage)159:
			case (GameMessage)166:
			case (GameMessage)167:
			case (GameMessage)168:
			case (GameMessage)169:
			case (GameMessage)171:
			case (GameMessage)172:
			case (GameMessage)173:
			case (GameMessage)174:
			case (GameMessage)175:
			case (GameMessage)176:
			case (GameMessage)177:
			case (GameMessage)178:
			case (GameMessage)179:
				break;
			case GameMessage.SelectBattleCmd:
				await this.GameMessage_SelectBattleCmd(reader);
				break;
			case GameMessage.SelectIdleCmd:
				await this.GameMessage_SelectIdleCmd(reader);
				break;
			case GameMessage.SelectEffectYn:
				await this.GameMessage_SelectEffectYn(reader);
				break;
			case GameMessage.SelectYesNo:
				await this.GameMessage_SelectYesNo(reader);
				break;
			case GameMessage.SelectOption:
				await this.GameMessage_SelectOption(reader);
				break;
			case GameMessage.SelectCard:
				await this.GameMessage_SelectCard(reader);
				break;
			case GameMessage.SelectChain:
				await this.GameMessage_SelectChain(reader);
				break;
			case GameMessage.SelectPlace:
				await this.GameMessage_SelectPlace(reader);
				break;
			case GameMessage.SelectPosition:
				await this.GameMessage_SelectPosition(reader);
				break;
			case GameMessage.SelectTribute:
				await this.GameMessage_SelectTribute(reader);
				break;
			case GameMessage.SortChain:
				await this.GameMessage_SortChain(reader);
				break;
			case GameMessage.SelectCounter:
				await this.GameMessage_SelectCounter(reader);
				break;
			case GameMessage.SelectSum:
				await this.GameMessage_SelectSum(reader);
				break;
			case GameMessage.SelectDisfield:
				await this.GameMessage_SelectDisfield(reader);
				break;
			case GameMessage.SortCard:
				await this.GameMessage_SortCard(reader);
				break;
			case GameMessage.SelectUnselect:
				await this.GameMessage_SelectUnselect(reader);
				break;
			case GameMessage.ConfirmDecktop:
				await this.GameMessage_ConfirmDecktop(reader);
				break;
			case GameMessage.ConfirmCards:
				await this.GameMessage_ConfirmCards(reader);
				break;
			case GameMessage.ShuffleDeck:
				await this.GameMessage_ShuffleDeck(reader);
				break;
			case GameMessage.ShuffleHand:
				await this.GameMessage_ShuffleHand(reader);
				break;
			case GameMessage.RefreshDeck:
				await this.GameMessage_RefreshDeck(reader);
				break;
			case GameMessage.SwapGraveDeck:
				await this.GameMessage_SwapGraveDeck(reader);
				break;
			case GameMessage.ShuffleSetCard:
				await this.GameMessage_ShuffleSetCard(reader);
				break;
			case GameMessage.ReverseDeck:
				await this.GameMessage_ReverseDeck(reader);
				break;
			case GameMessage.DeckTop:
				await this.GameMessage_DeckTop(reader);
				break;
			case GameMessage.ShuffleExtra:
				await this.GameMessage_ShuffleExtra(reader);
				break;
			case GameMessage.NewTurn:
				await this.GameMessage_NewTurn(reader);
				break;
			case GameMessage.NewPhase:
				await this.GameMessage_NewPhase(reader);
				break;
			case GameMessage.ConfirmExtratop:
				await this.GameMessage_ConfirmExtratop(reader);
				break;
			case GameMessage.Move:
				await this.GameMessage_Move(reader);
				break;
			case GameMessage.PosChange:
				await this.GameMessage_PosChange(reader);
				break;
			case GameMessage.Set:
				await this.GameMessage_Set(reader);
				break;
			case GameMessage.Swap:
				await this.GameMessage_Swap(reader);
				break;
			case GameMessage.FieldDisabled:
				await this.GameMessage_FieldDisabled(reader);
				break;
			case GameMessage.Summoning:
				await this.GameMessage_Summoning(reader);
				break;
			case GameMessage.Summoned:
				await this.GameMessage_Summoned(reader);
				break;
			case GameMessage.SpSummoning:
				await this.GameMessage_SpSummoning(reader);
				break;
			case GameMessage.SpSummoned:
				await this.GameMessage_SpSummoned(reader);
				break;
			case GameMessage.FlipSummoning:
				await this.GameMessage_FlipSummoning(reader);
				break;
			case GameMessage.FlipSummoned:
				await this.GameMessage_FlipSummoned(reader);
				break;
			case GameMessage.Chaining:
				await this.GameMessage_Chaining(reader);
				break;
			case GameMessage.Chained:
				await this.GameMessage_Chained(reader);
				break;
			case GameMessage.ChainSolving:
				await this.GameMessage_ChainSolving(reader);
				break;
			case GameMessage.ChainSolved:
				await this.GameMessage_ChainSolved(reader);
				break;
			case GameMessage.ChainEnd:
				await this.GameMessage_ChainEnd(reader);
				break;
			case GameMessage.ChainNegated:
				await this.GameMessage_ChainNegated(reader);
				break;
			case GameMessage.ChainDisabled:
				await this.GameMessage_ChainDisabled(reader);
				break;
			case GameMessage.CardSelected:
				await this.GameMessage_CardSelected(reader);
				break;
			case GameMessage.RandomSelected:
				await this.GameMessage_RandomSelected(reader);
				break;
			case GameMessage.BecomeTarget:
				await this.GameMessage_BecomeTarget(reader);
				break;
			case GameMessage.Draw:
				await this.GameMessage_Draw(reader);
				break;
			case GameMessage.Damage:
				await this.GameMessage_Damage(reader);
				break;
			case GameMessage.Recover:
				await this.GameMessage_Recover(reader);
				break;
			case GameMessage.Equip:
				await this.GameMessage_Equip(reader);
				break;
			case GameMessage.LpUpdate:
				await this.GameMessage_LpUpdate(reader);
				break;
			case GameMessage.Unequip:
				await this.GameMessage_Unequip(reader);
				break;
			case GameMessage.CardTarget:
				await this.GameMessage_CardTarget(reader);
				break;
			case GameMessage.CancelTarget:
				await this.GameMessage_CancelTarget(reader);
				break;
			case GameMessage.PayLpCost:
				await this.GameMessage_PayLpCost(reader);
				break;
			case GameMessage.AddCounter:
				await this.GameMessage_AddCounter(reader);
				break;
			case GameMessage.RemoveCounter:
				await this.GameMessage_RemoveCounter(reader);
				break;
			case GameMessage.Attack:
				await this.GameMessage_Attack(reader);
				break;
			case GameMessage.Battle:
				await this.GameMessage_Battle(reader);
				break;
			case GameMessage.AttackDisabled:
				await this.GameMessage_AttackDisabled(reader);
				break;
			case GameMessage.DamageStepStart:
				await this.GameMessage_DamageStepStart(reader);
				break;
			case GameMessage.DamageStepEnd:
				await this.GameMessage_DamageStepEnd(reader);
				break;
			case GameMessage.MissedEffect:
				await this.GameMessage_MissedEffect(reader);
				break;
			case GameMessage.BeChainTarget:
				await this.GameMessage_BeChainTarget(reader);
				break;
			case GameMessage.CreateRelation:
				await this.GameMessage_CreateRelation(reader);
				break;
			case GameMessage.ReleaseRelation:
				await this.GameMessage_ReleaseRelation(reader);
				break;
			case GameMessage.TossCoin:
				await this.GameMessage_TossCoin(reader);
				break;
			case GameMessage.TossDice:
				await this.GameMessage_TossDice(reader);
				break;
			case GameMessage.RockPaperScissors:
				await this.GameMessage_RockPaperScissors(reader);
				break;
			case GameMessage.HandResult:
				await this.GameMessage_HandResult(reader);
				break;
			case GameMessage.AnnounceRace:
				await this.GameMessage_AnnounceRace(reader);
				break;
			case GameMessage.AnnounceAttrib:
				await this.GameMessage_AnnounceAttrib(reader);
				break;
			case GameMessage.AnnounceCard:
				await this.GameMessage_AnnounceCard(reader);
				break;
			case GameMessage.AnnounceNumber:
				await this.GameMessage_AnnounceNumber(reader);
				break;
			case GameMessage.CardHint:
				await this.GameMessage_CardHint(reader);
				break;
			case GameMessage.TagSwap:
				await this.GameMessage_TagSwap(reader);
				break;
			case GameMessage.ReloadField:
				await this.GameMessage_ReloadField(reader);
				break;
			case GameMessage.AiName:
				await this.GameMessage_AiName(reader);
				break;
			case GameMessage.ShowHint:
				await this.GameMessage_ShowHint(reader);
				break;
			case GameMessage.PlayerHint:
				await this.GameMessage_PlayerHint(reader);
				break;
			case GameMessage.MatchKill:
				await this.GameMessage_MatchKill(reader);
				break;
			case GameMessage.CustomMsg:
				await this.GameMessage_CustomMsg(reader);
				break;
			default:
				if (message != GameMessage.DuelWinner)
				{
					switch (message)
					{
					case GameMessage.sibyl_chat:
						await this.GameMessage_sibyl_chat(reader);
						break;
					case GameMessage.sibyl_replay:
						await this.GameMessage_sibyl_replay(reader);
						break;
					case GameMessage.sibyl_clear:
						await this.GameMessage_sibyl_clear(reader);
						break;
					case GameMessage.sibyl_delay:
						await this.GameMessage_sibyl_delay(reader);
						break;
					case GameMessage.sibyl_book:
						await this.GameMessage_sibyl_book(reader);
						break;
					case GameMessage.sibyl_name:
						await this.GameMessage_sibyl_name(reader);
						break;
					case GameMessage.sibyl_quit:
						await this.GameMessage_sibyl_quit(reader);
						break;
					}
				}
				else
				{
					await this.GameMessage_DuelWinner(reader);
				}
				break;
			}
		}

		// Token: 0x06009C7C RID: 40060 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Retry(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C7D RID: 40061 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Hint(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C7E RID: 40062 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Waiting(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C7F RID: 40063 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Start(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C80 RID: 40064 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Win(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C81 RID: 40065 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_UpdateData(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C82 RID: 40066 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_UpdateCard(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C83 RID: 40067 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_RequestDeck(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C84 RID: 40068 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SelectBattleCmd(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C85 RID: 40069 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SelectIdleCmd(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C86 RID: 40070 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SelectEffectYn(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C87 RID: 40071 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SelectYesNo(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C88 RID: 40072 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SelectOption(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C89 RID: 40073 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SelectCard(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C8A RID: 40074 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SelectChain(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C8B RID: 40075 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SelectPlace(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C8C RID: 40076 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SelectPosition(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C8D RID: 40077 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SelectTribute(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C8E RID: 40078 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SortChain(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C8F RID: 40079 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SelectCounter(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C90 RID: 40080 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SelectSum(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C91 RID: 40081 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SelectDisfield(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C92 RID: 40082 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SortCard(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C93 RID: 40083 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SelectUnselect(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C94 RID: 40084 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ConfirmDecktop(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C95 RID: 40085 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ConfirmCards(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C96 RID: 40086 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ShuffleDeck(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C97 RID: 40087 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ShuffleHand(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C98 RID: 40088 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_RefreshDeck(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C99 RID: 40089 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SwapGraveDeck(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C9A RID: 40090 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ShuffleSetCard(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C9B RID: 40091 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ReverseDeck(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C9C RID: 40092 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_DeckTop(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C9D RID: 40093 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ShuffleExtra(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C9E RID: 40094 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_NewTurn(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009C9F RID: 40095 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_NewPhase(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CA0 RID: 40096 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ConfirmExtratop(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CA1 RID: 40097 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Move(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CA2 RID: 40098 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_PosChange(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CA3 RID: 40099 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Set(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CA4 RID: 40100 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Swap(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CA5 RID: 40101 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_FieldDisabled(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CA6 RID: 40102 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Summoning(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CA7 RID: 40103 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Summoned(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CA8 RID: 40104 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SpSummoning(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CA9 RID: 40105 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_SpSummoned(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CAA RID: 40106 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_FlipSummoning(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CAB RID: 40107 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_FlipSummoned(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CAC RID: 40108 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Chaining(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CAD RID: 40109 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Chained(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CAE RID: 40110 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ChainSolving(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CAF RID: 40111 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ChainSolved(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CB0 RID: 40112 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ChainEnd(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CB1 RID: 40113 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ChainNegated(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CB2 RID: 40114 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ChainDisabled(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CB3 RID: 40115 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_CardSelected(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CB4 RID: 40116 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_RandomSelected(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CB5 RID: 40117 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_BecomeTarget(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CB6 RID: 40118 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Draw(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CB7 RID: 40119 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Damage(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CB8 RID: 40120 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Recover(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CB9 RID: 40121 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Equip(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CBA RID: 40122 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_LpUpdate(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CBB RID: 40123 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Unequip(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CBC RID: 40124 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_CardTarget(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CBD RID: 40125 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_CancelTarget(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CBE RID: 40126 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_PayLpCost(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CBF RID: 40127 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_AddCounter(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CC0 RID: 40128 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_RemoveCounter(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CC1 RID: 40129 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Attack(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CC2 RID: 40130 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_Battle(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CC3 RID: 40131 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_AttackDisabled(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CC4 RID: 40132 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_DamageStepStart(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CC5 RID: 40133 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_DamageStepEnd(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CC6 RID: 40134 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_MissedEffect(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CC7 RID: 40135 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_BeChainTarget(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CC8 RID: 40136 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_CreateRelation(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CC9 RID: 40137 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ReleaseRelation(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CCA RID: 40138 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_TossCoin(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CCB RID: 40139 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_TossDice(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CCC RID: 40140 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_RockPaperScissors(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CCD RID: 40141 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_HandResult(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CCE RID: 40142 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_AnnounceRace(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CCF RID: 40143 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_AnnounceAttrib(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CD0 RID: 40144 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_AnnounceCard(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CD1 RID: 40145 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_AnnounceNumber(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CD2 RID: 40146 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_CardHint(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CD3 RID: 40147 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_TagSwap(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CD4 RID: 40148 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ReloadField(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CD5 RID: 40149 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_AiName(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CD6 RID: 40150 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_ShowHint(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CD7 RID: 40151 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_PlayerHint(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CD8 RID: 40152 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_MatchKill(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CD9 RID: 40153 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_CustomMsg(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CDA RID: 40154 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_DuelWinner(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CDB RID: 40155 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_sibyl_chat(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CDC RID: 40156 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_sibyl_replay(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CDD RID: 40157 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_sibyl_clear(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CDE RID: 40158 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_sibyl_delay(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CDF RID: 40159 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_sibyl_book(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CE0 RID: 40160 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_sibyl_name(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x06009CE1 RID: 40161 RVA: 0x0018F23F File Offset: 0x0018D43F
		protected virtual UniTask GameMessage_sibyl_quit(BinaryReader reader)
		{
			return UniTask.CompletedTask;
		}

		// Token: 0x0400DAB5 RID: 55989
		protected MessageDispatcher dispatcher;
	}
}
