using System;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000EDD RID: 3805
	public class PhaseSelectWindow : MonoBehaviour
	{
		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06006EFC RID: 28412 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isOpened
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006EFD RID: 28413 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(DuelClient host, Transform parent, UnityAction<PhaseSelectWindow> onFinish)
		{
		}

		// Token: 0x06006EFE RID: 28414 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(DuelClient host)
		{
		}

		// Token: 0x06006EFF RID: 28415 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetPhaseButton()
		{
		}

		// Token: 0x06006F00 RID: 28416 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenWindow()
		{
		}

		// Token: 0x06006F01 RID: 28417 RVA: 0x0000216D File Offset: 0x0000036D
		public void CloseWindow()
		{
		}

		// Token: 0x0400AA0A RID: 43530
		private const string LABEL_EO_WINDOW = "Window";

		// Token: 0x0400AA0B RID: 43531
		private const string LABEL_EO_CONTENT = "Content";

		// Token: 0x0400AA0C RID: 43532
		private const string LABEL_EO_BUTTON_CLOSE = "ButtonClose";

		// Token: 0x0400AA0D RID: 43533
		private const string LABEL_EO_BUTTONPHASE = "ButtonPhase";

		// Token: 0x0400AA0E RID: 43534
		private const string LABEL_EO_TEXTTITLE = "TextTitle";

		// Token: 0x0400AA0F RID: 43535
		private const string LABEL_TW_IN = "In";

		// Token: 0x0400AA10 RID: 43536
		private const string LABEL_TW_OUT = "Out";

		// Token: 0x0400AA11 RID: 43537
		private const string LABEL_TW_CURRENT = "Current";

		// Token: 0x0400AA12 RID: 43538
		private const string LABEL_TW_NORMAL = "Normal";

		// Token: 0x0400AA13 RID: 43539
		private const string LABEL_TW_SELECT = "select";

		// Token: 0x0400AA14 RID: 43540
		private DuelClient m_Host;

		// Token: 0x0400AA15 RID: 43541
		private ElementObjectManager m_RootManager;

		// Token: 0x0400AA16 RID: 43542
		private SelectionButton m_ButtonClose;

		// Token: 0x0400AA17 RID: 43543
		private SelectionButton[] m_ButtonPhases;

		// Token: 0x0400AA18 RID: 43544
		private const int m_PhaseNum = 6;
	}
}
