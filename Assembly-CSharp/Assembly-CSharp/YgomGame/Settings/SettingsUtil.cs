using System;
using System.Runtime.CompilerServices;
using YgomGame.Card;

namespace YgomGame.Settings
{
	// Token: 0x02000981 RID: 2433
	public class SettingsUtil
	{
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600476E RID: 18286 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int bgmVolume
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600476F RID: 18287 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int seVolume
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06004770 RID: 18288 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int voiceVolume
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06004771 RID: 18289 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool bgmMute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06004772 RID: 18290 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool seMute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06004773 RID: 18291 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool voiceMute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06004774 RID: 18292 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int performance
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06004775 RID: 18293 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.BasicParam.QUALITY quality
		{
			get
			{
				return SettingsUtil.BasicParam.QUALITY.STANDARD;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06004776 RID: 18294 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int resolution
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06004777 RID: 18295 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool fullScreen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06004778 RID: 18296 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.BasicParam.CARD_TEXT_SIZE cardTextSize
		{
			get
			{
				return SettingsUtil.BasicParam.CARD_TEXT_SIZE.LARGE;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06004779 RID: 18297 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool vibration
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x0600477A RID: 18298 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool chat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600477B RID: 18299 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool pushNotyfy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600477C RID: 18300 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.DuelParam.CHAIN_TYPE chainType
		{
			get
			{
				return SettingsUtil.DuelParam.CHAIN_TYPE.MY_CHAIN_ON;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600477D RID: 18301 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.DuelParam.MANUAL_TYPE manualType
		{
			get
			{
				return SettingsUtil.DuelParam.MANUAL_TYPE.NONE;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600477E RID: 18302 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.DuelParam.LOCATE_TYPE locateType
		{
			get
			{
				return SettingsUtil.DuelParam.LOCATE_TYPE.AUTO;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x0600477F RID: 18303 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.DuelParam.COMMAND_TYPE commandType
		{
			get
			{
				return SettingsUtil.DuelParam.COMMAND_TYPE.MASTERDUEL;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06004780 RID: 18304 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool autoPlay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06004781 RID: 18305 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool emote
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06004782 RID: 18306 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool decideActivateOrder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06004783 RID: 18307 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int showCardInfoType
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06004784 RID: 18308 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool showAudienceInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06004785 RID: 18309 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool showSetCard
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06004786 RID: 18310 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool showBattleStep
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06004787 RID: 18311 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.DuelParam.SKIP_TYPE skipSummonEffectType
		{
			get
			{
				return SettingsUtil.DuelParam.SKIP_TYPE.NONE;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06004788 RID: 18312 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.DuelParam.SKIP_TYPE skipMonsterCutinType
		{
			get
			{
				return SettingsUtil.DuelParam.SKIP_TYPE.NONE;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06004789 RID: 18313 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.DuelParam.SKIP_TYPE skipCardRunEffectType
		{
			get
			{
				return SettingsUtil.DuelParam.SKIP_TYPE.NONE;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x0600478A RID: 18314 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool useConsoleLayout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600478B RID: 18315 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.DuelParam.CAMERA_TYPE cameraType
		{
			get
			{
				return SettingsUtil.DuelParam.CAMERA_TYPE.NEAR;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600478C RID: 18316 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int showRivalName
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x0600478D RID: 18317 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int showCardReportType
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x0600478E RID: 18318 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int showHappenedEffectType
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x0600478F RID: 18319 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int audienceShowCardInfoType
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06004790 RID: 18320 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool audienceShowAudienceInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06004791 RID: 18321 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool audienceShowSetCard
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06004792 RID: 18322 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool audienceShowBattleStep
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06004793 RID: 18323 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.AudienceParam.SKIP_TYPE audienceSkipSummonEffectType
		{
			get
			{
				return SettingsUtil.AudienceParam.SKIP_TYPE.NONE;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06004794 RID: 18324 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.AudienceParam.SKIP_TYPE audienceSkipMonsterCutinType
		{
			get
			{
				return SettingsUtil.AudienceParam.SKIP_TYPE.NONE;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06004795 RID: 18325 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.AudienceParam.SKIP_TYPE audienceSkipCardRunEffectType
		{
			get
			{
				return SettingsUtil.AudienceParam.SKIP_TYPE.NONE;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06004796 RID: 18326 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool audienceUseConsoleLayout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06004797 RID: 18327 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SettingsUtil.AudienceParam.CAMERA_TYPE audienceCameraType
		{
			get
			{
				return SettingsUtil.AudienceParam.CAMERA_TYPE.NEAR;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06004798 RID: 18328 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int audienceShowRivalName
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06004799 RID: 18329 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int audienceShowCardReportType
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600479A RID: 18330 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int audienceShowHappenedEffectType
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600479B RID: 18331 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600479C RID: 18332 RVA: 0x0000216D File Offset: 0x0000036D
		public static bool isUseLightImages
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SaveSettings()
		{
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SaveSettings(SettingsUtil.DuelParam duelParam, SettingsUtil.SoundParam soundParam)
		{
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SaveSettings(SettingsUtil.DuelParam duelParam, SettingsUtil.SoundParam soundParam, SettingsUtil.BasicParam basicParam, SettingsUtil.SystemParam systemParam, SettingsUtil.AudienceParam audienceParam)
		{
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InitializeSettings()
		{
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ApplySettings(bool isDirtyQuality = true)
		{
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OnUpdateSettingsJsonData(object obj)
		{
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ApplyVolumeBGM(float volume)
		{
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ApplyVolumeSE(float volume)
		{
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ApplyVolumeVoice(float volume)
		{
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPerformance(int param)
		{
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetQuality(SettingsUtil.BasicParam.QUALITY quality)
		{
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x000029CC File Offset: 0x00000BCC
		public static CardQuality GetCardQuality()
		{
			return CardQuality.HIGH;
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetNumSelectableResolutions()
		{
			return 0;
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetResolution(int param)
		{
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetFullScreen(bool isFullScreen)
		{
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsChatSettingEnable()
		{
			return false;
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetVibration(bool vibration)
		{
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetCache()
		{
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetBgmVolume()
		{
			return 0;
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetSeVolume()
		{
			return 0;
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetVoiceVolume()
		{
			return 0;
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetBgmMute()
		{
			return false;
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetSeMute()
		{
			return false;
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetVoiceMute()
		{
			return false;
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetPerformance()
		{
			return 0;
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x000029CC File Offset: 0x00000BCC
		private static SettingsUtil.BasicParam.QUALITY GetQuality()
		{
			return SettingsUtil.BasicParam.QUALITY.STANDARD;
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetResolution()
		{
			return 0;
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x000029CC File Offset: 0x00000BCC
		private static SettingsUtil.BasicParam.CARD_TEXT_SIZE GetCardTextSize()
		{
			return SettingsUtil.BasicParam.CARD_TEXT_SIZE.LARGE;
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetVibration()
		{
			return false;
		}

		// Token: 0x060047BA RID: 18362 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetChat()
		{
			return false;
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetPushNotyfy()
		{
			return false;
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x000029CC File Offset: 0x00000BCC
		private static SettingsUtil.DuelParam.CHAIN_TYPE GetChainType()
		{
			return SettingsUtil.DuelParam.CHAIN_TYPE.MY_CHAIN_ON;
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x000029CC File Offset: 0x00000BCC
		private static SettingsUtil.DuelParam.MANUAL_TYPE GetManualType()
		{
			return SettingsUtil.DuelParam.MANUAL_TYPE.NONE;
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x000029CC File Offset: 0x00000BCC
		private static SettingsUtil.DuelParam.LOCATE_TYPE GetLocateType()
		{
			return SettingsUtil.DuelParam.LOCATE_TYPE.AUTO;
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x000029CC File Offset: 0x00000BCC
		private static SettingsUtil.DuelParam.COMMAND_TYPE GetCommandType()
		{
			return SettingsUtil.DuelParam.COMMAND_TYPE.MASTERDUEL;
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetEmote()
		{
			return false;
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetAutoPlay()
		{
			return false;
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetDecideActivateOrder()
		{
			return false;
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetShowCardInfoType()
		{
			return 0;
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetShowAudienceInfo()
		{
			return false;
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetShowSetCard()
		{
			return false;
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetShowBattleStep()
		{
			return false;
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x000029CC File Offset: 0x00000BCC
		private static SettingsUtil.DuelParam.SKIP_TYPE GetSkipSummonEffectType()
		{
			return SettingsUtil.DuelParam.SKIP_TYPE.NONE;
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x000029CC File Offset: 0x00000BCC
		private static SettingsUtil.DuelParam.SKIP_TYPE GetSkipMonsterCutinType()
		{
			return SettingsUtil.DuelParam.SKIP_TYPE.NONE;
		}

		// Token: 0x060047C9 RID: 18377 RVA: 0x000029CC File Offset: 0x00000BCC
		private static SettingsUtil.DuelParam.SKIP_TYPE GetSkipCardRunEffectType()
		{
			return SettingsUtil.DuelParam.SKIP_TYPE.NONE;
		}

		// Token: 0x060047CA RID: 18378 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetUseConsoleLayout()
		{
			return false;
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x000029CC File Offset: 0x00000BCC
		private static SettingsUtil.DuelParam.CAMERA_TYPE GetCameraType()
		{
			return SettingsUtil.DuelParam.CAMERA_TYPE.NEAR;
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetShowRivalName()
		{
			return 0;
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetShowCardReportType()
		{
			return 0;
		}

		// Token: 0x060047CE RID: 18382 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetShowHappenedEffectType()
		{
			return 0;
		}

		// Token: 0x060047CF RID: 18383 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetAudienceShowCardInfoType()
		{
			return 0;
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetAudienceShowAudienceInfo()
		{
			return false;
		}

		// Token: 0x060047D1 RID: 18385 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetAudienceSetCard()
		{
			return false;
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetAudienceShowBattleStep()
		{
			return false;
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x000029CC File Offset: 0x00000BCC
		private static SettingsUtil.AudienceParam.SKIP_TYPE GetAudienceSkipSummonEffectType()
		{
			return SettingsUtil.AudienceParam.SKIP_TYPE.NONE;
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x000029CC File Offset: 0x00000BCC
		private static SettingsUtil.AudienceParam.SKIP_TYPE GetAudienceSkipMonsterCutinType()
		{
			return SettingsUtil.AudienceParam.SKIP_TYPE.NONE;
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x000029CC File Offset: 0x00000BCC
		private static SettingsUtil.AudienceParam.SKIP_TYPE GetAudienceSkipCardRunEffectType()
		{
			return SettingsUtil.AudienceParam.SKIP_TYPE.NONE;
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool GetAudienceUseConsoleLayout()
		{
			return false;
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x000029CC File Offset: 0x00000BCC
		private static SettingsUtil.AudienceParam.CAMERA_TYPE GetAudienceCameraType()
		{
			return SettingsUtil.AudienceParam.CAMERA_TYPE.NEAR;
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetAudienceShowRivalName()
		{
			return 0;
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetAudienceShowCardReportType()
		{
			return 0;
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetAudienceShowHappenedEfectType()
		{
			return 0;
		}

		// Token: 0x040085B8 RID: 34232
		public static readonly string SETTING_JSON_PATH;

		// Token: 0x040085B9 RID: 34233
		public static readonly string CHAT_SETTING_PATH;

		// Token: 0x040085BA RID: 34234
		private static readonly SettingsUtil.SoundParam defaultSoundParam;

		// Token: 0x040085BB RID: 34235
		private static readonly SettingsUtil.BasicParam defaultBasicParam;

		// Token: 0x040085BC RID: 34236
		private static readonly SettingsUtil.SystemParam defaultSystemParam;

		// Token: 0x040085BD RID: 34237
		private static readonly SettingsUtil.DuelParam defaultDuelParam;

		// Token: 0x040085BE RID: 34238
		private static readonly SettingsUtil.AudienceParam defaultAudienceParam;

		// Token: 0x040085BF RID: 34239
		private static SettingsUtil.SoundParam soundParamCache;

		// Token: 0x040085C0 RID: 34240
		private static SettingsUtil.BasicParam basicParamCache;

		// Token: 0x040085C1 RID: 34241
		private static SettingsUtil.SystemParam systemParamCache;

		// Token: 0x040085C2 RID: 34242
		private static SettingsUtil.DuelParam duelParamCache;

		// Token: 0x040085C3 RID: 34243
		private static SettingsUtil.AudienceParam audienceParamCache;

		// Token: 0x040085C4 RID: 34244
		private static readonly int BaseWidth;

		// Token: 0x040085C5 RID: 34245
		private static readonly int BaseHeight;

		// Token: 0x040085C6 RID: 34246
		public static readonly int[] RESOLUTION_WIDTH;

		// Token: 0x040085C7 RID: 34247
		public static readonly int[] RESOLUTION_HEIGHT;

		// Token: 0x02000982 RID: 2434
		public struct SoundParam
		{
			// Token: 0x040085C8 RID: 34248
			public int bgm;

			// Token: 0x040085C9 RID: 34249
			public int se;

			// Token: 0x040085CA RID: 34250
			public int voice;

			// Token: 0x040085CB RID: 34251
			public bool bgmMute;

			// Token: 0x040085CC RID: 34252
			public bool seMute;

			// Token: 0x040085CD RID: 34253
			public bool voiceMute;

			// Token: 0x02000983 RID: 2435
			public enum INIT_TYPE
			{
				// Token: 0x040085CF RID: 34255
				DEFAULT,
				// Token: 0x040085D0 RID: 34256
				WORK
			}
		}

		// Token: 0x02000984 RID: 2436
		public struct BasicParam
		{
			// Token: 0x040085D1 RID: 34257
			public int perform;

			// Token: 0x040085D2 RID: 34258
			public SettingsUtil.BasicParam.QUALITY quality;

			// Token: 0x040085D3 RID: 34259
			public int resolution;

			// Token: 0x040085D4 RID: 34260
			public bool fullScreen;

			// Token: 0x040085D5 RID: 34261
			public SettingsUtil.BasicParam.CARD_TEXT_SIZE cardTextSize;

			// Token: 0x040085D6 RID: 34262
			public bool vibration;

			// Token: 0x02000985 RID: 2437
			public enum INIT_TYPE
			{
				// Token: 0x040085D8 RID: 34264
				DEFAULT,
				// Token: 0x040085D9 RID: 34265
				WORK
			}

			// Token: 0x02000986 RID: 2438
			public enum CARD_TEXT_SIZE
			{
				// Token: 0x040085DB RID: 34267
				LARGE,
				// Token: 0x040085DC RID: 34268
				MEDIUM,
				// Token: 0x040085DD RID: 34269
				SMALL
			}

			// Token: 0x02000987 RID: 2439
			public enum QUALITY
			{
				// Token: 0x040085DF RID: 34271
				STANDARD,
				// Token: 0x040085E0 RID: 34272
				RECOMMENDED,
				// Token: 0x040085E1 RID: 34273
				HIGH
			}
		}

		// Token: 0x02000988 RID: 2440
		public struct SystemParam
		{
			// Token: 0x040085E2 RID: 34274
			public bool notify;

			// Token: 0x040085E3 RID: 34275
			public bool chat;

			// Token: 0x02000989 RID: 2441
			public enum INIT_TYPE
			{
				// Token: 0x040085E5 RID: 34277
				DEFAULT,
				// Token: 0x040085E6 RID: 34278
				WORK
			}
		}

		// Token: 0x0200098A RID: 2442
		public enum SHOW_CARDREPORT_TYPE
		{
			// Token: 0x040085E8 RID: 34280
			NONE,
			// Token: 0x040085E9 RID: 34281
			ALL
		}

		// Token: 0x0200098B RID: 2443
		public enum SHOW_RIVALNAME_TYPE
		{
			// Token: 0x040085EB RID: 34283
			HIDE,
			// Token: 0x040085EC RID: 34284
			SHOW
		}

		// Token: 0x0200098C RID: 2444
		public enum SHOW_HAPPENEDEFFECT_TYPE
		{
			// Token: 0x040085EE RID: 34286
			OFF,
			// Token: 0x040085EF RID: 34287
			ON,
			// Token: 0x040085F0 RID: 34288
			ON_ONLY_DUELSTATUS
		}

		// Token: 0x0200098D RID: 2445
		public struct DuelParam
		{
			// Token: 0x040085F1 RID: 34289
			public SettingsUtil.DuelParam.CHAIN_TYPE chainType;

			// Token: 0x040085F2 RID: 34290
			public SettingsUtil.DuelParam.MANUAL_TYPE manualType;

			// Token: 0x040085F3 RID: 34291
			public SettingsUtil.DuelParam.LOCATE_TYPE locateType;

			// Token: 0x040085F4 RID: 34292
			public SettingsUtil.DuelParam.COMMAND_TYPE commandType;

			// Token: 0x040085F5 RID: 34293
			public bool autoPlayEnable;

			// Token: 0x040085F6 RID: 34294
			public bool enemyEmote;

			// Token: 0x040085F7 RID: 34295
			public bool decideActivateOrder;

			// Token: 0x040085F8 RID: 34296
			public int showCardInfoType;

			// Token: 0x040085F9 RID: 34297
			public bool showAudienceInfo;

			// Token: 0x040085FA RID: 34298
			public bool showSetCard;

			// Token: 0x040085FB RID: 34299
			public bool showBattleStep;

			// Token: 0x040085FC RID: 34300
			public SettingsUtil.DuelParam.SKIP_TYPE skipSummonEffectType;

			// Token: 0x040085FD RID: 34301
			public SettingsUtil.DuelParam.SKIP_TYPE skipMonsterCutinType;

			// Token: 0x040085FE RID: 34302
			public SettingsUtil.DuelParam.SKIP_TYPE skipCardRunEffectType;

			// Token: 0x040085FF RID: 34303
			public bool useConsoleLayout;

			// Token: 0x04008600 RID: 34304
			public SettingsUtil.DuelParam.CAMERA_TYPE cameraType;

			// Token: 0x04008601 RID: 34305
			public int showRivalName;

			// Token: 0x04008602 RID: 34306
			public int showCardReportType;

			// Token: 0x04008603 RID: 34307
			public int showHappenedEffectType;

			// Token: 0x0200098E RID: 2446
			public enum INIT_TYPE
			{
				// Token: 0x04008605 RID: 34309
				DEFAULT,
				// Token: 0x04008606 RID: 34310
				WORK
			}

			// Token: 0x0200098F RID: 2447
			public enum CHAIN_TYPE
			{
				// Token: 0x04008608 RID: 34312
				MY_CHAIN_ON,
				// Token: 0x04008609 RID: 34313
				MY_CHAIN_OFF
			}

			// Token: 0x02000990 RID: 2448
			public enum MANUAL_TYPE
			{
				// Token: 0x0400860B RID: 34315
				NONE,
				// Token: 0x0400860C RID: 34316
				TOUCH,
				// Token: 0x0400860D RID: 34317
				TOGGLE,
				// Token: 0x0400860E RID: 34318
				TOUCH2,
				// Token: 0x0400860F RID: 34319
				TOUCH3,
				// Token: 0x04008610 RID: 34320
				TOGGLE2
			}

			// Token: 0x02000991 RID: 2449
			public enum LOCATE_TYPE
			{
				// Token: 0x04008612 RID: 34322
				AUTO,
				// Token: 0x04008613 RID: 34323
				MANUAL
			}

			// Token: 0x02000992 RID: 2450
			public enum COMMAND_TYPE
			{
				// Token: 0x04008615 RID: 34325
				MASTERDUEL,
				// Token: 0x04008616 RID: 34326
				DUELLINKS
			}

			// Token: 0x02000993 RID: 2451
			public enum SKIP_TYPE
			{
				// Token: 0x04008618 RID: 34328
				NONE,
				// Token: 0x04008619 RID: 34329
				ONETIME,
				// Token: 0x0400861A RID: 34330
				ALWAYS
			}

			// Token: 0x02000994 RID: 2452
			public enum CAMERA_TYPE
			{
				// Token: 0x0400861C RID: 34332
				NEAR,
				// Token: 0x0400861D RID: 34333
				FAR
			}

			// Token: 0x02000995 RID: 2453
			public enum SHOW_CARDINFO_TYPE
			{
				// Token: 0x0400861F RID: 34335
				NONE,
				// Token: 0x04008620 RID: 34336
				AUTO_ALL,
				// Token: 0x04008621 RID: 34337
				AUTO_ONLY_CARDSHOW
			}
		}

		// Token: 0x02000996 RID: 2454
		public struct AudienceParam
		{
			// Token: 0x04008622 RID: 34338
			public bool enemyEmote;

			// Token: 0x04008623 RID: 34339
			public int showCardInfoType;

			// Token: 0x04008624 RID: 34340
			public bool showAudienceInfo;

			// Token: 0x04008625 RID: 34341
			public bool showSetCard;

			// Token: 0x04008626 RID: 34342
			public bool showBattleStep;

			// Token: 0x04008627 RID: 34343
			public SettingsUtil.AudienceParam.SKIP_TYPE skipSummonEffectType;

			// Token: 0x04008628 RID: 34344
			public SettingsUtil.AudienceParam.SKIP_TYPE skipMonsterCutinType;

			// Token: 0x04008629 RID: 34345
			public SettingsUtil.AudienceParam.SKIP_TYPE skipCardRunEffectType;

			// Token: 0x0400862A RID: 34346
			public bool useConsoleLayout;

			// Token: 0x0400862B RID: 34347
			public SettingsUtil.AudienceParam.CAMERA_TYPE cameraType;

			// Token: 0x0400862C RID: 34348
			public int showRivalName;

			// Token: 0x0400862D RID: 34349
			public int showCardReportType;

			// Token: 0x0400862E RID: 34350
			public int showHappenedEffectType;

			// Token: 0x02000997 RID: 2455
			public enum INIT_TYPE
			{
				// Token: 0x04008630 RID: 34352
				DEFAULT,
				// Token: 0x04008631 RID: 34353
				WORK
			}

			// Token: 0x02000998 RID: 2456
			public enum SKIP_TYPE
			{
				// Token: 0x04008633 RID: 34355
				NONE,
				// Token: 0x04008634 RID: 34356
				ALWAYS,
				// Token: 0x04008635 RID: 34357
				ONETIME
			}

			// Token: 0x02000999 RID: 2457
			public enum CAMERA_TYPE
			{
				// Token: 0x04008637 RID: 34359
				NEAR,
				// Token: 0x04008638 RID: 34360
				FAR
			}

			// Token: 0x0200099A RID: 2458
			public enum SHOW_CARDINFO_TYPE
			{
				// Token: 0x0400863A RID: 34362
				NONE,
				// Token: 0x0400863B RID: 34363
				AUTO_ALL,
				// Token: 0x0400863C RID: 34364
				AUTO_ONLY_CARDSHOW
			}
		}
	}
}
