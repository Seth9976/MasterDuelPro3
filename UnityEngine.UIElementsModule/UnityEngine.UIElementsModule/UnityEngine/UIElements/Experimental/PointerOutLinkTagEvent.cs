using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x020005D5 RID: 1493
	[EventCategory(EventCategory.EnterLeave)]
	public class PointerOutLinkTagEvent : PointerEventBase<PointerOutLinkTagEvent>
	{
		// Token: 0x06002862 RID: 10338 RVA: 0x000A6EAA File Offset: 0x000A50AA
		static PointerOutLinkTagEvent()
		{
			EventBase<PointerOutLinkTagEvent>.SetCreateFunction(() => new PointerOutLinkTagEvent());
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000A6EC3 File Offset: 0x000A50C3
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x0003EBEB File Offset: 0x0003CDEB
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000A6ED4 File Offset: 0x000A50D4
		public static PointerOutLinkTagEvent GetPooled(IPointerEvent evt, string linkID)
		{
			return PointerEventBase<PointerOutLinkTagEvent>.GetPooled(evt);
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x000A6EEE File Offset: 0x000A50EE
		public PointerOutLinkTagEvent()
		{
			this.LocalInit();
		}
	}
}
