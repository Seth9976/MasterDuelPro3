using System;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace TgomGame
{
	// Token: 0x02001194 RID: 4500
	public class PrivacySettingsTemplate : MonoBehaviour
	{
		// Token: 0x0600869F RID: 34463 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060086A0 RID: 34464 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetParams(int id, string informText, bool leftSelected, bool rightSelected, bool selected)
		{
		}

		// Token: 0x060086A1 RID: 34465 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickLeftCallBack(UnityAction callback)
		{
		}

		// Token: 0x060086A2 RID: 34466 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickRightCallBack(UnityAction callback)
		{
		}

		// Token: 0x060086A3 RID: 34467 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnClickLeft()
		{
			return false;
		}

		// Token: 0x060086A4 RID: 34468 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool OnClickRight()
		{
			return false;
		}

		// Token: 0x0400C13D RID: 49469
		private ElementObjectManager m_EOM;

		// Token: 0x0400C13E RID: 49470
		private SelectionButton m_LeftButton;

		// Token: 0x0400C13F RID: 49471
		private SelectionButton m_RightButton;

		// Token: 0x0400C140 RID: 49472
		private GameObject m_LeftImageOn;

		// Token: 0x0400C141 RID: 49473
		private GameObject m_LeftImageOff;

		// Token: 0x0400C142 RID: 49474
		private GameObject m_RightImageOn;

		// Token: 0x0400C143 RID: 49475
		private GameObject m_RightImageOff;

		// Token: 0x0400C144 RID: 49476
		private GameObject m_templateObj;

		// Token: 0x0400C145 RID: 49477
		public bool isLeftButton;

		// Token: 0x0400C146 RID: 49478
		public bool isRightButton;

		// Token: 0x0400C147 RID: 49479
		private readonly string LABEL_LEFT_BUTTON;

		// Token: 0x0400C148 RID: 49480
		private readonly string LABEL_RIGHT_BUTTON;
	}
}
