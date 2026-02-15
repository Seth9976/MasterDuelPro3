using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomGame.Scenario
{
	// Token: 0x020009C1 RID: 2497
	public class ScenarioBehaviour_ScreenFade : ScenarioBehaviour, IScenarioScreenActorBehaviour, IScenarioBehaviour
	{
		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060048A8 RID: 18600 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isOverrideBehaveScreen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehaviour_ScreenFade(object commandData)
			: base(null)
		{
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x000029CC File Offset: 0x00000BCC
		public ScenarioScreenContainer.Targets GetBehaveScreenTargets()
		{
			return ScenarioScreenContainer.Targets.None;
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x000029CC File Offset: 0x00000BCC
		public ScenarioScreenContainer.Operations GetBehaveScreenOperations()
		{
			return ScenarioScreenContainer.Operations.None;
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x060048AD RID: 18605 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressFinish()
		{
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsOverrideBehaviour(ScenarioBehaviour target)
		{
			return false;
		}

		// Token: 0x040086B5 RID: 34485
		private ScenarioScreenContainer.Targets m_ScreenTarget;

		// Token: 0x040086B6 RID: 34486
		private Graphic m_FadeTarget;

		// Token: 0x040086B7 RID: 34487
		private Color m_FromColor;

		// Token: 0x040086B8 RID: 34488
		private Color m_ToColor;

		// Token: 0x040086B9 RID: 34489
		private float m_FadeTime;

		// Token: 0x040086BA RID: 34490
		private float m_CrntTime;
	}
}
