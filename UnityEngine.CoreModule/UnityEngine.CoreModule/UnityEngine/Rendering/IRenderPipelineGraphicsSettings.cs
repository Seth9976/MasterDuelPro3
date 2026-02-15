using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200035C RID: 860
	public interface IRenderPipelineGraphicsSettings
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600168C RID: 5772
		int version { get; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0002F6BD File Offset: 0x0002D8BD
		bool isAvailableInPlayerBuild
		{
			get
			{
				return false;
			}
		}
	}
}
