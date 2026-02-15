using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.DualShock.LowLevel;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.DualShock
{
	// Token: 0x0200015F RID: 351
	[InputControlLayout(stateType = typeof(DualShock3HIDInputReport), hideInUI = true, displayName = "PS3 Controller")]
	public class DualShock3GamepadHID : DualShockGamepad
	{
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0004D047 File Offset: 0x0004B247
		// (set) Token: 0x06000F41 RID: 3905 RVA: 0x0004D04F File Offset: 0x0004B24F
		public ButtonControl leftTriggerButton { get; protected set; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0004D058 File Offset: 0x0004B258
		// (set) Token: 0x06000F43 RID: 3907 RVA: 0x0004D060 File Offset: 0x0004B260
		public ButtonControl rightTriggerButton { get; protected set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0004D069 File Offset: 0x0004B269
		// (set) Token: 0x06000F45 RID: 3909 RVA: 0x0004D071 File Offset: 0x0004B271
		public ButtonControl playStationButton { get; protected set; }

		// Token: 0x06000F46 RID: 3910 RVA: 0x0004D07A File Offset: 0x0004B27A
		protected override void FinishSetup()
		{
			this.leftTriggerButton = base.GetChildControl<ButtonControl>("leftTriggerButton");
			this.rightTriggerButton = base.GetChildControl<ButtonControl>("rightTriggerButton");
			this.playStationButton = base.GetChildControl<ButtonControl>("systemButton");
			base.FinishSetup();
		}
	}
}
