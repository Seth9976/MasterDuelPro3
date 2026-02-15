using System;

namespace System.Reflection
{
	/// <summary>Identifies the nature of the code in an executable file.</summary>
	// Token: 0x02000615 RID: 1557
	[Flags]
	public enum PortableExecutableKinds
	{
		/// <summary>The file is not in portable executable (PE) file format.</summary>
		// Token: 0x0400176E RID: 5998
		NotAPortableExecutableImage = 0,
		/// <summary>The executable contains only Microsoft intermediate language (MSIL), and is therefore neutral with respect to 32-bit or 64-bit platforms.</summary>
		// Token: 0x0400176F RID: 5999
		ILOnly = 1,
		/// <summary>The executable can be run on a 32-bit platform, or in the 32-bit Windows on Windows (WOW) environment on a 64-bit platform.</summary>
		// Token: 0x04001770 RID: 6000
		Required32Bit = 2,
		/// <summary>The executable requires a 64-bit platform.</summary>
		// Token: 0x04001771 RID: 6001
		PE32Plus = 4,
		/// <summary>The executable contains pure unmanaged code.</summary>
		// Token: 0x04001772 RID: 6002
		Unmanaged32Bit = 8,
		/// <summary>The executable is platform-agnostic but should be run on a 32-bit platform whenever possible.</summary>
		// Token: 0x04001773 RID: 6003
		Preferred32Bit = 16
	}
}
