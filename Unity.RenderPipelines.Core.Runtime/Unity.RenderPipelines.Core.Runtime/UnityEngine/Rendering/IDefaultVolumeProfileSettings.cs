using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000177 RID: 375
	public interface IDefaultVolumeProfileSettings : IRenderPipelineGraphicsSettings
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x000104EC File Offset: 0x0000E6EC
		bool IRenderPipelineGraphicsSettings.isAvailableInPlayerBuild
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000AC3 RID: 2755
		// (set) Token: 0x06000AC4 RID: 2756
		VolumeProfile volumeProfile { get; set; }
	}
}
