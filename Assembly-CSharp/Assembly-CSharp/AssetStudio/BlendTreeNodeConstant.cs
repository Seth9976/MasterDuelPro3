using System;

namespace AssetStudio
{
	// Token: 0x020000C6 RID: 198
	public class BlendTreeNodeConstant
	{
		// Token: 0x06000304 RID: 772 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		public BlendTreeNodeConstant(ObjectReader reader)
		{
			int[] version = reader.version;
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 1))
			{
				this.m_BlendType = reader.ReadUInt32();
			}
			this.m_BlendEventID = reader.ReadUInt32();
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 1))
			{
				this.m_BlendEventYID = reader.ReadUInt32();
			}
			this.m_ChildIndices = reader.ReadUInt32Array();
			if (version[0] < 4 || (version[0] == 4 && version[1] < 1))
			{
				this.m_ChildThresholdArray = reader.ReadSingleArray();
			}
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 1))
			{
				this.m_Blend1dData = new Blend1dDataConstant(reader);
				this.m_Blend2dData = new Blend2dDataConstant(reader);
			}
			if (version[0] >= 5)
			{
				this.m_BlendDirectData = new BlendDirectDataConstant(reader);
			}
			this.m_ClipID = reader.ReadUInt32();
			if (version[0] == 4 && version[1] >= 5)
			{
				this.m_ClipIndex = reader.ReadUInt32();
			}
			this.m_Duration = reader.ReadSingle();
			if (version[0] > 4 || (version[0] == 4 && version[1] > 1) || (version[0] == 4 && version[1] == 1 && version[2] >= 3))
			{
				this.m_CycleOffset = reader.ReadSingle();
				this.m_Mirror = reader.ReadBoolean();
				reader.AlignStream();
			}
		}

		// Token: 0x0400060B RID: 1547
		public uint m_BlendType;

		// Token: 0x0400060C RID: 1548
		public uint m_BlendEventID;

		// Token: 0x0400060D RID: 1549
		public uint m_BlendEventYID;

		// Token: 0x0400060E RID: 1550
		public uint[] m_ChildIndices;

		// Token: 0x0400060F RID: 1551
		public float[] m_ChildThresholdArray;

		// Token: 0x04000610 RID: 1552
		public Blend1dDataConstant m_Blend1dData;

		// Token: 0x04000611 RID: 1553
		public Blend2dDataConstant m_Blend2dData;

		// Token: 0x04000612 RID: 1554
		public BlendDirectDataConstant m_BlendDirectData;

		// Token: 0x04000613 RID: 1555
		public uint m_ClipID;

		// Token: 0x04000614 RID: 1556
		public uint m_ClipIndex;

		// Token: 0x04000615 RID: 1557
		public float m_Duration;

		// Token: 0x04000616 RID: 1558
		public float m_CycleOffset;

		// Token: 0x04000617 RID: 1559
		public bool m_Mirror;
	}
}
