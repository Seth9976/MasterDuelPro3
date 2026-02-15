using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000069 RID: 105
	[AttributeUsage(AttributeTargets.Class)]
	public class CustomStyleAttribute : Attribute
	{
		// Token: 0x06000317 RID: 791 RVA: 0x0000A4E3 File Offset: 0x000086E3
		public CustomStyleAttribute(string ussStyle)
		{
			this.ussStyle = ussStyle;
		}

		// Token: 0x0400016B RID: 363
		public readonly string ussStyle;
	}
}
