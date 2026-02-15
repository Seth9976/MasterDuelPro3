using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace Unity.XR.OpenVR
{
	// Token: 0x0200000A RID: 10
	[InputControlLayout(displayName = "Handed Vive Tracker", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class HandedViveTracker : ViveTracker
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000256D File Offset: 0x0000076D
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002575 File Offset: 0x00000775
		[InputControl]
		public AxisControl grip { get; protected set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000257E File Offset: 0x0000077E
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002586 File Offset: 0x00000786
		[InputControl]
		public ButtonControl gripPressed { get; protected set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000258F File Offset: 0x0000078F
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002597 File Offset: 0x00000797
		[InputControl]
		public ButtonControl primary { get; protected set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000025A0 File Offset: 0x000007A0
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000025A8 File Offset: 0x000007A8
		[InputControl(aliases = new string[] { "JoystickOrPadPressed" })]
		public ButtonControl trackpadPressed { get; protected set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000025B1 File Offset: 0x000007B1
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000025B9 File Offset: 0x000007B9
		[InputControl]
		public ButtonControl triggerPressed { get; protected set; }

		// Token: 0x06000055 RID: 85 RVA: 0x000025C4 File Offset: 0x000007C4
		protected override void FinishSetup()
		{
			this.grip = base.GetChildControl<AxisControl>("grip");
			this.primary = base.GetChildControl<ButtonControl>("primary");
			this.gripPressed = base.GetChildControl<ButtonControl>("gripPressed");
			this.trackpadPressed = base.GetChildControl<ButtonControl>("trackpadPressed");
			this.triggerPressed = base.GetChildControl<ButtonControl>("triggerPressed");
			base.FinishSetup();
		}
	}
}
