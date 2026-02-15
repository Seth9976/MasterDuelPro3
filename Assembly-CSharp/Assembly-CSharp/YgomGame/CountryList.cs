using System;
using System.Collections.Generic;

namespace YgomGame
{
	// Token: 0x020007B6 RID: 1974
	public class CountryList
	{
		// Token: 0x06003D45 RID: 15685 RVA: 0x00002739 File Offset: 0x00000939
		private CountryList()
		{
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x0000216D File Offset: 0x0000036D
		private void initialize(string lang)
		{
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x0000216A File Offset: 0x0000036A
		public CountryData GetDefault()
		{
			return null;
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x0000216A File Offset: 0x0000036A
		public CountryData GetDataByNumeric(int numeric)
		{
			return null;
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x0000216A File Offset: 0x0000036A
		public CountryData GetDataByAlpha2(string alpha2)
		{
			return null;
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x0000216A File Offset: 0x0000036A
		public int[] GetNumericList()
		{
			return null;
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] GetAlpha2List()
		{
			return null;
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] GetDisplayList()
		{
			return null;
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x0000216A File Offset: 0x0000036A
		public static CountryList Create(string lang)
		{
			return null;
		}

		// Token: 0x040035C6 RID: 13766
		private List<CountryData> m_list;

		// Token: 0x040035C7 RID: 13767
		private CountryData m_otherData;

		// Token: 0x040035C8 RID: 13768
		private CountryData m_defaultData;
	}
}
