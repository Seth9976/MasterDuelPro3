using System;

namespace AssetStudio
{
	// Token: 0x020000CB RID: 203
	public class StateMachineConstant
	{
		// Token: 0x06000309 RID: 777 RVA: 0x0000E514 File Offset: 0x0000C714
		public StateMachineConstant(ObjectReader reader)
		{
			int[] version = reader.version;
			int numStates = reader.ReadInt32();
			this.m_StateConstantArray = new StateConstant[numStates];
			for (int i = 0; i < numStates; i++)
			{
				this.m_StateConstantArray[i] = new StateConstant(reader);
			}
			int numAnyStates = reader.ReadInt32();
			this.m_AnyStateTransitionConstantArray = new TransitionConstant[numAnyStates];
			for (int j = 0; j < numAnyStates; j++)
			{
				this.m_AnyStateTransitionConstantArray[j] = new TransitionConstant(reader);
			}
			if (version[0] >= 5)
			{
				int numSelectors = reader.ReadInt32();
				this.m_SelectorStateConstantArray = new SelectorStateConstant[numSelectors];
				for (int k = 0; k < numSelectors; k++)
				{
					this.m_SelectorStateConstantArray[k] = new SelectorStateConstant(reader);
				}
			}
			this.m_DefaultState = reader.ReadUInt32();
			this.m_MotionSetCount = reader.ReadUInt32();
		}

		// Token: 0x04000630 RID: 1584
		public StateConstant[] m_StateConstantArray;

		// Token: 0x04000631 RID: 1585
		public TransitionConstant[] m_AnyStateTransitionConstantArray;

		// Token: 0x04000632 RID: 1586
		public SelectorStateConstant[] m_SelectorStateConstantArray;

		// Token: 0x04000633 RID: 1587
		public uint m_DefaultState;

		// Token: 0x04000634 RID: 1588
		public uint m_MotionSetCount;
	}
}
