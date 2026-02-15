using System;

namespace System.Globalization
{
	// Token: 0x020006C1 RID: 1729
	internal interface ISimpleCollator
	{
		// Token: 0x060036AD RID: 13997
		SortKey GetSortKey(string source, CompareOptions options);

		// Token: 0x060036AE RID: 13998
		int Compare(string s1, int idx1, int len1, string s2, int idx2, int len2, CompareOptions options);

		// Token: 0x060036AF RID: 13999
		bool IsPrefix(string src, string target, CompareOptions opt);

		// Token: 0x060036B0 RID: 14000
		bool IsSuffix(string src, string target, CompareOptions opt);

		// Token: 0x060036B1 RID: 14001
		int IndexOf(string s, string target, int start, int length, CompareOptions opt);

		// Token: 0x060036B2 RID: 14002
		int LastIndexOf(string s, string target, int start, int length, CompareOptions opt);
	}
}
