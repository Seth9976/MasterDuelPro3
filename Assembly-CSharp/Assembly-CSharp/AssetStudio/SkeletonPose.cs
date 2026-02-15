using System;

namespace AssetStudio
{
	// Token: 0x020000DB RID: 219
	public class SkeletonPose
	{
		// Token: 0x06000317 RID: 791 RVA: 0x0000ED40 File Offset: 0x0000CF40
		public SkeletonPose(ObjectReader reader)
		{
			int numXforms = reader.ReadInt32();
			this.m_X = new xform[numXforms];
			for (int i = 0; i < numXforms; i++)
			{
				this.m_X[i] = new xform(reader);
			}
		}

		// Token: 0x04000696 RID: 1686
		public xform[] m_X;
	}
}
