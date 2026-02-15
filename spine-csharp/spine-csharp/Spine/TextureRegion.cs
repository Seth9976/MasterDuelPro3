using System;

namespace Spine
{
	// Token: 0x02000083 RID: 131
	public class TextureRegion
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000A10D File Offset: 0x0000830D
		public virtual int OriginalWidth
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000A11E File Offset: 0x0000831E
		public virtual int OriginalHeight
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x040002DA RID: 730
		public int width;

		// Token: 0x040002DB RID: 731
		public int height;

		// Token: 0x040002DC RID: 732
		public float u;

		// Token: 0x040002DD RID: 733
		public float v;

		// Token: 0x040002DE RID: 734
		public float u2;

		// Token: 0x040002DF RID: 735
		public float v2;
	}
}
