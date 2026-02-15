using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XInput
{
	// Token: 0x020000FE RID: 254
	[InputControlLayout(displayName = "Xbox Controller")]
	public class XInputController : Gamepad
	{
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0003FA29 File Offset: 0x0003DC29
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x0003FA31 File Offset: 0x0003DC31
		[InputControl(name = "buttonSouth", displayName = "A")]
		[InputControl(name = "buttonEast", displayName = "B")]
		[InputControl(name = "buttonWest", displayName = "X")]
		[InputControl(name = "buttonNorth", displayName = "Y")]
		[InputControl(name = "leftShoulder", displayName = "Left Bumper", shortDisplayName = "LB")]
		[InputControl(name = "rightShoulder", displayName = "Right Bumper", shortDisplayName = "RB")]
		[InputControl(name = "leftTrigger", shortDisplayName = "LT")]
		[InputControl(name = "rightTrigger", shortDisplayName = "RT")]
		[InputControl(name = "start", displayName = "Menu", alias = "menu")]
		[InputControl(name = "select", displayName = "View", alias = "view")]
		public ButtonControl menu { get; protected set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0003FA3A File Offset: 0x0003DC3A
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x0003FA42 File Offset: 0x0003DC42
		public ButtonControl view { get; protected set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0003FA4B File Offset: 0x0003DC4B
		public XInputController.DeviceSubType subType
		{
			get
			{
				if (!this.m_HaveParsedCapabilities)
				{
					this.ParseCapabilities();
				}
				return this.m_SubType;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0003FA61 File Offset: 0x0003DC61
		public XInputController.DeviceFlags flags
		{
			get
			{
				if (!this.m_HaveParsedCapabilities)
				{
					this.ParseCapabilities();
				}
				return this.m_Flags;
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0003FA77 File Offset: 0x0003DC77
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.menu = base.startButton;
			this.view = base.selectButton;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0003FA98 File Offset: 0x0003DC98
		private void ParseCapabilities()
		{
			if (!string.IsNullOrEmpty(base.description.capabilities))
			{
				XInputController.Capabilities capabilities = JsonUtility.FromJson<XInputController.Capabilities>(base.description.capabilities);
				this.m_SubType = capabilities.subType;
				this.m_Flags = capabilities.flags;
			}
			this.m_HaveParsedCapabilities = true;
		}

		// Token: 0x040005C2 RID: 1474
		private bool m_HaveParsedCapabilities;

		// Token: 0x040005C3 RID: 1475
		private XInputController.DeviceSubType m_SubType;

		// Token: 0x040005C4 RID: 1476
		private XInputController.DeviceFlags m_Flags;

		// Token: 0x020000FF RID: 255
		internal enum DeviceType
		{
			// Token: 0x040005C6 RID: 1478
			Gamepad
		}

		// Token: 0x02000100 RID: 256
		public enum DeviceSubType
		{
			// Token: 0x040005C8 RID: 1480
			Unknown,
			// Token: 0x040005C9 RID: 1481
			Gamepad,
			// Token: 0x040005CA RID: 1482
			Wheel,
			// Token: 0x040005CB RID: 1483
			ArcadeStick,
			// Token: 0x040005CC RID: 1484
			FlightStick,
			// Token: 0x040005CD RID: 1485
			DancePad,
			// Token: 0x040005CE RID: 1486
			Guitar,
			// Token: 0x040005CF RID: 1487
			GuitarAlternate,
			// Token: 0x040005D0 RID: 1488
			DrumKit,
			// Token: 0x040005D1 RID: 1489
			GuitarBass = 11,
			// Token: 0x040005D2 RID: 1490
			ArcadePad = 19
		}

		// Token: 0x02000101 RID: 257
		[Flags]
		public new enum DeviceFlags
		{
			// Token: 0x040005D4 RID: 1492
			ForceFeedbackSupported = 1,
			// Token: 0x040005D5 RID: 1493
			Wireless = 2,
			// Token: 0x040005D6 RID: 1494
			VoiceSupported = 4,
			// Token: 0x040005D7 RID: 1495
			PluginModulesSupported = 8,
			// Token: 0x040005D8 RID: 1496
			NoNavigation = 16
		}

		// Token: 0x02000102 RID: 258
		[Serializable]
		internal struct Capabilities
		{
			// Token: 0x040005D9 RID: 1497
			public XInputController.DeviceType type;

			// Token: 0x040005DA RID: 1498
			public XInputController.DeviceSubType subType;

			// Token: 0x040005DB RID: 1499
			public XInputController.DeviceFlags flags;
		}
	}
}
