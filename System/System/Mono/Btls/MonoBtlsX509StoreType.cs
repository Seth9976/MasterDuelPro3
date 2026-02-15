using System;

namespace Mono.Btls
{
	// Token: 0x020000CF RID: 207
	internal enum MonoBtlsX509StoreType
	{
		// Token: 0x0400031F RID: 799
		Custom,
		// Token: 0x04000320 RID: 800
		MachineTrustedRoots,
		// Token: 0x04000321 RID: 801
		MachineIntermediateCA,
		// Token: 0x04000322 RID: 802
		MachineUntrusted,
		// Token: 0x04000323 RID: 803
		UserTrustedRoots,
		// Token: 0x04000324 RID: 804
		UserIntermediateCA,
		// Token: 0x04000325 RID: 805
		UserUntrusted
	}
}
