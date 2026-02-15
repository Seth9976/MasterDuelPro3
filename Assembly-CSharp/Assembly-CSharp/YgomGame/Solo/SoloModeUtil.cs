using System;

namespace YgomGame.Solo
{
	// Token: 0x020008FD RID: 2301
	public class SoloModeUtil
	{
		// Token: 0x040081AF RID: 33199
		public const int MAX_DIFFICULTY = 5;

		// Token: 0x020008FE RID: 2302
		public enum DialogType
		{
			// Token: 0x040081B1 RID: 33201
			DUEL,
			// Token: 0x040081B2 RID: 33202
			SCENARIO,
			// Token: 0x040081B3 RID: 33203
			LOCK,
			// Token: 0x040081B4 RID: 33204
			REWARD,
			// Token: 0x040081B5 RID: 33205
			TUTORIAL
		}

		// Token: 0x020008FF RID: 2303
		public enum DeckType
		{
			// Token: 0x040081B7 RID: 33207
			POSSESSION,
			// Token: 0x040081B8 RID: 33208
			STORY
		}

		// Token: 0x02000900 RID: 2304
		public enum ChapterStatus
		{
			// Token: 0x040081BA RID: 33210
			UNOPEN = -1,
			// Token: 0x040081BB RID: 33211
			OPEN,
			// Token: 0x040081BC RID: 33212
			RENTAL_CLEAR,
			// Token: 0x040081BD RID: 33213
			MYDECK_CLEAR,
			// Token: 0x040081BE RID: 33214
			COMPLETE
		}

		// Token: 0x02000901 RID: 2305
		public enum RewardType
		{
			// Token: 0x040081C0 RID: 33216
			STORY_CLEAR = 1,
			// Token: 0x040081C1 RID: 33217
			MYDECK_CLEAR
		}

		// Token: 0x02000902 RID: 2306
		public enum UnlockType
		{
			// Token: 0x040081C3 RID: 33219
			USER_LEVEL = 1,
			// Token: 0x040081C4 RID: 33220
			CHAPTER_OR,
			// Token: 0x040081C5 RID: 33221
			ITEM,
			// Token: 0x040081C6 RID: 33222
			CHAPTER_AND,
			// Token: 0x040081C7 RID: 33223
			HAS_ITEM
		}
	}
}
