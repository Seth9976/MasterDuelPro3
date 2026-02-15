using System;

namespace GooglePlayGames.BasicApi.Events
{
	// Token: 0x020011D7 RID: 4567
	public interface IEvent
	{
		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x060087CB RID: 34763
		string Id { get; }

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x060087CC RID: 34764
		string Name { get; }

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x060087CD RID: 34765
		string Description { get; }

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x060087CE RID: 34766
		string ImageUrl { get; }

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x060087CF RID: 34767
		ulong CurrentCount { get; }

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x060087D0 RID: 34768
		EventVisibility Visibility { get; }
	}
}
