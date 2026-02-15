using System;

namespace Unity.Properties
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public class MissingPropertyBagException : Exception
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000255B File Offset: 0x0000075B
		public MissingPropertyBagException(Type type)
			: base(MissingPropertyBagException.GetMessageForType(type))
		{
			this.<Type>k__BackingField = type;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002574 File Offset: 0x00000774
		private static string GetMessageForType(Type type)
		{
			return "No PropertyBag was found for Type=[" + type.FullName + "]. Please make sure all types are declared ahead of time using [GeneratePropertyBagAttribute], [GeneratePropertyBagsForTypeAttribute] or [GeneratePropertyBagsForTypesQualifiedWithAttribute]";
		}
	}
}
