using System;

namespace AssetStudio
{
	// Token: 0x020000C0 RID: 192
	public class TransitionConstant
	{
		// Token: 0x060002FE RID: 766 RVA: 0x0000DEF0 File Offset: 0x0000C0F0
		public TransitionConstant(ObjectReader reader)
		{
			int[] version = reader.version;
			int numConditions = reader.ReadInt32();
			this.m_ConditionConstantArray = new ConditionConstant[numConditions];
			for (int i = 0; i < numConditions; i++)
			{
				this.m_ConditionConstantArray[i] = new ConditionConstant(reader);
			}
			this.m_DestinationState = reader.ReadUInt32();
			if (version[0] >= 5)
			{
				this.m_FullPathID = reader.ReadUInt32();
			}
			this.m_ID = reader.ReadUInt32();
			this.m_UserID = reader.ReadUInt32();
			this.m_TransitionDuration = reader.ReadSingle();
			this.m_TransitionOffset = reader.ReadSingle();
			if (version[0] >= 5)
			{
				this.m_ExitTime = reader.ReadSingle();
				this.m_HasExitTime = reader.ReadBoolean();
				this.m_HasFixedDuration = reader.ReadBoolean();
				reader.AlignStream();
				this.m_InterruptionSource = reader.ReadInt32();
				this.m_OrderedInterruption = reader.ReadBoolean();
			}
			else
			{
				this.m_Atomic = reader.ReadBoolean();
			}
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 5))
			{
				this.m_CanTransitionToSelf = reader.ReadBoolean();
			}
			reader.AlignStream();
		}

		// Token: 0x040005F2 RID: 1522
		public ConditionConstant[] m_ConditionConstantArray;

		// Token: 0x040005F3 RID: 1523
		public uint m_DestinationState;

		// Token: 0x040005F4 RID: 1524
		public uint m_FullPathID;

		// Token: 0x040005F5 RID: 1525
		public uint m_ID;

		// Token: 0x040005F6 RID: 1526
		public uint m_UserID;

		// Token: 0x040005F7 RID: 1527
		public float m_TransitionDuration;

		// Token: 0x040005F8 RID: 1528
		public float m_TransitionOffset;

		// Token: 0x040005F9 RID: 1529
		public float m_ExitTime;

		// Token: 0x040005FA RID: 1530
		public bool m_HasExitTime;

		// Token: 0x040005FB RID: 1531
		public bool m_HasFixedDuration;

		// Token: 0x040005FC RID: 1532
		public int m_InterruptionSource;

		// Token: 0x040005FD RID: 1533
		public bool m_OrderedInterruption;

		// Token: 0x040005FE RID: 1534
		public bool m_Atomic;

		// Token: 0x040005FF RID: 1535
		public bool m_CanTransitionToSelf;
	}
}
