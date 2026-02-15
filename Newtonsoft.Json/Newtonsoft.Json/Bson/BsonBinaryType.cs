using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020001D4 RID: 468
	internal enum BsonBinaryType : byte
	{
		// Token: 0x0400082B RID: 2091
		Binary,
		// Token: 0x0400082C RID: 2092
		Function,
		// Token: 0x0400082D RID: 2093
		[Obsolete("This type has been deprecated in the BSON specification. Use Binary instead.")]
		BinaryOld,
		// Token: 0x0400082E RID: 2094
		[Obsolete("This type has been deprecated in the BSON specification. Use Uuid instead.")]
		UuidOld,
		// Token: 0x0400082F RID: 2095
		Uuid,
		// Token: 0x04000830 RID: 2096
		Md5,
		// Token: 0x04000831 RID: 2097
		UserDefined = 128
	}
}
