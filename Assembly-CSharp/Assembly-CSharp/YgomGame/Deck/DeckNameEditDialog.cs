using System;
using YgomGame.Menu;

namespace YgomGame.Deck
{
	// Token: 0x02000FDD RID: 4061
	public class DeckNameEditDialog : SelectDialogViewControllerBase<string>
	{
		// Token: 0x060079BC RID: 31164 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(Action<string> callback)
		{
		}

		// Token: 0x060079BD RID: 31165 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060079BE RID: 31166 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060079BF RID: 31167 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0400B122 RID: 45346
		private const string LABEL_TXT_HEADLINE = "TextHeadline";

		// Token: 0x0400B123 RID: 45347
		private const string LABEL_EIF_NAMEFIELD = "SearchField";

		// Token: 0x0400B124 RID: 45348
		private const string LABEL_TXT_INPUTFIELD = "TextName";

		// Token: 0x0400B125 RID: 45349
		private const string LABEL_SBN_CONFIRMBUTTON = "SearchButton";

		// Token: 0x0400B126 RID: 45350
		private const string PREFAB_PATH = "DeckEdit/DeckNameEditDialog";
	}
}
