using System;
using System.Collections;
using System.Configuration;
using System.Net.Configuration;

namespace System.Net
{
	/// <summary>Manages the authentication modules called during the client authentication process.</summary>
	// Token: 0x020003F9 RID: 1017
	public class AuthenticationManager
	{
		// Token: 0x0600194E RID: 6478 RVA: 0x0006C334 File Offset: 0x0006A534
		private static void EnsureModules()
		{
			object obj = AuthenticationManager.locker;
			lock (obj)
			{
				if (AuthenticationManager.modules == null)
				{
					AuthenticationManager.modules = new ArrayList();
					AuthenticationModulesSection authenticationModulesSection = ConfigurationManager.GetSection("system.net/authenticationModules") as AuthenticationModulesSection;
					if (authenticationModulesSection != null)
					{
						foreach (object obj2 in authenticationModulesSection.AuthenticationModules)
						{
							AuthenticationModuleElement authenticationModuleElement = (AuthenticationModuleElement)obj2;
							IAuthenticationModule authenticationModule = null;
							try
							{
								authenticationModule = (IAuthenticationModule)Activator.CreateInstance(Type.GetType(authenticationModuleElement.Type, true));
							}
							catch
							{
							}
							AuthenticationManager.modules.Add(authenticationModule);
						}
					}
				}
			}
		}

		/// <summary>Calls each registered authentication module to find the first module that can respond to the authentication request.</summary>
		/// <returns>An instance of the <see cref="T:System.Net.Authorization" /> class containing the result of the authorization attempt. If there is no authentication module to respond to the challenge, this method returns null.</returns>
		/// <param name="challenge">The challenge returned by the Internet resource. </param>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> that initiated the authentication challenge. </param>
		/// <param name="credentials">The <see cref="T:System.Net.ICredentials" /> associated with this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="challenge" /> is null.-or- <paramref name="request" /> is null.-or- <paramref name="credentials" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600194F RID: 6479 RVA: 0x0006C418 File Offset: 0x0006A618
		public static Authorization Authenticate(string challenge, WebRequest request, ICredentials credentials)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (credentials == null)
			{
				throw new ArgumentNullException("credentials");
			}
			if (challenge == null)
			{
				throw new ArgumentNullException("challenge");
			}
			return AuthenticationManager.DoAuthenticate(challenge, request, credentials);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0006C44C File Offset: 0x0006A64C
		private static Authorization DoAuthenticate(string challenge, WebRequest request, ICredentials credentials)
		{
			AuthenticationManager.EnsureModules();
			ArrayList arrayList = AuthenticationManager.modules;
			lock (arrayList)
			{
				foreach (object obj in AuthenticationManager.modules)
				{
					IAuthenticationModule authenticationModule = (IAuthenticationModule)obj;
					Authorization authorization = authenticationModule.Authenticate(challenge, request, credentials);
					if (authorization != null)
					{
						authorization.ModuleAuthenticationType = authenticationModule.AuthenticationType;
						return authorization;
					}
				}
			}
			return null;
		}

		/// <summary>Preauthenticates a request.</summary>
		/// <returns>An instance of the <see cref="T:System.Net.Authorization" /> class if the request can be preauthenticated; otherwise, null. If <paramref name="credentials" /> is null, this method returns null.</returns>
		/// <param name="request">A <see cref="T:System.Net.WebRequest" /> to an Internet resource. </param>
		/// <param name="credentials">The <see cref="T:System.Net.ICredentials" /> associated with the request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="request" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001951 RID: 6481 RVA: 0x0006C4F4 File Offset: 0x0006A6F4
		public static Authorization PreAuthenticate(WebRequest request, ICredentials credentials)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (credentials == null)
			{
				return null;
			}
			AuthenticationManager.EnsureModules();
			ArrayList arrayList = AuthenticationManager.modules;
			lock (arrayList)
			{
				foreach (object obj in AuthenticationManager.modules)
				{
					IAuthenticationModule authenticationModule = (IAuthenticationModule)obj;
					Authorization authorization = authenticationModule.PreAuthenticate(request, credentials);
					if (authorization != null)
					{
						authorization.ModuleAuthenticationType = authenticationModule.AuthenticationType;
						return authorization;
					}
				}
			}
			return null;
		}

		// Token: 0x04001014 RID: 4116
		private static ArrayList modules;

		// Token: 0x04001015 RID: 4117
		private static object locker = new object();

		// Token: 0x04001016 RID: 4118
		private static ICredentialPolicy credential_policy = null;
	}
}
