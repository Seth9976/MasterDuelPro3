using System;

namespace KonamiCommonIAB
{
	// Token: 0x020011AA RID: 4522
	internal class iPhoneResult : Result
	{
		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x06008751 RID: 34641 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int code
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06008752 RID: 34642 RVA: 0x000F755E File Offset: 0x000F575E
		public iPhoneResult(int r)
		{
		}

		// Token: 0x06008753 RID: 34643 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isSuccess()
		{
			return false;
		}

		// Token: 0x06008754 RID: 34644 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isFailure()
		{
			return false;
		}

		// Token: 0x06008755 RID: 34645 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int getResponse()
		{
			return 0;
		}

		// Token: 0x06008756 RID: 34646 RVA: 0x0000216A File Offset: 0x0000036A
		public override string getMessage()
		{
			return null;
		}

		// Token: 0x0400C19B RID: 49563
		private int _code;
	}
}
