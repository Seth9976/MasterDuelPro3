using System;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x020013F7 RID: 5111
	public class DeviceIcon : MonoBehaviour
	{
		// Token: 0x06009431 RID: 37937 RVA: 0x00151ED6 File Offset: 0x001500D6
		private void Awake()
		{
			UserInput.OnControlDeviceChange += this.OnControlDeviceChange;
			this.OnControlDeviceChange(UserInput.PlayerInput.currentControlScheme);
		}

		// Token: 0x06009432 RID: 37938 RVA: 0x00151EF9 File Offset: 0x001500F9
		private void OnDestroy()
		{
			UserInput.OnControlDeviceChange -= this.OnControlDeviceChange;
		}

		// Token: 0x06009433 RID: 37939 RVA: 0x00151F0C File Offset: 0x0015010C
		private void OnControlDeviceChange(string scheme)
		{
			if (this.displayInputDevice == DeviceIcon.InputDevice.GamePad)
			{
				if (scheme == UserInput.GamepadSchemeName)
				{
					this.Show();
					return;
				}
				this.Hide();
				return;
			}
			else
			{
				if (scheme == UserInput.GamepadSchemeName)
				{
					this.Hide();
					return;
				}
				this.Show();
				return;
			}
		}

		// Token: 0x06009434 RID: 37940 RVA: 0x00151F4C File Offset: 0x0015014C
		private void Show()
		{
			if (this.dispTarget == DeviceIcon.DispType.Graphic)
			{
				CanvasGroup component = base.GetComponent<CanvasGroup>();
				component.alpha = 1f;
				component.blocksRaycasts = true;
				return;
			}
			base.gameObject.SetActive(true);
		}

		// Token: 0x06009435 RID: 37941 RVA: 0x00151F7A File Offset: 0x0015017A
		private void Hide()
		{
			if (this.dispTarget == DeviceIcon.DispType.Graphic)
			{
				CanvasGroup component = base.GetComponent<CanvasGroup>();
				component.alpha = 0f;
				component.blocksRaycasts = false;
				return;
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x0400D27E RID: 53886
		public DeviceIcon.InputDevice displayInputDevice;

		// Token: 0x0400D27F RID: 53887
		public DeviceIcon.DispType dispTarget;

		// Token: 0x020013F8 RID: 5112
		public enum InputDevice
		{
			// Token: 0x0400D281 RID: 53889
			PointingDevice,
			// Token: 0x0400D282 RID: 53890
			GamePad
		}

		// Token: 0x020013F9 RID: 5113
		public enum DispType
		{
			// Token: 0x0400D284 RID: 53892
			Graphic,
			// Token: 0x0400D285 RID: 53893
			GameObject
		}
	}
}
