using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200049D RID: 1181
	[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public abstract class UxmlTraits : BaseUxmlTraits
	{
		// Token: 0x060021FF RID: 8703 RVA: 0x000020EA File Offset: 0x000002EA
		public virtual void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
		{
		}
	}
}
