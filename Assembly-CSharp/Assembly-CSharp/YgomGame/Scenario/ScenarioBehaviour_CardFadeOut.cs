using System;
using System.Collections.Generic;

namespace YgomGame.Scenario
{
	// Token: 0x020009B8 RID: 2488
	public class ScenarioBehaviour_CardFadeOut : ScenarioBehaviour, IScenarioCardActorBehaviour
	{
		// Token: 0x06004881 RID: 18561 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehaviour_CardFadeOut(object commandData)
			: base(null)
		{
		}

		// Token: 0x06004882 RID: 18562 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<int> GetBehaveCardSlots()
		{
			return null;
		}

		// Token: 0x06004883 RID: 18563 RVA: 0x000029CC File Offset: 0x00000BCC
		public ScenarioCardContainer.Operations GetBehaveCardOperations()
		{
			return (ScenarioCardContainer.Operations)0;
		}

		// Token: 0x06004884 RID: 18564 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x06004885 RID: 18565 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x06004886 RID: 18566 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsOverrideBehaviour(ScenarioBehaviour target)
		{
			return false;
		}

		// Token: 0x040086A3 RID: 34467
		private ScenarioCardActor m_CardActor;

		// Token: 0x040086A4 RID: 34468
		private bool m_PlayTrigger;

		// Token: 0x040086A5 RID: 34469
		private int m_Mrk;

		// Token: 0x040086A6 RID: 34470
		private int[] m_ReserveSlots;
	}
}
