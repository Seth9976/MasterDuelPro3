using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200137F RID: 4991
	[RequireComponent(typeof(ScrollRect))]
	public class HorizontalScrollbarMouseWheel : MonoBehaviour, IScrollHandler, IEventSystemHandler
	{
		// Token: 0x06009072 RID: 36978 RVA: 0x0013C256 File Offset: 0x0013A456
		private void Start()
		{
			this.scrollbar = base.GetComponent<ScrollRect>().horizontalScrollbar;
		}

		// Token: 0x06009073 RID: 36979 RVA: 0x0013C26C File Offset: 0x0013A46C
		public void OnScroll(PointerEventData eventData)
		{
			float scrollDelta = eventData.scrollDelta.y;
			if (scrollDelta < 0f)
			{
				this.scrollbar.value += this.scrollbar.size * this.scrollSensitivity;
				return;
			}
			if (scrollDelta > 0f)
			{
				this.scrollbar.value -= this.scrollbar.size * this.scrollSensitivity;
			}
		}

		// Token: 0x0400CF25 RID: 53029
		private Scrollbar scrollbar;

		// Token: 0x0400CF26 RID: 53030
		public float scrollSensitivity = 0.3f;
	}
}
