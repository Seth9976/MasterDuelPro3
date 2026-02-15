using System;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.Deck
{
	// Token: 0x02000FF1 RID: 4081
	public class SearchBoxDialog : SelectDialogViewControllerBase<string, string>
	{
		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06007B28 RID: 31528 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_SearchButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06007B29 RID: 31529 RVA: 0x0000216A File Offset: 0x0000036A
		private ExtendedInputField m_InputField
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007B2A RID: 31530 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string keyword, Action<string> callback)
		{
		}

		// Token: 0x06007B2B RID: 31531 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007B2C RID: 31532 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06007B2D RID: 31533 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0400B25B RID: 45659
		private const string LABEL_SBN_SEARCHBUTTON = "SearchButton";

		// Token: 0x0400B25C RID: 45660
		private const string LABEL_TXT_INPUTFIELD = "TextName";

		// Token: 0x0400B25D RID: 45661
		private const string LABEL_TXT_PLACEHOLDER = "PlaceHolder";

		// Token: 0x0400B25E RID: 45662
		private const string LABEL_EIF_SEARCHFIELD = "SearchField";

		// Token: 0x0400B25F RID: 45663
		private const string PREFAB_PATH = "DeckEdit/SearchBoxDialog";
	}
}
