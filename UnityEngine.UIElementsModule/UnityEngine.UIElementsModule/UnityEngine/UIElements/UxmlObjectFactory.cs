using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004A5 RID: 1189
	[Obsolete("UxmlObjectFactory<TCreatedType, TTraits> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	internal class UxmlObjectFactory<TCreatedType, TTraits> : BaseUxmlFactory<TCreatedType, TTraits>, IUxmlObjectFactory<TCreatedType>, IBaseUxmlObjectFactory, IBaseUxmlFactory where TCreatedType : new() where TTraits : UxmlObjectTraits<TCreatedType>, new()
	{
		// Token: 0x06002210 RID: 8720 RVA: 0x0007C8D8 File Offset: 0x0007AAD8
		public virtual TCreatedType CreateObject(IUxmlAttributes bag, CreationContext cc)
		{
			TCreatedType obj = new TCreatedType();
			this.m_Traits.Init(ref obj, bag, cc);
			return obj;
		}
	}
}
