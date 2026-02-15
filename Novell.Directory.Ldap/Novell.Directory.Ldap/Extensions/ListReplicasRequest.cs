using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A7 RID: 167
	public class ListReplicasRequest : LdapExtendedOperation
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x00014EB4 File Offset: 0x000130B4
		static ListReplicasRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.20", Type.GetType("Novell.Directory.Ldap.Extensions.ListReplicasResponse"));
			}
			catch (Exception)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00014EFC File Offset: 0x000130FC
		public ListReplicasRequest(string serverName)
			: base("2.16.840.1.113719.1.27.100.19", null)
		{
			try
			{
				if (serverName == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				new Asn1OctetString(serverName).encode(lberencoder, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
