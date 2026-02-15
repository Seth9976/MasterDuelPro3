using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D6 RID: 214
	[Obsolete("This variant is obsolete and kept only for not breaking user code. Use IVolumeDebugSettings instead. #from(23.2) (UnityUpgradable) -> IVolumeDebugSettings", false)]
	public interface IVolumeDebugSettings2 : IVolumeDebugSettings
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060006FD RID: 1789
		[Obsolete("This property is obsolete and kept only for not breaking user code. VolumeDebugSettings will use current pipeline when it needs to gather volume component types and paths. #from(23.2)", false)]
		Type targetRenderPipeline { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060006FE RID: 1790
		[Obsolete("This property is obsolete and kept only for not breaking user code. VolumeDebugSettings will use current pipeline when it needs to gather volume component types and paths. #from(23.2)", false)]
		List<ValueTuple<string, Type>> volumeComponentsPathAndType { get; }
	}
}
