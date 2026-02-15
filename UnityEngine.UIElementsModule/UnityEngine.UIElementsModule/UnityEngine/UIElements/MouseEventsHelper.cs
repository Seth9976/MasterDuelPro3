using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000206 RID: 518
	internal static class MouseEventsHelper
	{
		// Token: 0x06000E4D RID: 3661 RVA: 0x00040234 File Offset: 0x0003E434
		internal static void SendEnterLeave<TLeaveEvent, TEnterEvent>(VisualElement previousTopElementUnderMouse, VisualElement currentTopElementUnderMouse, IMouseEvent triggerEvent, Vector2 mousePosition) where TLeaveEvent : MouseEventBase<TLeaveEvent>, new() where TEnterEvent : MouseEventBase<TEnterEvent>, new()
		{
			bool flag = previousTopElementUnderMouse != null && previousTopElementUnderMouse.panel == null;
			if (flag)
			{
				previousTopElementUnderMouse = null;
			}
			int prevDepth = 0;
			VisualElement p;
			for (p = previousTopElementUnderMouse; p != null; p = p.hierarchy.parent)
			{
				prevDepth++;
			}
			int currDepth = 0;
			VisualElement c;
			for (c = currentTopElementUnderMouse; c != null; c = c.hierarchy.parent)
			{
				currDepth++;
			}
			p = previousTopElementUnderMouse;
			c = currentTopElementUnderMouse;
			while (prevDepth > currDepth)
			{
				using (TLeaveEvent leaveEvent = MouseEventBase<TLeaveEvent>.GetPooled(triggerEvent, mousePosition))
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
				using (TLeaveEvent leaveEvent2 = MouseEventBase<TLeaveEvent>.GetPooled(triggerEvent, mousePosition))
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
				using (TEnterEvent enterEvent = MouseEventBase<TEnterEvent>.GetPooled(triggerEvent, mousePosition))
				{
					enterEvent.elementTarget = enteringElements[i];
					enteringElements[i].SendEvent(enterEvent);
				}
			}
			VisualElementListPool.Release(enteringElements);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00040460 File Offset: 0x0003E660
		internal static void SendMouseOverMouseOut(VisualElement previousTopElementUnderMouse, VisualElement currentTopElementUnderMouse, IMouseEvent triggerEvent, Vector2 mousePosition)
		{
			bool flag = previousTopElementUnderMouse != null && previousTopElementUnderMouse.panel != null;
			if (flag)
			{
				using (MouseOutEvent outEvent = MouseEventBase<MouseOutEvent>.GetPooled(triggerEvent, mousePosition))
				{
					outEvent.elementTarget = previousTopElementUnderMouse;
					previousTopElementUnderMouse.SendEvent(outEvent);
				}
			}
			bool flag2 = currentTopElementUnderMouse != null;
			if (flag2)
			{
				using (MouseOverEvent overEvent = MouseEventBase<MouseOverEvent>.GetPooled(triggerEvent, mousePosition))
				{
					overEvent.elementTarget = currentTopElementUnderMouse;
					currentTopElementUnderMouse.SendEvent(overEvent);
				}
			}
		}
	}
}
