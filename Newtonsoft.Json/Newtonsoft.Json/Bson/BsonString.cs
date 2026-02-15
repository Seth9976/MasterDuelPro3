using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020001E0 RID: 480
	internal class BsonString : BsonValue
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x000462C0 File Offset: 0x000444C0
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x000462C8 File Offset: 0x000444C8
		public int ByteCount { get; set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x000462D1 File Offset: 0x000444D1
		public bool IncludeLength { get; }

		// Token: 0x0600101B RID: 4123 RVA: 0x000462D9 File Offset: 0x000444D9
		public BsonString(object value, bool includeLength)
			: base(value, BsonType.String)
		{
			this.IncludeLength = includeLength;
		}
	}
}
