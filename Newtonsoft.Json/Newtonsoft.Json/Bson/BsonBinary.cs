using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020001E1 RID: 481
	internal class BsonBinary : BsonValue
	{
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x000462EA File Offset: 0x000444EA
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x000462F2 File Offset: 0x000444F2
		public BsonBinaryType BinaryType { get; set; }

		// Token: 0x0600101E RID: 4126 RVA: 0x000462FB File Offset: 0x000444FB
		public BsonBinary(byte[] value, BsonBinaryType binaryType)
			: base(value, BsonType.Binary)
		{
			this.BinaryType = binaryType;
		}
	}
}
