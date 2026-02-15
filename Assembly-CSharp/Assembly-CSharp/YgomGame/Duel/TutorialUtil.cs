using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000F33 RID: 3891
	public static class TutorialUtil
	{
		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x0600728D RID: 29325 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600728E RID: 29326 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool isReady
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

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x0600728F RID: 29327 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007290 RID: 29328 RVA: 0x0000216D File Offset: 0x0000036D
		private static DuelGameObjectManager goManager
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

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06007291 RID: 29329 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007292 RID: 29330 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool isTutorial
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

		// Token: 0x06007293 RID: 29331 RVA: 0x0000216D File Offset: 0x0000036D
		private static void LoadTutorialSetting(Action onFinished)
		{
		}

		// Token: 0x06007294 RID: 29332 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InitializeTutorial(DuelGameObjectManager goManager, Action onFinished)
		{
		}

		// Token: 0x06007295 RID: 29333 RVA: 0x0000216D File Offset: 0x0000036D
		public static void HideTutorialMessage()
		{
		}

		// Token: 0x06007296 RID: 29334 RVA: 0x0000216D File Offset: 0x0000036D
		public static void TerminateTutorial()
		{
		}

		// Token: 0x06007297 RID: 29335 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ShowCenterMessage(IList<string> textIDList, Action finishedCallback, float delay)
		{
		}

		// Token: 0x06007298 RID: 29336 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ShowHelp(string helpLabelPath, Action onFinished)
		{
		}

		// Token: 0x06007299 RID: 29337 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool HelpExists()
		{
			return false;
		}

		// Token: 0x0600729A RID: 29338 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetHelp()
		{
			return null;
		}

		// Token: 0x0600729B RID: 29339 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ShowTopMessage(string textID)
		{
		}

		// Token: 0x0600729C RID: 29340 RVA: 0x0000216D File Offset: 0x0000036D
		private static void HideTopMessage()
		{
		}

		// Token: 0x0600729D RID: 29341 RVA: 0x0000216D File Offset: 0x0000036D
		private static void HideCenterMessage()
		{
		}

		// Token: 0x0600729E RID: 29342 RVA: 0x0000216A File Offset: 0x0000036A
		private static List<string> ParseTextIDs(IList<string> textIDs)
		{
			return null;
		}

		// Token: 0x0600729F RID: 29343 RVA: 0x0000216A File Offset: 0x0000036A
		private static string ParseTextID(string textID)
		{
			return null;
		}

		// Token: 0x060072A0 RID: 29344 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Check(DuelTutorialData.View view, Action finishedCallback)
		{
		}

		// Token: 0x060072A1 RID: 29345 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckPhaseChange(Engine.Phase phase, Action finishedCallback)
		{
		}

		// Token: 0x060072A2 RID: 29346 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckWaitInput(Action finishedCallback)
		{
		}

		// Token: 0x060072A3 RID: 29347 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPhaseChanged()
		{
		}

		// Token: 0x060072A4 RID: 29348 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPreDoCommand(Engine.CommandType commandType)
		{
		}

		// Token: 0x060072A5 RID: 29349 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetPreDoCommand()
		{
		}

		// Token: 0x060072A6 RID: 29350 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckBattleAttack(Action finishedCallback)
		{
		}

		// Token: 0x060072A7 RID: 29351 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckCardHappen(Action finishedCallback)
		{
		}

		// Token: 0x060072A8 RID: 29352 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckChainEnd(Action finishedCallback)
		{
		}

		// Token: 0x060072A9 RID: 29353 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckCommandBegin(Action finishedCallback)
		{
		}

		// Token: 0x060072AA RID: 29354 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckCommandExecuted(Action finishedCallback)
		{
		}

		// Token: 0x060072AB RID: 29355 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckLocationBegin(Action finishedCallback)
		{
		}

		// Token: 0x060072AC RID: 29356 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckLocationExecuted(Action finishedCallback)
		{
		}

		// Token: 0x060072AD RID: 29357 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckBeginDialog(Action finishedCallback)
		{
		}

		// Token: 0x060072AE RID: 29358 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsInvalidPhase(Engine.Phase phase)
		{
			return false;
		}

		// Token: 0x060072AF RID: 29359 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowInvalidPhaseMessage()
		{
		}

		// Token: 0x060072B0 RID: 29360 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsInvalidCommand(Engine.CommandType command)
		{
			return false;
		}

		// Token: 0x060072B1 RID: 29361 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowInvalidCommandMessage()
		{
		}

		// Token: 0x060072B2 RID: 29362 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsInvalidStandType(CardCommandEx.StandType standType)
		{
			return false;
		}

		// Token: 0x060072B3 RID: 29363 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowInvalidStandTypeMessage()
		{
		}

		// Token: 0x060072B4 RID: 29364 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsInvalidCardID(int cardID)
		{
			return false;
		}

		// Token: 0x060072B5 RID: 29365 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowInvalidCardMessage()
		{
		}

		// Token: 0x060072B6 RID: 29366 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowNoCancelMessage()
		{
		}

		// Token: 0x060072B7 RID: 29367 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowBlockExtraDeckCommandMessage()
		{
		}

		// Token: 0x060072B8 RID: 29368 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsEffectSelectionTarget(int effectIndex)
		{
			return false;
		}

		// Token: 0x060072B9 RID: 29369 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowInvalidEffectSelectionTargetMessage()
		{
		}

		// Token: 0x060072BA RID: 29370 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsLocationTargetPosition(int position)
		{
			return false;
		}

		// Token: 0x060072BB RID: 29371 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckEffectField()
		{
		}

		// Token: 0x060072BC RID: 29372 RVA: 0x0000216D File Offset: 0x0000036D
		private static void CheckEffectCard()
		{
		}

		// Token: 0x060072BD RID: 29373 RVA: 0x0000216D File Offset: 0x0000036D
		private static void CheckEffectPhaseButton()
		{
		}

		// Token: 0x060072BE RID: 29374 RVA: 0x0000216D File Offset: 0x0000036D
		private static void CheckEffectGrave()
		{
		}

		// Token: 0x060072BF RID: 29375 RVA: 0x0000216D File Offset: 0x0000036D
		private static void CheckEffectExtra()
		{
		}

		// Token: 0x060072C0 RID: 29376 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckEffectUIPhase(Engine.Phase phase, Transform target)
		{
		}

		// Token: 0x060072C1 RID: 29377 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckEffectUICommand(Engine.CommandType command, Transform target)
		{
		}

		// Token: 0x060072C2 RID: 29378 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckEffectUIActivateButton(Transform target)
		{
		}

		// Token: 0x060072C3 RID: 29379 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckEffectUIListCard(int cardID, Transform target)
		{
		}

		// Token: 0x060072C4 RID: 29380 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCurrentTutorialContainsUIListCard()
		{
			return false;
		}

		// Token: 0x060072C5 RID: 29381 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsEffectTargetUIListCard(int cardID)
		{
			return false;
		}

		// Token: 0x060072C6 RID: 29382 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckEffectUIStandType(CardCommandEx.StandType standType, Transform target)
		{
		}

		// Token: 0x060072C7 RID: 29383 RVA: 0x0000216D File Offset: 0x0000036D
		private static void AddPlayingEffect(DuelTutorialData.EffectInfo effectInfo, SimpleEffect effect)
		{
		}

		// Token: 0x060072C8 RID: 29384 RVA: 0x0000216D File Offset: 0x0000036D
		private static void AddPlayingUIEffect(DuelTutorialData.EffectInfo effectInfo, GameObject effect)
		{
		}

		// Token: 0x060072C9 RID: 29385 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool IsPlayingEffect(DuelTutorialData.EffectInfo effectInfo)
		{
			return false;
		}

		// Token: 0x060072CA RID: 29386 RVA: 0x0000216A File Offset: 0x0000036A
		private static GameObject CreateUIEffect(Transform parent, string showLabel)
		{
			return null;
		}

		// Token: 0x060072CB RID: 29387 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearEffectAll()
		{
		}

		// Token: 0x060072CC RID: 29388 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearUIEffectPhase()
		{
		}

		// Token: 0x060072CD RID: 29389 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearUIEffectCommand()
		{
		}

		// Token: 0x060072CE RID: 29390 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearUIEffectStandType()
		{
		}

		// Token: 0x060072CF RID: 29391 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearUIEffectActivateButton()
		{
		}

		// Token: 0x060072D0 RID: 29392 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearUIEffectListCard()
		{
		}

		// Token: 0x060072D1 RID: 29393 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearUIEffectListCard(int cardID)
		{
		}

		// Token: 0x060072D2 RID: 29394 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ClearUIEffect(DuelTutorialData.Effect effect)
		{
		}

		// Token: 0x060072D3 RID: 29395 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ClearUIEffect(DuelTutorialData.Effect effect, int param)
		{
		}

		// Token: 0x060072D4 RID: 29396 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearUIEffectAll()
		{
		}

		// Token: 0x060072D5 RID: 29397 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsSkipActivation()
		{
			return false;
		}

		// Token: 0x060072D6 RID: 29398 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool NoCancel()
		{
			return false;
		}

		// Token: 0x060072D7 RID: 29399 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool BlockExtraDeckCommand()
		{
			return false;
		}

		// Token: 0x060072D8 RID: 29400 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsClimaxBGMPlayable()
		{
			return false;
		}

		// Token: 0x060072D9 RID: 29401 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsFirstTutorial()
		{
			return false;
		}

		// Token: 0x060072DA RID: 29402 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsStrategy()
		{
			return false;
		}

		// Token: 0x060072DB RID: 29403 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDone()
		{
			return false;
		}

		// Token: 0x0400AC1F RID: 44063
		private static bool initialized;

		// Token: 0x0400AC20 RID: 44064
		private static DuelTutorialSetting setting;

		// Token: 0x0400AC21 RID: 44065
		private static DuelTutorialSetting setting2;

		// Token: 0x0400AC22 RID: 44066
		private static DuelTutorialData data;

		// Token: 0x0400AC23 RID: 44067
		private static DuelTutorialData.MessageInfo currentMessageInfo;

		// Token: 0x0400AC24 RID: 44068
		private static int currentMessageIndex;

		// Token: 0x0400AC25 RID: 44069
		private static List<DuelTutorialData.MessageInfo> doneMessageInfoList;

		// Token: 0x0400AC26 RID: 44070
		private static TutorialUtil.CommandType preDoCommand;

		// Token: 0x0400AC27 RID: 44071
		private static bool phaseStartStep;

		// Token: 0x0400AC28 RID: 44072
		private static List<TutorialUtil.EffectInfo> playingEffectList;

		// Token: 0x0400AC29 RID: 44073
		private static List<TutorialUtil.UIEffectInfo> playingUIEffectList;

		// Token: 0x0400AC2A RID: 44074
		private static GameObject prefabUIEffect;

		// Token: 0x02000F34 RID: 3892
		private enum CommandType
		{
			// Token: 0x0400AC2C RID: 44076
			None,
			// Token: 0x0400AC2D RID: 44077
			Summon,
			// Token: 0x0400AC2E RID: 44078
			Set,
			// Token: 0x0400AC2F RID: 44079
			Attack,
			// Token: 0x0400AC30 RID: 44080
			SummonSp,
			// Token: 0x0400AC31 RID: 44081
			Action,
			// Token: 0x0400AC32 RID: 44082
			Pendulum
		}

		// Token: 0x02000F35 RID: 3893
		private class EffectInfo
		{
			// Token: 0x060072DC RID: 29404 RVA: 0x00002739 File Offset: 0x00000939
			public EffectInfo(DuelTutorialData.EffectInfo info, SimpleEffect effect)
			{
			}

			// Token: 0x0400AC33 RID: 44083
			public DuelTutorialData.EffectInfo info;

			// Token: 0x0400AC34 RID: 44084
			public SimpleEffect effect;
		}

		// Token: 0x02000F36 RID: 3894
		private class UIEffectInfo
		{
			// Token: 0x060072DD RID: 29405 RVA: 0x00002739 File Offset: 0x00000939
			public UIEffectInfo(DuelTutorialData.EffectInfo info, GameObject effect)
			{
			}

			// Token: 0x0400AC35 RID: 44085
			public DuelTutorialData.EffectInfo info;

			// Token: 0x0400AC36 RID: 44086
			public GameObject effect;
		}
	}
}
