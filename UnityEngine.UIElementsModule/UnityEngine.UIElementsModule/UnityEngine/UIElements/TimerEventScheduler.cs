using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B7 RID: 695
	internal class TimerEventScheduler : IScheduler
	{
		// Token: 0x060012CC RID: 4812 RVA: 0x0004DDF4 File Offset: 0x0004BFF4
		public void Schedule(ScheduledItem item)
		{
			bool flag = item == null;
			if (!flag)
			{
				bool flag2 = item == null;
				if (flag2)
				{
					throw new NotSupportedException("Scheduled Item type is not supported by this scheduler");
				}
				bool transactionMode = this.m_TransactionMode;
				if (transactionMode)
				{
					bool flag3 = this.m_UnscheduleTransactions.Remove(item);
					if (!flag3)
					{
						bool flag4 = this.m_ScheduledItems.Contains(item) || this.m_ScheduleTransactions.Contains(item);
						if (flag4)
						{
							throw new ArgumentException("Cannot schedule function " + item + " more than once");
						}
						this.m_ScheduleTransactions.Add(item);
					}
				}
				else
				{
					bool flag5 = this.m_ScheduledItems.Contains(item);
					if (flag5)
					{
						throw new ArgumentException("Cannot schedule function " + item + " more than once");
					}
					this.m_ScheduledItems.Add(item);
				}
			}
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0004DECC File Offset: 0x0004C0CC
		private bool RemovedScheduledItemAt(int index)
		{
			bool flag = index >= 0;
			bool flag3;
			if (flag)
			{
				bool flag2 = index <= this.m_LastUpdatedIndex;
				if (flag2)
				{
					this.m_LastUpdatedIndex--;
				}
				this.m_ScheduledItems.RemoveAt(index);
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0004DF1C File Offset: 0x0004C11C
		public void Unschedule(ScheduledItem item)
		{
			bool flag = item != null;
			if (flag)
			{
				bool transactionMode = this.m_TransactionMode;
				if (transactionMode)
				{
					bool flag2 = this.m_UnscheduleTransactions.Contains(item);
					if (flag2)
					{
						throw new ArgumentException("Cannot unschedule scheduled function twice" + ((item != null) ? item.ToString() : null));
					}
					bool flag3 = this.m_ScheduleTransactions.Remove(item);
					if (!flag3)
					{
						bool flag4 = this.m_ScheduledItems.Contains(item);
						if (!flag4)
						{
							throw new ArgumentException("Cannot unschedule unknown scheduled function " + ((item != null) ? item.ToString() : null));
						}
						this.m_UnscheduleTransactions.Add(item);
					}
				}
				else
				{
					bool flag5 = !this.PrivateUnSchedule(item);
					if (flag5)
					{
						throw new ArgumentException("Cannot unschedule unknown scheduled function " + ((item != null) ? item.ToString() : null));
					}
				}
				item.OnItemUnscheduled();
			}
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0004E008 File Offset: 0x0004C208
		private bool PrivateUnSchedule(ScheduledItem sItem)
		{
			return this.m_ScheduleTransactions.Remove(sItem) || this.RemovedScheduledItemAt(this.m_ScheduledItems.IndexOf(sItem));
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0004E040 File Offset: 0x0004C240
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x0004E058 File Offset: 0x0004C258
		public long FrameCount
		{
			get
			{
				return this.frameCount;
			}
			set
			{
				this.frameCount = value;
			}
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0004E064 File Offset: 0x0004C264
		public void UpdateScheduledEvents()
		{
			bool incrementFrame = true;
			try
			{
				this.m_TransactionMode = true;
				long currentTime = Panel.TimeSinceStartupMs();
				int itemsCount = this.m_ScheduledItems.Count;
				int startIndex = this.m_LastUpdatedIndex + 1;
				bool flag = startIndex >= itemsCount;
				if (flag)
				{
					startIndex = 0;
				}
				for (int i = 0; i < itemsCount; i++)
				{
					int index = startIndex + i;
					bool flag2 = index >= itemsCount;
					if (flag2)
					{
						index -= itemsCount;
					}
					ScheduledItem scheduledItem = this.m_ScheduledItems[index];
					bool unscheduleItem = false;
					bool flag3 = currentTime - scheduledItem.delayMs >= scheduledItem.startMs;
					if (flag3)
					{
						TimerState timerState = new TimerState
						{
							start = scheduledItem.startMs,
							now = currentTime
						};
						bool flag4 = !this.m_UnscheduleTransactions.Contains(scheduledItem);
						if (flag4)
						{
							scheduledItem.PerformTimerUpdate(timerState);
						}
						scheduledItem.startMs = currentTime;
						scheduledItem.delayMs = scheduledItem.intervalMs;
						bool flag5 = scheduledItem.ShouldUnschedule();
						if (flag5)
						{
							unscheduleItem = true;
						}
					}
					bool flag6 = unscheduleItem || (scheduledItem.endTimeMs > 0L && currentTime > scheduledItem.endTimeMs);
					if (flag6)
					{
						bool flag7 = !this.m_UnscheduleTransactions.Contains(scheduledItem);
						if (flag7)
						{
							this.Unschedule(scheduledItem);
						}
					}
					this.m_LastUpdatedIndex = index;
				}
			}
			finally
			{
				this.m_TransactionMode = false;
				foreach (ScheduledItem item in this.m_UnscheduleTransactions)
				{
					this.PrivateUnSchedule(item);
				}
				this.m_UnscheduleTransactions.Clear();
				foreach (ScheduledItem item2 in this.m_ScheduleTransactions)
				{
					this.Schedule(item2);
				}
				this.m_ScheduleTransactions.Clear();
				bool flag8 = incrementFrame;
				if (flag8)
				{
					long num = this.FrameCount + 1L;
					this.FrameCount = num;
				}
			}
		}

		// Token: 0x04000ADB RID: 2779
		private readonly List<ScheduledItem> m_ScheduledItems = new List<ScheduledItem>();

		// Token: 0x04000ADC RID: 2780
		private bool m_TransactionMode;

		// Token: 0x04000ADD RID: 2781
		private readonly List<ScheduledItem> m_ScheduleTransactions = new List<ScheduledItem>();

		// Token: 0x04000ADE RID: 2782
		private readonly HashSet<ScheduledItem> m_UnscheduleTransactions = new HashSet<ScheduledItem>();

		// Token: 0x04000ADF RID: 2783
		internal bool disableThrottling = false;

		// Token: 0x04000AE0 RID: 2784
		private int m_LastUpdatedIndex = -1;

		// Token: 0x04000AE1 RID: 2785
		private long frameCount;
	}
}
