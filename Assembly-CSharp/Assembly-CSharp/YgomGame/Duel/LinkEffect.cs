using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000EB1 RID: 3761
	public class LinkEffect : SummonEffectBase
	{
		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06006D7B RID: 28027 RVA: 0x000029CC File Offset: 0x00000BCC
		public override Engine.SpSummonType spSummonType
		{
			get
			{
				return Engine.SpSummonType.Fusion;
			}
		}

		// Token: 0x06006D7C RID: 28028 RVA: 0x0000216A File Offset: 0x0000036A
		public static LinkEffect Create()
		{
			return null;
		}

		// Token: 0x06006D7D RID: 28029 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Load(int destCardID, int destCardUniqueID, int[] materialCardIDs, int[] materialUniqueIDs, int materialNum, int destRareID, bool destIsMyself)
		{
		}

		// Token: 0x06006D7E RID: 28030 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool PlayEffect(Action onFinished)
		{
			return false;
		}

		// Token: 0x06006D7F RID: 28031 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayLinkEffect(int materialNum, Action onFinished)
		{
		}

		// Token: 0x0400A88E RID: 43150
		private List<int> linkMarker;
	}
}
