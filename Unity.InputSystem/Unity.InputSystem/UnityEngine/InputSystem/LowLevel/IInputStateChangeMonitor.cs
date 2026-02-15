using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001D3 RID: 467
	public interface IInputStateChangeMonitor
	{
		// Token: 0x0600115A RID: 4442
		void NotifyControlStateChanged(InputControl control, double time, InputEventPtr eventPtr, long monitorIndex);

		// Token: 0x0600115B RID: 4443
		void NotifyTimerExpired(InputControl control, double time, long monitorIndex, int timerIndex);
	}
}
