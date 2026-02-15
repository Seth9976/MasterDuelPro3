using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020003AB RID: 939
	internal struct CGPoint
	{
		// Token: 0x06001E29 RID: 7721 RVA: 0x00095AC7 File Offset: 0x00093CC7
		public CGPoint(int x, int y)
		{
			this.x = (float)x;
			this.y = (float)y;
		}

		// Token: 0x04001D47 RID: 7495
		public float x;

		// Token: 0x04001D48 RID: 7496
		public float y;
	}
}
