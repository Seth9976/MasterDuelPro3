using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YgomGame.Scenario
{
	// Token: 0x020009B1 RID: 2481
	public abstract class ScenarioBehaviour : IScenarioBehaviour
	{
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06004858 RID: 18520 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004859 RID: 18521 RVA: 0x0000216D File Offset: 0x0000036D
		public ScenarioBehaviour.Step step
		{
			get
			{
				return ScenarioBehaviour.Step.None;
			}
			protected set
			{
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600485A RID: 18522 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600485B RID: 18523 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isFinish
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x0600485C RID: 18524 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600485D RID: 18525 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<ScenarioBehaviour, ScenarioBehaviour.Step> onChangeStepEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x0600485E RID: 18526 RVA: 0x00002739 File Offset: 0x00000939
		public ScenarioBehaviour(object commandData)
		{
		}

		// Token: 0x0600485F RID: 18527 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetScenarioWork(ScenarioWork work)
		{
		}

		// Token: 0x06004860 RID: 18528 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetStep(ScenarioBehaviour.Step s)
		{
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool Update()
		{
			return false;
		}

		// Token: 0x06004862 RID: 18530 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool OnResult()
		{
			return false;
		}

		// Token: 0x06004863 RID: 18531 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isError()
		{
			return false;
		}

		// Token: 0x06004864 RID: 18532 RVA: 0x0000216D File Offset: 0x0000036D
		public void Abort()
		{
		}

		// Token: 0x06004865 RID: 18533 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ProgressControllerCheck()
		{
		}

		// Token: 0x06004866 RID: 18534 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void ProgressInit()
		{
		}

		// Token: 0x06004867 RID: 18535 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void ProgressAction()
		{
		}

		// Token: 0x06004868 RID: 18536 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void ProgressWait()
		{
		}

		// Token: 0x06004869 RID: 18537 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void ProgressWaitInput()
		{
		}

		// Token: 0x0600486A RID: 18538 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void ProgressFinish()
		{
		}

		// Token: 0x0600486B RID: 18539 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnAbort()
		{
		}

		// Token: 0x0600486C RID: 18540 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnChangedStep(ScenarioBehaviour.Step step)
		{
		}

		// Token: 0x0600486D RID: 18541 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual bool IsOverrideBehaviour(ScenarioBehaviour target)
		{
			return false;
		}

		// Token: 0x0600486E RID: 18542 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnPointerClick()
		{
		}

		// Token: 0x04008684 RID: 34436
		public readonly string commandKey;

		// Token: 0x04008685 RID: 34437
		public readonly ScenarioDef.BehaviourAsyncMode asyncMode;

		// Token: 0x04008686 RID: 34438
		protected readonly Dictionary<string, object> m_Args;

		// Token: 0x04008687 RID: 34439
		protected ScenarioBehaviour.Step m_Step;

		// Token: 0x04008688 RID: 34440
		private bool m_IsEnd;

		// Token: 0x04008689 RID: 34441
		protected ScenarioWork work;

		// Token: 0x020009B2 RID: 2482
		public enum Step
		{
			// Token: 0x0400868B RID: 34443
			None,
			// Token: 0x0400868C RID: 34444
			ControllerCheck,
			// Token: 0x0400868D RID: 34445
			Init,
			// Token: 0x0400868E RID: 34446
			Action,
			// Token: 0x0400868F RID: 34447
			Wait,
			// Token: 0x04008690 RID: 34448
			WaitInput,
			// Token: 0x04008691 RID: 34449
			Finish,
			// Token: 0x04008692 RID: 34450
			Error
		}
	}
}
