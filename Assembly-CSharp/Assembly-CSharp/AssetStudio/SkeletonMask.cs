using System;

namespace AssetStudio
{
	// Token: 0x020000BD RID: 189
	public class SkeletonMask
	{
		// Token: 0x060002FB RID: 763 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
		public SkeletonMask(ObjectReader reader)
		{
			int numElements = reader.ReadInt32();
			this.m_Data = new SkeletonMaskElement[numElements];
			for (int i = 0; i < numElements; i++)
			{
				this.m_Data[i] = new SkeletonMaskElement(reader);
			}
		}

		// Token: 0x040005E4 RID: 1508
		public SkeletonMaskElement[] m_Data;
	}
}
