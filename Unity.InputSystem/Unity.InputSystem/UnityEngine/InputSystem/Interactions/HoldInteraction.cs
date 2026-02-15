using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x02000227 RID: 551
	[DisplayName("Hold")]
	public class HoldInteraction : IInputInteraction
	{
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0005C90B File Offset: 0x0005AB0B
		private float durationOrDefault
		{
			get
			{
				if ((double)this.duration <= 0.0)
				{
					return InputSystem.settings.defaultHoldTime;
				}
				return this.duration;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0005C930 File Offset: 0x0005AB30
		private float pressPointOrDefault
		{
			get
			{
				if ((double)this.pressPoint <= 0.0)
				{
					return ButtonControl.s_GlobalDefaultButtonPressPoint;
				}
				return this.pressPoint;
			}
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0005C950 File Offset: 0x0005AB50
		public void Process(ref InputInteractionContext context)
		{
			if (context.timerHasExpired)
			{
				context.PerformedAndStayPerformed();
				return;
			}
			switch (context.phase)
			{
			case InputActionPhase.Waiting:
				if (context.ControlIsActuated(this.pressPointOrDefault))
				{
					this.m_TimePressed = context.time;
					context.Started();
					context.SetTimeout(this.durationOrDefault);
					return;
				}
				break;
			case InputActionPhase.Started:
				if (context.time - this.m_TimePressed >= (double)this.durationOrDefault)
				{
					context.PerformedAndStayPerformed();
				}
				if (!context.ControlIsActuated(0f))
				{
					context.Canceled();
					return;
				}
				break;
			case InputActionPhase.Performed:
				if (!context.ControlIsActuated(this.pressPointOrDefault))
				{
					context.Canceled();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0005C9F9 File Offset: 0x0005ABF9
		public void Reset()
		{
			this.m_TimePressed = 0.0;
		}

		// Token: 0x04000C11 RID: 3089
		public float duration;

		// Token: 0x04000C12 RID: 3090
		public float pressPoint;

		// Token: 0x04000C13 RID: 3091
		private double m_TimePressed;
	}
}
