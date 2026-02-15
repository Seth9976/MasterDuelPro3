using System;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000122 RID: 290
	[AddComponentMenu("Input/Virtual Mouse")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.11/manual/UISupport.html#virtual-mouse-cursor-control")]
	public class VirtualMouseInput : MonoBehaviour
	{
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00045835 File Offset: 0x00043A35
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x0004583D File Offset: 0x00043A3D
		public RectTransform cursorTransform
		{
			get
			{
				return this.m_CursorTransform;
			}
			set
			{
				this.m_CursorTransform = value;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00045846 File Offset: 0x00043A46
		// (set) Token: 0x06000DDC RID: 3548 RVA: 0x0004584E File Offset: 0x00043A4E
		public float cursorSpeed
		{
			get
			{
				return this.m_CursorSpeed;
			}
			set
			{
				this.m_CursorSpeed = value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00045857 File Offset: 0x00043A57
		// (set) Token: 0x06000DDE RID: 3550 RVA: 0x00045860 File Offset: 0x00043A60
		public VirtualMouseInput.CursorMode cursorMode
		{
			get
			{
				return this.m_CursorMode;
			}
			set
			{
				if (this.m_CursorMode == value)
				{
					return;
				}
				if (this.m_CursorMode == VirtualMouseInput.CursorMode.HardwareCursorIfAvailable && this.m_SystemMouse != null)
				{
					InputSystem.EnableDevice(this.m_SystemMouse);
					this.m_SystemMouse = null;
				}
				this.m_CursorMode = value;
				if (this.m_CursorMode == VirtualMouseInput.CursorMode.HardwareCursorIfAvailable)
				{
					this.TryEnableHardwareCursor();
					return;
				}
				if (this.m_CursorGraphic != null)
				{
					this.m_CursorGraphic.enabled = true;
				}
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x000458CB File Offset: 0x00043ACB
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x000458D3 File Offset: 0x00043AD3
		public Graphic cursorGraphic
		{
			get
			{
				return this.m_CursorGraphic;
			}
			set
			{
				this.m_CursorGraphic = value;
				this.TryFindCanvas();
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x000458E2 File Offset: 0x00043AE2
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x000458EA File Offset: 0x00043AEA
		public float scrollSpeed
		{
			get
			{
				return this.m_ScrollSpeed;
			}
			set
			{
				this.m_ScrollSpeed = value;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x000458F3 File Offset: 0x00043AF3
		public Mouse virtualMouse
		{
			get
			{
				return this.m_VirtualMouse;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x000458FB File Offset: 0x00043AFB
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x00045903 File Offset: 0x00043B03
		public InputActionProperty stickAction
		{
			get
			{
				return this.m_StickAction;
			}
			set
			{
				VirtualMouseInput.SetAction(ref this.m_StickAction, value);
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x00045911 File Offset: 0x00043B11
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x0004591C File Offset: 0x00043B1C
		public InputActionProperty leftButtonAction
		{
			get
			{
				return this.m_LeftButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_LeftButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_LeftButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_LeftButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00045969 File Offset: 0x00043B69
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x00045974 File Offset: 0x00043B74
		public InputActionProperty rightButtonAction
		{
			get
			{
				return this.m_RightButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_RightButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_RightButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_RightButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x000459C1 File Offset: 0x00043BC1
		// (set) Token: 0x06000DEB RID: 3563 RVA: 0x000459CC File Offset: 0x00043BCC
		public InputActionProperty middleButtonAction
		{
			get
			{
				return this.m_MiddleButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_MiddleButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_MiddleButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_MiddleButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00045A19 File Offset: 0x00043C19
		// (set) Token: 0x06000DED RID: 3565 RVA: 0x00045A24 File Offset: 0x00043C24
		public InputActionProperty forwardButtonAction
		{
			get
			{
				return this.m_ForwardButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_ForwardButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_ForwardButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_ForwardButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00045A71 File Offset: 0x00043C71
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x00045A7C File Offset: 0x00043C7C
		public InputActionProperty backButtonAction
		{
			get
			{
				return this.m_BackButtonAction;
			}
			set
			{
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_BackButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				}
				VirtualMouseInput.SetAction(ref this.m_BackButtonAction, value);
				if (this.m_ButtonActionTriggeredDelegate != null)
				{
					VirtualMouseInput.SetActionCallback(this.m_BackButtonAction, this.m_ButtonActionTriggeredDelegate, true);
				}
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00045AC9 File Offset: 0x00043CC9
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x00045AD1 File Offset: 0x00043CD1
		public InputActionProperty scrollWheelAction
		{
			get
			{
				return this.m_ScrollWheelAction;
			}
			set
			{
				VirtualMouseInput.SetAction(ref this.m_ScrollWheelAction, value);
			}
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00045AE0 File Offset: 0x00043CE0
		protected void OnEnable()
		{
			if (this.m_CursorMode == VirtualMouseInput.CursorMode.HardwareCursorIfAvailable)
			{
				this.TryEnableHardwareCursor();
			}
			if (this.m_VirtualMouse == null)
			{
				this.m_VirtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse", null, null);
			}
			else if (!this.m_VirtualMouse.added)
			{
				InputSystem.AddDevice(this.m_VirtualMouse);
			}
			if (this.m_CursorTransform != null)
			{
				Vector2 position = this.m_CursorTransform.anchoredPosition;
				InputState.Change<Vector2>(this.m_VirtualMouse.position, position, InputUpdateType.None, default(InputEventPtr));
				Mouse systemMouse = this.m_SystemMouse;
				if (systemMouse != null)
				{
					systemMouse.WarpCursorPosition(position);
				}
			}
			if (this.m_AfterInputUpdateDelegate == null)
			{
				this.m_AfterInputUpdateDelegate = new Action(this.OnAfterInputUpdate);
			}
			InputSystem.onAfterUpdate += this.m_AfterInputUpdateDelegate;
			if (this.m_ButtonActionTriggeredDelegate == null)
			{
				this.m_ButtonActionTriggeredDelegate = new Action<InputAction.CallbackContext>(this.OnButtonActionTriggered);
			}
			VirtualMouseInput.SetActionCallback(this.m_LeftButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			VirtualMouseInput.SetActionCallback(this.m_RightButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			VirtualMouseInput.SetActionCallback(this.m_MiddleButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			VirtualMouseInput.SetActionCallback(this.m_ForwardButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			VirtualMouseInput.SetActionCallback(this.m_BackButtonAction, this.m_ButtonActionTriggeredDelegate, true);
			InputAction action = this.m_StickAction.action;
			if (action != null)
			{
				action.Enable();
			}
			InputAction action2 = this.m_LeftButtonAction.action;
			if (action2 != null)
			{
				action2.Enable();
			}
			InputAction action3 = this.m_RightButtonAction.action;
			if (action3 != null)
			{
				action3.Enable();
			}
			InputAction action4 = this.m_MiddleButtonAction.action;
			if (action4 != null)
			{
				action4.Enable();
			}
			InputAction action5 = this.m_ForwardButtonAction.action;
			if (action5 != null)
			{
				action5.Enable();
			}
			InputAction action6 = this.m_BackButtonAction.action;
			if (action6 != null)
			{
				action6.Enable();
			}
			InputAction action7 = this.m_ScrollWheelAction.action;
			if (action7 == null)
			{
				return;
			}
			action7.Enable();
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00045CB0 File Offset: 0x00043EB0
		protected void OnDisable()
		{
			if (this.m_VirtualMouse != null && this.m_VirtualMouse.added)
			{
				InputSystem.RemoveDevice(this.m_VirtualMouse);
			}
			if (this.m_SystemMouse != null)
			{
				InputSystem.EnableDevice(this.m_SystemMouse);
				this.m_SystemMouse = null;
			}
			if (this.m_AfterInputUpdateDelegate != null)
			{
				InputSystem.onAfterUpdate -= this.m_AfterInputUpdateDelegate;
			}
			InputAction action = this.m_StickAction.action;
			if (action != null)
			{
				action.Disable();
			}
			InputAction action2 = this.m_LeftButtonAction.action;
			if (action2 != null)
			{
				action2.Disable();
			}
			InputAction action3 = this.m_RightButtonAction.action;
			if (action3 != null)
			{
				action3.Disable();
			}
			InputAction action4 = this.m_MiddleButtonAction.action;
			if (action4 != null)
			{
				action4.Disable();
			}
			InputAction action5 = this.m_ForwardButtonAction.action;
			if (action5 != null)
			{
				action5.Disable();
			}
			InputAction action6 = this.m_BackButtonAction.action;
			if (action6 != null)
			{
				action6.Disable();
			}
			InputAction action7 = this.m_ScrollWheelAction.action;
			if (action7 != null)
			{
				action7.Disable();
			}
			if (this.m_ButtonActionTriggeredDelegate != null)
			{
				VirtualMouseInput.SetActionCallback(this.m_LeftButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				VirtualMouseInput.SetActionCallback(this.m_RightButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				VirtualMouseInput.SetActionCallback(this.m_MiddleButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				VirtualMouseInput.SetActionCallback(this.m_ForwardButtonAction, this.m_ButtonActionTriggeredDelegate, false);
				VirtualMouseInput.SetActionCallback(this.m_BackButtonAction, this.m_ButtonActionTriggeredDelegate, false);
			}
			this.m_LastTime = 0.0;
			this.m_LastStickValue = default(Vector2);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00045E21 File Offset: 0x00044021
		private void TryFindCanvas()
		{
			Graphic cursorGraphic = this.m_CursorGraphic;
			this.m_Canvas = ((cursorGraphic != null) ? cursorGraphic.GetComponentInParent<Canvas>() : null);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00045E3C File Offset: 0x0004403C
		private unsafe void TryEnableHardwareCursor()
		{
			ReadOnlyArray<InputDevice> devices = InputSystem.devices;
			for (int i = 0; i < devices.Count; i++)
			{
				InputDevice device = devices[i];
				if (device.native)
				{
					Mouse mouse = device as Mouse;
					if (mouse != null)
					{
						this.m_SystemMouse = mouse;
						break;
					}
				}
			}
			if (this.m_SystemMouse == null)
			{
				if (this.m_CursorGraphic != null)
				{
					this.m_CursorGraphic.enabled = true;
				}
				return;
			}
			InputSystem.DisableDevice(this.m_SystemMouse, false);
			if (this.m_VirtualMouse != null)
			{
				this.m_SystemMouse.WarpCursorPosition(*this.m_VirtualMouse.position.value);
			}
			if (this.m_CursorGraphic != null)
			{
				this.m_CursorGraphic.enabled = false;
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00045EF8 File Offset: 0x000440F8
		private unsafe void UpdateMotion()
		{
			if (this.m_VirtualMouse == null)
			{
				return;
			}
			InputAction stickAction = this.m_StickAction.action;
			if (stickAction == null)
			{
				return;
			}
			Vector2 stickValue = stickAction.ReadValue<Vector2>();
			if (Mathf.Approximately(0f, stickValue.x) && Mathf.Approximately(0f, stickValue.y))
			{
				this.m_LastTime = 0.0;
				this.m_LastStickValue = default(Vector2);
			}
			else
			{
				double currentTime = InputState.currentTime;
				if (Mathf.Approximately(0f, this.m_LastStickValue.x) && Mathf.Approximately(0f, this.m_LastStickValue.y))
				{
					this.m_LastTime = currentTime;
				}
				float deltaTime = (float)(currentTime - this.m_LastTime);
				Vector2 delta = new Vector2(this.m_CursorSpeed * stickValue.x * deltaTime, this.m_CursorSpeed * stickValue.y * deltaTime);
				Vector2 newPosition = *this.m_VirtualMouse.position.value + delta;
				if (this.m_Canvas != null)
				{
					Rect pixelRect = this.m_Canvas.pixelRect;
					newPosition.x = Mathf.Clamp(newPosition.x, pixelRect.xMin, pixelRect.xMax);
					newPosition.y = Mathf.Clamp(newPosition.y, pixelRect.yMin, pixelRect.yMax);
				}
				InputState.Change<Vector2>(this.m_VirtualMouse.position, newPosition, InputUpdateType.None, default(InputEventPtr));
				InputState.Change<Vector2>(this.m_VirtualMouse.delta, delta, InputUpdateType.None, default(InputEventPtr));
				if (this.m_CursorTransform != null && (this.m_CursorMode == VirtualMouseInput.CursorMode.SoftwareCursor || (this.m_CursorMode == VirtualMouseInput.CursorMode.HardwareCursorIfAvailable && this.m_SystemMouse == null)))
				{
					this.m_CursorTransform.anchoredPosition = newPosition;
				}
				this.m_LastStickValue = stickValue;
				this.m_LastTime = currentTime;
				Mouse systemMouse = this.m_SystemMouse;
				if (systemMouse != null)
				{
					systemMouse.WarpCursorPosition(newPosition);
				}
			}
			InputAction scrollAction = this.m_ScrollWheelAction.action;
			if (scrollAction != null)
			{
				Vector2 scrollValue = scrollAction.ReadValue<Vector2>();
				scrollValue.x *= this.m_ScrollSpeed;
				scrollValue.y *= this.m_ScrollSpeed;
				InputState.Change<Vector2>(this.m_VirtualMouse.scroll, scrollValue, InputUpdateType.None, default(InputEventPtr));
			}
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00046138 File Offset: 0x00044338
		private void OnButtonActionTriggered(InputAction.CallbackContext context)
		{
			if (this.m_VirtualMouse == null)
			{
				return;
			}
			InputAction action = context.action;
			MouseButton? button = null;
			if (action == this.m_LeftButtonAction.action)
			{
				button = new MouseButton?(MouseButton.Left);
			}
			else if (action == this.m_RightButtonAction.action)
			{
				button = new MouseButton?(MouseButton.Right);
			}
			else if (action == this.m_MiddleButtonAction.action)
			{
				button = new MouseButton?(MouseButton.Middle);
			}
			else if (action == this.m_ForwardButtonAction.action)
			{
				button = new MouseButton?(MouseButton.Forward);
			}
			else if (action == this.m_BackButtonAction.action)
			{
				button = new MouseButton?(MouseButton.Back);
			}
			if (button != null)
			{
				bool isPressed = context.control.IsPressed(0f);
				MouseState mouseState;
				this.m_VirtualMouse.CopyState(out mouseState);
				mouseState.WithButton(button.Value, isPressed);
				InputState.Change<MouseState>(this.m_VirtualMouse, mouseState, InputUpdateType.None, default(InputEventPtr));
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00046224 File Offset: 0x00044424
		private static void SetActionCallback(InputActionProperty field, Action<InputAction.CallbackContext> callback, bool install = true)
		{
			InputAction action = field.action;
			if (action == null)
			{
				return;
			}
			if (install)
			{
				action.started += callback;
				action.canceled += callback;
				return;
			}
			action.started -= callback;
			action.canceled -= callback;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00046260 File Offset: 0x00044460
		private static void SetAction(ref InputActionProperty field, InputActionProperty value)
		{
			InputActionProperty oldValue = field;
			field = value;
			if (oldValue.reference == null)
			{
				InputAction oldAction = oldValue.action;
				if (oldAction != null && oldAction.enabled)
				{
					oldAction.Disable();
					if (value.reference == null)
					{
						InputAction action = value.action;
						if (action == null)
						{
							return;
						}
						action.Enable();
					}
				}
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x000462C3 File Offset: 0x000444C3
		private void OnAfterInputUpdate()
		{
			this.UpdateMotion();
		}

		// Token: 0x040006B3 RID: 1715
		[Header("Cursor")]
		[Tooltip("Whether the component should set the cursor position of the hardware mouse cursor, if one is available. If so, the software cursor pointed (to by 'Cursor Graphic') will be hidden.")]
		[SerializeField]
		private VirtualMouseInput.CursorMode m_CursorMode;

		// Token: 0x040006B4 RID: 1716
		[Tooltip("The graphic that represents the software cursor. This is hidden if a hardware cursor (see 'Cursor Mode') is used.")]
		[SerializeField]
		private Graphic m_CursorGraphic;

		// Token: 0x040006B5 RID: 1717
		[Tooltip("The transform for the software cursor. Will only be set if a software cursor is used (see 'Cursor Mode'). Moving the cursor updates the anchored position of the transform.")]
		[SerializeField]
		private RectTransform m_CursorTransform;

		// Token: 0x040006B6 RID: 1718
		[Header("Motion")]
		[Tooltip("Speed in pixels per second with which to move the cursor. Scaled by the input from 'Stick Action'.")]
		[SerializeField]
		private float m_CursorSpeed = 400f;

		// Token: 0x040006B7 RID: 1719
		[Tooltip("Scale factor to apply to 'Scroll Wheel Action' when setting the mouse 'scrollWheel' control.")]
		[SerializeField]
		private float m_ScrollSpeed = 45f;

		// Token: 0x040006B8 RID: 1720
		[Space(10f)]
		[Tooltip("Vector2 action that moves the cursor left/right (X) and up/down (Y) on screen.")]
		[SerializeField]
		private InputActionProperty m_StickAction;

		// Token: 0x040006B9 RID: 1721
		[Tooltip("Button action that triggers a left-click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_LeftButtonAction;

		// Token: 0x040006BA RID: 1722
		[Tooltip("Button action that triggers a middle-click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_MiddleButtonAction;

		// Token: 0x040006BB RID: 1723
		[Tooltip("Button action that triggers a right-click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_RightButtonAction;

		// Token: 0x040006BC RID: 1724
		[Tooltip("Button action that triggers a forward button (button #4) click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_ForwardButtonAction;

		// Token: 0x040006BD RID: 1725
		[Tooltip("Button action that triggers a back button (button #5) click on the mouse.")]
		[SerializeField]
		private InputActionProperty m_BackButtonAction;

		// Token: 0x040006BE RID: 1726
		[Tooltip("Vector2 action that feeds into the mouse 'scrollWheel' action (scaled by 'Scroll Speed').")]
		[SerializeField]
		private InputActionProperty m_ScrollWheelAction;

		// Token: 0x040006BF RID: 1727
		private Canvas m_Canvas;

		// Token: 0x040006C0 RID: 1728
		private Mouse m_VirtualMouse;

		// Token: 0x040006C1 RID: 1729
		private Mouse m_SystemMouse;

		// Token: 0x040006C2 RID: 1730
		private Action m_AfterInputUpdateDelegate;

		// Token: 0x040006C3 RID: 1731
		private Action<InputAction.CallbackContext> m_ButtonActionTriggeredDelegate;

		// Token: 0x040006C4 RID: 1732
		private double m_LastTime;

		// Token: 0x040006C5 RID: 1733
		private Vector2 m_LastStickValue;

		// Token: 0x02000123 RID: 291
		public enum CursorMode
		{
			// Token: 0x040006C7 RID: 1735
			SoftwareCursor,
			// Token: 0x040006C8 RID: 1736
			HardwareCursorIfAvailable
		}
	}
}
