using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200000D RID: 13
public class EventSystemTest : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISelectHandler
{
	// Token: 0x0600002B RID: 43 RVA: 0x00002979 File Offset: 0x00000B79
	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Clicked");
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002985 File Offset: 0x00000B85
	public void OnSelect(BaseEventData eventData)
	{
		Debug.Log("Selected");
	}
}
