using System;

namespace AssetStudio
{
	// Token: 0x020000DF RID: 223
	public class Human
	{
		// Token: 0x0600031B RID: 795 RVA: 0x0000EE40 File Offset: 0x0000D040
		public Human(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_RootX = new xform(reader);
			this.m_Skeleton = new Skeleton(reader);
			this.m_SkeletonPose = new SkeletonPose(reader);
			this.m_LeftHand = new Hand(reader);
			this.m_RightHand = new Hand(reader);
			if (version[0] < 2018 || (version[0] == 2018 && version[1] < 2))
			{
				int numHandles = reader.ReadInt32();
				this.m_Handles = new Handle[numHandles];
				for (int i = 0; i < numHandles; i++)
				{
					this.m_Handles[i] = new Handle(reader);
				}
				int numColliders = reader.ReadInt32();
				this.m_ColliderArray = new Collider[numColliders];
				for (int j = 0; j < numColliders; j++)
				{
					this.m_ColliderArray[j] = new Collider(reader);
				}
			}
			this.m_HumanBoneIndex = reader.ReadInt32Array();
			this.m_HumanBoneMass = reader.ReadSingleArray();
			if (version[0] < 2018 || (version[0] == 2018 && version[1] < 2))
			{
				this.m_ColliderIndex = reader.ReadInt32Array();
			}
			this.m_Scale = reader.ReadSingle();
			this.m_ArmTwist = reader.ReadSingle();
			this.m_ForeArmTwist = reader.ReadSingle();
			this.m_UpperLegTwist = reader.ReadSingle();
			this.m_LegTwist = reader.ReadSingle();
			this.m_ArmStretch = reader.ReadSingle();
			this.m_LegStretch = reader.ReadSingle();
			this.m_FeetSpacing = reader.ReadSingle();
			this.m_HasLeftHand = reader.ReadBoolean();
			this.m_HasRightHand = reader.ReadBoolean();
			if (version[0] > 5 || (version[0] == 5 && version[1] >= 2))
			{
				this.m_HasTDoF = reader.ReadBoolean();
			}
			reader.AlignStream();
		}

		// Token: 0x040006A4 RID: 1700
		public xform m_RootX;

		// Token: 0x040006A5 RID: 1701
		public Skeleton m_Skeleton;

		// Token: 0x040006A6 RID: 1702
		public SkeletonPose m_SkeletonPose;

		// Token: 0x040006A7 RID: 1703
		public Hand m_LeftHand;

		// Token: 0x040006A8 RID: 1704
		public Hand m_RightHand;

		// Token: 0x040006A9 RID: 1705
		public Handle[] m_Handles;

		// Token: 0x040006AA RID: 1706
		public Collider[] m_ColliderArray;

		// Token: 0x040006AB RID: 1707
		public int[] m_HumanBoneIndex;

		// Token: 0x040006AC RID: 1708
		public float[] m_HumanBoneMass;

		// Token: 0x040006AD RID: 1709
		public int[] m_ColliderIndex;

		// Token: 0x040006AE RID: 1710
		public float m_Scale;

		// Token: 0x040006AF RID: 1711
		public float m_ArmTwist;

		// Token: 0x040006B0 RID: 1712
		public float m_ForeArmTwist;

		// Token: 0x040006B1 RID: 1713
		public float m_UpperLegTwist;

		// Token: 0x040006B2 RID: 1714
		public float m_LegTwist;

		// Token: 0x040006B3 RID: 1715
		public float m_ArmStretch;

		// Token: 0x040006B4 RID: 1716
		public float m_LegStretch;

		// Token: 0x040006B5 RID: 1717
		public float m_FeetSpacing;

		// Token: 0x040006B6 RID: 1718
		public bool m_HasLeftHand;

		// Token: 0x040006B7 RID: 1719
		public bool m_HasRightHand;

		// Token: 0x040006B8 RID: 1720
		public bool m_HasTDoF;
	}
}
