using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000495 RID: 1173
	public class UxmlHash128AttributeDescription : TypedUxmlAttributeDescription<Hash128>
	{
		// Token: 0x060021F0 RID: 8688 RVA: 0x0007C5FC File Offset: 0x0007A7FC
		public UxmlHash128AttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = default(Hash128);
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x0007C63C File Offset: 0x0007A83C
		public override Hash128 GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<Hash128>(bag, cc, (string s, Hash128 i) => Hash128.Parse(s), base.defaultValue);
		}
	}
}
