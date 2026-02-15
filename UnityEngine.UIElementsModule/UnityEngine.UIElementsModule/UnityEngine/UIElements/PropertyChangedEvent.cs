using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004D1 RID: 1233
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class PropertyChangedEvent : EventBase<PropertyChangedEvent>
	{
		// Token: 0x060022DE RID: 8926 RVA: 0x00080249 File Offset: 0x0007E449
		static PropertyChangedEvent()
		{
			EventBase<PropertyChangedEvent>.SetCreateFunction(() => new PropertyChangedEvent());
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x00080262 File Offset: 0x0007E462
		// (set) Token: 0x060022E0 RID: 8928 RVA: 0x0008026A File Offset: 0x0007E46A
		public BindingId property { get; set; }

		// Token: 0x060022E1 RID: 8929 RVA: 0x00080273 File Offset: 0x0007E473
		public PropertyChangedEvent()
		{
			base.bubbles = false;
			base.tricklesDown = false;
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x00080290 File Offset: 0x0007E490
		public static PropertyChangedEvent GetPooled(in BindingId property)
		{
			PropertyChangedEvent e = EventBase<PropertyChangedEvent>.GetPooled();
			e.property = property;
			return e;
		}
	}
}
