using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000EAE RID: 3758
	public class LethalEffect : MonoBehaviour
	{
		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06006D6E RID: 28014 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006D6F RID: 28015 RVA: 0x0000216D File Offset: 0x0000036D
		public bool playing
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

		// Token: 0x06006D70 RID: 28016 RVA: 0x0000216A File Offset: 0x0000036A
		public static LethalEffect Create(DuelGameObjectManager goManager)
		{
			return null;
		}

		// Token: 0x06006D71 RID: 28017 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play(int loser, Action onFinished, bool useEffect, Vector3 effectPosition, bool draw, bool isDeckOut, LethalEffect.EffectType effectType, Vector3 attackDirection)
		{
		}

		// Token: 0x06006D72 RID: 28018 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0400A883 RID: 43139
		private AllCardBreaker allCardBreaker;

		// Token: 0x0400A884 RID: 43140
		private AllCardBreaker allCardBreakerOtherSide;

		// Token: 0x0400A885 RID: 43141
		private bool playedSlow;

		// Token: 0x0400A886 RID: 43142
		private DuelGameObjectManager goManager;

		// Token: 0x0400A887 RID: 43143
		private Action onFinished;

		// Token: 0x02000EAF RID: 3759
		public enum EffectType
		{
			// Token: 0x0400A889 RID: 43145
			Normal,
			// Token: 0x0400A88A RID: 43146
			DarkMagician,
			// Token: 0x0400A88B RID: 43147
			BlueEyes,
			// Token: 0x0400A88C RID: 43148
			RedEyes
		}
	}
}
