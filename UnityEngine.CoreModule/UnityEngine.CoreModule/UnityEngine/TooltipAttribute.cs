using System;

namespace UnityEngine
{
	// Token: 0x0200015E RID: 350
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	public class TooltipAttribute : PropertyAttribute
	{
		// Token: 0x06000F2C RID: 3884 RVA: 0x00020171 File Offset: 0x0001E371
		public TooltipAttribute(string tooltip)
		{
			this.tooltip = tooltip;
		}

		// Token: 0x040005F7 RID: 1527
		public readonly string tooltip;
	}
}
