using System;
using UnityEngine.UI;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x02001478 RID: 5240
	public class OnlineServantUI : ServantUI
	{
		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x06009852 RID: 38994 RVA: 0x00166B7C File Offset: 0x00164D7C
		private SelectionToggle_Online ToggleLegacy
		{
			get
			{
				return this.m_ToggleLegacy = ((this.m_ToggleLegacy != null) ? this.m_ToggleLegacy : base.Manager.GetElement<SelectionToggle_Online>("ToggleLegacy"));
			}
		}

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x06009853 RID: 38995 RVA: 0x00166BB8 File Offset: 0x00164DB8
		private SelectionToggle_Online ToggleHost
		{
			get
			{
				return this.m_ToggleHost = ((this.m_ToggleHost != null) ? this.m_ToggleHost : base.Manager.GetElement<SelectionToggle_Online>("ToggleLocal"));
			}
		}

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x06009854 RID: 38996 RVA: 0x00166BF4 File Offset: 0x00164DF4
		private SelectionToggle_Online ToggleMyCard
		{
			get
			{
				return this.m_ToggleMyCard = ((this.m_ToggleMyCard != null) ? this.m_ToggleMyCard : base.Manager.GetElement<SelectionToggle_Online>("ToggleMyCard"));
			}
		}

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x06009855 RID: 38997 RVA: 0x00166C30 File Offset: 0x00164E30
		public PageLegacy PageLegacy
		{
			get
			{
				return this.m_PageLegacy = ((this.m_PageLegacy != null) ? this.m_PageLegacy : base.Manager.GetElement<PageLegacy>("PageLegacy"));
			}
		}

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x06009856 RID: 38998 RVA: 0x00166C6C File Offset: 0x00164E6C
		public PageHost PageHost
		{
			get
			{
				return this.m_PageHost = ((this.m_PageHost != null) ? this.m_PageHost : base.Manager.GetElement<PageHost>("PageHost"));
			}
		}

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x06009857 RID: 38999 RVA: 0x00166CA8 File Offset: 0x00164EA8
		public PageMyCard PageMyCard
		{
			get
			{
				return this.m_PageMyCard = ((this.m_PageMyCard != null) ? this.m_PageMyCard : base.Manager.GetElement<PageMyCard>("PageMyCard"));
			}
		}

		// Token: 0x06009858 RID: 39000 RVA: 0x00166CE4 File Offset: 0x00164EE4
		private void Awake()
		{
			this.ToggleLegacy.SetToggleOn(true);
		}

		// Token: 0x06009859 RID: 39001 RVA: 0x00166CF2 File Offset: 0x00164EF2
		public override void ShowEvent()
		{
			base.ShowEvent();
			this.PageLegacy.PrintAddresses("");
		}

		// Token: 0x0600985A RID: 39002 RVA: 0x00166D0C File Offset: 0x00164F0C
		public void SelectLastSelectable(Selectable lastSelectable)
		{
			if (this.PageLegacy.gameObject.activeSelf)
			{
				if (lastSelectable != null && lastSelectable.transform.IsChildOf(this.PageLegacy.transform))
				{
					lastSelectable.Select();
					return;
				}
				this.PageLegacy.SelectDefault();
				return;
			}
			else
			{
				if (!this.PageHost.gameObject.activeSelf)
				{
					if (this.PageMyCard.gameObject.activeSelf)
					{
						if (lastSelectable != null && lastSelectable.transform.IsChildOf(this.PageMyCard.transform))
						{
							lastSelectable.Select();
							return;
						}
						this.PageMyCard.SelectDefault();
					}
					return;
				}
				if (lastSelectable != null && lastSelectable.transform.IsChildOf(this.PageHost.transform))
				{
					lastSelectable.Select();
					return;
				}
				this.PageHost.SelectDefault();
				return;
			}
		}

		// Token: 0x0600985B RID: 39003 RVA: 0x00166DEA File Offset: 0x00164FEA
		public void PageLeft()
		{
			this.ToggleLegacy.OnLeftSelection();
		}

		// Token: 0x0600985C RID: 39004 RVA: 0x00166DF7 File Offset: 0x00164FF7
		public void PageRight()
		{
			this.ToggleLegacy.OnRightSelection();
		}

		// Token: 0x0400D670 RID: 54896
		private const string LABEL_STG_LEGACY = "ToggleLegacy";

		// Token: 0x0400D671 RID: 54897
		private SelectionToggle_Online m_ToggleLegacy;

		// Token: 0x0400D672 RID: 54898
		private const string LABEL_STG_HOST = "ToggleLocal";

		// Token: 0x0400D673 RID: 54899
		private SelectionToggle_Online m_ToggleHost;

		// Token: 0x0400D674 RID: 54900
		private const string LABEL_STG_MyCard = "ToggleMyCard";

		// Token: 0x0400D675 RID: 54901
		private SelectionToggle_Online m_ToggleMyCard;

		// Token: 0x0400D676 RID: 54902
		private const string LABEL_MONO_PAGELEGACY = "PageLegacy";

		// Token: 0x0400D677 RID: 54903
		private PageLegacy m_PageLegacy;

		// Token: 0x0400D678 RID: 54904
		private const string LABEL_MONO_PAGEHOST = "PageHost";

		// Token: 0x0400D679 RID: 54905
		private PageHost m_PageHost;

		// Token: 0x0400D67A RID: 54906
		private const string LABEL_MONO_PAGEMYCARD = "PageMyCard";

		// Token: 0x0400D67B RID: 54907
		private PageMyCard m_PageMyCard;
	}
}
