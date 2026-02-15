using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000474 RID: 1140
	[Obsolete("UxmlRootElementTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlRootElementTraits : UxmlTraits
	{
		// Token: 0x04000ECE RID: 3790
		protected UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name"
		};

		// Token: 0x04000ECF RID: 3791
		private UxmlStringAttributeDescription m_Class = new UxmlStringAttributeDescription
		{
			name = "class"
		};
	}
}
