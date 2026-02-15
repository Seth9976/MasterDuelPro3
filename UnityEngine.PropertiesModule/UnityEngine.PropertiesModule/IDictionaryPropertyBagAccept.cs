using System;

namespace Unity.Properties
{
	// Token: 0x0200004C RID: 76
	public interface IDictionaryPropertyBagAccept<TContainer>
	{
		// Token: 0x06000131 RID: 305
		void Accept(IDictionaryPropertyBagVisitor visitor, ref TContainer container);
	}
}
