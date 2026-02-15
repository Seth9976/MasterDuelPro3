using System;
using System.Collections.Generic;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace YgomGame.Scenario
{
	// Token: 0x020009E0 RID: 2528
	public class ScenarioScreenContainer : ScenarioContainerBase
	{
		// Token: 0x0600498B RID: 18827 RVA: 0x000F4916 File Offset: 0x000F2B16
		public ScenarioScreenContainer(ElementObjectManager eom, Image screenTextUnder, Image screenTextOver)
			: base(null)
		{
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x0000216A File Offset: 0x0000036A
		public Image GetScreen(ScenarioScreenContainer.Targets target)
		{
			return null;
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsReadyControllBehaviour(IScenarioScreenActorBehaviour controllBehaviour)
		{
			return false;
		}

		// Token: 0x0600498E RID: 18830 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool AssignBehaviour(IScenarioScreenActorBehaviour controllBehaviour)
		{
			return false;
		}

		// Token: 0x0600498F RID: 18831 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool RemoveBehaviour(IScenarioScreenActorBehaviour controllBehaviour)
		{
			return false;
		}

		// Token: 0x04008767 RID: 34663
		private readonly List<IScenarioScreenActorBehaviour> m_RegistedBehaviours;

		// Token: 0x04008768 RID: 34664
		private readonly List<IScenarioScreenActorBehaviour> m_ReserveRemoveBehaviours;

		// Token: 0x04008769 RID: 34665
		private readonly Image m_ScreenTextUnder;

		// Token: 0x0400876A RID: 34666
		private readonly Image m_ScreenTextOver;

		// Token: 0x020009E1 RID: 2529
		public enum Targets
		{
			// Token: 0x0400876C RID: 34668
			None,
			// Token: 0x0400876D RID: 34669
			TextUnder,
			// Token: 0x0400876E RID: 34670
			TextOver
		}

		// Token: 0x020009E2 RID: 2530
		public enum Operations
		{
			// Token: 0x04008770 RID: 34672
			None,
			// Token: 0x04008771 RID: 34673
			Color
		}
	}
}
