using System;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020003AD RID: 941
	internal struct HIViewID
	{
		// Token: 0x06001E2B RID: 7723 RVA: 0x00095AF6 File Offset: 0x00093CF6
		public HIViewID(uint type, uint id)
		{
			this.type = type;
			this.id = id;
		}

		// Token: 0x04001D4B RID: 7499
		public uint type;

		// Token: 0x04001D4C RID: 7500
		public uint id;
	}
}
