using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200048E RID: 1166
	public class UxmlLongAttributeDescription : TypedUxmlAttributeDescription<long>
	{
		// Token: 0x060021D6 RID: 8662 RVA: 0x0007C101 File Offset: 0x0007A301
		public UxmlLongAttributeDescription()
		{
			base.type = "long";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0L;
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x0007C12C File Offset: 0x0007A32C
		public override long GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<long>(bag, cc, (string s, long l) => UxmlLongAttributeDescription.ConvertValueToLong(s, l), base.defaultValue);
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0007C16C File Offset: 0x0007A36C
		private static long ConvertValueToLong(string v, long defaultValue)
		{
			long i;
			bool flag = v == null || !long.TryParse(v, out i);
			long num;
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
