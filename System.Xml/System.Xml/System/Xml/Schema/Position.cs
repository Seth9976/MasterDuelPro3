using System;

namespace System.Xml.Schema
{
	// Token: 0x0200021C RID: 540
	internal struct Position
	{
		// Token: 0x06001A97 RID: 6807 RVA: 0x0009A7DC File Offset: 0x000989DC
		public Position(int symbol, object particle)
		{
			this.symbol = symbol;
			this.particle = particle;
		}

		// Token: 0x04000B64 RID: 2916
		public int symbol;

		// Token: 0x04000B65 RID: 2917
		public object particle;
	}
}
