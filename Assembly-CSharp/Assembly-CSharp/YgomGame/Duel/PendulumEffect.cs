using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000EDA RID: 3802
	public class PendulumEffect : SummonEffectBase
	{
		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06006ECD RID: 28365 RVA: 0x000029CC File Offset: 0x00000BCC
		public override Engine.SpSummonType spSummonType
		{
			get
			{
				return Engine.SpSummonType.Fusion;
			}
		}

		// Token: 0x06006ECE RID: 28366 RVA: 0x0000216A File Offset: 0x0000036A
		public static PendulumEffect Create()
		{
			return null;
		}

		// Token: 0x06006ECF RID: 28367 RVA: 0x0000216D File Offset: 0x0000036D
		public void LoadScaleCard(int leftCardID, int leftUniqueID, int leftScale, int rightCardID, int rightUniqueID, int rightScale)
		{
		}

		// Token: 0x06006ED0 RID: 28368 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool PlayEffect(Action onFinished)
		{
			return false;
		}

		// Token: 0x06006ED1 RID: 28369 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayPendulumEffect(int materialNum, Action onFinished)
		{
		}

		// Token: 0x0400A9A8 RID: 43432
		private Texture leftScaleSpriteOnes;

		// Token: 0x0400A9A9 RID: 43433
		private Texture leftScaleSpriteTens;

		// Token: 0x0400A9AA RID: 43434
		private Texture rightScaleSpriteOnes;

		// Token: 0x0400A9AB RID: 43435
		private Texture rightScaleSpriteTens;

		// Token: 0x0400A9AC RID: 43436
		private Texture2D leftScaleTextureFront;

		// Token: 0x0400A9AD RID: 43437
		private Material leftScaleTextureBack;

		// Token: 0x0400A9AE RID: 43438
		private Texture2D rightScaleTextureFront;

		// Token: 0x0400A9AF RID: 43439
		private Material rightScaleTextureBack;

		// Token: 0x0400A9B0 RID: 43440
		private int leftScale;

		// Token: 0x0400A9B1 RID: 43441
		private int rightScale;

		// Token: 0x0400A9B2 RID: 43442
		private const string SUMMON_PENDULUM = "Duel/Timeline/Duel/Universal/Summon/SummonPendulum/SummonPendulum01";
	}
}
