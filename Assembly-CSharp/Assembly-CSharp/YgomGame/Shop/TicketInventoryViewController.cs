using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Shop
{
	// Token: 0x0200097A RID: 2426
	public class TicketInventoryViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06004716 RID: 18198 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(int currentShopId, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x0000216A File Offset: 0x0000036A
		private ProductContext TryGetProductContext(int shopId)
		{
			return null;
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x0000216A File Offset: 0x0000036A
		private TicketInventoryViewController.Context TryCreateTicketContexts(int currentShopId, bool isPeriod, int itemCategory, int itemId, int usableShopId)
		{
			return null;
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntity(GameObject entity, int idx)
		{
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickEntity(int idx)
		{
		}

		// Token: 0x0400853B RID: 34107
		private const string k_ArgsCurrentShopId = "priceContexts";

		// Token: 0x0400853C RID: 34108
		private const string k_ELabelScrollView = "ScrollView";

		// Token: 0x0400853D RID: 34109
		private const string k_ELabelTextEmpty = "TextEmpty";

		// Token: 0x0400853E RID: 34110
		private const string k_ELabelEntity_On = "On";

		// Token: 0x0400853F RID: 34111
		private const string k_ELabelEntity_Off = "Off";

		// Token: 0x04008540 RID: 34112
		private const string k_ELabelEntity_TicketThumbHolder = "TicketThumbHolder";

		// Token: 0x04008541 RID: 34113
		private const string k_ELabelEntity_ProductThumbHolder = "ProductThumbHolder";

		// Token: 0x04008542 RID: 34114
		private const string k_ELabelEntity_TextName = "TextName";

		// Token: 0x04008543 RID: 34115
		private InfinityScrollView m_ScrollView;

		// Token: 0x04008544 RID: 34116
		private List<TicketInventoryViewController.Context> m_Contexts;

		// Token: 0x04008545 RID: 34117
		private Dictionary<int, ProductContext> m_ProductContextCacheMap;

		// Token: 0x04008546 RID: 34118
		private int[] m_UsableShopIds;

		// Token: 0x0200097B RID: 2427
		private class Context : IComparable<TicketInventoryViewController.Context>
		{
			// Token: 0x17000633 RID: 1587
			// (get) Token: 0x06004720 RID: 18208 RVA: 0x0000216A File Offset: 0x0000036A
			public ProductContext productContext
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000634 RID: 1588
			// (get) Token: 0x06004721 RID: 18209 RVA: 0x000F1669 File Offset: 0x000EF869
			public long limitdateTs
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x17000635 RID: 1589
			// (get) Token: 0x06004722 RID: 18210 RVA: 0x0000216A File Offset: 0x0000036A
			public string limitdate
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000636 RID: 1590
			// (get) Token: 0x06004723 RID: 18211 RVA: 0x000029CC File Offset: 0x00000BCC
			public int itemHave
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06004724 RID: 18212 RVA: 0x00002739 File Offset: 0x00000939
			public Context(int shopId, bool currentShopId, bool isPeriod, int itemCategory, int itemId, int priceIdx, ProductContext productContext)
			{
			}

			// Token: 0x06004725 RID: 18213 RVA: 0x000029CC File Offset: 0x00000BCC
			public override bool Equals(object obj)
			{
				return false;
			}

			// Token: 0x06004726 RID: 18214 RVA: 0x000029CC File Offset: 0x00000BCC
			public override int GetHashCode()
			{
				return 0;
			}

			// Token: 0x06004727 RID: 18215 RVA: 0x000029CC File Offset: 0x00000BCC
			public int Compare(TicketInventoryViewController.Context a, TicketInventoryViewController.Context b)
			{
				return 0;
			}

			// Token: 0x06004728 RID: 18216 RVA: 0x000029CC File Offset: 0x00000BCC
			public int CompareTo(TicketInventoryViewController.Context other)
			{
				return 0;
			}

			// Token: 0x04008547 RID: 34119
			public readonly int shopId;

			// Token: 0x04008548 RID: 34120
			public readonly bool currentShopTarget;

			// Token: 0x04008549 RID: 34121
			public readonly bool isPeriod;

			// Token: 0x0400854A RID: 34122
			public readonly int itemCategory;

			// Token: 0x0400854B RID: 34123
			public readonly int itemId;

			// Token: 0x0400854C RID: 34124
			public readonly int priceIdx;

			// Token: 0x0400854D RID: 34125
			public readonly ProductContext m_ProductContext;
		}
	}
}
