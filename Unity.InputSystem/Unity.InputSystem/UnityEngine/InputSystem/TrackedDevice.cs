using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000BD RID: 189
	[InputControlLayout(displayName = "Tracked Device", isGenericTypeOfDevice = true)]
	public class TrackedDevice : InputDevice
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00034876 File Offset: 0x00032A76
		// (set) Token: 0x06000A1E RID: 2590 RVA: 0x0003487E File Offset: 0x00032A7E
		[InputControl(synthetic = true)]
		public IntegerControl trackingState { get; protected set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00034887 File Offset: 0x00032A87
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x0003488F File Offset: 0x00032A8F
		[InputControl(synthetic = true)]
		public ButtonControl isTracked { get; protected set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00034898 File Offset: 0x00032A98
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x000348A0 File Offset: 0x00032AA0
		[InputControl(noisy = true, dontReset = true)]
		public Vector3Control devicePosition { get; protected set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x000348A9 File Offset: 0x00032AA9
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x000348B1 File Offset: 0x00032AB1
		[InputControl(noisy = true, dontReset = true)]
		public QuaternionControl deviceRotation { get; protected set; }

		// Token: 0x06000A25 RID: 2597 RVA: 0x000348BC File Offset: 0x00032ABC
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.trackingState = base.GetChildControl<IntegerControl>("trackingState");
			this.isTracked = base.GetChildControl<ButtonControl>("isTracked");
			this.devicePosition = base.GetChildControl<Vector3Control>("devicePosition");
			this.deviceRotation = base.GetChildControl<QuaternionControl>("deviceRotation");
		}
	}
}
