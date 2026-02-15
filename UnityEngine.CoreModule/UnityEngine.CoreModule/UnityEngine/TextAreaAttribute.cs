using System;

namespace UnityEngine
{
	// Token: 0x02000164 RID: 356
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class TextAreaAttribute : PropertyAttribute
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x000201F3 File Offset: 0x0001E3F3
		public TextAreaAttribute()
		{
			this.minLines = 3;
			this.maxLines = 3;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0002020B File Offset: 0x0001E40B
		public TextAreaAttribute(int minLines, int maxLines)
		{
			this.minLines = minLines;
			this.maxLines = maxLines;
		}

		// Token: 0x040005FE RID: 1534
		public readonly int minLines;

		// Token: 0x040005FF RID: 1535
		public readonly int maxLines;
	}
}
