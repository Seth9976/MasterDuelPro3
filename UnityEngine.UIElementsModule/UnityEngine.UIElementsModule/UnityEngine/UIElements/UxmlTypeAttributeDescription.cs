using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000492 RID: 1170
	public class UxmlTypeAttributeDescription<TBase> : TypedUxmlAttributeDescription<Type>
	{
		// Token: 0x060021E2 RID: 8674 RVA: 0x0007C259 File Offset: 0x0007A459
		public UxmlTypeAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = null;
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x0007C284 File Offset: 0x0007A484
		public override Type GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<Type>(bag, cc, (string s, Type type1) => this.ConvertValueToType(s, type1), base.defaultValue);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0007C2B0 File Offset: 0x0007A4B0
		private Type ConvertValueToType(string v, Type defaultValue)
		{
			bool flag = string.IsNullOrEmpty(v);
			Type type2;
			if (flag)
			{
				type2 = defaultValue;
			}
			else
			{
				try
				{
					Type type = Type.GetType(v, true);
					bool flag2 = !typeof(TBase).IsAssignableFrom(type);
					if (!flag2)
					{
						return type;
					}
					Debug.LogError(string.Concat(new string[]
					{
						"Type: Invalid type \"",
						v,
						"\". Type must derive from ",
						typeof(TBase).FullName,
						"."
					}));
				}
				catch (Exception e)
				{
					Debug.LogException(e);
				}
				type2 = defaultValue;
			}
			return type2;
		}
	}
}
