using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021F RID: 543
	internal static class PointerEventHelper
	{
		// Token: 0x06000EC4 RID: 3780 RVA: 0x00040FE0 File Offset: 0x0003F1E0
		public static EventBase GetPooled(EventType eventType, Vector3 mousePosition, Vector2 delta, int button, int clickCount, EventModifiers modifiers, int displayIndex)
		{
			bool flag = eventType == EventType.MouseDown && !PointerDeviceState.HasAdditionalPressedButtons(PointerId.mousePointerId, button);
			EventBase eventBase;
			if (flag)
			{
				eventBase = PointerEventBase<PointerDownEvent>.GetPooled(eventType, mousePosition, delta, button, clickCount, modifiers, displayIndex);
			}
			else
			{
				bool flag2 = eventType == EventType.MouseUp && !PointerDeviceState.HasAdditionalPressedButtons(PointerId.mousePointerId, button);
				if (flag2)
				{
					eventBase = PointerEventBase<PointerUpEvent>.GetPooled(eventType, mousePosition, delta, button, clickCount, modifiers, displayIndex);
				}
				else
				{
					eventBase = PointerEventBase<PointerMoveEvent>.GetPooled(eventType, mousePosition, delta, button, clickCount, modifiers, displayIndex);
				}
			}
			return eventBase;
		}
	}
}
