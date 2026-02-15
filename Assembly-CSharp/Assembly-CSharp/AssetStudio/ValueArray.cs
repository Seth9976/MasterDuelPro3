using System;

namespace AssetStudio
{
	// Token: 0x020000CC RID: 204
	public class ValueArray
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		public ValueArray(ObjectReader reader)
		{
			int[] version = reader.version;
			if (version[0] < 5 || (version[0] == 5 && version[1] < 5))
			{
				this.m_BoolValues = reader.ReadBooleanArray();
				reader.AlignStream();
				this.m_IntValues = reader.ReadInt32Array();
				this.m_FloatValues = reader.ReadSingleArray();
			}
			if (version[0] < 4 || (version[0] == 4 && version[1] < 3))
			{
				this.m_VectorValues = reader.ReadVector4Array();
				return;
			}
			int numPosValues = reader.ReadInt32();
			this.m_PositionValues = new Vector3[numPosValues];
			for (int i = 0; i < numPosValues; i++)
			{
				this.m_PositionValues[i] = ((version[0] > 5 || (version[0] == 5 && version[1] >= 4)) ? reader.ReadVector3() : reader.ReadVector4());
			}
			this.m_QuaternionValues = reader.ReadVector4Array();
			int numScaleValues = reader.ReadInt32();
			this.m_ScaleValues = new Vector3[numScaleValues];
			for (int j = 0; j < numScaleValues; j++)
			{
				this.m_ScaleValues[j] = ((version[0] > 5 || (version[0] == 5 && version[1] >= 4)) ? reader.ReadVector3() : reader.ReadVector4());
			}
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 5))
			{
				this.m_FloatValues = reader.ReadSingleArray();
				this.m_IntValues = reader.ReadInt32Array();
				this.m_BoolValues = reader.ReadBooleanArray();
				reader.AlignStream();
			}
		}

		// Token: 0x04000635 RID: 1589
		public bool[] m_BoolValues;

		// Token: 0x04000636 RID: 1590
		public int[] m_IntValues;

		// Token: 0x04000637 RID: 1591
		public float[] m_FloatValues;

		// Token: 0x04000638 RID: 1592
		public Vector4[] m_VectorValues;

		// Token: 0x04000639 RID: 1593
		public Vector3[] m_PositionValues;

		// Token: 0x0400063A RID: 1594
		public Vector4[] m_QuaternionValues;

		// Token: 0x0400063B RID: 1595
		public Vector3[] m_ScaleValues;
	}
}
