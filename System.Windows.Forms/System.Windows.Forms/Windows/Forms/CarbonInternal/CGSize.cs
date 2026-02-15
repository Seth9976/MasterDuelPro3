using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020003A9 RID: 937
	internal struct CGSize
	{
		// Token: 0x06001E28 RID: 7720 RVA: 0x00095AB5 File Offset: 0x00093CB5
		public CGSize(int w, int h)
		{
			this.width = (float)w;
			this.height = (float)h;
		}

		// Token: 0x04001D43 RID: 7491
		public float width;

		// Token: 0x04001D44 RID: 7492
		public float height;
	}
}
