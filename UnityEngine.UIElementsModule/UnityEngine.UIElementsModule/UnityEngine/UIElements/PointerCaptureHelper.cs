using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000293 RID: 659
	public static class PointerCaptureHelper
	{
		// Token: 0x060011CD RID: 4557 RVA: 0x0004A7EC File Offset: 0x000489EC
		private static PointerDispatchState GetStateFor(IEventHandler handler)
		{
			VisualElement v = handler as VisualElement;
			PointerDispatchState pointerDispatchState;
			if (v == null)
			{
				pointerDispatchState = null;
			}
			else
			{
				IPanel panel = v.panel;
				if (panel == null)
				{
					pointerDispatchState = null;
				}
				else
				{
					EventDispatcher dispatcher = panel.dispatcher;
					pointerDispatchState = ((dispatcher != null) ? dispatcher.pointerState : null);
				}
			}
			return pointerDispatchState;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0004A82C File Offset: 0x00048A2C
		public static bool HasPointerCapture(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState stateFor = PointerCaptureHelper.GetStateFor(handler);
			return stateFor != null && stateFor.HasPointerCapture(handler, pointerId);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0004A852 File Offset: 0x00048A52
		public static void CapturePointer(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState stateFor = PointerCaptureHelper.GetStateFor(handler);
			if (stateFor != null)
			{
				stateFor.CapturePointer(handler, pointerId);
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0004A869 File Offset: 0x00048A69
		public static void ReleasePointer(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState stateFor = PointerCaptureHelper.GetStateFor(handler);
			if (stateFor != null)
			{
				stateFor.ReleasePointer(handler, pointerId);
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0004A880 File Offset: 0x00048A80
		public static IEventHandler GetCapturingElement(this IPanel panel, int pointerId)
		{
			IEventHandler eventHandler;
			if (panel == null)
			{
				eventHandler = null;
			}
			else
			{
				EventDispatcher dispatcher = panel.dispatcher;
				eventHandler = ((dispatcher != null) ? dispatcher.pointerState.GetCapturingElement(pointerId) : null);
			}
			return eventHandler;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0004A8B0 File Offset: 0x00048AB0
		public static void ReleasePointer(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.ReleasePointer(pointerId);
				}
			}
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0004A8D0 File Offset: 0x00048AD0
		internal static void ActivateCompatibilityMouseEvents(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.ActivateCompatibilityMouseEvents(pointerId);
				}
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0004A8F0 File Offset: 0x00048AF0
		internal static void PreventCompatibilityMouseEvents(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.PreventCompatibilityMouseEvents(pointerId);
				}
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0004A910 File Offset: 0x00048B10
		internal static bool ShouldSendCompatibilityMouseEvents(this IPanel panel, IPointerEvent evt)
		{
			bool? flag;
			if (panel == null)
			{
				flag = null;
			}
			else
			{
				EventDispatcher dispatcher = panel.dispatcher;
				flag = ((dispatcher != null) ? new bool?(dispatcher.pointerState.ShouldSendCompatibilityMouseEvents(evt)) : null);
			}
			return flag ?? true;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0004A969 File Offset: 0x00048B69
		internal static void ProcessPointerCapture(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.ProcessPointerCapture(pointerId);
				}
			}
		}
	}
}
