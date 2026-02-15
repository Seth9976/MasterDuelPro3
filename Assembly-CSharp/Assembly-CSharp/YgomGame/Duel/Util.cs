using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomGame.Settings;

namespace YgomGame.Duel
{
	// Token: 0x02000F39 RID: 3897
	public class Util
	{
		// Token: 0x060072E0 RID: 29408 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initiaize()
		{
		}

		// Token: 0x060072E1 RID: 29409 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ShowResult()
		{
			return 0;
		}

		// Token: 0x060072E2 RID: 29410 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Util.GameMode GetGameMode()
		{
			return Util.GameMode.Normal;
		}

		// Token: 0x060072E3 RID: 29411 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsSingleMode()
		{
			return false;
		}

		// Token: 0x060072E4 RID: 29412 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsQuestionMode()
		{
			return false;
		}

		// Token: 0x060072E5 RID: 29413 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsAudience()
		{
			return false;
		}

		// Token: 0x060072E6 RID: 29414 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsReplay()
		{
			return false;
		}

		// Token: 0x060072E7 RID: 29415 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsOnlineMode()
		{
			return false;
		}

		// Token: 0x060072E8 RID: 29416 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Util.EvalType EvaluatorGetEval(int player)
		{
			return Util.EvalType.Advantage;
		}

		// Token: 0x060072E9 RID: 29417 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetNextPath()
		{
			return null;
		}

		// Token: 0x060072EA RID: 29418 RVA: 0x0000216A File Offset: 0x0000036A
		public static int[] GetMrkByFinishType(Engine.FinishType finishType)
		{
			return null;
		}

		// Token: 0x060072EB RID: 29419 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetSleeveId(int uniqueId)
		{
			return 0;
		}

		// Token: 0x060072EC RID: 29420 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetSleeveIdFromPlayer(int player)
		{
			return 0;
		}

		// Token: 0x060072ED RID: 29421 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetFaceIconIdFromPlayer(int player)
		{
			return 0;
		}

		// Token: 0x060072EE RID: 29422 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetFaceIconFrameIdFromPlayer(int player)
		{
			return 0;
		}

		// Token: 0x060072EF RID: 29423 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetAvatarIconIdFromPlayer(int player)
		{
			return 0;
		}

		// Token: 0x060072F0 RID: 29424 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetAvatarStandIdFromPlayer(int player)
		{
			return 0;
		}

		// Token: 0x060072F1 RID: 29425 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetGraveIdFromPlayer(int player)
		{
			return 0;
		}

		// Token: 0x060072F2 RID: 29426 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetMatIdFromPlayer(int player)
		{
			return 0;
		}

		// Token: 0x060072F3 RID: 29427 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetPcodeFromPlayer(int player)
		{
			return 0L;
		}

		// Token: 0x060072F4 RID: 29428 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IncludeMySelf()
		{
			return false;
		}

		// Token: 0x060072F5 RID: 29429 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetRankFromPlayer(int player)
		{
			return 0;
		}

		// Token: 0x060072F6 RID: 29430 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Util.PlatformID GetPlatformID(int player)
		{
			return Util.PlatformID.Invalid;
		}

		// Token: 0x060072F7 RID: 29431 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int Myself()
		{
			return 0;
		}

		// Token: 0x060072F8 RID: 29432 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int FirstPlayer()
		{
			return 0;
		}

		// Token: 0x060072F9 RID: 29433 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Util.OpeningMessageType GetOpeningMessageType(int player)
		{
			return Util.OpeningMessageType.None;
		}

		// Token: 0x060072FA RID: 29434 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetOpeningMessageBgPath(int player)
		{
			return null;
		}

		// Token: 0x060072FB RID: 29435 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetOpeningMessageText(int player)
		{
			return null;
		}

		// Token: 0x060072FC RID: 29436 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetUserName(int player)
		{
			return null;
		}

		// Token: 0x060072FD RID: 29437 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetMaskedName(int idx)
		{
			return null;
		}

		// Token: 0x060072FE RID: 29438 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetSpAccountTypeid(int player)
		{
			return 0;
		}

		// Token: 0x060072FF RID: 29439 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsBlockRelativeCardList()
		{
			return false;
		}

		// Token: 0x06007300 RID: 29440 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsSpFieldUsed()
		{
			return false;
		}

		// Token: 0x06007301 RID: 29441 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetDuelistLevel(int player)
		{
			return 0;
		}

		// Token: 0x06007302 RID: 29442 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetRankText(int player)
		{
			return null;
		}

		// Token: 0x06007303 RID: 29443 RVA: 0x0000216A File Offset: 0x0000036A
		public static string[] GetRankIconPaths(int player)
		{
			return null;
		}

		// Token: 0x06007304 RID: 29444 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetUserDetailBgPath(int player)
		{
			return null;
		}

		// Token: 0x06007305 RID: 29445 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCountryId(int player)
		{
			return 0;
		}

		// Token: 0x06007306 RID: 29446 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetCountryName(int player)
		{
			return null;
		}

		// Token: 0x06007307 RID: 29447 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsNewCountry(int player)
		{
			return false;
		}

		// Token: 0x06007308 RID: 29448 RVA: 0x0000216A File Offset: 0x0000036A
		public static string[] GetFreeTextOfWin(int player)
		{
			return null;
		}

		// Token: 0x06007309 RID: 29449 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool FreeTextOfWinExists(int player)
		{
			return false;
		}

		// Token: 0x0600730A RID: 29450 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetFreeTextOfTurnChange(int player)
		{
			return null;
		}

		// Token: 0x0600730B RID: 29451 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool FreeTextOfTurnChangeExists(int player)
		{
			return false;
		}

		// Token: 0x0600730C RID: 29452 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetRealtimeSpectate()
		{
			return false;
		}

		// Token: 0x0600730D RID: 29453 RVA: 0x000F6478 File Offset: 0x000F4678
		public static ValueTuple<int, int, PvpMenuDefine.MatchingType> GetLogoInfo(int logoMixID)
		{
			return default(ValueTuple<int, int, PvpMenuDefine.MatchingType>);
		}

		// Token: 0x0600730E RID: 29454 RVA: 0x000F6490 File Offset: 0x000F4690
		public static ValueTuple<int, int, PvpMenuDefine.MatchingType> GetLogoInfoFromClientWork()
		{
			return default(ValueTuple<int, int, PvpMenuDefine.MatchingType>);
		}

		// Token: 0x0600730F RID: 29455 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetDuelLiveContinuousCount()
		{
			return 0;
		}

		// Token: 0x06007310 RID: 29456 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDuelLiveCoutinuous()
		{
			return false;
		}

		// Token: 0x06007311 RID: 29457 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DecDuelLiveContinuousCount()
		{
		}

		// Token: 0x06007312 RID: 29458 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetDuelLiveCoutinuousCount(int count)
		{
		}

		// Token: 0x06007313 RID: 29459 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Util.AttackLevel GetAttackLevel(int atk)
		{
			return Util.AttackLevel.Small;
		}

		// Token: 0x06007314 RID: 29460 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] GetQuestionData(int qid)
		{
			return null;
		}

		// Token: 0x06007315 RID: 29461 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetDuelEndReasonTextFormat(Engine.ResultType resultType, Engine.FinishType finishType, string playerNameNear, string playerNameFar)
		{
			return null;
		}

		// Token: 0x06007316 RID: 29462 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetDuelEndReasonText(string textFormat, Engine.ResultType resultType, Engine.FinishType finishType, string playerNameNear, string playerNameFar)
		{
			return null;
		}

		// Token: 0x06007317 RID: 29463 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int PvpNetworkTimedOutThrethold(int defaultTime = 30)
		{
			return 0;
		}

		// Token: 0x06007318 RID: 29464 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCardOwner(int uniqueId)
		{
			return 0;
		}

		// Token: 0x06007319 RID: 29465 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsPveMode()
		{
			return false;
		}

		// Token: 0x0600731A RID: 29466 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsPvPMode()
		{
			return false;
		}

		// Token: 0x0600731B RID: 29467 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetReplayMaxTurn()
		{
			return 0;
		}

		// Token: 0x0600731C RID: 29468 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Util.PublicLevel GetPublicLevel()
		{
			return Util.PublicLevel.AllClose;
		}

		// Token: 0x0600731D RID: 29469 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetChapter()
		{
			return 0;
		}

		// Token: 0x0600731E RID: 29470 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsRentalDeck()
		{
			return false;
		}

		// Token: 0x0600731F RID: 29471 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetPlayerProfile(int player, bool isMyself)
		{
			return null;
		}

		// Token: 0x06007320 RID: 29472 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetIsSameOS(int player, bool myself)
		{
			return false;
		}

		// Token: 0x06007321 RID: 29473 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetOnlineID(int player, bool isSameOS)
		{
			return null;
		}

		// Token: 0x06007322 RID: 29474 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Util.DisplayType GetDisplayType()
		{
			return Util.DisplayType.Vista;
		}

		// Token: 0x06007323 RID: 29475 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMobileLayout()
		{
			return false;
		}

		// Token: 0x06007324 RID: 29476 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCameraTypeNear()
		{
			return false;
		}

		// Token: 0x06007325 RID: 29477 RVA: 0x000F64A8 File Offset: 0x000F46A8
		public static Vector3 ScreenPointToFixedHeightWorldPoint(Vector2 screenPoint, float height, Camera camera)
		{
			return default(Vector3);
		}

		// Token: 0x06007326 RID: 29478 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsStrategicTutorial()
		{
			return false;
		}

		// Token: 0x06007327 RID: 29479 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsTutorialDone()
		{
			return false;
		}

		// Token: 0x06007328 RID: 29480 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.DuelParam.CHAIN_TYPE GetChainTypeSetting()
		{
			return SettingsUtil.DuelParam.CHAIN_TYPE.MY_CHAIN_ON;
		}

		// Token: 0x06007329 RID: 29481 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDecisionActivationOrder()
		{
			return false;
		}

		// Token: 0x0600732A RID: 29482 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsAutoLocation()
		{
			return false;
		}

		// Token: 0x0600732B RID: 29483 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool UseRetry()
		{
			return false;
		}

		// Token: 0x0600732C RID: 29484 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsAutoShowCardInfo(bool cardShow)
		{
			return false;
		}

		// Token: 0x0600732D RID: 29485 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsShowCardReport()
		{
			return false;
		}

		// Token: 0x0600732E RID: 29486 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsShowAudienceInfo()
		{
			return false;
		}

		// Token: 0x0600732F RID: 29487 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsShowBattleStep()
		{
			return false;
		}

		// Token: 0x06007330 RID: 29488 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsShowSetCard()
		{
			return false;
		}

		// Token: 0x06007331 RID: 29489 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.DuelParam.MANUAL_TYPE GetManualType()
		{
			return SettingsUtil.DuelParam.MANUAL_TYPE.NONE;
		}

		// Token: 0x06007332 RID: 29490 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.SHOW_HAPPENEDEFFECT_TYPE GetHappenedEffectType()
		{
			return SettingsUtil.SHOW_HAPPENEDEFFECT_TYPE.OFF;
		}

		// Token: 0x06007333 RID: 29491 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsFastestReplay()
		{
			return false;
		}

		// Token: 0x06007334 RID: 29492 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsSkipSummonEffect(Engine.SpSummonType spSummonType)
		{
			return false;
		}

		// Token: 0x06007335 RID: 29493 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RecordPlayedSpSummonEffectType(Engine.SpSummonType spSummonType)
		{
		}

		// Token: 0x06007336 RID: 29494 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsSkipMonsterCutin(int cardID)
		{
			return false;
		}

		// Token: 0x06007337 RID: 29495 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RecordPlayedMonsterCutinCardID(int cardID)
		{
		}

		// Token: 0x06007338 RID: 29496 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsSkipCardRunEffect(int cardID)
		{
			return false;
		}

		// Token: 0x06007339 RID: 29497 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RecordPlayedCardRunEffectCardID(int cardID)
		{
		}

		// Token: 0x0600733A RID: 29498 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsHidePlayerName()
		{
			return false;
		}

		// Token: 0x0600733B RID: 29499 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool UseDoubleClickDecide()
		{
			return false;
		}

		// Token: 0x0600733C RID: 29500 RVA: 0x000F64C0 File Offset: 0x000F46C0
		public static ValueTuple<SharedDefinition.Location, int, int, int, int> GetCrossPos(SharedDefinition.Location location, int centerPosition)
		{
			return default(ValueTuple<SharedDefinition.Location, int, int, int, int>);
		}

		// Token: 0x0600733D RID: 29501 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsInsightTargetCard(int player, int position, bool showSetCardOption)
		{
			return false;
		}

		// Token: 0x0600733E RID: 29502 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsStatusDetailTargetCard(int position)
		{
			return false;
		}

		// Token: 0x0600733F RID: 29503 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetTotalAtk(int player)
		{
			return 0;
		}

		// Token: 0x06007340 RID: 29504 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowProfileCard(Transform parent, Transform buttonParent, int player, bool isMyself, bool force, Dictionary<string, object> profileData = null)
		{
		}

		// Token: 0x06007341 RID: 29505 RVA: 0x0000216A File Offset: 0x0000036A
		public static GameObject ShowProfileCard(Transform parent, Transform buttonParent, bool force, Dictionary<string, object> profileData)
		{
			return null;
		}

		// Token: 0x06007342 RID: 29506 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsFixedPositonCommand()
		{
			return false;
		}

		// Token: 0x06007343 RID: 29507 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetStyleID(int uniqueID)
		{
			return 0;
		}

		// Token: 0x06007344 RID: 29508 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool UseKeyActivateToggle()
		{
			return false;
		}

		// Token: 0x06007345 RID: 29509 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UpdateOverrider(GameObject gob, bool force = false)
		{
		}

		// Token: 0x0400AC37 RID: 44087
		public static Dictionary<int, int> initDeckNum;

		// Token: 0x0400AC38 RID: 44088
		public static Dictionary<int, int> initExDeckNum;

		// Token: 0x0400AC39 RID: 44089
		private static GameObject profileCardParent;

		// Token: 0x0400AC3A RID: 44090
		private static List<Engine.SpSummonType> playedSpSummonEffect;

		// Token: 0x0400AC3B RID: 44091
		private static List<int> playedMonsterCutinCardID;

		// Token: 0x0400AC3C RID: 44092
		private static List<int> playedCardRunEffectCardID;

		// Token: 0x0400AC3D RID: 44093
		private const string PLAYER_NAME_MASKED = "DUELIST";

		// Token: 0x0400AC3E RID: 44094
		private const string PLAYER_NAME_MASKED_A = "DUELIST A";

		// Token: 0x0400AC3F RID: 44095
		private const string PLAYER_NAME_MASKED_B = "DUELIST B";

		// Token: 0x0400AC40 RID: 44096
		private const string PLAYER_NAME_MASKED_C = "DUELIST C";

		// Token: 0x0400AC41 RID: 44097
		private const string PLAYER_NAME_MASKED_D = "DUELIST D";

		// Token: 0x0400AC42 RID: 44098
		private const string PLAYER_NAME_MASKED_E = "DUELIST E";

		// Token: 0x0400AC43 RID: 44099
		private const int SpAccessoryId_WCS = 2002;

		// Token: 0x0400AC44 RID: 44100
		public const int PosPhaseButton = 19;

		// Token: 0x02000F3A RID: 3898
		public enum GameMode
		{
			// Token: 0x0400AC46 RID: 44102
			Normal,
			// Token: 0x0400AC47 RID: 44103
			Free,
			// Token: 0x0400AC48 RID: 44104
			Single,
			// Token: 0x0400AC49 RID: 44105
			Rank,
			// Token: 0x0400AC4A RID: 44106
			Tournament,
			// Token: 0x0400AC4B RID: 44107
			TournamentSingle,
			// Token: 0x0400AC4C RID: 44108
			Audience,
			// Token: 0x0400AC4D RID: 44109
			Replay,
			// Token: 0x0400AC4E RID: 44110
			RankSingle,
			// Token: 0x0400AC4F RID: 44111
			SoloSingle,
			// Token: 0x0400AC50 RID: 44112
			Room,
			// Token: 0x0400AC51 RID: 44113
			Exhibition,
			// Token: 0x0400AC52 RID: 44114
			DuelistCup,
			// Token: 0x0400AC53 RID: 44115
			RankEvent,
			// Token: 0x0400AC54 RID: 44116
			TeamMatch,
			// Token: 0x0400AC55 RID: 44117
			DuelTrial,
			// Token: 0x0400AC56 RID: 44118
			WCS,
			// Token: 0x0400AC57 RID: 44119
			Versus,
			// Token: 0x0400AC58 RID: 44120
			WcsFinal,
			// Token: 0x0400AC59 RID: 44121
			Null
		}

		// Token: 0x02000F3B RID: 3899
		public enum EvalType
		{
			// Token: 0x0400AC5B RID: 44123
			Advantage,
			// Token: 0x0400AC5C RID: 44124
			Normal,
			// Token: 0x0400AC5D RID: 44125
			Danger
		}

		// Token: 0x02000F3C RID: 3900
		public enum AttackLevel
		{
			// Token: 0x0400AC5F RID: 44127
			Small,
			// Token: 0x0400AC60 RID: 44128
			Medium,
			// Token: 0x0400AC61 RID: 44129
			Large,
			// Token: 0x0400AC62 RID: 44130
			Largest
		}

		// Token: 0x02000F3D RID: 3901
		public enum OpeningMessageType
		{
			// Token: 0x0400AC64 RID: 44132
			None,
			// Token: 0x0400AC65 RID: 44133
			Promotion,
			// Token: 0x0400AC66 RID: 44134
			Demotion
		}

		// Token: 0x02000F3E RID: 3902
		public enum PlatformID
		{
			// Token: 0x0400AC68 RID: 44136
			Invalid,
			// Token: 0x0400AC69 RID: 44137
			Android,
			// Token: 0x0400AC6A RID: 44138
			iOS,
			// Token: 0x0400AC6B RID: 44139
			Steam,
			// Token: 0x0400AC6C RID: 44140
			PS4,
			// Token: 0x0400AC6D RID: 44141
			NX,
			// Token: 0x0400AC6E RID: 44142
			XboxOne,
			// Token: 0x0400AC6F RID: 44143
			Stadia,
			// Token: 0x0400AC70 RID: 44144
			PS5,
			// Token: 0x0400AC71 RID: 44145
			XboxSX,
			// Token: 0x0400AC72 RID: 44146
			Editor = 100
		}

		// Token: 0x02000F3F RID: 3903
		public enum PublicLevel
		{
			// Token: 0x0400AC74 RID: 44148
			AllClose,
			// Token: 0x0400AC75 RID: 44149
			FrontOpen,
			// Token: 0x0400AC76 RID: 44150
			AllOpen
		}

		// Token: 0x02000F40 RID: 3904
		public enum DisplayType
		{
			// Token: 0x0400AC78 RID: 44152
			Vista,
			// Token: 0x0400AC79 RID: 44153
			Standard,
			// Token: 0x0400AC7A RID: 44154
			MobileDevice
		}
	}
}
