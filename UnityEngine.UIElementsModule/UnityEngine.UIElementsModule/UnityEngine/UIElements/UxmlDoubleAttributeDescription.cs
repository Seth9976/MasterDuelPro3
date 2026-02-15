using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000486 RID: 1158
	public class UxmlDoubleAttributeDescription : TypedUxmlAttributeDescription<double>
	{
		// Token: 0x060021BC RID: 8636 RVA: 0x0007BDFB File Offset: 0x00079FFB
		public UxmlDoubleAttributeDescription()
		{
			base.type = "double";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0.0;
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x0007BE30 File Offset: 0x0007A030
		public override double GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<double>(bag, cc, (string s, double d) => UxmlDoubleAttributeDescription.ConvertValueToDouble(s, d), base.defaultValue);
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x0007BE70 File Offset: 0x0007A070
		private static double ConvertValueToDouble(string v, double defaultValue)
		{
			double i;
			bool flag = v == null || !double.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture, out i);
			double num;
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
