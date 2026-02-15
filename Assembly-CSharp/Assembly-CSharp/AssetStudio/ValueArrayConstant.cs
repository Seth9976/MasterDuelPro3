using System;

namespace AssetStudio
{
	// Token: 0x020000B1 RID: 177
	public class ValueArrayConstant
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0000D260 File Offset: 0x0000B460
		public ValueArrayConstant(ObjectReader reader)
		{
			int numVals = reader.ReadInt32();
			this.m_ValueArray = new ValueConstant[numVals];
			for (int i = 0; i < numVals; i++)
			{
				this.m_ValueArray[i] = new ValueConstant(reader);
			}
		}

		// Token: 0x04000592 RID: 1426
		public ValueConstant[] m_ValueArray;
	}
}
