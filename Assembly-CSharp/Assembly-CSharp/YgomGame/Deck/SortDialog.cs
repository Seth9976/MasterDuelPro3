using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.Deck
{
	// Token: 0x02001007 RID: 4103
	public class SortDialog : SelectDialogViewControllerBase<SortComparer.Sorter>, IBokeSupported
	{
		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06007B7F RID: 31615 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform m_SortButtonArea
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06007B80 RID: 31616 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_CancelButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007B81 RID: 31617 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(SortComparer.METHOD method, SortComparer.ORDER order, Action<SortComparer.Sorter> callback = null)
		{
		}

		// Token: 0x06007B82 RID: 31618 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007B83 RID: 31619 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007B84 RID: 31620 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetSorterName(SortComparer.Sorter s)
		{
			return null;
		}

		// Token: 0x0400B31D RID: 45853
		private const string PREFAB_PATH_SORTDIALOG = "DeckEdit/SortDialog";

		// Token: 0x0400B31E RID: 45854
		private const string LABEL_SBN_CANCELBUTTON = "ButtonFooter";

		// Token: 0x0400B31F RID: 45855
		private const string LABEL_RT_SORTBUTTONAREA = "SortButtonsArea";

		// Token: 0x0400B320 RID: 45856
		private const string Label_Obtained = "入";

		// Token: 0x0400B321 RID: 45857
		private const string Label_Inventroy = "所\ufffd";

		// Token: 0x0400B322 RID: 45858
		private const string Label_Rarity = "レ\ufffd";

		// Token: 0x0400B323 RID: 45859
		private const string Label_Stars = "レベル\ufffd";

		// Token: 0x0400B324 RID: 45860
		private const string Label_Atk = "攻\ufffd";

		// Token: 0x0400B325 RID: 45861
		private const string Label_Def = "守\ufffd";

		// Token: 0x0400B326 RID: 45862
		private SorterToggle template;

		// Token: 0x0400B327 RID: 45863
		private static Dictionary<string, SortComparer.METHOD> methodTbl;

		// Token: 0x0400B328 RID: 45864
		private static Dictionary<string, string> methodLabelTbl;
	}
}
