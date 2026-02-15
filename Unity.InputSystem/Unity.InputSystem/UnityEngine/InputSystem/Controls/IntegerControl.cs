using System;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200021E RID: 542
	public class IntegerControl : InputControl<int>
	{
		// Token: 0x060013E4 RID: 5092 RVA: 0x0005BCE4 File Offset: 0x00059EE4
		public IntegerControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatInt;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0005BCFC File Offset: 0x00059EFC
		public unsafe override int ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1229870112)
			{
				return *(int*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return this.m_StateBlock.ReadInt(statePtr);
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0005BD2B File Offset: 0x00059F2B
		public unsafe override void WriteValueIntoState(int value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1229870112)
			{
				*(int*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.m_StateBlock.WriteInt(statePtr, value);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0005BD5C File Offset: 0x00059F5C
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.format == InputStateBlock.FormatInt && this.m_StateBlock.sizeInBits == 32U && this.m_StateBlock.bitOffset == 0U)
			{
				return InputStateBlock.FormatInt;
			}
			return InputStateBlock.FormatInvalid;
		}
	}
}
