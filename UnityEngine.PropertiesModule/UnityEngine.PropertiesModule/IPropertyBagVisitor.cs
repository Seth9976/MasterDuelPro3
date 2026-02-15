using System;

namespace Unity.Properties
{
	// Token: 0x02000050 RID: 80
	public interface IPropertyBagVisitor
	{
		// Token: 0x06000135 RID: 309
		void Visit<TContainer>(IPropertyBag<TContainer> properties, ref TContainer container);
	}
}
