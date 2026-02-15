using System;
using System.Collections.Generic;

namespace YgomGame.Scenario
{
	// Token: 0x020009B9 RID: 2489
	public class ScenarioBehaviour_CardFadeOutAll : ScenarioBehaviour, IScenarioCardActorBehaviour
	{
		// Token: 0x06004887 RID: 18567 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehaviour_CardFadeOutAll(object commandData)
			: base(null)
		{
		}

		// Token: 0x06004888 RID: 18568 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<int> GetBehaveCardSlots()
		{
			return null;
		}

		// Token: 0x06004889 RID: 18569 RVA: 0x000029CC File Offset: 0x00000BCC
		public ScenarioCardContainer.Operations GetBehaveCardOperations()
		{
			return (ScenarioCardContainer.Operations)0;
		}

		// Token: 0x0600488A RID: 18570 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x0600488B RID: 18571 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x0600488C RID: 18572 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsOverrideBehaviour(ScenarioBehaviour target)
		{
			return false;
		}

		// Token: 0x040086A7 RID: 34471
		private List<int> m_CardSlots;

		// Token: 0x040086A8 RID: 34472
		private ScenarioCardActor[] m_CardActors;

		// Token: 0x040086A9 RID: 34473
		private bool m_PlayTrigger;
	}
}
