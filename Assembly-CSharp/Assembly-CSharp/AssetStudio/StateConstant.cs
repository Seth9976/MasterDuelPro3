using System;

namespace AssetStudio
{
	// Token: 0x020000C8 RID: 200
	public class StateConstant
	{
		// Token: 0x06000306 RID: 774 RVA: 0x0000E284 File Offset: 0x0000C484
		public StateConstant(ObjectReader reader)
		{
			int[] version = reader.version;
			int numTransistions = reader.ReadInt32();
			this.m_TransitionConstantArray = new TransitionConstant[numTransistions];
			for (int i = 0; i < numTransistions; i++)
			{
				this.m_TransitionConstantArray[i] = new TransitionConstant(reader);
			}
			this.m_BlendTreeConstantIndexArray = reader.ReadInt32Array();
			if (version[0] < 5 || (version[0] == 5 && version[1] < 2))
			{
				int numInfos = reader.ReadInt32();
				this.m_LeafInfoArray = new LeafInfoConstant[numInfos];
				for (int j = 0; j < numInfos; j++)
				{
					this.m_LeafInfoArray[j] = new LeafInfoConstant(reader);
				}
			}
			int numBlends = reader.ReadInt32();
			this.m_BlendTreeConstantArray = new BlendTreeConstant[numBlends];
			for (int k = 0; k < numBlends; k++)
			{
				this.m_BlendTreeConstantArray[k] = new BlendTreeConstant(reader);
			}
			this.m_NameID = reader.ReadUInt32();
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 3))
			{
				this.m_PathID = reader.ReadUInt32();
			}
			if (version[0] >= 5)
			{
				this.m_FullPathID = reader.ReadUInt32();
			}
			this.m_TagID = reader.ReadUInt32();
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 1))
			{
				this.m_SpeedParamID = reader.ReadUInt32();
				this.m_MirrorParamID = reader.ReadUInt32();
				this.m_CycleOffsetParamID = reader.ReadUInt32();
			}
			if (version[0] > 2017 || (version[0] == 2017 && version[1] >= 2))
			{
				reader.ReadUInt32();
			}
			this.m_Speed = reader.ReadSingle();
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 1))
			{
				this.m_CycleOffset = reader.ReadSingle();
			}
			this.m_IKOnFeet = reader.ReadBoolean();
			if (version[0] >= 5)
			{
				this.m_WriteDefaultValues = reader.ReadBoolean();
			}
			this.m_Loop = reader.ReadBoolean();
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 1))
			{
				this.m_Mirror = reader.ReadBoolean();
			}
			reader.AlignStream();
		}

		// Token: 0x0400061A RID: 1562
		public TransitionConstant[] m_TransitionConstantArray;

		// Token: 0x0400061B RID: 1563
		public int[] m_BlendTreeConstantIndexArray;

		// Token: 0x0400061C RID: 1564
		public LeafInfoConstant[] m_LeafInfoArray;

		// Token: 0x0400061D RID: 1565
		public BlendTreeConstant[] m_BlendTreeConstantArray;

		// Token: 0x0400061E RID: 1566
		public uint m_NameID;

		// Token: 0x0400061F RID: 1567
		public uint m_PathID;

		// Token: 0x04000620 RID: 1568
		public uint m_FullPathID;

		// Token: 0x04000621 RID: 1569
		public uint m_TagID;

		// Token: 0x04000622 RID: 1570
		public uint m_SpeedParamID;

		// Token: 0x04000623 RID: 1571
		public uint m_MirrorParamID;

		// Token: 0x04000624 RID: 1572
		public uint m_CycleOffsetParamID;

		// Token: 0x04000625 RID: 1573
		public float m_Speed;

		// Token: 0x04000626 RID: 1574
		public float m_CycleOffset;

		// Token: 0x04000627 RID: 1575
		public bool m_IKOnFeet;

		// Token: 0x04000628 RID: 1576
		public bool m_WriteDefaultValues;

		// Token: 0x04000629 RID: 1577
		public bool m_Loop;

		// Token: 0x0400062A RID: 1578
		public bool m_Mirror;
	}
}
