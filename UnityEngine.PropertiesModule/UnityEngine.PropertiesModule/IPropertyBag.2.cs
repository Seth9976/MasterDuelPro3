using System;

namespace Unity.Properties
{
	// Token: 0x02000033 RID: 51
	public interface IPropertyBag<TContainer> : IPropertyBag
	{
		// Token: 0x060000BF RID: 191
		PropertyCollection<TContainer> GetProperties();

		// Token: 0x060000C0 RID: 192
		PropertyCollection<TContainer> GetProperties(ref TContainer container);

		// Token: 0x060000C1 RID: 193
		void Accept(IPropertyBagVisitor visitor, ref TContainer container);
	}
}
