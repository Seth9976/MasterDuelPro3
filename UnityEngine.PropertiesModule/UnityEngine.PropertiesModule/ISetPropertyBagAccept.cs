using System;

namespace Unity.Properties
{
	// Token: 0x0200004B RID: 75
	public interface ISetPropertyBagAccept<TContainer>
	{
		// Token: 0x06000130 RID: 304
		void Accept(ISetPropertyBagVisitor visitor, ref TContainer container);
	}
}
