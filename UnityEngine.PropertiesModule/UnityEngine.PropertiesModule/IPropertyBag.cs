using System;

namespace Unity.Properties
{
	// Token: 0x02000032 RID: 50
	public interface IPropertyBag
	{
		// Token: 0x060000BD RID: 189
		void Accept(ITypeVisitor visitor);

		// Token: 0x060000BE RID: 190
		void Accept(IPropertyBagVisitor visitor, ref object container);
	}
}
