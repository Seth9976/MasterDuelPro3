using System;

namespace AssetStudio
{
	// Token: 0x020000A0 RID: 160
	public class CompressedAnimationCurve
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x0000CB00 File Offset: 0x0000AD00
		public CompressedAnimationCurve(ObjectReader reader)
		{
			this.m_Path = reader.ReadAlignedString();
			this.m_Times = new PackedIntVector(reader);
			this.m_Values = new PackedQuatVector(reader);
			this.m_Slopes = new PackedFloatVector(reader);
			this.m_PreInfinity = reader.ReadInt32();
			this.m_PostInfinity = reader.ReadInt32();
		}

		// Token: 0x04000551 RID: 1361
		public string m_Path;

		// Token: 0x04000552 RID: 1362
		public PackedIntVector m_Times;

		// Token: 0x04000553 RID: 1363
		public PackedQuatVector m_Values;

		// Token: 0x04000554 RID: 1364
		public PackedFloatVector m_Slopes;

		// Token: 0x04000555 RID: 1365
		public int m_PreInfinity;

		// Token: 0x04000556 RID: 1366
		public int m_PostInfinity;
	}
}
