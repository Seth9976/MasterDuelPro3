using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DC RID: 476
	public class FocusInEvent : FocusEventBase<FocusInEvent>
	{
		// Token: 0x06000D6C RID: 3436 RVA: 0x0003EC82 File Offset: 0x0003CE82
		static FocusInEvent()
		{
			EventBase<FocusInEvent>.SetCreateFunction(() => new FocusInEvent());
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0003EC9B File Offset: 0x0003CE9B
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0003EBEB File Offset: 0x0003CDEB
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0003ECAC File Offset: 0x0003CEAC
		public FocusInEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0003ECBD File Offset: 0x0003CEBD
		protected internal override void PostDispatch(IPanel panel)
		{
			base.focusController.ProcessPendingFocusChange(base.elementTarget);
			base.PostDispatch(panel);
		}
	}
}
