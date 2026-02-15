using System;

namespace System.Reflection
{
	/// <summary>Provides information about an <see cref="T:System.Reflection.Assembly" /> reference.</summary>
	// Token: 0x020005EC RID: 1516
	[Flags]
	public enum AssemblyNameFlags
	{
		/// <summary>Specifies that no flags are in effect.</summary>
		// Token: 0x040016C2 RID: 5826
		None = 0,
		/// <summary>Specifies that a public key is formed from the full public key rather than the public key token.</summary>
		// Token: 0x040016C3 RID: 5827
		PublicKey = 1,
		/// <summary>Specifies that just-in-time (JIT) compiler optimization is disabled for the assembly. This is the exact opposite of the meaning that is suggested by the member name.</summary>
		// Token: 0x040016C4 RID: 5828
		EnableJITcompileOptimizer = 16384,
		/// <summary>Specifies that just-in-time (JIT) compiler tracking is enabled for the assembly.</summary>
		// Token: 0x040016C5 RID: 5829
		EnableJITcompileTracking = 32768,
		/// <summary>Specifies that the assembly can be retargeted at runtime to an assembly from a different publisher. This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x040016C6 RID: 5830
		Retargetable = 256
	}
}
