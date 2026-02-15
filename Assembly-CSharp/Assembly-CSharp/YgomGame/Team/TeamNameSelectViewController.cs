using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Deck;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;
using YgomSystem.Utility;
using YgomSystem.YGomTMPro;

namespace YgomGame.Team
{
	// Token: 0x020008CC RID: 2252
	public class TeamNameSelectViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported, IBokeSupported
	{
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060041D0 RID: 16848 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060041D1 RID: 16849 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060041D2 RID: 16850 RVA: 0x0000216D File Offset: 0x0000036D
		public bool initialized
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

		// Token: 0x060041D3 RID: 16851 RVA: 0x0000216D File Offset: 0x0000036D
		private void ExecInit()
		{
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x0000216D File Offset: 0x0000036D
		private void Close()
		{
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Start()
		{
			return null;
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator LoadAllCardIDs()
		{
			return null;
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator SortCardsFirst()
		{
			return null;
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x0000216D File Offset: 0x0000036D
		private void Filter(string searchText)
		{
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnCardSelected(int cardId)
		{
			return false;
		}

		// Token: 0x0400802F RID: 32815
		public static readonly string ARG_ONRESULT;

		// Token: 0x04008030 RID: 32816
		private TeamNameSelectViewController.CardListArea _cardArea;

		// Token: 0x04008031 RID: 32817
		private Action<int> _onResult;

		// Token: 0x04008032 RID: 32818
		private const string LABEL_BTNCANCEL = "ButtonCancel";

		// Token: 0x04008033 RID: 32819
		[SerializeField]
		private KeyConfigContainer _keyConfig;

		// Token: 0x04008034 RID: 32820
		private ElementObjectManager _contentView;

		// Token: 0x04008035 RID: 32821
		private ElementObjectManager _cardCollectionView;

		// Token: 0x04008036 RID: 32822
		private ElementObjectManager _cardScrollTop;

		// Token: 0x04008037 RID: 32823
		private InputFieldWidget _cardSearchField;

		// Token: 0x04008038 RID: 32824
		private ExtendedTextMeshProUGUI _teamName;

		// Token: 0x04008039 RID: 32825
		private BindingTextMeshProUGUI _title;

		// Token: 0x0400803A RID: 32826
		private SelectionButton _cancelBtn;

		// Token: 0x0400803B RID: 32827
		private SelectionButton _decideBtn;

		// Token: 0x0400803C RID: 32828
		private List<CardBaseData> _allCardData;

		// Token: 0x0400803D RID: 32829
		private int _selectedCardId;

		// Token: 0x0400803E RID: 32830
		private bool _backSent;

		// Token: 0x020008CD RID: 2253
		private class CardListArea
		{
			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x060041DD RID: 16861 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060041DE RID: 16862 RVA: 0x0000216D File Offset: 0x0000036D
			internal List<CardBaseData> cardData
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x060041DF RID: 16863 RVA: 0x00002739 File Offset: 0x00000939
			internal CardListArea(TeamNameSelectViewController vc, InfinityScrollView v)
			{
			}

			// Token: 0x060041E0 RID: 16864 RVA: 0x0000216D File Offset: 0x0000036D
			internal void Initialize(List<CardBaseData> data)
			{
			}

			// Token: 0x060041E1 RID: 16865 RVA: 0x0000216D File Offset: 0x0000036D
			internal void Refresh()
			{
			}

			// Token: 0x060041E2 RID: 16866 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnEntityUpdate(GameObject obj, int index)
			{
			}

			// Token: 0x060041E3 RID: 16867 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnEntityDeactivate(GameObject obj)
			{
			}

			// Token: 0x060041E4 RID: 16868 RVA: 0x0000216D File Offset: 0x0000036D
			private void SetupShortcuts()
			{
			}

			// Token: 0x0400803F RID: 32831
			private const string LABEL_IMGCARD = "ImageCard";

			// Token: 0x04008040 RID: 32832
			private const string LABEL_SELECT_TOGGLE = "SelectedStateToggle";

			// Token: 0x04008041 RID: 32833
			private readonly TeamNameSelectViewController _vc;

			// Token: 0x04008042 RID: 32834
			private readonly InfinityScrollView _infinityScroll;

			// Token: 0x04008043 RID: 32835
			private readonly Selector _selector;

			// Token: 0x04008044 RID: 32836
			private readonly GridLayoutGroup _gridlayout;

			// Token: 0x04008045 RID: 32837
			private GameObject _selectedCardObj;
		}
	}
}
