using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000498 RID: 1176
	internal class UxmlObjectListAttributeDescription<T> : UxmlObjectAttributeDescription<List<T>> where T : new()
	{
		// Token: 0x060021F8 RID: 8696 RVA: 0x0007C718 File Offset: 0x0007A918
		public override List<T> GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			VisualTreeAsset visualTreeAsset = cc.visualTreeAsset;
			List<T> uxmlObjects = ((visualTreeAsset != null) ? visualTreeAsset.GetUxmlObjects<T>(bag, cc) : null);
			bool flag = uxmlObjects != null;
			List<T> list2;
			if (flag)
			{
				List<T> list = null;
				foreach (T child in uxmlObjects)
				{
					bool flag2 = list == null;
					if (flag2)
					{
						list = new List<T>();
					}
					list.Add(child);
				}
				list2 = list;
			}
			else
			{
				list2 = base.defaultValue;
			}
			return list2;
		}
	}
}
