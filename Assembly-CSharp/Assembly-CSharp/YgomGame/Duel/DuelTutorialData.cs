using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000DA4 RID: 3492
	public class DuelTutorialData : ScriptableObject
	{
		// Token: 0x060066AF RID: 26287 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelTutorialData.MessageInfo Get(int turn, DuelTutorialData.View view, List<DuelTutorialData.MessageInfo> ignoreList)
		{
			return null;
		}

		// Token: 0x060066B0 RID: 26288 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelTutorialData.MessageInfo Get(int index, int turn, DuelTutorialData.View view)
		{
			return null;
		}

		// Token: 0x060066B1 RID: 26289 RVA: 0x0000216A File Offset: 0x0000036A
		public List<string> GetHelp()
		{
			return null;
		}

		// Token: 0x0400A0C4 RID: 41156
		public List<DuelTutorialData.MessageInfo> infoList;

		// Token: 0x0400A0C5 RID: 41157
		public bool playClimaxBGM;

		// Token: 0x0400A0C6 RID: 41158
		public bool isFirstTutorial;

		// Token: 0x0400A0C7 RID: 41159
		public bool isStrategy;

		// Token: 0x02000DA5 RID: 3493
		[Serializable]
		public class MessageInfo
		{
			// Token: 0x0400A0C8 RID: 41160
			public int startTurn;

			// Token: 0x0400A0C9 RID: 41161
			public DuelTutorialData.View startView;

			// Token: 0x0400A0CA RID: 41162
			public float startDelay;

			// Token: 0x0400A0CB RID: 41163
			public List<string> centerMessage;

			// Token: 0x0400A0CC RID: 41164
			public bool useTopMessage;

			// Token: 0x0400A0CD RID: 41165
			public string topMessage;

			// Token: 0x0400A0CE RID: 41166
			public int finishTurn;

			// Token: 0x0400A0CF RID: 41167
			public DuelTutorialData.View finishView;

			// Token: 0x0400A0D0 RID: 41168
			public List<Engine.Phase> invalidPhase;

			// Token: 0x0400A0D1 RID: 41169
			public string invalidPhaseMessage;

			// Token: 0x0400A0D2 RID: 41170
			public List<Engine.CommandType> invalidCommand;

			// Token: 0x0400A0D3 RID: 41171
			public string invalidCommandMessage;

			// Token: 0x0400A0D4 RID: 41172
			public List<DuelTutorialData.EffectInfo> effectList;

			// Token: 0x0400A0D5 RID: 41173
			public bool skipCardActivation;

			// Token: 0x0400A0D6 RID: 41174
			public bool noCancel;

			// Token: 0x0400A0D7 RID: 41175
			public string noCancelMessage;

			// Token: 0x0400A0D8 RID: 41176
			public CardCommandEx.StandType invalidStandType;

			// Token: 0x0400A0D9 RID: 41177
			public string invalidStandTypeMessage;

			// Token: 0x0400A0DA RID: 41178
			public List<int> invalidCardID;

			// Token: 0x0400A0DB RID: 41179
			public string invalidCardIDMessage;

			// Token: 0x0400A0DC RID: 41180
			public int effectSelectionTarget;

			// Token: 0x0400A0DD RID: 41181
			public string invalidEffectSelectionTargetMessage;

			// Token: 0x0400A0DE RID: 41182
			public List<int> locationTargetPosition;

			// Token: 0x0400A0DF RID: 41183
			public bool blockExtraDeckCommand;

			// Token: 0x0400A0E0 RID: 41184
			public string blockExtraDeckCommandMessage;

			// Token: 0x0400A0E1 RID: 41185
			public string helpLabelPath;
		}

		// Token: 0x02000DA6 RID: 3494
		public enum View
		{
			// Token: 0x0400A0E3 RID: 41187
			None,
			// Token: 0x0400A0E4 RID: 41188
			DuelStart,
			// Token: 0x0400A0E5 RID: 41189
			PhaseChangeDrawStart,
			// Token: 0x0400A0E6 RID: 41190
			PhaseChangeStandbyStart,
			// Token: 0x0400A0E7 RID: 41191
			PhaseChangeMain1Start,
			// Token: 0x0400A0E8 RID: 41192
			PhaseChangeBattleStart,
			// Token: 0x0400A0E9 RID: 41193
			PhaseChangeMain2Start,
			// Token: 0x0400A0EA RID: 41194
			PhaseChangeEndStart,
			// Token: 0x0400A0EB RID: 41195
			CommandSummon,
			// Token: 0x0400A0EC RID: 41196
			AllAttacked,
			// Token: 0x0400A0ED RID: 41197
			CommandAttack,
			// Token: 0x0400A0EE RID: 41198
			CommandSet,
			// Token: 0x0400A0EF RID: 41199
			BattleAttack,
			// Token: 0x0400A0F0 RID: 41200
			CardHappen,
			// Token: 0x0400A0F1 RID: 41201
			ChainEnd,
			// Token: 0x0400A0F2 RID: 41202
			FirstWaitInputMainPhase1,
			// Token: 0x0400A0F3 RID: 41203
			CommandSummonSp,
			// Token: 0x0400A0F4 RID: 41204
			CommandAction,
			// Token: 0x0400A0F5 RID: 41205
			CommandPendulum,
			// Token: 0x0400A0F6 RID: 41206
			CommandBegin,
			// Token: 0x0400A0F7 RID: 41207
			CommandExecuted,
			// Token: 0x0400A0F8 RID: 41208
			LocationBegin,
			// Token: 0x0400A0F9 RID: 41209
			LocationExecuted,
			// Token: 0x0400A0FA RID: 41210
			BeginDialog
		}

		// Token: 0x02000DA7 RID: 3495
		public enum Effect
		{
			// Token: 0x0400A0FC RID: 41212
			HighlightCard,
			// Token: 0x0400A0FD RID: 41213
			HighlightPhase,
			// Token: 0x0400A0FE RID: 41214
			HighlightGrave,
			// Token: 0x0400A0FF RID: 41215
			ArrowCard,
			// Token: 0x0400A100 RID: 41216
			UIHighlightActivateButton,
			// Token: 0x0400A101 RID: 41217
			UIHighlightListCard,
			// Token: 0x0400A102 RID: 41218
			UIHighlightCommand,
			// Token: 0x0400A103 RID: 41219
			UIHighlightPhase,
			// Token: 0x0400A104 RID: 41220
			HighlightExtra,
			// Token: 0x0400A105 RID: 41221
			UIHighlightStandType
		}

		// Token: 0x02000DA8 RID: 3496
		[Serializable]
		public class EffectInfo
		{
			// Token: 0x0400A106 RID: 41222
			public DuelTutorialData.Effect effect;

			// Token: 0x0400A107 RID: 41223
			public int param;
		}
	}
}
