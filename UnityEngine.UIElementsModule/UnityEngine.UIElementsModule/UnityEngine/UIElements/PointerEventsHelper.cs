using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000207 RID: 519
	internal static class PointerEventsHelper
	{
		// Token: 0x06000E4F RID: 3663 RVA: 0x000404F8 File Offset: 0x0003E6F8
		internal static void SendEnterLeave<TLeaveEvent, TEnterEvent>(VisualElement previousTopElementUnderPointer, VisualElement currentTopElementUnderPointer, IPointerEvent triggerEvent, Vector2 position, int pointerId) where TLeaveEvent : PointerEventBase<TLeaveEvent>, new() where TEnterEvent : PointerEventBase<TEnterEvent>, new()
		{
			bool flag = previousTopElementUnderPointer != null && previousTopElementUnderPointer.panel == null;
			if (flag)
			{
				previousTopElementUnderPointer = null;
			}
			int prevDepth = 0;
			VisualElement p;
			for (p = previousTopElementUnderPointer; p != null; p = p.hierarchy.parent)
			{
				prevDepth++;
			}
			int currDepth = 0;
			VisualElement c;
			for (c = currentTopElementUnderPointer; c != null; c = c.hierarchy.parent)
			{
				currDepth++;
			}
			p = previousTopElementUnderPointer;
			c = currentTopElementUnderPointer;
			while (prevDepth > currDepth)
			{
				using (TLeaveEvent leaveEvent = PointerEventBase<TLeaveEvent>.GetPooled(triggerEvent, position, pointerId))
				{
					leaveEvent.elementTarget = p;
					p.SendEvent(leaveEvent);
				}
				prevDepth--;
				p = p.hierarchy.parent;
			}
			List<VisualElement> enteringElements = VisualElementListPool.Get(currDepth);
			while (currDepth > prevDepth)
			{
				enteringElements.Add(c);
				currDepth--;
				c = c.hierarchy.parent;
			}
			while (p != c)
			{
				using (TLeaveEvent leaveEvent2 = PointerEventBase<TLeaveEvent>.GetPooled(triggerEvent, position, pointerId))
				{
					leaveEvent2.elementTarget = p;
					p.SendEvent(leaveEvent2);
				}
				enteringElements.Add(c);
				p = p.hierarchy.parent;
				c = c.hierarchy.parent;
			}
			for (int i = enteringElements.Count - 1; i >= 0; i--)
			{
				using (TEnterEvent enterEvent = PointerEventBase<TEnterEvent>.GetPooled(triggerEvent, position, pointerId))
				{
					enterEvent.elementTarget = enteringElements[i];
					enteringElements[i].SendEvent(enterEvent);
				}
			}
			VisualElementListPool.Release(enteringElements);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00040728 File Offset: 0x0003E928
		internal static void SendOverOut(VisualElement previousTopElementUnderPointer, VisualElement currentTopElementUnderPointer, IPointerEvent triggerEvent, Vector2 position, int pointerId)
		{
			bool flag = previousTopElementUnderPointer != null && previousTopElementUnderPointer.panel != null;
			if (flag)
			{
				using (PointerOutEvent outEvent = PointerEventBase<PointerOutEvent>.GetPooled(triggerEvent, position, pointerId))
				{
					outEvent.elementTarget = previousTopElementUnderPointer;
					previousTopElementUnderPointer.SendEvent(outEvent);
				}
			}
			bool flag2 = currentTopElementUnderPointer != null;
			if (flag2)
			{
				using (PointerOverEvent overEvent = PointerEventBase<PointerOverEvent>.GetPooled(triggerEvent, position, pointerId))
				{
					overEvent.elementTarget = currentTopElementUnderPointer;
					currentTopElementUnderPointer.SendEvent(overEvent);
				}
			}
		}
	}
}
