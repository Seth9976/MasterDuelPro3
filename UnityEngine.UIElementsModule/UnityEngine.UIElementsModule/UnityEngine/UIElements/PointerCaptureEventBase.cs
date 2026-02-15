using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B1 RID: 433
	[EventCategory(EventCategory.Pointer)]
	public abstract class PointerCaptureEventBase<T> : EventBase<T> where T : PointerCaptureEventBase<T>, new()
	{
		// Token: 0x1700023D RID: 573
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x0003BE21 File Offset: 0x0003A021
		private IEventHandler relatedTarget
		{
			[CompilerGenerated]
			set
			{
				this.<relatedTarget>k__BackingField = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0003BE2A File Offset: 0x0003A02A
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x0003BE32 File Offset: 0x0003A032
		public int pointerId { get; private set; }

		// Token: 0x06000C62 RID: 3170 RVA: 0x0003BE3B File Offset: 0x0003A03B
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0003BE4C File Offset: 0x0003A04C
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			this.relatedTarget = null;
			this.pointerId = PointerId.invalidPointerId;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0003BE6C File Offset: 0x0003A06C
		public static T GetPooled(IEventHandler target, IEventHandler relatedTarget, int pointerId)
		{
			T e = EventBase<T>.GetPooled();
			e.elementTarget = (VisualElement)target;
			e.relatedTarget = relatedTarget;
			e.pointerId = pointerId;
			return e;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0003BEB1 File Offset: 0x0003A0B1
		protected PointerCaptureEventBase()
		{
			this.LocalInit();
		}
	}
}
