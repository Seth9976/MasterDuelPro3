using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000060 RID: 96
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class ReloadAttribute : Attribute
	{
		// Token: 0x060004DC RID: 1244 RVA: 0x00002050 File Offset: 0x00000250
		public ReloadAttribute(string[] paths, ReloadAttribute.Package package = ReloadAttribute.Package.Root)
		{
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00009287 File Offset: 0x00007487
		public ReloadAttribute(string path, ReloadAttribute.Package package = ReloadAttribute.Package.Root)
			: this(new string[] { path }, package)
		{
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00002050 File Offset: 0x00000250
		public ReloadAttribute(string pathFormat, int rangeMin, int rangeMax, ReloadAttribute.Package package = ReloadAttribute.Package.Root)
		{
		}

		// Token: 0x02000061 RID: 97
		public enum Package
		{
			// Token: 0x0400013D RID: 317
			Builtin,
			// Token: 0x0400013E RID: 318
			Root,
			// Token: 0x0400013F RID: 319
			BuiltinExtra
		}
	}
}
