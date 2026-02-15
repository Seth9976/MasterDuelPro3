using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D8 RID: 472
	public class FocusOutEvent : FocusEventBase<FocusOutEvent>
	{
		// Token: 0x06000D5F RID: 3423 RVA: 0x0003EBC1 File Offset: 0x0003CDC1
		static FocusOutEvent()
		{
			EventBase<FocusOutEvent>.SetCreateFunction(() => new FocusOutEvent());
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0003EBDA File Offset: 0x0003CDDA
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0003EBEB File Offset: 0x0003CDEB
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0003EBF6 File Offset: 0x0003CDF6
		public FocusOutEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0003EC08 File Offset: 0x0003CE08
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = base.relatedTarget == null;
			if (flag)
			{
				base.focusController.ProcessPendingFocusChange(null);
			}
			base.PostDispatch(panel);
		}
	}
}
