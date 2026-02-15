using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B30 RID: 2864
	public class FieldResourceBinder : ResourceBinderBase
	{
		// Token: 0x06005386 RID: 21382 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(FieldResourceBinder.FieldPathData pathData)
		{
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetFieldIconPath(int itemId)
		{
			return null;
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindFieldIcon(Image target, int itemId, bool async = true)
		{
			return null;
		}

		// Token: 0x06005389 RID: 21385 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetFieldLargePath(int itemId)
		{
			return null;
		}

		// Token: 0x0600538A RID: 21386 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindFieldLarge(Image target, int itemId, bool async = true)
		{
			return null;
		}

		// Token: 0x0600538B RID: 21387 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetFieldObjIconPath(int itemId)
		{
			return null;
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindFieldObjIcon(Image target, int itemId, bool async = true)
		{
			return null;
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetFieldObjLargePath(int itemId)
		{
			return null;
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindFieldObjIconLarge(Image target, int itemId, bool async = true)
		{
			return null;
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetFieldAvatarBaseIconPath(int itemId)
		{
			return null;
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindFieldAvatarBaseIcon(Image target, int itemId, bool async = true)
		{
			return null;
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetFieldAvatarBaseIconLargePath(int itemId)
		{
			return null;
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingImageEx BindFieldAvatarBaseIconLarge(Image target, int itemId, bool async = true)
		{
			return null;
		}

		// Token: 0x06005393 RID: 21395 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemFieldBinder_002EBindItem(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x06005394 RID: 21396 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemFieldObjBinder_002EBindItem(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemAvatarHomeBinder_002EBindItem(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemFieldBinder_002EBindItemLarge(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x06005397 RID: 21399 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemFieldObjBinder_002EBindItemLarge(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x06005398 RID: 21400 RVA: 0x0000216A File Offset: 0x0000036A
		private Component YgomGame_002EMenu_002ECommon_002EIItemAvatarHomeBinder_002EBindItemLarge(GameObject target, int itemID)
		{
			return null;
		}

		// Token: 0x04009133 RID: 37171
		private FieldResourceBinder.FieldPathData m_PathData;

		// Token: 0x02000B31 RID: 2865
		[Serializable]
		public class FieldPathData
		{
			// Token: 0x04009134 RID: 37172
			public ResourceBindingPathSetting.ItemPathData m_FieldIconPath;

			// Token: 0x04009135 RID: 37173
			public ResourceBindingPathSetting.ItemPathData m_FieldObjIconPath;

			// Token: 0x04009136 RID: 37174
			public ResourceBindingPathSetting.ItemPathData m_AvatarBaseIconPath;
		}
	}
}
