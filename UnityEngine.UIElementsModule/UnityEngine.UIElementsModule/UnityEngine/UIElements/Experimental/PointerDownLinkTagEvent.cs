using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x020005D7 RID: 1495
	public sealed class PointerDownLinkTagEvent : PointerEventBase<PointerDownLinkTagEvent>
	{
		// Token: 0x0600286A RID: 10346 RVA: 0x000A6F12 File Offset: 0x000A5112
		static PointerDownLinkTagEvent()
		{
			EventBase<PointerDownLinkTagEvent>.SetCreateFunction(() => new PointerDownLinkTagEvent());
		}

		// Token: 0x17000A6F RID: 2671
		// (set) Token: 0x0600286B RID: 10347 RVA: 0x000A6F2B File Offset: 0x000A512B
		private string linkID
		{
			[CompilerGenerated]
			set
			{
				this.<linkID>k__BackingField = value;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (set) Token: 0x0600286C RID: 10348 RVA: 0x000A6F34 File Offset: 0x000A5134
		private string linkText
		{
			[CompilerGenerated]
			set
			{
				this.<linkText>k__BackingField = value;
			}
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x000A6F3D File Offset: 0x000A513D
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x0003EBEB File Offset: 0x0003CDEB
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x000A6F50 File Offset: 0x000A5150
		public static PointerDownLinkTagEvent GetPooled(IPointerEvent evt, string linkID, string linkText)
		{
			PointerDownLinkTagEvent e = PointerEventBase<PointerDownLinkTagEvent>.GetPooled(evt);
			e.linkID = linkID;
			e.linkText = linkText;
			return e;
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000A6F7A File Offset: 0x000A517A
		public PointerDownLinkTagEvent()
		{
			this.LocalInit();
		}
	}
}
