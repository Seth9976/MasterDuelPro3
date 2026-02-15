using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200048C RID: 1164
	public class UxmlUnsignedLongAttributeDescription : TypedUxmlAttributeDescription<ulong>
	{
		// Token: 0x060021D0 RID: 8656 RVA: 0x0007C055 File Offset: 0x0007A255
		public UxmlUnsignedLongAttributeDescription()
		{
			base.type = "unsignedLong";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0UL;
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x0007C080 File Offset: 0x0007A280
		public override ulong GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<ulong>(bag, cc, (string s, ulong l) => UxmlUnsignedLongAttributeDescription.ConvertValueToUlong(s, l), base.defaultValue);
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x0007C0C0 File Offset: 0x0007A2C0
		private static ulong ConvertValueToUlong(string v, ulong defaultValue)
		{
			ulong i;
			bool flag = v == null || !ulong.TryParse(v, out i);
			ulong num;
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
