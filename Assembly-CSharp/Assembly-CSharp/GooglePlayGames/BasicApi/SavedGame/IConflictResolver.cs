using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020011C5 RID: 4549
	public interface IConflictResolver
	{
		// Token: 0x06008793 RID: 34707
		void ChooseMetadata(ISavedGameMetadata chosenMetadata);

		// Token: 0x06008794 RID: 34708
		void ResolveConflict(ISavedGameMetadata chosenMetadata, SavedGameMetadataUpdate metadataUpdate, byte[] updatedData);
	}
}
