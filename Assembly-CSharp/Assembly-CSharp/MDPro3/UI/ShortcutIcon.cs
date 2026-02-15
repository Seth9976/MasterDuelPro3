using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200140C RID: 5132
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(LayoutElement))]
	public class ShortcutIcon : MonoBehaviour
	{
		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06009473 RID: 38003 RVA: 0x00152CCD File Offset: 0x00150ECD
		// (set) Token: 0x06009474 RID: 38004 RVA: 0x00152CD5 File Offset: 0x00150ED5
		public bool Show
		{
			get
			{
				return this.show;
			}
			set
			{
				this.show = value;
				this.ChangeShortcutIcon(string.Empty);
			}
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06009475 RID: 38005 RVA: 0x00152CE9 File Offset: 0x00150EE9
		// (set) Token: 0x06009476 RID: 38006 RVA: 0x00152CF1 File Offset: 0x00150EF1
		public bool GroupShow
		{
			get
			{
				return this.groupShow;
			}
			set
			{
				this.groupShow = value;
				this.ChangeShortcutIcon(string.Empty);
			}
		}

		// Token: 0x06009477 RID: 38007 RVA: 0x00152D05 File Offset: 0x00150F05
		private void OnEnable()
		{
			UserInput.OnControlDeviceChange += this.ChangeShortcutIcon;
			this.ChangeShortcutIcon(string.Empty);
		}

		// Token: 0x06009478 RID: 38008 RVA: 0x00152D23 File Offset: 0x00150F23
		private void OnDisable()
		{
			UserInput.OnControlDeviceChange -= this.ChangeShortcutIcon;
		}

		// Token: 0x06009479 RID: 38009 RVA: 0x00152D38 File Offset: 0x00150F38
		private void ChangeShortcutIcon(string scheme)
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (UserInput.gamepadType == UserInput.GamepadType.None || !this.Show || !this.GroupShow)
			{
				base.GetComponent<CanvasGroup>().alpha = 0f;
				base.GetComponent<LayoutElement>().ignoreLayout = true;
				return;
			}
			base.StartCoroutine(this.SetIconAsync());
		}

		// Token: 0x0600947A RID: 38010 RVA: 0x00152D94 File Offset: 0x00150F94
		private IEnumerator SetIconAsync()
		{
			base.GetComponent<CanvasGroup>().alpha = 0f;
			base.GetComponent<LayoutElement>().ignoreLayout = true;
			while (TextureManager.container == null)
			{
				yield return null;
			}
			Sprite icon = TextureManager.container.GetGamepadIcon(this.button);
			if (icon != null)
			{
				this.Image.sprite = icon;
			}
			if (this.Image2 != null)
			{
				Sprite icon2 = TextureManager.container.GetGamepadIcon(this.button2);
				if (icon2 != null)
				{
					this.Image2.sprite = icon2;
				}
			}
			base.GetComponent<CanvasGroup>().alpha = 1f;
			base.GetComponent<LayoutElement>().ignoreLayout = false;
			yield break;
		}

		// Token: 0x0400D2B6 RID: 53942
		public ShortcutIcon.GamePadButton button;

		// Token: 0x0400D2B7 RID: 53943
		public ShortcutIcon.GamePadButton button2;

		// Token: 0x0400D2B8 RID: 53944
		public Image Image;

		// Token: 0x0400D2B9 RID: 53945
		public Image Image2;

		// Token: 0x0400D2BA RID: 53946
		private bool show = true;

		// Token: 0x0400D2BB RID: 53947
		private bool groupShow = true;

		// Token: 0x0200140D RID: 5133
		public enum GamePadButton
		{
			// Token: 0x0400D2BD RID: 53949
			ButtonSouth,
			// Token: 0x0400D2BE RID: 53950
			ButtonEast,
			// Token: 0x0400D2BF RID: 53951
			ButtonWest,
			// Token: 0x0400D2C0 RID: 53952
			ButtonNorth,
			// Token: 0x0400D2C1 RID: 53953
			LeftShoulder,
			// Token: 0x0400D2C2 RID: 53954
			RightShoulder,
			// Token: 0x0400D2C3 RID: 53955
			LeftTrigger,
			// Token: 0x0400D2C4 RID: 53956
			RightTrigger,
			// Token: 0x0400D2C5 RID: 53957
			LeftStick,
			// Token: 0x0400D2C6 RID: 53958
			RightStick,
			// Token: 0x0400D2C7 RID: 53959
			Select,
			// Token: 0x0400D2C8 RID: 53960
			Start
		}
	}
}
