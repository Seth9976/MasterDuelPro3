using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000223 RID: 547
	[InputControlLayout(hideInUI = true)]
	public class TouchPhaseControl : InputControl<TouchPhase>
	{
		// Token: 0x06001423 RID: 5155 RVA: 0x0005C43E File Offset: 0x0005A63E
		public TouchPhaseControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatInt;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0005C458 File Offset: 0x0005A658
		public unsafe override TouchPhase ReadUnprocessedValueFromState(void* statePtr)
		{
			return (TouchPhase)base.stateBlock.ReadInt(statePtr);
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0005C474 File Offset: 0x0005A674
		public unsafe override void WriteValueIntoState(TouchPhase value, void* statePtr)
		{
			*(int*)((byte*)statePtr + this.m_StateBlock.byteOffset) = (int)value;
		}
	}
}
