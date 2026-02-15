using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.PropertyOverrider;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BC8 RID: 3016
	public class MDMarkupTableCellItem : ElementWidgetBase, IMDMarkupItemWidget, IMDMarkupButtonWidget
	{
		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06005610 RID: 22032 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005611 RID: 22033 RVA: 0x0000216D File Offset: 0x0000036D
		public bool borderVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06005612 RID: 22034 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005613 RID: 22035 RVA: 0x0000216D File Offset: 0x0000036D
		public bool itemIsPeriod
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06005614 RID: 22036 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005615 RID: 22037 RVA: 0x0000216D File Offset: 0x0000036D
		public int itemCategory
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

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06005616 RID: 22038 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005617 RID: 22039 RVA: 0x0000216D File Offset: 0x0000036D
		public int itemId
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

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06005618 RID: 22040 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005619 RID: 22041 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionButton button
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

		// Token: 0x0600561A RID: 22042 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MDMarkupTableCellItem(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x0600561B RID: 22043 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetItem(bool isPeriod, int itemCategory, int itemId, MDMarkupDef.ItemSize itemSize, float overrideHeight = 0f)
		{
		}

		// Token: 0x0600561C RID: 22044 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAlignment(TextAlignmentOptions alignment)
		{
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSizeRate(float sizeRate)
		{
		}

		// Token: 0x0600561E RID: 22046 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClick(UnityAction callback)
		{
		}

		// Token: 0x04009306 RID: 37638
		private readonly string k_ELabelButton;

		// Token: 0x04009307 RID: 37639
		private readonly string k_ELabelRoot;

		// Token: 0x04009308 RID: 37640
		private readonly string k_ELabelThumbHolder;

		// Token: 0x04009309 RID: 37641
		private readonly string k_OVGroupLabel_Default;

		// Token: 0x0400930A RID: 37642
		private readonly string k_OVGroupLabel_Structure;

		// Token: 0x0400930B RID: 37643
		public readonly PlatformOverriderGroup m_OVGroup;

		// Token: 0x0400930C RID: 37644
		public readonly LayoutElement m_LayoutElement;

		// Token: 0x0400930D RID: 37645
		public readonly RectTransform m_Root;

		// Token: 0x0400930E RID: 37646
		public readonly AspectRatioFitter m_AspectRatioFitter;

		// Token: 0x0400930F RID: 37647
		public readonly GameObject m_ThumbHolder;
	}
}
