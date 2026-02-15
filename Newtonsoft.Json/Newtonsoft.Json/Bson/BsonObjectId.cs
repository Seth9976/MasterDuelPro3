using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020001D6 RID: 470
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonObjectId
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00045519 File Offset: 0x00043719
		public byte[] Value { get; }

		// Token: 0x06000FDC RID: 4060 RVA: 0x00045521 File Offset: 0x00043721
		public BsonObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw new ArgumentException("An ObjectId must be 12 bytes", "value");
			}
			this.Value = value;
		}
	}
}
