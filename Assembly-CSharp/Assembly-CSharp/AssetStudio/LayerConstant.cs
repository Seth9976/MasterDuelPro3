using System;

namespace AssetStudio
{
	// Token: 0x020000BE RID: 190
	public class LayerConstant
	{
		// Token: 0x060002FC RID: 764 RVA: 0x0000DE08 File Offset: 0x0000C008
		public LayerConstant(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_StateMachineIndex = reader.ReadUInt32();
			this.m_StateMachineMotionSetIndex = reader.ReadUInt32();
			this.m_BodyMask = new HumanPoseMask(reader);
			this.m_SkeletonMask = new SkeletonMask(reader);
			this.m_Binding = reader.ReadUInt32();
			this.m_LayerBlendingMode = reader.ReadInt32();
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 2))
			{
				this.m_DefaultWeight = reader.ReadSingle();
			}
			this.m_IKPass = reader.ReadBoolean();
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 2))
			{
				this.m_SyncedLayerAffectsTiming = reader.ReadBoolean();
			}
			reader.AlignStream();
		}

		// Token: 0x040005E5 RID: 1509
		public uint m_StateMachineIndex;

		// Token: 0x040005E6 RID: 1510
		public uint m_StateMachineMotionSetIndex;

		// Token: 0x040005E7 RID: 1511
		public HumanPoseMask m_BodyMask;

		// Token: 0x040005E8 RID: 1512
		public SkeletonMask m_SkeletonMask;

		// Token: 0x040005E9 RID: 1513
		public uint m_Binding;

		// Token: 0x040005EA RID: 1514
		public int m_LayerBlendingMode;

		// Token: 0x040005EB RID: 1515
		public float m_DefaultWeight;

		// Token: 0x040005EC RID: 1516
		public bool m_IKPass;

		// Token: 0x040005ED RID: 1517
		public bool m_SyncedLayerAffectsTiming;
	}
}
