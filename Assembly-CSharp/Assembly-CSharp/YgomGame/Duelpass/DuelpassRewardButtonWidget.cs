using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C43 RID: 3139
	public class DuelpassRewardButtonWidget : ElementWidgetBase
	{
		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06005987 RID: 22919 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005988 RID: 22920 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionButton Button
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06005989 RID: 22921 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600598A RID: 22922 RVA: 0x0000216D File Offset: 0x0000036D
		public int RewardId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x0600598B RID: 22923 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600598C RID: 22924 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsReceivable
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x0600598D RID: 22925 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public DuelpassRewardButtonWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x0600598E RID: 22926 RVA: 0x0000216D File Offset: 0x0000036D
		public void Init(int rewardId)
		{
		}

		// Token: 0x0600598F RID: 22927 RVA: 0x0000216D File Offset: 0x0000036D
		public void Off()
		{
		}

		// Token: 0x06005990 RID: 22928 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClick()
		{
		}

		// Token: 0x06005991 RID: 22929 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateButtonState(DuelpassRewardContext context)
		{
		}

		// Token: 0x06005992 RID: 22930 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06005993 RID: 22931 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReceivedCondi()
		{
			return false;
		}

		// Token: 0x04009543 RID: 38211
		private TMP_Text countText;

		// Token: 0x04009544 RID: 38212
		private GameObject rewardThumbHolder;

		// Token: 0x04009545 RID: 38213
		private GameObject completedOutline;

		// Token: 0x04009546 RID: 38214
		private GameObject progressOutline;

		// Token: 0x04009547 RID: 38215
		private GameObject recievableOutline;

		// Token: 0x04009548 RID: 38216
		private TMP_Text rewardNumText;

		// Token: 0x04009549 RID: 38217
		private GameObject recievedIcon;

		// Token: 0x0400954A RID: 38218
		private GameObject completedShadow;

		// Token: 0x0400954B RID: 38219
		private GameObject reward;

		// Token: 0x0400954C RID: 38220
		private GameObject body;

		// Token: 0x0400954D RID: 38221
		private GameObject blankLine;

		// Token: 0x0400954E RID: 38222
		private RectTransform rewardThumbHolderRT;

		// Token: 0x0400954F RID: 38223
		private Vector2 originalOffsetMin;

		// Token: 0x04009550 RID: 38224
		private Vector2 originalOffsetMax;
	}
}
