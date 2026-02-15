using System;

namespace YgomGame.Menu.AgeGate
{
	// Token: 0x02000B66 RID: 2918
	public class AgeGateViewController : CommonScreenViewController
	{
		// Token: 0x0600544E RID: 21582 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool verifyDate()
		{
			return false;
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x0000216D File Offset: 0x0000036D
		private void updateStatus()
		{
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06005453 RID: 21587 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x040091CD RID: 37325
		private Action<int, int, int> m_resultCallback;

		// Token: 0x040091CE RID: 37326
		private DateTime m_today;

		// Token: 0x040091CF RID: 37327
		private YearNumber m_year;

		// Token: 0x040091D0 RID: 37328
		private MonthNumber m_month;

		// Token: 0x040091D1 RID: 37329
		private DayNumber m_day;
	}
}
