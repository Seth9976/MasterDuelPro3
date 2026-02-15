using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000B7 RID: 183
	public class SplitPartitionRequest : LdapExtendedOperation
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x000156F4 File Offset: 0x000138F4
		public SplitPartitionRequest(string dn, int flags)
			: base("2.16.840.1.113719.1.27.100.3", null)
		{
			try
			{
				if (dn == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1Integer(flags);
				Asn1OctetString asn1OctetString = new Asn1OctetString(dn);
				asn1Object.encode(lberencoder, memoryStream);
				asn1OctetString.encode(lberencoder, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
