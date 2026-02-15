using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomGame.Duel;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame
{
	// Token: 0x020007C9 RID: 1993
	public class GenericCardListEx : MonoBehaviour, IGenericScrollViewSupport
	{
		// Token: 0x06003E3A RID: 15930 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(Transform parent, UnityAction<int> onClick, bool ismobile, UnityAction<GenericCardListEx> onFinish)
		{
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFullScreenUiBg(FullScreenUiBg fullScreenUiBg)
		{
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show()
		{
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close(bool closebg = true)
		{
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowRelativeCardByCardid(int cardid)
		{
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x0000216D File Offset: 0x0000036D
		protected void Initialize(UnityAction<int> onClick, bool ismobile)
		{
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitCardInfo(UnityAction<int> onClick, bool ismobile)
		{
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitTween(GameObject root, GameObject scrollview)
		{
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetRelativeCardList(int cardid, bool updatelist = true)
		{
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateScrollView()
		{
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemExit(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize(GameObject gob)
		{
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x0400372A RID: 14122
		protected ElementObjectManager m_EoManager;

		// Token: 0x0400372B RID: 14123
		protected GenericScrollView m_ScrollView;

		// Token: 0x0400372C RID: 14124
		protected RawImage m_OriginCard;

		// Token: 0x0400372D RID: 14125
		protected SelectionButton m_OriginCardSbtn;

		// Token: 0x0400372E RID: 14126
		protected List<int> m_Datalist;

		// Token: 0x0400372F RID: 14127
		protected CardInfoForGenericCardListEx m_CardInfoForRelativeCard;

		// Token: 0x04003730 RID: 14128
		protected int m_OriginCardid;

		// Token: 0x04003731 RID: 14129
		protected int m_ShowingCardid;

		// Token: 0x04003732 RID: 14130
		protected bool m_IsVisible;

		// Token: 0x04003733 RID: 14131
		protected SelectionButton m_RelativeCardSbtn;

		// Token: 0x04003734 RID: 14132
		protected bool m_IsFading;

		// Token: 0x04003735 RID: 14133
		protected ExtendedTextMeshProUGUI m_TitleText;

		// Token: 0x04003736 RID: 14134
		protected FullScreenUiBg m_FullScreenUiBg;

		// Token: 0x04003737 RID: 14135
		protected UiSwitchTweenAnimationController m_UiSwitchTweenAnimationController;
	}
}
