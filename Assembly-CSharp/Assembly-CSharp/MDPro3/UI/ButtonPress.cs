using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200137B RID: 4987
	public class ButtonPress : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x06009058 RID: 36952 RVA: 0x0013B834 File Offset: 0x00139A34
		private void Awake()
		{
			this.imageColor = base.GetComponent<Image>().color;
			if (this.icon != null)
			{
				this.iconColor = this.icon.color;
			}
			if (this.text != null)
			{
				this.textColor = this.text.color;
			}
		}

		// Token: 0x06009059 RID: 36953 RVA: 0x0013B890 File Offset: 0x00139A90
		public void OnPointerDown(PointerEventData eventData)
		{
			base.GetComponent<Image>().color = new Color(this.imageColor.r * this.pressColor * this.m_disableColor, this.imageColor.g * this.pressColor * this.m_disableColor, this.imageColor.b * this.pressColor * this.m_disableColor, this.imageColor.a);
			if (this.icon != null && this.icon.color != Color.black)
			{
				this.icon.color = new Color(this.iconColor.r * this.pressColor * this.m_disableColor, this.iconColor.g * this.pressColor * this.m_disableColor, this.iconColor.b * this.pressColor * this.m_disableColor, this.iconColor.a);
			}
			if (this.text != null)
			{
				this.text.color = new Color(this.textColor.r * this.pressColor * this.m_disableColor, this.textColor.g * this.pressColor * this.m_disableColor, this.textColor.b * this.pressColor * this.m_disableColor, this.textColor.a);
			}
		}

		// Token: 0x0600905A RID: 36954 RVA: 0x0013BA04 File Offset: 0x00139C04
		public void OnPointerUp(PointerEventData eventData)
		{
			base.GetComponent<Image>().color = new Color(this.imageColor.r * this.m_disableColor, this.imageColor.g * this.m_disableColor, this.imageColor.b * this.m_disableColor, this.imageColor.a);
			if (this.icon != null && this.icon.color != Color.black)
			{
				this.icon.color = new Color(this.iconColor.r * this.m_disableColor, this.iconColor.g * this.m_disableColor, this.iconColor.b * this.m_disableColor, this.iconColor.a);
			}
			if (this.text != null)
			{
				this.text.color = new Color(this.textColor.r * this.m_disableColor, this.textColor.g * this.m_disableColor, this.textColor.b * this.m_disableColor, this.textColor.a);
			}
		}

		// Token: 0x0600905B RID: 36955 RVA: 0x0013BB38 File Offset: 0x00139D38
		public void SetInteractable(bool interactable)
		{
			Selectable selectable = base.GetComponent<Selectable>();
			if (selectable == null)
			{
				return;
			}
			selectable.interactable = interactable;
			if (interactable)
			{
				this.m_disableColor = 1f;
			}
			else
			{
				this.m_disableColor = this.disableColor;
			}
			this.OnPointerUp(null);
		}

		// Token: 0x0400CEFE RID: 52990
		private Color imageColor;

		// Token: 0x0400CEFF RID: 52991
		private Color iconColor;

		// Token: 0x0400CF00 RID: 52992
		private Color textColor;

		// Token: 0x0400CF01 RID: 52993
		public Image icon;

		// Token: 0x0400CF02 RID: 52994
		public Text text;

		// Token: 0x0400CF03 RID: 52995
		public float pressColor = 0.5f;

		// Token: 0x0400CF04 RID: 52996
		public float disableColor = 0.5f;

		// Token: 0x0400CF05 RID: 52997
		private float m_disableColor = 1f;
	}
}
