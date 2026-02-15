using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000225 RID: 549
	public class Vector2Control : InputControl<Vector2>
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0005C529 File Offset: 0x0005A729
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x0005C531 File Offset: 0x0005A731
		[InputControl(offset = 0U, displayName = "X")]
		public AxisControl x { get; set; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0005C53A File Offset: 0x0005A73A
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x0005C542 File Offset: 0x0005A742
		[InputControl(offset = 4U, displayName = "Y")]
		public AxisControl y { get; set; }

		// Token: 0x0600142E RID: 5166 RVA: 0x0005C54B File Offset: 0x0005A74B
		public Vector2Control()
		{
			this.m_StateBlock.format = InputStateBlock.FormatVector2;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0005C563 File Offset: 0x0005A763
		protected override void FinishSetup()
		{
			this.x = base.GetChildControl<AxisControl>("x");
			this.y = base.GetChildControl<AxisControl>("y");
			base.FinishSetup();
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0005C590 File Offset: 0x0005A790
		public unsafe override Vector2 ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1447379762)
			{
				return *(Vector2*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new Vector2(this.x.ReadUnprocessedValueFromStateWithCaching(statePtr), this.y.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0005C5E0 File Offset: 0x0005A7E0
		public unsafe override void WriteValueIntoState(Vector2 value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1447379762)
			{
				*(Vector2*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.x.WriteValueIntoState(value.x, statePtr);
			this.y.WriteValueIntoState(value.y, statePtr);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0005C638 File Offset: 0x0005A838
		public unsafe override float EvaluateMagnitude(void* statePtr)
		{
			return base.ReadValueFromStateWithCaching(statePtr).magnitude;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0005C654 File Offset: 0x0005A854
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.sizeInBits == 64U && this.m_StateBlock.bitOffset == 0U && this.x.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 4U)
			{
				return InputStateBlock.FormatVector2;
			}
			return InputStateBlock.FormatInvalid;
		}
	}
}
