using System;
using System.Diagnostics;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x0200014D RID: 333
	public static class EnhancedTouchSupport
	{
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0004A6E7 File Offset: 0x000488E7
		public static bool enabled
		{
			get
			{
				return EnhancedTouchSupport.s_Enabled > 0;
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0004A6F4 File Offset: 0x000488F4
		public static void Enable()
		{
			EnhancedTouchSupport.s_Enabled++;
			if (EnhancedTouchSupport.s_Enabled > 1)
			{
				return;
			}
			InputSystem.onDeviceChange += EnhancedTouchSupport.OnDeviceChange;
			InputSystem.onBeforeUpdate += Touch.BeginUpdate;
			InputSystem.onSettingsChange += EnhancedTouchSupport.OnSettingsChange;
			EnhancedTouchSupport.SetUpState();
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0004A750 File Offset: 0x00048950
		public static void Disable()
		{
			if (!EnhancedTouchSupport.enabled)
			{
				return;
			}
			EnhancedTouchSupport.s_Enabled--;
			if (EnhancedTouchSupport.s_Enabled > 0)
			{
				return;
			}
			InputSystem.onDeviceChange -= EnhancedTouchSupport.OnDeviceChange;
			InputSystem.onBeforeUpdate -= Touch.BeginUpdate;
			InputSystem.onSettingsChange -= EnhancedTouchSupport.OnSettingsChange;
			EnhancedTouchSupport.TearDownState();
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0004A7B2 File Offset: 0x000489B2
		internal static void Reset()
		{
			Touch.s_GlobalState.touchscreens = default(InlinedArray<Touchscreen>);
			Touch.s_GlobalState.playerState.Destroy();
			Touch.s_GlobalState.playerState = default(Touch.FingerAndTouchState);
			EnhancedTouchSupport.s_Enabled = 0;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0004A7EC File Offset: 0x000489EC
		private static void SetUpState()
		{
			Touch.s_GlobalState.playerState.updateMask = InputUpdateType.Dynamic | InputUpdateType.Fixed | InputUpdateType.Manual;
			EnhancedTouchSupport.s_UpdateMode = InputSystem.settings.updateMode;
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				EnhancedTouchSupport.OnDeviceChange(inputDevice, InputDeviceChange.Added);
			}
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0004A860 File Offset: 0x00048A60
		internal static void TearDownState()
		{
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				EnhancedTouchSupport.OnDeviceChange(inputDevice, InputDeviceChange.Removed);
			}
			Touch.s_GlobalState.playerState.Destroy();
			Touch.s_GlobalState.playerState = default(Touch.FingerAndTouchState);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0004A8D4 File Offset: 0x00048AD4
		private static void OnDeviceChange(InputDevice device, InputDeviceChange change)
		{
			if (change != InputDeviceChange.Added)
			{
				if (change != InputDeviceChange.Removed)
				{
					return;
				}
				Touchscreen touchscreen = device as Touchscreen;
				if (touchscreen != null)
				{
					Touch.RemoveTouchscreen(touchscreen);
				}
			}
			else
			{
				Touchscreen touchscreen2 = device as Touchscreen;
				if (touchscreen2 != null)
				{
					Touch.AddTouchscreen(touchscreen2);
					return;
				}
			}
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0004A90C File Offset: 0x00048B0C
		private static void OnSettingsChange()
		{
			InputSettings.UpdateMode currentUpdateMode = InputSystem.settings.updateMode;
			if (EnhancedTouchSupport.s_UpdateMode == currentUpdateMode)
			{
				return;
			}
			EnhancedTouchSupport.TearDownState();
			EnhancedTouchSupport.SetUpState();
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0004A937 File Offset: 0x00048B37
		[Conditional("DEVELOPMENT_BUILD")]
		[Conditional("UNITY_EDITOR")]
		internal static void CheckEnabled()
		{
			if (!EnhancedTouchSupport.enabled)
			{
				throw new InvalidOperationException("EnhancedTouch API is not enabled; call EnhancedTouchSupport.Enable()");
			}
		}

		// Token: 0x04000857 RID: 2135
		private static int s_Enabled;

		// Token: 0x04000858 RID: 2136
		private static InputSettings.UpdateMode s_UpdateMode;
	}
}
