using System;

namespace GooglePlayGames.BasicApi
{
	// Token: 0x020011B8 RID: 4536
	public class SignInHelper
	{
		// Token: 0x06008775 RID: 34677 RVA: 0x000029CC File Offset: 0x00000BCC
		public static SignInStatus ToSignInStatus(int code)
		{
			return SignInStatus.Success;
		}

		// Token: 0x06008776 RID: 34678 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPromptUiSignIn(bool value)
		{
		}

		// Token: 0x06008777 RID: 34679 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ShouldPromptUiSignIn()
		{
			return false;
		}

		// Token: 0x0400C1D3 RID: 49619
		private static int True;

		// Token: 0x0400C1D4 RID: 49620
		private static int False;

		// Token: 0x0400C1D5 RID: 49621
		private const string PromptSignInKey = "prompt_sign_in";
	}
}
