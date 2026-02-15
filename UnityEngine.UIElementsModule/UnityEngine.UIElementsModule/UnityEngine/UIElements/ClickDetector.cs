using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004D RID: 77
	internal class ClickDetector
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000B6E6 File Offset: 0x000098E6
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000B6ED File Offset: 0x000098ED
		internal static int s_DoubleClickTime { get; set; } = -1;

		// Token: 0x06000255 RID: 597 RVA: 0x0000B6F8 File Offset: 0x000098F8
		public ClickDetector()
		{
			this.m_ClickStatus = new List<ClickDetector.ButtonClickStatus>(PointerId.maxPointers);
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				this.m_ClickStatus.Add(new ClickDetector.ButtonClickStatus());
			}
			bool flag = ClickDetector.s_DoubleClickTime == -1;
			if (flag)
			{
				ClickDetector.s_DoubleClickTime = Event.GetDoubleClickTime();
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B760 File Offset: 0x00009960
		private void StartClickTracking(EventBase evt)
		{
			IPointerEvent pe = evt as IPointerEvent;
			bool flag = pe == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus clickStatus = this.m_ClickStatus[pe.pointerId];
				VisualElement newTarget = evt.elementTarget;
				bool flag2 = newTarget != clickStatus.m_Target;
				if (flag2)
				{
					clickStatus.Reset();
				}
				clickStatus.m_Target = newTarget;
				bool flag3 = evt.timestamp - clickStatus.m_LastPointerDownTime > (long)ClickDetector.s_DoubleClickTime;
				if (flag3)
				{
					clickStatus.m_ClickCount = 1;
				}
				else
				{
					clickStatus.m_ClickCount++;
				}
				clickStatus.m_LastPointerDownTime = evt.timestamp;
				clickStatus.m_PointerDownPosition = pe.position;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B80C File Offset: 0x00009A0C
		private void SendClickEvent(EventBase evt)
		{
			IPointerEvent pe = evt as IPointerEvent;
			bool flag = pe == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus clickStatus = this.m_ClickStatus[pe.pointerId];
				VisualElement element = evt.elementTarget;
				bool flag2 = element != null && ClickDetector.ContainsPointer(element, pe.position);
				if (flag2)
				{
					bool flag3 = clickStatus.m_Target != null && clickStatus.m_ClickCount > 0;
					if (flag3)
					{
						VisualElement target = clickStatus.m_Target.FindCommonAncestor(evt.elementTarget);
						bool flag4 = target != null;
						if (flag4)
						{
							using (ClickEvent clickEvent = ClickEvent.GetPooled(pe, clickStatus.m_ClickCount))
							{
								clickEvent.elementTarget = target;
								target.SendEvent(clickEvent);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		private void CancelClickTracking(EventBase evt)
		{
			IPointerEvent pe = evt as IPointerEvent;
			bool flag = pe == null;
			if (!flag)
			{
				ClickDetector.ButtonClickStatus clickStatus = this.m_ClickStatus[pe.pointerId];
				clickStatus.Reset();
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B924 File Offset: 0x00009B24
		public void ProcessEvent<TEvent>(PointerEventBase<TEvent> evt) where TEvent : PointerEventBase<TEvent>, new()
		{
			bool flag = evt.eventTypeId == EventBase<PointerDownEvent>.TypeId() && evt.button == 0;
			if (flag)
			{
				this.StartClickTracking(evt);
			}
			else
			{
				bool flag2 = evt.eventTypeId == EventBase<PointerMoveEvent>.TypeId();
				if (flag2)
				{
					bool flag3 = evt.button == 0 && (evt.pressedButtons & 1) == 1;
					if (flag3)
					{
						this.StartClickTracking(evt);
					}
					else
					{
						bool flag4 = evt.button == 0 && (evt.pressedButtons & 1) == 0;
						if (flag4)
						{
							this.SendClickEvent(evt);
						}
						else
						{
							ClickDetector.ButtonClickStatus clickStatus = this.m_ClickStatus[evt.pointerId];
							bool flag5 = clickStatus.m_Target != null;
							if (flag5)
							{
								clickStatus.m_LastPointerDownTime = 0L;
							}
						}
					}
				}
				else
				{
					bool flag6 = evt.eventTypeId == EventBase<PointerCancelEvent>.TypeId();
					if (flag6)
					{
						this.CancelClickTracking(evt);
					}
					else
					{
						bool flag7 = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId() && evt.button == 0;
						if (flag7)
						{
							this.SendClickEvent(evt);
						}
					}
				}
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000BA3C File Offset: 0x00009C3C
		private static bool ContainsPointer(VisualElement element, Vector2 position)
		{
			bool flag = !element.worldBound.Contains(position) || element.panel == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				VisualElement elementUnderPointer = element.panel.Pick(position);
				flag2 = element == elementUnderPointer || element.Contains(elementUnderPointer);
			}
			return flag2;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000BA90 File Offset: 0x00009C90
		internal void Cleanup(List<VisualElement> elements)
		{
			foreach (ClickDetector.ButtonClickStatus status in this.m_ClickStatus)
			{
				bool flag = status.m_Target == null;
				if (!flag)
				{
					bool flag2 = elements.Contains(status.m_Target);
					if (flag2)
					{
						status.Reset();
					}
				}
			}
		}

		// Token: 0x0400017A RID: 378
		private List<ClickDetector.ButtonClickStatus> m_ClickStatus;

		// Token: 0x0200004E RID: 78
		private class ButtonClickStatus
		{
			// Token: 0x0600025D RID: 605 RVA: 0x0000BB10 File Offset: 0x00009D10
			public void Reset()
			{
				this.m_Target = null;
				this.m_ClickCount = 0;
				this.m_LastPointerDownTime = 0L;
				this.m_PointerDownPosition = Vector3.zero;
			}

			// Token: 0x0400017C RID: 380
			public VisualElement m_Target;

			// Token: 0x0400017D RID: 381
			public Vector3 m_PointerDownPosition;

			// Token: 0x0400017E RID: 382
			public long m_LastPointerDownTime;

			// Token: 0x0400017F RID: 383
			public int m_ClickCount;
		}
	}
}
