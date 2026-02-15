using System;
using TMPro;

namespace YgomGame.Utility
{
	// Token: 0x0200082D RID: 2093
	public class NGWordCheck
	{
		// Token: 0x06004093 RID: 16531 RVA: 0x000F472C File Offset: 0x000F292C
		public static ValueTuple<NGWordCheck.ErrorType, string> Check(string src, TMP_FontAsset fontAsset = null)
		{
			return default(ValueTuple<NGWordCheck.ErrorType, string>);
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetErrorMessage(NGWordCheck.ErrorType errorType)
		{
			return null;
		}

		// Token: 0x04003993 RID: 14739
		private const int lowerLimit = 3;

		// Token: 0x04003994 RID: 14740
		private const int upperLimit = 12;

		// Token: 0x04003995 RID: 14741
		private const int numLimit = 6;

		// Token: 0x04003996 RID: 14742
		private const string numberRegex = "\\d";

		// Token: 0x0200082E RID: 2094
		public enum ErrorType
		{
			// Token: 0x04003998 RID: 14744
			Normal,
			// Token: 0x04003999 RID: 14745
			Error1,
			// Token: 0x0400399A RID: 14746
			Error2
		}
	}
}
