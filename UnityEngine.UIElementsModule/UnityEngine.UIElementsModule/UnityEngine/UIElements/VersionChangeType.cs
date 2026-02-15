using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000280 RID: 640
	[Flags]
	public enum VersionChangeType
	{
		// Token: 0x040009D5 RID: 2517
		Bindings = 1,
		// Token: 0x040009D6 RID: 2518
		ViewData = 2,
		// Token: 0x040009D7 RID: 2519
		Hierarchy = 4,
		// Token: 0x040009D8 RID: 2520
		Layout = 8,
		// Token: 0x040009D9 RID: 2521
		StyleSheet = 16,
		// Token: 0x040009DA RID: 2522
		Styles = 32,
		// Token: 0x040009DB RID: 2523
		Overflow = 64,
		// Token: 0x040009DC RID: 2524
		BorderRadius = 128,
		// Token: 0x040009DD RID: 2525
		BorderWidth = 256,
		// Token: 0x040009DE RID: 2526
		Transform = 512,
		// Token: 0x040009DF RID: 2527
		Size = 1024,
		// Token: 0x040009E0 RID: 2528
		Repaint = 2048,
		// Token: 0x040009E1 RID: 2529
		Opacity = 4096,
		// Token: 0x040009E2 RID: 2530
		Color = 8192,
		// Token: 0x040009E3 RID: 2531
		RenderHints = 16384,
		// Token: 0x040009E4 RID: 2532
		TransitionProperty = 32768,
		// Token: 0x040009E5 RID: 2533
		EventCallbackCategories = 65536,
		// Token: 0x040009E6 RID: 2534
		DisableRendering = 131072,
		// Token: 0x040009E7 RID: 2535
		BindingRegistration = 262144,
		// Token: 0x040009E8 RID: 2536
		DataSource = 524288,
		// Token: 0x040009E9 RID: 2537
		Picking = 1048576
	}
}
