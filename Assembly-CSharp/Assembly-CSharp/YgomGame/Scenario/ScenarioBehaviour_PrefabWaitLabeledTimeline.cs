using System;
using YgomSystem.Timeline;

namespace YgomGame.Scenario
{
	// Token: 0x020009C0 RID: 2496
	public class ScenarioBehaviour_PrefabWaitLabeledTimeline : ScenarioBehaviour
	{
		// Token: 0x060048A4 RID: 18596 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehaviour_PrefabWaitLabeledTimeline(object commandData)
			: base(null)
		{
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x060048A7 RID: 18599 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPointerClick()
		{
		}

		// Token: 0x040086B3 RID: 34483
		private LabeledPlayableController m_Target;

		// Token: 0x040086B4 RID: 34484
		private bool m_IsEnableSkip;
	}
}
