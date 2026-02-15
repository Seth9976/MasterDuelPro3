using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000224 RID: 548
	[InputControlLayout(hideInUI = true)]
	public class TouchPressControl : ButtonControl
	{
		// Token: 0x06001426 RID: 5158 RVA: 0x0005C488 File Offset: 0x0005A688
		protected override void FinishSetup()
		{
			base.FinishSetup();
			if (!base.stateBlock.format.IsIntegerFormat())
			{
				throw new NotSupportedException(string.Format("Non-integer format '{0}' is not supported for TouchButtonControl '{1}'", base.stateBlock.format, this));
			}
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0005C4D4 File Offset: 0x0005A6D4
		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			TouchPhase phaseValue = (TouchPhase)MemoryHelpers.ReadMultipleBitsAsUInt((void*)((byte*)statePtr + this.m_StateBlock.byteOffset), this.m_StateBlock.bitOffset, this.m_StateBlock.sizeInBits);
			float value = 0f;
			if (phaseValue == TouchPhase.Began || phaseValue == TouchPhase.Stationary || phaseValue == TouchPhase.Moved)
			{
				value = 1f;
			}
			return base.Preprocess(value);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00004C6C File Offset: 0x00002E6C
		public unsafe override void WriteValueIntoState(float value, void* statePtr)
		{
			throw new NotSupportedException();
		}
	}
}
