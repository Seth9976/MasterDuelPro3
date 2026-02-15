using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000C9 RID: 201
	internal struct RenderersBatchersContextDesc
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x000131C8 File Offset: 0x000113C8
		public static RenderersBatchersContextDesc NewDefault()
		{
			return new RenderersBatchersContextDesc
			{
				instanceNumInfo = new InstanceNumInfo(1024, 32)
			};
		}

		// Token: 0x040003F2 RID: 1010
		public InstanceNumInfo instanceNumInfo;

		// Token: 0x040003F3 RID: 1011
		public bool supportDitheringCrossFade;

		// Token: 0x040003F4 RID: 1012
		public bool enableBoundingSpheresInstanceData;

		// Token: 0x040003F5 RID: 1013
		public float smallMeshScreenPercentage;

		// Token: 0x040003F6 RID: 1014
		public bool enableCullerDebugStats;
	}
}
