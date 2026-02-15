using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.Utility;

namespace YgomGame.Credit
{
	// Token: 0x02001010 RID: 4112
	public class CreditEneconWidget
	{
		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06007BB2 RID: 31666 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool EneconIsActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007BB3 RID: 31667 RVA: 0x00002739 File Offset: 0x00000939
		public CreditEneconWidget(ElementObjectManager eom)
		{
		}

		// Token: 0x06007BB4 RID: 31668 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayButtonTween(string label)
		{
		}

		// Token: 0x06007BB5 RID: 31669 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegistCallbacks()
		{
		}

		// Token: 0x06007BB6 RID: 31670 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEneconIconClick()
		{
		}

		// Token: 0x0400B35F RID: 45919
		private readonly string BTN_ENECONICON_LABEL;

		// Token: 0x0400B360 RID: 45920
		private readonly string IMAGE_CARD_LABEL;

		// Token: 0x0400B361 RID: 45921
		private readonly string BTN_UP_LABEL;

		// Token: 0x0400B362 RID: 45922
		private readonly string BTN_DOWN_LABEL;

		// Token: 0x0400B363 RID: 45923
		private readonly string BTN_RIGHT_LABEL;

		// Token: 0x0400B364 RID: 45924
		private readonly string BTN_LEFT_LABEL;

		// Token: 0x0400B365 RID: 45925
		private readonly string BTN_A_LABEL;

		// Token: 0x0400B366 RID: 45926
		private readonly string BTN_B_LABEL;

		// Token: 0x0400B367 RID: 45927
		private readonly string BTN_C_LABEL;

		// Token: 0x0400B368 RID: 45928
		private readonly string BTN_START_LABEL;

		// Token: 0x0400B369 RID: 45929
		private readonly string ENECON_ROOT_LABEL;

		// Token: 0x0400B36A RID: 45930
		private readonly string TLABEL_ENECONIN;

		// Token: 0x0400B36B RID: 45931
		private readonly string TLABEL_ENECONOUT;

		// Token: 0x0400B36C RID: 45932
		private readonly string TLABEL_ENECONUP;

		// Token: 0x0400B36D RID: 45933
		private readonly string TLABEL_ENECONDOWN;

		// Token: 0x0400B36E RID: 45934
		private readonly string TLABEL_ENECONRIGHT;

		// Token: 0x0400B36F RID: 45935
		private readonly string TLABEL_ENECONLEFT;

		// Token: 0x0400B370 RID: 45936
		private readonly string TLABEL_ENECONA;

		// Token: 0x0400B371 RID: 45937
		private readonly string TLABEL_ENECONB;

		// Token: 0x0400B372 RID: 45938
		private readonly string TLABEL_ENECONC;

		// Token: 0x0400B373 RID: 45939
		private readonly string TLABEL_ENECONSTART;

		// Token: 0x0400B374 RID: 45940
		private readonly string TLABEL_CARDIN;

		// Token: 0x0400B375 RID: 45941
		private readonly string TLABEL_CARDOUT;

		// Token: 0x0400B376 RID: 45942
		private readonly int ENECON_MRK;

		// Token: 0x0400B377 RID: 45943
		private string m_tLabelEneconIn;

		// Token: 0x0400B378 RID: 45944
		private string m_tLabelEneconOut;

		// Token: 0x0400B379 RID: 45945
		private ElementObjectManager m_Eom;

		// Token: 0x0400B37A RID: 45946
		private GameObject m_eneconRootGO;

		// Token: 0x0400B37B RID: 45947
		private RawImage m_cardRawImage;

		// Token: 0x0400B37C RID: 45948
		private GameObject m_eneconObject;

		// Token: 0x0400B37D RID: 45949
		private bool m_eneconIsActive;

		// Token: 0x0400B37E RID: 45950
		private bool m_goIsActive;

		// Token: 0x0400B37F RID: 45951
		private bool m_useVirtualEnecon;

		// Token: 0x0400B380 RID: 45952
		public List<ValueTuple<string, Action<KeyCommand.OnKeyResult>>> labelCallBackPairs;

		// Token: 0x0400B381 RID: 45953
		private List<KeyCommand> m_keyCommandList;

		// Token: 0x0400B382 RID: 45954
		public Action<KeyCommand.OnKeyResult> OnKonamiCommandResultCallback;

		// Token: 0x0400B383 RID: 45955
		public Action<KeyCommand.OnKeyResult> OnEneconReleaseResultCallback;

		// Token: 0x0400B384 RID: 45956
		public Action<KeyCommand.OnKeyResult> OnEneconBreakResultCallback;

		// Token: 0x0400B385 RID: 45957
		private ElementObjectManager m_eneconEom;
	}
}
