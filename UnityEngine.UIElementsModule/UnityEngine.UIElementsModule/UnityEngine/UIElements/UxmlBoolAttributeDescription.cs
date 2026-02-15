using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000490 RID: 1168
	public class UxmlBoolAttributeDescription : TypedUxmlAttributeDescription<bool>
	{
		// Token: 0x060021DC RID: 8668 RVA: 0x0007C1AD File Offset: 0x0007A3AD
		public UxmlBoolAttributeDescription()
		{
			base.type = "boolean";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = false;
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x0007C1D8 File Offset: 0x0007A3D8
		public override bool GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<bool>(bag, cc, (string s, bool b) => UxmlBoolAttributeDescription.ConvertValueToBool(s, b), base.defaultValue);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0007C218 File Offset: 0x0007A418
		private static bool ConvertValueToBool(string v, bool defaultValue)
		{
			bool i;
			bool flag = v == null || !bool.TryParse(v, out i);
			bool flag2;
			if (flag)
			{
				flag2 = defaultValue;
			}
			else
			{
				flag2 = i;
			}
			return flag2;
		}
	}
}
