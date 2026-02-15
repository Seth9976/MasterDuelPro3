using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Help
{
	// Token: 0x02000BF5 RID: 3061
	public class HelpViewController : BaseMenuViewController, IBackButtonWithoutSCSupported, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060056E0 RID: 22240 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060056E1 RID: 22241 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open()
		{
		}

		// Token: 0x060056E2 RID: 22242 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenHelp(string labelPath, Action callback = null, bool isOverlay = true, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060056E3 RID: 22243 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenHelp(string groupLabel, string sectionLabel, string recordLabel, Action callback = null, bool isOverlay = true, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x060056E4 RID: 22244 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060056E5 RID: 22245 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060056E6 RID: 22246 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x060056E7 RID: 22247 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060056E8 RID: 22248 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x060056E9 RID: 22249 RVA: 0x0000216D File Offset: 0x0000036D
		private void SelectSection(int sectionIdx)
		{
		}

		// Token: 0x060056EA RID: 22250 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetTextData(string sourceTextId)
		{
			return null;
		}

		// Token: 0x060056EB RID: 22251 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedSectionEntity(GameObject entity)
		{
		}

		// Token: 0x060056EC RID: 22252 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsSelectableDataIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateSectionEntity(GameObject entity, int idx)
		{
		}

		// Token: 0x060056EE RID: 22254 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickSectionEntity(HelpViewController.SectionWidget sectionWidget)
		{
		}

		// Token: 0x060056EF RID: 22255 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelectedSectionEntity(HelpViewController.SectionWidget sectionWidget)
		{
		}

		// Token: 0x060056F0 RID: 22256 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedRecordEntity(GameObject entity)
		{
		}

		// Token: 0x060056F1 RID: 22257 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateRecordEntity(GameObject entity, int idx)
		{
		}

		// Token: 0x060056F2 RID: 22258 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickRecordEntity(HelpViewController.SectionWidget recordWidget)
		{
		}

		// Token: 0x060056F3 RID: 22259 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnLeftRecordEntity(HelpViewController.SectionWidget sectionWidget)
		{
		}

		// Token: 0x040093A0 RID: 37792
		private readonly string k_ELabelSectionList;

		// Token: 0x040093A1 RID: 37793
		private readonly string k_ELabelRecordList;

		// Token: 0x040093A2 RID: 37794
		private readonly string k_ElabelBackShortCutbutton;

		// Token: 0x040093A3 RID: 37795
		private bool m_nowRecordSelected;

		// Token: 0x040093A4 RID: 37796
		private readonly int k_SectionGroupTNo;

		// Token: 0x040093A5 RID: 37797
		private readonly int k_SectionButtonTNo;

		// Token: 0x040093A6 RID: 37798
		private readonly int k_SectionSpacerTNo;

		// Token: 0x040093A7 RID: 37799
		private HelpMappingData m_HelpMapping;

		// Token: 0x040093A8 RID: 37800
		private InfinityScrollView m_SectionScrollView;

		// Token: 0x040093A9 RID: 37801
		private Selector m_SectionSelector;

		// Token: 0x040093AA RID: 37802
		private List<HelpViewController.SectionContext> m_SectionContexts;

		// Token: 0x040093AB RID: 37803
		private List<int> m_SectionTemplates;

		// Token: 0x040093AC RID: 37804
		private Dictionary<GameObject, HelpViewController.SectionWidget> m_SectionEntityMap;

		// Token: 0x040093AD RID: 37805
		private InfinityScrollView m_RecordScrollView;

		// Token: 0x040093AE RID: 37806
		private List<HelpViewController.RecordContext> m_RecordContexts;

		// Token: 0x040093AF RID: 37807
		private Dictionary<GameObject, HelpViewController.RecordWidget> m_RecordEntityMap;

		// Token: 0x040093B0 RID: 37808
		private List<string> m_LoadedTextGroups;

		// Token: 0x040093B1 RID: 37809
		private int m_CurrentSectionIdx;

		// Token: 0x02000BF6 RID: 3062
		private class SectionContext
		{
			// Token: 0x040093B2 RID: 37810
			public string text;

			// Token: 0x040093B3 RID: 37811
			public string groupLabel;

			// Token: 0x040093B4 RID: 37812
			public string sectionLabel;
		}

		// Token: 0x02000BF7 RID: 3063
		private class SectionWidget : ElementWidgetBase
		{
			// Token: 0x14000083 RID: 131
			// (add) Token: 0x060056F6 RID: 22262 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x060056F7 RID: 22263 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action<HelpViewController.SectionWidget> onClickEvent
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

			// Token: 0x14000084 RID: 132
			// (add) Token: 0x060056F8 RID: 22264 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x060056F9 RID: 22265 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action<HelpViewController.SectionWidget> onSelectedEvent
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

			// Token: 0x14000085 RID: 133
			// (add) Token: 0x060056FA RID: 22266 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x060056FB RID: 22267 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action<HelpViewController.SectionWidget> onRightEvent
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

			// Token: 0x14000086 RID: 134
			// (add) Token: 0x060056FC RID: 22268 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x060056FD RID: 22269 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action<HelpViewController.SectionWidget> onLeftEvent
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

			// Token: 0x060056FE RID: 22270 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public SectionWidget(ElementObjectManager eom)
				: base(null)
			{
			}

			// Token: 0x060056FF RID: 22271 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetIsOn(bool isOn)
			{
			}

			// Token: 0x06005700 RID: 22272 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnClick()
			{
			}

			// Token: 0x06005701 RID: 22273 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnSelected()
			{
			}

			// Token: 0x06005702 RID: 22274 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnRight()
			{
			}

			// Token: 0x06005703 RID: 22275 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnLeft()
			{
			}

			// Token: 0x040093B5 RID: 37813
			private readonly string k_ELabelText;

			// Token: 0x040093B6 RID: 37814
			private readonly string k_ELabelButton;

			// Token: 0x040093B7 RID: 37815
			private readonly string k_ELabelOn;

			// Token: 0x040093B8 RID: 37816
			private readonly string k_ELabelOff;

			// Token: 0x040093B9 RID: 37817
			public readonly TextMeshProUGUI text;

			// Token: 0x040093BA RID: 37818
			public readonly SelectionButton button;

			// Token: 0x040093BB RID: 37819
			private readonly string k_ELabelOffText;

			// Token: 0x040093BC RID: 37820
			public readonly TextMeshProUGUI textOff;
		}

		// Token: 0x02000BF8 RID: 3064
		private class RecordContext
		{
			// Token: 0x040093BD RID: 37821
			public string text;

			// Token: 0x040093BE RID: 37822
			public string path;
		}

		// Token: 0x02000BF9 RID: 3065
		private class RecordWidget : HelpViewController.SectionWidget
		{
			// Token: 0x06005705 RID: 22277 RVA: 0x000F4CEA File Offset: 0x000F2EEA
			public RecordWidget(ElementObjectManager eom)
				: base(null)
			{
			}
		}
	}
}
