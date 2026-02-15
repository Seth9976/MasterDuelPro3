using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200000E RID: 14
public class DoWhenOnDrag : MonoBehaviour, IDragHandler, IEventSystemHandler
{
	// Token: 0x0600002E RID: 46 RVA: 0x00002991 File Offset: 0x00000B91
	public void OnDrag(PointerEventData eventData)
	{
		Action action = this.action;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x04000026 RID: 38
	public Action action;
}
