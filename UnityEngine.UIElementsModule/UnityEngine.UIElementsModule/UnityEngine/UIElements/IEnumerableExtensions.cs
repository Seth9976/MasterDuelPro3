using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200025E RID: 606
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class IEnumerableExtensions
	{
		// Token: 0x0600108D RID: 4237 RVA: 0x00047098 File Offset: 0x00045298
		internal static bool HasValues(this IEnumerable<string> collection)
		{
			bool flag = collection == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				using (IEnumerator<string> enumerator = collection.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						string unused = enumerator.Current;
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}
	}
}
