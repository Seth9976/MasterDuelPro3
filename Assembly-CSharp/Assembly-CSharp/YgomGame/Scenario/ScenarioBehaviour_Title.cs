using System;

namespace YgomGame.Scenario
{
	// Token: 0x020009C7 RID: 2503
	public class ScenarioBehaviour_Title : ScenarioBehaviour, IScenarioFadeInTransitionBehaviour, IScenarioPreGenerateTextBehaviour, IScenarioLogBehavior
	{
		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060048C6 RID: 18630 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsFadeInTransitionCompleted()
		{
			return false;
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehaviour_Title(object commandData)
			: base(null)
		{
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPreGenerateText()
		{
			return null;
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x040086C5 RID: 34501
		private string m_TitleText;

		// Token: 0x040086C6 RID: 34502
		private ScenarioBGActor m_BGActor;

		// Token: 0x040086C7 RID: 34503
		private float m_CurrentSec;

		// Token: 0x040086C8 RID: 34504
		private int m_InnerStep;
	}
}
