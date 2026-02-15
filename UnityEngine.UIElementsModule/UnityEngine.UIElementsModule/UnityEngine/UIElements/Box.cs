using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000094 RID: 148
	public class Box : VisualElement
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x0001B2E6 File Offset: 0x000194E6
		public Box()
		{
			base.AddToClassList(Box.ussClassName);
		}

		// Token: 0x04000346 RID: 838
		public static readonly string ussClassName = "unity-box";

		// Token: 0x02000095 RID: 149
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Box>
		{
		}
	}
}
