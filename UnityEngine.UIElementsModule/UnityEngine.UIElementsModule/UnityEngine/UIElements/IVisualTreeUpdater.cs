using System;
using Unity.Profiling;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004F4 RID: 1268
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal interface IVisualTreeUpdater : IDisposable
	{
		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600236F RID: 9071
		// (set) Token: 0x06002370 RID: 9072
		long FrameCount { get; set; }

		// Token: 0x1700094F RID: 2383
		// (set) Token: 0x06002371 RID: 9073
		BaseVisualElementPanel panel { set; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002372 RID: 9074
		ProfilerMarker profilerMarker { get; }

		// Token: 0x06002373 RID: 9075
		void Update();

		// Token: 0x06002374 RID: 9076
		void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType);
	}
}
