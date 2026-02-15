using System;
using YgomSystem.ElementSystem;

namespace YgomGame.Scenario
{
	// Token: 0x020009A4 RID: 2468
	public class ScenarioActorContainer : ScenarioContainerBase
	{
		// Token: 0x060047E7 RID: 18407 RVA: 0x000F4916 File Offset: 0x000F2B16
		public ScenarioActorContainer(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x0400863D RID: 34365
		private readonly string k_ELabelUnitGroup;

		// Token: 0x0400863E RID: 34366
		private readonly string k_ELabelCardGroup;

		// Token: 0x0400863F RID: 34367
		public readonly ScenarioUnitContainer unitContainer;

		// Token: 0x04008640 RID: 34368
		public readonly ScenarioCardContainer cardContainer;
	}
}
