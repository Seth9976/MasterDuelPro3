using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C2 RID: 450
	internal class ElementUnderPointer
	{
		// Token: 0x06000C9C RID: 3228 RVA: 0x0003C210 File Offset: 0x0003A410
		internal VisualElement GetTopElementUnderPointer(int pointerId, out Vector2 pickPosition, out bool isTemporary)
		{
			pickPosition = this.m_PickingPointerPositions[pointerId];
			isTemporary = this.m_IsPickingPointerTemporaries[pointerId];
			return this.m_PendingTopElementUnderPointer[pointerId];
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0003C248 File Offset: 0x0003A448
		internal VisualElement GetTopElementUnderPointer(int pointerId)
		{
			return this.m_PendingTopElementUnderPointer[pointerId];
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0003C264 File Offset: 0x0003A464
		internal void SetElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, Vector2 pointerPos)
		{
			Debug.Assert(pointerId >= 0, "SetElementUnderPointer expects pointerId >= 0");
			VisualElement previousTopElementUnderPointer = this.m_TopElementUnderPointer[pointerId];
			this.m_IsPickingPointerTemporaries[pointerId] = false;
			this.m_PickingPointerPositions[pointerId] = pointerPos;
			bool flag = newElementUnderPointer == previousTopElementUnderPointer;
			if (!flag)
			{
				this.m_PendingTopElementUnderPointer[pointerId] = newElementUnderPointer;
				this.m_TriggerEvent[pointerId] = null;
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0003C2C0 File Offset: 0x0003A4C0
		private Vector2 GetEventPointerPosition(EventBase triggerEvent)
		{
			IPointerEvent pointerEvent = triggerEvent as IPointerEvent;
			bool flag = pointerEvent != null;
			Vector2 vector;
			if (flag)
			{
				vector = new Vector2(pointerEvent.position.x, pointerEvent.position.y);
			}
			else
			{
				IMouseEvent mouseEvt = triggerEvent as IMouseEvent;
				bool flag2 = mouseEvt != null;
				if (flag2)
				{
					vector = mouseEvt.mousePosition;
				}
				else
				{
					vector = new Vector2(float.MinValue, float.MinValue);
				}
			}
			return vector;
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0003C32A File Offset: 0x0003A52A
		internal void SetTemporaryElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, EventBase triggerEvent)
		{
			this.SetElementUnderPointer(newElementUnderPointer, pointerId, triggerEvent, true);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0003C338 File Offset: 0x0003A538
		internal void SetElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, EventBase triggerEvent)
		{
			this.SetElementUnderPointer(newElementUnderPointer, pointerId, triggerEvent, false);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0003C348 File Offset: 0x0003A548
		private void SetElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, EventBase triggerEvent, bool temporary)
		{
			Debug.Assert(pointerId >= 0, "SetElementUnderPointer expects pointerId >= 0");
			this.m_IsPickingPointerTemporaries[pointerId] = temporary;
			this.m_PickingPointerPositions[pointerId] = this.GetEventPointerPosition(triggerEvent);
			this.m_PendingTopElementUnderPointer[pointerId] = newElementUnderPointer;
			VisualElement previousTopElementUnderPointer = this.m_TopElementUnderPointer[pointerId];
			bool flag = newElementUnderPointer == previousTopElementUnderPointer;
			if (!flag)
			{
				IPointerOrMouseEvent p;
				bool flag2;
				if (this.m_TriggerEvent[pointerId] == null)
				{
					p = triggerEvent as IPointerOrMouseEvent;
					flag2 = p != null;
				}
				else
				{
					flag2 = false;
				}
				bool flag3 = flag2;
				if (flag3)
				{
					this.m_TriggerEvent[pointerId] = p;
				}
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0003C3CC File Offset: 0x0003A5CC
		internal void CommitElementUnderPointers(EventDispatcher dispatcher, ContextType contextType)
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				IPointerOrMouseEvent triggerEvent = this.m_TriggerEvent[i];
				VisualElement previous = this.m_TopElementUnderPointer[i];
				VisualElement current = this.m_PendingTopElementUnderPointer[i];
				bool flag = current == previous;
				if (flag)
				{
					bool flag2 = triggerEvent != null;
					if (flag2)
					{
						this.m_PickingPointerPositions[i] = triggerEvent.position;
					}
				}
				else
				{
					this.m_TopElementUnderPointer[i] = current;
					bool flag3 = triggerEvent == null;
					if (flag3)
					{
						using (new EventDispatcherGate(dispatcher))
						{
							Vector2 position = PointerDeviceState.GetPointerPosition(i, contextType);
							PointerEventsHelper.SendOverOut(previous, current, null, position, i);
							PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(previous, current, null, position, i);
							this.m_PickingPointerPositions[i] = position;
							bool flag4 = i == PointerId.mousePointerId;
							if (flag4)
							{
								MouseEventsHelper.SendMouseOverMouseOut(previous, current, null, position);
								MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(previous, current, null, position);
							}
						}
					}
					bool flag5 = triggerEvent != null;
					if (flag5)
					{
						Vector3 pos = triggerEvent.position;
						this.m_PickingPointerPositions[i] = pos;
						EventBase baseEvent = triggerEvent as EventBase;
						bool flag6 = baseEvent != null;
						if (flag6)
						{
							bool flag7 = baseEvent.eventTypeId == EventBase<PointerMoveEvent>.TypeId() || baseEvent.eventTypeId == EventBase<PointerDownEvent>.TypeId() || baseEvent.eventTypeId == EventBase<PointerUpEvent>.TypeId() || baseEvent.eventTypeId == EventBase<PointerCancelEvent>.TypeId();
							if (flag7)
							{
								using (new EventDispatcherGate(dispatcher))
								{
									PointerEventsHelper.SendOverOut(previous, current, (IPointerEvent)triggerEvent, pos, i);
									PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(previous, current, (IPointerEvent)triggerEvent, pos, i);
									IMouseEvent mouseEvent;
									bool flag8;
									if (triggerEvent.pointerId == PointerId.mousePointerId)
									{
										mouseEvent = ((IPointerEventInternal)triggerEvent).compatibilityMouseEvent;
										flag8 = mouseEvent != null;
									}
									else
									{
										flag8 = false;
									}
									bool flag9 = flag8;
									if (flag9)
									{
										MouseEventsHelper.SendMouseOverMouseOut(previous, current, mouseEvent, pos);
										MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(previous, current, mouseEvent, pos);
									}
								}
							}
							else
							{
								bool flag10 = baseEvent.eventTypeId == EventBase<WheelEvent>.TypeId();
								if (flag10)
								{
									using (new EventDispatcherGate(dispatcher))
									{
										MouseEventsHelper.SendMouseOverMouseOut(previous, current, (IMouseEvent)triggerEvent, pos);
										MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(previous, current, (IMouseEvent)triggerEvent, pos);
									}
								}
								else
								{
									bool flag11 = baseEvent.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() || baseEvent.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId();
									if (flag11)
									{
										using (new EventDispatcherGate(dispatcher))
										{
											PointerEventsHelper.SendOverOut(previous, current, null, pos, i);
											PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(previous, current, null, pos, i);
											bool flag12 = i == PointerId.mousePointerId;
											if (flag12)
											{
												MouseEventsHelper.SendMouseOverMouseOut(previous, current, (IMouseEvent)triggerEvent, pos);
												MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(previous, current, (IMouseEvent)triggerEvent, pos);
											}
										}
									}
								}
							}
						}
					}
					this.m_TriggerEvent[i] = null;
				}
			}
		}

		// Token: 0x040007F1 RID: 2033
		private VisualElement[] m_PendingTopElementUnderPointer = new VisualElement[PointerId.maxPointers];

		// Token: 0x040007F2 RID: 2034
		private VisualElement[] m_TopElementUnderPointer = new VisualElement[PointerId.maxPointers];

		// Token: 0x040007F3 RID: 2035
		private IPointerOrMouseEvent[] m_TriggerEvent = new IPointerOrMouseEvent[PointerId.maxPointers];

		// Token: 0x040007F4 RID: 2036
		private Vector2[] m_PickingPointerPositions = new Vector2[PointerId.maxPointers];

		// Token: 0x040007F5 RID: 2037
		private bool[] m_IsPickingPointerTemporaries = new bool[PointerId.maxPointers];
	}
}
