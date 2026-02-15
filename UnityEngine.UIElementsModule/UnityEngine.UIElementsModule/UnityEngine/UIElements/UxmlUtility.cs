using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004B0 RID: 1200
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class UxmlUtility
	{
		// Token: 0x06002238 RID: 8760 RVA: 0x0007CFF4 File Offset: 0x0007B1F4
		public static List<string> ParseStringListAttribute(string itemList)
		{
			bool flag = string.IsNullOrEmpty((itemList != null) ? itemList.Trim() : null);
			List<string> list;
			if (flag)
			{
				list = null;
			}
			else
			{
				string[] items = itemList.Split(',', StringSplitOptions.None);
				bool flag2 = items.Length != 0;
				if (flag2)
				{
					List<string> result = new List<string>();
					foreach (string item in items)
					{
						result.Add(item.Trim());
					}
					list = result;
				}
				else
				{
					list = null;
				}
			}
			return list;
		}
	}
}
