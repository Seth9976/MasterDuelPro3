using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomGame.Shop
{
	// Token: 0x02000944 RID: 2372
	public class ProductWidgetController
	{
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600459A RID: 17818 RVA: 0x0000216A File Offset: 0x0000036A
		public List<GameObject> templateList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600459B RID: 17819 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600459C RID: 17820 RVA: 0x0000216D File Offset: 0x0000036D
		public int headerTemplateIdx
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600459D RID: 17821 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600459E RID: 17822 RVA: 0x0000216D File Offset: 0x0000036D
		public int emptyTemplateIdx
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x0600459F RID: 17823 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x060045A0 RID: 17824 RVA: 0x0000216D File Offset: 0x0000036D
		public float containerWidth
		{
			[CompilerGenerated]
			get
			{
				return 0f;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060045A1 RID: 17825 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060045A2 RID: 17826 RVA: 0x0000216D File Offset: 0x0000036D
		public RectOffset containerPadding
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060045A3 RID: 17827 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x060045A4 RID: 17828 RVA: 0x0000216D File Offset: 0x0000036D
		public float containerSpacing
		{
			[CompilerGenerated]
			get
			{
				return 0f;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsContainerTemplateIdx(int templateIdx)
		{
			return false;
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetContainerTemplateIdx(float productHeight)
		{
			return 0;
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x000F4898 File Offset: 0x000F2A98
		public Vector2 GetProductSize(string productWidgetLabel)
		{
			return default(Vector2);
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsReservedProductWidget(ProductWidget productWidget)
		{
			return false;
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x00002739 File Offset: 0x00000939
		public ProductWidgetController(AssetContainer productWidgetMap)
		{
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x0000216D File Offset: 0x0000036D
		public void CreateTemplates(Transform parent)
		{
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x0000216D File Offset: 0x0000036D
		private void AssignTemplate(GameObject pref, string label = null)
		{
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x0000216D File Offset: 0x0000036D
		private void AssignProductTemplate(string label, Transform parent, GameObject pref)
		{
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject CreateContainerTemplate(Transform parent, float height)
		{
			return null;
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x0000216A File Offset: 0x0000036A
		public ProductWidget RentProductWidget(string label, Transform parent)
		{
			return null;
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReturnProductWidget(ProductWidget widget)
		{
		}

		// Token: 0x040083AF RID: 33711
		private readonly AssetContainer m_ProductWidgetContainer;

		// Token: 0x040083B0 RID: 33712
		internal const string k_MapLabel_ProductHeader = "Product_Header";

		// Token: 0x040083B1 RID: 33713
		internal const string k_MapLabel_ProductEmpty = "Product_Empty";

		// Token: 0x040083B2 RID: 33714
		internal const string k_MapLabel_ProductDefault = "Product_M";

		// Token: 0x040083B3 RID: 33715
		private const string k_MapLabel_ProductContainer = "Product_Container";

		// Token: 0x040083B4 RID: 33716
		private List<GameObject> m_TemplateList;

		// Token: 0x040083B5 RID: 33717
		private Dictionary<string, int> m_TemplateIdxMap;

		// Token: 0x040083B6 RID: 33718
		private Dictionary<float, int> m_ContainerTemplateIdxMap;

		// Token: 0x040083B7 RID: 33719
		private Dictionary<string, GameObject> m_WidgetPrefMap;

		// Token: 0x040083B8 RID: 33720
		private Dictionary<string, Vector2> m_ProductSizeMap;

		// Token: 0x040083B9 RID: 33721
		private Dictionary<string, Stack<ProductWidget>> m_ProductWidgetReserves;
	}
}
