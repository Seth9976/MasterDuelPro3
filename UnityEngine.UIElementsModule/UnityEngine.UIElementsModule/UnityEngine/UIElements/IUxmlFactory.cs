using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004A0 RID: 1184
	[Obsolete("IUxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public interface IUxmlFactory : IBaseUxmlFactory
	{
		// Token: 0x06002206 RID: 8710
		VisualElement Create(IUxmlAttributes bag, CreationContext cc);
	}
}
