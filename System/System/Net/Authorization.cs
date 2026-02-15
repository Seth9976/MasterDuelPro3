using System;

namespace System.Net
{
	/// <summary>Contains an authentication message for an Internet server.</summary>
	// Token: 0x020003A1 RID: 929
	public class Authorization
	{
		/// <summary>Gets the message returned to the server in response to an authentication challenge.</summary>
		/// <returns>The message that will be returned to the server in response to an authentication challenge.</returns>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x00063EC8 File Offset: 0x000620C8
		public string Message
		{
			get
			{
				return this.m_Message;
			}
		}

		/// <summary>Gets the completion status of the authorization.</summary>
		/// <returns>true if the authentication process is complete; otherwise, false.</returns>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x00063ED0 File Offset: 0x000620D0
		public bool Complete
		{
			get
			{
				return this.m_Complete;
			}
		}

		// Token: 0x04000E61 RID: 3681
		private string m_Message;

		// Token: 0x04000E62 RID: 3682
		private bool m_Complete;

		// Token: 0x04000E63 RID: 3683
		internal string ModuleAuthenticationType;
	}
}
