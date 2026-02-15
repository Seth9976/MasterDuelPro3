using System;
using System.Collections.Generic;

namespace YgomGame.Scenario
{
	// Token: 0x0200099C RID: 2460
	public interface IScenarioCardActorBehaviour
	{
		// Token: 0x060047DD RID: 18397
		IReadOnlyList<int> GetBehaveCardSlots();

		// Token: 0x060047DE RID: 18398
		ScenarioCardContainer.Operations GetBehaveCardOperations();
	}
}
