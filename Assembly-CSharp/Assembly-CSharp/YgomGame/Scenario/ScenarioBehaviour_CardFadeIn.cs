using System;
using System.Collections.Generic;

namespace YgomGame.Scenario
{
	// Token: 0x020009B6 RID: 2486
	public class ScenarioBehaviour_CardFadeIn : ScenarioBehaviour, IScenarioCardActorBehaviour, IScenarioLoadGroupHandleBehaviour
	{
		// Token: 0x06004878 RID: 18552 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehaviour_CardFadeIn(object commandData)
			: base(null)
		{
		}

		// Token: 0x06004879 RID: 18553 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<int> GetBehaveCardSlots()
		{
			return null;
		}

		// Token: 0x0600487A RID: 18554 RVA: 0x000029CC File Offset: 0x00000BCC
		public ScenarioCardContainer.Operations GetBehaveCardOperations()
		{
			return (ScenarioCardContainer.Operations)0;
		}

		// Token: 0x0600487B RID: 18555 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x0600487C RID: 18556 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x0600487D RID: 18557 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsOverrideBehaviour(ScenarioBehaviour target)
		{
			return false;
		}

		// Token: 0x0600487E RID: 18558 RVA: 0x0000216D File Offset: 0x0000036D
		public void CollectLoadPath(List<ValueTuple<string, Type>> res)
		{
		}

		// Token: 0x0600487F RID: 18559 RVA: 0x0000216D File Offset: 0x0000036D
		public void CollectLoadMrk(List<int> res)
		{
		}

		// Token: 0x0400869C RID: 34460
		private List<ScenarioCardActor> m_CardActorLoads;

		// Token: 0x0400869D RID: 34461
		private Queue<ScenarioBehaviour_CardFadeIn.PlayData> m_CardActorQueue;

		// Token: 0x0400869E RID: 34462
		private ScenarioBehaviour_CardFadeIn.PlayData m_PlayingData;

		// Token: 0x0400869F RID: 34463
		private List<int> m_ReserveSlots;

		// Token: 0x040086A0 RID: 34464
		private List<int> m_Mrks;

		// Token: 0x020009B7 RID: 2487
		private class PlayData
		{
			// Token: 0x06004880 RID: 18560 RVA: 0x00002739 File Offset: 0x00000939
			public PlayData(ScenarioCardActor cardActor, string seLabel)
			{
			}

			// Token: 0x040086A1 RID: 34465
			public readonly ScenarioCardActor cardActor;

			// Token: 0x040086A2 RID: 34466
			public readonly string seLabel;
		}
	}
}
