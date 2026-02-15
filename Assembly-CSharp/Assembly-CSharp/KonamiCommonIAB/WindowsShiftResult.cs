using System;

namespace KonamiCommonIAB
{
	// Token: 0x020011A8 RID: 4520
	internal class WindowsShiftResult : Result
	{
		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06008743 RID: 34627 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int code
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06008744 RID: 34628 RVA: 0x000F755E File Offset: 0x000F575E
		public WindowsShiftResult(int r)
		{
		}

		// Token: 0x06008745 RID: 34629 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isSuccess()
		{
			return false;
		}

		// Token: 0x06008746 RID: 34630 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isFailure()
		{
			return false;
		}

		// Token: 0x06008747 RID: 34631 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int getResponse()
		{
			return 0;
		}

		// Token: 0x06008748 RID: 34632 RVA: 0x0000216A File Offset: 0x0000036A
		public override string getMessage()
		{
			return null;
		}

		// Token: 0x0400C199 RID: 49561
		private int _code;
	}
}
