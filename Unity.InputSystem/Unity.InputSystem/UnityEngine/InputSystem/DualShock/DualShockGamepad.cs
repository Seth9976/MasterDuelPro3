using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.DualShock
{
	// Token: 0x02000157 RID: 343
	[InputControlLayout(displayName = "PlayStation Controller")]
	public class DualShockGamepad : Gamepad, IDualShockHaptics, IDualMotorRumble, IHaptics
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0004C080 File Offset: 0x0004A280
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x0004C088 File Offset: 0x0004A288
		[InputControl(name = "buttonWest", displayName = "Square", shortDisplayName = "Square")]
		[InputControl(name = "buttonNorth", displayName = "Triangle", shortDisplayName = "Triangle")]
		[InputControl(name = "buttonEast", displayName = "Circle", shortDisplayName = "Circle")]
		[InputControl(name = "buttonSouth", displayName = "Cross", shortDisplayName = "Cross")]
		[InputControl]
		public ButtonControl touchpadButton { get; protected set; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0004C091 File Offset: 0x0004A291
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x0004C099 File Offset: 0x0004A299
		[InputControl(name = "start", displayName = "Options")]
		public ButtonControl optionsButton { get; protected set; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x0004C0A2 File Offset: 0x0004A2A2
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x0004C0AA File Offset: 0x0004A2AA
		[InputControl(name = "select", displayName = "Share")]
		public ButtonControl shareButton { get; protected set; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0004C0B3 File Offset: 0x0004A2B3
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x0004C0BB File Offset: 0x0004A2BB
		[InputControl(name = "leftShoulder", displayName = "L1", shortDisplayName = "L1")]
		public ButtonControl L1 { get; protected set; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x0004C0C4 File Offset: 0x0004A2C4
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x0004C0CC File Offset: 0x0004A2CC
		[InputControl(name = "rightShoulder", displayName = "R1", shortDisplayName = "R1")]
		public ButtonControl R1 { get; protected set; }

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x0004C0D5 File Offset: 0x0004A2D5
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x0004C0DD File Offset: 0x0004A2DD
		[InputControl(name = "leftTrigger", displayName = "L2", shortDisplayName = "L2")]
		public ButtonControl L2 { get; protected set; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0004C0E6 File Offset: 0x0004A2E6
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x0004C0EE File Offset: 0x0004A2EE
		[InputControl(name = "rightTrigger", displayName = "R2", shortDisplayName = "R2")]
		public ButtonControl R2 { get; protected set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0004C0F7 File Offset: 0x0004A2F7
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x0004C0FF File Offset: 0x0004A2FF
		[InputControl(name = "leftStickPress", displayName = "L3", shortDisplayName = "L3")]
		public ButtonControl L3 { get; protected set; }

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0004C108 File Offset: 0x0004A308
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x0004C110 File Offset: 0x0004A310
		[InputControl(name = "rightStickPress", displayName = "R3", shortDisplayName = "R3")]
		public ButtonControl R3 { get; protected set; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0004C119 File Offset: 0x0004A319
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x0004C120 File Offset: 0x0004A320
		public new static DualShockGamepad current { get; private set; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0004C128 File Offset: 0x0004A328
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x0004C130 File Offset: 0x0004A330
		internal HID.HIDDeviceDescriptor hidDescriptor { get; private set; }

		// Token: 0x06000F0C RID: 3852 RVA: 0x0004C139 File Offset: 0x0004A339
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			DualShockGamepad.current = this;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0004C147 File Offset: 0x0004A347
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (DualShockGamepad.current == this)
			{
				DualShockGamepad.current = null;
			}
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0004C160 File Offset: 0x0004A360
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.touchpadButton = base.GetChildControl<ButtonControl>("touchpadButton");
			this.optionsButton = base.startButton;
			this.shareButton = base.selectButton;
			this.L1 = base.leftShoulder;
			this.R1 = base.rightShoulder;
			this.L2 = base.leftTrigger;
			this.R2 = base.rightTrigger;
			this.L3 = base.leftStickButton;
			this.R3 = base.rightStickButton;
			if (this.m_Description.capabilities != null && this.m_Description.interfaceName == "HID")
			{
				this.hidDescriptor = HID.HIDDeviceDescriptor.FromJson(this.m_Description.capabilities);
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x000049FE File Offset: 0x00002BFE
		public virtual void SetLightBarColor(Color color)
		{
		}
	}
}
