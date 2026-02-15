using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;
using YgomSystem.Utility;

namespace YgomGame.Credit
{
	// Token: 0x02001013 RID: 4115
	public class CreditViewController : BaseMenuViewController, IBackButtonWithoutSCSupported, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06007BBB RID: 31675 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007BBC RID: 31676 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007BBD RID: 31677 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnBackCommand()
		{
		}

		// Token: 0x06007BBE RID: 31678 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x06007BBF RID: 31679 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007BC0 RID: 31680 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnKonamiCommandResultCallback(KeyCommand.OnKeyResult result)
		{
		}

		// Token: 0x06007BC1 RID: 31681 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEneconBreakResultCallback(KeyCommand.OnKeyResult result)
		{
		}

		// Token: 0x06007BC2 RID: 31682 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEneconReleaseResultCallback(KeyCommand.OnKeyResult result)
		{
		}

		// Token: 0x06007BC3 RID: 31683 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06007BC4 RID: 31684 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCreditTemplateSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06007BC5 RID: 31685 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadInfoFromScriptableObject(UnityAction callBack)
		{
		}

		// Token: 0x0400B38D RID: 45965
		private readonly string BTN_BACK_SHORTCUT_LABEL;

		// Token: 0x0400B38E RID: 45966
		private readonly string CREDIT_LIST_LABEL;

		// Token: 0x0400B38F RID: 45967
		private readonly string ENECONWIDGET_LABEL;

		// Token: 0x0400B390 RID: 45968
		private readonly string TEXT_GROUPNAME_LABEL;

		// Token: 0x0400B391 RID: 45969
		private readonly string TEXT_POSITIONNAME_LABEL;

		// Token: 0x0400B392 RID: 45970
		private readonly string TEXT_NAME_LABEL;

		// Token: 0x0400B393 RID: 45971
		private readonly string TEXT_NAMEFONTCHANGED_LABEL;

		// Token: 0x0400B394 RID: 45972
		private readonly string TEXT_NAMEFONTCHANGED2_LABEL;

		// Token: 0x0400B395 RID: 45973
		private readonly string TEXT_POSITIONNAMEFONTCHANGED_LABEL;

		// Token: 0x0400B396 RID: 45974
		private readonly string TEXT_GROUPNAMEFONTCHANGED_LABEL;

		// Token: 0x0400B397 RID: 45975
		private readonly string TEXT_NAME2_LABEL;

		// Token: 0x0400B398 RID: 45976
		private readonly string TEMPLATE_SPACERM_LABEL;

		// Token: 0x0400B399 RID: 45977
		private readonly string TEMPLATE_GROUP_LABEL;

		// Token: 0x0400B39A RID: 45978
		private readonly string TEMPLATE_NAMEONLY_LABEL;

		// Token: 0x0400B39B RID: 45979
		private readonly string CONTENT_LABEL;

		// Token: 0x0400B39C RID: 45980
		private readonly string OBJ_MATE_LABEL;

		// Token: 0x0400B39D RID: 45981
		private readonly string TLABEL_MATEIN;

		// Token: 0x0400B39E RID: 45982
		private readonly string TLABEL_FONTCHANGE;

		// Token: 0x0400B39F RID: 45983
		private InfinityScrollView m_infinityScrollView;

		// Token: 0x0400B3A0 RID: 45984
		private ExtendedScrollRect m_extendedScrollRect;

		// Token: 0x0400B3A1 RID: 45985
		private List<int> m_templates;

		// Token: 0x0400B3A2 RID: 45986
		private readonly int k_GroupTNo;

		// Token: 0x0400B3A3 RID: 45987
		private readonly int k_PositionAndNameTNo;

		// Token: 0x0400B3A4 RID: 45988
		private readonly int k_NameOnlyTNo;

		// Token: 0x0400B3A5 RID: 45989
		private readonly int k_Member2TNo;

		// Token: 0x0400B3A6 RID: 45990
		private readonly int k_SpacerSTNo;

		// Token: 0x0400B3A7 RID: 45991
		private readonly int k_SpacerMTNo;

		// Token: 0x0400B3A8 RID: 45992
		private readonly int k_SpacerLTNo;

		// Token: 0x0400B3A9 RID: 45993
		private float m_scrollSpeed;

		// Token: 0x0400B3AA RID: 45994
		private bool m_mateIsActive;

		// Token: 0x0400B3AB RID: 45995
		private bool isFontChanged;

		// Token: 0x0400B3AC RID: 45996
		private CreditBGTimeline m_creditBGTimeline;

		// Token: 0x0400B3AD RID: 45997
		private CreditEneconWidget m_eneconWidget;

		// Token: 0x0400B3AE RID: 45998
		private bool isStarted;

		// Token: 0x0400B3AF RID: 45999
		private bool isEnd;

		// Token: 0x0400B3B0 RID: 46000
		private List<CreditInfo> m_creditInfoList;

		// Token: 0x0400B3B1 RID: 46001
		private int presentedCounter;
	}
}
