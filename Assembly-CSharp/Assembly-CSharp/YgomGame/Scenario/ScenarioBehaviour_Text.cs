using System;
using System.Collections.Generic;
using TMPro;

namespace YgomGame.Scenario
{
	// Token: 0x020009C6 RID: 2502
	public class ScenarioBehaviour_Text : ScenarioBehaviour, IScenarioLogTextBehavior, IScenarioLogBehavior, IScenarioPreGenerateTextBehaviour
	{
		// Token: 0x060048BB RID: 18619 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehaviour_Text(object commandData)
			: base(null)
		{
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPreGenerateText()
		{
			return null;
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressWaitInput()
		{
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressFinish()
		{
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPointerClick()
		{
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetLogText()
		{
			return null;
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetAutoFinihSec()
		{
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangeSuspend(bool isSuspend)
		{
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangeAuto(bool isAuto)
		{
		}

		// Token: 0x040086BD RID: 34493
		public float waitLineSec;

		// Token: 0x040086BE RID: 34494
		private List<TextMeshProUGUI> m_LineTextMeshs;

		// Token: 0x040086BF RID: 34495
		private int m_PlayPos;

		// Token: 0x040086C0 RID: 34496
		private float m_RemineWaitLineSec;

		// Token: 0x040086C1 RID: 34497
		private bool m_PlayTrigger;

		// Token: 0x040086C2 RID: 34498
		private float m_AutoFinishSec;

		// Token: 0x040086C3 RID: 34499
		private float m_TextFilledArrowWaitSec;

		// Token: 0x040086C4 RID: 34500
		private string m_Text;
	}
}
