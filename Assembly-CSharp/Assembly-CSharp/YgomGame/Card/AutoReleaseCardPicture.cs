using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YgomGame.Card
{
	// Token: 0x020010EF RID: 4335
	public class AutoReleaseCardPicture : MonoBehaviour
	{
		// Token: 0x0600810B RID: 33035 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardMaterialManager(CardPictureManager cardPictureManager)
		{
		}

		// Token: 0x0600810C RID: 33036 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardList(List<int> cardidList, List<UnityAction<Texture2D>> onFinishList, UnityAction onFinishAll = null)
		{
		}

		// Token: 0x0600810D RID: 33037 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCard(int cardid, UnityAction<Texture2D> onFinish)
		{
		}

		// Token: 0x0600810E RID: 33038 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReleaseCardPicture()
		{
		}

		// Token: 0x0600810F RID: 33039 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadCardPicture()
		{
		}

		// Token: 0x06008110 RID: 33040 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0400B98A RID: 47498
		private CardPictureManager cardPictureManager;

		// Token: 0x0400B98B RID: 47499
		[SerializeField]
		private List<int> cardidList;

		// Token: 0x0400B98C RID: 47500
		private List<int> taskidList;

		// Token: 0x0400B98D RID: 47501
		private List<UnityAction<Texture2D>> onFinishList;

		// Token: 0x0400B98E RID: 47502
		private UnityAction onFinishAll;

		// Token: 0x0400B98F RID: 47503
		private int loadingCount;
	}
}
