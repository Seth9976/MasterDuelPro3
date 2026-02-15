using System;

namespace System.Security.Cryptography
{
	// Token: 0x020001AB RID: 427
	internal enum AsnDecodeStatus
	{
		// Token: 0x04000795 RID: 1941
		NotDecoded = -1,
		// Token: 0x04000796 RID: 1942
		Ok,
		// Token: 0x04000797 RID: 1943
		BadAsn,
		// Token: 0x04000798 RID: 1944
		BadTag,
		// Token: 0x04000799 RID: 1945
		BadLength,
		// Token: 0x0400079A RID: 1946
		InformationNotAvailable
	}
}
