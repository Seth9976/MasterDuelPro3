using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000099 RID: 153
	public class AddReplicaRequest : LdapExtendedOperation
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x00013FA0 File Offset: 0x000121A0
		public AddReplicaRequest(string dn, string serverDN, int replicaType, int flags)
			: base("2.16.840.1.113719.1.27.100.7", null)
		{
			try
			{
				if (dn == null || serverDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1Integer(flags);
				Asn1Integer asn1Integer = new Asn1Integer(replicaType);
				Asn1OctetString asn1OctetString = new Asn1OctetString(serverDN);
				Asn1OctetString asn1OctetString2 = new Asn1OctetString(dn);
				asn1Object.encode(lberencoder, memoryStream);
				asn1Integer.encode(lberencoder, memoryStream);
				asn1OctetString.encode(lberencoder, memoryStream);
				asn1OctetString2.encode(lberencoder, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
