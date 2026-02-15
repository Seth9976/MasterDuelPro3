using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001378 RID: 4984
	public class ButtonList : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		// Token: 0x0600904A RID: 36938 RVA: 0x0013B595 File Offset: 0x00139795
		private void Awake()
		{
			if (this.text != null)
			{
				this.textColor = this.text.color;
			}
		}

		// Token: 0x0600904B RID: 36939 RVA: 0x0013B5B6 File Offset: 0x001397B6
		public void OnPointerClick(PointerEventData eventData)
		{
			if (!this.selected)
			{
				this.selected = true;
				this.SelectThis();
			}
		}

		// Token: 0x0600904C RID: 36940 RVA: 0x0013B5CD File Offset: 0x001397CD
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!this.selected)
			{
				base.GetComponent<Image>().sprite = this.hoverSprite;
			}
		}

		// Token: 0x0600904D RID: 36941 RVA: 0x0013B5E8 File Offset: 0x001397E8
		public void OnPointerExit(PointerEventData eventData)
		{
			if (!this.selected)
			{
				base.GetComponent<Image>().sprite = this.normalSprite;
			}
		}

		// Token: 0x0600904E RID: 36942 RVA: 0x0013B604 File Offset: 0x00139804
		public virtual void SelectThis()
		{
			this.selected = true;
			base.GetComponent<Image>().sprite = this.selectedSprite;
			if (this.text != null)
			{
				this.text.color = Color.black;
			}
			if (this.scrollRect != null)
			{
				this.scrollRect.gameObject.SetActive(true);
				this.scrollRect.normalizedPosition = new Vector2(0f, 1f);
			}
			foreach (ButtonList btn in base.transform.parent.GetComponentsInChildren<ButtonList>(true))
			{
				if (btn != this)
				{
					btn.UnselectThis();
				}
			}
		}

		// Token: 0x0600904F RID: 36943 RVA: 0x0013B6B4 File Offset: 0x001398B4
		public void UnselectThis()
		{
			this.selected = false;
			base.GetComponent<Image>().sprite = this.normalSprite;
			if (this.text != null)
			{
				this.text.color = Color.white;
			}
			if (this.scrollRect != null)
			{
				this.scrollRect.gameObject.SetActive(false);
			}
		}

		// Token: 0x06009050 RID: 36944 RVA: 0x0013B718 File Offset: 0x00139918
		public void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left && ((this.text != null) & !this.selected))
			{
				this.text.color = new Color(this.textColor.r * this.pressColor, this.textColor.g * this.pressColor, this.textColor.b * this.pressColor, 1f);
			}
		}

		// Token: 0x06009051 RID: 36945 RVA: 0x0013B790 File Offset: 0x00139990
		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left && this.text != null && !this.selected)
			{
				this.text.color = this.textColor;
			}
		}

		// Token: 0x0400CEF3 RID: 52979
		public Sprite normalSprite;

		// Token: 0x0400CEF4 RID: 52980
		public Sprite hoverSprite;

		// Token: 0x0400CEF5 RID: 52981
		public Sprite selectedSprite;

		// Token: 0x0400CEF6 RID: 52982
		public bool defaultButton;

		// Token: 0x0400CEF7 RID: 52983
		public ScrollRect scrollRect;

		// Token: 0x0400CEF8 RID: 52984
		public Text text;

		// Token: 0x0400CEF9 RID: 52985
		private bool selected;

		// Token: 0x0400CEFA RID: 52986
		private Color textColor;

		// Token: 0x0400CEFB RID: 52987
		public float pressColor = 0.5f;
	}
}
