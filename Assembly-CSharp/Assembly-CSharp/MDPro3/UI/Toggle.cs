using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001383 RID: 4995
	public class Toggle : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler
	{
		// Token: 0x06009087 RID: 36999 RVA: 0x0013CCF4 File Offset: 0x0013AEF4
		private void Awake()
		{
			Button button = base.GetComponent<Button>();
			this.normalSprite = base.GetComponent<Image>().sprite;
			this.hoverSprite = button.spriteState.highlightedSprite;
			this.normalSprite2 = button.spriteState.selectedSprite;
			this.hoverSprite2 = button.spriteState.disabledSprite;
			if (this.label != null)
			{
				this.labelOffColor = this.label.color;
			}
			if (this.icon != null)
			{
				this.iconOffColor = this.icon.color;
			}
		}

		// Token: 0x06009088 RID: 37000 RVA: 0x0013CD93 File Offset: 0x0013AF93
		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.switchOn)
			{
				this.OnClickOff();
				return;
			}
			this.OnClickOn();
		}

		// Token: 0x06009089 RID: 37001 RVA: 0x0013CDAA File Offset: 0x0013AFAA
		public virtual void OnClickOn()
		{
			AudioManager.PlaySE("SE_MENU_S_DECIDE_01", 1f);
			this.SwitchOn();
		}

		// Token: 0x0600908A RID: 37002 RVA: 0x0013CDC1 File Offset: 0x0013AFC1
		public virtual void OnClickOff()
		{
			AudioManager.PlaySE("SE_MENU_S_DECIDE_02", 1f);
			this.SwitchOff();
		}

		// Token: 0x0600908B RID: 37003 RVA: 0x0013CDD8 File Offset: 0x0013AFD8
		public void OnPointerDown(PointerEventData eventData)
		{
			base.GetComponent<Image>().color = new Color(this.pressColor, this.pressColor, this.pressColor, 1f);
			if (this.label != null)
			{
				this.normalColor = this.label.color;
				this.label.color = new Color(this.normalColor.r * this.pressColor, this.normalColor.g * this.pressColor, this.normalColor.b * this.pressColor, 1f);
			}
			if (this.icon != null)
			{
				this.normalColor = this.icon.color;
				this.icon.color = new Color(this.normalColor.r * this.pressColor, this.normalColor.g * this.pressColor, this.normalColor.b * this.pressColor, 1f);
			}
		}

		// Token: 0x0600908C RID: 37004 RVA: 0x0013CEE0 File Offset: 0x0013B0E0
		public void OnPointerUp(PointerEventData eventData)
		{
			base.GetComponent<Image>().color = Color.white;
			if (this.label != null)
			{
				this.label.color = this.normalColor;
			}
			if (this.icon != null)
			{
				this.icon.color = this.normalColor;
			}
		}

		// Token: 0x0600908D RID: 37005 RVA: 0x0013CF3B File Offset: 0x0013B13B
		public virtual void SwitchOn()
		{
			this.SwitchOnWithoutAction();
		}

		// Token: 0x0600908E RID: 37006 RVA: 0x0013CF44 File Offset: 0x0013B144
		public void SwitchOnWithoutAction()
		{
			this.switchOn = true;
			if (this.normalSprite == null)
			{
				this.Awake();
			}
			SpriteState state = base.GetComponent<Button>().spriteState;
			base.GetComponent<Image>().sprite = this.normalSprite2;
			state.highlightedSprite = this.hoverSprite2;
			state.pressedSprite = this.hoverSprite2;
			base.GetComponent<Button>().spriteState = state;
			if (this.label != null)
			{
				this.label.color = this.labelOnColor;
			}
			if (this.icon != null)
			{
				this.icon.color = this.iconOnColor;
			}
		}

		// Token: 0x0600908F RID: 37007 RVA: 0x0013CFED File Offset: 0x0013B1ED
		public virtual void SwitchOff()
		{
			this.SwitchOffWithoutAction();
		}

		// Token: 0x06009090 RID: 37008 RVA: 0x0013CFF8 File Offset: 0x0013B1F8
		public void SwitchOffWithoutAction()
		{
			this.switchOn = false;
			if (this.normalSprite == null)
			{
				this.Awake();
			}
			SpriteState state = base.GetComponent<Button>().spriteState;
			base.GetComponent<Image>().sprite = this.normalSprite;
			state.highlightedSprite = this.hoverSprite;
			state.pressedSprite = this.hoverSprite;
			base.GetComponent<Button>().spriteState = state;
			if (this.label != null)
			{
				this.label.color = this.labelOffColor;
			}
			if (this.icon != null)
			{
				this.icon.color = this.iconOffColor;
			}
		}

		// Token: 0x0400CF38 RID: 53048
		private Sprite normalSprite;

		// Token: 0x0400CF39 RID: 53049
		private Sprite hoverSprite;

		// Token: 0x0400CF3A RID: 53050
		private Sprite normalSprite2;

		// Token: 0x0400CF3B RID: 53051
		private Sprite hoverSprite2;

		// Token: 0x0400CF3C RID: 53052
		public bool switchOn;

		// Token: 0x0400CF3D RID: 53053
		public Text label;

		// Token: 0x0400CF3E RID: 53054
		public Image icon;

		// Token: 0x0400CF3F RID: 53055
		private Color normalColor;

		// Token: 0x0400CF40 RID: 53056
		private Color labelOffColor;

		// Token: 0x0400CF41 RID: 53057
		public Color labelOnColor;

		// Token: 0x0400CF42 RID: 53058
		private Color iconOffColor;

		// Token: 0x0400CF43 RID: 53059
		public Color iconOnColor;

		// Token: 0x0400CF44 RID: 53060
		public float pressColor = 0.5f;
	}
}
