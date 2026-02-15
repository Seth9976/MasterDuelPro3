using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200022B RID: 555
	[EventCategory(EventCategory.EnterLeave)]
	public sealed class PointerEnterEvent : PointerEventBase<PointerEnterEvent>
	{
		// Token: 0x06000F3A RID: 3898 RVA: 0x00042DF9 File Offset: 0x00040FF9
		static PointerEnterEvent()
		{
			EventBase<PointerEnterEvent>.SetCreateFunction(() => new PointerEnterEvent());
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00042E12 File Offset: 0x00041012
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0003FCD6 File Offset: 0x0003DED6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00042E23 File Offset: 0x00041023
		public PointerEnterEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0003FCF2 File Offset: 0x0003DEF2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00042E34 File Offset: 0x00041034
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.elementTarget.containedPointerIds |= 1 << base.pointerId;
			base.elementTarget.UpdateHoverPseudoState();
		}
	}
}
