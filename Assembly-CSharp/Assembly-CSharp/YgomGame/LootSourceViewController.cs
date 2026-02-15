using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Menu;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame
{
	// Token: 0x020007CB RID: 1995
	public class LootSourceViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06003E4E RID: 15950 RVA: 0x0000216A File Offset: 0x0000036A
		private RawImage m_CardImage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06003E4F RID: 15951 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton m_BackButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06003E50 RID: 15952 RVA: 0x0000216A File Offset: 0x0000036A
		public InfinityScrollView m_InfinityScroll
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06003E51 RID: 15953 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeScroll()
		{
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnCreateEntity(GameObject obj)
		{
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnUpdateEntity(GameObject obj, int idx)
		{
		}

		// Token: 0x04003739 RID: 14137
		private const string LABEL_IMG_IMAGECARD = "ImageCard";

		// Token: 0x0400373A RID: 14138
		private const string LABEL_SCV_SOURCESCROLL = "SourceScroll";

		// Token: 0x0400373B RID: 14139
		private const string LABEL_SBN_BACK = "Back";

		// Token: 0x0400373C RID: 14140
		private const string LABEL_EMPTY_MESSAGE_ROOT = "EmptyMessageRoot";

		// Token: 0x0400373D RID: 14141
		public const string argsKeyCardID = "CardID";

		// Token: 0x0400373E RID: 14142
		public const string argsKeyCallBack = "DecideCallback";

		// Token: 0x0400373F RID: 14143
		private int m_CardID;

		// Token: 0x04003740 RID: 14144
		private GameObject m_EmptyMessageRoot;

		// Token: 0x04003741 RID: 14145
		private List<object> m_DataList;

		// Token: 0x04003742 RID: 14146
		private List<int> m_ShopList;

		// Token: 0x04003743 RID: 14147
		private Action<int, List<int>> decideCallback;
	}
}
