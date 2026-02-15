using System;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public enum TokenTypes
	{
		// Token: 0x0400024F RID: 591
		EOL = 10,
		// Token: 0x04000250 RID: 592
		EOF = -1,
		// Token: 0x04000251 RID: 593
		NUMBER = -2,
		// Token: 0x04000252 RID: 594
		WORD = -3,
		// Token: 0x04000253 RID: 595
		REAL = -4,
		// Token: 0x04000254 RID: 596
		STRING = -5
	}
}
