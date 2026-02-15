using System;

namespace Percy
{
	// Token: 0x020011D9 RID: 4569
	internal class Package
	{
		// Token: 0x060087D2 RID: 34770 RVA: 0x000F76DF File Offset: 0x000F58DF
		public Package()
		{
			this.Fuction = 0;
			this.Data = new BinaryMaster(null);
		}

		// Token: 0x0400C24D RID: 49741
		public BinaryMaster Data;

		// Token: 0x0400C24E RID: 49742
		public int Fuction;
	}
}
