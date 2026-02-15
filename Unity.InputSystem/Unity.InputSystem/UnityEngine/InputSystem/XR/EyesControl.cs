using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x020000F3 RID: 243
	public class EyesControl : InputControl<Eyes>
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0003F11E File Offset: 0x0003D31E
		// (set) Token: 0x06000C5C RID: 3164 RVA: 0x0003F126 File Offset: 0x0003D326
		[InputControl(offset = 0U, displayName = "LeftEyePosition")]
		public Vector3Control leftEyePosition { get; set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0003F12F File Offset: 0x0003D32F
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x0003F137 File Offset: 0x0003D337
		[InputControl(offset = 12U, displayName = "LeftEyeRotation")]
		public QuaternionControl leftEyeRotation { get; set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0003F140 File Offset: 0x0003D340
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x0003F148 File Offset: 0x0003D348
		[InputControl(offset = 28U, displayName = "RightEyePosition")]
		public Vector3Control rightEyePosition { get; set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0003F151 File Offset: 0x0003D351
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x0003F159 File Offset: 0x0003D359
		[InputControl(offset = 40U, displayName = "RightEyeRotation")]
		public QuaternionControl rightEyeRotation { get; set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0003F162 File Offset: 0x0003D362
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x0003F16A File Offset: 0x0003D36A
		[InputControl(offset = 56U, displayName = "FixationPoint")]
		public Vector3Control fixationPoint { get; set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0003F173 File Offset: 0x0003D373
		// (set) Token: 0x06000C66 RID: 3174 RVA: 0x0003F17B File Offset: 0x0003D37B
		[InputControl(offset = 68U, displayName = "LeftEyeOpenAmount")]
		public AxisControl leftEyeOpenAmount { get; set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0003F184 File Offset: 0x0003D384
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x0003F18C File Offset: 0x0003D38C
		[InputControl(offset = 72U, displayName = "RightEyeOpenAmount")]
		public AxisControl rightEyeOpenAmount { get; set; }

		// Token: 0x06000C69 RID: 3177 RVA: 0x0003F198 File Offset: 0x0003D398
		protected override void FinishSetup()
		{
			this.leftEyePosition = base.GetChildControl<Vector3Control>("leftEyePosition");
			this.leftEyeRotation = base.GetChildControl<QuaternionControl>("leftEyeRotation");
			this.rightEyePosition = base.GetChildControl<Vector3Control>("rightEyePosition");
			this.rightEyeRotation = base.GetChildControl<QuaternionControl>("rightEyeRotation");
			this.fixationPoint = base.GetChildControl<Vector3Control>("fixationPoint");
			this.leftEyeOpenAmount = base.GetChildControl<AxisControl>("leftEyeOpenAmount");
			this.rightEyeOpenAmount = base.GetChildControl<AxisControl>("rightEyeOpenAmount");
			base.FinishSetup();
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0003F224 File Offset: 0x0003D424
		public unsafe override Eyes ReadUnprocessedValueFromState(void* statePtr)
		{
			return new Eyes
			{
				leftEyePosition = this.leftEyePosition.ReadUnprocessedValueFromStateWithCaching(statePtr),
				leftEyeRotation = this.leftEyeRotation.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rightEyePosition = this.rightEyePosition.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rightEyeRotation = this.rightEyeRotation.ReadUnprocessedValueFromStateWithCaching(statePtr),
				fixationPoint = this.fixationPoint.ReadUnprocessedValueFromStateWithCaching(statePtr),
				leftEyeOpenAmount = this.leftEyeOpenAmount.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rightEyeOpenAmount = this.rightEyeOpenAmount.ReadUnprocessedValueFromStateWithCaching(statePtr)
			};
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0003F2C0 File Offset: 0x0003D4C0
		public unsafe override void WriteValueIntoState(Eyes value, void* statePtr)
		{
			this.leftEyePosition.WriteValueIntoState(value.leftEyePosition, statePtr);
			this.leftEyeRotation.WriteValueIntoState(value.leftEyeRotation, statePtr);
			this.rightEyePosition.WriteValueIntoState(value.rightEyePosition, statePtr);
			this.rightEyeRotation.WriteValueIntoState(value.rightEyeRotation, statePtr);
			this.fixationPoint.WriteValueIntoState(value.fixationPoint, statePtr);
			this.leftEyeOpenAmount.WriteValueIntoState(value.leftEyeOpenAmount, statePtr);
			this.rightEyeOpenAmount.WriteValueIntoState(value.rightEyeOpenAmount, statePtr);
		}
	}
}
