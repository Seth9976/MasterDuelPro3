using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000AE RID: 174
	public class ReceiveAllUpdatesRequest : LdapExtendedOperation
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x00015294 File Offset: 0x00013494
		public ReceiveAllUpdatesRequest(string partitionRoot, string toServerDN, string fromServerDN)
			: base("2.16.840.1.113719.1.27.100.21", null)
		{
			try
			{
				if (partitionRoot == null || toServerDN == null || fromServerDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1OctetString(partitionRoot);
				Asn1OctetString asn1OctetString = new Asn1OctetString(toServerDN);
				Asn1OctetString asn1OctetString2 = new Asn1OctetString(fromServerDN);
				asn1Object.encode(lberencoder, memoryStream);
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
