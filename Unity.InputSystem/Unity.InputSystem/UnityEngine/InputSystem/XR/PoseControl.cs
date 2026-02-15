using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Scripting;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x020000E1 RID: 225
	[Preserve]
	[InputControlLayout(stateType = typeof(PoseState))]
	public class PoseControl : InputControl<PoseState>
	{
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0003D9E7 File Offset: 0x0003BBE7
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0003D9EF File Offset: 0x0003BBEF
		public ButtonControl isTracked { get; set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0003D9F8 File Offset: 0x0003BBF8
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x0003DA00 File Offset: 0x0003BC00
		public IntegerControl trackingState { get; set; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0003DA09 File Offset: 0x0003BC09
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x0003DA11 File Offset: 0x0003BC11
		public Vector3Control position { get; set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0003DA1A File Offset: 0x0003BC1A
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x0003DA22 File Offset: 0x0003BC22
		public QuaternionControl rotation { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0003DA2B File Offset: 0x0003BC2B
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x0003DA33 File Offset: 0x0003BC33
		public Vector3Control velocity { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0003DA3C File Offset: 0x0003BC3C
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x0003DA44 File Offset: 0x0003BC44
		public Vector3Control angularVelocity { get; set; }

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0003DA4D File Offset: 0x0003BC4D
		public PoseControl()
		{
			this.m_StateBlock.format = PoseState.s_Format;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0003DA68 File Offset: 0x0003BC68
		protected override void FinishSetup()
		{
			this.isTracked = base.GetChildControl<ButtonControl>("isTracked");
			this.trackingState = base.GetChildControl<IntegerControl>("trackingState");
			this.position = base.GetChildControl<Vector3Control>("position");
			this.rotation = base.GetChildControl<QuaternionControl>("rotation");
			this.velocity = base.GetChildControl<Vector3Control>("velocity");
			this.angularVelocity = base.GetChildControl<Vector3Control>("angularVelocity");
			base.FinishSetup();
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0003DAE4 File Offset: 0x0003BCE4
		public unsafe override PoseState ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1349481317)
			{
				return *(PoseState*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new PoseState
			{
				isTracked = (this.isTracked.ReadUnprocessedValueFromStateWithCaching(statePtr) > 0.5f),
				trackingState = (InputTrackingState)this.trackingState.ReadUnprocessedValueFromStateWithCaching(statePtr),
				position = this.position.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rotation = this.rotation.ReadUnprocessedValueFromStateWithCaching(statePtr),
				velocity = this.velocity.ReadUnprocessedValueFromStateWithCaching(statePtr),
				angularVelocity = this.angularVelocity.ReadUnprocessedValueFromStateWithCaching(statePtr)
			};
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0003DB98 File Offset: 0x0003BD98
		public unsafe override void WriteValueIntoState(PoseState value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1349481317)
			{
				*(PoseState*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.isTracked.WriteValueIntoState(value.isTracked, statePtr);
			this.trackingState.WriteValueIntoState((uint)value.trackingState, statePtr);
			this.position.WriteValueIntoState(value.position, statePtr);
			this.rotation.WriteValueIntoState(value.rotation, statePtr);
			this.velocity.WriteValueIntoState(value.velocity, statePtr);
			this.angularVelocity.WriteValueIntoState(value.angularVelocity, statePtr);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0003DC38 File Offset: 0x0003BE38
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.sizeInBits == 480U && this.m_StateBlock.bitOffset == 0U && this.isTracked.optimizedControlDataType == 1113150533 && this.trackingState.optimizedControlDataType == 1229870112 && this.position.optimizedControlDataType == 1447379763 && this.rotation.optimizedControlDataType == 1364541780 && this.velocity.optimizedControlDataType == 1447379763 && this.angularVelocity.optimizedControlDataType == 1447379763 && this.trackingState.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 4U && this.position.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 8U && this.rotation.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 20U && this.velocity.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 36U && this.angularVelocity.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 48U)
			{
				return 1349481317;
			}
			return 0;
		}
	}
}
