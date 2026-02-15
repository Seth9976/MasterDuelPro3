using System;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000080 RID: 128
	public class RfcLdapDN : RfcLdapString
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x00011739 File Offset: 0x0000F939
		public RfcLdapDN(string s)
			: base(s)
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000136DB File Offset: 0x000118DB
		[CLSCompliant(false)]
		public RfcLdapDN(sbyte[] s)
			: base(s)
		{
		}
	}
}
