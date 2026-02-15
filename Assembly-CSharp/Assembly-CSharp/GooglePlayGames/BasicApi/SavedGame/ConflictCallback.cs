using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020011C3 RID: 4547
	// (Invoke) Token: 0x06008790 RID: 34704
	public delegate void ConflictCallback(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, ISavedGameMetadata unmerged, byte[] unmergedData);
}
