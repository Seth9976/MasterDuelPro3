using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A2 RID: 162
	public class GetReplicationFilterRequest : LdapExtendedOperation
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x00014624 File Offset: 0x00012824
		static GetReplicationFilterRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.38", Type.GetType("Novell.Directory.Ldap.Extensions.GetReplicationFilterResponse"));
			}
			catch (Exception)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001466C File Offset: 0x0001286C
		public GetReplicationFilterRequest(string serverDN)
			: base("2.16.840.1.113719.1.27.100.37", null)
		{
			try
			{
				if (serverDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				new Asn1OctetString(serverDN).encode(lberencoder, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
