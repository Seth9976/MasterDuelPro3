using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000150 RID: 336
	internal struct PostProcessParams
	{
		// Token: 0x0600073B RID: 1851 RVA: 0x00023478 File Offset: 0x00021678
		public static PostProcessParams Create()
		{
			PostProcessParams ppParams;
			ppParams.blitMaterial = null;
			ppParams.requestColorFormat = GraphicsFormat.None;
			return ppParams;
		}

		// Token: 0x040007C5 RID: 1989
		public Material blitMaterial;

		// Token: 0x040007C6 RID: 1990
		public GraphicsFormat requestColorFormat;
	}
}
