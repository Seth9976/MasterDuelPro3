using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000488 RID: 1160
	public class UxmlIntAttributeDescription : TypedUxmlAttributeDescription<int>
	{
		// Token: 0x060021C2 RID: 8642 RVA: 0x0007BEBB File Offset: 0x0007A0BB
		public UxmlIntAttributeDescription()
		{
			base.type = "int";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0;
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x0007BEE8 File Offset: 0x0007A0E8
		public override int GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<int>(bag, cc, (string s, int i) => UxmlIntAttributeDescription.ConvertValueToInt(s, i), base.defaultValue);
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x0007BF28 File Offset: 0x0007A128
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref int value)
		{
			return base.TryGetValueFromBag<int>(bag, cc, (string s, int i) => UxmlIntAttributeDescription.ConvertValueToInt(s, i), base.defaultValue, ref value);
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x0007BF68 File Offset: 0x0007A168
		private static int ConvertValueToInt(string v, int defaultValue)
		{
			int i;
			bool flag = v == null || !int.TryParse(v, out i);
			int num;
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
