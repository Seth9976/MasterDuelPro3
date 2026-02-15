using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x0200022C RID: 556
	[DisplayName("Long Tap")]
	public class SlowTapInteraction : IInputInteraction
	{
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0005CDE1 File Offset: 0x0005AFE1
		private float durationOrDefault
		{
			get
			{
				if (this.duration <= 0f)
				{
					return InputSystem.settings.defaultSlowTapTime;
				}
				return this.duration;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x0005CE01 File Offset: 0x0005B001
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

		// Token: 0x06001453 RID: 5203 RVA: 0x0005CE1C File Offset: 0x0005B01C
		public void Process(ref InputInteractionContext context)
		{
			if (context.isWaiting && context.ControlIsActuated(this.pressPointOrDefault))
			{
				this.m_SlowTapStartTime = context.time;
				context.Started();
				return;
			}
			if (context.isStarted && !context.ControlIsActuated(this.pressPointOrDefault))
			{
				if (context.time - this.m_SlowTapStartTime >= (double)this.durationOrDefault)
				{
					context.Performed();
					return;
				}
				context.Canceled();
			}
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0005CE8B File Offset: 0x0005B08B
		public void Reset()
		{
			this.m_SlowTapStartTime = 0.0;
		}

		// Token: 0x04000C27 RID: 3111
		public float duration;

		// Token: 0x04000C28 RID: 3112
		public float pressPoint;

		// Token: 0x04000C29 RID: 3113
		private double m_SlowTapStartTime;
	}
}
