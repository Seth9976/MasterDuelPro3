using System;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000218 RID: 536
	public class DiscreteButtonControl : ButtonControl
	{
		// Token: 0x060013CA RID: 5066 RVA: 0x0005B7E0 File Offset: 0x000599E0
		protected override void FinishSetup()
		{
			base.FinishSetup();
			if (!base.stateBlock.format.IsIntegerFormat())
			{
				throw new NotSupportedException(string.Format("Non-integer format '{0}' is not supported for DiscreteButtonControl '{1}'", base.stateBlock.format, this));
			}
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0005B82C File Offset: 0x00059A2C
		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			int intValue = MemoryHelpers.ReadTwosComplementMultipleBitsAsInt((void*)((byte*)statePtr + this.m_StateBlock.byteOffset), this.m_StateBlock.bitOffset, this.m_StateBlock.sizeInBits);
			float value = 0f;
			if (this.minValue > this.maxValue)
			{
				if (this.wrapAtValue == this.nullValue)
				{
					this.wrapAtValue = this.minValue;
				}
				if ((intValue >= this.minValue && intValue <= this.wrapAtValue) || (intValue != this.nullValue && intValue <= this.maxValue))
				{
					value = 1f;
				}
			}
			else
			{
				value = ((intValue >= this.minValue && intValue <= this.maxValue) ? 1f : 0f);
			}
			return base.Preprocess(value);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0005B8E4 File Offset: 0x00059AE4
		public unsafe override void WriteValueIntoState(float value, void* statePtr)
		{
			if (this.writeMode == DiscreteButtonControl.WriteMode.WriteNullAndMaxValue)
			{
				void* ptr = (void*)((byte*)statePtr + this.m_StateBlock.byteOffset);
				int valueToWrite = ((value >= base.pressPointOrDefault) ? this.maxValue : this.nullValue);
				MemoryHelpers.WriteIntAsTwosComplementMultipleBits(ptr, this.m_StateBlock.bitOffset, this.m_StateBlock.sizeInBits, valueToWrite);
				return;
			}
			throw new NotSupportedException("Writing value states for DiscreteButtonControl is not supported as a single value may correspond to multiple states");
		}

		// Token: 0x04000BE3 RID: 3043
		public int minValue;

		// Token: 0x04000BE4 RID: 3044
		public int maxValue;

		// Token: 0x04000BE5 RID: 3045
		public int wrapAtValue;

		// Token: 0x04000BE6 RID: 3046
		public int nullValue;

		// Token: 0x04000BE7 RID: 3047
		public DiscreteButtonControl.WriteMode writeMode;

		// Token: 0x02000219 RID: 537
		public enum WriteMode
		{
			// Token: 0x04000BE9 RID: 3049
			WriteDisabled,
			// Token: 0x04000BEA RID: 3050
			WriteNullAndMaxValue
		}
	}
}
