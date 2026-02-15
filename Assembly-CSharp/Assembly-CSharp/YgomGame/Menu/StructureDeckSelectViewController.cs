using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000AF4 RID: 2804
	public class StructureDeckSelectViewController : BaseMenuViewController
	{
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06005185 RID: 20869 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06005186 RID: 20870 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isMobile
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x0000216D File Offset: 0x0000036D
		public static void GotoFirstStructDeckSelection(bool restore = false)
		{
		}

		// Token: 0x06005188 RID: 20872 RVA: 0x0000216A File Offset: 0x0000036A
		private StructureDeckSelectViewController.DeckCaseImageData GetDeckCaseSpriteData(int structureId)
		{
			return null;
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickDeck(int clickedIdx)
		{
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnSelectedDeck(int structureDeckId)
		{
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x0000216A File Offset: 0x0000036A
		protected IEnumerator OpenStructureDeckBrowser(int structureDeckId)
		{
			return null;
		}

		// Token: 0x04008FDB RID: 36827
		protected const string k_ArgKeyStructureDeckIds = "sdid";

		// Token: 0x04008FDC RID: 36828
		private const string k_ArgKeyBeforeNextViewEvent = "BeforeNextViewEvent";

		// Token: 0x04008FDD RID: 36829
		protected readonly string k_ELabelTitleText;

		// Token: 0x04008FDE RID: 36830
		private readonly string k_ELabelDeckTemplate;

		// Token: 0x04008FDF RID: 36831
		private const string TWEEN_SELECT_LABEL = "select";

		// Token: 0x04008FE0 RID: 36832
		private const string TWEEN_DESELECT_LABEL = "deselect";

		// Token: 0x04008FE1 RID: 36833
		private const string ANDROID_BACK_KEY_LABEL = "AndroidBackKey";

		// Token: 0x04008FE2 RID: 36834
		[SerializeField]
		private ElementObjectManager m_PrefabUI;

		// Token: 0x04008FE3 RID: 36835
		protected ElementObjectManager m_UI;

		// Token: 0x04008FE4 RID: 36836
		private ElementObjectManager m_DeckTemplate;

		// Token: 0x04008FE5 RID: 36837
		private int[] m_StructureDeckIds;

		// Token: 0x04008FE6 RID: 36838
		private SelectionItem[] m_StructureDeckItems;

		// Token: 0x04008FE7 RID: 36839
		private int m_DefaultSelectIdx;

		// Token: 0x04008FE8 RID: 36840
		private Action _callback;

		// Token: 0x04008FE9 RID: 36841
		[SerializeField]
		private List<StructureDeckSelectViewController.DeckCaseImageData> _deckCaseSpriteDataList;

		// Token: 0x04008FEA RID: 36842
		private Dictionary<int, int> _cacheStructureIdToIconNimberMap;

		// Token: 0x04008FEB RID: 36843
		private bool _isDeckSelectDisabled;

		// Token: 0x02000AF5 RID: 2805
		[Serializable]
		public class DeckCaseImageData
		{
			// Token: 0x04008FEC RID: 36844
			public int _caseIconNumber;

			// Token: 0x04008FED RID: 36845
			public Sprite _deckSprite;

			// Token: 0x04008FEE RID: 36846
			public Sprite _openedDeckSprite;

			// Token: 0x04008FEF RID: 36847
			public Sprite[] _monsterSprites;
		}
	}
}
