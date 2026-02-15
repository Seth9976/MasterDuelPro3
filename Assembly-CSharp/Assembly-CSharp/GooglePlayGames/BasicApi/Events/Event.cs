using System;

namespace GooglePlayGames.BasicApi.Events
{
	// Token: 0x020011D5 RID: 4565
	internal class Event : IEvent
	{
		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x060087C4 RID: 34756 RVA: 0x0000216A File Offset: 0x0000036A
		public string Id
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x060087C5 RID: 34757 RVA: 0x0000216A File Offset: 0x0000036A
		public string Name
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x060087C6 RID: 34758 RVA: 0x0000216A File Offset: 0x0000036A
		public string Description
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x060087C7 RID: 34759 RVA: 0x0000216A File Offset: 0x0000036A
		public string ImageUrl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x060087C8 RID: 34760 RVA: 0x000F1669 File Offset: 0x000EF869
		public ulong CurrentCount
		{
			get
			{
				return 0UL;
			}
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x060087C9 RID: 34761 RVA: 0x000029CC File Offset: 0x00000BCC
		public EventVisibility Visibility
		{
			get
			{
				return (EventVisibility)0;
			}
		}

		// Token: 0x060087CA RID: 34762 RVA: 0x00002739 File Offset: 0x00000939
		internal Event(string id, string name, string description, string imageUrl, ulong currentCount, EventVisibility visibility)
		{
		}

		// Token: 0x0400C241 RID: 49729
		private string mId;

		// Token: 0x0400C242 RID: 49730
		private string mName;

		// Token: 0x0400C243 RID: 49731
		private string mDescription;

		// Token: 0x0400C244 RID: 49732
		private string mImageUrl;

		// Token: 0x0400C245 RID: 49733
		private ulong mCurrentCount;

		// Token: 0x0400C246 RID: 49734
		private EventVisibility mVisibility;
	}
}
