using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YgomSystem.ElementSystem;

namespace YgomGame.Enquete
{
	// Token: 0x02000C1A RID: 3098
	public class GroupWidget : SheetContentWidget, ISheetContentCompleteCheckWidget
	{
		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06005870 RID: 22640 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005871 RID: 22641 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06005872 RID: 22642 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06005873 RID: 22643 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x06005874 RID: 22644 RVA: 0x000F4D13 File Offset: 0x000F2F13
		public GroupWidget(ElementObjectManager eom, string label, SheetWidgetFactory sheetWidgetFactory)
			: base(null, null)
		{
		}

		// Token: 0x06005875 RID: 22645 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetContents(List<ISheetContentContext> contents)
		{
		}

		// Token: 0x06005876 RID: 22646 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ImportInputValues(Dictionary<string, object> importValues)
		{
		}

		// Token: 0x06005877 RID: 22647 RVA: 0x0000216D File Offset: 0x0000036D
		public override void CollectSelectionItems(SheetSelectionItemMap sheetSelectionItemMap)
		{
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x0000216D File Offset: 0x0000036D
		public override void CollectInputValues(Dictionary<string, object> resultValues)
		{
		}

		// Token: 0x06005879 RID: 22649 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckInputComplete()
		{
		}

		// Token: 0x040094C7 RID: 38087
		private readonly SheetWidgetFactory m_SheetWidgetFactory;

		// Token: 0x040094C8 RID: 38088
		public readonly List<ISheetContentWidget> m_ContentWidgets;

		// Token: 0x040094C9 RID: 38089
		public readonly List<ISheetContentCompleteCheckWidget> m_ContentCompleteCheckWidgets;

		// Token: 0x040094CA RID: 38090
		public bool isMust;
	}
}
