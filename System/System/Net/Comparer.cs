using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x020003E1 RID: 993
	internal class Comparer : IComparer
	{
		// Token: 0x060018B2 RID: 6322 RVA: 0x000693D8 File Offset: 0x000675D8
		int IComparer.Compare(object ol, object or)
		{
			Cookie cookie = (Cookie)ol;
			Cookie cookie2 = (Cookie)or;
			int num;
			if ((num = string.Compare(cookie.Name, cookie2.Name, StringComparison.OrdinalIgnoreCase)) != 0)
			{
				return num;
			}
			if ((num = string.Compare(cookie.Domain, cookie2.Domain, StringComparison.OrdinalIgnoreCase)) != 0)
			{
				return num;
			}
			if ((num = string.Compare(cookie.Path, cookie2.Path, StringComparison.Ordinal)) != 0)
			{
				return num;
			}
			return 0;
		}
	}
}
