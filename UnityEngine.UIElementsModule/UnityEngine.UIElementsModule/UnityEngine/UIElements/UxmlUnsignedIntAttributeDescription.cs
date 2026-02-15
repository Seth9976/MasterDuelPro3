using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200048A RID: 1162
	public class UxmlUnsignedIntAttributeDescription : TypedUxmlAttributeDescription<uint>
	{
		// Token: 0x060021CA RID: 8650 RVA: 0x0007BFA9 File Offset: 0x0007A1A9
		public UxmlUnsignedIntAttributeDescription()
		{
			base.type = "unsignedInt";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0U;
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x0007BFD4 File Offset: 0x0007A1D4
		public override uint GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<uint>(bag, cc, (string s, uint i) => UxmlUnsignedIntAttributeDescription.ConvertValueToUInt(s, i), base.defaultValue);
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x0007C014 File Offset: 0x0007A214
		private static uint ConvertValueToUInt(string v, uint defaultValue)
		{
			uint i;
			bool flag = v == null || !uint.TryParse(v, out i);
			uint num;
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
