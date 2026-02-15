using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200006A RID: 106
	[AttributeUsage(AttributeTargets.Class)]
	internal class MenuCategoryAttribute : Attribute
	{
		// Token: 0x06000318 RID: 792 RVA: 0x0000A4F2 File Offset: 0x000086F2
		public MenuCategoryAttribute(string category)
		{
			this.category = category ?? string.Empty;
		}

		// Token: 0x0400016C RID: 364
		public readonly string category;
	}
}
