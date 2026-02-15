using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020011C7 RID: 4551
	public interface ISavedGameMetadata
	{
		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x0600879C RID: 34716
		bool IsOpen { get; }

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x0600879D RID: 34717
		string Filename { get; }

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x0600879E RID: 34718
		string Description { get; }

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x0600879F RID: 34719
		string CoverImageURL { get; }

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x060087A0 RID: 34720
		TimeSpan TotalTimePlayed { get; }

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x060087A1 RID: 34721
		DateTime LastModifiedTimestamp { get; }
	}
}
