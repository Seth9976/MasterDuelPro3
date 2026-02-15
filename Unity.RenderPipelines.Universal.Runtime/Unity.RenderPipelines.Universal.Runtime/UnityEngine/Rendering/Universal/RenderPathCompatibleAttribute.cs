using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001E1 RID: 481
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class RenderPathCompatibleAttribute : Attribute
	{
		// Token: 0x06000AC7 RID: 2759 RVA: 0x00038F99 File Offset: 0x00037199
		public RenderPathCompatibleAttribute(RenderPathCompatibility renderPath)
		{
			this.renderPath = renderPath;
		}

		// Token: 0x04000BD2 RID: 3026
		public RenderPathCompatibility renderPath;
	}
}
