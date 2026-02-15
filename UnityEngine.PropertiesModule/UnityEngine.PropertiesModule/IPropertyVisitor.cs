using System;

namespace Unity.Properties
{
	// Token: 0x02000055 RID: 85
	public interface IPropertyVisitor
	{
		// Token: 0x0600013A RID: 314
		void Visit<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container);
	}
}
