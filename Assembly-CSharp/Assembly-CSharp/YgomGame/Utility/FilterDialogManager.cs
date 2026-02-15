using System;
using System.Collections;
using System.Collections.Generic;
using YgomGame.Deck;

namespace YgomGame.Utility
{
	// Token: 0x02000825 RID: 2085
	public class FilterDialogManager
	{
		// Token: 0x06004040 RID: 16448 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsFiltered()
		{
			return false;
		}

		// Token: 0x06004041 RID: 16449 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool CheckDiff(BitArray ba1, BitArray ba2)
		{
			return false;
		}

		// Token: 0x06004042 RID: 16450 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool checkAny(BitArray ba)
		{
			return false;
		}

		// Token: 0x06004043 RID: 16451 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenFilterDialog(int regID = -1, Action resultCallback = null)
		{
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x0000216A File Offset: 0x0000036A
		private List<CardBaseData> GetCardBaseDataList(List<int> mrks)
		{
			return null;
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x0000216A File Offset: 0x0000036A
		public List<int> GetFilteredList(List<int> mrks)
		{
			return null;
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDefaultSetting(SearchFilter.Setting setting)
		{
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFilterGroupTypes(List<FilterDialog.FilterGroupType> filterGroupTypes)
		{
		}

		// Token: 0x06004048 RID: 16456 RVA: 0x0000216D File Offset: 0x0000036D
		private void CopyDefaultSetting()
		{
		}

		// Token: 0x04003955 RID: 14677
		private SearchFilter.Setting m_Setting;

		// Token: 0x04003956 RID: 14678
		private SearchFilter.Setting m_DefaultSetting;

		// Token: 0x04003957 RID: 14679
		private List<FilterDialog.FilterGroupType> m_FilterGroupTypes;
	}
}
