using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000034 RID: 52
	public class LdapIntermediateResponse : LdapResponse
	{
		// Token: 0x0600020B RID: 523 RVA: 0x00008F69 File Offset: 0x00007169
		public static void register(string oid, Type extendedResponseClass)
		{
			LdapIntermediateResponse.registeredResponses.registerResponseExtension(oid, extendedResponseClass);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008F77 File Offset: 0x00007177
		public static RespExtensionSet getRegisteredResponses()
		{
			return LdapIntermediateResponse.registeredResponses;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008F52 File Offset: 0x00007152
		public LdapIntermediateResponse(RfcLdapMessage message)
			: base(message)
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008F80 File Offset: 0x00007180
		public string getID()
		{
			RfcLdapOID responseName = ((RfcIntermediateResponse)this.message.Response).getResponseName();
			if (responseName == null)
			{
				return null;
			}
			return responseName.stringValue();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008FB0 File Offset: 0x000071B0
		[CLSCompliant(false)]
		public sbyte[] getValue()
		{
			Asn1OctetString response = ((RfcIntermediateResponse)this.message.Response).getResponse();
			if (response == null)
			{
				return null;
			}
			return response.byteValue();
		}

		// Token: 0x04000123 RID: 291
		private static RespExtensionSet registeredResponses = new RespExtensionSet();
	}
}
