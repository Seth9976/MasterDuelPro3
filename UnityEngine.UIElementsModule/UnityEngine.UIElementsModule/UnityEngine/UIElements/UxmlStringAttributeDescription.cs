using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000482 RID: 1154
	public class UxmlStringAttributeDescription : TypedUxmlAttributeDescription<string>
	{
		// Token: 0x060021AF RID: 8623 RVA: 0x0007BC85 File Offset: 0x00079E85
		public UxmlStringAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = "";
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0007BCB4 File Offset: 0x00079EB4
		public override string GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<string>(bag, cc, (string s, string t) => s, base.defaultValue);
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x0007BCF4 File Offset: 0x00079EF4
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref string value)
		{
			return base.TryGetValueFromBag<string>(bag, cc, (string s, string t) => s, base.defaultValue, ref value);
		}
	}
}
