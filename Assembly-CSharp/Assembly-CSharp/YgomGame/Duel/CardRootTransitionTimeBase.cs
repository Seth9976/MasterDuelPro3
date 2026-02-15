using System;

namespace YgomGame.Duel
{
	// Token: 0x02000CFD RID: 3325
	public abstract class CardRootTransitionTimeBase : CardRootTransition
	{
		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06005FA7 RID: 24487 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected virtual float dulation
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06005FA8 RID: 24488
		protected abstract void UpdateTransition(float t);

		// Token: 0x06005FA9 RID: 24489 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool OnUpdate()
		{
			return false;
		}

		// Token: 0x06005FAA RID: 24490 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnImmediate()
		{
		}

		// Token: 0x04009A80 RID: 39552
		private float time;
	}
}
