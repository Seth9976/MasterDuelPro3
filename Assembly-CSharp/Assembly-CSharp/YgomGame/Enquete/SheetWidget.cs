using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Enquete
{
	// Token: 0x02000C33 RID: 3123
	public class SheetWidget
	{
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060058FA RID: 22778 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060058FB RID: 22779 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isMust
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

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060058FC RID: 22780 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060058FD RID: 22781 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isInputComplete
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

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060058FE RID: 22782 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<ISheetContentWidget> contentWidgets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x060058FF RID: 22783 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005900 RID: 22784 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onChangeComplete
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x00002739 File Offset: 0x00000939
		public SheetWidget(RectTransform rectTransform, SheetWidgetFactory sheetWidgetFactory)
		{
		}

		// Token: 0x06005902 RID: 22786 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetContext(SheetContext sheetContext)
		{
		}

		// Token: 0x06005903 RID: 22787 RVA: 0x0000216D File Offset: 0x0000036D
		public void CollectInputValues(Dictionary<string, object> resultValues)
		{
		}

		// Token: 0x06005904 RID: 22788 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckInputComplete()
		{
		}

		// Token: 0x040094F5 RID: 38133
		public readonly GameObject gameObject;

		// Token: 0x040094F6 RID: 38134
		public readonly RectTransform rectTransform;

		// Token: 0x040094F7 RID: 38135
		private readonly SheetWidgetFactory m_SheetWidgetFactory;

		// Token: 0x040094F8 RID: 38136
		public readonly List<ISheetContentWidget> m_ContentWidgets;

		// Token: 0x040094F9 RID: 38137
		public readonly List<ISheetContentCompleteCheckWidget> m_ContentCompleteCheckWidgets;

		// Token: 0x040094FA RID: 38138
		public readonly SheetSelectionItemMap selectionItemMap;
	}
}
