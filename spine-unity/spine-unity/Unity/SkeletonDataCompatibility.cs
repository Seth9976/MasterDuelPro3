using System;

namespace Spine.Unity
{
	// Token: 0x02000014 RID: 20
	public static class SkeletonDataCompatibility
	{
		// Token: 0x02000015 RID: 21
		public enum SourceType
		{
			// Token: 0x0400003D RID: 61
			Json,
			// Token: 0x0400003E RID: 62
			Binary
		}

		// Token: 0x02000016 RID: 22
		[Serializable]
		public class VersionInfo
		{
			// Token: 0x0400003F RID: 63
			public string rawVersion;

			// Token: 0x04000040 RID: 64
			public int[] version;

			// Token: 0x04000041 RID: 65
			public SkeletonDataCompatibility.SourceType sourceType;
		}

		// Token: 0x02000017 RID: 23
		[Serializable]
		public class CompatibilityProblemInfo
		{
			// Token: 0x06000075 RID: 117 RVA: 0x0000398C File Offset: 0x00001B8C
			public string DescriptionString()
			{
				if (!string.IsNullOrEmpty(this.explicitProblemDescription))
				{
					return this.explicitProblemDescription;
				}
				string compatibleVersionString = "";
				string optionalOr = null;
				foreach (int[] version in this.compatibleVersions)
				{
					compatibleVersionString += string.Format("{0}{1}.{2}", optionalOr, version[0], version[1]);
					optionalOr = " or ";
				}
				return string.Format("Skeleton data could not be loaded. Data version: {0}. Required version: {1}.\nPlease re-export skeleton data with Spine {1} or change runtime to version {2}.{3}.", new object[]
				{
					this.actualVersion.rawVersion,
					compatibleVersionString,
					this.actualVersion.version[0],
					this.actualVersion.version[1]
				});
			}

			// Token: 0x04000042 RID: 66
			public SkeletonDataCompatibility.VersionInfo actualVersion;

			// Token: 0x04000043 RID: 67
			public int[][] compatibleVersions;

			// Token: 0x04000044 RID: 68
			public string explicitProblemDescription;
		}
	}
}
