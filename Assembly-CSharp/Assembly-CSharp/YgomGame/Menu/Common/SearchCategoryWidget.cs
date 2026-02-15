using System;
using TMPro;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B5F RID: 2911
	public class SearchCategoryWidget : ElementWidgetBase
	{
		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06005432 RID: 21554 RVA: 0x000029CC File Offset: 0x00000BCC
		public int categoryId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06005433 RID: 21555 RVA: 0x0000216A File Offset: 0x0000036A
		public string categoryName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public SearchCategoryWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x0000216A File Offset: 0x0000036A
		public SearchCategoryWidget Binding(int id, string name)
		{
			return null;
		}

		// Token: 0x040091B1 RID: 37297
		private int m_categoryId;

		// Token: 0x040091B2 RID: 37298
		private string m_categoryName;

		// Token: 0x040091B3 RID: 37299
		private TextMeshProUGUI m_TextOn;

		// Token: 0x040091B4 RID: 37300
		private TextMeshProUGUI m_TextOff;
	}
}
