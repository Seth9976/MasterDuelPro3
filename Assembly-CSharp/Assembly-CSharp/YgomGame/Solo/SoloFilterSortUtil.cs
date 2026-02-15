using System;

namespace YgomGame.Solo
{
	// Token: 0x020008F5 RID: 2293
	public class SoloFilterSortUtil
	{
		// Token: 0x06004312 RID: 17170 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SaveFilterSort(SoloFilterSortUtil.GateFilter gateFilter, SoloFilterSortUtil.GateSort gateSort)
		{
		}

		// Token: 0x06004313 RID: 17171 RVA: 0x000F47B0 File Offset: 0x000F29B0
		public static ValueTuple<SoloFilterSortUtil.GateFilter, SoloFilterSortUtil.GateSort> LoadFilterSort()
		{
			return default(ValueTuple<SoloFilterSortUtil.GateFilter, SoloFilterSortUtil.GateSort>);
		}

		// Token: 0x04008183 RID: 33155
		private const string PATH_SOLO_SAVE = "SoloSave";

		// Token: 0x04008184 RID: 33156
		private const string KEY_FILTER = "Filter";

		// Token: 0x04008185 RID: 33157
		private const string KEY_SORT = "Sort";

		// Token: 0x020008F6 RID: 2294
		[Flags]
		public enum GateFilter
		{
			// Token: 0x04008187 RID: 33159
			NONE = 1,
			// Token: 0x04008188 RID: 33160
			NOT_CLEAR = 2,
			// Token: 0x04008189 RID: 33161
			CLEAR = 4,
			// Token: 0x0400818A RID: 33162
			LOCK = 8,
			// Token: 0x0400818B RID: 33163
			COMPLETE = 16
		}

		// Token: 0x020008F7 RID: 2295
		public enum GateSort
		{
			// Token: 0x0400818D RID: 33165
			ASC_DEFAULT = 1,
			// Token: 0x0400818E RID: 33166
			DESC_DEFAULT,
			// Token: 0x0400818F RID: 33167
			ASC_RECENT_RELEASE,
			// Token: 0x04008190 RID: 33168
			DESC_RECENT_RELEASE,
			// Token: 0x04008191 RID: 33169
			ASC_RECENT_PLAY,
			// Token: 0x04008192 RID: 33170
			DESC_RECENT_PLAY
		}
	}
}
