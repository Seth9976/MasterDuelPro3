using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200000F RID: 15
	public enum EncryptionAlgorithm
	{
		// Token: 0x04000036 RID: 54
		None,
		// Token: 0x04000037 RID: 55
		PkzipClassic,
		// Token: 0x04000038 RID: 56
		Des = 26113,
		// Token: 0x04000039 RID: 57
		RC2,
		// Token: 0x0400003A RID: 58
		TripleDes168,
		// Token: 0x0400003B RID: 59
		TripleDes112 = 26121,
		// Token: 0x0400003C RID: 60
		Aes128 = 26126,
		// Token: 0x0400003D RID: 61
		Aes192,
		// Token: 0x0400003E RID: 62
		Aes256,
		// Token: 0x0400003F RID: 63
		RC2Corrected = 26370,
		// Token: 0x04000040 RID: 64
		Blowfish = 26400,
		// Token: 0x04000041 RID: 65
		Twofish,
		// Token: 0x04000042 RID: 66
		RC4 = 26625,
		// Token: 0x04000043 RID: 67
		Unknown = 65535
	}
}
