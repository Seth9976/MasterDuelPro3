using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Switch;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace MDPro3
{
	// Token: 0x020012A1 RID: 4769
	public class UserInput : MonoBehaviour
	{
		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x06008BCC RID: 35788 RVA: 0x0011F48C File Offset: 0x0011D68C
		// (remove) Token: 0x06008BCD RID: 35789 RVA: 0x0011F4C0 File Offset: 0x0011D6C0
		public static event UserInput.UserInputAction OnDragStart;

		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x06008BCE RID: 35790 RVA: 0x0011F4F4 File Offset: 0x0011D6F4
		// (remove) Token: 0x06008BCF RID: 35791 RVA: 0x0011F528 File Offset: 0x0011D728
		public static event UserInput.UserInputAction OnDragEnd;

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06008BD0 RID: 35792 RVA: 0x0011F55B File Offset: 0x0011D75B
		// (set) Token: 0x06008BD1 RID: 35793 RVA: 0x0011F562 File Offset: 0x0011D762
		public static bool Draging
		{
			get
			{
				return UserInput.m_Draging;
			}
			set
			{
				UserInput.m_Draging = value;
				if (UserInput.m_Draging)
				{
					UserInput.OnDragStart();
					return;
				}
				UserInput.OnDragEnd();
			}
		}

		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x06008BD2 RID: 35794 RVA: 0x0011F588 File Offset: 0x0011D788
		// (remove) Token: 0x06008BD3 RID: 35795 RVA: 0x0011F5BC File Offset: 0x0011D7BC
		public static event UserInput.UserInputAction OnMouseMovedAction;

		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x06008BD4 RID: 35796 RVA: 0x0011F5F0 File Offset: 0x0011D7F0
		// (remove) Token: 0x06008BD5 RID: 35797 RVA: 0x0011F624 File Offset: 0x0011D824
		public static event UserInput.UserInputAction OnMouseCursorHide;

		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06008BD6 RID: 35798 RVA: 0x0011F658 File Offset: 0x0011D858
		// (remove) Token: 0x06008BD7 RID: 35799 RVA: 0x0011F68C File Offset: 0x0011D88C
		public static event UserInput.ControlDeviceChange OnControlDeviceChange;

		// Token: 0x06008BD8 RID: 35800 RVA: 0x0011F6C0 File Offset: 0x0011D8C0
		private void Awake()
		{
			UserInput.instance = this;
			UserInput.PlayerInput = base.GetComponent<PlayerInput>();
			UserInput.PlayerInput.onControlsChanged += this.OnControlsChanged;
			this.moveAction = UserInput.PlayerInput.actions["Navigate"];
			this.cancelAction = UserInput.PlayerInput.actions["Cancel"];
			this.submitAction = UserInput.PlayerInput.actions["Submit"];
			this.mouseAction = UserInput.PlayerInput.actions["MousePos"];
			this.leftClickAction = UserInput.PlayerInput.actions["Click"];
			this.rightClickAction = UserInput.PlayerInput.actions["RightClick"];
			this.middleClickAction = UserInput.PlayerInput.actions["MiddleClick"];
			this.leftScrollAction = UserInput.PlayerInput.actions["LeftScrollWheel"];
			this.rightScrollAction = UserInput.PlayerInput.actions["RightScrollWheel"];
			this.gamepadButtonWestAction = UserInput.PlayerInput.actions["GamepadButtonWest"];
			this.gamepadButtonNorthAction = UserInput.PlayerInput.actions["GamepadButtonNorth"];
			this.leftStickAction = UserInput.PlayerInput.actions["LeftStickPress"];
			this.rightStickAction = UserInput.PlayerInput.actions["RightStickPress"];
			this.leftShoulderAction = UserInput.PlayerInput.actions["LeftShoulderPress"];
			this.rightShoulderAction = UserInput.PlayerInput.actions["RightShoulderPress"];
			this.leftTriggerAction = UserInput.PlayerInput.actions["LeftTriggerPress"];
			this.rightTriggerAction = UserInput.PlayerInput.actions["RightTriggerPress"];
			this.gamepadSelectAction = UserInput.PlayerInput.actions["GamepadSelect"];
			this.gamepadStartAction = UserInput.PlayerInput.actions["GamepadStart"];
			UserInput.OnMouseMovedAction += this.ShowCursor;
		}

		// Token: 0x06008BD9 RID: 35801 RVA: 0x0011F8F4 File Offset: 0x0011DAF4
		private void Update()
		{
			UserInput.MoveInput = this.moveAction.ReadValue<Vector2>();
			UserInput.MousePos = this.mouseAction.ReadValue<Vector2>();
			UserInput.LeftScrollWheel = this.leftScrollAction.ReadValue<Vector2>();
			UserInput.RightScrollWheel = this.rightScrollAction.ReadValue<Vector2>();
			if (UserInput.MousePos != this.lastMousePos)
			{
				this.MouseMovedEvent();
			}
			if (UserInput.MoveInput != Vector2.zero && Cursor.lockState == CursorLockMode.None)
			{
				this.HideCursor();
			}
			UserInput.WasCancelPressed = this.cancelAction.WasPressedThisFrame();
			UserInput.WasSubmitPressed = this.submitAction.WasPressedThisFrame();
			UserInput.WasGamepadButtonWestPressed = this.gamepadButtonWestAction.WasPressedThisFrame();
			UserInput.WasGamepadButtonNorthPressed = this.gamepadButtonNorthAction.WasPressedThisFrame();
			UserInput.WasLeftStickPressed = this.leftStickAction.WasPressedThisFrame();
			UserInput.WasRightStickPressed = this.rightStickAction.WasPressedThisFrame();
			UserInput.WasLeftShoulderPressed = this.leftShoulderAction.WasPressedThisFrame();
			UserInput.WasRightShoulderPressed = this.rightShoulderAction.WasPressedThisFrame();
			UserInput.WasLeftShoulderPressing = this.leftShoulderAction.IsPressed();
			UserInput.WasRightShoulderPressing = this.rightShoulderAction.IsPressed();
			UserInput.WasLeftTriggerPressed = this.leftTriggerAction.WasPressedThisFrame();
			UserInput.WasRightTriggerPressed = this.rightTriggerAction.WasPressedThisFrame();
			UserInput.WasGamepadSelectPressed = this.gamepadSelectAction.WasPressedThisFrame();
			UserInput.WasGamepadStartPressed = this.gamepadStartAction.WasPressedThisFrame();
			UserInput.MouseLeftDown = this.leftClickAction.WasPressedThisFrame();
			UserInput.MouseRightDown = this.rightClickAction.WasPressedThisFrame();
			UserInput.MouseMiddleDown = this.middleClickAction.WasPressedThisFrame();
			UserInput.MouseLeftPressing = this.leftClickAction.IsPressed();
			UserInput.MouseMiddlePressing = this.middleClickAction.IsPressed();
			UserInput.MouseRightPressing = this.rightClickAction.IsPressed();
			UserInput.MouseLeftUp = this.leftClickAction.WasReleasedThisFrame();
			UserInput.MouseRightUp = this.rightClickAction.WasReleasedThisFrame();
			UserInput.MouseMiddleUp = this.middleClickAction.WasReleasedThisFrame();
			this.lastMousePos = UserInput.MousePos;
			if (UserInput.MoveInput.x > 0f)
			{
				if (this.rightPressingTime == 0f)
				{
					UserInput.WasRightPressed = true;
				}
				else
				{
					UserInput.WasRightPressed = false;
				}
				this.rightPressingTime += Time.unscaledDeltaTime;
			}
			else
			{
				this.rightPressingTime = 0f;
				UserInput.WasRightPressed = false;
			}
			if (this.rightPressingTime > 0f && !UserInput.WasRightPressed && this.rightPressingTime > 0.4f && this.rightPressingTime - 0.4f > 0.2f)
			{
				UserInput.WasRightPressed = true;
				this.rightPressingTime -= 0.2f;
			}
			if (UserInput.MoveInput.x < 0f)
			{
				if (this.leftPressingTime == 0f)
				{
					UserInput.WasLeftPressed = true;
				}
				else
				{
					UserInput.WasLeftPressed = false;
				}
				this.leftPressingTime += Time.unscaledDeltaTime;
			}
			else
			{
				this.leftPressingTime = 0f;
				UserInput.WasLeftPressed = false;
			}
			if (this.leftPressingTime > 0f && !UserInput.WasLeftPressed && this.leftPressingTime > 0.4f && this.leftPressingTime - 0.4f > 0.2f)
			{
				UserInput.WasLeftPressed = true;
				this.leftPressingTime -= 0.2f;
			}
			if (UserInput.MoveInput.y > 0f)
			{
				if (this.upPressingTime == 0f)
				{
					UserInput.WasUpPressed = true;
				}
				else
				{
					UserInput.WasUpPressed = false;
				}
				this.upPressingTime += Time.unscaledDeltaTime;
			}
			else
			{
				this.upPressingTime = 0f;
				UserInput.WasUpPressed = false;
			}
			if (this.upPressingTime > 0f && !UserInput.WasUpPressed && this.upPressingTime > 0.4f && this.upPressingTime - 0.4f > 0.2f)
			{
				UserInput.WasUpPressed = true;
				this.upPressingTime -= 0.2f;
			}
			if (UserInput.MoveInput.y < 0f)
			{
				if (this.downPressingTime == 0f)
				{
					UserInput.WasDownPressed = true;
				}
				else
				{
					UserInput.WasDownPressed = false;
				}
				this.downPressingTime += Time.unscaledDeltaTime;
			}
			else
			{
				this.downPressingTime = 0f;
				UserInput.WasDownPressed = false;
			}
			if (this.downPressingTime > 0f && !UserInput.WasDownPressed && this.downPressingTime > 0.4f && this.downPressingTime - 0.4f > 0.2f)
			{
				UserInput.WasDownPressed = true;
				this.downPressingTime -= 0.2f;
			}
			UserInput.HoverObject = null;
			RaycastHit hit;
			if (Program.instance.camera_.cameraMain.gameObject.activeInHierarchy && !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(Program.instance.camera_.cameraMain.ScreenPointToRay(UserInput.MousePos), out hit))
			{
				UserInput.HoverObject = hit.collider.gameObject;
			}
		}

		// Token: 0x06008BDA RID: 35802 RVA: 0x0011FDCB File Offset: 0x0011DFCB
		private void MouseMovedEvent()
		{
			if (UserInput.PlayerInput.currentControlScheme != UserInput.GamepadSchemeName)
			{
				UserInput.UserInputAction onMouseMovedAction = UserInput.OnMouseMovedAction;
				if (onMouseMovedAction == null)
				{
					return;
				}
				onMouseMovedAction();
			}
		}

		// Token: 0x06008BDB RID: 35803 RVA: 0x0011FDF2 File Offset: 0x0011DFF2
		private void OnControlsChanged(PlayerInput input)
		{
			base.StartCoroutine(this.OnControlsChangedAsync(input));
		}

		// Token: 0x06008BDC RID: 35804 RVA: 0x0011FE02 File Offset: 0x0011E002
		private IEnumerator OnControlsChangedAsync(PlayerInput input)
		{
			yield return null;
			UserInput.gamepadType = UserInput.GamepadType.None;
			if (UserInput.PlayerInput.currentControlScheme == UserInput.GamepadSchemeName)
			{
				UserInput.gamepadType = UserInput.GamepadType.Xbox;
				if (Gamepad.current is DualShockGamepad)
				{
					UserInput.gamepadType = UserInput.GamepadType.PlayStation;
				}
				else if (Gamepad.current is SwitchProControllerHID)
				{
					UserInput.gamepadType = UserInput.GamepadType.Nintendo;
				}
			}
			UserInput.ControlDeviceChange onControlDeviceChange = UserInput.OnControlDeviceChange;
			if (onControlDeviceChange != null)
			{
				onControlDeviceChange(input.currentControlScheme);
			}
			yield break;
		}

		// Token: 0x06008BDD RID: 35805 RVA: 0x0011FE11 File Offset: 0x0011E011
		public static bool NeedDefaultSelect()
		{
			return UserInput.PlayerInput.currentControlScheme == UserInput.GamepadSchemeName || Cursor.lockState == CursorLockMode.Locked;
		}

		// Token: 0x06008BDE RID: 35806 RVA: 0x0011FE36 File Offset: 0x0011E036
		public static void SetMoveRepeatRate(float rate)
		{
			UserInput.instance.GetComponent<InputSystemUIInputModule>().moveRepeatRate = rate;
		}

		// Token: 0x06008BDF RID: 35807 RVA: 0x0011FE48 File Offset: 0x0011E048
		public void Rumble(float lowFrequency, float highFrequence, float duration)
		{
			if (!Config.GetBool("Rumble", true))
			{
				return;
			}
			if (UserInput.PlayerInput.currentControlScheme != UserInput.GamepadSchemeName)
			{
				return;
			}
			this.pad = Gamepad.current;
			if (this.pad == null)
			{
				return;
			}
			Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequence);
			if (this.stopRumbleAfterTimeCoroutine != null)
			{
				base.StopCoroutine(this.stopRumbleAfterTimeCoroutine);
			}
			this.stopRumbleAfterTimeCoroutine = base.StartCoroutine(this.StopRumble(duration, this.pad));
		}

		// Token: 0x06008BE0 RID: 35808 RVA: 0x0011FEC7 File Offset: 0x0011E0C7
		private IEnumerator StopRumble(float duration, Gamepad pad)
		{
			float elapsedTime = 0f;
			while (elapsedTime < duration)
			{
				elapsedTime += Time.unscaledDeltaTime;
				yield return null;
			}
			if (pad != null)
			{
				pad.SetMotorSpeeds(0f, 0f);
			}
			this.stopRumbleAfterTimeCoroutine = null;
			yield break;
		}

		// Token: 0x06008BE1 RID: 35809 RVA: 0x0011FEE4 File Offset: 0x0011E0E4
		public static void RumbleForUp()
		{
			UserInput.instance.Rumble(0.1f, 1f, 0.1f);
		}

		// Token: 0x06008BE2 RID: 35810 RVA: 0x0011FEFF File Offset: 0x0011E0FF
		public static void RumbleForDown()
		{
			UserInput.instance.Rumble(1f, 0.1f, 0.1f);
		}

		// Token: 0x06008BE3 RID: 35811 RVA: 0x0011FF1A File Offset: 0x0011E11A
		private void ShowCursor()
		{
			if (this.ignoreNextCursorMove)
			{
				this.ignoreNextCursorMove = false;
				return;
			}
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		// Token: 0x06008BE4 RID: 35812 RVA: 0x0011FF38 File Offset: 0x0011E138
		private void HideCursor()
		{
			this.ignoreNextCursorMove = true;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			UserInput.UserInputAction onMouseCursorHide = UserInput.OnMouseCursorHide;
			if (onMouseCursorHide == null)
			{
				return;
			}
			onMouseCursorHide();
		}

		// Token: 0x06008BE5 RID: 35813 RVA: 0x0011FF5C File Offset: 0x0011E15C
		public static bool InputFieldActivating()
		{
			GameObject current = EventSystem.current.currentSelectedGameObject;
			if (current == null)
			{
				return false;
			}
			Selectable selectable;
			if (!current.TryGetComponent<Selectable>(out selectable))
			{
				return false;
			}
			TMP_InputField inputField = selectable as TMP_InputField;
			return inputField != null && inputField.isFocused;
		}

		// Token: 0x0400C961 RID: 51553
		public static UserInput instance;

		// Token: 0x0400C962 RID: 51554
		public static PlayerInput PlayerInput;

		// Token: 0x0400C963 RID: 51555
		public static string KeyboardSchemeName = "Keyboard&Mouse";

		// Token: 0x0400C964 RID: 51556
		public static string GamepadSchemeName = "Gamepad";

		// Token: 0x0400C965 RID: 51557
		public static bool NextSelectionIsAxis;

		// Token: 0x0400C966 RID: 51558
		public static GameObject HoverObject;

		// Token: 0x0400C969 RID: 51561
		private static bool m_Draging;

		// Token: 0x0400C96D RID: 51565
		public static UserInput.GamepadType gamepadType = UserInput.GamepadType.None;

		// Token: 0x0400C96E RID: 51566
		public static Vector2 MoveInput;

		// Token: 0x0400C96F RID: 51567
		public static Vector2 MousePos;

		// Token: 0x0400C970 RID: 51568
		public static Vector2 LeftScrollWheel;

		// Token: 0x0400C971 RID: 51569
		public static Vector2 RightScrollWheel;

		// Token: 0x0400C972 RID: 51570
		public static bool WasCancelPressed;

		// Token: 0x0400C973 RID: 51571
		public static bool WasSubmitPressed;

		// Token: 0x0400C974 RID: 51572
		public static bool WasLeftPressed;

		// Token: 0x0400C975 RID: 51573
		public static bool WasRightPressed;

		// Token: 0x0400C976 RID: 51574
		public static bool WasUpPressed;

		// Token: 0x0400C977 RID: 51575
		public static bool WasDownPressed;

		// Token: 0x0400C978 RID: 51576
		public static bool WasGamepadButtonWestPressed;

		// Token: 0x0400C979 RID: 51577
		public static bool WasGamepadButtonNorthPressed;

		// Token: 0x0400C97A RID: 51578
		public static bool WasLeftStickPressed;

		// Token: 0x0400C97B RID: 51579
		public static bool WasRightStickPressed;

		// Token: 0x0400C97C RID: 51580
		public static bool WasLeftShoulderPressed;

		// Token: 0x0400C97D RID: 51581
		public static bool WasRightShoulderPressed;

		// Token: 0x0400C97E RID: 51582
		public static bool WasLeftShoulderPressing;

		// Token: 0x0400C97F RID: 51583
		public static bool WasRightShoulderPressing;

		// Token: 0x0400C980 RID: 51584
		public static bool WasLeftTriggerPressed;

		// Token: 0x0400C981 RID: 51585
		public static bool WasRightTriggerPressed;

		// Token: 0x0400C982 RID: 51586
		public static bool WasGamepadSelectPressed;

		// Token: 0x0400C983 RID: 51587
		public static bool WasGamepadStartPressed;

		// Token: 0x0400C984 RID: 51588
		public static bool MouseLeftDown;

		// Token: 0x0400C985 RID: 51589
		public static bool MouseRightDown;

		// Token: 0x0400C986 RID: 51590
		public static bool MouseMiddleDown;

		// Token: 0x0400C987 RID: 51591
		public static bool MouseLeftPressing;

		// Token: 0x0400C988 RID: 51592
		public static bool MouseRightPressing;

		// Token: 0x0400C989 RID: 51593
		public static bool MouseMiddlePressing;

		// Token: 0x0400C98A RID: 51594
		public static bool MouseLeftUp;

		// Token: 0x0400C98B RID: 51595
		public static bool MouseRightUp;

		// Token: 0x0400C98C RID: 51596
		public static bool MouseMiddleUp;

		// Token: 0x0400C98D RID: 51597
		private InputAction moveAction;

		// Token: 0x0400C98E RID: 51598
		private InputAction cancelAction;

		// Token: 0x0400C98F RID: 51599
		private InputAction submitAction;

		// Token: 0x0400C990 RID: 51600
		private InputAction mouseAction;

		// Token: 0x0400C991 RID: 51601
		private InputAction leftClickAction;

		// Token: 0x0400C992 RID: 51602
		private InputAction rightClickAction;

		// Token: 0x0400C993 RID: 51603
		private InputAction middleClickAction;

		// Token: 0x0400C994 RID: 51604
		private InputAction leftScrollAction;

		// Token: 0x0400C995 RID: 51605
		private InputAction rightScrollAction;

		// Token: 0x0400C996 RID: 51606
		private InputAction gamepadButtonWestAction;

		// Token: 0x0400C997 RID: 51607
		private InputAction gamepadButtonNorthAction;

		// Token: 0x0400C998 RID: 51608
		private InputAction leftStickAction;

		// Token: 0x0400C999 RID: 51609
		private InputAction rightStickAction;

		// Token: 0x0400C99A RID: 51610
		private InputAction leftShoulderAction;

		// Token: 0x0400C99B RID: 51611
		private InputAction rightShoulderAction;

		// Token: 0x0400C99C RID: 51612
		private InputAction leftTriggerAction;

		// Token: 0x0400C99D RID: 51613
		private InputAction rightTriggerAction;

		// Token: 0x0400C99E RID: 51614
		private InputAction gamepadSelectAction;

		// Token: 0x0400C99F RID: 51615
		private InputAction gamepadStartAction;

		// Token: 0x0400C9A0 RID: 51616
		private Vector2 lastMousePos;

		// Token: 0x0400C9A1 RID: 51617
		private Gamepad pad;

		// Token: 0x0400C9A2 RID: 51618
		private Coroutine stopRumbleAfterTimeCoroutine;

		// Token: 0x0400C9A3 RID: 51619
		public static string gamePadName;

		// Token: 0x0400C9A4 RID: 51620
		private float leftPressingTime;

		// Token: 0x0400C9A5 RID: 51621
		private float rightPressingTime;

		// Token: 0x0400C9A6 RID: 51622
		private float upPressingTime;

		// Token: 0x0400C9A7 RID: 51623
		private float downPressingTime;

		// Token: 0x0400C9A8 RID: 51624
		private const float moveRepeatDelay = 0.4f;

		// Token: 0x0400C9A9 RID: 51625
		private const float moveRepeatRate = 0.2f;

		// Token: 0x0400C9AA RID: 51626
		private bool ignoreNextCursorMove;

		// Token: 0x020012A2 RID: 4770
		// (Invoke) Token: 0x06008BE9 RID: 35817
		public delegate void UserInputAction();

		// Token: 0x020012A3 RID: 4771
		// (Invoke) Token: 0x06008BED RID: 35821
		public delegate void ControlDeviceChange(string scheme);

		// Token: 0x020012A4 RID: 4772
		public enum GamepadType
		{
			// Token: 0x0400C9AC RID: 51628
			None,
			// Token: 0x0400C9AD RID: 51629
			Xbox,
			// Token: 0x0400C9AE RID: 51630
			PlayStation,
			// Token: 0x0400C9AF RID: 51631
			Nintendo
		}
	}
}
