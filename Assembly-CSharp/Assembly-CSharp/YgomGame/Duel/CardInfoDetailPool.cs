using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Duel
{
	// Token: 0x02000CD6 RID: 3286
	public class CardInfoDetailPool : MonoBehaviour
	{
		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06005DF6 RID: 24054 RVA: 0x0000216A File Offset: 0x0000036A
		private static CardInfoDetailPool instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005DF7 RID: 24055 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetReusableCount(bool flag)
		{
			return 0;
		}

		// Token: 0x06005DF8 RID: 24056 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06005DF9 RID: 24057 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CreateOrReuse(Transform parent, Action finishedCallback, bool flag = false)
		{
		}

		// Token: 0x06005DFA RID: 24058 RVA: 0x0000216D File Offset: 0x0000036D
		private void InnerCreateOrReuse(Transform parent, Action finishedCallback, bool flag = false)
		{
		}

		// Token: 0x06005DFB RID: 24059 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ReserveToReuse(Action finishedCallback, bool flag = false)
		{
		}

		// Token: 0x06005DFC RID: 24060 RVA: 0x0000216D File Offset: 0x0000036D
		private void InnerReserveToReuse(Action finishedCallback, bool flag = false)
		{
		}

		// Token: 0x06005DFD RID: 24061 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ReturnToReuse(CardInfoDetail cardInfoDetail, bool flag = false)
		{
		}

		// Token: 0x06005DFE RID: 24062 RVA: 0x0000216D File Offset: 0x0000036D
		private void InnerReturnToReuse(CardInfoDetail cardInfoDetail, bool flag = false)
		{
		}

		// Token: 0x06005DFF RID: 24063 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CreateOrReuseRelativeList(Transform parent, Action<CardInfoDetailPool.RelativeList> finishedCallback)
		{
		}

		// Token: 0x06005E00 RID: 24064 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ReturnToReuseRelative(CardInfoDetailPool.RelativeList relativeList)
		{
		}

		// Token: 0x0400998D RID: 39309
		private static CardInfoDetailPool s_Instance;

		// Token: 0x0400998E RID: 39310
		private Stack<CardInfoDetail> m_CardDetailPool;

		// Token: 0x0400998F RID: 39311
		private Stack<CardInfoDetail> m_DownloadCardDetailPool;

		// Token: 0x04009990 RID: 39312
		private Stack<CardInfoDetailPool.RelativeList> m_RelativeListPool;

		// Token: 0x02000CD7 RID: 3287
		public class RelativeList
		{
			// Token: 0x17000A2C RID: 2604
			// (get) Token: 0x06005E02 RID: 24066 RVA: 0x0000216A File Offset: 0x0000036A
			public GenericCardListEx widget
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000A2D RID: 2605
			// (get) Token: 0x06005E03 RID: 24067 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isStandby
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06005E04 RID: 24068 RVA: 0x0000216D File Offset: 0x0000036D
			public static void Create(Transform parent, Action<CardInfoDetailPool.RelativeList> finishedCallback)
			{
			}

			// Token: 0x06005E05 RID: 24069 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnClickCard(int mrk)
			{
			}

			// Token: 0x04009991 RID: 39313
			private GenericCardListEx m_Widget;

			// Token: 0x04009992 RID: 39314
			private GenericScrollView m_ScrollView;

			// Token: 0x04009993 RID: 39315
			public Action<int> onClickCard;
		}
	}
}
