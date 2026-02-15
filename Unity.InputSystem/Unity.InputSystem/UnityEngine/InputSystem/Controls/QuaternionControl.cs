using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000220 RID: 544
	public class QuaternionControl : InputControl<Quaternion>
	{
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x0005BE48 File Offset: 0x0005A048
		// (set) Token: 0x060013EE RID: 5102 RVA: 0x0005BE50 File Offset: 0x0005A050
		[InputControl(displayName = "X")]
		public AxisControl x { get; set; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0005BE59 File Offset: 0x0005A059
		// (set) Token: 0x060013F0 RID: 5104 RVA: 0x0005BE61 File Offset: 0x0005A061
		[InputControl(displayName = "Y")]
		public AxisControl y { get; set; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x0005BE6A File Offset: 0x0005A06A
		// (set) Token: 0x060013F2 RID: 5106 RVA: 0x0005BE72 File Offset: 0x0005A072
		[InputControl(displayName = "Z")]
		public AxisControl z { get; set; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0005BE7B File Offset: 0x0005A07B
		// (set) Token: 0x060013F4 RID: 5108 RVA: 0x0005BE83 File Offset: 0x0005A083
		[InputControl(displayName = "W")]
		public AxisControl w { get; set; }

		// Token: 0x060013F5 RID: 5109 RVA: 0x0005BE8C File Offset: 0x0005A08C
		public QuaternionControl()
		{
			this.m_StateBlock.sizeInBits = 128U;
			this.m_StateBlock.format = InputStateBlock.FormatQuaternion;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0005BEB4 File Offset: 0x0005A0B4
		protected override void FinishSetup()
		{
			this.x = base.GetChildControl<AxisControl>("x");
			this.y = base.GetChildControl<AxisControl>("y");
			this.z = base.GetChildControl<AxisControl>("z");
			this.w = base.GetChildControl<AxisControl>("w");
			base.FinishSetup();
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0005BF0C File Offset: 0x0005A10C
		public unsafe override Quaternion ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1364541780)
			{
				return *(Quaternion*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new Quaternion(this.x.ReadValueFromStateWithCaching(statePtr), this.y.ReadValueFromStateWithCaching(statePtr), this.z.ReadValueFromStateWithCaching(statePtr), this.w.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0005BF74 File Offset: 0x0005A174
		public unsafe override void WriteValueIntoState(Quaternion value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1364541780)
			{
				*(Quaternion*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.x.WriteValueIntoState(value.x, statePtr);
			this.y.WriteValueIntoState(value.y, statePtr);
			this.z.WriteValueIntoState(value.z, statePtr);
			this.w.WriteValueIntoState(value.w, statePtr);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0005BFF0 File Offset: 0x0005A1F0
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.sizeInBits == 128U && this.m_StateBlock.bitOffset == 0U && this.x.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.optimizedControlDataType == InputStateBlock.FormatFloat && this.z.optimizedControlDataType == InputStateBlock.FormatFloat && this.w.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 4U && this.z.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 8U && this.w.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 12U && this.x.m_ProcessorStack.length == 0 && this.y.m_ProcessorStack.length == 0 && this.z.m_ProcessorStack.length == 0)
			{
				return InputStateBlock.FormatQuaternion;
			}
			return InputStateBlock.FormatInvalid;
		}
	}
}
