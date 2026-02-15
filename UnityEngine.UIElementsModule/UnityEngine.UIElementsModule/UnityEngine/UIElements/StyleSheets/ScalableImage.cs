using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005AF RID: 1455
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal struct ScalableImage
	{
		// Token: 0x06002788 RID: 10120 RVA: 0x000A0EB8 File Offset: 0x0009F0B8
		public override string ToString()
		{
			return string.Format("{0}: {1}, {2}: {3}", new object[] { "normalImage", this.normalImage, "highResolutionImage", this.highResolutionImage });
		}

		// Token: 0x040014E8 RID: 5352
		public Texture2D normalImage;

		// Token: 0x040014E9 RID: 5353
		public Texture2D highResolutionImage;
	}
}
