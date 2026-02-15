using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E9 RID: 489
	[EventCategory(EventCategory.Geometry)]
	public class GeometryChangedEvent : EventBase<GeometryChangedEvent>
	{
		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003F355 File Offset: 0x0003D555
		static GeometryChangedEvent()
		{
			EventBase<GeometryChangedEvent>.SetCreateFunction(() => new GeometryChangedEvent());
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0003F370 File Offset: 0x0003D570
		public static GeometryChangedEvent GetPooled(Rect oldRect, Rect newRect)
		{
			GeometryChangedEvent e = EventBase<GeometryChangedEvent>.GetPooled();
			e.oldRect = oldRect;
			e.newRect = newRect;
			return e;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003F399 File Offset: 0x0003D599
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0003F3AA File Offset: 0x0003D5AA
		private void LocalInit()
		{
			this.oldRect = Rect.zero;
			this.newRect = Rect.zero;
			this.layoutPass = 0;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0003F3CD File Offset: 0x0003D5CD
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x0003F3D5 File Offset: 0x0003D5D5
		public Rect oldRect { get; private set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0003F3DE File Offset: 0x0003D5DE
		// (set) Token: 0x06000DAE RID: 3502 RVA: 0x0003F3E6 File Offset: 0x0003D5E6
		public Rect newRect { get; private set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0003F3EF File Offset: 0x0003D5EF
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x0003F3F7 File Offset: 0x0003D5F7
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal int layoutPass { get; set; }

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0003F400 File Offset: 0x0003D600
		public GeometryChangedEvent()
		{
			this.LocalInit();
		}
	}
}
