using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000244 RID: 580
	[EventCategory(EventCategory.IMGUI)]
	public class IMGUIEvent : EventBase<IMGUIEvent>
	{
		// Token: 0x06000F92 RID: 3986 RVA: 0x000433CD File Offset: 0x000415CD
		static IMGUIEvent()
		{
			EventBase<IMGUIEvent>.SetCreateFunction(() => new IMGUIEvent());
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x000433E8 File Offset: 0x000415E8
		public static IMGUIEvent GetPooled(Event systemEvent)
		{
			IMGUIEvent e = EventBase<IMGUIEvent>.GetPooled();
			e.imguiEvent = systemEvent;
			return e;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00043409 File Offset: 0x00041609
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0003EBEB File Offset: 0x0003CDEB
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0004341A File Offset: 0x0004161A
		public IMGUIEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0004342B File Offset: 0x0004162B
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToPanelRoot(this, panel);
		}
	}
}
