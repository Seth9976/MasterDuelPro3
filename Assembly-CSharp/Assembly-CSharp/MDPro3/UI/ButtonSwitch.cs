using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200137D RID: 4989
	public class ButtonSwitch : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler
	{
		// Token: 0x06009062 RID: 36962 RVA: 0x0013BC18 File Offset: 0x00139E18
		private void Start()
		{
			this.normalSprite = base.GetComponent<Image>().sprite;
			this.hoverSprite = base.GetComponent<Button>().spriteState.highlightedSprite;
			this.selectedSprite = base.GetComponent<Button>().spriteState.selectedSprite;
			if (this.onObj != null)
			{
				this.onObj.SetActive(false);
			}
		}

		// Token: 0x06009063 RID: 36963 RVA: 0x0013BC82 File Offset: 0x00139E82
		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.switchOn)
			{
				this.OnSwitchOff();
				return;
			}
			this.OnSwitchOn();
		}

		// Token: 0x06009064 RID: 36964 RVA: 0x0013BC9C File Offset: 0x00139E9C
		public void OnPointerDown(PointerEventData eventData)
		{
			base.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
			if (this.onObj != null)
			{
				Image imageOn = this.onObj.GetComponent<Image>();
				if (imageOn != null)
				{
					this.onColor = imageOn.color;
					imageOn.color = new Color(this.onColor.r * 0.5f, this.onColor.g * 0.5f, this.onColor.b * 0.5f, this.onColor.a);
				}
				Text textOn = this.onObj.GetComponent<Text>();
				if (textOn != null)
				{
					this.onColor = textOn.color;
					textOn.color = new Color(this.onColor.r * 0.5f, this.onColor.g * 0.5f, this.onColor.b * 0.5f, this.onColor.a);
				}
			}
			if (this.offObj != null)
			{
				Image imageOff = this.offObj.GetComponent<Image>();
				if (imageOff != null)
				{
					this.offColor = imageOff.color;
					imageOff.color = new Color(this.offColor.r * 0.5f, this.offColor.g * 0.5f, this.offColor.b * 0.5f, this.offColor.a);
				}
				Text textOff = this.onObj.GetComponent<Text>();
				if (textOff != null)
				{
					this.offColor = textOff.color;
					textOff.color = new Color(this.offColor.r * 0.5f, this.offColor.g * 0.5f, this.offColor.b * 0.5f, this.offColor.a);
				}
			}
		}

		// Token: 0x06009065 RID: 36965 RVA: 0x0013BE98 File Offset: 0x0013A098
		public void OnPointerUp(PointerEventData eventData)
		{
			base.GetComponent<Image>().color = Color.white;
			if (this.onObj != null)
			{
				Image imageOn = this.onObj.GetComponent<Image>();
				if (imageOn != null)
				{
					imageOn.color = this.onColor;
				}
				Text textOn = this.onObj.GetComponent<Text>();
				if (textOn != null)
				{
					textOn.color = this.onColor;
				}
			}
			if (this.offObj != null)
			{
				Image imageOff = this.offObj.GetComponent<Image>();
				if (imageOff != null)
				{
					imageOff.color = this.offColor;
				}
				Text textOff = this.offObj.GetComponent<Text>();
				if (textOff != null)
				{
					textOff.color = this.offColor;
				}
			}
		}

		// Token: 0x06009066 RID: 36966 RVA: 0x0013BF58 File Offset: 0x0013A158
		public virtual void OnSwitchOn()
		{
			this.switchOn = true;
			if (this.onObj != null)
			{
				this.onObj.SetActive(true);
			}
			if (this.offObj != null)
			{
				this.offObj.SetActive(false);
			}
			base.GetComponent<Image>().sprite = this.selectedSprite;
			SpriteState state = base.GetComponent<Button>().spriteState;
			state.highlightedSprite = this.selectedSprite;
			state.pressedSprite = this.selectedSprite;
			base.GetComponent<Button>().spriteState = state;
			if (this.text != null)
			{
				this.text.color = this.textOnColor;
			}
			Action action = this.onAction;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06009067 RID: 36967 RVA: 0x0013C014 File Offset: 0x0013A214
		public virtual void OnSwitchOff()
		{
			this.switchOn = false;
			if (this.onObj != null)
			{
				this.onObj.SetActive(false);
			}
			if (this.offObj != null)
			{
				this.offObj.SetActive(true);
			}
			base.GetComponent<Image>().sprite = this.normalSprite;
			SpriteState state = base.GetComponent<Button>().spriteState;
			state.highlightedSprite = this.hoverSprite;
			state.pressedSprite = this.hoverSprite;
			base.GetComponent<Button>().spriteState = state;
			if (this.text != null)
			{
				this.text.color = this.textOffColor;
			}
			Action action = this.offAction;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0400CF0A RID: 53002
		private bool switchOn;

		// Token: 0x0400CF0B RID: 53003
		private Sprite selectedSprite;

		// Token: 0x0400CF0C RID: 53004
		private Sprite normalSprite;

		// Token: 0x0400CF0D RID: 53005
		private Sprite hoverSprite;

		// Token: 0x0400CF0E RID: 53006
		public GameObject onObj;

		// Token: 0x0400CF0F RID: 53007
		public GameObject offObj;

		// Token: 0x0400CF10 RID: 53008
		private Color offColor;

		// Token: 0x0400CF11 RID: 53009
		private Color onColor;

		// Token: 0x0400CF12 RID: 53010
		public Text text;

		// Token: 0x0400CF13 RID: 53011
		public Color textOnColor = Color.black;

		// Token: 0x0400CF14 RID: 53012
		public Color textOffColor = Color.white;

		// Token: 0x0400CF15 RID: 53013
		public Action onAction;

		// Token: 0x0400CF16 RID: 53014
		public Action offAction;
	}
}
