using System;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000216 RID: 534
	public class ButtonControl : AxisControl
	{
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x0005B51E File Offset: 0x0005971E
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x0005B526 File Offset: 0x00059726
		internal bool needsToCheckFramePress { get; private set; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x0005B52F File Offset: 0x0005972F
		public float pressPointOrDefault
		{
			get
			{
				if (this.pressPoint <= 0f)
				{
					return ButtonControl.s_GlobalDefaultButtonPressPoint;
				}
				return this.pressPoint;
			}
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0005B54C File Offset: 0x0005974C
		public ButtonControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatBit;
			this.m_MinValue = 0f;
			this.m_MaxValue = 1f;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0005B5A8 File Offset: 0x000597A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public new bool IsValueConsideredPressed(float value)
		{
			return value >= this.pressPointOrDefault;
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0005B5B6 File Offset: 0x000597B6
		public unsafe bool isPressed
		{
			get
			{
				if (!this.needsToCheckFramePress)
				{
					return this.IsValueConsideredPressed(*base.value);
				}
				return this.m_LastUpdateWasPress;
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0005B5D4 File Offset: 0x000597D4
		private void BeginTestingForFramePresses(bool currentlyPressed, bool pressedLastFrame)
		{
			this.needsToCheckFramePress = true;
			base.device.m_ButtonControlsCheckingPressState.Add(this);
			this.m_LastUpdateWasPress = currentlyPressed;
			if (currentlyPressed && !pressedLastFrame)
			{
				this.m_UpdateCountLastPressed = base.device.m_CurrentUpdateStepCount;
				return;
			}
			if (pressedLastFrame && !currentlyPressed)
			{
				this.m_UpdateCountLastReleased = base.device.m_CurrentUpdateStepCount;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0005B630 File Offset: 0x00059830
		public unsafe bool wasPressedThisFrame
		{
			get
			{
				if (!this.needsToCheckFramePress)
				{
					bool currentlyPressed = this.IsValueConsideredPressed(*base.value);
					bool pressedLastFrame = this.IsValueConsideredPressed(base.ReadValueFromPreviousFrame());
					this.BeginTestingForFramePresses(currentlyPressed, pressedLastFrame);
					return base.device.wasUpdatedThisFrame && currentlyPressed && !pressedLastFrame;
				}
				return InputUpdate.s_UpdateStepCount == this.m_UpdateCountLastPressed;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0005B68C File Offset: 0x0005988C
		public unsafe bool wasReleasedThisFrame
		{
			get
			{
				if (!this.needsToCheckFramePress)
				{
					bool currentlyPressed = this.IsValueConsideredPressed(*base.value);
					bool pressedLastFrame = this.IsValueConsideredPressed(base.ReadValueFromPreviousFrame());
					this.BeginTestingForFramePresses(currentlyPressed, pressedLastFrame);
					return base.device.wasUpdatedThisFrame && !currentlyPressed && pressedLastFrame;
				}
				return InputUpdate.s_UpdateStepCount == this.m_UpdateCountLastReleased;
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0005B6E8 File Offset: 0x000598E8
		internal unsafe void UpdateWasPressed()
		{
			bool isNowPressed = this.IsValueConsideredPressed(*base.value);
			if (this.m_LastUpdateWasPress != isNowPressed)
			{
				if (isNowPressed)
				{
					this.m_UpdateCountLastPressed = base.device.m_CurrentUpdateStepCount;
				}
				else
				{
					this.m_UpdateCountLastReleased = base.device.m_CurrentUpdateStepCount;
				}
				this.m_LastUpdateWasPress = isNowPressed;
			}
		}

		// Token: 0x04000BD6 RID: 3030
		private bool m_NeedsToCheckFramePress;

		// Token: 0x04000BD7 RID: 3031
		private uint m_UpdateCountLastPressed = uint.MaxValue;

		// Token: 0x04000BD8 RID: 3032
		private uint m_UpdateCountLastReleased = uint.MaxValue;

		// Token: 0x04000BD9 RID: 3033
		private bool m_LastUpdateWasPress;

		// Token: 0x04000BDB RID: 3035
		public float pressPoint = -1f;

		// Token: 0x04000BDC RID: 3036
		internal static float s_GlobalDefaultButtonPressPoint;

		// Token: 0x04000BDD RID: 3037
		internal static float s_GlobalDefaultButtonReleaseThreshold;

		// Token: 0x04000BDE RID: 3038
		internal const float kMinButtonPressPoint = 0.0001f;
	}
}
