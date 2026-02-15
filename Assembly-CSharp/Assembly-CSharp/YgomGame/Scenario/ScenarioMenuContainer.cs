using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Scenario
{
	// Token: 0x020009D8 RID: 2520
	public class ScenarioMenuContainer : ElementWidgetBehaviourBase<ScenarioMenuContainer>
	{
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600493E RID: 18750 RVA: 0x0000216A File Offset: 0x0000036A
		public Selector selector
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600493F RID: 18751 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06004940 RID: 18752 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionButton autoButton
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06004941 RID: 18753 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06004942 RID: 18754 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionButton logButton
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06004943 RID: 18755 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06004944 RID: 18756 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionButton skipButton
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06004945 RID: 18757 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06004946 RID: 18758 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionButton viewButton
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06004947 RID: 18759 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004948 RID: 18760 RVA: 0x0000216D File Offset: 0x0000036D
		public bool visible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06004949 RID: 18761 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600494A RID: 18762 RVA: 0x0000216D File Offset: 0x0000036D
		public bool expandIconVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600494B RID: 18763 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600494C RID: 18764 RVA: 0x0000216D File Offset: 0x0000036D
		public bool shrinkIconVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x0600494D RID: 18765 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600494E RID: 18766 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onClickAnyButtonEvent
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

		// Token: 0x0600494F RID: 18767 RVA: 0x0000216A File Offset: 0x0000036A
		public static ScenarioMenuContainer Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ScenarioWork work)
		{
		}

		// Token: 0x06004952 RID: 18770 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToExpand(bool immediate = false)
		{
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToShrink(bool immediate = false)
		{
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayHide(bool immediate = false)
		{
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayShow(bool immediate = false)
		{
		}

		// Token: 0x06004958 RID: 18776 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayOnAutoStart(bool immediate = false)
		{
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayOnAutoEnd(bool immediate = false)
		{
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TryShrink(bool immediate = false)
		{
			return false;
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickCancelButton()
		{
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickRootButton()
		{
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangedAutoActive(bool isAuto)
		{
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnBack()
		{
			return false;
		}

		// Token: 0x04008719 RID: 34585
		private readonly string k_ELabelRoot;

		// Token: 0x0400871A RID: 34586
		private readonly string k_ELabelShrinkShortCutButton;

		// Token: 0x0400871B RID: 34587
		private readonly string k_ELabelRootButton;

		// Token: 0x0400871C RID: 34588
		private readonly string k_ELabelChildButtonGroup;

		// Token: 0x0400871D RID: 34589
		private readonly string k_ELabelAutoButton;

		// Token: 0x0400871E RID: 34590
		private readonly string k_ELabelLogButton;

		// Token: 0x0400871F RID: 34591
		private readonly string k_ELabelSkipButton;

		// Token: 0x04008720 RID: 34592
		private readonly string k_ELabelViewButton;

		// Token: 0x04008721 RID: 34593
		private readonly string k_ELabelExpandedIcon;

		// Token: 0x04008722 RID: 34594
		private readonly string k_ELabelShlinkedIcon;

		// Token: 0x04008723 RID: 34595
		private readonly string k_TweenShow;

		// Token: 0x04008724 RID: 34596
		private readonly string k_TweenHide;

		// Token: 0x04008725 RID: 34597
		private readonly string k_TweenExpand;

		// Token: 0x04008726 RID: 34598
		private readonly string k_TweenShrink;

		// Token: 0x04008727 RID: 34599
		private readonly string k_TweenOnStartAuto;

		// Token: 0x04008728 RID: 34600
		private readonly string k_TweenOnEndAuto;

		// Token: 0x04008729 RID: 34601
		private GameObject m_Root;

		// Token: 0x0400872A RID: 34602
		private GameObject m_ChildGroup;

		// Token: 0x0400872B RID: 34603
		private Selector m_Selector;

		// Token: 0x0400872C RID: 34604
		private LayoutGroup m_ChildGroupLayoutGroup;

		// Token: 0x0400872D RID: 34605
		private SelectionButton m_RootButton;

		// Token: 0x0400872E RID: 34606
		private ScenarioWork m_Work;

		// Token: 0x0400872F RID: 34607
		private bool m_IsVisible;

		// Token: 0x04008730 RID: 34608
		private bool m_IsExpanded;

		// Token: 0x04008731 RID: 34609
		private bool m_IsPlayingExpandOrShrink;
	}
}
