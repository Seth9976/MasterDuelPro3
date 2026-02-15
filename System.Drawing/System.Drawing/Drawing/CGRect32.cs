using System;

namespace System.Drawing
{
	// Token: 0x02000072 RID: 114
	internal struct CGRect32
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x0000D6AC File Offset: 0x0000B8AC
		public CGRect32(float x, float y, float width, float height)
		{
			this.origin.x = x;
			this.origin.y = y;
			this.size.width = width;
			this.size.height = height;
		}

		// Token: 0x04000211 RID: 529
		public CGPoint32 origin;

		// Token: 0x04000212 RID: 530
		public CGSize32 size;
	}
}
