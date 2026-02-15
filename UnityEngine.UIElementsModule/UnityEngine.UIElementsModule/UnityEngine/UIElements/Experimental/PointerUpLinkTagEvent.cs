using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x020005D9 RID: 1497
	public class PointerUpLinkTagEvent : PointerEventBase<PointerUpLinkTagEvent>
	{
		// Token: 0x06002874 RID: 10356 RVA: 0x000A6F9E File Offset: 0x000A519E
		static PointerUpLinkTagEvent()
		{
			EventBase<PointerUpLinkTagEvent>.SetCreateFunction(() => new PointerUpLinkTagEvent());
		}

		// Token: 0x17000A71 RID: 2673
		// (set) Token: 0x06002875 RID: 10357 RVA: 0x000A6FB7 File Offset: 0x000A51B7
		private string linkID
		{
			[CompilerGenerated]
			set
			{
				this.<linkID>k__BackingField = value;
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (set) Token: 0x06002876 RID: 10358 RVA: 0x000A6FC0 File Offset: 0x000A51C0
		private string linkText
		{
			[CompilerGenerated]
			set
			{
				this.<linkText>k__BackingField = value;
			}
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x000A6FC9 File Offset: 0x000A51C9
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x0003EBEB File Offset: 0x0003CDEB
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000A6FDC File Offset: 0x000A51DC
		public static PointerUpLinkTagEvent GetPooled(IPointerEvent evt, string linkID, string linkText)
		{
			PointerUpLinkTagEvent e = PointerEventBase<PointerUpLinkTagEvent>.GetPooled(evt);
			e.linkID = linkID;
			e.linkText = linkText;
			return e;
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000A7006 File Offset: 0x000A5206
		public PointerUpLinkTagEvent()
		{
			this.LocalInit();
		}
	}
}
