using System;

namespace YgomGame.Friend
{
	// Token: 0x02000C14 RID: 3092
	public interface IPlayerContext : IComparable<IPlayerContext>
	{
		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x0600580F RID: 22543
		long pcode { get; }

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06005810 RID: 22544
		string playerName { get; }

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06005811 RID: 22545
		string platformPlayerName { get; }

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06005812 RID: 22546
		bool isRegistedPlatform { get; }

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06005813 RID: 22547
		bool isSamePlatform { get; }

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06005814 RID: 22548
		int iconId { get; }

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06005815 RID: 22549
		int iconFrameId { get; }

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06005816 RID: 22550
		int wallpaperId { get; }

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06005817 RID: 22551
		FollowState followState { get; }

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06005818 RID: 22552
		bool isPin { get; }

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06005819 RID: 22553
		long onlineTime { get; }

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x0600581A RID: 22554
		long loginTime { get; }

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x0600581B RID: 22555
		long followedTime { get; }

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x0600581C RID: 22556
		bool isOnline { get; }

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x0600581D RID: 22557
		bool isEnableDuelWatch { get; }

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x0600581E RID: 22558
		int invitedRoomId { get; }

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x0600581F RID: 22559
		int invitedTeamId { get; }
	}
}
