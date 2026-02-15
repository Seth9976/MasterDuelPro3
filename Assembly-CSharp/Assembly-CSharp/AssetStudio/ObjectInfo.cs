using System;

namespace AssetStudio
{
	// Token: 0x02000174 RID: 372
	public class ObjectInfo
	{
		// Token: 0x040009BC RID: 2492
		public long byteStart;

		// Token: 0x040009BD RID: 2493
		public uint byteSize;

		// Token: 0x040009BE RID: 2494
		public int typeID;

		// Token: 0x040009BF RID: 2495
		public int classID;

		// Token: 0x040009C0 RID: 2496
		public ushort isDestroyed;

		// Token: 0x040009C1 RID: 2497
		public byte stripped;

		// Token: 0x040009C2 RID: 2498
		public long m_PathID;

		// Token: 0x040009C3 RID: 2499
		public SerializedType serializedType;
	}
}
