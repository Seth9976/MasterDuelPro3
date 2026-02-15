using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x020000F2 RID: 242
	public class BoneControl : InputControl<Bone>
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0003F01D File Offset: 0x0003D21D
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x0003F025 File Offset: 0x0003D225
		[InputControl(offset = 0U, displayName = "parentBoneIndex")]
		public IntegerControl parentBoneIndex { get; set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0003F02E File Offset: 0x0003D22E
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x0003F036 File Offset: 0x0003D236
		[InputControl(offset = 4U, displayName = "Position")]
		public Vector3Control position { get; set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0003F03F File Offset: 0x0003D23F
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x0003F047 File Offset: 0x0003D247
		[InputControl(offset = 16U, displayName = "Rotation")]
		public QuaternionControl rotation { get; set; }

		// Token: 0x06000C57 RID: 3159 RVA: 0x0003F050 File Offset: 0x0003D250
		protected override void FinishSetup()
		{
			this.parentBoneIndex = base.GetChildControl<IntegerControl>("parentBoneIndex");
			this.position = base.GetChildControl<Vector3Control>("position");
			this.rotation = base.GetChildControl<QuaternionControl>("rotation");
			base.FinishSetup();
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0003F08C File Offset: 0x0003D28C
		public unsafe override Bone ReadUnprocessedValueFromState(void* statePtr)
		{
			return new Bone
			{
				parentBoneIndex = (uint)this.parentBoneIndex.ReadUnprocessedValueFromStateWithCaching(statePtr),
				position = this.position.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rotation = this.rotation.ReadUnprocessedValueFromStateWithCaching(statePtr)
			};
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0003F0DB File Offset: 0x0003D2DB
		public unsafe override void WriteValueIntoState(Bone value, void* statePtr)
		{
			this.parentBoneIndex.WriteValueIntoState((int)value.parentBoneIndex, statePtr);
			this.position.WriteValueIntoState(value.position, statePtr);
			this.rotation.WriteValueIntoState(value.rotation, statePtr);
		}
	}
}
