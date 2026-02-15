using System;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Duel
{
	// Token: 0x02000CD5 RID: 3285
	public class CardInfoDetailForDuel : CardInfoDetail
	{
		// Token: 0x06005DF0 RID: 24048 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(DuelClient host, Transform parent, UnityAction<CardInfoDetailForDuel> finishedCallback)
		{
		}

		// Token: 0x06005DF1 RID: 24049 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void InitializeBase()
		{
		}

		// Token: 0x06005DF2 RID: 24050 RVA: 0x0000216D File Offset: 0x0000036D
		public new void Show()
		{
		}

		// Token: 0x06005DF3 RID: 24051 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitToggle()
		{
		}

		// Token: 0x06005DF4 RID: 24052 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetCardByOriginalInfo()
		{
		}

		// Token: 0x04009988 RID: 39304
		protected const string PATH_PREHABFORDUEL = "Prefabs/Duel/UI/CardInfoDetailForDuel";

		// Token: 0x04009989 RID: 39305
		private ToggleWidget m_OriginInfoToggle;

		// Token: 0x0400998A RID: 39306
		private SelectionButton m_RelativeCardButton;

		// Token: 0x0400998B RID: 39307
		private DuelClient m_Host;

		// Token: 0x0400998C RID: 39308
		private bool m_BlcokRelatriveCard;
	}
}
