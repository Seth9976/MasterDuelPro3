using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000087 RID: 135
	[InputControlLayout(stateType = typeof(GamepadState), isGenericTypeOfDevice = true)]
	public class Gamepad : InputDevice, IDualMotorRumble, IHaptics
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x000197EB File Offset: 0x000179EB
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x000197F3 File Offset: 0x000179F3
		public ButtonControl buttonWest { get; protected set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x000197FC File Offset: 0x000179FC
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x00019804 File Offset: 0x00017A04
		public ButtonControl buttonNorth { get; protected set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001980D File Offset: 0x00017A0D
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x00019815 File Offset: 0x00017A15
		public ButtonControl buttonSouth { get; protected set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001981E File Offset: 0x00017A1E
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x00019826 File Offset: 0x00017A26
		public ButtonControl buttonEast { get; protected set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001982F File Offset: 0x00017A2F
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x00019837 File Offset: 0x00017A37
		public ButtonControl leftStickButton { get; protected set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00019840 File Offset: 0x00017A40
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x00019848 File Offset: 0x00017A48
		public ButtonControl rightStickButton { get; protected set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00019851 File Offset: 0x00017A51
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x00019859 File Offset: 0x00017A59
		public ButtonControl startButton { get; protected set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00019862 File Offset: 0x00017A62
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x0001986A File Offset: 0x00017A6A
		public ButtonControl selectButton { get; protected set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00019873 File Offset: 0x00017A73
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x0001987B File Offset: 0x00017A7B
		public DpadControl dpad { get; protected set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00019884 File Offset: 0x00017A84
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x0001988C File Offset: 0x00017A8C
		public ButtonControl leftShoulder { get; protected set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00019895 File Offset: 0x00017A95
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x0001989D File Offset: 0x00017A9D
		public ButtonControl rightShoulder { get; protected set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x000198A6 File Offset: 0x00017AA6
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x000198AE File Offset: 0x00017AAE
		public StickControl leftStick { get; protected set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x000198B7 File Offset: 0x00017AB7
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x000198BF File Offset: 0x00017ABF
		public StickControl rightStick { get; protected set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x000198C8 File Offset: 0x00017AC8
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x000198D0 File Offset: 0x00017AD0
		public ButtonControl leftTrigger { get; protected set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x000198D9 File Offset: 0x00017AD9
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x000198E1 File Offset: 0x00017AE1
		public ButtonControl rightTrigger { get; protected set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x000198EA File Offset: 0x00017AEA
		public ButtonControl aButton
		{
			get
			{
				return this.buttonSouth;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x000198F2 File Offset: 0x00017AF2
		public ButtonControl bButton
		{
			get
			{
				return this.buttonEast;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x000198FA File Offset: 0x00017AFA
		public ButtonControl xButton
		{
			get
			{
				return this.buttonWest;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00019902 File Offset: 0x00017B02
		public ButtonControl yButton
		{
			get
			{
				return this.buttonNorth;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00019902 File Offset: 0x00017B02
		public ButtonControl triangleButton
		{
			get
			{
				return this.buttonNorth;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x000198FA File Offset: 0x00017AFA
		public ButtonControl squareButton
		{
			get
			{
				return this.buttonWest;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x000198F2 File Offset: 0x00017AF2
		public ButtonControl circleButton
		{
			get
			{
				return this.buttonEast;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x000198EA File Offset: 0x00017AEA
		public ButtonControl crossButton
		{
			get
			{
				return this.buttonSouth;
			}
		}

		// Token: 0x170001C3 RID: 451
		public ButtonControl this[GamepadButton button]
		{
			get
			{
				switch (button)
				{
				case GamepadButton.DpadUp:
					return this.dpad.up;
				case GamepadButton.DpadDown:
					return this.dpad.down;
				case GamepadButton.DpadLeft:
					return this.dpad.left;
				case GamepadButton.DpadRight:
					return this.dpad.right;
				case GamepadButton.North:
					return this.buttonNorth;
				case GamepadButton.East:
					return this.buttonEast;
				case GamepadButton.South:
					return this.buttonSouth;
				case GamepadButton.West:
					return this.buttonWest;
				case GamepadButton.LeftStick:
					return this.leftStickButton;
				case GamepadButton.RightStick:
					return this.rightStickButton;
				case GamepadButton.LeftShoulder:
					return this.leftShoulder;
				case GamepadButton.RightShoulder:
					return this.rightShoulder;
				case GamepadButton.Start:
					return this.startButton;
				case GamepadButton.Select:
					return this.selectButton;
				default:
					if (button == GamepadButton.LeftTrigger)
					{
						return this.leftTrigger;
					}
					if (button != GamepadButton.RightTrigger)
					{
						throw new InvalidEnumArgumentException("button", (int)button, typeof(GamepadButton));
					}
					return this.rightTrigger;
				}
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x000199FF File Offset: 0x00017BFF
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x00019A06 File Offset: 0x00017C06
		public static Gamepad current { get; private set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00019A0E File Offset: 0x00017C0E
		public new static ReadOnlyArray<Gamepad> all
		{
			get
			{
				return new ReadOnlyArray<Gamepad>(Gamepad.s_Gamepads, 0, Gamepad.s_GamepadCount);
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00019A20 File Offset: 0x00017C20
		protected override void FinishSetup()
		{
			this.buttonWest = base.GetChildControl<ButtonControl>("buttonWest");
			this.buttonNorth = base.GetChildControl<ButtonControl>("buttonNorth");
			this.buttonSouth = base.GetChildControl<ButtonControl>("buttonSouth");
			this.buttonEast = base.GetChildControl<ButtonControl>("buttonEast");
			this.startButton = base.GetChildControl<ButtonControl>("start");
			this.selectButton = base.GetChildControl<ButtonControl>("select");
			this.leftStickButton = base.GetChildControl<ButtonControl>("leftStickPress");
			this.rightStickButton = base.GetChildControl<ButtonControl>("rightStickPress");
			this.dpad = base.GetChildControl<DpadControl>("dpad");
			this.leftShoulder = base.GetChildControl<ButtonControl>("leftShoulder");
			this.rightShoulder = base.GetChildControl<ButtonControl>("rightShoulder");
			this.leftStick = base.GetChildControl<StickControl>("leftStick");
			this.rightStick = base.GetChildControl<StickControl>("rightStick");
			this.leftTrigger = base.GetChildControl<ButtonControl>("leftTrigger");
			this.rightTrigger = base.GetChildControl<ButtonControl>("rightTrigger");
			base.FinishSetup();
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00019B32 File Offset: 0x00017D32
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Gamepad.current = this;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00019B40 File Offset: 0x00017D40
		protected override void OnAdded()
		{
			ArrayHelpers.AppendWithCapacity<Gamepad>(ref Gamepad.s_Gamepads, ref Gamepad.s_GamepadCount, this, 10);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00019B58 File Offset: 0x00017D58
		protected override void OnRemoved()
		{
			if (Gamepad.current == this)
			{
				Gamepad.current = null;
			}
			int index = Gamepad.s_Gamepads.IndexOfReference(this, Gamepad.s_GamepadCount);
			if (index != -1)
			{
				Gamepad.s_Gamepads.EraseAtWithCapacity(ref Gamepad.s_GamepadCount, index);
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00019B98 File Offset: 0x00017D98
		public virtual void PauseHaptics()
		{
			this.m_Rumble.PauseHaptics(this);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00019BA6 File Offset: 0x00017DA6
		public virtual void ResumeHaptics()
		{
			this.m_Rumble.ResumeHaptics(this);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00019BB4 File Offset: 0x00017DB4
		public virtual void ResetHaptics()
		{
			this.m_Rumble.ResetHaptics(this);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00019BC2 File Offset: 0x00017DC2
		public virtual void SetMotorSpeeds(float lowFrequency, float highFrequency)
		{
			this.m_Rumble.SetMotorSpeeds(this, lowFrequency, highFrequency);
		}

		// Token: 0x040002FA RID: 762
		private DualMotorRumble m_Rumble;

		// Token: 0x040002FB RID: 763
		private static int s_GamepadCount;

		// Token: 0x040002FC RID: 764
		private static Gamepad[] s_Gamepads;
	}
}
