using System;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200021A RID: 538
	public class DoubleControl : InputControl<double>
	{
		// Token: 0x060013CE RID: 5070 RVA: 0x0005B94E File Offset: 0x00059B4E
		public DoubleControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatDouble;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0005B966 File Offset: 0x00059B66
		public unsafe override double ReadUnprocessedValueFromState(void* statePtr)
		{
			return this.m_StateBlock.ReadDouble(statePtr);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0005B974 File Offset: 0x00059B74
		public unsafe override void WriteValueIntoState(double value, void* statePtr)
		{
			this.m_StateBlock.WriteDouble(statePtr, value);
		}
	}
}
