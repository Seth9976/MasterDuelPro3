using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004A2 RID: 1186
	[Obsolete("IUxmlObjectFactory<out T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	internal interface IUxmlObjectFactory<out T> : IBaseUxmlObjectFactory, IBaseUxmlFactory where T : new()
	{
		// Token: 0x06002207 RID: 8711
		T CreateObject(IUxmlAttributes bag, CreationContext cc);
	}
}
