using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200008C RID: 140
	[InputControlLayout(stateType = typeof(JoystickState), isGenericTypeOfDevice = true)]
	public class Joystick : InputDevice
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001AA4D File Offset: 0x00018C4D
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x0001AA55 File Offset: 0x00018C55
		public ButtonControl trigger { get; protected set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001AA5E File Offset: 0x00018C5E
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0001AA66 File Offset: 0x00018C66
		public StickControl stick { get; protected set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001AA6F File Offset: 0x00018C6F
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0001AA77 File Offset: 0x00018C77
		public AxisControl twist { get; protected set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001AA80 File Offset: 0x00018C80
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0001AA88 File Offset: 0x00018C88
		public Vector2Control hatswitch { get; protected set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001AA91 File Offset: 0x00018C91
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0001AA98 File Offset: 0x00018C98
		public static Joystick current { get; private set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001AAA0 File Offset: 0x00018CA0
		public new static ReadOnlyArray<Joystick> all
		{
			get
			{
				return new ReadOnlyArray<Joystick>(Joystick.s_Joysticks, 0, Joystick.s_JoystickCount);
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001AAB4 File Offset: 0x00018CB4
		protected override void FinishSetup()
		{
			this.trigger = base.GetChildControl<ButtonControl>("{PrimaryTrigger}");
			this.stick = base.GetChildControl<StickControl>("{Primary2DMotion}");
			this.twist = base.TryGetChildControl<AxisControl>("{Twist}");
			this.hatswitch = base.TryGetChildControl<Vector2Control>("{Hatswitch}");
			base.FinishSetup();
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001AB0B File Offset: 0x00018D0B
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Joystick.current = this;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001AB19 File Offset: 0x00018D19
		protected override void OnAdded()
		{
			ArrayHelpers.AppendWithCapacity<Joystick>(ref Joystick.s_Joysticks, ref Joystick.s_JoystickCount, this, 10);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001AB30 File Offset: 0x00018D30
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Joystick.current == this)
			{
				Joystick.current = null;
			}
			int index = Joystick.s_Joysticks.IndexOfReference(this, Joystick.s_JoystickCount);
			if (index != -1)
			{
				Joystick.s_Joysticks.EraseAtWithCapacity(ref Joystick.s_JoystickCount, index);
			}
		}

		// Token: 0x04000339 RID: 825
		private static int s_JoystickCount;

		// Token: 0x0400033A RID: 826
		private static Joystick[] s_Joysticks;
	}
}
