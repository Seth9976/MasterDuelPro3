using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000014 RID: 20
	public enum AuthenticationTypes
	{
		// Token: 0x04000039 RID: 57
		Anonymous = 16,
		// Token: 0x0400003A RID: 58
		Delegation = 256,
		// Token: 0x0400003B RID: 59
		Encryption = 2,
		// Token: 0x0400003C RID: 60
		FastBind = 32,
		// Token: 0x0400003D RID: 61
		None = 0,
		// Token: 0x0400003E RID: 62
		ReadonlyServer = 4,
		// Token: 0x0400003F RID: 63
		Sealing = 128,
		// Token: 0x04000040 RID: 64
		Secure = 1,
		// Token: 0x04000041 RID: 65
		SecureSocketsLayer,
		// Token: 0x04000042 RID: 66
		ServerBind = 512,
		// Token: 0x04000043 RID: 67
		Signing = 64
	}
}
