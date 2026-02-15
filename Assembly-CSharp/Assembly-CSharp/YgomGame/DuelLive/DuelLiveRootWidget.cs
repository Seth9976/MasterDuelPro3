using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C56 RID: 3158
	public class DuelLiveRootWidget : ElementWidgetBase
	{
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06005A20 RID: 23072 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelLiveRootWidget.Context context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06005A21 RID: 23073 RVA: 0x000029CC File Offset: 0x00000BCC
		public int currentSubTabIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06005A22 RID: 23074 RVA: 0x000029CC File Offset: 0x00000BCC
		public int currentSubTabSectionIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06005A23 RID: 23075 RVA: 0x0000216A File Offset: 0x0000036A
		public ProductShowcaseWidget currentShowcase
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06005A24 RID: 23076 RVA: 0x0000216A File Offset: 0x0000036A
		public ProductShowcaseWidget.Context currentShowcaseCtx
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06005A25 RID: 23077 RVA: 0x0000216A File Offset: 0x0000036A
		public ProductListWidget currentProductList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06005A26 RID: 23078 RVA: 0x0000216A File Offset: 0x0000036A
		public ProductListWidget.Context currentProductListCtx
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06005A27 RID: 23079 RVA: 0x0000216A File Offset: 0x0000036A
		public SubTabListWidget currentSubTabList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06005A28 RID: 23080 RVA: 0x0000216A File Offset: 0x0000036A
		public List<SubTabListWidget.TabContext> currentSubTabListCtxs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06005A29 RID: 23081 RVA: 0x0000216A File Offset: 0x0000036A
		public SubTabListWidget.TabContext currentSubTabListCtx
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06005A2A RID: 23082 RVA: 0x000029CC File Offset: 0x00000BCC
		public int currentSubTabSectionLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06005A2B RID: 23083 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetSectionIdxByProduct(IProductContext product)
		{
			return 0;
		}

		// Token: 0x06005A2C RID: 23084 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public DuelLiveRootWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06005A2D RID: 23085 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitContext(DuelLiveRootWidget.Context context)
		{
		}

		// Token: 0x06005A2E RID: 23086 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitData()
		{
		}

		// Token: 0x06005A2F RID: 23087 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitProductGruopData()
		{
		}

		// Token: 0x06005A30 RID: 23088 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitBGImage()
		{
		}

		// Token: 0x06005A31 RID: 23089 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitSubCategoryTab()
		{
		}

		// Token: 0x06005A32 RID: 23090 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitProduct(Action onComplete)
		{
		}

		// Token: 0x06005A33 RID: 23091 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateProductContexts()
		{
		}

		// Token: 0x06005A34 RID: 23092 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateShowcase(int asyncCnt = 0, bool resetPos = true, Action onComplete = null)
		{
		}

		// Token: 0x06005A35 RID: 23093 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateProductList(int asyncCnt = 0, bool resetPos = true, Action onComplete = null)
		{
		}

		// Token: 0x06005A36 RID: 23094 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateSubTabList()
		{
		}

		// Token: 0x040095AC RID: 38316
		private const string k_ELabelMonsterImage = "MonsterImage";

		// Token: 0x040095AD RID: 38317
		private const string k_ALabelGroupHeaderPref = "DuelLiveGroupHeaderWidget";

		// Token: 0x040095AE RID: 38318
		private const string k_ALabelGroupEmptyPref = "DuelLiveGroupEmptyWidget";

		// Token: 0x040095AF RID: 38319
		private const string k_ALabelContainerWidget = "DuelLiveContainerWidget";

		// Token: 0x040095B0 RID: 38320
		private const string k_ALabelProductWidgetPref = "DuelLiveProductWidget";

		// Token: 0x040095B1 RID: 38321
		private const string k_ALabelProductRandomWidgetPref = "DuelLiveProductRandomWidget";

		// Token: 0x040095B2 RID: 38322
		private const string k_ALabelProductVSWidgetPref = "DuelLiveProductVSWidget";

		// Token: 0x040095B3 RID: 38323
		private const string k_ALabelProductEventWidgetPref = "DuelLiveProductEventWidget";

		// Token: 0x040095B4 RID: 38324
		private const string k_ALabelProductOfficialAccountWidgetPref = "DuelLiveProductOfficialAccountWidget";

		// Token: 0x040095B5 RID: 38325
		private const string k_ALabelProductCommingSoonWidgetPref = "DuelLiveProductCommingSoonWidget";

		// Token: 0x040095B6 RID: 38326
		public readonly ProductShowcaseWidget productShowcase;

		// Token: 0x040095B7 RID: 38327
		private DuelLiveRootWidget.Context m_Context;

		// Token: 0x040095B8 RID: 38328
		public bool isPopWallPaper;

		// Token: 0x040095B9 RID: 38329
		private List<List<IProductContext>> m_SubProductsTmpList;

		// Token: 0x02000C57 RID: 3159
		public class Context
		{
			// Token: 0x06005A37 RID: 23095 RVA: 0x00002739 File Offset: 0x00000939
			public Context(DuelLiveSettings duelLiveSettings)
			{
			}

			// Token: 0x06005A38 RID: 23096 RVA: 0x0000216D File Offset: 0x0000036D
			public void ImportAll()
			{
			}

			// Token: 0x040095BA RID: 38330
			public readonly DuelLiveSettings duelLiveSettings;

			// Token: 0x040095BB RID: 38331
			public readonly ProductContextCollection<DuelLiveProductContext> productCollection;
		}
	}
}
