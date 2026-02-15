using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200051A RID: 1306
	internal class DrawParams
	{
		// Token: 0x06002443 RID: 9283 RVA: 0x00088AAC File Offset: 0x00086CAC
		public void Reset()
		{
			this.view.Clear();
			this.view.Push(Matrix4x4.identity);
			this.scissor.Clear();
			this.scissor.Push(DrawParams.k_UnlimitedRect);
			this.renderTexture.Clear();
			this.defaultMaterial.Clear();
		}

		// Token: 0x040010FD RID: 4349
		internal static readonly Rect k_UnlimitedRect = new Rect(-100000f, -100000f, 200000f, 200000f);

		// Token: 0x040010FE RID: 4350
		internal static readonly Rect k_FullNormalizedRect = new Rect(-1f, -1f, 2f, 2f);

		// Token: 0x040010FF RID: 4351
		internal readonly Stack<Matrix4x4> view = new Stack<Matrix4x4>(8);

		// Token: 0x04001100 RID: 4352
		internal readonly Stack<Rect> scissor = new Stack<Rect>(8);

		// Token: 0x04001101 RID: 4353
		internal readonly List<RenderTexture> renderTexture = new List<RenderTexture>(8);

		// Token: 0x04001102 RID: 4354
		internal readonly List<Material> defaultMaterial = new List<Material>(8);
	}
}
