using System;

namespace Unity.Properties
{
	// Token: 0x02000048 RID: 72
	public abstract class ConcreteTypeVisitor : IPropertyBagVisitor
	{
		// Token: 0x0600012B RID: 299
		protected abstract void VisitContainer<TContainer>(ref TContainer container);

		// Token: 0x0600012C RID: 300 RVA: 0x000055BF File Offset: 0x000037BF
		void IPropertyBagVisitor.Visit<TContainer>(IPropertyBag<TContainer> properties, ref TContainer container)
		{
			this.VisitContainer<TContainer>(ref container);
		}
	}
}
