using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000497 RID: 1175
	internal class UxmlObjectAttributeDescription<T> where T : new()
	{
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x0007C692 File Offset: 0x0007A892
		public T defaultValue { get; }

		// Token: 0x060021F6 RID: 8694 RVA: 0x0007C69C File Offset: 0x0007A89C
		public virtual T GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			VisualTreeAsset visualTreeAsset = cc.visualTreeAsset;
			List<T> uxmlObjects = ((visualTreeAsset != null) ? visualTreeAsset.GetUxmlObjects<T>(bag, cc) : null);
			bool flag = uxmlObjects != null;
			if (flag)
			{
				using (List<T>.Enumerator enumerator = uxmlObjects.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return enumerator.Current;
					}
				}
			}
			return this.defaultValue;
		}
	}
}
