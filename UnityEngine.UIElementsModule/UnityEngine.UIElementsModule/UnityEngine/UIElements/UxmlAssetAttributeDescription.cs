using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200047E RID: 1150
	public class UxmlAssetAttributeDescription<T> : TypedUxmlAttributeDescription<T> where T : Object
	{
		// Token: 0x06002199 RID: 8601 RVA: 0x0007B654 File Offset: 0x00079854
		public UxmlAssetAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = default(T);
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x0007B694 File Offset: 0x00079894
		public override T GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			string path;
			VisualTreeAsset sourceAsset;
			bool flag = base.TryGetValueFromBagAsString(bag, cc, out path, out sourceAsset) && sourceAsset != null;
			T t;
			if (flag)
			{
				t = sourceAsset.GetAsset<T>(path);
			}
			else
			{
				t = default(T);
			}
			return t;
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x0007B6D8 File Offset: 0x000798D8
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, out T value)
		{
			string path;
			VisualTreeAsset sourceAsset;
			bool flag = base.TryGetValueFromBagAsString(bag, cc, out path, out sourceAsset) && sourceAsset != null;
			bool flag2;
			if (flag)
			{
				value = sourceAsset.GetAsset<T>(path);
				flag2 = true;
			}
			else
			{
				value = default(T);
				flag2 = false;
			}
			return flag2;
		}
	}
}
