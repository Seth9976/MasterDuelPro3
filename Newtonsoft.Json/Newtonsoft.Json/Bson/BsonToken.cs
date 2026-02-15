using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020001DA RID: 474
	internal abstract class BsonToken
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001000 RID: 4096
		public abstract BsonType Type { get; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x00046185 File Offset: 0x00044385
		// (set) Token: 0x06001002 RID: 4098 RVA: 0x0004618D File Offset: 0x0004438D
		public BsonToken Parent { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x00046196 File Offset: 0x00044396
		// (set) Token: 0x06001004 RID: 4100 RVA: 0x0004619E File Offset: 0x0004439E
		public int CalculatedSize { get; set; }
	}
}
