using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000213 RID: 531
	[InputControlLayout(hideInUI = true)]
	public class AnyKeyControl : ButtonControl
	{
		// Token: 0x060013AA RID: 5034 RVA: 0x0005B113 File Offset: 0x00059313
		public AnyKeyControl()
		{
			this.m_StateBlock.sizeInBits = 1U;
			this.m_StateBlock.format = InputStateBlock.FormatBit;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0005B137 File Offset: 0x00059337
		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			if (!this.CheckStateIsAtDefault(statePtr, null))
			{
				return 1f;
			}
			return 0f;
		}
	}
}
