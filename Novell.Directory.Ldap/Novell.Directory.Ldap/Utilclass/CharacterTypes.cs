using System;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000057 RID: 87
	[CLSCompliant(false)]
	[Serializable]
	public enum CharacterTypes : sbyte
	{
		// Token: 0x040001C6 RID: 454
		WHITESPACE = 1,
		// Token: 0x040001C7 RID: 455
		NUMERIC,
		// Token: 0x040001C8 RID: 456
		ALPHABETIC = 4,
		// Token: 0x040001C9 RID: 457
		STRINGQUOTE = 8,
		// Token: 0x040001CA RID: 458
		COMMENTCHAR = 16
	}
}
