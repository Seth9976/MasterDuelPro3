using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020001E2 RID: 482
	internal class BsonRegex : BsonToken
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x0004630C File Offset: 0x0004450C
		// (set) Token: 0x06001020 RID: 4128 RVA: 0x00046314 File Offset: 0x00044514
		public BsonString Pattern { get; set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x0004631D File Offset: 0x0004451D
		// (set) Token: 0x06001022 RID: 4130 RVA: 0x00046325 File Offset: 0x00044525
		public BsonString Options { get; set; }

		// Token: 0x06001023 RID: 4131 RVA: 0x0004632E File Offset: 0x0004452E
		public BsonRegex(string pattern, string options)
		{
			this.Pattern = new BsonString(pattern, false);
			this.Options = new BsonString(options, false);
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x00046350 File Offset: 0x00044550
		public override BsonType Type
		{
			get
			{
				return BsonType.Regex;
			}
		}
	}
}
