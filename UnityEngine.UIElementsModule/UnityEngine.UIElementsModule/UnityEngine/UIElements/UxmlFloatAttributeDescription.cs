using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000484 RID: 1156
	public class UxmlFloatAttributeDescription : TypedUxmlAttributeDescription<float>
	{
		// Token: 0x060021B6 RID: 8630 RVA: 0x0007BD40 File Offset: 0x00079F40
		public UxmlFloatAttributeDescription()
		{
			base.type = "float";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0f;
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x0007BD70 File Offset: 0x00079F70
		public override float GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<float>(bag, cc, (string s, float f) => UxmlFloatAttributeDescription.ConvertValueToFloat(s, f), base.defaultValue);
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x0007BDB0 File Offset: 0x00079FB0
		private static float ConvertValueToFloat(string v, float defaultValue)
		{
			float i;
			bool flag = v == null || !float.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture, out i);
			float num;
			if (flag)
			{
				num = defaultValue;
			}
			else
			{
				num = i;
			}
			return num;
		}
	}
}
