using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace YgomGame.Card
{
	// Token: 0x02001111 RID: 4369
	public abstract class CardTextureCreatorBase : MonoBehaviour
	{
		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x060081F4 RID: 33268 RVA: 0x0000216A File Offset: 0x0000036A
		public RenderTexture renderTexture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x060081F5 RID: 33269 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected float m_Compression
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x060081F6 RID: 33270 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060081F7 RID: 33271 RVA: 0x0000216D File Offset: 0x0000036D
		public CardQuality quality
		{
			[CompilerGenerated]
			get
			{
				return CardQuality.HIGH;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x060081F8 RID: 33272 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardQuality(CardQuality quality)
		{
		}

		// Token: 0x060081F9 RID: 33273 RVA: 0x0000216D File Offset: 0x0000036D
		public void CreateTextureAsync(UnityAction<Texture2D> onfinished, int cardid, Texture2D tex)
		{
		}

		// Token: 0x060081FA RID: 33274 RVA: 0x0000216D File Offset: 0x0000036D
		public void CancelAllTask()
		{
		}

		// Token: 0x060081FB RID: 33275 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x060081FC RID: 33276
		public abstract void CancelCardPictureTask(int cardid);

		// Token: 0x060081FD RID: 33277
		protected abstract void InitComponent();

		// Token: 0x060081FE RID: 33278
		protected abstract void SetCanvas();

		// Token: 0x060081FF RID: 33279
		protected abstract void SetCPRenderTexture();

		// Token: 0x06008200 RID: 33280
		protected abstract bool CreateTaskImpl(CardTextureCreatorBase.TaskDesc desc);

		// Token: 0x06008201 RID: 33281 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCamera()
		{
		}

		// Token: 0x06008202 RID: 33282 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetStandByTask(CardTextureCreatorBase.TaskDesc task)
		{
		}

		// Token: 0x06008203 RID: 33283 RVA: 0x0000216D File Offset: 0x0000036D
		protected void CopyTexPixel()
		{
		}

		// Token: 0x06008204 RID: 33284 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool RemoveTaskFromQueueByCardid(int cardid)
		{
			return false;
		}

		// Token: 0x0400BA5F RID: 47711
		protected const int MAXCARDPICTUREHEIGHT = 1054;

		// Token: 0x0400BA60 RID: 47712
		protected const int MAXCARDPICTUREWIDTH = 723;

		// Token: 0x0400BA61 RID: 47713
		public const int LAYER_CARDCREATOR = 9;

		// Token: 0x0400BA62 RID: 47714
		public const int CARDWIDTH = 59;

		// Token: 0x0400BA63 RID: 47715
		public const int CARDHEIGHT = 86;

		// Token: 0x0400BA64 RID: 47716
		protected const float TARGETFRAME = 40f;

		// Token: 0x0400BA65 RID: 47717
		public UnityAction<CardTextureCreatorBase.TaskDesc> onCancelTask;

		// Token: 0x0400BA66 RID: 47718
		public UnityAction<CardTextureCreatorBase.TaskDesc> onReturnInstance;

		// Token: 0x0400BA67 RID: 47719
		protected Dictionary<int, UnityAction<Texture2D>> m_CardidCallbackTable;

		// Token: 0x0400BA68 RID: 47720
		protected Queue<CardTextureCreatorBase.TaskDesc> m_TaskQueue;

		// Token: 0x0400BA69 RID: 47721
		protected CardTextureCreatorBase.TaskDesc m_StandByTask;

		// Token: 0x0400BA6A RID: 47722
		protected Canvas m_CPCanvas;

		// Token: 0x0400BA6B RID: 47723
		protected Camera m_CPCamera;

		// Token: 0x0400BA6C RID: 47724
		protected RenderTexture m_RenderTexture;

		// Token: 0x0400BA6D RID: 47725
		protected bool m_IsStandby;

		// Token: 0x02001112 RID: 4370
		public struct TaskDesc
		{
			// Token: 0x0400BA6E RID: 47726
			public int cardid;

			// Token: 0x0400BA6F RID: 47727
			public Texture2D tex;
		}
	}
}
