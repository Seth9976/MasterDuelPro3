using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200022D RID: 557
	[EventCategory(EventCategory.EnterLeave)]
	public sealed class PointerLeaveEvent : PointerEventBase<PointerLeaveEvent>
	{
		// Token: 0x06000F43 RID: 3907 RVA: 0x00042E7C File Offset: 0x0004107C
		static PointerLeaveEvent()
		{
			EventBase<PointerLeaveEvent>.SetCreateFunction(() => new PointerLeaveEvent());
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00042E95 File Offset: 0x00041095
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0003FCD6 File Offset: 0x0003DED6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00042EA6 File Offset: 0x000410A6
		public PointerLeaveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0003FCF2 File Offset: 0x0003DEF2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00042EB7 File Offset: 0x000410B7
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.elementTarget.containedPointerIds &= ~(1 << base.pointerId);
			base.elementTarget.UpdateHoverPseudoState();
		}
	}
}
