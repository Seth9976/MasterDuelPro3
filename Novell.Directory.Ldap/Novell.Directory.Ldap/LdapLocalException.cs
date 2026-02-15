using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000035 RID: 53
	public class LdapLocalException : LdapException
	{
		// Token: 0x06000211 RID: 529 RVA: 0x00008FEA File Offset: 0x000071EA
		public LdapLocalException()
		{
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008FF2 File Offset: 0x000071F2
		public LdapLocalException(string messageOrKey, int resultCode)
			: base(messageOrKey, resultCode, null)
		{
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008FFD File Offset: 0x000071FD
		public LdapLocalException(string messageOrKey, object[] arguments, int resultCode)
			: base(messageOrKey, arguments, resultCode, null)
		{
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009009 File Offset: 0x00007209
		public LdapLocalException(string messageOrKey, int resultCode, Exception rootException)
			: base(messageOrKey, resultCode, null, rootException)
		{
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00009015 File Offset: 0x00007215
		public LdapLocalException(string messageOrKey, object[] arguments, int resultCode, Exception rootException)
			: base(messageOrKey, arguments, resultCode, null, rootException)
		{
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009023 File Offset: 0x00007223
		public override string ToString()
		{
			return this.getExceptionString("LdapLocalException");
		}
	}
}
