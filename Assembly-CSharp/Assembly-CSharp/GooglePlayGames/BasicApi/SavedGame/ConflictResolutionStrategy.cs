using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020011C4 RID: 4548
	public enum ConflictResolutionStrategy
	{
		// Token: 0x0400C208 RID: 49672
		UseLongestPlaytime,
		// Token: 0x0400C209 RID: 49673
		UseOriginal,
		// Token: 0x0400C20A RID: 49674
		UseUnmerged,
		// Token: 0x0400C20B RID: 49675
		UseManual,
		// Token: 0x0400C20C RID: 49676
		UseLastKnownGood,
		// Token: 0x0400C20D RID: 49677
		UseMostRecentlySaved
	}
}
