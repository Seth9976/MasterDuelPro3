using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000478 RID: 1144
	[Obsolete("UxmlTemplateTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlTemplateTraits : UxmlTraits
	{
		// Token: 0x04000ED3 RID: 3795
		private UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name",
			use = UxmlAttributeDescription.Use.Required
		};

		// Token: 0x04000ED4 RID: 3796
		private UxmlStringAttributeDescription m_Path = new UxmlStringAttributeDescription
		{
			name = "path"
		};

		// Token: 0x04000ED5 RID: 3797
		private UxmlStringAttributeDescription m_Src = new UxmlStringAttributeDescription
		{
			name = "src"
		};
	}
}
