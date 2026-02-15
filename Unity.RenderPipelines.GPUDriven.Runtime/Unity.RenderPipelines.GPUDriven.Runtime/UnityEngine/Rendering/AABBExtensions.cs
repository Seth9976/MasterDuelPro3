using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000007 RID: 7
	internal static class AABBExtensions
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021AC File Offset: 0x000003AC
		public static AABB ToAABB(this Bounds bounds)
		{
			return new AABB
			{
				center = bounds.center,
				extents = bounds.extents
			};
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021E8 File Offset: 0x000003E8
		public static Bounds ToBounds(this AABB aabb)
		{
			return new Bounds
			{
				center = aabb.center,
				extents = aabb.extents
			};
		}
	}
}
