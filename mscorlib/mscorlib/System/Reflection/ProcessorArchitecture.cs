using System;

namespace System.Reflection
{
	/// <summary>Identifies the processor and bits-per-word of the platform targeted by an executable.</summary>
	// Token: 0x02000616 RID: 1558
	public enum ProcessorArchitecture
	{
		/// <summary>An unknown or unspecified combination of processor and bits-per-word.</summary>
		// Token: 0x04001775 RID: 6005
		None,
		/// <summary>Neutral with respect to processor and bits-per-word.</summary>
		// Token: 0x04001776 RID: 6006
		MSIL,
		/// <summary>A 32-bit Intel processor, either native or in the Windows on Windows environment on a 64-bit platform (WOW64).</summary>
		// Token: 0x04001777 RID: 6007
		X86,
		/// <summary>A 64-bit Intel processor only.</summary>
		// Token: 0x04001778 RID: 6008
		IA64,
		/// <summary>A 64-bit AMD processor only.</summary>
		// Token: 0x04001779 RID: 6009
		Amd64,
		/// <summary>An ARM processor.</summary>
		// Token: 0x0400177A RID: 6010
		Arm
	}
}
