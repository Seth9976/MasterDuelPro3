using System;

namespace System.Xml
{
	// Token: 0x02000035 RID: 53
	internal abstract class IncrementalReadDecoder
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001B6 RID: 438
		internal abstract int DecodedCount { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001B7 RID: 439
		internal abstract bool IsFull { get; }

		// Token: 0x060001B8 RID: 440
		internal abstract void SetNextOutputBuffer(Array array, int offset, int len);

		// Token: 0x060001B9 RID: 441
		internal abstract int Decode(char[] chars, int startPos, int len);

		// Token: 0x060001BA RID: 442
		internal abstract int Decode(string str, int startPos, int len);

		// Token: 0x060001BB RID: 443
		internal abstract void Reset();
	}
}
