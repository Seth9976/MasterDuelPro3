using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Deck;
using YgomGame.Menu;
using YgomGame.Menu.Common;
using YgomGame.Utility;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Regulation
{
	// Token: 0x02000A0A RID: 2570
	public class CardListBrowserRegulationFilterViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06004A93 RID: 19091 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitDefaultSetting()
		{
		}

		// Token: 0x06004A95 RID: 19093 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string regulationName, int regulationId, ViewControllerManager manager = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenHome(string regulationName, int regulationId, ViewControllerManager manager = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateListDisplay()
		{
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x0000216D File Offset: 0x0000036D
		private void RemoveExtraCard()
		{
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x0000216A File Offset: 0x0000036A
		private List<int>[] CreateStandardLists()
		{
			return null;
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x0000216D File Offset: 0x0000036D
		private void SubtractStandard()
		{
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateRegMrksList()
		{
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x0000216A File Offset: 0x0000036A
		private List<int> SelectInCardRare(List<int> rawList)
		{
			return null;
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnEntityCreated(GameObject gob)
		{
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnEntityUpdate(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickEntity(CardListBrowserRegulationFilterViewController.CardWidget clickedWidget)
		{
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x0000216D File Offset: 0x0000036D
		private void FilteredCallBack()
		{
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToggleFilterButton(bool isFiltered)
		{
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeFilterButton()
		{
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
		{
		}

		// Token: 0x040088B7 RID: 34999
		public const string k_ArgRegulationId = "regulationId";

		// Token: 0x040088B8 RID: 35000
		public const string k_ArgRegulationName = "regulationName";

		// Token: 0x040088B9 RID: 35001
		private readonly string k_ELabelRegulationNameText;

		// Token: 0x040088BA RID: 35002
		private readonly string k_ELabelCardList;

		// Token: 0x040088BB RID: 35003
		private readonly string k_ELabelEmptyText;

		// Token: 0x040088BC RID: 35004
		private readonly string k_ELabelFilterForm;

		// Token: 0x040088BD RID: 35005
		private readonly string k_ELabelShortcutButtonRarityBack;

		// Token: 0x040088BE RID: 35006
		private readonly string k_ELabelShortcutButtonRarityNext;

		// Token: 0x040088BF RID: 35007
		private readonly string k_ELabelFilterButton;

		// Token: 0x040088C0 RID: 35008
		private readonly string k_ELabelDisplayInFilterText;

		// Token: 0x040088C1 RID: 35009
		private const string k_ELabelFilterOnIcon = "IconOn";

		// Token: 0x040088C2 RID: 35010
		private const string k_ELabelFilterOffIcon = "IconOff";

		// Token: 0x040088C3 RID: 35011
		private const string k_ELabelFilterOnImage = "On";

		// Token: 0x040088C4 RID: 35012
		private const string k_ELabelFilterOffImage = "Off";

		// Token: 0x040088C5 RID: 35013
		private const string k_ELabelRegulationLogo = "RegulationLogo";

		// Token: 0x040088C6 RID: 35014
		private const string k_ELabelAnalogDirectionItem = "AnalogDirectionItem";

		// Token: 0x040088C7 RID: 35015
		private List<int> m_DisplayCardMrks;

		// Token: 0x040088C8 RID: 35016
		private bool m_RegulationVisible;

		// Token: 0x040088C9 RID: 35017
		private int m_RegulationId;

		// Token: 0x040088CA RID: 35018
		private InfinityScrollView m_ScrollView;

		// Token: 0x040088CB RID: 35019
		private TMP_Text m_EmptyText;

		// Token: 0x040088CC RID: 35020
		private Image m_RegulationLogoImage;

		// Token: 0x040088CD RID: 35021
		private TMP_Text m_DisplayInFilterText;

		// Token: 0x040088CE RID: 35022
		private bool isFiltered;

		// Token: 0x040088CF RID: 35023
		private CardListBrowserRegulationFilterViewController.FilterFormWidget m_FilterFormWidget;

		// Token: 0x040088D0 RID: 35024
		private RectTransform m_FilterButtonImageOn;

		// Token: 0x040088D1 RID: 35025
		private RectTransform m_FilterButtonImageOff;

		// Token: 0x040088D2 RID: 35026
		private RectTransform m_FilterButtonIconOn;

		// Token: 0x040088D3 RID: 35027
		private RectTransform m_FilterButtonIconOff;

		// Token: 0x040088D4 RID: 35028
		private Dictionary<GameObject, CardListBrowserRegulationFilterViewController.CardWidget> m_CardWidgetDic;

		// Token: 0x040088D5 RID: 35029
		private List<int>[] m_regMrksLists;

		// Token: 0x040088D6 RID: 35030
		private FilterDialogManager m_FilterDialogManager;

		// Token: 0x040088D7 RID: 35031
		private SelectionButton m_FilterButton;

		// Token: 0x040088D8 RID: 35032
		private List<FilterDialog.FilterGroupType> m_FilterGroupTypes;

		// Token: 0x040088D9 RID: 35033
		private SearchFilter.Setting m_DefaultSetting;

		// Token: 0x02000A0B RID: 2571
		private class CardWidget : ElementWidgetBase
		{
			// Token: 0x170006CD RID: 1741
			// (get) Token: 0x06004AA8 RID: 19112 RVA: 0x000029CC File Offset: 0x00000BCC
			public int idx
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170006CE RID: 1742
			// (get) Token: 0x06004AA9 RID: 19113 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004AAA RID: 19114 RVA: 0x0000216D File Offset: 0x0000036D
			public bool highlightVisible
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x170006CF RID: 1743
			// (get) Token: 0x06004AAB RID: 19115 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004AAC RID: 19116 RVA: 0x0000216D File Offset: 0x0000036D
			public bool newIconVisible
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x170006D0 RID: 1744
			// (get) Token: 0x06004AAD RID: 19117 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004AAE RID: 19118 RVA: 0x0000216D File Offset: 0x0000036D
			public bool limitIconVisible
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x170006D1 RID: 1745
			// (get) Token: 0x06004AAF RID: 19119 RVA: 0x0000216A File Offset: 0x0000036A
			public Image limitIconImage
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170006D2 RID: 1746
			// (get) Token: 0x06004AB0 RID: 19120 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004AB1 RID: 19121 RVA: 0x0000216D File Offset: 0x0000036D
			public bool innerTextVisible
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			// Token: 0x170006D3 RID: 1747
			// (get) Token: 0x06004AB2 RID: 19122 RVA: 0x0000216A File Offset: 0x0000036A
			public TMP_Text innerText
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170006D4 RID: 1748
			// (get) Token: 0x06004AB3 RID: 19123 RVA: 0x0000216A File Offset: 0x0000036A
			public SelectionButton button
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170006D5 RID: 1749
			// (get) Token: 0x06004AB4 RID: 19124 RVA: 0x0000216A File Offset: 0x0000036A
			public BindingCardMaterial bindingCardMaterial
			{
				get
				{
					return null;
				}
			}

			// Token: 0x14000061 RID: 97
			// (add) Token: 0x06004AB5 RID: 19125 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x06004AB6 RID: 19126 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action<CardListBrowserRegulationFilterViewController.CardWidget> onClickEvent
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

			// Token: 0x06004AB7 RID: 19127 RVA: 0x0000216A File Offset: 0x0000036A
			public static CardListBrowserRegulationFilterViewController.CardWidget Create(ElementObjectManager eom)
			{
				return null;
			}

			// Token: 0x06004AB8 RID: 19128 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public CardWidget(ElementObjectManager eom)
				: base(null)
			{
			}

			// Token: 0x06004AB9 RID: 19129 RVA: 0x0000216D File Offset: 0x0000036D
			public virtual void Binding(int idx, int mrk, int styleId = 1)
			{
			}

			// Token: 0x06004ABA RID: 19130 RVA: 0x0000216D File Offset: 0x0000036D
			protected virtual void OnClick()
			{
			}

			// Token: 0x040088DA RID: 35034
			private readonly string k_ECardLabelButton;

			// Token: 0x040088DB RID: 35035
			private readonly string k_ECardLabelHighlight;

			// Token: 0x040088DC RID: 35036
			private readonly string k_ECardLabelIconRarity;

			// Token: 0x040088DD RID: 35037
			private readonly string k_ECardLabelLimitIcon;

			// Token: 0x040088DE RID: 35038
			private readonly string k_ECardLabelNumTextArea;

			// Token: 0x040088DF RID: 35039
			private readonly string k_ECardLabelNumText;

			// Token: 0x040088E0 RID: 35040
			private readonly string k_ECardLabelNewIcon;

			// Token: 0x040088E1 RID: 35041
			private int m_Idx;

			// Token: 0x040088E2 RID: 35042
			private int m_Mrk;

			// Token: 0x040088E3 RID: 35043
			private int m_StyleId;

			// Token: 0x040088E4 RID: 35044
			private readonly RawImage m_CardRawImage;

			// Token: 0x040088E5 RID: 35045
			private BindingCardMaterial m_BindingCardMaterial;

			// Token: 0x040088E6 RID: 35046
			public int regurationId;
		}

		// Token: 0x02000A0C RID: 2572
		private class FilterFormWidget : ElementWidgetBase
		{
			// Token: 0x06004ABB RID: 19131 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public FilterFormWidget(ElementObjectManager eom)
				: base(null)
			{
			}

			// Token: 0x040088E7 RID: 35047
			private readonly string k_ELabelFilterLimitTabs;

			// Token: 0x040088E8 RID: 35048
			public readonly DirectionalToggleGroupWidget limitToggleWidget;
		}
	}
}
