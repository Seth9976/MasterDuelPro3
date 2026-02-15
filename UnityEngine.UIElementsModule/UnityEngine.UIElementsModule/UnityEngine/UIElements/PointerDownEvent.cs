using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000221 RID: 545
	[EventCategory(EventCategory.PointerDown)]
	public sealed class PointerDownEvent : PointerEventBase<PointerDownEvent>
	{
		// Token: 0x06000F0B RID: 3851 RVA: 0x000429CC File Offset: 0x00040BCC
		static PointerDownEvent()
		{
			EventBase<PointerDownEvent>.SetCreateFunction(() => new PointerDownEvent());
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x000429E5 File Offset: 0x00040BE5
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x000429F6 File Offset: 0x00040BF6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
			((IPointerEventInternal)this).triggeredByOS = true;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00042A09 File Offset: 0x00040C09
		public PointerDownEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00042A1C File Offset: 0x00040C1C
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool flag = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag)
			{
				((IPointerEventInternal)this).compatibilityMouseEvent = MouseDownEvent.GetPooled(this);
			}
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00042A4C File Offset: 0x00040C4C
		protected internal override void PostDispatch(IPanel panel)
		{
			panel.focusController.SwitchFocusOnEvent(panel.focusController.GetLeafFocusedElement(), this);
			base.PostDispatch(panel);
		}
	}
}
