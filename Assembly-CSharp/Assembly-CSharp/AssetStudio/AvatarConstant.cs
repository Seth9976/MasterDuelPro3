using System;

namespace AssetStudio
{
	// Token: 0x020000E0 RID: 224
	public class AvatarConstant
	{
		// Token: 0x0600031C RID: 796 RVA: 0x0000EFEC File Offset: 0x0000D1EC
		public AvatarConstant(ObjectReader reader)
		{
			int[] version = reader.version;
			this.m_AvatarSkeleton = new Skeleton(reader);
			this.m_AvatarSkeletonPose = new SkeletonPose(reader);
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 3))
			{
				this.m_DefaultPose = new SkeletonPose(reader);
				this.m_SkeletonNameIDArray = reader.ReadUInt32Array();
			}
			this.m_Human = new Human(reader);
			this.m_HumanSkeletonIndexArray = reader.ReadInt32Array();
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 3))
			{
				this.m_HumanSkeletonReverseIndexArray = reader.ReadInt32Array();
			}
			this.m_RootMotionBoneIndex = reader.ReadInt32();
			this.m_RootMotionBoneX = new xform(reader);
			if (version[0] > 4 || (version[0] == 4 && version[1] >= 3))
			{
				this.m_RootMotionSkeleton = new Skeleton(reader);
				this.m_RootMotionSkeletonPose = new SkeletonPose(reader);
				this.m_RootMotionSkeletonIndexArray = reader.ReadInt32Array();
			}
		}

		// Token: 0x040006B9 RID: 1721
		public Skeleton m_AvatarSkeleton;

		// Token: 0x040006BA RID: 1722
		public SkeletonPose m_AvatarSkeletonPose;

		// Token: 0x040006BB RID: 1723
		public SkeletonPose m_DefaultPose;

		// Token: 0x040006BC RID: 1724
		public uint[] m_SkeletonNameIDArray;

		// Token: 0x040006BD RID: 1725
		public Human m_Human;

		// Token: 0x040006BE RID: 1726
		public int[] m_HumanSkeletonIndexArray;

		// Token: 0x040006BF RID: 1727
		public int[] m_HumanSkeletonReverseIndexArray;

		// Token: 0x040006C0 RID: 1728
		public int m_RootMotionBoneIndex;

		// Token: 0x040006C1 RID: 1729
		public xform m_RootMotionBoneX;

		// Token: 0x040006C2 RID: 1730
		public Skeleton m_RootMotionSkeleton;

		// Token: 0x040006C3 RID: 1731
		public SkeletonPose m_RootMotionSkeletonPose;

		// Token: 0x040006C4 RID: 1732
		public int[] m_RootMotionSkeletonIndexArray;
	}
}
