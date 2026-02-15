using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x020001E4 RID: 484
	public interface IVolume
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000DBE RID: 3518
		// (set) Token: 0x06000DBF RID: 3519
		bool isGlobal { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000DC0 RID: 3520
		List<Collider> colliders { get; }
	}
}
