using System;

namespace Unity.Properties
{
	// Token: 0x0200004A RID: 74
	public interface IListPropertyBagAccept<TContainer>
	{
		// Token: 0x0600012F RID: 303
		void Accept(IListPropertyBagVisitor visitor, ref TContainer container);
	}
}
