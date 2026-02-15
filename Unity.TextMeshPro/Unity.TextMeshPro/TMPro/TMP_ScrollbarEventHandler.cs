using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TMPro
{
	// Token: 0x0200006E RID: 110
	public class TMP_ScrollbarEventHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISelectHandler, IDeselectHandler
	{
		// Token: 0x0600034E RID: 846 RVA: 0x00012362 File Offset: 0x00010562
		public void OnPointerClick(PointerEventData eventData)
		{
			Debug.Log("Scrollbar click...");
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0001236E File Offset: 0x0001056E
		public void OnSelect(BaseEventData eventData)
		{
			Debug.Log("Scrollbar selected");
			this.isSelected = true;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00012381 File Offset: 0x00010581
		public void OnDeselect(BaseEventData eventData)
		{
			Debug.Log("Scrollbar De-Selected");
			this.isSelected = false;
		}

		// Token: 0x0400030C RID: 780
		public bool isSelected;
	}
}
