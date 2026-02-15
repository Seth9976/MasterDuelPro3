using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000065 RID: 101
	internal class TimeFieldAttribute : PropertyAttribute
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000A4CC File Offset: 0x000086CC
		public TimeFieldAttribute.UseEditMode useEditMode { get; }

		// Token: 0x06000314 RID: 788 RVA: 0x0000A4D4 File Offset: 0x000086D4
		public TimeFieldAttribute(TimeFieldAttribute.UseEditMode useEditMode = TimeFieldAttribute.UseEditMode.ApplyEditMode)
		{
			this.useEditMode = useEditMode;
		}

		// Token: 0x02000066 RID: 102
		public enum UseEditMode
		{
			// Token: 0x04000169 RID: 361
			None,
			// Token: 0x0400016A RID: 362
			ApplyEditMode
		}
	}
}
