using System;

namespace MDPro3
{
	// Token: 0x0200121B RID: 4635
	public class Package
	{
		// Token: 0x06008972 RID: 35186 RVA: 0x0010A59C File Offset: 0x0010879C
		public Package()
		{
			this.Function = 1;
			this.Data = new BinaryMaster(null);
		}

		// Token: 0x0400C47F RID: 50303
		public BinaryMaster Data;

		// Token: 0x0400C480 RID: 50304
		public int Function;
	}
}
