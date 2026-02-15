using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000024 RID: 36
	public class LdapBindRequest : LdapMessage
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005D4D File Offset: 0x00003F4D
		public virtual string AuthenticationDN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005D5A File Offset: 0x00003F5A
		[CLSCompliant(false)]
		public LdapBindRequest(int version, string dn, sbyte[] passwd, LdapControl[] cont)
			: base(0, new RfcBindRequest(new Asn1Integer(version), new RfcLdapDN(dn), new RfcAuthenticationChoice(new Asn1Tagged(new Asn1Identifier(2, false, 0), new Asn1OctetString(passwd), false))), cont)
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005D8F File Offset: 0x00003F8F
		[CLSCompliant(false)]
		public LdapBindRequest(int version, string dn, string mechanism, sbyte[] credentials, LdapControl[] cont)
			: base(0, new RfcBindRequest(version, dn, mechanism, credentials), cont)
		{
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004A19 File Offset: 0x00002C19
		public override string ToString()
		{
			return this.Asn1Object.ToString();
		}
	}
}
