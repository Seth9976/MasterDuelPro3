using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Deck
{
	// Token: 0x02000FA5 RID: 4005
	public class CardBase : MonoBehaviour
	{
		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06007656 RID: 30294 RVA: 0x000F65F4 File Offset: 0x000F47F4
		// (set) Token: 0x06007657 RID: 30295 RVA: 0x0000216D File Offset: 0x0000036D
		public CardBaseData m_BaseData
		{
			[CompilerGenerated]
			get
			{
				return default(CardBaseData);
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x06007658 RID: 30296 RVA: 0x0000216D File Offset: 0x0000036D
		protected void InitializeElemnts()
		{
		}

		// Token: 0x06007659 RID: 30297 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x0600765A RID: 30298 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600765B RID: 30299 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void SetData(CardBaseData data, int regulationID = -1)
		{
		}

		// Token: 0x0600765C RID: 30300 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardImage(Texture image)
		{
		}

		// Token: 0x0600765D RID: 30301 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardImageMaterial(Material mat)
		{
		}

		// Token: 0x0600765E RID: 30302 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetMonochrome(bool b)
		{
		}

		// Token: 0x0600765F RID: 30303 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRentalImage(bool b)
		{
		}

		// Token: 0x06007660 RID: 30304 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnsetCardImagTexture()
		{
		}

		// Token: 0x06007661 RID: 30305 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDisp(bool disp)
		{
		}

		// Token: 0x06007662 RID: 30306 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedCallback(UnityAction callback)
		{
		}

		// Token: 0x06007663 RID: 30307 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnDeselectedCallback(UnityAction callback)
		{
		}

		// Token: 0x06007664 RID: 30308 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCallback(UnityAction callback)
		{
		}

		// Token: 0x06007665 RID: 30309 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnRightClickCallback(UnityAction<bool> callback)
		{
		}

		// Token: 0x06007666 RID: 30310 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectedKeyL2Callback(UnityAction callback)
		{
		}

		// Token: 0x06007667 RID: 30311 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDragBeginCallback(UnityAction<Vector2> callback)
		{
		}

		// Token: 0x06007668 RID: 30312 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDragCallback(UnityAction<Vector2> callback)
		{
		}

		// Token: 0x06007669 RID: 30313 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDragEndCallback(UnityAction<Vector2> callback)
		{
		}

		// Token: 0x0600766A RID: 30314 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeDragBeginCallback(Vector2 screenPoint)
		{
		}

		// Token: 0x0600766B RID: 30315 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeDragCallback(Vector2 screenPoint)
		{
		}

		// Token: 0x0600766C RID: 30316 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeDragEndCallback(Vector2 screenPoint)
		{
		}

		// Token: 0x0400AF6E RID: 44910
		protected ElementObjectManager m_eom;

		// Token: 0x0400AF6F RID: 44911
		protected const string LABEL_IMG_CARDIMAGE = "ImageCard";

		// Token: 0x0400AF70 RID: 44912
		protected const string LABEL_IMG_NOCARD = "NoCard";

		// Token: 0x0400AF71 RID: 44913
		protected const string LABEL_IMG_RENTALCARD = "RentalCard";

		// Token: 0x0400AF72 RID: 44914
		public SelectionButton m_ImageCardButton;

		// Token: 0x0400AF73 RID: 44915
		public Image m_ImageNoCard;

		// Token: 0x0400AF74 RID: 44916
		public Image m_ImageRentalCard;

		// Token: 0x0400AF75 RID: 44917
		protected RawImage m_CardImage;

		// Token: 0x0400AF76 RID: 44918
		protected UnityAction m_OnClickAction;

		// Token: 0x0400AF77 RID: 44919
		protected UnityAction m_OnSelectedAction;

		// Token: 0x0400AF78 RID: 44920
		protected UnityAction m_OnDeselectedAction;

		// Token: 0x0400AF79 RID: 44921
		protected UnityAction<bool> m_OnRightClickAction;

		// Token: 0x0400AF7A RID: 44922
		protected UnityAction m_SelectedKeyL2Action;

		// Token: 0x0400AF7B RID: 44923
		protected UnityAction<Vector2> m_DragBeginAction;

		// Token: 0x0400AF7C RID: 44924
		protected UnityAction<Vector2> m_DragAction;

		// Token: 0x0400AF7D RID: 44925
		protected UnityAction<Vector2> m_DragEndAction;

		// Token: 0x0400AF7E RID: 44926
		private bool isIni;
	}
}
