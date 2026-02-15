using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001B9 RID: 441
	internal class ImportStructWorkItem
	{
		// Token: 0x0600153F RID: 5439 RVA: 0x0006908C File Offset: 0x0006728C
		internal ImportStructWorkItem(StructModel model, StructMapping mapping)
		{
			this.model = model;
			this.mapping = mapping;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x000690A2 File Offset: 0x000672A2
		internal StructModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x000690AA File Offset: 0x000672AA
		internal StructMapping Mapping
		{
			get
			{
				return this.mapping;
			}
		}

		// Token: 0x04000995 RID: 2453
		private StructModel model;

		// Token: 0x04000996 RID: 2454
		private StructMapping mapping;
	}
}
