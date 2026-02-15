using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200049E RID: 1182
	[Obsolete("UxmlObjectTraits<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	internal abstract class UxmlObjectTraits<T> : BaseUxmlTraits
	{
		// Token: 0x06002201 RID: 8705 RVA: 0x000020EA File Offset: 0x000002EA
		public virtual void Init(ref T obj, IUxmlAttributes bag, CreationContext cc)
		{
		}
	}
}
