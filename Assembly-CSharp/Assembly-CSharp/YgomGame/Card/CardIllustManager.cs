using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace YgomGame.Card
{
	// Token: 0x02001100 RID: 4352
	public class CardIllustManager
	{
		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x06008168 RID: 33128 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CardIllustCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06008169 RID: 33129 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardIllustManager Create()
		{
			return null;
		}

		// Token: 0x0600816A RID: 33130 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetRichTextCardPath()
		{
		}

		// Token: 0x0600816B RID: 33131 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetPathByCardId(int mrk)
		{
			return null;
		}

		// Token: 0x0600816C RID: 33132 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x0600816D RID: 33133 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool UpdateIllustLoadProcess()
		{
			return false;
		}

		// Token: 0x0600816E RID: 33134 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool AsyncLoadImpl()
		{
			return false;
		}

		// Token: 0x0600816F RID: 33135 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool AsyncLoadComplete()
		{
			return false;
		}

		// Token: 0x06008170 RID: 33136 RVA: 0x0000216D File Offset: 0x0000036D
		private void releaseAll()
		{
		}

		// Token: 0x06008171 RID: 33137 RVA: 0x0000216D File Offset: 0x0000036D
		private void requestCompleteHandler(string path)
		{
		}

		// Token: 0x06008172 RID: 33138 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Release(int mrk)
		{
			return false;
		}

		// Token: 0x06008173 RID: 33139 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReleaseAll()
		{
		}

		// Token: 0x06008174 RID: 33140 RVA: 0x0000216D File Offset: 0x0000036D
		public void Destroy()
		{
		}

		// Token: 0x06008175 RID: 33141 RVA: 0x0000216D File Offset: 0x0000036D
		public void GetIllustAsync(UnityAction<Texture2D> handler, int cardid, bool immediateOnReuse = false)
		{
		}

		// Token: 0x06008176 RID: 33142 RVA: 0x0000216D File Offset: 0x0000036D
		protected void RemoveCompleteCallbackByCardid(int cardid)
		{
		}

		// Token: 0x06008177 RID: 33143 RVA: 0x0000216D File Offset: 0x0000036D
		protected void CardIllustDebugMessage(int cardid, bool isrelease)
		{
		}

		// Token: 0x0400B9F2 RID: 47602
		private const string BASEPATH = "Card/Images/";

		// Token: 0x0400B9F3 RID: 47603
		private const string DUMMYILLUSTPATH = "Card/dummyCard";

		// Token: 0x0400B9F4 RID: 47604
		private const string MrkZero = "0000";

		// Token: 0x0400B9F5 RID: 47605
		private Dictionary<uint, CardIllustManager.TexInfo> pathToTexDictionary;

		// Token: 0x0400B9F6 RID: 47606
		private Queue<CardIllustManager.CallBackInfo> loadCompleteCallBackQueue;

		// Token: 0x0400B9F7 RID: 47607
		private StringBuilder stringBuilder;

		// Token: 0x0400B9F8 RID: 47608
		private List<int> m_AsyncLoadQueue;

		// Token: 0x02001101 RID: 4353
		private class TexInfo
		{
			// Token: 0x06008179 RID: 33145 RVA: 0x00002739 File Offset: 0x00000939
			public TexInfo(Texture2D texture = null)
			{
			}

			// Token: 0x0600817A RID: 33146 RVA: 0x0000216D File Offset: 0x0000036D
			public void ChangeStep(CardIllustManager.IllustLoadStep step)
			{
			}

			// Token: 0x0400B9F9 RID: 47609
			public Texture2D tex;

			// Token: 0x0400B9FA RID: 47610
			public int refCount;

			// Token: 0x0400B9FB RID: 47611
			public CardIllustManager.IllustLoadStep step;

			// Token: 0x0400B9FC RID: 47612
			public int cardid;

			// Token: 0x0400B9FD RID: 47613
			public List<UnityAction<Texture2D>> handlerList;
		}

		// Token: 0x02001102 RID: 4354
		private class CallBackInfo
		{
			// Token: 0x0600817B RID: 33147 RVA: 0x00002739 File Offset: 0x00000939
			public CallBackInfo(int cardid, UnityAction<Texture2D> callback, Texture2D tex)
			{
			}

			// Token: 0x0400B9FE RID: 47614
			public UnityAction<Texture2D> callback;

			// Token: 0x0400B9FF RID: 47615
			public Texture2D tex;

			// Token: 0x0400BA00 RID: 47616
			public int cardid;
		}

		// Token: 0x02001103 RID: 4355
		private enum IllustLoadStep
		{
			// Token: 0x0400BA02 RID: 47618
			WAIT,
			// Token: 0x0400BA03 RID: 47619
			LOADING,
			// Token: 0x0400BA04 RID: 47620
			END
		}
	}
}
