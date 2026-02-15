using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x02001376 RID: 4982
	public class ButtonHold : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x06009044 RID: 36932 RVA: 0x0013B53D File Offset: 0x0013973D
		public void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				UnityEvent unityEvent = this.onPointDown;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
			}
		}

		// Token: 0x06009045 RID: 36933 RVA: 0x0013B557 File Offset: 0x00139757
		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				UnityEvent unityEvent = this.onPointUp;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
			}
		}

		// Token: 0x0400CEEF RID: 52975
		public UnityEvent onPointDown;

		// Token: 0x0400CEF0 RID: 52976
		public UnityEvent onPointUp;
	}
}
