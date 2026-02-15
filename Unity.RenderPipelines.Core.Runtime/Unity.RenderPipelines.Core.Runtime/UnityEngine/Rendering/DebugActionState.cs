using System;
using UnityEngine.InputSystem;

namespace UnityEngine.Rendering
{
	// Token: 0x02000097 RID: 151
	internal class DebugActionState
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000D647 File Offset: 0x0000B847
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0000D64F File Offset: 0x0000B84F
		internal bool runningAction { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000D658 File Offset: 0x0000B858
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x0000D660 File Offset: 0x0000B860
		internal float actionState { get; private set; }

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000D66C File Offset: 0x0000B86C
		private void Trigger(int triggerCount, float state)
		{
			this.actionState = state;
			this.runningAction = true;
			this.m_Timer = 0f;
			this.m_TriggerPressedUp = new bool[triggerCount];
			for (int i = 0; i < this.m_TriggerPressedUp.Length; i++)
			{
				this.m_TriggerPressedUp[i] = false;
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000D6BC File Offset: 0x0000B8BC
		public void TriggerWithButton(InputAction action, float state)
		{
			this.inputAction = action;
			this.Trigger(action.bindings.Count, state);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000D6E5 File Offset: 0x0000B8E5
		private void Reset()
		{
			this.runningAction = false;
			this.m_Timer = 0f;
			this.m_TriggerPressedUp = null;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000D700 File Offset: 0x0000B900
		public void Update(DebugActionDesc desc)
		{
			this.actionState = 0f;
			if (this.m_TriggerPressedUp != null)
			{
				this.m_Timer += Time.deltaTime;
				for (int i = 0; i < this.m_TriggerPressedUp.Length; i++)
				{
					if (this.inputAction != null)
					{
						this.m_TriggerPressedUp[i] |= Mathf.Approximately(this.inputAction.ReadValue<float>(), 0f);
					}
				}
				bool allTriggerUp = true;
				foreach (bool value in this.m_TriggerPressedUp)
				{
					allTriggerUp = allTriggerUp && value;
				}
				if (allTriggerUp || (this.m_Timer > desc.repeatDelay && desc.repeatMode == DebugActionRepeatMode.Delay))
				{
					this.Reset();
				}
			}
		}

		// Token: 0x040001F5 RID: 501
		private DebugActionState.DebugActionKeyType m_Type;

		// Token: 0x040001F6 RID: 502
		private InputAction inputAction;

		// Token: 0x040001F7 RID: 503
		private bool[] m_TriggerPressedUp;

		// Token: 0x040001F8 RID: 504
		private float m_Timer;

		// Token: 0x02000098 RID: 152
		private enum DebugActionKeyType
		{
			// Token: 0x040001FC RID: 508
			Button,
			// Token: 0x040001FD RID: 509
			Axis,
			// Token: 0x040001FE RID: 510
			Key
		}
	}
}
