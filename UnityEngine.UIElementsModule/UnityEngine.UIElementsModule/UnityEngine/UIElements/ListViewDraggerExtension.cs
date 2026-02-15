using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200019D RID: 413
	internal static class ListViewDraggerExtension
	{
		// Token: 0x06000C17 RID: 3095 RVA: 0x0003A0F4 File Offset: 0x000382F4
		public static ReusableCollectionItem GetRecycledItemFromId(this BaseVerticalCollectionView listView, int id)
		{
			foreach (ReusableCollectionItem recycledItem in listView.activeItems)
			{
				bool flag = recycledItem.id.Equals(id);
				if (flag)
				{
					return recycledItem;
				}
			}
			return null;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0003A160 File Offset: 0x00038360
		public static ReusableCollectionItem GetRecycledItemFromIndex(this BaseVerticalCollectionView listView, int index)
		{
			foreach (ReusableCollectionItem recycledItem in listView.activeItems)
			{
				bool flag = recycledItem.index.Equals(index);
				if (flag)
				{
					return recycledItem;
				}
			}
			return null;
		}
	}
}
