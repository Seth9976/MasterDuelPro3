using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomGame.Dialog.CommonDialog;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Menu
{
	// Token: 0x02000AA5 RID: 2725
	public class LoginBonusSlotWidget : ElementWidgetBase
	{
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06004F62 RID: 20322 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton button
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06004F63 RID: 20323 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text labelText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06004F64 RID: 20324 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject rewardThumb
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06004F65 RID: 20325 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text rewardNum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06004F66 RID: 20326 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject recievedCover
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06004F67 RID: 20327 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject recieveFocusCover
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06004F68 RID: 20328 RVA: 0x000029CC File Offset: 0x00000BCC
		public int rewardCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public LoginBonusSlotWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x0000216D File Offset: 0x0000036D
		public void BindImage(string label, Sprite image)
		{
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetMode(LoginBonusSlotWidget.Mode mode)
		{
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x0000216D File Offset: 0x0000036D
		public void Ready()
		{
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddData(Dictionary<string, object> source, int slotNumber)
		{
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowItem()
		{
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowObtainedItem(EntryItemListData itemList, Action callback, bool isPresentBoxSent = false)
		{
		}

		// Token: 0x04008D4B RID: 36171
		private const string k_ELabelLabelText = "LabelText";

		// Token: 0x04008D4C RID: 36172
		private const string k_ELabelRewardThumb = "RewardThumb";

		// Token: 0x04008D4D RID: 36173
		private const string k_ELabelRewardNum = "RewardNum";

		// Token: 0x04008D4E RID: 36174
		private const string k_ELabelRecievedCover = "RecievedCover";

		// Token: 0x04008D4F RID: 36175
		private const string k_ELabelRecieveFocusCover = "RecieveFocusCover";

		// Token: 0x04008D50 RID: 36176
		internal const string k_ELabelLabelTextImage = "LabelTextImage";

		// Token: 0x04008D51 RID: 36177
		private LoginBonusSlotWidget.Mode _mode;

		// Token: 0x04008D52 RID: 36178
		private List<LoginBonusSlotWidget.Reward> _rewards;

		// Token: 0x02000AA6 RID: 2726
		public enum Mode
		{
			// Token: 0x04008D54 RID: 36180
			None,
			// Token: 0x04008D55 RID: 36181
			Recieved,
			// Token: 0x04008D56 RID: 36182
			RecieveFocus
		}

		// Token: 0x02000AA7 RID: 2727
		private struct Reward
		{
			// Token: 0x04008D57 RID: 36183
			internal int day;

			// Token: 0x04008D58 RID: 36184
			internal int num;

			// Token: 0x04008D59 RID: 36185
			internal int category;

			// Token: 0x04008D5A RID: 36186
			internal int itemId;

			// Token: 0x04008D5B RID: 36187
			internal bool is_period;
		}
	}
}
