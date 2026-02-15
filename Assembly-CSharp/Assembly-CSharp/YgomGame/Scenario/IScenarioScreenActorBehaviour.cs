using System;

namespace YgomGame.Scenario
{
	// Token: 0x020009A2 RID: 2466
	public interface IScenarioScreenActorBehaviour : IScenarioBehaviour
	{
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060047E4 RID: 18404
		bool isOverrideBehaveScreen { get; }

		// Token: 0x060047E5 RID: 18405
		ScenarioScreenContainer.Targets GetBehaveScreenTargets();

		// Token: 0x060047E6 RID: 18406
		ScenarioScreenContainer.Operations GetBehaveScreenOperations();
	}
}
