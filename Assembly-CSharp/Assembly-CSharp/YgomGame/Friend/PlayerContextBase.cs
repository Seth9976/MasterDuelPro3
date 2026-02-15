using System;
using System.Runtime.CompilerServices;

namespace YgomGame.Friend
{
	// Token: 0x02000C15 RID: 3093
	public abstract class PlayerContextBase : IPlayerContext, IComparable<IPlayerContext>
	{
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06005820 RID: 22560 RVA: 0x000F1669 File Offset: 0x000EF869
		// (set) Token: 0x06005821 RID: 22561 RVA: 0x0000216D File Offset: 0x0000036D
		public long pcode
		{
			[CompilerGenerated]
			get
			{
				return 0L;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06005822 RID: 22562 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005823 RID: 22563 RVA: 0x0000216D File Offset: 0x0000036D
		public string playerName
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06005824 RID: 22564 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005825 RID: 22565 RVA: 0x0000216D File Offset: 0x0000036D
		public string platformPlayerName
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06005826 RID: 22566 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005827 RID: 22567 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isRegistedPlatform
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06005828 RID: 22568 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005829 RID: 22569 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isSamePlatform
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x0600582A RID: 22570 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600582B RID: 22571 RVA: 0x0000216D File Offset: 0x0000036D
		public int iconId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x0600582C RID: 22572 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600582D RID: 22573 RVA: 0x0000216D File Offset: 0x0000036D
		public int iconFrameId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x0600582E RID: 22574 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600582F RID: 22575 RVA: 0x0000216D File Offset: 0x0000036D
		public int wallpaperId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06005830 RID: 22576 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005831 RID: 22577 RVA: 0x0000216D File Offset: 0x0000036D
		public FollowState followState
		{
			[CompilerGenerated]
			get
			{
				return FollowState.None;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06005832 RID: 22578 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005833 RID: 22579 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isPin
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06005834 RID: 22580 RVA: 0x000F1669 File Offset: 0x000EF869
		// (set) Token: 0x06005835 RID: 22581 RVA: 0x0000216D File Offset: 0x0000036D
		public long onlineTime
		{
			[CompilerGenerated]
			get
			{
				return 0L;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06005836 RID: 22582 RVA: 0x000F1669 File Offset: 0x000EF869
		// (set) Token: 0x06005837 RID: 22583 RVA: 0x0000216D File Offset: 0x0000036D
		public long loginTime
		{
			[CompilerGenerated]
			get
			{
				return 0L;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06005838 RID: 22584 RVA: 0x000F1669 File Offset: 0x000EF869
		// (set) Token: 0x06005839 RID: 22585 RVA: 0x0000216D File Offset: 0x0000036D
		public long followedTime
		{
			[CompilerGenerated]
			get
			{
				return 0L;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x0600583A RID: 22586 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600583B RID: 22587 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isOnline
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x0600583C RID: 22588 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600583D RID: 22589 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isEnableDuelWatch
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x0600583E RID: 22590 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600583F RID: 22591 RVA: 0x0000216D File Offset: 0x0000036D
		public int invitedRoomId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06005840 RID: 22592 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005841 RID: 22593 RVA: 0x0000216D File Offset: 0x0000036D
		public int invitedTeamId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x0000216D File Offset: 0x0000036D
		protected void Import(object playerData)
		{
		}

		// Token: 0x06005843 RID: 22595 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportRefreshData(object playerRefreshData)
		{
		}

		// Token: 0x06005844 RID: 22596 RVA: 0x000029CC File Offset: 0x00000BCC
		public virtual int CompareTo(IPlayerContext other)
		{
			return 0;
		}
	}
}
