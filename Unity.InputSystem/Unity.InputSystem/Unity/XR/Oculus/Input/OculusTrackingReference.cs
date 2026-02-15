using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace Unity.XR.Oculus.Input
{
	// Token: 0x0200000E RID: 14
	public class OculusTrackingReference : TrackedDevice
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002BE4 File Offset: 0x00000DE4
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00002BEC File Offset: 0x00000DEC
		[InputControl(aliases = new string[] { "trackingReferenceTrackingState" })]
		public new IntegerControl trackingState { get; protected set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00002BF5 File Offset: 0x00000DF5
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00002BFD File Offset: 0x00000DFD
		[InputControl(aliases = new string[] { "trackingReferenceIsTracked" })]
		public new ButtonControl isTracked { get; protected set; }

		// Token: 0x060000B3 RID: 179 RVA: 0x00002C06 File Offset: 0x00000E06
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.trackingState = base.GetChildControl<IntegerControl>("trackingState");
			this.isTracked = base.GetChildControl<ButtonControl>("isTracked");
		}
	}
}
