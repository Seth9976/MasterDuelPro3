using System;
using System.Collections.Generic;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020011C6 RID: 4550
	public interface ISavedGameClient
	{
		// Token: 0x06008795 RID: 34709
		void OpenWithAutomaticConflictResolution(string filename, DataSource source, ConflictResolutionStrategy resolutionStrategy, Action<SavedGameRequestStatus, ISavedGameMetadata> callback);

		// Token: 0x06008796 RID: 34710
		void OpenWithManualConflictResolution(string filename, DataSource source, bool prefetchDataOnConflict, ConflictCallback conflictCallback, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback);

		// Token: 0x06008797 RID: 34711
		void ReadBinaryData(ISavedGameMetadata metadata, Action<SavedGameRequestStatus, byte[]> completedCallback);

		// Token: 0x06008798 RID: 34712
		void ShowSelectSavedGameUI(string uiTitle, uint maxDisplayedSavedGames, bool showCreateSaveUI, bool showDeleteSaveUI, Action<SelectUIStatus, ISavedGameMetadata> callback);

		// Token: 0x06008799 RID: 34713
		void CommitUpdate(ISavedGameMetadata metadata, SavedGameMetadataUpdate updateForMetadata, byte[] updatedBinaryData, Action<SavedGameRequestStatus, ISavedGameMetadata> callback);

		// Token: 0x0600879A RID: 34714
		void FetchAllSavedGames(DataSource source, Action<SavedGameRequestStatus, List<ISavedGameMetadata>> callback);

		// Token: 0x0600879B RID: 34715
		void Delete(ISavedGameMetadata metadata);
	}
}
