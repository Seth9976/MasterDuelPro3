using System;

namespace YgomGame.Scenario
{
	// Token: 0x020009C9 RID: 2505
	public class ScenarioBehaviour_WaitClick : ScenarioBehaviour
	{
		// Token: 0x060048CE RID: 18638 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehaviour_WaitClick(object commandData)
			: base(null)
		{
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressWaitInput()
		{
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressFinish()
		{
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPointerClick()
		{
		}

		// Token: 0x060048D3 RID: 18643 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetAutoFinihSec()
		{
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangeSuspend(bool isSuspend)
		{
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangeAuto(bool isAuto)
		{
		}

		// Token: 0x040086CA RID: 34506
		private float m_AutoFinishSec;
	}
}
