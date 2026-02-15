using System;

namespace AssetStudio
{
	// Token: 0x020000C9 RID: 201
	public class SelectorTransitionConstant
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000E468 File Offset: 0x0000C668
		public SelectorTransitionConstant(ObjectReader reader)
		{
			this.m_Destination = reader.ReadUInt32();
			int numConditions = reader.ReadInt32();
			this.m_ConditionConstantArray = new ConditionConstant[numConditions];
			for (int i = 0; i < numConditions; i++)
			{
				this.m_ConditionConstantArray[i] = new ConditionConstant(reader);
			}
		}

		// Token: 0x0400062B RID: 1579
		public uint m_Destination;

		// Token: 0x0400062C RID: 1580
		public ConditionConstant[] m_ConditionConstantArray;
	}
}
