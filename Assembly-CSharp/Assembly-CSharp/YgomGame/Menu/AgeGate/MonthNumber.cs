using System;

namespace YgomGame.Menu.AgeGate
{
	// Token: 0x02000B69 RID: 2921
	internal class MonthNumber : AgeNumber
	{
		// Token: 0x06005465 RID: 21605 RVA: 0x000F4C74 File Offset: 0x000F2E74
		public MonthNumber(int defaultMonth)
		{
		}

		// Token: 0x06005466 RID: 21606 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int indexToValue(int index)
		{
			return 0;
		}

		// Token: 0x06005467 RID: 21607 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string getUnselectText()
		{
			return null;
		}

		// Token: 0x040091D6 RID: 37334
		private static readonly string[] monthNames;
	}
}
