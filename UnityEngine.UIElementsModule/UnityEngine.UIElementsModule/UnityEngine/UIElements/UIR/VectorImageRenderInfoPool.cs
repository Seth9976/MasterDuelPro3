using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000560 RID: 1376
	internal class VectorImageRenderInfoPool : LinkedPool<VectorImageRenderInfo>
	{
		// Token: 0x060025E5 RID: 9701 RVA: 0x00096DA8 File Offset: 0x00094FA8
		public VectorImageRenderInfoPool()
			: base(() => new VectorImageRenderInfo(), delegate(VectorImageRenderInfo vectorImageInfo)
			{
				vectorImageInfo.Reset();
			}, 10000)
		{
		}
	}
}
