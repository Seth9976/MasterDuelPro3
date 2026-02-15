using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x02001372 RID: 4978
	public class HintAutoMove : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
	{
		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06009026 RID: 36902 RVA: 0x0013AC60 File Offset: 0x00138E60
		private RectTransform Rect
		{
			get
			{
				return this.m_Rect = ((this.m_Rect != null) ? this.m_Rect : base.GetComponent<RectTransform>());
			}
		}

		// Token: 0x06009027 RID: 36903 RVA: 0x0013AC94 File Offset: 0x00138E94
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.top)
			{
				this.top = false;
				this.Rect.anchoredPosition = new Vector2(0f, -280f);
				return;
			}
			this.top = true;
			this.Rect.anchoredPosition = new Vector2(0f, 280f);
		}

		// Token: 0x0400CEC4 RID: 52932
		private bool top;

		// Token: 0x0400CEC5 RID: 52933
		private RectTransform m_Rect;
	}
}
