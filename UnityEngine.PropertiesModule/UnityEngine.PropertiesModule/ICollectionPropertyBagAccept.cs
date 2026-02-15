using System;

namespace Unity.Properties
{
	// Token: 0x02000049 RID: 73
	public interface ICollectionPropertyBagAccept<TContainer>
	{
		// Token: 0x0600012E RID: 302
		void Accept(ICollectionPropertyBagVisitor visitor, ref TContainer container);
	}
}
