using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020001E3 RID: 483
	internal class BsonProperty
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x00046354 File Offset: 0x00044554
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x0004635C File Offset: 0x0004455C
		public BsonString Name { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x00046365 File Offset: 0x00044565
		// (set) Token: 0x06001028 RID: 4136 RVA: 0x0004636D File Offset: 0x0004456D
		public BsonToken Value { get; set; }
	}
}
