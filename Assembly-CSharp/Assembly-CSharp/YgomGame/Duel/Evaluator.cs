using System;

namespace YgomGame.Duel
{
	// Token: 0x02000E89 RID: 3721
	public class Evaluator
	{
		// Token: 0x06006C08 RID: 27656 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool MaybeFinishingBlow(int player)
		{
			return false;
		}

		// Token: 0x06006C09 RID: 27657 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool MaybeLastAttack(int player, int srcLocate, int dstLocate)
		{
			return false;
		}

		// Token: 0x06006C0A RID: 27658 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool MaybeLastAttackImpl(int player, int srcLocate, int dstLocate)
		{
			return false;
		}

		// Token: 0x06006C0B RID: 27659 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetMaxDamage(int turnPlayer)
		{
			return 0;
		}

		// Token: 0x06006C0C RID: 27660 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetTotalAtk(int player)
		{
			return 0;
		}
	}
}
