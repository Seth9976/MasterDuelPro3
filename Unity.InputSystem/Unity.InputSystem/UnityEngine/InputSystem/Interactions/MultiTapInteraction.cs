using System;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x02000228 RID: 552
	public class MultiTapInteraction : IInputInteraction<float>, IInputInteraction
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0005CA0A File Offset: 0x0005AC0A
		private float tapTimeOrDefault
		{
			get
			{
				if ((double)this.tapTime <= 0.0)
				{
					return InputSystem.settings.defaultTapTime;
				}
				return this.tapTime;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0005CA2F File Offset: 0x0005AC2F
		internal float tapDelayOrDefault
		{
			get
			{
				if ((double)this.tapDelay <= 0.0)
				{
					return InputSystem.settings.multiTapDelayTime;
				}
				return this.tapDelay;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0005CA54 File Offset: 0x0005AC54
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

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0005CA6F File Offset: 0x0005AC6F
		private float releasePointOrDefault
		{
			get
			{
				return this.pressPointOrDefault * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
			}
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0005CA80 File Offset: 0x0005AC80
		public void Process(ref InputInteractionContext context)
		{
			if (context.timerHasExpired)
			{
				context.Canceled();
				return;
			}
			switch (this.m_CurrentTapPhase)
			{
			case MultiTapInteraction.TapPhase.None:
				if (context.ControlIsActuated(this.pressPointOrDefault))
				{
					this.m_CurrentTapPhase = MultiTapInteraction.TapPhase.WaitingForNextRelease;
					this.m_CurrentTapStartTime = context.time;
					context.Started();
					float maxTapTime = this.tapTimeOrDefault;
					float maxDelayInBetween = this.tapDelayOrDefault;
					context.SetTimeout(maxTapTime);
					context.SetTotalTimeoutCompletionTime(maxTapTime * (float)this.tapCount + (float)(this.tapCount - 1) * maxDelayInBetween);
					return;
				}
				break;
			case MultiTapInteraction.TapPhase.WaitingForNextRelease:
				if (!context.ControlIsActuated(this.releasePointOrDefault))
				{
					if (context.time - this.m_CurrentTapStartTime > (double)this.tapTimeOrDefault)
					{
						context.Canceled();
						return;
					}
					this.m_CurrentTapCount++;
					if (this.m_CurrentTapCount >= this.tapCount)
					{
						context.Performed();
						return;
					}
					this.m_CurrentTapPhase = MultiTapInteraction.TapPhase.WaitingForNextPress;
					this.m_LastTapReleaseTime = context.time;
					context.SetTimeout(this.tapDelayOrDefault);
					return;
				}
				break;
			case MultiTapInteraction.TapPhase.WaitingForNextPress:
				if (context.ControlIsActuated(this.pressPointOrDefault))
				{
					if (context.time - this.m_LastTapReleaseTime <= (double)this.tapDelayOrDefault)
					{
						this.m_CurrentTapPhase = MultiTapInteraction.TapPhase.WaitingForNextRelease;
						this.m_CurrentTapStartTime = context.time;
						context.SetTimeout(this.tapTimeOrDefault);
						return;
					}
					context.Canceled();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0005CBCC File Offset: 0x0005ADCC
		public void Reset()
		{
			this.m_CurrentTapPhase = MultiTapInteraction.TapPhase.None;
			this.m_CurrentTapCount = 0;
			this.m_CurrentTapStartTime = 0.0;
			this.m_LastTapReleaseTime = 0.0;
		}

		// Token: 0x04000C14 RID: 3092
		[Tooltip("The maximum time (in seconds) allowed to elapse between pressing and releasing a control for it to register as a tap.")]
		public float tapTime;

		// Token: 0x04000C15 RID: 3093
		[Tooltip("The maximum delay (in seconds) allowed between each tap. If this time is exceeded, the multi-tap is canceled.")]
		public float tapDelay;

		// Token: 0x04000C16 RID: 3094
		[Tooltip("How many taps need to be performed in succession. Two means double-tap, three means triple-tap, and so on.")]
		public int tapCount = 2;

		// Token: 0x04000C17 RID: 3095
		public float pressPoint;

		// Token: 0x04000C18 RID: 3096
		private MultiTapInteraction.TapPhase m_CurrentTapPhase;

		// Token: 0x04000C19 RID: 3097
		private int m_CurrentTapCount;

		// Token: 0x04000C1A RID: 3098
		private double m_CurrentTapStartTime;

		// Token: 0x04000C1B RID: 3099
		private double m_LastTapReleaseTime;

		// Token: 0x02000229 RID: 553
		private enum TapPhase
		{
			// Token: 0x04000C1D RID: 3101
			None,
			// Token: 0x04000C1E RID: 3102
			WaitingForNextRelease,
			// Token: 0x04000C1F RID: 3103
			WaitingForNextPress
		}
	}
}
