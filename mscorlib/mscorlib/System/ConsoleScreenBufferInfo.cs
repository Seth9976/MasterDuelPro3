using System;

namespace System
{
	// Token: 0x02000201 RID: 513
	internal struct ConsoleScreenBufferInfo
	{
		// Token: 0x040009A9 RID: 2473
		public Coord Size;

		// Token: 0x040009AA RID: 2474
		public Coord CursorPosition;

		// Token: 0x040009AB RID: 2475
		public short Attribute;

		// Token: 0x040009AC RID: 2476
		public SmallRect Window;

		// Token: 0x040009AD RID: 2477
		public Coord MaxWindowSize;
	}
}
