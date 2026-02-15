using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000B6 RID: 182
	public class SplitOrphanPartitionRequest : LdapExtendedOperation
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x00015670 File Offset: 0x00013870
		public SplitOrphanPartitionRequest(string serverDN, string contextName)
			: base("2.16.840.1.113719.1.27.100.39", null)
		{
			try
			{
				if (serverDN == null || contextName == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1OctetString(serverDN);
				Asn1OctetString asn1OctetString = new Asn1OctetString(contextName);
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
