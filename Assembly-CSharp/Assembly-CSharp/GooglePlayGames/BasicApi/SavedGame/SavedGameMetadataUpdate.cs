using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020011C8 RID: 4552
	public struct SavedGameMetadataUpdate
	{
		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x060087A2 RID: 34722 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDescriptionUpdated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x060087A3 RID: 34723 RVA: 0x0000216A File Offset: 0x0000036A
		public string UpdatedDescription
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x060087A4 RID: 34724 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsCoverImageUpdated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x060087A5 RID: 34725 RVA: 0x0000216A File Offset: 0x0000036A
		public byte[] UpdatedPngCoverImage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x060087A6 RID: 34726 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayedTimeUpdated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x060087A7 RID: 34727 RVA: 0x000F7598 File Offset: 0x000F5798
		public TimeSpan? UpdatedPlayedTime
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400C20E RID: 49678
		private readonly bool mDescriptionUpdated;

		// Token: 0x0400C20F RID: 49679
		private readonly string mNewDescription;

		// Token: 0x0400C210 RID: 49680
		private readonly bool mCoverImageUpdated;

		// Token: 0x0400C211 RID: 49681
		private readonly byte[] mNewPngCoverImage;

		// Token: 0x0400C212 RID: 49682
		private readonly TimeSpan? mNewPlayedTime;

		// Token: 0x020011C9 RID: 4553
		public struct Builder
		{
			// Token: 0x060087A8 RID: 34728 RVA: 0x000F75B0 File Offset: 0x000F57B0
			public SavedGameMetadataUpdate.Builder WithUpdatedDescription(string description)
			{
				return default(SavedGameMetadataUpdate.Builder);
			}

			// Token: 0x060087A9 RID: 34729 RVA: 0x000F75C8 File Offset: 0x000F57C8
			public SavedGameMetadataUpdate.Builder WithUpdatedPngCoverImage(byte[] newPngCoverImage)
			{
				return default(SavedGameMetadataUpdate.Builder);
			}

			// Token: 0x060087AA RID: 34730 RVA: 0x000F75E0 File Offset: 0x000F57E0
			public SavedGameMetadataUpdate.Builder WithUpdatedPlayedTime(TimeSpan newPlayedTime)
			{
				return default(SavedGameMetadataUpdate.Builder);
			}

			// Token: 0x060087AB RID: 34731 RVA: 0x000F75F8 File Offset: 0x000F57F8
			public SavedGameMetadataUpdate Build()
			{
				return default(SavedGameMetadataUpdate);
			}

			// Token: 0x0400C213 RID: 49683
			internal bool mDescriptionUpdated;

			// Token: 0x0400C214 RID: 49684
			internal string mNewDescription;

			// Token: 0x0400C215 RID: 49685
			internal bool mCoverImageUpdated;

			// Token: 0x0400C216 RID: 49686
			internal byte[] mNewPngCoverImage;

			// Token: 0x0400C217 RID: 49687
			internal TimeSpan? mNewPlayedTime;
		}
	}
}
