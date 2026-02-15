using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x020005D3 RID: 1491
	[EventCategory(EventCategory.PointerMove)]
	public class PointerMoveLinkTagEvent : PointerEventBase<PointerMoveLinkTagEvent>
	{
		// Token: 0x06002858 RID: 10328 RVA: 0x000A6E1E File Offset: 0x000A501E
		static PointerMoveLinkTagEvent()
		{
			EventBase<PointerMoveLinkTagEvent>.SetCreateFunction(() => new PointerMoveLinkTagEvent());
		}

		// Token: 0x17000A6D RID: 2669
		// (set) Token: 0x06002859 RID: 10329 RVA: 0x000A6E37 File Offset: 0x000A5037
		private string linkID
		{
			[CompilerGenerated]
			set
			{
				this.<linkID>k__BackingField = value;
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (set) Token: 0x0600285A RID: 10330 RVA: 0x000A6E40 File Offset: 0x000A5040
		private string linkText
		{
			[CompilerGenerated]
			set
			{
				this.<linkText>k__BackingField = value;
			}
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x000A6E49 File Offset: 0x000A5049
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x0003EBEB File Offset: 0x0003CDEB
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000A6E5C File Offset: 0x000A505C
		public static PointerMoveLinkTagEvent GetPooled(IPointerEvent evt, string linkID, string linkText)
		{
			PointerMoveLinkTagEvent e = PointerEventBase<PointerMoveLinkTagEvent>.GetPooled(evt);
			e.linkID = linkID;
			e.linkText = linkText;
			return e;
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000A6E86 File Offset: 0x000A5086
		public PointerMoveLinkTagEvent()
		{
			this.LocalInit();
		}
	}
}
