using System;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000EDE RID: 3806
	public class PlaceStatusLabel : MonoBehaviour
	{
		// Token: 0x06006F03 RID: 28419 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(PlaceStatusManager manager)
		{
		}

		// Token: 0x06006F04 RID: 28420 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup(SharedDefinition.Location location, bool lieDown)
		{
		}

		// Token: 0x06006F05 RID: 28421 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show(bool immediate)
		{
		}

		// Token: 0x06006F06 RID: 28422 RVA: 0x0000216D File Offset: 0x0000036D
		public void Hide(bool immediate)
		{
		}

		// Token: 0x06006F07 RID: 28423 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFinishedTweenShow()
		{
		}

		// Token: 0x06006F08 RID: 28424 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnFinishedTweenHide()
		{
		}

		// Token: 0x06006F09 RID: 28425 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateNumCards(Vector2 scrPos, int numCards)
		{
		}

		// Token: 0x06006F0A RID: 28426 RVA: 0x0000216D File Offset: 0x0000036D
		private void ApplyPosition(GameObject go, Vector2 scrPos)
		{
		}

		// Token: 0x0400AA19 RID: 43545
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x0400AA1A RID: 43546
		private ElementObjectManager ui;

		// Token: 0x0400AA1B RID: 43547
		private Vector2 defaultSize;

		// Token: 0x0400AA1C RID: 43548
		private ExtendedTextMeshProUGUI numCardsValue;

		// Token: 0x0400AA1D RID: 43549
		private int preNumCardValue;

		// Token: 0x0400AA1E RID: 43550
		private bool hiding;

		// Token: 0x0400AA1F RID: 43551
		private Camera uiCamera;

		// Token: 0x0400AA20 RID: 43552
		private PlaceStatusManager manager;
	}
}
