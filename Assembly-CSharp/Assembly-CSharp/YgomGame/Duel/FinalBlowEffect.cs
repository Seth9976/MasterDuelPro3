using System;
using UnityEngine.Playables;

namespace YgomGame.Duel
{
	// Token: 0x02000E8E RID: 3726
	public class FinalBlowEffect
	{
		// Token: 0x06006C40 RID: 27712 RVA: 0x0000216A File Offset: 0x0000036A
		public static FinalBlowEffect Create()
		{
			return null;
		}

		// Token: 0x06006C41 RID: 27713 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06006C42 RID: 27714 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play(Action<FinalBlowEffect.State> onStateChanged)
		{
		}

		// Token: 0x06006C43 RID: 27715 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x06006C44 RID: 27716 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetState(FinalBlowEffect.State state)
		{
		}

		// Token: 0x06006C45 RID: 27717 RVA: 0x0000216D File Offset: 0x0000036D
		private void InvokeStateChanged(FinalBlowEffect.State state)
		{
		}

		// Token: 0x06006C46 RID: 27718 RVA: 0x0000216D File Offset: 0x0000036D
		public void Stop()
		{
		}

		// Token: 0x0400A788 RID: 42888
		private PlayableDirector timeline;

		// Token: 0x0400A789 RID: 42889
		private const string prefabPath = "Duel/Timeline/Duel/Universal/DuelFinalBlow/ACDuelFinalBlow";

		// Token: 0x0400A78A RID: 42890
		private FinalBlowEffect.State state;

		// Token: 0x0400A78B RID: 42891
		private float timer;

		// Token: 0x0400A78C RID: 42892
		private const float waitTime = 1f;

		// Token: 0x0400A78D RID: 42893
		private Action<FinalBlowEffect.State> onStateChanged;

		// Token: 0x02000E8F RID: 3727
		public enum State
		{
			// Token: 0x0400A78F RID: 42895
			None,
			// Token: 0x0400A790 RID: 42896
			Load,
			// Token: 0x0400A791 RID: 42897
			In,
			// Token: 0x0400A792 RID: 42898
			Loop,
			// Token: 0x0400A793 RID: 42899
			Out,
			// Token: 0x0400A794 RID: 42900
			Finish
		}
	}
}
