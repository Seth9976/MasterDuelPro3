using System;

namespace AssetStudio
{
	// Token: 0x020000A9 RID: 169
	public class HumanPose
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		public HumanPose(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_RootX = new xform(reader);
			this.m_LookAtPosition = ((version[0] > 5 || (version[0] == 5 && version[1] >= 4)) ? reader.ReadVector3() : reader.ReadVector4());
			this.m_LookAtWeight = reader.ReadVector4();
			int numGoals = reader.ReadInt32();
			this.m_GoalArray = new HumanGoal[numGoals];
			for (int i = 0; i < numGoals; i++)
			{
				this.m_GoalArray[i] = new HumanGoal(reader);
			}
			this.m_LeftHandPose = new HandPose(reader);
			this.m_RightHandPose = new HandPose(reader);
			this.m_DoFArray = reader.ReadSingleArray();
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 2))
			{
				int numTDof = reader.ReadInt32();
				this.m_TDoFArray = new Vector3[numTDof];
				for (int j = 0; j < numTDof; j++)
				{
					this.m_TDoFArray[j] = ((version[0] > 5 || (version[0] == 5 && version[1] >= 4)) ? reader.ReadVector3() : reader.ReadVector4());
				}
			}
		}

		// Token: 0x04000575 RID: 1397
		public xform m_RootX;

		// Token: 0x04000576 RID: 1398
		public Vector3 m_LookAtPosition;

		// Token: 0x04000577 RID: 1399
		public Vector4 m_LookAtWeight;

		// Token: 0x04000578 RID: 1400
		public HumanGoal[] m_GoalArray;

		// Token: 0x04000579 RID: 1401
		public HandPose m_LeftHandPose;

		// Token: 0x0400057A RID: 1402
		public HandPose m_RightHandPose;

		// Token: 0x0400057B RID: 1403
		public float[] m_DoFArray;

		// Token: 0x0400057C RID: 1404
		public Vector3[] m_TDoFArray;
	}
}
