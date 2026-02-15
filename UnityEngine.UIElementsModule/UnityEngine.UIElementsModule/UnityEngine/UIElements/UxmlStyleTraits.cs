using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000476 RID: 1142
	[Obsolete("UxmlStyleTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
	public class UxmlStyleTraits : UxmlTraits
	{
		// Token: 0x04000ED0 RID: 3792
		private UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
		{
			name = "name"
		};

		// Token: 0x04000ED1 RID: 3793
		private UxmlStringAttributeDescription m_Path = new UxmlStringAttributeDescription
		{
			name = "path"
		};

		// Token: 0x04000ED2 RID: 3794
		private UxmlStringAttributeDescription m_Src = new UxmlStringAttributeDescription
		{
			name = "src"
		};
	}
}
