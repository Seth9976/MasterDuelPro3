using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D7 RID: 471
	[EventCategory(EventCategory.Focus)]
	public abstract class FocusEventBase<T> : EventBase<T> where T : FocusEventBase<T>, new()
	{
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0003EAD4 File Offset: 0x0003CCD4
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x0003EADC File Offset: 0x0003CCDC
		public Focusable relatedTarget { get; private set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0003EAE5 File Offset: 0x0003CCE5
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x0003EAED File Offset: 0x0003CCED
		public FocusChangeDirection direction { get; private set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0003EAF6 File Offset: 0x0003CCF6
		// (set) Token: 0x06000D58 RID: 3416 RVA: 0x0003EAFE File Offset: 0x0003CCFE
		private protected FocusController focusController { protected get; private set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0003EB07 File Offset: 0x0003CD07
		// (set) Token: 0x06000D5A RID: 3418 RVA: 0x0003EB0F File Offset: 0x0003CD0F
		internal bool IsFocusDelegated { get; private set; }

		// Token: 0x06000D5B RID: 3419 RVA: 0x0003EB18 File Offset: 0x0003CD18
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0003EB29 File Offset: 0x0003CD29
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
			this.relatedTarget = null;
			this.direction = FocusChangeDirection.unspecified;
			this.focusController = null;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0003EB50 File Offset: 0x0003CD50
		public static T GetPooled(IEventHandler target, Focusable relatedTarget, FocusChangeDirection direction, FocusController focusController, bool bIsFocusDelegated = false)
		{
			T e = EventBase<T>.GetPooled();
			e.elementTarget = (VisualElement)target;
			e.relatedTarget = relatedTarget;
			e.direction = direction;
			e.focusController = focusController;
			e.IsFocusDelegated = bIsFocusDelegated;
			return e;
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0003EBB0 File Offset: 0x0003CDB0
		protected FocusEventBase()
		{
			this.LocalInit();
		}
	}
}
