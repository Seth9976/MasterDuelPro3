using System;

namespace YgomGame.Menu.AgeGate
{
	// Token: 0x02000B6B RID: 2923
	internal class YearNumber : AgeNumber
	{
		// Token: 0x06005469 RID: 21609 RVA: 0x000F4C74 File Offset: 0x000F2E74
		public YearNumber(int nowYear, int range, int defaultYear)
		{
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int indexToValue(int index)
		{
			return 0;
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string getUnselectText()
		{
			return null;
		}

		// Token: 0x040091DB RID: 37339
		private int m_nowYear;
	}
}
