using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000066 RID: 102
	public struct InputInteractionContext
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x000130B8 File Offset: 0x000112B8
		public InputAction action
		{
			get
			{
				return this.m_State.GetActionOrNull(ref this.m_TriggerState);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x000130CB File Offset: 0x000112CB
		public InputControl control
		{
			get
			{
				return this.m_State.GetControl(ref this.m_TriggerState);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000130DE File Offset: 0x000112DE
		public InputActionPhase phase
		{
			get
			{
				return this.m_TriggerState.phase;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x000130EB File Offset: 0x000112EB
		public double time
		{
			get
			{
				return this.m_TriggerState.time;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x000130F8 File Offset: 0x000112F8
		public double startTime
		{
			get
			{
				return this.m_TriggerState.startTime;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00013105 File Offset: 0x00011305
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x00013112 File Offset: 0x00011312
		public bool timerHasExpired
		{
			get
			{
				return (this.m_Flags & InputInteractionContext.Flags.TimerHasExpired) > (InputInteractionContext.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputInteractionContext.Flags.TimerHasExpired;
					return;
				}
				this.m_Flags &= ~InputInteractionContext.Flags.TimerHasExpired;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00013135 File Offset: 0x00011335
		public bool isWaiting
		{
			get
			{
				return this.phase == InputActionPhase.Waiting;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00013140 File Offset: 0x00011340
		public bool isStarted
		{
			get
			{
				return this.phase == InputActionPhase.Started;
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001314B File Offset: 0x0001134B
		public float ComputeMagnitude()
		{
			return this.m_TriggerState.magnitude;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00013158 File Offset: 0x00011358
		public bool ControlIsActuated(float threshold = 0f)
		{
			return InputActionState.IsActuated(ref this.m_TriggerState, threshold);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00013166 File Offset: 0x00011366
		public void Started()
		{
			this.m_TriggerState.startTime = this.time;
			this.m_State.ChangePhaseOfInteraction(InputActionPhase.Started, ref this.m_TriggerState, InputActionPhase.Waiting, InputActionPhase.Waiting, true);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001318E File Offset: 0x0001138E
		public void Performed()
		{
			if (this.m_TriggerState.phase == InputActionPhase.Waiting)
			{
				this.m_TriggerState.startTime = this.time;
			}
			this.m_State.ChangePhaseOfInteraction(InputActionPhase.Performed, ref this.m_TriggerState, InputActionPhase.Waiting, InputActionPhase.Waiting, true);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000131C4 File Offset: 0x000113C4
		public void PerformedAndStayStarted()
		{
			if (this.m_TriggerState.phase == InputActionPhase.Waiting)
			{
				this.m_TriggerState.startTime = this.time;
			}
			this.m_State.ChangePhaseOfInteraction(InputActionPhase.Performed, ref this.m_TriggerState, InputActionPhase.Started, InputActionPhase.Waiting, true);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000131FA File Offset: 0x000113FA
		public void PerformedAndStayPerformed()
		{
			if (this.m_TriggerState.phase == InputActionPhase.Waiting)
			{
				this.m_TriggerState.startTime = this.time;
			}
			this.m_State.ChangePhaseOfInteraction(InputActionPhase.Performed, ref this.m_TriggerState, InputActionPhase.Performed, InputActionPhase.Waiting, true);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00013230 File Offset: 0x00011430
		public void Canceled()
		{
			if (this.m_TriggerState.phase != InputActionPhase.Canceled)
			{
				this.m_State.ChangePhaseOfInteraction(InputActionPhase.Canceled, ref this.m_TriggerState, InputActionPhase.Waiting, InputActionPhase.Waiting, true);
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00013255 File Offset: 0x00011455
		public void Waiting()
		{
			if (this.m_TriggerState.phase != InputActionPhase.Waiting)
			{
				this.m_State.ChangePhaseOfInteraction(InputActionPhase.Waiting, ref this.m_TriggerState, InputActionPhase.Waiting, InputActionPhase.Waiting, true);
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001327A File Offset: 0x0001147A
		public void SetTimeout(float seconds)
		{
			this.m_State.StartTimeout(seconds, ref this.m_TriggerState);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001328E File Offset: 0x0001148E
		public void SetTotalTimeoutCompletionTime(float seconds)
		{
			if (seconds <= 0f)
			{
				throw new ArgumentException("Seconds must be a positive value", "seconds");
			}
			this.m_State.SetTotalTimeoutCompletionTime(seconds, ref this.m_TriggerState);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000132BA File Offset: 0x000114BA
		public TValue ReadValue<TValue>() where TValue : struct
		{
			return this.m_State.ReadValue<TValue>(this.m_TriggerState.bindingIndex, this.m_TriggerState.controlIndex, false);
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000132DE File Offset: 0x000114DE
		internal int mapIndex
		{
			get
			{
				return this.m_TriggerState.mapIndex;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000132EB File Offset: 0x000114EB
		internal int controlIndex
		{
			get
			{
				return this.m_TriggerState.controlIndex;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x000132F8 File Offset: 0x000114F8
		internal int bindingIndex
		{
			get
			{
				return this.m_TriggerState.bindingIndex;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00013305 File Offset: 0x00011505
		internal int interactionIndex
		{
			get
			{
				return this.m_TriggerState.interactionIndex;
			}
		}

		// Token: 0x04000241 RID: 577
		internal InputActionState m_State;

		// Token: 0x04000242 RID: 578
		internal InputInteractionContext.Flags m_Flags;

		// Token: 0x04000243 RID: 579
		internal InputActionState.TriggerState m_TriggerState;

		// Token: 0x02000067 RID: 103
		[Flags]
		internal enum Flags
		{
			// Token: 0x04000245 RID: 581
			TimerHasExpired = 2
		}
	}
}
