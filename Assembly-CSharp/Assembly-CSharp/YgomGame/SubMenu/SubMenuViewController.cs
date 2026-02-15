using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.SubMenu
{
	// Token: 0x020008E6 RID: 2278
	public abstract class SubMenuViewController : BaseBlurOverlayViewController
	{
		// Token: 0x060042BE RID: 17086 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetBadgeData()
		{
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTitleText(string str)
		{
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddTitleItem(string text)
		{
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x0000216D File Offset: 0x0000036D
		protected void AddMenuItem(string text, UnityAction clickCallback, string onclickSL = null, SubMenuViewController.Badge badgeType = SubMenuViewController.Badge.DEFAULT)
		{
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnMenuItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBadge(GameObject gom, int count = 0)
		{
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsSelectableDataIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenLicense()
		{
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddHelpButton()
		{
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddInquiryButton()
		{
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenInquirySheet()
		{
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenUserRegulationSheet(string title, bool fromHome = false)
		{
		}

		// Token: 0x04008129 RID: 33065
		private readonly string BTN_MASK_LABEL;

		// Token: 0x0400812A RID: 33066
		private readonly string TITLE_TEXT_LABEL;

		// Token: 0x0400812B RID: 33067
		private readonly string TITLE_TEMPLATE_LABEL;

		// Token: 0x0400812C RID: 33068
		private readonly string ITEM_TEMPLATE_LABEL;

		// Token: 0x0400812D RID: 33069
		private readonly string ITEM_TEXT_LABEL;

		// Token: 0x0400812E RID: 33070
		private readonly string MENU_LIST_LABEL;

		// Token: 0x0400812F RID: 33071
		private readonly string IMG_NUMBADGE_LABEL;

		// Token: 0x04008130 RID: 33072
		private readonly string IMG_NEWBADGE_LABEL;

		// Token: 0x04008131 RID: 33073
		private readonly string TXT_BADGE_LABEL;

		// Token: 0x04008132 RID: 33074
		private Dictionary<SubMenuViewController.Badge, int> badgeNumDic;

		// Token: 0x04008133 RID: 33075
		private Dictionary<string, SubMenuViewController.Badge> badgeTextDic;

		// Token: 0x04008134 RID: 33076
		private Dictionary<int, string> menuMap;

		// Token: 0x04008135 RID: 33077
		private InfinityScrollView m_InfinityScrollView;

		// Token: 0x04008136 RID: 33078
		private List<int> m_Templates;

		// Token: 0x04008137 RID: 33079
		private List<string> m_Texts;

		// Token: 0x04008138 RID: 33080
		private readonly int k_TitleTNo;

		// Token: 0x04008139 RID: 33081
		private readonly int k_ItemTNo;

		// Token: 0x0400813A RID: 33082
		private readonly int k_SpacerNo;

		// Token: 0x0400813B RID: 33083
		private List<string> m_SoundLabelsClick;

		// Token: 0x0400813C RID: 33084
		private List<UnityAction> m_ClickCallBacks;

		// Token: 0x0400813D RID: 33085
		private float delayFactor;

		// Token: 0x020008E7 RID: 2279
		public enum Badge
		{
			// Token: 0x0400813F RID: 33087
			DEFAULT,
			// Token: 0x04008140 RID: 33088
			WCS_CONFIRM,
			// Token: 0x04008141 RID: 33089
			WCS_DECK_REGIST
		}
	}
}
