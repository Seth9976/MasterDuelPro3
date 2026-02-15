using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001381 RID: 4993
	public class Tab : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerClickHandler
	{
		// Token: 0x0600907A RID: 36986 RVA: 0x0013C58C File Offset: 0x0013A78C
		private void Awake()
		{
			this.normalSprite = base.GetComponent<Image>().sprite;
			this.hoverSprite = base.GetComponent<Button>().spriteState.highlightedSprite;
			this.selectedSprite = base.GetComponent<Button>().spriteState.selectedSprite;
		}

		// Token: 0x0600907B RID: 36987 RVA: 0x0013C5DC File Offset: 0x0013A7DC
		private void Start()
		{
			if (this.id == 1)
			{
				this.TabThis();
			}
		}

		// Token: 0x0600907C RID: 36988 RVA: 0x0013C5F0 File Offset: 0x0013A7F0
		public void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				base.GetComponent<Image>().color = new Color(this.pressColor, this.pressColor, this.pressColor, 1f);
				this.iconColor = this.icon.color;
				this.icon.color = new Color(this.iconColor.r * this.pressColor, this.iconColor.g * this.pressColor, this.iconColor.b * this.pressColor, 1f);
			}
		}

		// Token: 0x0600907D RID: 36989 RVA: 0x0013C68B File Offset: 0x0013A88B
		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				base.GetComponent<Image>().color = Color.white;
				this.icon.color = this.iconColor;
			}
		}

		// Token: 0x0600907E RID: 36990 RVA: 0x0013C6B6 File Offset: 0x0013A8B6
		public void OnPointerClick(PointerEventData eventData)
		{
			AudioManager.PlaySE("SE_MENU_SELECT_01", 1f);
			if (!this.selected)
			{
				this.TabThis();
			}
		}

		// Token: 0x0600907F RID: 36991 RVA: 0x0013C6D8 File Offset: 0x0013A8D8
		public void TabThis()
		{
			this.selected = true;
			base.transform.parent.GetComponent<Tabs>().Tab(this);
			SpriteState state = base.GetComponent<Button>().spriteState;
			base.GetComponent<Image>().sprite = this.selectedSprite;
			state.highlightedSprite = this.selectedSprite;
			state.pressedSprite = this.selectedSprite;
			base.GetComponent<Button>().spriteState = state;
			this.icon.color = this.iconOnColor;
			this.text.color = this.textOnColor;
			this.AdjustSize();
			Action action = this.onSelected;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06009080 RID: 36992 RVA: 0x0013C780 File Offset: 0x0013A980
		public void CancelSelect()
		{
			this.selected = false;
			SpriteState state = base.GetComponent<Button>().spriteState;
			base.GetComponent<Image>().sprite = this.normalSprite;
			state.highlightedSprite = this.hoverSprite;
			state.pressedSprite = this.hoverSprite;
			base.GetComponent<Button>().spriteState = state;
			this.icon.color = this.iconOffColor;
			this.text.color = this.textOffColor;
			this.AdjustSize();
		}

		// Token: 0x06009081 RID: 36993 RVA: 0x0013C800 File Offset: 0x0013AA00
		public void AdjustSize()
		{
			float parentWidth = base.transform.parent.GetComponent<RectTransform>().sizeDelta.x;
			if (this.id == 1)
			{
				if (this.selected)
				{
					base.GetComponent<RectTransform>().sizeDelta = new Vector2((parentWidth - 20f) / 2f, 56f);
				}
				else
				{
					base.GetComponent<RectTransform>().sizeDelta = new Vector2((parentWidth - 20f) / 4f, 56f);
				}
			}
			else if (this.id == 2)
			{
				if (this.selected)
				{
					base.GetComponent<RectTransform>().anchoredPosition = new Vector2((parentWidth - 20f) / 4f + 10f, 0f);
					base.GetComponent<RectTransform>().sizeDelta = new Vector2((parentWidth - 20f) / 2f, 56f);
				}
				else
				{
					base.GetComponent<RectTransform>().sizeDelta = new Vector2((parentWidth - 20f) / 4f, 56f);
					if (base.transform.parent.GetComponent<Tabs>().tabs[0].selected)
					{
						base.GetComponent<RectTransform>().anchoredPosition = new Vector2((parentWidth - 20f) / 2f + 10f, 0f);
					}
					else
					{
						base.GetComponent<RectTransform>().anchoredPosition = new Vector2((parentWidth - 20f) / 4f + 10f, 0f);
					}
				}
			}
			else if (this.id == 3)
			{
				if (this.selected)
				{
					base.GetComponent<RectTransform>().sizeDelta = new Vector2((parentWidth - 20f) / 2f, 56f);
				}
				else
				{
					base.GetComponent<RectTransform>().sizeDelta = new Vector2((parentWidth - 20f) / 4f, 56f);
				}
			}
			float width = base.GetComponent<RectTransform>().sizeDelta.x;
			float iconWidth = this.icon.GetComponent<RectTransform>().sizeDelta.x;
			if (!this.selected)
			{
				this.icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(width / 2f, 0f);
				return;
			}
			if (width > iconWidth + 110f)
			{
				float side = width - iconWidth - 110f;
				this.icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(side / 2f + iconWidth / 2f, 0f);
				this.text.GetComponent<RectTransform>().anchoredPosition = new Vector2(side / 2f + iconWidth + 10f + 50f, 0f);
				this.icon.color = this.iconOnColor;
				this.text.color = this.textOnColor;
				return;
			}
			if (width > 100f)
			{
				this.icon.color = new Color(0f, 0f, 0f, 0f);
				this.text.color = this.textOnColor;
				this.text.GetComponent<RectTransform>().anchoredPosition = new Vector2(width / 2f, 0f);
				return;
			}
			this.icon.color = this.iconOnColor;
			this.text.color = new Color(0f, 0f, 0f, 0f);
			this.icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(width / 2f, 0f);
		}

		// Token: 0x0400CF29 RID: 53033
		private Sprite normalSprite;

		// Token: 0x0400CF2A RID: 53034
		private Sprite hoverSprite;

		// Token: 0x0400CF2B RID: 53035
		private Sprite selectedSprite;

		// Token: 0x0400CF2C RID: 53036
		public int id;

		// Token: 0x0400CF2D RID: 53037
		public float pressColor = 0.5f;

		// Token: 0x0400CF2E RID: 53038
		public bool selected;

		// Token: 0x0400CF2F RID: 53039
		public Action onSelected;

		// Token: 0x0400CF30 RID: 53040
		public Image icon;

		// Token: 0x0400CF31 RID: 53041
		public Text text;

		// Token: 0x0400CF32 RID: 53042
		public Color iconOffColor = Color.white;

		// Token: 0x0400CF33 RID: 53043
		public Color iconOnColor = Color.black;

		// Token: 0x0400CF34 RID: 53044
		public Color textOffColor = new Color(0f, 0f, 0f, 0f);

		// Token: 0x0400CF35 RID: 53045
		public Color textOnColor = Color.black;

		// Token: 0x0400CF36 RID: 53046
		private Color iconColor;
	}
}
