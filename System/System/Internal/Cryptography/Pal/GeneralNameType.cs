using System;

namespace Internal.Cryptography.Pal
{
	// Token: 0x020000D8 RID: 216
	internal enum GeneralNameType
	{
		// Token: 0x04000341 RID: 833
		OtherName,
		// Token: 0x04000342 RID: 834
		Rfc822Name,
		// Token: 0x04000343 RID: 835
		Email = 1,
		// Token: 0x04000344 RID: 836
		DnsName,
		// Token: 0x04000345 RID: 837
		X400Address,
		// Token: 0x04000346 RID: 838
		DirectoryName,
		// Token: 0x04000347 RID: 839
		EdiPartyName,
		// Token: 0x04000348 RID: 840
		UniformResourceIdentifier,
		// Token: 0x04000349 RID: 841
		IPAddress,
		// Token: 0x0400034A RID: 842
		RegisteredId
	}
}
