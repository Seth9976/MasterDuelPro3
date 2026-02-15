using System;
using System.Reflection;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x0200005C RID: 92
	public class IntermediateResponseFactory
	{
		// Token: 0x06000362 RID: 866 RVA: 0x0000F32C File Offset: 0x0000D52C
		public static LdapIntermediateResponse convertToIntermediateResponse(RfcLdapMessage inResponse)
		{
			LdapIntermediateResponse ldapIntermediateResponse = new LdapIntermediateResponse(inResponse);
			string id = ldapIntermediateResponse.getID();
			RespExtensionSet registeredResponses = LdapIntermediateResponse.getRegisteredResponses();
			try
			{
				Type type = registeredResponses.findResponseExtension(id);
				if (type == null)
				{
					return ldapIntermediateResponse;
				}
				Type[] array = new Type[] { typeof(RfcLdapMessage) };
				object[] array2 = new object[] { inResponse };
				try
				{
					ConstructorInfo constructor = type.GetConstructor(array);
					try
					{
						return (LdapIntermediateResponse)constructor.Invoke(array2);
					}
					catch (UnauthorizedAccessException)
					{
					}
					catch (TargetInvocationException)
					{
					}
				}
				catch (MissingMethodException)
				{
				}
			}
			catch (MissingFieldException)
			{
			}
			return ldapIntermediateResponse;
		}
	}
}
