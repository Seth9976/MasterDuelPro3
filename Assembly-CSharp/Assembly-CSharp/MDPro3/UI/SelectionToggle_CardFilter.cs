using System;
using MDPro3.UI.ServantUI;

namespace MDPro3.UI
{
	// Token: 0x020013BA RID: 5050
	public class SelectionToggle_CardFilter : SelectionToggle
	{
		// Token: 0x06009261 RID: 37473 RVA: 0x00147B1F File Offset: 0x00145D1F
		protected override void Awake()
		{
			base.Awake();
			SelectionToggle_CardFilter.Instance = this;
			this.SetClickEvent(delegate
			{
				Program.instance.deckEditor.GetUI<DeckEditorUI>().CardCollectionView.ShowFilters();
			});
		}

		// Token: 0x06009262 RID: 37474 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnClick()
		{
		}

		// Token: 0x0400D0DC RID: 53468
		public static SelectionToggle_CardFilter Instance;
	}
}
