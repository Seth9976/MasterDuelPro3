using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000D2A RID: 3370
	public class DuelActivatedCardEffect
	{
		// Token: 0x060061C4 RID: 25028 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CutinActivate(int player)
		{
		}

		// Token: 0x060061C5 RID: 25029 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CardHappen(int cardId, int efxNo)
		{
		}

		// Token: 0x060061C6 RID: 25030 RVA: 0x0000216D File Offset: 0x0000036D
		public static void EffectTaskRunDialog(int cardId, int efxNo, int player)
		{
		}

		// Token: 0x060061C7 RID: 25031 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetShowActivatedIconCardEffectTextByButton(bool show)
		{
		}

		// Token: 0x060061C8 RID: 25032 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetUniqueIdActivatedIconCardEffectTextByButton(int uId)
		{
		}

		// Token: 0x060061C9 RID: 25033 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckActivatedCardEffectNo(int cardId, int owner)
		{
			return false;
		}

		// Token: 0x060061CA RID: 25034 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<int> GetActivatedCardEffectNo(int cardId, int owner)
		{
			return null;
		}

		// Token: 0x060061CB RID: 25035 RVA: 0x0000216D File Offset: 0x0000036D
		private static void AddActivatedCardEffectNoDict(Dictionary<int, List<int>> activatedCardEffectNoDict, int cardId, int efxNo)
		{
		}

		// Token: 0x060061CC RID: 25036 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InitActivatedCardEffectDict()
		{
		}

		// Token: 0x060061CD RID: 25037 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetActivatedCardEffectDictValue()
		{
		}

		// Token: 0x04009CBE RID: 40126
		private static Dictionary<int, List<int>> myActivatedCardEffectNoDict;

		// Token: 0x04009CBF RID: 40127
		private static Dictionary<int, List<int>> rivalActivatedCardEffectNoDict;

		// Token: 0x04009CC0 RID: 40128
		private static bool myCardActivated;

		// Token: 0x04009CC1 RID: 40129
		private static bool rivalCardActivated;

		// Token: 0x04009CC2 RID: 40130
		public static bool showingActivatedIconCardEffectTextByButton;

		// Token: 0x04009CC3 RID: 40131
		public static int uIdActivatedIconCardEffectTextByButton;
	}
}
