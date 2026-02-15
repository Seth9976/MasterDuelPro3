using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020003AC RID: 940
	internal struct HIRect
	{
		// Token: 0x06001E2A RID: 7722 RVA: 0x00095AD9 File Offset: 0x00093CD9
		public HIRect(int x, int y, int w, int h)
		{
			this.origin = new CGPoint(x, y);
			this.size = new CGSize(w, h);
		}

		// Token: 0x04001D49 RID: 7497
		public CGPoint origin;

		// Token: 0x04001D4A RID: 7498
		public CGSize size;
	}
}
