using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B5 RID: 693
	internal abstract class ScheduledItem
	{
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x0004DD26 File Offset: 0x0004BF26
		// (set) Token: 0x060012BC RID: 4796 RVA: 0x0004DD2E File Offset: 0x0004BF2E
		public long startMs { get; set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0004DD37 File Offset: 0x0004BF37
		// (set) Token: 0x060012BE RID: 4798 RVA: 0x0004DD3F File Offset: 0x0004BF3F
		public long delayMs { get; set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0004DD48 File Offset: 0x0004BF48
		// (set) Token: 0x060012C0 RID: 4800 RVA: 0x0004DD50 File Offset: 0x0004BF50
		public long intervalMs { get; set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x0004DD59 File Offset: 0x0004BF59
		public long endTimeMs { get; }

		// Token: 0x060012C2 RID: 4802 RVA: 0x0004DD61 File Offset: 0x0004BF61
		public ScheduledItem()
		{
			this.ResetStartTime();
			this.timerUpdateStopCondition = ScheduledItem.OnceCondition;
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0004DD7D File Offset: 0x0004BF7D
		protected void ResetStartTime()
		{
			this.startMs = Panel.TimeSinceStartupMs();
		}

		// Token: 0x060012C4 RID: 4804
		public abstract void PerformTimerUpdate(TimerState state);

		// Token: 0x060012C5 RID: 4805 RVA: 0x000020EA File Offset: 0x000002EA
		internal virtual void OnItemUnscheduled()
		{
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0004DD8C File Offset: 0x0004BF8C
		public virtual bool ShouldUnschedule()
		{
			bool flag = this.timerUpdateStopCondition != null;
			return flag && this.timerUpdateStopCondition();
		}

		// Token: 0x04000AD3 RID: 2771
		public Func<bool> timerUpdateStopCondition;

		// Token: 0x04000AD4 RID: 2772
		public static readonly Func<bool> OnceCondition = () => true;

		// Token: 0x04000AD5 RID: 2773
		public static readonly Func<bool> ForeverCondition = () => false;
	}
}
