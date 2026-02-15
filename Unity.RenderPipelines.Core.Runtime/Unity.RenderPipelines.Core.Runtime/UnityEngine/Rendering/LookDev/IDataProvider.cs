using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.LookDev
{
	// Token: 0x0200029D RID: 669
	public interface IDataProvider
	{
		// Token: 0x060011DB RID: 4571
		void FirstInitScene(StageRuntimeInterface stage);

		// Token: 0x060011DC RID: 4572
		void UpdateSky(Camera camera, Sky sky, StageRuntimeInterface stage);

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060011DD RID: 4573
		IEnumerable<string> supportedDebugModes { get; }

		// Token: 0x060011DE RID: 4574
		void UpdateDebugMode(int debugIndex);

		// Token: 0x060011DF RID: 4575
		void GetShadowMask(ref RenderTexture output, StageRuntimeInterface stage);

		// Token: 0x060011E0 RID: 4576
		void OnBeginRendering(StageRuntimeInterface stage);

		// Token: 0x060011E1 RID: 4577
		void OnEndRendering(StageRuntimeInterface stage);

		// Token: 0x060011E2 RID: 4578
		void Cleanup(StageRuntimeInterface SRI);
	}
}
