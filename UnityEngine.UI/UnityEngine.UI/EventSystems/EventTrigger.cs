using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UnityEngine.EventSystems
{
	// Token: 0x020000B3 RID: 179
	[AddComponentMenu("Event/Event Trigger")]
	public class EventTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IUpdateSelectedHandler, ISelectHandler, IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00019B6F File Offset: 0x00017D6F
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x00019B77 File Offset: 0x00017D77
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use triggers instead (UnityUpgradable) -> triggers", true)]
		public List<EventTrigger.Entry> delegates
		{
			get
			{
				return this.triggers;
			}
			set
			{
				this.triggers = value;
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0000520E File Offset: 0x0000340E
		protected EventTrigger()
		{
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00019B80 File Offset: 0x00017D80
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x00019B9B File Offset: 0x00017D9B
		public List<EventTrigger.Entry> triggers
		{
			get
			{
				if (this.m_Delegates == null)
				{
					this.m_Delegates = new List<EventTrigger.Entry>();
				}
				return this.m_Delegates;
			}
			set
			{
				this.m_Delegates = value;
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00019BA4 File Offset: 0x00017DA4
		private void Execute(EventTriggerType id, BaseEventData eventData)
		{
			for (int i = 0; i < this.triggers.Count; i++)
			{
				EventTrigger.Entry ent = this.triggers[i];
				if (ent.eventID == id && ent.callback != null)
				{
					ent.callback.Invoke(eventData);
				}
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00019BF1 File Offset: 0x00017DF1
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerEnter, eventData);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00019BFB File Offset: 0x00017DFB
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerExit, eventData);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00019C05 File Offset: 0x00017E05
		public virtual void OnDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drag, eventData);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00019C0F File Offset: 0x00017E0F
		public virtual void OnDrop(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drop, eventData);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00019C19 File Offset: 0x00017E19
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerDown, eventData);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00019C23 File Offset: 0x00017E23
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerUp, eventData);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00019C2D File Offset: 0x00017E2D
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerClick, eventData);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00019C37 File Offset: 0x00017E37
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Select, eventData);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00019C42 File Offset: 0x00017E42
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Deselect, eventData);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00019C4D File Offset: 0x00017E4D
		public virtual void OnScroll(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Scroll, eventData);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00019C57 File Offset: 0x00017E57
		public virtual void OnMove(AxisEventData eventData)
		{
			this.Execute(EventTriggerType.Move, eventData);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00019C62 File Offset: 0x00017E62
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.UpdateSelected, eventData);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00019C6C File Offset: 0x00017E6C
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.InitializePotentialDrag, eventData);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00019C77 File Offset: 0x00017E77
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.BeginDrag, eventData);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00019C82 File Offset: 0x00017E82
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.EndDrag, eventData);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00019C8D File Offset: 0x00017E8D
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Submit, eventData);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00019C98 File Offset: 0x00017E98
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Cancel, eventData);
		}

		// Token: 0x040002EE RID: 750
		[FormerlySerializedAs("delegates")]
		[SerializeField]
		private List<EventTrigger.Entry> m_Delegates;

		// Token: 0x020000B4 RID: 180
		[Serializable]
		public class TriggerEvent : UnityEvent<BaseEventData>
		{
		}

		// Token: 0x020000B5 RID: 181
		[Serializable]
		public class Entry
		{
			// Token: 0x040002EF RID: 751
			public EventTriggerType eventID = EventTriggerType.PointerClick;

			// Token: 0x040002F0 RID: 752
			public EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent();
		}
	}
}
