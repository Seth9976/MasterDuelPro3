using System;

namespace Mono.Security.X509
{
	// Token: 0x0200001D RID: 29
	[Flags]
	[Serializable]
	public enum X509ChainStatusFlags
	{
		// Token: 0x04000079 RID: 121
		InvalidBasicConstraints = 1024,
		// Token: 0x0400007A RID: 122
		NoError = 0,
		// Token: 0x0400007B RID: 123
		NotSignatureValid = 8,
		// Token: 0x0400007C RID: 124
		NotTimeNested = 2,
		// Token: 0x0400007D RID: 125
		NotTimeValid = 1,
		// Token: 0x0400007E RID: 126
		PartialChain = 65536,
		// Token: 0x0400007F RID: 127
		UntrustedRoot = 32
	}
}
