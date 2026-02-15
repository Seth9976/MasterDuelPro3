using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006C RID: 108
	public abstract class ContextualMenuManager
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x000133B5 File Offset: 0x000115B5
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x000133BD File Offset: 0x000115BD
		internal bool displayMenuHandledOSX { get; set; }

		// Token: 0x060003E2 RID: 994
		public abstract void DisplayMenuIfEventMatches(EventBase evt, IEventHandler eventHandler);

		// Token: 0x060003E3 RID: 995 RVA: 0x000133C8 File Offset: 0x000115C8
		public void DisplayMenu(EventBase triggerEvent, IEventHandler target)
		{
			DropdownMenu menu = new DropdownMenu();
			this.DisplayMenu(triggerEvent, target, menu);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000133E8 File Offset: 0x000115E8
		internal void DisplayMenu(EventBase triggerEvent, IEventHandler target, DropdownMenu menu)
		{
			int pointerId;
			int button;
			using (ContextualMenuPopulateEvent cme = ContextualMenuPopulateEvent.GetPooled(triggerEvent, menu, target, this))
			{
				IPointerEvent pe = triggerEvent as IPointerEvent;
				pointerId = ((pe != null) ? pe.pointerId : PointerId.mousePointerId);
				button = cme.button;
				if (target != null)
				{
					target.SendEvent(cme);
				}
			}
			bool isOSXContextualMenuPlatform = UIElementsUtility.isOSXContextualMenuPlatform;
			if (isOSXContextualMenuPlatform)
			{
				this.displayMenuHandledOSX = true;
				bool flag = button >= 0;
				if (flag)
				{
					PointerDeviceState.ReleaseButton(pointerId, button);
				}
			}
		}

		// Token: 0x060003E5 RID: 997
		protected internal abstract void DoDisplayMenu(DropdownMenu menu, EventBase triggerEvent);
	}
}
