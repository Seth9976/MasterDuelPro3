using System;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C64 RID: 3172
	public class ProductContainerWidget : ElementWidgetBase
	{
		// Token: 0x06005A91 RID: 23185 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ProductContainerWidget(ElementObjectManager eom, ElementObjectManager productPref, int contentLength)
			: base(null)
		{
		}

		// Token: 0x06005A92 RID: 23186 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClick(ProductWidget clickedWidget)
		{
		}

		// Token: 0x040095E3 RID: 38371
		public readonly ProductWidget[] productWidgets;

		// Token: 0x040095E4 RID: 38372
		public Action<ProductContainerWidget, ProductWidget> onClickCallback;
	}
}
