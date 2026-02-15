using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000013 RID: 19
	internal static class MatchTargetFieldConstants
	{
		// Token: 0x06000048 RID: 72 RVA: 0x000029C6 File Offset: 0x00000BC6
		public static bool HasAny(this MatchTargetFields me, MatchTargetFields fields)
		{
			return (me & fields) != MatchTargetFieldConstants.None;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029D5 File Offset: 0x00000BD5
		public static MatchTargetFields Toggle(this MatchTargetFields me, MatchTargetFields flag)
		{
			return me ^ flag;
		}

		// Token: 0x04000043 RID: 67
		public static MatchTargetFields All = MatchTargetFields.PositionX | MatchTargetFields.PositionY | MatchTargetFields.PositionZ | MatchTargetFields.RotationX | MatchTargetFields.RotationY | MatchTargetFields.RotationZ;

		// Token: 0x04000044 RID: 68
		public static MatchTargetFields None = (MatchTargetFields)0;

		// Token: 0x04000045 RID: 69
		public static MatchTargetFields Position = MatchTargetFields.PositionX | MatchTargetFields.PositionY | MatchTargetFields.PositionZ;

		// Token: 0x04000046 RID: 70
		public static MatchTargetFields Rotation = MatchTargetFields.RotationX | MatchTargetFields.RotationY | MatchTargetFields.RotationZ;
	}
}
