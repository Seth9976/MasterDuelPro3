using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x020005D1 RID: 1489
	[EventCategory(EventCategory.EnterLeave)]
	public class PointerOverLinkTagEvent : PointerEventBase<PointerOverLinkTagEvent>
	{
		// Token: 0x0600284E RID: 10318 RVA: 0x000A6D91 File Offset: 0x000A4F91
		static PointerOverLinkTagEvent()
		{
			EventBase<PointerOverLinkTagEvent>.SetCreateFunction(() => new PointerOverLinkTagEvent());
		}

		// Token: 0x17000A6B RID: 2667
		// (set) Token: 0x0600284F RID: 10319 RVA: 0x000A6DAA File Offset: 0x000A4FAA
		private string linkID
		{
			[CompilerGenerated]
			set
			{
				this.<linkID>k__BackingField = value;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (set) Token: 0x06002850 RID: 10320 RVA: 0x000A6DB3 File Offset: 0x000A4FB3
		private string linkText
		{
			[CompilerGenerated]
			set
			{
				this.<linkText>k__BackingField = value;
			}
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000A6DBC File Offset: 0x000A4FBC
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x0003EBEB File Offset: 0x0003CDEB
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000A6DD0 File Offset: 0x000A4FD0
		public static PointerOverLinkTagEvent GetPooled(IPointerEvent evt, string linkID, string linkText)
		{
			PointerOverLinkTagEvent e = PointerEventBase<PointerOverLinkTagEvent>.GetPooled(evt);
			e.linkID = linkID;
			e.linkText = linkText;
			return e;
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x000A6DFA File Offset: 0x000A4FFA
		public PointerOverLinkTagEvent()
		{
			this.LocalInit();
		}
	}
}
