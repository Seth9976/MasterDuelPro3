using System;

namespace Unity.Properties
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class GeneratePropertyBagsForTypesQualifiedWithAttribute : Attribute
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002508 File Offset: 0x00000708
		public GeneratePropertyBagsForTypesQualifiedWithAttribute(Type type, TypeGenerationOptions options = TypeGenerationOptions.Default)
		{
			bool flag = type == null;
			if (flag)
			{
				throw new ArgumentException("type is null.");
			}
			bool flag2 = !type.IsInterface;
			if (flag2)
			{
				throw new ArgumentException("GeneratePropertyBagsForTypesQualifiedWithAttribute Type must be an interface type.");
			}
			this.<Type>k__BackingField = type;
			this.<Options>k__BackingField = options;
		}
	}
}
