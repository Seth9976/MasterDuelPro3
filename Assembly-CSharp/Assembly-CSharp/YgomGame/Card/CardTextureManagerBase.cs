using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YgomGame.Card
{
	// Token: 0x02001113 RID: 4371
	public abstract class CardTextureManagerBase
	{
		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06008206 RID: 33286 RVA: 0x0000216A File Offset: 0x0000036A
		public static Texture2D DUMMYCARD
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06008207 RID: 33287 RVA: 0x000029CC File Offset: 0x00000BCC
		protected int m_CardInstanceNum
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x06008208 RID: 33288 RVA: 0x000029CC File Offset: 0x00000BCC
		public int RefedTextureCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06008209 RID: 33289 RVA: 0x000029CC File Offset: 0x00000BCC
		public int NoRefTextureCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x0600820A RID: 33290 RVA: 0x000029CC File Offset: 0x00000BCC
		public int TaskCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x0600820B RID: 33291 RVA: 0x000029CC File Offset: 0x00000BCC
		public int UsedInstanceCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x0600820C RID: 33292 RVA: 0x000029CC File Offset: 0x00000BCC
		public int FreeInstanceCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x0600820D RID: 33293 RVA: 0x000029CC File Offset: 0x00000BCC
		public int AllInstanceCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x0600820E RID: 33294 RVA: 0x000029CC File Offset: 0x00000BCC
		public int AddInstanceCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600820F RID: 33295 RVA: 0x0000216D File Offset: 0x0000036D
		protected void Initialize()
		{
		}

		// Token: 0x06008210 RID: 33296 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool ExecuteCallback(int cardid, Texture2D cardpicture)
		{
			return false;
		}

		// Token: 0x06008211 RID: 33297 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ReduceNoReferenceData()
		{
		}

		// Token: 0x06008212 RID: 33298 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCacheActive(bool enable)
		{
		}

		// Token: 0x06008213 RID: 33299 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearCache()
		{
		}

		// Token: 0x06008214 RID: 33300 RVA: 0x0000216D File Offset: 0x0000036D
		public void Release(int cardid, int taskid = 0)
		{
		}

		// Token: 0x06008215 RID: 33301 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReleaseAll()
		{
		}

		// Token: 0x06008216 RID: 33302 RVA: 0x0000216D File Offset: 0x0000036D
		public void Destroy()
		{
		}

		// Token: 0x06008217 RID: 33303 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetCardTextureAsync(UnityAction<Texture2D, bool> onFinished, int cardid)
		{
			return 0;
		}

		// Token: 0x06008218 RID: 33304 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardQuality(CardQuality quality)
		{
		}

		// Token: 0x06008219 RID: 33305 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool PreLoadCardPictureAsync(int cardid, bool force)
		{
			return false;
		}

		// Token: 0x0600821A RID: 33306 RVA: 0x0000216A File Offset: 0x0000036A
		protected Texture2D GetTexInstance(int cardid, bool force = true)
		{
			return null;
		}

		// Token: 0x0600821B RID: 33307 RVA: 0x0000216D File Offset: 0x0000036D
		protected void ResetTexInstance()
		{
		}

		// Token: 0x0600821C RID: 33308 RVA: 0x0000216D File Offset: 0x0000036D
		protected void CreateTextureImpl(int cardid)
		{
		}

		// Token: 0x0600821D RID: 33309 RVA: 0x0000216D File Offset: 0x0000036D
		protected void CancelTaskImpl(int cardid)
		{
		}

		// Token: 0x0600821E RID: 33310 RVA: 0x0000216D File Offset: 0x0000036D
		protected void CardPictureDebugMessage(int cardid, bool isrelease)
		{
		}

		// Token: 0x0400BA70 RID: 47728
		protected static Texture2D m_DummyCard;

		// Token: 0x0400BA71 RID: 47729
		protected const int CARD_INSTANCE_NUM_BASE = 128;

		// Token: 0x0400BA72 RID: 47730
		protected const int CARD_INSTANCE_NUM_BASE_MOBILE = 128;

		// Token: 0x0400BA73 RID: 47731
		protected const string DUMMYCARDPATH = "Card/dummyCard";

		// Token: 0x0400BA74 RID: 47732
		protected CardTextureCreatorBase m_CardCreator;

		// Token: 0x0400BA75 RID: 47733
		protected List<int> m_NoReferenceCardIdList;

		// Token: 0x0400BA76 RID: 47734
		protected Queue<Texture2D> m_FreeTexInstances;

		// Token: 0x0400BA77 RID: 47735
		protected Dictionary<int, CardTextureManagerBase.TexInfo> m_CardIdTextureTable;

		// Token: 0x0400BA78 RID: 47736
		protected Dictionary<int, Dictionary<int, UnityAction<Texture2D, bool>>> m_CallbackTable;

		// Token: 0x0400BA79 RID: 47737
		protected bool m_EnableCache;

		// Token: 0x0400BA7A RID: 47738
		protected int m_TaskIndex;

		// Token: 0x0400BA7B RID: 47739
		protected string m_Tag;

		// Token: 0x0400BA7C RID: 47740
		protected int m_CardInstanceNumBsae;

		// Token: 0x0400BA7D RID: 47741
		protected int m_AddCardInstanceNum;

		// Token: 0x02001114 RID: 4372
		protected class TexInfo
		{
			// Token: 0x06008220 RID: 33312 RVA: 0x00002739 File Offset: 0x00000939
			public TexInfo(Texture2D tex)
			{
			}

			// Token: 0x0400BA7E RID: 47742
			public Texture2D tex;

			// Token: 0x0400BA7F RID: 47743
			public int refCount;
		}
	}
}
