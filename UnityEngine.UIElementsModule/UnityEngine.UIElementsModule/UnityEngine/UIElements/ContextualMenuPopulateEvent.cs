using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000204 RID: 516
	public class ContextualMenuPopulateEvent : MouseEventBase<ContextualMenuPopulateEvent>
	{
		// Token: 0x06000E40 RID: 3648 RVA: 0x0003FFA6 File Offset: 0x0003E1A6
		static ContextualMenuPopulateEvent()
		{
			EventBase<ContextualMenuPopulateEvent>.SetCreateFunction(() => new ContextualMenuPopulateEvent());
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0003FFBF File Offset: 0x0003E1BF
		// (set) Token: 0x06000E42 RID: 3650 RVA: 0x0003FFC7 File Offset: 0x0003E1C7
		public DropdownMenu menu { get; private set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0003FFD0 File Offset: 0x0003E1D0
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x0003FFD8 File Offset: 0x0003E1D8
		public EventBase triggerEvent { get; private set; }

		// Token: 0x06000E45 RID: 3653 RVA: 0x0003FFE4 File Offset: 0x0003E1E4
		public static ContextualMenuPopulateEvent GetPooled(EventBase triggerEvent, DropdownMenu menu, IEventHandler target, ContextualMenuManager menuManager)
		{
			ContextualMenuPopulateEvent e = EventBase<ContextualMenuPopulateEvent>.GetPooled(triggerEvent);
			bool flag = triggerEvent != null;
			if (flag)
			{
				triggerEvent.Acquire();
				e.triggerEvent = triggerEvent;
				IMouseEvent mouseEvent = triggerEvent as IMouseEvent;
				bool flag2 = mouseEvent != null;
				if (flag2)
				{
					e.modifiers = mouseEvent.modifiers;
					e.mousePosition = mouseEvent.mousePosition;
					e.localMousePosition = mouseEvent.mousePosition;
					e.mouseDelta = mouseEvent.mouseDelta;
					e.button = mouseEvent.button;
					e.clickCount = mouseEvent.clickCount;
				}
				else
				{
					IPointerEvent pointerEvent = triggerEvent as IPointerEvent;
					bool flag3 = pointerEvent != null;
					if (flag3)
					{
						e.modifiers = pointerEvent.modifiers;
						e.mousePosition = pointerEvent.position;
						e.localMousePosition = pointerEvent.position;
						e.mouseDelta = pointerEvent.deltaPosition;
						e.button = pointerEvent.button;
						e.clickCount = pointerEvent.clickCount;
					}
				}
				IMouseEventInternal mouseEventInternal = triggerEvent as IMouseEventInternal;
				bool flag4 = mouseEventInternal != null;
				if (flag4)
				{
					((IMouseEventInternal)e).triggeredByOS = mouseEventInternal.triggeredByOS;
				}
				else
				{
					IPointerEventInternal pointerEventInternal = triggerEvent as IPointerEventInternal;
					bool flag5 = pointerEventInternal != null;
					if (flag5)
					{
						((IMouseEventInternal)e).triggeredByOS = pointerEventInternal.triggeredByOS;
					}
				}
			}
			e.elementTarget = (VisualElement)target;
			e.menu = menu;
			e.m_ContextualMenuManager = menuManager;
			return e;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00040158 File Offset: 0x0003E358
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0004016C File Offset: 0x0003E36C
		private void LocalInit()
		{
			this.menu = null;
			this.m_ContextualMenuManager = null;
			bool flag = this.triggerEvent != null;
			if (flag)
			{
				this.triggerEvent.Dispose();
				this.triggerEvent = null;
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x000401AC File Offset: 0x0003E3AC
		public ContextualMenuPopulateEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x000401C0 File Offset: 0x0003E3C0
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = this.menu.Count > 0 && this.m_ContextualMenuManager != null;
			if (flag)
			{
				this.menu.PrepareForDisplay(this.triggerEvent);
				this.m_ContextualMenuManager.DoDisplayMenu(this.menu, this.triggerEvent);
			}
			base.PostDispatch(panel);
		}

		// Token: 0x0400086A RID: 2154
		private ContextualMenuManager m_ContextualMenuManager;
	}
}
