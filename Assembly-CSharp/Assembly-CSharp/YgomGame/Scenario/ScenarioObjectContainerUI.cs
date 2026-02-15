using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Scenario
{
	// Token: 0x020009DB RID: 2523
	public class ScenarioObjectContainerUI : ScenarioContainerBase
	{
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06004969 RID: 18793 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject rootUI
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x0600496A RID: 18794 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600496B RID: 18795 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onClickAreaEvent
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

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x0600496C RID: 18796 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600496D RID: 18797 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onClickInputBlockerEvent
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

		// Token: 0x0600496E RID: 18798 RVA: 0x000F4916 File Offset: 0x000F2B16
		public ScenarioObjectContainerUI(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ScenarioWork work)
		{
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnBack()
		{
			return false;
		}

		// Token: 0x0400873D RID: 34621
		private readonly string k_ELabelRootUI;

		// Token: 0x0400873E RID: 34622
		private readonly string k_ELabelClickAreaButton;

		// Token: 0x0400873F RID: 34623
		private readonly string k_ELabelRootScreen;

		// Token: 0x04008740 RID: 34624
		private readonly string k_ELabelTextArea;

		// Token: 0x04008741 RID: 34625
		private readonly string k_ELabelScreenTextUnder;

		// Token: 0x04008742 RID: 34626
		private readonly string k_ELabelScreenTextOver;

		// Token: 0x04008743 RID: 34627
		private readonly string k_ELabelMenuButtonAcordion;

		// Token: 0x04008744 RID: 34628
		private readonly string k_ELabelLogScreen;

		// Token: 0x04008745 RID: 34629
		private readonly string k_ELabelInputBlocker;

		// Token: 0x04008746 RID: 34630
		private readonly string k_ELabelInputBlockerButton;

		// Token: 0x04008747 RID: 34631
		public readonly Selector inputBlocker;

		// Token: 0x04008748 RID: 34632
		public readonly SelectionButton clickAreaButton;

		// Token: 0x04008749 RID: 34633
		public readonly ScenarioRootScreen rootScreen;

		// Token: 0x0400874A RID: 34634
		public readonly ScenarioTextContainer textContainer;

		// Token: 0x0400874B RID: 34635
		public readonly ScenarioScreenContainer screenContainer;

		// Token: 0x0400874C RID: 34636
		public readonly ScenarioMenuContainer menuContainer;

		// Token: 0x0400874D RID: 34637
		public readonly ScenarioLogContainer logContainer;
	}
}
