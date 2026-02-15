using System;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x020013FA RID: 5114
	[RequireComponent(typeof(CanvasGroup))]
	public class GamepadCursor : MonoBehaviour
	{
		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06009437 RID: 37943 RVA: 0x00151FA8 File Offset: 0x001501A8
		// (set) Token: 0x06009438 RID: 37944 RVA: 0x00151FB0 File Offset: 0x001501B0
		public bool Show
		{
			get
			{
				return this._show;
			}
			set
			{
				this._show = value;
				this.OnControlDeviceChange(UserInput.PlayerInput.currentControlScheme);
			}
		}

		// Token: 0x06009439 RID: 37945 RVA: 0x00151FC9 File Offset: 0x001501C9
		private void OnEnable()
		{
			UserInput.OnControlDeviceChange += this.OnControlDeviceChange;
			this.OnControlDeviceChange(UserInput.PlayerInput.currentControlScheme);
		}

		// Token: 0x0600943A RID: 37946 RVA: 0x00151FEC File Offset: 0x001501EC
		private void OnDisable()
		{
			UserInput.OnControlDeviceChange -= this.OnControlDeviceChange;
		}

		// Token: 0x0600943B RID: 37947 RVA: 0x00151FFF File Offset: 0x001501FF
		private void OnControlDeviceChange(string scheme)
		{
			if (scheme == UserInput.GamepadSchemeName && this.Show)
			{
				base.GetComponent<CanvasGroup>().alpha = 1f;
				return;
			}
			base.GetComponent<CanvasGroup>().alpha = 0f;
		}

		// Token: 0x0400D286 RID: 53894
		private bool _show = true;
	}
}
