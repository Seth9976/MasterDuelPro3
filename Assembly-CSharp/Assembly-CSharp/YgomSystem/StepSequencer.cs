using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomSystem
{
	// Token: 0x020004DB RID: 1243
	public class StepSequencer
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060027B8 RID: 10168 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isRunning
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isStepChanged
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060027BA RID: 10170 RVA: 0x000029CC File Offset: 0x00000BCC
		public int currentStep
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x000029CC File Offset: 0x00000BCC
		public int result
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x0000216A File Offset: 0x0000036A
		private StepSequencer.StepEntry registerStep(int step, StepSequencer.ProcessType type, object proccess)
		{
			return null;
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepCoroutineProcess(StepSequencer.StepEntry entry)
		{
			return null;
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x0000216D File Offset: 0x0000036D
		private void runStepCoroutine(StepSequencer.StepEntry entry)
		{
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x0000216D File Offset: 0x0000036D
		private void stopStepCoroutine()
		{
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x0000216D File Offset: 0x0000036D
		public void Destroy()
		{
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegisterStep(int stepValue, Action<StepSequencer> stepMethod)
		{
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegisterStep<T>(T stepValue, Action<StepSequencer> stepMethod) where T : Enum
		{
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegisterStep(int stepValue, Func<StepSequencer, IEnumerator> stepGenerator)
		{
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegisterStep<T>(T stepValue, Func<StepSequencer, IEnumerator> stepGenerator) where T : Enum
		{
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsStepRegistered<T>(T step) where T : Enum
		{
			return false;
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartSequence(int startStep)
		{
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartSequence<T>(T startStep) where T : Enum
		{
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x0000216D File Offset: 0x0000036D
		public void Reset()
		{
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool UpdateSequence()
		{
			return false;
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetStep(int step)
		{
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetStep<T>(T stepValue) where T : Enum
		{
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetEnd(int result = 0)
		{
		}

		// Token: 0x0400287B RID: 10363
		private Dictionary<int, StepSequencer.StepEntry> m_steps;

		// Token: 0x0400287C RID: 10364
		private int m_currentStep;

		// Token: 0x0400287D RID: 10365
		private int m_prevStep;

		// Token: 0x0400287E RID: 10366
		private bool m_stepChanged;

		// Token: 0x0400287F RID: 10367
		private IEnumerator m_stepCoroutine;

		// Token: 0x04002880 RID: 10368
		private const int INVALID_STEP = -1;

		// Token: 0x04002881 RID: 10369
		private StepSequencer.Status m_status;

		// Token: 0x04002882 RID: 10370
		private int m_result;

		// Token: 0x020004DC RID: 1244
		private enum Status
		{
			// Token: 0x04002884 RID: 10372
			Idle,
			// Token: 0x04002885 RID: 10373
			Running,
			// Token: 0x04002886 RID: 10374
			End
		}

		// Token: 0x020004DD RID: 1245
		private enum ProcessType
		{
			// Token: 0x04002888 RID: 10376
			None,
			// Token: 0x04002889 RID: 10377
			Method,
			// Token: 0x0400288A RID: 10378
			Coroutine
		}

		// Token: 0x020004DE RID: 1246
		private class StepEntry
		{
			// Token: 0x0400288B RID: 10379
			public int step;

			// Token: 0x0400288C RID: 10380
			public StepSequencer.ProcessType type;

			// Token: 0x0400288D RID: 10381
			public object proccess;

			// Token: 0x0400288E RID: 10382
			public IEnumerator enumerator;
		}
	}
}
