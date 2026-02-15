using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001D2 RID: 466
	public interface IInputStateCallbackReceiver
	{
		// Token: 0x06001157 RID: 4439
		void OnNextUpdate();

		// Token: 0x06001158 RID: 4440
		void OnStateEvent(InputEventPtr eventPtr);

		// Token: 0x06001159 RID: 4441
		bool GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset);
	}
}
