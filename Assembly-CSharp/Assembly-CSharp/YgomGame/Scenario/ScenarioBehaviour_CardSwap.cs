using System;
using System.Collections.Generic;

namespace YgomGame.Scenario
{
	// Token: 0x020009BA RID: 2490
	public class ScenarioBehaviour_CardSwap : ScenarioBehaviour, IScenarioCardActorBehaviour, IScenarioLoadGroupHandleBehaviour
	{
		// Token: 0x0600488D RID: 18573 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehaviour_CardSwap(object commandData)
			: base(null)
		{
		}

		// Token: 0x0600488E RID: 18574 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<int> GetBehaveCardSlots()
		{
			return null;
		}

		// Token: 0x0600488F RID: 18575 RVA: 0x000029CC File Offset: 0x00000BCC
		public ScenarioCardContainer.Operations GetBehaveCardOperations()
		{
			return (ScenarioCardContainer.Operations)0;
		}

		// Token: 0x06004890 RID: 18576 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x06004892 RID: 18578 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsOverrideBehaviour(ScenarioBehaviour target)
		{
			return false;
		}

		// Token: 0x06004893 RID: 18579 RVA: 0x0000216D File Offset: 0x0000036D
		public void CollectLoadPath(List<ValueTuple<string, Type>> res)
		{
		}

		// Token: 0x06004894 RID: 18580 RVA: 0x0000216D File Offset: 0x0000036D
		public void CollectLoadMrk(List<int> res)
		{
		}

		// Token: 0x040086AA RID: 34474
		private ScenarioCardActor m_CardActor;

		// Token: 0x040086AB RID: 34475
		private int m_OldMrk;

		// Token: 0x040086AC RID: 34476
		private int m_NewMrk;

		// Token: 0x040086AD RID: 34477
		private int[] m_ReserveSlots;

		// Token: 0x040086AE RID: 34478
		private bool m_PlayTrigger;

		// Token: 0x040086AF RID: 34479
		private bool m_PreLoadComplete;
	}
}
