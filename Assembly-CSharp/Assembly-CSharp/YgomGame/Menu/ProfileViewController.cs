using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomGame.Duel;
using YgomGame.Menu.Common;
using YgomGame.Utility;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000AD1 RID: 2769
	public class ProfileViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060050AF RID: 20655 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator DelayedInvokeCallback(Action action)
		{
			return null;
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitMenuButtons()
		{
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickBlock()
		{
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateProfile(Dictionary<string, object> profileDic)
		{
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayMateMotion()
		{
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitMateSettings(MateTransformSetting modelLocateSettings)
		{
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetFollowButton(bool isFollowed)
		{
		}

		// Token: 0x04008EDA RID: 36570
		private readonly string MATE_TRANSFORM_SETTING_PATH;

		// Token: 0x04008EDB RID: 36571
		private readonly string TITLE_NAME;

		// Token: 0x04008EDC RID: 36572
		private readonly string BTN_MENU_TMP_LABEL;

		// Token: 0x04008EDD RID: 36573
		private readonly string ROOT_MENU_LABEL;

		// Token: 0x04008EDE RID: 36574
		private readonly string ROOT_OVERVIEW_LABEL;

		// Token: 0x04008EDF RID: 36575
		private readonly string TMP_BTN_LABEL;

		// Token: 0x04008EE0 RID: 36576
		private readonly string TMP_TXT_LABEL;

		// Token: 0x04008EE1 RID: 36577
		private readonly string OBJ_MATE_LABEL;

		// Token: 0x04008EE2 RID: 36578
		private TextMeshProUGUI titleText;

		// Token: 0x04008EE3 RID: 36579
		private GameObject rootMenu;

		// Token: 0x04008EE4 RID: 36580
		private GameObject rootOverview;

		// Token: 0x04008EE5 RID: 36581
		private long pcode;

		// Token: 0x04008EE6 RID: 36582
		private bool isFollowed;

		// Token: 0x04008EE7 RID: 36583
		private bool isMine;

		// Token: 0x04008EE8 RID: 36584
		private bool fromDuel;

		// Token: 0x04008EE9 RID: 36585
		private MateTransformSetting m_ModelLocateSettings;

		// Token: 0x04008EEA RID: 36586
		private DefinitionSetting matchingDefine;

		// Token: 0x04008EEB RID: 36587
		private Character2D chara;

		// Token: 0x04008EEC RID: 36588
		private int charaId;

		// Token: 0x04008EED RID: 36589
		private bool isNeedCharaCreate;

		// Token: 0x04008EEE RID: 36590
		private List<GameObject> menuGOs;

		// Token: 0x04008EEF RID: 36591
		private GameObject followButton;

		// Token: 0x04008EF0 RID: 36592
		private ProfileCard profileCard;

		// Token: 0x02000AD2 RID: 2770
		private enum ProfileMenu
		{
			// Token: 0x04008EF2 RID: 36594
			OVERVIEW,
			// Token: 0x04008EF3 RID: 36595
			REPLAY,
			// Token: 0x04008EF4 RID: 36596
			DATA
		}
	}
}
