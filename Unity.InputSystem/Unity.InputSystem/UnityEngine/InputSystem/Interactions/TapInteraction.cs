using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x0200022D RID: 557
	[DisplayName("Tap")]
	public class TapInteraction : IInputInteraction
	{
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0005CE9C File Offset: 0x0005B09C
		private float durationOrDefault
		{
			get
			{
				if ((double)this.duration <= 0.0)
				{
					return InputSystem.settings.defaultTapTime;
				}
				return this.duration;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0005CEC1 File Offset: 0x0005B0C1
		private float pressPointOrDefault
		{
			get
			{
				if (this.pressPoint <= 0f)
				{
					return ButtonControl.s_GlobalDefaultButtonPressPoint;
				}
				return this.pressPoint;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0005CEDC File Offset: 0x0005B0DC
		private float releasePointOrDefault
		{
			get
			{
				return this.pressPointOrDefault * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
			}
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0005CEEC File Offset: 0x0005B0EC
		public void Process(ref InputInteractionContext context)
		{
			if (context.timerHasExpired)
			{
				context.Canceled();
				return;
			}
			if (context.isWaiting && context.ControlIsActuated(this.pressPointOrDefault))
			{
				this.m_TapStartTime = context.time;
				context.Started();
				context.SetTimeout(this.durationOrDefault + 1E-05f);
				return;
			}
			if (context.isStarted && !context.ControlIsActuated(this.releasePointOrDefault))
			{
				if (context.time - this.m_TapStartTime <= (double)this.durationOrDefault)
				{
					context.Performed();
					return;
				}
				context.Canceled();
			}
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0005CF7C File Offset: 0x0005B17C
		public void Reset()
		{
			this.m_TapStartTime = 0.0;
		}

		// Token: 0x04000C2A RID: 3114
		public float duration;

		// Token: 0x04000C2B RID: 3115
		public float pressPoint;

		// Token: 0x04000C2C RID: 3116
		private double m_TapStartTime;
	}
}
