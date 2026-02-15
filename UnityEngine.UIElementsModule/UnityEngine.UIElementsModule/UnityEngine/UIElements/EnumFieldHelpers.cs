using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000BF RID: 191
	internal static class EnumFieldHelpers
	{
		// Token: 0x06000610 RID: 1552 RVA: 0x0001CE48 File Offset: 0x0001B048
		internal static bool ExtractValue(IUxmlAttributes bag, CreationContext cc, out Type resEnumType, out Enum resEnumValue, out bool resIncludeObsoleteValues)
		{
			resIncludeObsoleteValues = false;
			resEnumValue = null;
			resEnumType = EnumFieldHelpers.type.GetValueFromBag(bag, cc);
			bool flag = resEnumType == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				string specifiedValue = null;
				object resEnumValueObject = null;
				bool flag3 = EnumFieldHelpers.value.TryGetValueFromBag(bag, cc, ref specifiedValue) && !Enum.TryParse(resEnumType, specifiedValue, false, out resEnumValueObject);
				if (flag3)
				{
					Debug.LogErrorFormat("EnumField: Could not parse value of '{0}', because it isn't defined in the {1} enum.", new object[] { specifiedValue, resEnumType.FullName });
					flag2 = false;
				}
				else
				{
					resEnumValue = ((specifiedValue != null && resEnumValueObject != null) ? ((Enum)resEnumValueObject) : ((Enum)Enum.ToObject(resEnumType, 0)));
					resIncludeObsoleteValues = EnumFieldHelpers.includeObsoleteValues.GetValueFromBag(bag, cc);
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x040003B7 RID: 951
		internal static readonly UxmlTypeAttributeDescription<Enum> type = new UxmlTypeAttributeDescription<Enum>
		{
			name = "type"
		};

		// Token: 0x040003B8 RID: 952
		internal static readonly UxmlStringAttributeDescription value = new UxmlStringAttributeDescription
		{
			name = "value"
		};

		// Token: 0x040003B9 RID: 953
		internal static readonly UxmlBoolAttributeDescription includeObsoleteValues = new UxmlBoolAttributeDescription
		{
			name = "include-obsolete-values",
			defaultValue = false
		};
	}
}
