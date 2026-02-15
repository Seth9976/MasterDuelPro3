using System;

namespace YgomGame.Scenario
{
	// Token: 0x020009BB RID: 2491
	public class ScenarioBehaviour_LoadGroup_Begin : ScenarioBehaviour, IScenarioFadeInTransitionBehaviour
	{
		// Token: 0x06004895 RID: 18581 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsFadeInTransitionCompleted()
		{
			return false;
		}

		// Token: 0x06004896 RID: 18582 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehaviour_LoadGroup_Begin(object commandData)
			: base(null)
		{
		}

		// Token: 0x06004897 RID: 18583 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x040086B0 RID: 34480
		private int m_InnerStep;
	}
}
