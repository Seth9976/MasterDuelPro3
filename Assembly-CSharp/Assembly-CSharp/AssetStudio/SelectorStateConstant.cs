using System;

namespace AssetStudio
{
	// Token: 0x020000CA RID: 202
	public class SelectorStateConstant
	{
		// Token: 0x06000308 RID: 776 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		public SelectorStateConstant(ObjectReader reader)
		{
			int numTransitions = reader.ReadInt32();
			this.m_TransitionConstantArray = new SelectorTransitionConstant[numTransitions];
			for (int i = 0; i < numTransitions; i++)
			{
				this.m_TransitionConstantArray[i] = new SelectorTransitionConstant(reader);
			}
			this.m_FullPathID = reader.ReadUInt32();
			this.m_isEntry = reader.ReadBoolean();
			reader.AlignStream();
		}

		// Token: 0x0400062D RID: 1581
		public SelectorTransitionConstant[] m_TransitionConstantArray;

		// Token: 0x0400062E RID: 1582
		public uint m_FullPathID;

		// Token: 0x0400062F RID: 1583
		public bool m_isEntry;
	}
}
