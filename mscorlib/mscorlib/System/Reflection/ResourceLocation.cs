using System;

namespace System.Reflection
{
	/// <summary>Specifies the resource location.</summary>
	// Token: 0x0200061B RID: 1563
	[Flags]
	public enum ResourceLocation
	{
		/// <summary>Specifies that the resource is contained in another assembly.</summary>
		// Token: 0x0400178A RID: 6026
		ContainedInAnotherAssembly = 2,
		/// <summary>Specifies that the resource is contained in the manifest file.</summary>
		// Token: 0x0400178B RID: 6027
		ContainedInManifestFile = 4,
		/// <summary>Specifies an embedded (that is, non-linked) resource.</summary>
		// Token: 0x0400178C RID: 6028
		Embedded = 1
	}
}
