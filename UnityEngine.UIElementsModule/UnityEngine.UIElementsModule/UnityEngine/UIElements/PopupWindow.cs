using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000124 RID: 292
	public class PopupWindow : TextElement
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x0002B238 File Offset: 0x00029438
		public PopupWindow()
		{
			base.AddToClassList(PopupWindow.ussClassName);
			this.m_ContentContainer = new VisualElement
			{
				name = "unity-content-container"
			};
			this.m_ContentContainer.AddToClassList(PopupWindow.contentUssClassName);
			base.hierarchy.Add(this.m_ContentContainer);
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0002B298 File Offset: 0x00029498
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x040005A4 RID: 1444
		private VisualElement m_ContentContainer;

		// Token: 0x040005A5 RID: 1445
		public new static readonly string ussClassName = "unity-popup-window";

		// Token: 0x040005A6 RID: 1446
		public static readonly string contentUssClassName = PopupWindow.ussClassName + "__content-container";

		// Token: 0x02000125 RID: 293
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<PopupWindow, PopupWindow.UxmlTraits>
		{
		}

		// Token: 0x02000126 RID: 294
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextElement.UxmlTraits
		{
		}
	}
}
