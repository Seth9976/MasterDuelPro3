using System;
using UnityEngine.TextCore;

namespace UnityEngine.UIElements
{
	// Token: 0x020004CA RID: 1226
	internal static class LanguageDirectionExtensions
	{
		// Token: 0x060022BC RID: 8892 RVA: 0x0007FE10 File Offset: 0x0007E010
		internal static LanguageDirection toTextCore(this LanguageDirection dir)
		{
			LanguageDirection languageDirection;
			if (dir > LanguageDirection.LTR)
			{
				if (dir != LanguageDirection.RTL)
				{
					throw new ArgumentOutOfRangeException("dir", dir, "impossible to convert value");
				}
				languageDirection = LanguageDirection.RTL;
			}
			else
			{
				languageDirection = LanguageDirection.LTR;
			}
			return languageDirection;
		}
	}
}
