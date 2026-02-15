using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000B3 RID: 179
	public class SchemaSyncRequest : LdapExtendedOperation
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x00015454 File Offset: 0x00013654
		public SchemaSyncRequest(string serverName, int delay)
			: base("2.16.840.1.113719.1.27.100.27", null)
		{
			try
			{
				if (serverName == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1OctetString(serverName);
				Asn1Integer asn1Integer = new Asn1Integer(delay);
				asn1Object.encode(lberencoder, memoryStream);
				asn1Integer.encode(lberencoder, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
