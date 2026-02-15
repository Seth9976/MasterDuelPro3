using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Card
{
	// Token: 0x02001109 RID: 4361
	public class CardPictureCreator : CardTextureCreatorBase
	{
		// Token: 0x060081D1 RID: 33233 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardPictureCreator CreateCardPictureCreator(CardIllustManager cardIllustManager)
		{
			return null;
		}

		// Token: 0x060081D2 RID: 33234 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool CreateTaskImpl(CardTextureCreatorBase.TaskDesc desc)
		{
			return false;
		}

		// Token: 0x060081D3 RID: 33235 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InitComponent()
		{
		}

		// Token: 0x060081D4 RID: 33236 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SetCanvas()
		{
		}

		// Token: 0x060081D5 RID: 33237 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SetCPRenderTexture()
		{
		}

		// Token: 0x060081D6 RID: 33238 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060081D7 RID: 33239 RVA: 0x0000216D File Offset: 0x0000036D
		public override void CancelCardPictureTask(int cardid)
		{
		}

		// Token: 0x060081D8 RID: 33240 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupCardAsyncCallback(CardTextureCreatorBase.TaskDesc desc)
		{
		}

		// Token: 0x0400BA3C RID: 47676
		private const int MAXRETRYCOUNT = 10;

		// Token: 0x0400BA3D RID: 47677
		private CardIllustManager m_CardIllustManager;

		// Token: 0x0400BA3E RID: 47678
		protected CardPicture m_CardPictureBase;

		// Token: 0x0400BA3F RID: 47679
		protected Dictionary<int, Texture2D> m_TaskQueue_WaitStandby;

		// Token: 0x0400BA40 RID: 47680
		protected Dictionary<int, int> m_RetryCountTable;

		// Token: 0x0400BA41 RID: 47681
		protected bool m_NoneCallBackStandbyFlag;

		// Token: 0x0400BA42 RID: 47682
		public bool m_IgnoreIllsut;
	}
}
