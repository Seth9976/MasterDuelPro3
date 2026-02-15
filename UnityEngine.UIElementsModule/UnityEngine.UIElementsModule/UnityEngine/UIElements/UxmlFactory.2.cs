using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004A6 RID: 1190
	[Obsolete("UxmlFactory<TCreatedType> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlFactory<TCreatedType> : UxmlFactory<TCreatedType, VisualElement.UxmlTraits> where TCreatedType : VisualElement, new()
	{
	}
}
