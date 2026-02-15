using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000033 RID: 51
	public class LdapExtendedResponse : LdapResponse
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00008EE0 File Offset: 0x000070E0
		public virtual string ID
		{
			get
			{
				RfcLdapOID responseName = ((RfcExtendedResponse)this.message.Response).ResponseName;
				if (responseName == null)
				{
					return null;
				}
				return responseName.stringValue();
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00008F1A File Offset: 0x0000711A
		public static RespExtensionSet RegisteredResponses
		{
			get
			{
				return LdapExtendedResponse.registeredResponses;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00008F24 File Offset: 0x00007124
		[CLSCompliant(false)]
		public virtual sbyte[] Value
		{
			get
			{
				Asn1OctetString response = ((RfcExtendedResponse)this.message.Response).Response;
				if (response == null)
				{
					return null;
				}
				return response.byteValue();
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008F52 File Offset: 0x00007152
		public LdapExtendedResponse(RfcLdapMessage message)
			: base(message)
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008F5B File Offset: 0x0000715B
		public static void register(string oid, Type extendedResponseClass)
		{
			LdapExtendedResponse.registeredResponses.registerResponseExtension(oid, extendedResponseClass);
		}

		// Token: 0x04000122 RID: 290
		private static RespExtensionSet registeredResponses = new RespExtensionSet();
	}
}
