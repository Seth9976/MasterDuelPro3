using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x02000938 RID: 2360
	public class ProductContainerWidget : ElementWidgetBase
	{
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060044D3 RID: 17619 RVA: 0x0000216A File Offset: 0x0000036A
		public List<ProductWidget> productWidgets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ProductContainerWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x0000216D File Offset: 0x0000036D
		public void ApplyContents(ProductWidgetController widgetController, ProductContainerWidget.Context ctx, IProductContainerWidgetHandler handler, Action<ProductWidget> onReturnWidget = null)
		{
		}

		// Token: 0x0400832D RID: 33581
		private List<ProductWidget> m_ActiveWidgets;

		// Token: 0x0400832E RID: 33582
		private Dictionary<string, List<ProductWidget>> m_HoldWidgetsMap;

		// Token: 0x0400832F RID: 33583
		public Action<ProductWidget, ProductContext> onUpdateContentWidget;

		// Token: 0x04008330 RID: 33584
		public Action<ProductWidget, int> onClickCallback;

		// Token: 0x04008331 RID: 33585
		public Action<ProductWidget, int> onSelectedCallback;

		// Token: 0x02000939 RID: 2361
		public class Context
		{
			// Token: 0x1700059B RID: 1435
			// (get) Token: 0x060044D6 RID: 17622 RVA: 0x000029CC File Offset: 0x00000BCC
			public int Count
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x060044D7 RID: 17623 RVA: 0x00002739 File Offset: 0x00000939
			public Context(int capacity)
			{
			}

			// Token: 0x060044D8 RID: 17624 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x060044D9 RID: 17625 RVA: 0x0000216D File Offset: 0x0000036D
			public void Add(string productWidgetLabel, ProductContext productCtx)
			{
			}

			// Token: 0x04008332 RID: 33586
			public readonly List<ProductContext> productCtxs;
		}
	}
}
