using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.Rendering
{
	// Token: 0x0200001D RID: 29
	public class FreeCamera : MonoBehaviour
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004BB6 File Offset: 0x00002DB6
		private void OnEnable()
		{
			this.RegisterInputs();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004BC0 File Offset: 0x00002DC0
		private void RegisterInputs()
		{
			InputActionMap map = new InputActionMap("Free Camera");
			this.lookAction = map.AddAction("look", InputActionType.Value, "<Mouse>/delta", null, null, null, null);
			this.moveAction = map.AddAction("move", InputActionType.Value, "<Gamepad>/leftStick", null, null, null, null);
			this.speedAction = map.AddAction("speed", InputActionType.Value, "<Gamepad>/dpad", null, null, null, null);
			this.yMoveAction = map.AddAction("yMove", InputActionType.Value, null, null, null, null, null);
			this.lookAction.AddBinding("<Gamepad>/rightStick", null, null, null).WithProcessor("scaleVector2(x=15, y=15)");
			this.moveAction.AddCompositeBinding("Dpad", null, null).With("Up", "<Keyboard>/w", null, null).With("Up", "<Keyboard>/upArrow", null, null)
				.With("Down", "<Keyboard>/s", null, null)
				.With("Down", "<Keyboard>/downArrow", null, null)
				.With("Left", "<Keyboard>/a", null, null)
				.With("Left", "<Keyboard>/leftArrow", null, null)
				.With("Right", "<Keyboard>/d", null, null)
				.With("Right", "<Keyboard>/rightArrow", null, null);
			this.speedAction.AddCompositeBinding("Dpad", null, null).With("Up", "<Keyboard>/home", null, null).With("Down", "<Keyboard>/end", null, null);
			this.yMoveAction.AddCompositeBinding("Dpad", null, null).With("Up", "<Keyboard>/pageUp", null, null).With("Down", "<Keyboard>/pageDown", null, null)
				.With("Up", "<Keyboard>/e", null, null)
				.With("Down", "<Keyboard>/q", null, null)
				.With("Up", "<Gamepad>/rightshoulder", null, null)
				.With("Down", "<Gamepad>/leftshoulder", null, null);
			this.moveAction.Enable();
			this.lookAction.Enable();
			this.speedAction.Enable();
			this.yMoveAction.Enable();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004E08 File Offset: 0x00003008
		private void UpdateInputs()
		{
			this.inputRotateAxisX = 0f;
			this.inputRotateAxisY = 0f;
			this.leftShiftBoost = false;
			this.fire1 = false;
			Vector2 lookDelta = this.lookAction.ReadValue<Vector2>();
			this.inputRotateAxisX = lookDelta.x * this.m_LookSpeedMouse * 0.01f;
			this.inputRotateAxisY = lookDelta.y * this.m_LookSpeedMouse * 0.01f;
			Keyboard current = Keyboard.current;
			bool? flag;
			if (current == null)
			{
				flag = null;
			}
			else
			{
				KeyControl leftShiftKey = current.leftShiftKey;
				flag = ((leftShiftKey != null) ? new bool?(leftShiftKey.isPressed) : null);
			}
			bool? flag2 = flag;
			this.leftShift = flag2.GetValueOrDefault();
			Mouse current2 = Mouse.current;
			bool flag3;
			if (current2 == null)
			{
				flag3 = false;
			}
			else
			{
				ButtonControl leftButton = current2.leftButton;
				flag2 = ((leftButton != null) ? new bool?(leftButton.isPressed) : null);
				bool flag4 = true;
				flag3 = (flag2.GetValueOrDefault() == flag4) & (flag2 != null);
			}
			bool flag5;
			if (!flag3)
			{
				Gamepad current3 = Gamepad.current;
				if (current3 == null)
				{
					flag5 = false;
				}
				else
				{
					ButtonControl xButton = current3.xButton;
					flag2 = ((xButton != null) ? new bool?(xButton.isPressed) : null);
					bool flag4 = true;
					flag5 = (flag2.GetValueOrDefault() == flag4) & (flag2 != null);
				}
			}
			else
			{
				flag5 = true;
			}
			this.fire1 = flag5;
			this.inputChangeSpeed = this.speedAction.ReadValue<Vector2>().y;
			Vector2 moveDelta = this.moveAction.ReadValue<Vector2>();
			this.inputVertical = moveDelta.y;
			this.inputHorizontal = moveDelta.x;
			this.inputYAxis = this.yMoveAction.ReadValue<Vector2>().y;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004F94 File Offset: 0x00003194
		private void Update()
		{
			if (DebugManager.instance.displayRuntimeUI)
			{
				return;
			}
			this.UpdateInputs();
			if (this.inputChangeSpeed != 0f)
			{
				this.m_MoveSpeed += this.inputChangeSpeed * this.m_MoveSpeedIncrement;
				if (this.m_MoveSpeed < this.m_MoveSpeedIncrement)
				{
					this.m_MoveSpeed = this.m_MoveSpeedIncrement;
				}
			}
			if (this.inputRotateAxisX != 0f || this.inputRotateAxisY != 0f || this.inputVertical != 0f || this.inputHorizontal != 0f || this.inputYAxis != 0f)
			{
				float x = base.transform.localEulerAngles.x;
				float newRotationY = base.transform.localEulerAngles.y + this.inputRotateAxisX;
				float newRotationX = x - this.inputRotateAxisY;
				if (x <= 90f && newRotationX >= 0f)
				{
					newRotationX = Mathf.Clamp(newRotationX, 0f, 90f);
				}
				if (x >= 270f)
				{
					newRotationX = Mathf.Clamp(newRotationX, 270f, 360f);
				}
				base.transform.localRotation = Quaternion.Euler(newRotationX, newRotationY, base.transform.localEulerAngles.z);
				float moveSpeed = Time.deltaTime * this.m_MoveSpeed;
				if (this.fire1 || (this.leftShiftBoost && this.leftShift))
				{
					moveSpeed *= this.m_Turbo;
				}
				base.transform.position += base.transform.forward * moveSpeed * this.inputVertical;
				base.transform.position += base.transform.right * moveSpeed * this.inputHorizontal;
				base.transform.position += Vector3.up * moveSpeed * this.inputYAxis;
			}
		}

		// Token: 0x04000086 RID: 134
		private const float k_MouseSensitivityMultiplier = 0.01f;

		// Token: 0x04000087 RID: 135
		public float m_LookSpeedController = 120f;

		// Token: 0x04000088 RID: 136
		public float m_LookSpeedMouse = 4f;

		// Token: 0x04000089 RID: 137
		public float m_MoveSpeed = 10f;

		// Token: 0x0400008A RID: 138
		public float m_MoveSpeedIncrement = 2.5f;

		// Token: 0x0400008B RID: 139
		public float m_Turbo = 10f;

		// Token: 0x0400008C RID: 140
		private InputAction lookAction;

		// Token: 0x0400008D RID: 141
		private InputAction moveAction;

		// Token: 0x0400008E RID: 142
		private InputAction speedAction;

		// Token: 0x0400008F RID: 143
		private InputAction yMoveAction;

		// Token: 0x04000090 RID: 144
		private float inputRotateAxisX;

		// Token: 0x04000091 RID: 145
		private float inputRotateAxisY;

		// Token: 0x04000092 RID: 146
		private float inputChangeSpeed;

		// Token: 0x04000093 RID: 147
		private float inputVertical;

		// Token: 0x04000094 RID: 148
		private float inputHorizontal;

		// Token: 0x04000095 RID: 149
		private float inputYAxis;

		// Token: 0x04000096 RID: 150
		private bool leftShiftBoost;

		// Token: 0x04000097 RID: 151
		private bool leftShift;

		// Token: 0x04000098 RID: 152
		private bool fire1;
	}
}
