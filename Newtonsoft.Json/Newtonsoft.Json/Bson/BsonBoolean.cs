using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020001DF RID: 479
	internal class BsonBoolean : BsonValue
	{
		// Token: 0x06001016 RID: 4118 RVA: 0x00046299 File Offset: 0x00044499
		private BsonBoolean(bool value)
			: base(value, BsonType.Boolean)
		{
		}

		// Token: 0x0400085C RID: 2140
		public static readonly BsonBoolean False = new BsonBoolean(false);

		// Token: 0x0400085D RID: 2141
		public static readonly BsonBoolean True = new BsonBoolean(true);
	}
}
