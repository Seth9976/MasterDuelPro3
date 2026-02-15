using System;
using Cysharp.Threading.Tasks;
using MDPro3.Net;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013D6 RID: 5078
	public class SelectionToggle_Watch : SelectionToggle_ScrollRectItem
	{
		// Token: 0x06009323 RID: 37667 RVA: 0x0014B6B9 File Offset: 0x001498B9
		public override void Refresh()
		{
			base.Refresh();
			base.Manager.GetElement<TextMeshProUGUI>("Player0Name").text = this.player0Name;
			base.Manager.GetElement<TextMeshProUGUI>("Player1Name").text = this.player1Name;
		}

		// Token: 0x06009324 RID: 37668 RVA: 0x0014B6F8 File Offset: 0x001498F8
		protected override async UniTask RefreshAsync()
		{
			this.refreshed = false;
			base.Manager.GetElement<RawImage>("Face0").texture = Appearance.defaultFace0.texture;
			base.Manager.GetElement<RawImage>("Face1").texture = Appearance.defaultFace1.texture;
			RawImage rawImage = base.Manager.GetElement<RawImage>("Face0");
			Texture2D texture2D = await MyCard.GetAvatarAsync(this.player0Name);
			rawImage.texture = texture2D;
			rawImage = null;
			rawImage = base.Manager.GetElement<RawImage>("Face1");
			texture2D = await MyCard.GetAvatarAsync(this.player1Name);
			rawImage.texture = texture2D;
			rawImage = null;
			this.refreshed = true;
		}

		// Token: 0x06009325 RID: 37669 RVA: 0x0014B73B File Offset: 0x0014993B
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			Program.instance.online.lastSelectedWatchItem = this;
		}

		// Token: 0x06009326 RID: 37670 RVA: 0x0014B753 File Offset: 0x00149953
		protected override void CallSubmitEvent()
		{
			AudioManager.PlaySE("SE_MENU_DECIDE", 1f);
			base.CallSubmitEvent();
			this.WaitPasswordToJoin();
		}

		// Token: 0x06009327 RID: 37671 RVA: 0x0014B774 File Offset: 0x00149974
		private async UniTask WaitPasswordToJoin()
		{
			string password = await MyCard.GetJoinRoomPassword(this.options, this.roomId, MyCard.account.user.id, false);
			TcpHelper.LinkStart("tiramisu.moenext.com", MyCard.account.user.username, 8911.ToString(), password, false, null);
		}

		// Token: 0x06009328 RID: 37672 RVA: 0x0014B7B7 File Offset: 0x001499B7
		protected override void OnClick()
		{
			Program.instance.online.lastSelectedWatchItem = this;
			this.CallSubmitEvent();
		}

		// Token: 0x06009329 RID: 37673 RVA: 0x001465A5 File Offset: 0x001447A5
		protected override void ToggleOn()
		{
			this.isOn = true;
		}

		// Token: 0x0600932A RID: 37674 RVA: 0x001465A5 File Offset: 0x001447A5
		public override void ToggleOnNow()
		{
			this.isOn = true;
		}

		// Token: 0x0600932B RID: 37675 RVA: 0x001465AE File Offset: 0x001447AE
		protected override void ToggleOff()
		{
			this.isOn = false;
		}

		// Token: 0x0600932C RID: 37676 RVA: 0x001465AE File Offset: 0x001447AE
		public override void ToggleOffNow()
		{
			this.isOn = false;
		}

		// Token: 0x0600932D RID: 37677 RVA: 0x0014B7D0 File Offset: 0x001499D0
		protected override void OnNavigation(AxisEventData eventData)
		{
			int selfIndex = this.index;
			if (selfIndex < 0)
			{
				selfIndex = base.transform.GetSiblingIndex();
			}
			int count = Program.instance.online.GetUI<OnlineServantUI>().PageMyCard.WatchList.superScrollView.items.Count;
			int columes = Program.instance.online.GetUI<OnlineServantUI>().PageMyCard.WatchList.superScrollView.GetColumnCount();
			int targetIndex = selfIndex + 1;
			if (eventData.moveDir == MoveDirection.Left)
			{
				if (selfIndex % columes == 0)
				{
					Program.instance.online.GetUI<OnlineServantUI>().PageMyCard.ButtonDeckSelector.GetSelectable().Select();
					return;
				}
				targetIndex = this.index - 1;
			}
			else if (eventData.moveDir == MoveDirection.Right)
			{
				if (selfIndex % columes == columes - 1 || this.index == count - 1)
				{
					Program.instance.online.GetUI<OnlineServantUI>().PageMyCard.SelectDefault();
					return;
				}
			}
			else if (eventData.moveDir == MoveDirection.Up)
			{
				targetIndex = selfIndex - columes;
			}
			else if (eventData.moveDir == MoveDirection.Down)
			{
				int lastLineLeft = count % columes;
				int bound = count - lastLineLeft - 1;
				if (lastLineLeft == 0)
				{
					bound -= columes;
				}
				if (selfIndex > bound)
				{
					return;
				}
				targetIndex = this.index + columes;
			}
			if (targetIndex < 0)
			{
				return;
			}
			if (targetIndex >= count)
			{
				targetIndex = count - 1;
			}
			if (targetIndex == this.index)
			{
				return;
			}
			for (int i = 0; i < base.transform.parent.childCount; i++)
			{
				Transform child = base.transform.parent.GetChild(i);
				if (child.gameObject.activeSelf)
				{
					int buttonIndex = child.GetComponent<SelectionButton>().index;
					if (buttonIndex < 0)
					{
						buttonIndex = i;
					}
					if (buttonIndex == targetIndex)
					{
						UserInput.NextSelectionIsAxis = true;
						EventSystem.current.SetSelectedGameObject(base.transform.parent.GetChild(i).gameObject);
						return;
					}
				}
			}
		}

		// Token: 0x0400D175 RID: 53621
		[Header("SelectionToggle Watch")]
		public string roomId;

		// Token: 0x0400D176 RID: 53622
		public string roomTitile;

		// Token: 0x0400D177 RID: 53623
		public string player0Name;

		// Token: 0x0400D178 RID: 53624
		public string player1Name;

		// Token: 0x0400D179 RID: 53625
		public string arena;

		// Token: 0x0400D17A RID: 53626
		public MyCardRoomOptions options = new MyCardRoomOptions();
	}
}
