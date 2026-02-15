using System;

namespace Spine
{
	// Token: 0x02000005 RID: 5
	public static class SpineSkeletonExtensions
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002768 File Offset: 0x00000968
		public static bool IsWeighted(this VertexAttachment va)
		{
			return va.Bones != null && va.Bones.Length != 0;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000277E File Offset: 0x0000097E
		public static bool InheritsRotation(this Inherit mode)
		{
			return mode == Inherit.Normal || mode == Inherit.NoScale || mode == Inherit.NoScaleOrReflection;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000278D File Offset: 0x0000098D
		public static bool InheritsScale(this Inherit mode)
		{
			return mode == Inherit.Normal || mode == Inherit.NoRotationOrReflection;
		}
	}
}
