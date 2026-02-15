using System;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013AC RID: 5036
	public class SelectionButton_RoomPlayer : SelectionButton
	{
		// Token: 0x060091EB RID: 37355 RVA: 0x0014555E File Offset: 0x0014375E
		protected override void CallHoverOnEvent()
		{
			base.CallHoverOnEvent();
			if (RoomServant.IsHost && this.playerIndex != RoomServant.SelfType)
			{
				base.Manager.GetElement("KickIcon").SetActive(true);
			}
		}

		// Token: 0x060091EC RID: 37356 RVA: 0x00145590 File Offset: 0x00143790
		protected override void CallHoverOffEvent()
		{
			base.CallHoverOffEvent();
			base.Manager.GetElement("KickIcon").SetActive(false);
		}

		// Token: 0x060091ED RID: 37357 RVA: 0x001455B0 File Offset: 0x001437B0
		protected override void OnSubmit()
		{
			base.OnSubmit();
			if (RoomServant.IsHost)
			{
				if (this.playerIndex != RoomServant.SelfType)
				{
					Program.instance.room.GetUI<RoomServantUI>().OnKick(this.playerIndex);
					return;
				}
				Program.instance.room.GetUI<RoomServantUI>().OnReady();
			}
		}

		// Token: 0x060091EE RID: 37358 RVA: 0x00145606 File Offset: 0x00143806
		public Image GetAvatar()
		{
			return base.Manager.GetElement<Image>("Avatar");
		}

		// Token: 0x060091EF RID: 37359 RVA: 0x00145618 File Offset: 0x00143818
		public void SetReadyIcon(bool ready)
		{
			base.Manager.GetElement("ReadyIcon").SetActive(ready);
		}

		// Token: 0x060091F0 RID: 37360 RVA: 0x00145630 File Offset: 0x00143830
		public new void SetButtonTextColor(Color color)
		{
			base.Manager.GetElement<TextMeshProUGUI>("ButtonText").color = color;
		}

		// Token: 0x060091F1 RID: 37361 RVA: 0x00145648 File Offset: 0x00143848
		protected override void OnDisable()
		{
			base.OnDisable();
			base.Manager.GetElement("KickIcon").SetActive(false);
		}

		// Token: 0x0400D093 RID: 53395
		[Header("SelectionButton RoomPlayer")]
		public int playerIndex;
	}
}
