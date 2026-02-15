using System;

namespace YgomGame.Menu.AgeGate
{
	// Token: 0x02000B68 RID: 2920
	internal class DayNumber : AgeNumber
	{
		// Token: 0x0600545F RID: 21599 RVA: 0x0000216A File Offset: 0x0000036A
		private static string[] createDaysList(int dayNum)
		{
			return null;
		}

		// Token: 0x06005460 RID: 21600 RVA: 0x000F4C74 File Offset: 0x000F2E74
		public DayNumber(int dayNum, int defaultDay)
		{
		}

		// Token: 0x06005461 RID: 21601 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int indexToValue(int index)
		{
			return 0;
		}

		// Token: 0x06005462 RID: 21602 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string getUnselectText()
		{
			return null;
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetDays()
		{
			return 0;
		}

		// Token: 0x06005464 RID: 21604 RVA: 0x0000216D File Offset: 0x0000036D
		public void Rebuild(int dayNum)
		{
		}

		// Token: 0x040091D5 RID: 37333
		private static readonly string[] fullList;
	}
}
