using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B2C RID: 2860
	public class DeckResourceBinder : ResourceBinderBase
	{
		// Token: 0x0600535F RID: 21343 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(DeckResourceBinder.DeckPathData data)
		{
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetDeckIconNumberFromStructureId(int structureId)
		{
			return 0;
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetStructureBoxIconPath(int structureId)
		{
			return null;
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetStructureBoxOpenIconPath(int structureId)
		{
			return null;
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x0000216A File Offset: 0x0000036A
		public StructureBoxWidget BindStructureBoxWidget(ElementObjectManager eom, int structureId, Sprite deckSprite = null, Sprite openedDeckSprite = null, Sprite[] monsterSprites = null)
		{
			return null;
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindStructureBoxIcon(Image target, int id, bool async = true)
		{
			return null;
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindStructureBoxIcon(GameObject target, int id, bool async = true)
		{
			return null;
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindStructureBoxOpenIcon(Image target, int id, bool async = true)
		{
			return null;
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x000029CC File Offset: 0x00000BCC
		private int caseIDtoIconNumber(int caseID)
		{
			return 0;
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetDeckCaseIconPath(int caseID, bool isLarge = false, bool isReverse = false)
		{
			return null;
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetDeckCaseOpenIconPath(int caseID, bool isLarge = false)
		{
			return null;
		}

		// Token: 0x0600536A RID: 21354 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindDeckCaseIcon(Image target, int id, bool async = true, bool isLarge = false, bool isReverse = false)
		{
			return null;
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindDeckCaseOpenIcon(Image target, int id, bool async = true, bool isLarge = false)
		{
			return null;
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindDeckCaseIcon(GameObject target, int id, bool async = true, bool isLarge = false, bool isReverse = false)
		{
			return null;
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCaseWidget BindDeckCaseWidget(GameObject rootObj, int caseId, int protectorId, string deckName, int[] pickupCards, int[] pickupDecos, bool opened, bool isLarge = false, bool isDestroyTweens = false)
		{
			return null;
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x0000216A File Offset: 0x0000036A
		public PublicDeckCaseWidget BindPublicDeckCaseWidget(GameObject rootObj, int caseId, int pickupCard)
		{
			return null;
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x0000216A File Offset: 0x0000036A
		public SearchCategoryWidget BindSearchCategoryWidget(GameObject rootObj, int categoryId, string categoryName)
		{
			return null;
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemDeckCaseBinder_002EBindItem(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemDeckCaseBinder_002EBindItemLarge(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemStructureBinder_002EBindItem(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemDeckLimitBinder_002EBindItem(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemDeckLimitBinder_002EBindItemLarge(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x04009128 RID: 37160
		private DeckResourceBinder.DeckPathData m_Data;

		// Token: 0x02000B2D RID: 2861
		[Serializable]
		public class DeckPathData
		{
			// Token: 0x04009129 RID: 37161
			public ResourceBindingPathSetting.ItemPathData m_DeckCasePath;

			// Token: 0x0400912A RID: 37162
			public ResourceBindingPathSetting.ItemPathData m_OpenCasePath;

			// Token: 0x0400912B RID: 37163
			public ResourceBindingPathSetting.ItemPathData m_DeckLimitPath;
		}
	}
}
