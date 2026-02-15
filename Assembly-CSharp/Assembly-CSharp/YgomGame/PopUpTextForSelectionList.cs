using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Utility;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame
{
	// Token: 0x020007D2 RID: 2002
	public class PopUpTextForSelectionList : MonoBehaviour
	{
		// Token: 0x06003E6F RID: 15983 RVA: 0x0000216D File Offset: 0x0000036D
		public void RegistPopUpCallback(SelectionButton sbtn, string text)
		{
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnDisappear()
		{
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x0000216D File Offset: 0x0000036D
		private void RegistPopUpCallbackImpl(SelectionButton sbtn, string text)
		{
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetMaxWidth(int maxWidth)
		{
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Show(string text)
		{
			return null;
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x0000216D File Offset: 0x0000036D
		private void StopAllTween()
		{
		}

		// Token: 0x04003768 RID: 14184
		private Dictionary<SelectionButton, string> m_PupUpBtnTabel;

		// Token: 0x04003769 RID: 14185
		private float m_MaxWidth;

		// Token: 0x0400376A RID: 14186
		private Coroutine m_CurrentCoroutine;

		// Token: 0x0400376B RID: 14187
		private SelectionButton m_TargetSbtn;

		// Token: 0x0400376C RID: 14188
		private Image m_Arrow;

		// Token: 0x0400376D RID: 14189
		private TweenAlpha m_TweenAlphaTo;

		// Token: 0x0400376E RID: 14190
		private TweenScaleTo m_TweenScaleRootTo;

		// Token: 0x0400376F RID: 14191
		private TweenScaleTo m_TweenScaleArrowTo;

		// Token: 0x04003770 RID: 14192
		private PopUpTextForSelectionList.Status m_Status;

		// Token: 0x04003771 RID: 14193
		private ElementObjectManager m_EOManager;

		// Token: 0x04003772 RID: 14194
		private ExtendedTextMeshProUGUI m_PopUpText;

		// Token: 0x04003773 RID: 14195
		private RectTransform m_Rt;

		// Token: 0x04003774 RID: 14196
		private AdaptiveTextContainer adaptiveTextContainer;

		// Token: 0x020007D3 RID: 2003
		private enum Status
		{
			// Token: 0x04003776 RID: 14198
			Opening,
			// Token: 0x04003777 RID: 14199
			Showing,
			// Token: 0x04003778 RID: 14200
			Closing,
			// Token: 0x04003779 RID: 14201
			Free
		}
	}
}
