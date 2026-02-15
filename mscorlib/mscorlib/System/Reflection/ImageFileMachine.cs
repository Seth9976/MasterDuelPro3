using System;

namespace System.Reflection
{
	/// <summary>Identifies the platform targeted by an executable.</summary>
	// Token: 0x02000602 RID: 1538
	public enum ImageFileMachine
	{
		/// <summary>Targets a 32-bit Intel processor.</summary>
		// Token: 0x04001713 RID: 5907
		I386 = 332,
		/// <summary>Targets a 64-bit Intel processor.</summary>
		// Token: 0x04001714 RID: 5908
		IA64 = 512,
		/// <summary>Targets a 64-bit AMD processor.</summary>
		// Token: 0x04001715 RID: 5909
		AMD64 = 34404,
		/// <summary>Targets an ARM processor.</summary>
		// Token: 0x04001716 RID: 5910
		ARM = 452
	}
}
