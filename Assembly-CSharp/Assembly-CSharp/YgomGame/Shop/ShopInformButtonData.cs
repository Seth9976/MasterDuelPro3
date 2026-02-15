using System;
using UnityEngine;

namespace YgomGame.Shop
{
	// Token: 0x0200095A RID: 2394
	[Serializable]
	public class ShopInformButtonData
	{
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x0600460F RID: 17935 RVA: 0x000029CC File Offset: 0x00000BCC
		public ShopInformButtonData.Behaviour behaviour
		{
			get
			{
				return ShopInformButtonData.Behaviour.OpenHelp;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06004610 RID: 17936 RVA: 0x0000216A File Offset: 0x0000036A
		public string[] behaviourParams
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06004611 RID: 17937 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopInformButtonData.BehaviourParamFormtData[] behaviourFormatDatas
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06004612 RID: 17938 RVA: 0x000029CC File Offset: 0x00000BCC
		public ShopInformButtonData.ButtonStyle buttonStyle
		{
			get
			{
				return ShopInformButtonData.ButtonStyle.Normal;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06004613 RID: 17939 RVA: 0x0000216A File Offset: 0x0000036A
		public string labelTextId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06004614 RID: 17940 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopInformButtonData.FormatData[] labelFormatDatas
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06004615 RID: 17941 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool skipOnBlockPurchase
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04008469 RID: 33897
		[SerializeField]
		private ShopInformButtonData.Behaviour m_Behaviour;

		// Token: 0x0400846A RID: 33898
		[SerializeField]
		private string[] m_BehaviourParams;

		// Token: 0x0400846B RID: 33899
		[SerializeField]
		private ShopInformButtonData.BehaviourParamFormtData[] m_BehaviourFormatDatas;

		// Token: 0x0400846C RID: 33900
		[SerializeField]
		private ShopInformButtonData.ButtonStyle m_ButtonStyle;

		// Token: 0x0400846D RID: 33901
		[SerializeField]
		private string m_LabelTextId;

		// Token: 0x0400846E RID: 33902
		[SerializeField]
		private ShopInformButtonData.FormatData[] m_LabelFormatDatas;

		// Token: 0x0400846F RID: 33903
		[SerializeField]
		private bool m_SkipOnBlockPurchase;

		// Token: 0x0200095B RID: 2395
		public enum Behaviour
		{
			// Token: 0x04008471 RID: 33905
			OpenHelp,
			// Token: 0x04008472 RID: 33906
			OpenCardPackRateList,
			// Token: 0x04008473 RID: 33907
			OpenCardPoolList,
			// Token: 0x04008474 RID: 33908
			OpenCardPickupList,
			// Token: 0x04008475 RID: 33909
			OpenStructureDeckBrowser,
			// Token: 0x04008476 RID: 33910
			OpenItemViewer,
			// Token: 0x04008477 RID: 33911
			OpenDuelPassRewardList,
			// Token: 0x04008478 RID: 33912
			OpenHelpSwitchDx,
			// Token: 0x04008479 RID: 33913
			OpenHelpSwitchPackType,
			// Token: 0x0400847A RID: 33914
			OpenItemPreview,
			// Token: 0x0400847B RID: 33915
			OpenPrizeList,
			// Token: 0x0400847C RID: 33916
			OpenPrizeResult,
			// Token: 0x0400847D RID: 33917
			OpenCardStandardPackRateList
		}

		// Token: 0x0200095C RID: 2396
		public enum BehaviourParamFormtData
		{
			// Token: 0x0400847F RID: 33919
			ShopId = 10,
			// Token: 0x04008480 RID: 33920
			PackId
		}

		// Token: 0x0200095D RID: 2397
		public enum ButtonStyle
		{
			// Token: 0x04008482 RID: 33922
			Normal,
			// Token: 0x04008483 RID: 33923
			Small,
			// Token: 0x04008484 RID: 33924
			Highlight
		}

		// Token: 0x0200095E RID: 2398
		public enum FormatData
		{
			// Token: 0x04008486 RID: 33926
			ProductName = 1,
			// Token: 0x04008487 RID: 33927
			ProductSubLabel
		}
	}
}
