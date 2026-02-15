using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200000F RID: 15
public class UIClickHandler : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000030 RID: 48 RVA: 0x000029A3 File Offset: 0x00000BA3
	public void OnPointerDown(PointerEventData eventData)
	{
		Action action = this.action;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x04000027 RID: 39
	public Action action;
}
