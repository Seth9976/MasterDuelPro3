using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000063 RID: 99
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	internal class SupportsChildTracksAttribute : Attribute
	{
		// Token: 0x06000311 RID: 785 RVA: 0x0000A4B6 File Offset: 0x000086B6
		public SupportsChildTracksAttribute(Type childType = null, int levels = 2147483647)
		{
			this.childType = childType;
			this.levels = levels;
		}

		// Token: 0x04000165 RID: 357
		public readonly Type childType;

		// Token: 0x04000166 RID: 358
		public readonly int levels;
	}
}
