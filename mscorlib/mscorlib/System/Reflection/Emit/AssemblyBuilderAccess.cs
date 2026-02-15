using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines the access modes for a dynamic assembly. </summary>
	// Token: 0x02000657 RID: 1623
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum AssemblyBuilderAccess
	{
		/// <summary>The dynamic assembly can be executed, but not saved.</summary>
		// Token: 0x0400191B RID: 6427
		Run = 1,
		/// <summary>The dynamic assembly can be saved, but not executed.</summary>
		// Token: 0x0400191C RID: 6428
		Save = 2,
		/// <summary>The dynamic assembly can be executed and saved.</summary>
		// Token: 0x0400191D RID: 6429
		RunAndSave = 3,
		/// <summary>The dynamic assembly is loaded into the reflection-only context, and cannot be executed.</summary>
		// Token: 0x0400191E RID: 6430
		ReflectionOnly = 6,
		/// <summary>The dynamic assembly can be unloaded and its memory reclaimed, subject to the restrictions described in Collectible Assemblies for Dynamic Type Generation.</summary>
		// Token: 0x0400191F RID: 6431
		RunAndCollect = 9
	}
}
