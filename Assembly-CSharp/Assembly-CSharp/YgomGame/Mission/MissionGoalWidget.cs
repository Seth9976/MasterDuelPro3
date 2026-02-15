using System;
using System.Collections;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Mission
{
	// Token: 0x02000A31 RID: 2609
	public class MissionGoalWidget : ElementWidgetBase
	{
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06004BB1 RID: 19377 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool existsRecievedIcon
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06004BB2 RID: 19378 RVA: 0x0000216A File Offset: 0x0000036A
		private GameObject recievedIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06004BB3 RID: 19379 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text countText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06004BB4 RID: 19380 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject rewardThumbHolder
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06004BB5 RID: 19381 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text rewardNumText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MissionGoalWidget(ElementObjectManager eom, MissionGoalWidget.GoalType goalType)
			: base(null)
		{
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRecieveBetweenWaitSpeed(float speed)
		{
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayingRecieveBetweenWait()
		{
			return false;
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator yPlayRecieveBetweenWait()
		{
			return null;
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRecievedSpeed(float speed)
		{
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayRecieved()
		{
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayRecieved()
		{
			return null;
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlayingRecieved()
		{
			return false;
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRecieved()
		{
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearRecieved()
		{
		}

		// Token: 0x040089A1 RID: 35233
		private readonly string k_ELabelCountText;

		// Token: 0x040089A2 RID: 35234
		private readonly string k_ELabelRewardThumbHolder;

		// Token: 0x040089A3 RID: 35235
		private readonly string k_ELabelRewardNumText;

		// Token: 0x040089A4 RID: 35236
		private const string k_ELabelRecievedIcon = "RecievedIcon";

		// Token: 0x040089A5 RID: 35237
		private const string k_TLabelOnRecieveBetweenWait = "OnRecieveBetweenWait";

		// Token: 0x040089A6 RID: 35238
		private const string k_TLabelOnRecieved = "OnRecieved";

		// Token: 0x040089A7 RID: 35239
		public readonly MissionGoalWidget.GoalType goalType;

		// Token: 0x02000A32 RID: 2610
		public enum GoalType
		{
			// Token: 0x040089A9 RID: 35241
			None,
			// Token: 0x040089AA RID: 35242
			InProgress,
			// Token: 0x040089AB RID: 35243
			Recievable,
			// Token: 0x040089AC RID: 35244
			Complete
		}
	}
}
