using System;
using YgomSystem.ElementSystem;

namespace YgomGame.Scenario
{
	// Token: 0x020009D9 RID: 2521
	public class ScenarioObjectContainer : ScenarioContainerBase
	{
		// Token: 0x06004960 RID: 18784 RVA: 0x000F4916 File Offset: 0x000F2B16
		public ScenarioObjectContainer(ElementObjectManager eom3D, ElementObjectManager eomUI)
			: base(null)
		{
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ScenarioWork work)
		{
		}

		// Token: 0x06004962 RID: 18786 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnStackRemove()
		{
		}

		// Token: 0x06004963 RID: 18787 RVA: 0x0000216A File Offset: 0x0000036A
		public ScenarioContainerBase GetShakeTarget(int targetNo)
		{
			return null;
		}

		// Token: 0x04008732 RID: 34610
		public readonly ScenarioObjectContainer3D container3D;

		// Token: 0x04008733 RID: 34611
		public readonly ScenarioObjectContainerUI containerUI;
	}
}
