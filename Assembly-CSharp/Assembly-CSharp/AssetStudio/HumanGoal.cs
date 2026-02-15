using System;

namespace AssetStudio
{
	// Token: 0x020000A8 RID: 168
	public class HumanGoal
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		public HumanGoal(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_X = new xform(reader);
			this.m_WeightT = reader.ReadSingle();
			this.m_WeightR = reader.ReadSingle();
			if (version[0] >= 5)
			{
				this.m_HintT = ((version[0] > 5 || (version[0] == 5 && version[1] >= 4)) ? reader.ReadVector3() : reader.ReadVector4());
				this.m_HintWeightT = reader.ReadSingle();
			}
		}

		// Token: 0x04000570 RID: 1392
		public xform m_X;

		// Token: 0x04000571 RID: 1393
		public float m_WeightT;

		// Token: 0x04000572 RID: 1394
		public float m_WeightR;

		// Token: 0x04000573 RID: 1395
		public Vector3 m_HintT;

		// Token: 0x04000574 RID: 1396
		public float m_HintWeightT;
	}
}
