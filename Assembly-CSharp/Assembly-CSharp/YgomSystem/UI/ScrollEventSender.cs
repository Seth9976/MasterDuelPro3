using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x020005C1 RID: 1473
	public class ScrollEventSender : MonoBehaviour, IInitializePotentialDragHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
	{
		// Token: 0x06002E55 RID: 11861 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnInitializePotentialDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnScroll(PointerEventData eventData)
		{
		}

		// Token: 0x04002C04 RID: 11268
		[SerializeField]
		private ScrollRect m_TargetScrollRect;
	}
}
