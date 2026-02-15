using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000AB RID: 171
	public class PartitionEntryCountRequest : LdapExtendedOperation
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x000150C8 File Offset: 0x000132C8
		static PartitionEntryCountRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.14", Type.GetType("Novell.Directory.Ldap.Extensions.PartitionEntryCountResponse"));
			}
			catch (Exception)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00015110 File Offset: 0x00013310
		public PartitionEntryCountRequest(string dn)
			: base("2.16.840.1.113719.1.27.100.13", null)
		{
			try
			{
				if (dn == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				new Asn1OctetString(dn).encode(lberencoder, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
