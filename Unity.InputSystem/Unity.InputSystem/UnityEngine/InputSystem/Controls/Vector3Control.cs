using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000226 RID: 550
	public class Vector3Control : InputControl<Vector3>
	{
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0005C6DA File Offset: 0x0005A8DA
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x0005C6E2 File Offset: 0x0005A8E2
		[InputControl(offset = 0U, displayName = "X")]
		public AxisControl x { get; set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0005C6EB File Offset: 0x0005A8EB
		// (set) Token: 0x06001437 RID: 5175 RVA: 0x0005C6F3 File Offset: 0x0005A8F3
		[InputControl(offset = 4U, displayName = "Y")]
		public AxisControl y { get; set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0005C6FC File Offset: 0x0005A8FC
		// (set) Token: 0x06001439 RID: 5177 RVA: 0x0005C704 File Offset: 0x0005A904
		[InputControl(offset = 8U, displayName = "Z")]
		public AxisControl z { get; set; }

		// Token: 0x0600143A RID: 5178 RVA: 0x0005C70D File Offset: 0x0005A90D
		public Vector3Control()
		{
			this.m_StateBlock.format = InputStateBlock.FormatVector3;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0005C725 File Offset: 0x0005A925
		protected override void FinishSetup()
		{
			this.x = base.GetChildControl<AxisControl>("x");
			this.y = base.GetChildControl<AxisControl>("y");
			this.z = base.GetChildControl<AxisControl>("z");
			base.FinishSetup();
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0005C760 File Offset: 0x0005A960
		public unsafe override Vector3 ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1447379763)
			{
				return *(Vector3*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new Vector3(this.x.ReadUnprocessedValueFromStateWithCaching(statePtr), this.y.ReadUnprocessedValueFromStateWithCaching(statePtr), this.z.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0005C7BC File Offset: 0x0005A9BC
		public unsafe override void WriteValueIntoState(Vector3 value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1447379763)
			{
				*(Vector3*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.x.WriteValueIntoState(value.x, statePtr);
			this.y.WriteValueIntoState(value.y, statePtr);
			this.z.WriteValueIntoState(value.z, statePtr);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0005C828 File Offset: 0x0005AA28
		public unsafe override float EvaluateMagnitude(void* statePtr)
		{
			return base.ReadValueFromStateWithCaching(statePtr).magnitude;
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0005C844 File Offset: 0x0005AA44
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.sizeInBits == 96U && this.m_StateBlock.bitOffset == 0U && this.x.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.optimizedControlDataType == InputStateBlock.FormatFloat && this.z.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 4U && this.z.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 8U)
			{
				return InputStateBlock.FormatVector3;
			}
			return InputStateBlock.FormatInvalid;
		}
	}
}
