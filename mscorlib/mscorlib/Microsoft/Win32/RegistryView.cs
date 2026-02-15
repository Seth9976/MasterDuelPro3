using System;

namespace Microsoft.Win32
{
	/// <summary>Specifies which registry view to target on a 64-bit operating system.</summary>
	// Token: 0x02000085 RID: 133
	public enum RegistryView
	{
		/// <summary>The default view.</summary>
		// Token: 0x0400026D RID: 621
		Default,
		/// <summary>The 64-bit view.</summary>
		// Token: 0x0400026E RID: 622
		Registry64 = 256,
		/// <summary>The 32-bit view.</summary>
		// Token: 0x0400026F RID: 623
		Registry32 = 512
	}
}
