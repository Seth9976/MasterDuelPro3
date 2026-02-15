using System;

namespace YgomGame.Scenario
{
	// Token: 0x020009BC RID: 2492
	public class ScenarioBehaviour_LoadGroup_End : ScenarioBehaviour, IScenarioFadeInTransitionBehaviour
	{
		// Token: 0x06004898 RID: 18584 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsFadeInTransitionCompleted()
		{
			return false;
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehaviour_LoadGroup_End(object commandData)
			: base(null)
		{
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}
	}
}
