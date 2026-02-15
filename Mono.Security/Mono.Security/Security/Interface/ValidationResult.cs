using System;

namespace Mono.Security.Interface
{
	// Token: 0x0200003A RID: 58
	public class ValidationResult
	{
		// Token: 0x06000133 RID: 307 RVA: 0x0000938B File Offset: 0x0000758B
		public ValidationResult(bool trusted, bool user_denied, int error_code, MonoSslPolicyErrors? policy_errors)
		{
			this.trusted = trusted;
			this.user_denied = user_denied;
			this.error_code = error_code;
			this.policy_errors = policy_errors;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000093B0 File Offset: 0x000075B0
		public bool Trusted
		{
			get
			{
				return this.trusted;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000093B8 File Offset: 0x000075B8
		public bool UserDenied
		{
			get
			{
				return this.user_denied;
			}
		}

		// Token: 0x040000B8 RID: 184
		private bool trusted;

		// Token: 0x040000B9 RID: 185
		private bool user_denied;

		// Token: 0x040000BA RID: 186
		private int error_code;

		// Token: 0x040000BB RID: 187
		private MonoSslPolicyErrors? policy_errors;
	}
}
