using System;

namespace AssetStudio
{
	// Token: 0x020000BC RID: 188
	public class SkeletonMaskElement
	{
		// Token: 0x060002FA RID: 762 RVA: 0x0000DDA8 File Offset: 0x0000BFA8
		public SkeletonMaskElement(ObjectReader reader)
		{
			this.m_PathHash = reader.ReadUInt32();
			this.m_Weight = reader.ReadSingle();
		}

		// Token: 0x040005E2 RID: 1506
		public uint m_PathHash;

		// Token: 0x040005E3 RID: 1507
		public float m_Weight;
	}
}
