using System;

namespace AssetStudio
{
	// Token: 0x020000CD RID: 205
	public class ControllerConstant
	{
		// Token: 0x0600030B RID: 779 RVA: 0x0000E744 File Offset: 0x0000C944
		public ControllerConstant(ObjectReader reader)
		{
			int numLayers = reader.ReadInt32();
			this.m_LayerArray = new LayerConstant[numLayers];
			for (int i = 0; i < numLayers; i++)
			{
				this.m_LayerArray[i] = new LayerConstant(reader);
			}
			int numStates = reader.ReadInt32();
			this.m_StateMachineArray = new StateMachineConstant[numStates];
			for (int j = 0; j < numStates; j++)
			{
				this.m_StateMachineArray[j] = new StateMachineConstant(reader);
			}
			this.m_Values = new ValueArrayConstant(reader);
			this.m_DefaultValues = new ValueArray(reader);
		}

		// Token: 0x0400063C RID: 1596
		public LayerConstant[] m_LayerArray;

		// Token: 0x0400063D RID: 1597
		public StateMachineConstant[] m_StateMachineArray;

		// Token: 0x0400063E RID: 1598
		public ValueArrayConstant m_Values;

		// Token: 0x0400063F RID: 1599
		public ValueArray m_DefaultValues;
	}
}
