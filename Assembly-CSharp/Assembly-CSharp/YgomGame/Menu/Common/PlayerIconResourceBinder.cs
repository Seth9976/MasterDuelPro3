using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B4D RID: 2893
	public class PlayerIconResourceBinder : ResourceBinderBase
	{
		// Token: 0x060053DB RID: 21467 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(PlayerIconResourceBinder.PlayerIconPathData pathData)
		{
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPlayerIconBasePath(int id, bool isLarge = false)
		{
			return null;
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPlayerIconFramePath(int id, bool isLarge = false)
		{
			return null;
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPlayerIconFrameMatPath(int id)
		{
			return null;
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPlayerIconRankPath(int id, PlayerIconResourceBinder.Size size = PlayerIconResourceBinder.Size.SMALL)
		{
			return null;
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPlayerIconEventRankPath(int id, PlayerIconResourceBinder.Size size = PlayerIconResourceBinder.Size.SMALL)
		{
			return null;
		}

		// Token: 0x060053E1 RID: 21473 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPlayerIconRankBGPath(int id)
		{
			return null;
		}

		// Token: 0x060053E2 RID: 21474 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingProfileFrameIcon BindPlayerIcon(GameObject target, int baseID, int frameID, bool fitParentSize = false, bool async = true)
		{
			return null;
		}

		// Token: 0x060053E3 RID: 21475 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingProfileFrameIcon BindPlayerIconBase(GameObject target, int baseID, bool fitParentSize = false, bool async = true)
		{
			return null;
		}

		// Token: 0x060053E4 RID: 21476 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingProfileFrameIcon BindPlayerIconFrame(GameObject target, int frameID, bool async = true, bool isLarge = false)
		{
			return null;
		}

		// Token: 0x060053E5 RID: 21477 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingProfileFrameIcon BindPlayerIconBaseLarge(Image target, int baseID, bool async = true, bool isLarge = false)
		{
			return null;
		}

		// Token: 0x060053E6 RID: 21478 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingProfileFrameIcon BindPlayerIconFrameLarge(Image target, int frameID, bool async = true, bool isLarge = false)
		{
			return null;
		}

		// Token: 0x060053E7 RID: 21479 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingGameObjectEx BindPlayerIconRank(GameObject target, int rank, int tier, bool async = true, PlayerIconResourceBinder.Size size = PlayerIconResourceBinder.Size.SMALL, bool fitPrefabScale = true)
		{
			return null;
		}

		// Token: 0x060053E8 RID: 21480 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindPlayerIconRankBG(Image target, int id, bool async = true)
		{
			return null;
		}

		// Token: 0x060053E9 RID: 21481 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingGameObjectEx BindPlayerIconEventRank(GameObject target, int rank, int tier, bool async = true, PlayerIconResourceBinder.Size size = PlayerIconResourceBinder.Size.SMALL, bool fitPrefabScale = true)
		{
			return null;
		}

		// Token: 0x060053EA RID: 21482 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetEventTier(int tier, ElementObjectManager eom)
		{
		}

		// Token: 0x060053EB RID: 21483 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetTier(int tier, ElementObjectManager eom)
		{
		}

		// Token: 0x060053EC RID: 21484 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetAnchor(RectTransform rt)
		{
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x0000216D File Offset: 0x0000036D
		private void FitPrefabScale(GameObject parent, GameObject go)
		{
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemIconBinder_002EBindItem(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x060053EF RID: 21487 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemIconFrameBinder_002EBindItem(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x060053F0 RID: 21488 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemIconBinder_002EBindItemLarge(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x060053F1 RID: 21489 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemIconFrameBinder_002EBindItemLarge(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x04009159 RID: 37209
		private PlayerIconResourceBinder.PlayerIconPathData m_PathData;

		// Token: 0x02000B4E RID: 2894
		[Serializable]
		public class PlayerIconPathData
		{
			// Token: 0x0400915A RID: 37210
			public ResourceBindingPathSetting.ItemPathData m_PlayerIconBasePath;

			// Token: 0x0400915B RID: 37211
			public ResourceBindingPathSetting.ItemPathData m_PlayerFramePath;

			// Token: 0x0400915C RID: 37212
			public string m_PlayerFrameMatPath;

			// Token: 0x0400915D RID: 37213
			public string m_RankIconPath;

			// Token: 0x0400915E RID: 37214
			public string m_RankIconBGPath;

			// Token: 0x0400915F RID: 37215
			public string m_EventRankIconPath;

			// Token: 0x04009160 RID: 37216
			public string m_PlayerIconBlankPath;

			// Token: 0x04009161 RID: 37217
			public string m_PlayerFrameMatDefaultPath;
		}

		// Token: 0x02000B4F RID: 2895
		public enum Size
		{
			// Token: 0x04009163 RID: 37219
			SMALL,
			// Token: 0x04009164 RID: 37220
			LARGE
		}
	}
}
