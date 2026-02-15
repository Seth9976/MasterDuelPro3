using System;

namespace YgomGame.Duel
{
	// Token: 0x02000F46 RID: 3910
	public class XyzEffect : SummonEffectBase
	{
		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06007356 RID: 29526 RVA: 0x000029CC File Offset: 0x00000BCC
		public override Engine.SpSummonType spSummonType
		{
			get
			{
				return Engine.SpSummonType.Fusion;
			}
		}

		// Token: 0x06007357 RID: 29527 RVA: 0x0000216A File Offset: 0x0000036A
		public static XyzEffect Create()
		{
			return null;
		}

		// Token: 0x06007358 RID: 29528 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Load(int destCardID, int destCardUniqueID, int[] materialCardIDs, int[] materialUniqueIDs, int materialNum, int destRareID, bool destIsMyself)
		{
		}

		// Token: 0x06007359 RID: 29529 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool PlayEffect(Action onFinished)
		{
			return false;
		}

		// Token: 0x0600735A RID: 29530 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayXyzEffect(int materialNum, Action onFinished)
		{
		}
	}
}
