using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004A4 RID: 1188
	[Obsolete("UxmlFactory<TCreatedType, TTraits> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlFactory<TCreatedType, TTraits> : BaseUxmlFactory<TCreatedType, TTraits>, IUxmlFactory, IBaseUxmlFactory where TCreatedType : VisualElement, new() where TTraits : UxmlTraits, new()
	{
		// Token: 0x0600220E RID: 8718 RVA: 0x0007C898 File Offset: 0x0007AA98
		public virtual VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			TCreatedType ve = new TCreatedType();
			this.m_Traits.Init(ve, bag, cc);
			return ve;
		}
	}
}
