using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x02001377 RID: 4983
	public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x06009047 RID: 36935 RVA: 0x0013B571 File Offset: 0x00139771
		public void OnPointerEnter(PointerEventData eventData)
		{
			Action action = this.hoverIn;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06009048 RID: 36936 RVA: 0x0013B583 File Offset: 0x00139783
		public void OnPointerExit(PointerEventData eventData)
		{
			Action action = this.hoverOut;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0400CEF1 RID: 52977
		public Action hoverIn;

		// Token: 0x0400CEF2 RID: 52978
		public Action hoverOut;
	}
}
