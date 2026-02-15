using System;

namespace YgomGame.Card
{
	// Token: 0x02001104 RID: 4356
	public class CardMaskCreator : CardTextureCreatorBase
	{
		// Token: 0x0600817C RID: 33148 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardMaskCreator CreateCardMaskCreator()
		{
			return null;
		}

		// Token: 0x0600817D RID: 33149 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool CreateTaskImpl(CardTextureCreatorBase.TaskDesc desc)
		{
			return false;
		}

		// Token: 0x0600817E RID: 33150 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InitComponent()
		{
		}

		// Token: 0x0600817F RID: 33151 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SetCanvas()
		{
		}

		// Token: 0x06008180 RID: 33152 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void SetCPRenderTexture()
		{
		}

		// Token: 0x06008181 RID: 33153 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06008182 RID: 33154 RVA: 0x0000216D File Offset: 0x0000036D
		public override void CancelCardPictureTask(int cardid)
		{
		}

		// Token: 0x0400BA05 RID: 47621
		private CardPictureTop m_CardPictureTopBase;
	}
}
