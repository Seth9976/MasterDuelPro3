using System;
using UnityEngine;
using UnityEngine.Events;

namespace YgomGame.Card
{
	// Token: 0x020010EC RID: 4332
	public class AutoReleaseCardIllust : MonoBehaviour
	{
		// Token: 0x060080F9 RID: 33017 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardIllustManager(CardIllustManager cardIllustManager)
		{
		}

		// Token: 0x060080FA RID: 33018 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCard(int cardid, UnityAction<Texture2D> onFinish, bool immediateOnReuse = false)
		{
		}

		// Token: 0x060080FB RID: 33019 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadCardIllust(bool immediateOnReuse = false)
		{
		}

		// Token: 0x060080FC RID: 33020 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReleaseCardIllust()
		{
		}

		// Token: 0x060080FD RID: 33021 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0400B97A RID: 47482
		private CardIllustManager cardIllustManager;

		// Token: 0x0400B97B RID: 47483
		[SerializeField]
		private int requestCardId;

		// Token: 0x0400B97C RID: 47484
		[SerializeField]
		private int loadedCardid;

		// Token: 0x0400B97D RID: 47485
		private UnityAction<Texture2D> onFinish;
	}
}
