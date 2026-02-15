using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CFE RID: 3326
	public static class CardRunEffect
	{
		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06005FAC RID: 24492 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005FAD RID: 24493 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06005FAE RID: 24494 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isReserved
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005FAF RID: 24495 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadSetting(Action loaded_callback, DuelGameObjectManager go_manager)
		{
		}

		// Token: 0x06005FB0 RID: 24496 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadEffect(int mrk, CardRunEffectSetting.Player player, Action<CardRunEffectSetting.CardRunEffectInfo> onLoaded)
		{
		}

		// Token: 0x06005FB1 RID: 24497 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ReserveEffect(int mrk, CardRunEffectSetting.Player player, Vector3 position)
		{
			return false;
		}

		// Token: 0x06005FB2 RID: 24498 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool PlayOnChainRun(CardRunEffectSetting.Player player, Vector3 position, Action on_stop = null)
		{
			return false;
		}

		// Token: 0x06005FB3 RID: 24499 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool PlayOnCardBreak(CardRunEffectSetting.Player player, Vector3 position, Action on_stop = null)
		{
			return false;
		}

		// Token: 0x06005FB4 RID: 24500 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool PlayOnCardMove(CardRunEffectSetting.Player player, Vector3 position, Action on_stop = null)
		{
			return false;
		}

		// Token: 0x06005FB5 RID: 24501 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardRunEffectSetting.CardRunEffectInfo GetOnSpecialFxInfo()
		{
			return null;
		}

		// Token: 0x06005FB6 RID: 24502 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool PlayOnCardDisable(CardRunEffectSetting.Player player, Vector3 position, Action on_stop = null)
		{
			return false;
		}

		// Token: 0x06005FB7 RID: 24503 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OnChainEnd()
		{
		}

		// Token: 0x06005FB8 RID: 24504 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetRerservedEffectCardID()
		{
			return 0;
		}

		// Token: 0x06005FB9 RID: 24505 RVA: 0x000029CC File Offset: 0x00000BCC
		public static CardRunEffectSetting.RunTiming GerReservedEffectPlayTiming()
		{
			return CardRunEffectSetting.RunTiming.ChainRun;
		}

		// Token: 0x06005FBA RID: 24506 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool Play(CardRunEffectSetting.Player player, CardRunEffectSetting.CardRunEffectInfo info, Vector3 position, Action on_stop)
		{
			return false;
		}

		// Token: 0x06005FBB RID: 24507 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool PlayEffect(CardRunEffectSetting.Player player, CardRunEffectSetting.PlayType playType, string effectPath, int effectType, float delay, CardRunEffectSetting.RotationType rotationType, Vector3 pivot, CardRunEffectSetting.ExtraSetting extraSetting, Vector3 position, Action onStop, CardRoot targetCard = null)
		{
			return false;
		}

		// Token: 0x06005FBC RID: 24508 RVA: 0x0000216D File Offset: 0x0000036D
		private static void OnStop(CardRunEffectSetting.Player player, CardRunEffectSetting.ExtraSetting extraSetting)
		{
		}

		// Token: 0x06005FBD RID: 24509 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PlaySE(string seLabel, bool is3D, Vector3 position)
		{
		}

		// Token: 0x06005FBE RID: 24510 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ApplyExtraSettingPositionActivation(Transform effect, CardRunEffectSetting.Player player)
		{
		}

		// Token: 0x06005FBF RID: 24511 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ApplyExtraSettingCardAttackPosition(Transform effect, bool isAttack)
		{
		}

		// Token: 0x06005FC0 RID: 24512 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ApplyChangeLayerMagic(CardRunEffectSetting.Player player, bool onStart)
		{
		}

		// Token: 0x06005FC1 RID: 24513 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ApplyChangeLayerDuelOver3D(GameObject effect)
		{
		}

		// Token: 0x06005FC2 RID: 24514 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsExist(int cardID, CardRunEffectSetting.Player player)
		{
			return false;
		}

		// Token: 0x06005FC3 RID: 24515 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsExist(int cardID)
		{
			return false;
		}

		// Token: 0x06005FC4 RID: 24516 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsEssential(int cardID, CardRunEffectSetting.Player player)
		{
			return false;
		}

		// Token: 0x06005FC5 RID: 24517 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsEssential(int cardID)
		{
			return false;
		}

		// Token: 0x04009A81 RID: 39553
		private static string settingPath;

		// Token: 0x04009A82 RID: 39554
		public static CardRunEffectSetting setting;

		// Token: 0x04009A83 RID: 39555
		private static DuelGameObjectManager goManager;

		// Token: 0x04009A84 RID: 39556
		private static CardRunEffectSetting.CardRunEffectInfo reservedEffect;
	}
}
