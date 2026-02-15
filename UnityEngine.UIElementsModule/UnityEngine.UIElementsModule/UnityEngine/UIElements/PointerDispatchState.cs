using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000294 RID: 660
	internal class PointerDispatchState
	{
		// Token: 0x060011D7 RID: 4567 RVA: 0x0004A98C File Offset: 0x00048B8C
		public PointerDispatchState()
		{
			this.Reset();
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0004A9D8 File Offset: 0x00048BD8
		internal void Reset()
		{
			for (int i = 0; i < this.m_PointerCapture.Length; i++)
			{
				this.m_PendingPointerCapture[i] = null;
				this.m_PointerCapture[i] = null;
				this.m_ShouldSendCompatibilityMouseEvents[i] = true;
			}
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0004AA1C File Offset: 0x00048C1C
		public IEventHandler GetCapturingElement(int pointerId)
		{
			return this.m_PendingPointerCapture[pointerId];
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0004AA38 File Offset: 0x00048C38
		public bool HasPointerCapture(IEventHandler handler, int pointerId)
		{
			return this.m_PendingPointerCapture[pointerId] == handler;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0004AA58 File Offset: 0x00048C58
		public void CapturePointer(IEventHandler handler, int pointerId)
		{
			bool flag = pointerId == PointerId.mousePointerId && this.m_PendingPointerCapture[pointerId] != handler && GUIUtility.hotControl != 0;
			if (flag)
			{
				GUIUtility.hotControl = 0;
			}
			this.m_PendingPointerCapture[pointerId] = handler;
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0004AA9A File Offset: 0x00048C9A
		public void ReleasePointer(int pointerId)
		{
			this.m_PendingPointerCapture[pointerId] = null;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0004AAA8 File Offset: 0x00048CA8
		public void ReleasePointer(IEventHandler handler, int pointerId)
		{
			bool flag = handler == this.m_PendingPointerCapture[pointerId];
			if (flag)
			{
				this.m_PendingPointerCapture[pointerId] = null;
			}
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0004AAD0 File Offset: 0x00048CD0
		public void ProcessPointerCapture(int pointerId)
		{
			IEventHandler capture = this.m_PointerCapture[pointerId];
			bool flag = capture == this.m_PendingPointerCapture[pointerId];
			if (!flag)
			{
				bool flag2 = capture != null;
				if (flag2)
				{
					using (PointerCaptureOutEvent e = PointerCaptureEventBase<PointerCaptureOutEvent>.GetPooled(capture, this.m_PendingPointerCapture[pointerId], pointerId))
					{
						capture.SendEvent(e);
					}
					bool flag3 = pointerId == PointerId.mousePointerId && this.m_PointerCapture[pointerId] == capture;
					if (flag3)
					{
						using (MouseCaptureOutEvent e2 = PointerCaptureEventBase<MouseCaptureOutEvent>.GetPooled(capture, this.m_PendingPointerCapture[pointerId], pointerId))
						{
							capture.SendEvent(e2);
						}
					}
				}
				IEventHandler pendingCapture = this.m_PendingPointerCapture[pointerId];
				bool flag4 = pendingCapture != null;
				if (flag4)
				{
					using (PointerCaptureEvent e3 = PointerCaptureEventBase<PointerCaptureEvent>.GetPooled(pendingCapture, this.m_PointerCapture[pointerId], pointerId))
					{
						pendingCapture.SendEvent(e3);
					}
					bool flag5 = pointerId == PointerId.mousePointerId && this.m_PendingPointerCapture[pointerId] == pendingCapture;
					if (flag5)
					{
						using (MouseCaptureEvent e4 = PointerCaptureEventBase<MouseCaptureEvent>.GetPooled(pendingCapture, this.m_PointerCapture[pointerId], pointerId))
						{
							pendingCapture.SendEvent(e4);
						}
					}
				}
				this.m_PointerCapture[pointerId] = this.m_PendingPointerCapture[pointerId];
			}
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0004AC48 File Offset: 0x00048E48
		public void ActivateCompatibilityMouseEvents(int pointerId)
		{
			this.m_ShouldSendCompatibilityMouseEvents[pointerId] = true;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0004AC54 File Offset: 0x00048E54
		public void PreventCompatibilityMouseEvents(int pointerId)
		{
			this.m_ShouldSendCompatibilityMouseEvents[pointerId] = false;
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0004AC60 File Offset: 0x00048E60
		public bool ShouldSendCompatibilityMouseEvents(IPointerEvent evt)
		{
			return evt.isPrimary && this.m_ShouldSendCompatibilityMouseEvents[evt.pointerId];
		}

		// Token: 0x04000A4A RID: 2634
		private IEventHandler[] m_PendingPointerCapture = new IEventHandler[PointerId.maxPointers];

		// Token: 0x04000A4B RID: 2635
		private IEventHandler[] m_PointerCapture = new IEventHandler[PointerId.maxPointers];

		// Token: 0x04000A4C RID: 2636
		private bool[] m_ShouldSendCompatibilityMouseEvents = new bool[PointerId.maxPointers];
	}
}
