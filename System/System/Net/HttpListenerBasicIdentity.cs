using System;
using System.Security.Principal;

namespace System.Net
{
	/// <summary>Holds the user name and password from a basic authentication request.</summary>
	// Token: 0x0200040E RID: 1038
	public class HttpListenerBasicIdentity : GenericIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerBasicIdentity" /> class using the specified user name and password.</summary>
		/// <param name="username">The user name.</param>
		/// <param name="password">The password.</param>
		// Token: 0x060019D6 RID: 6614 RVA: 0x0006F37D File Offset: 0x0006D57D
		public HttpListenerBasicIdentity(string username, string password)
			: base(username, "Basic")
		{
			this.password = password;
		}

		// Token: 0x04001081 RID: 4225
		private string password;
	}
}
