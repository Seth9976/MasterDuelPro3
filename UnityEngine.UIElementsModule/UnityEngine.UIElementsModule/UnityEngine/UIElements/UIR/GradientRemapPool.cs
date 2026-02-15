using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000563 RID: 1379
	internal class GradientRemapPool : LinkedPool<GradientRemap>
	{
		// Token: 0x060025EC RID: 9708 RVA: 0x00096E44 File Offset: 0x00095044
		public GradientRemapPool()
			: base(() => new GradientRemap(), delegate(GradientRemap gradientRemap)
			{
				gradientRemap.Reset();
			}, 10000)
		{
		}
	}
}
