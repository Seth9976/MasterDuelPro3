using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001CF RID: 463
	internal class EventCallbackListPool
	{
		// Token: 0x06000D08 RID: 3336 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		public EventCallbackList Get(EventCallbackList initializer)
		{
			bool flag = this.m_Stack.Count == 0;
			EventCallbackList element;
			if (flag)
			{
				bool flag2 = initializer != null;
				if (flag2)
				{
					element = new EventCallbackList(initializer);
				}
				else
				{
					element = new EventCallbackList();
				}
			}
			else
			{
				element = this.m_Stack.Pop();
				bool flag3 = initializer != null;
				if (flag3)
				{
					element.AddRange(initializer);
				}
			}
			return element;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0003D330 File Offset: 0x0003B530
		public void Release(EventCallbackList element)
		{
			element.Clear();
			this.m_Stack.Push(element);
		}

		// Token: 0x04000825 RID: 2085
		private readonly Stack<EventCallbackList> m_Stack = new Stack<EventCallbackList>();
	}
}
