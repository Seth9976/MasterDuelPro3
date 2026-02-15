using System;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200009C RID: 156
	public class GetBindDNRequest : LdapExtendedOperation
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x000140F0 File Offset: 0x000122F0
		static GetBindDNRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.32", Type.GetType("Novell.Directory.Ldap.Extensions.GetBindDNResponse"));
			}
			catch (Exception)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00014138 File Offset: 0x00012338
		public GetBindDNRequest()
			: base("2.16.840.1.113719.1.27.100.31", null)
		{
		}
	}
}
