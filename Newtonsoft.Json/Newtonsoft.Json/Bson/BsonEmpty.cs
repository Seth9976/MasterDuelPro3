using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020001DD RID: 477
	internal class BsonEmpty : BsonToken
	{
		// Token: 0x06001010 RID: 4112 RVA: 0x00046243 File Offset: 0x00044443
		private BsonEmpty(BsonType type)
		{
			this.Type = type;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x00046252 File Offset: 0x00044452
		public override BsonType Type { get; }

		// Token: 0x04000857 RID: 2135
		public static readonly BsonToken Null = new BsonEmpty(BsonType.Null);

		// Token: 0x04000858 RID: 2136
		public static readonly BsonToken Undefined = new BsonEmpty(BsonType.Undefined);
	}
}
