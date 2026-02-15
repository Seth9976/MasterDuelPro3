using System;

namespace System.Globalization
{
	// Token: 0x020006C4 RID: 1732
	[Serializable]
	internal class CodePageDataItem
	{
		// Token: 0x060036E5 RID: 14053 RVA: 0x000D3352 File Offset: 0x000D1552
		internal CodePageDataItem(int dataIndex)
		{
			this.m_dataIndex = dataIndex;
			this.m_uiFamilyCodePage = (int)EncodingTable.codePageDataPtr[dataIndex].uiFamilyCodePage;
			this.m_flags = EncodingTable.codePageDataPtr[dataIndex].flags;
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000D338D File Offset: 0x000D158D
		internal static string CreateString(string pStrings, uint index)
		{
			if (pStrings[0] == '|')
			{
				return pStrings.Split(CodePageDataItem.sep, StringSplitOptions.RemoveEmptyEntries)[(int)index];
			}
			return pStrings;
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060036E7 RID: 14055 RVA: 0x000D33AA File Offset: 0x000D15AA
		public string WebName
		{
			get
			{
				if (this.m_webName == null)
				{
					this.m_webName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 0U);
				}
				return this.m_webName;
			}
		}

		// Token: 0x04001D7A RID: 7546
		internal int m_dataIndex;

		// Token: 0x04001D7B RID: 7547
		internal int m_uiFamilyCodePage;

		// Token: 0x04001D7C RID: 7548
		internal string m_webName;

		// Token: 0x04001D7D RID: 7549
		internal uint m_flags;

		// Token: 0x04001D7E RID: 7550
		private static readonly char[] sep = new char[] { '|' };
	}
}
