using System;

namespace UnityEngine
{
	// Token: 0x0200015F RID: 351
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class SpaceAttribute : PropertyAttribute
	{
		// Token: 0x06000F2D RID: 3885 RVA: 0x00020182 File Offset: 0x0001E382
		public SpaceAttribute()
		{
			this.height = 8f;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00020197 File Offset: 0x0001E397
		public SpaceAttribute(float height)
		{
			this.height = height;
		}

		// Token: 0x040005F8 RID: 1528
		public readonly float height;
	}
}
