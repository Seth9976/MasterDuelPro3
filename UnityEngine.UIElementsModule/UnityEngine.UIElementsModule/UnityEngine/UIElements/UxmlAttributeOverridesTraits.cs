using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200047A RID: 1146
	[Obsolete("UxmlAttributeOverridesTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlAttributeOverridesTraits : UxmlTraits
	{
		// Token: 0x04000ED6 RID: 3798
		private UxmlStringAttributeDescription m_ElementName = new UxmlStringAttributeDescription
		{
			name = "element-name",
			use = UxmlAttributeDescription.Use.Required
		};
	}
}
