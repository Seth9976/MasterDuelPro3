using System;

namespace Unity.Properties
{
	// Token: 0x0200004D RID: 77
	public interface IPropertyAccept<TContainer>
	{
		// Token: 0x06000132 RID: 306
		void Accept(IPropertyVisitor visitor, ref TContainer container);
	}
}
