using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x0200022A RID: 554
	[DisplayName("Press")]
	public class PressInteraction : IInputInteraction
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0005CC09 File Offset: 0x0005AE09
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

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0005CC24 File Offset: 0x0005AE24
		private float releasePointOrDefault
		{
			get
			{
				return this.pressPointOrDefault * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
			}
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0005CC34 File Offset: 0x0005AE34
		public void Process(ref InputInteractionContext context)
		{
			float actuation = context.ComputeMagnitude();
			switch (this.behavior)
			{
			case PressBehavior.PressOnly:
				if (this.m_WaitingForRelease)
				{
					if (actuation <= this.releasePointOrDefault)
					{
						this.m_WaitingForRelease = false;
						if (Mathf.Approximately(0f, actuation))
						{
							context.Canceled();
							return;
						}
						context.Started();
						return;
					}
				}
				else
				{
					if (actuation >= this.pressPointOrDefault)
					{
						this.m_WaitingForRelease = true;
						context.PerformedAndStayPerformed();
						return;
					}
					if (actuation > 0f && !context.isStarted)
					{
						context.Started();
						return;
					}
					if (Mathf.Approximately(0f, actuation) && context.isStarted)
					{
						context.Canceled();
						return;
					}
				}
				break;
			case PressBehavior.ReleaseOnly:
				if (this.m_WaitingForRelease)
				{
					if (actuation <= this.releasePointOrDefault)
					{
						this.m_WaitingForRelease = false;
						context.Performed();
						context.Canceled();
						return;
					}
				}
				else if (actuation >= this.pressPointOrDefault)
				{
					this.m_WaitingForRelease = true;
					if (!context.isStarted)
					{
						context.Started();
						return;
					}
				}
				else
				{
					bool started = context.isStarted;
					if (actuation > 0f && !started)
					{
						context.Started();
						return;
					}
					if (Mathf.Approximately(0f, actuation) && started)
					{
						context.Canceled();
						return;
					}
				}
				break;
			case PressBehavior.PressAndRelease:
				if (this.m_WaitingForRelease)
				{
					if (actuation <= this.releasePointOrDefault)
					{
						this.m_WaitingForRelease = false;
						context.Performed();
						if (Mathf.Approximately(0f, actuation))
						{
							context.Canceled();
							return;
						}
					}
				}
				else
				{
					if (actuation >= this.pressPointOrDefault)
					{
						this.m_WaitingForRelease = true;
						context.PerformedAndStayPerformed();
						return;
					}
					bool started2 = context.isStarted;
					if (actuation > 0f && !started2)
					{
						context.Started();
						return;
					}
					if (Mathf.Approximately(0f, actuation) && started2)
					{
						context.Canceled();
					}
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0005CDD8 File Offset: 0x0005AFD8
		public void Reset()
		{
			this.m_WaitingForRelease = false;
		}

		// Token: 0x04000C20 RID: 3104
		[Tooltip("The amount of actuation a control requires before being considered pressed. If not set, default to 'Default Press Point' in the global input settings.")]
		public float pressPoint;

		// Token: 0x04000C21 RID: 3105
		[Tooltip("Determines how button presses trigger the action. By default (PressOnly), the action is performed on press. With ReleaseOnly, the action is performed on release. With PressAndRelease, the action is performed on press and release.")]
		public PressBehavior behavior;

		// Token: 0x04000C22 RID: 3106
		private bool m_WaitingForRelease;
	}
}
