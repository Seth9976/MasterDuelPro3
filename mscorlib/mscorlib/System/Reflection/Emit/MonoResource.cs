using System;
using System.IO;

namespace System.Reflection.Emit
{
	// Token: 0x02000654 RID: 1620
	internal struct MonoResource
	{
		// Token: 0x040018E2 RID: 6370
		public byte[] data;

		// Token: 0x040018E3 RID: 6371
		public string name;

		// Token: 0x040018E4 RID: 6372
		public string filename;

		// Token: 0x040018E5 RID: 6373
		public ResourceAttributes attrs;

		// Token: 0x040018E6 RID: 6374
		public int offset;

		// Token: 0x040018E7 RID: 6375
		public Stream stream;
	}
}
