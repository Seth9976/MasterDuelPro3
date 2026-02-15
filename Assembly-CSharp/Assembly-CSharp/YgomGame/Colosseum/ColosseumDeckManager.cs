using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Colosseum
{
	// Token: 0x02001027 RID: 4135
	public class ColosseumDeckManager
	{
		// Token: 0x06007C2A RID: 31786 RVA: 0x00002739 File Offset: 0x00000939
		public ColosseumDeckManager(ColosseumUtil.PlayMode playMode, int identifier = 0)
		{
		}

		// Token: 0x06007C2B RID: 31787 RVA: 0x00002739 File Offset: 0x00000939
		public ColosseumDeckManager(ColosseumUtil.PlayMode playMode, bool isRental, int identifier = 0)
		{
		}

		// Token: 0x06007C2C RID: 31788 RVA: 0x00002739 File Offset: 0x00000939
		public ColosseumDeckManager(ColosseumPathManager pathManager)
		{
		}

		// Token: 0x06007C2D RID: 31789 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCaseWidget SetDeckCase(Dictionary<string, object> deckDic, DeckCaseWidget deckCase, Transform parent)
		{
			return null;
		}

		// Token: 0x06007C2E RID: 31790 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickDeck(bool isSetDeck, ViewControllerManager manager, int id, int logoId, ElementObjectManager overviewEOM, Action<int, bool> openDeckEditCallback, int stage = 0)
		{
		}

		// Token: 0x06007C2F RID: 31791 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject CreateEmbedObj(int logoId, ElementObjectManager overviewEOM, int stage, string deckName = "")
		{
			return null;
		}

		// Token: 0x06007C30 RID: 31792 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<string, object> GetDeckList(int rentalState = 0)
		{
			return null;
		}

		// Token: 0x06007C31 RID: 31793 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> GetDeckAccessory()
		{
			return null;
		}

		// Token: 0x06007C32 RID: 31794 RVA: 0x0000216A File Offset: 0x0000036A
		private int[] GetDeckPickUpCardIDs()
		{
			return null;
		}

		// Token: 0x06007C33 RID: 31795 RVA: 0x0000216A File Offset: 0x0000036A
		private int[] GetDeckPickUpCardDecorations()
		{
			return null;
		}

		// Token: 0x06007C34 RID: 31796 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetDeckNameTextID()
		{
			return null;
		}

		// Token: 0x0400B3FE RID: 46078
		public ColosseumPathManager pm;
	}
}
