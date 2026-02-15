using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A9 RID: 169
	public class MergePartitionsRequest : LdapExtendedOperation
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x00015044 File Offset: 0x00013244
		public MergePartitionsRequest(string dn, int flags)
			: base("2.16.840.1.113719.1.27.100.5", null)
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
