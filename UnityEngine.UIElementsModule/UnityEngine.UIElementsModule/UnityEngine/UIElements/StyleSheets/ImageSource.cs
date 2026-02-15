using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005B1 RID: 1457
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal struct ImageSource
	{
		// Token: 0x06002789 RID: 10121 RVA: 0x000A0EFC File Offset: 0x0009F0FC
		public bool IsNull()
		{
			return this.texture == null && this.sprite == null && this.vectorImage == null && this.renderTexture == null;
		}

		// Token: 0x040014EC RID: 5356
		public Texture2D texture;

		// Token: 0x040014ED RID: 5357
		public Sprite sprite;

		// Token: 0x040014EE RID: 5358
		public VectorImage vectorImage;

		// Token: 0x040014EF RID: 5359
		public RenderTexture renderTexture;
	}
}
